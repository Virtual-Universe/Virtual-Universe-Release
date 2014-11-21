/***************************************************************************
 *	                VIRTUAL REALITY PUBLIC SOURCE LICENSE
 * 
 * Date				: Sun January 1, 2006
 * Copyright		: (c) 2006-2014 by Virtual Reality Development Team. 
 *                    All Rights Reserved.
 * Website			: http://www.syndarveruleiki.is
 *
 * Product Name		: Virtual Reality
 * License Text     : packages/docs/VRLICENSE.txt
 * 
 * Planetary Info   : Information about the Planetary code
 * 
 * Copyright        : (c) 2014-2024 by Second Galaxy Development Team
 *                    All Rights Reserved.
 * 
 * Website          : http://www.secondgalaxy.com
 * 
 * Product Name     : Virtual Reality
 * License Text     : packages/docs/SGLICENSE.txt
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the WhiteCore-Sim Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
***************************************************************************/

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using OpenMetaverse;

namespace OpenMetaverse.TestClient
{
    /// <summary>
    /// Request the raw terrain file from the simulator, save it as a file.
    /// 
    /// Can only be used by the Estate Owner
    /// </summary>
    public class DownloadTerrainCommand : Command
    {
        /// <summary>
        /// Create a Synchronization event object
        /// </summary>
        private static AutoResetEvent xferTimeout = new AutoResetEvent(false);

        /// <summary>A string we use to report the result of the request with.</summary>
        private static System.Text.StringBuilder result = new System.Text.StringBuilder();

        private static string fileName;

        /// <summary>
        /// Download a simulators raw terrain data and save it to a file
        /// </summary>
        /// <param name="testClient"></param>
        public DownloadTerrainCommand(TestClient testClient)
        {
            Name = "downloadterrain";
            Description = "Download the RAW terrain file for this estate. Usage: downloadterrain [timeout]";
            Category = CommandCategory.Simulator;
        }

        /// <summary>
        /// Execute the application
        /// </summary>
        /// <param name="args">arguments passed to this module</param>
        /// <param name="fromAgentID">The ID of the avatar sending the request</param>
        /// <returns></returns>
        public override string Execute(string[] args, UUID fromAgentID)
        {
            int timeout = 120000; // default the timeout to 2 minutes
            fileName = Client.Network.CurrentSim.Name + ".raw";

            if (args.Length > 0 && int.TryParse(args[0], out timeout) != true)
                return "Usage: downloadterrain [timeout]";

            // Create a delegate which will be fired when the simulator receives our download request
            // Starts the actual transfer request
            EventHandler<InitiateDownloadEventArgs> initiateDownloadDelegate =
                delegate(object sender, InitiateDownloadEventArgs e)
                {
                    Client.Assets.RequestAssetXfer(e.SimFileName, false, false, UUID.Zero, AssetType.Unknown, false);
                };

            // Subscribe to the event that will tell us the status of the download
            Client.Assets.XferReceived += new EventHandler<XferReceivedEventArgs>(Assets_XferReceived);
            // subscribe to the event which tells us when the simulator has received our request
            Client.Assets.InitiateDownload += initiateDownloadDelegate;

            // configure request to tell the simulator to send us the file
            List<string> parameters = new List<string>();
            parameters.Add("download filename");
            parameters.Add(fileName);
            // send the request
            Client.Estate.EstateOwnerMessage("terrain", parameters);

            // wait for (timeout) seconds for the request to complete (defaults 2 minutes)
            if (!xferTimeout.WaitOne(timeout, false))
            {
                result.Append("Timeout while waiting for terrain data");
            }

            // unsubscribe from events
            Client.Assets.InitiateDownload -= initiateDownloadDelegate;
            Client.Assets.XferReceived -= new EventHandler<XferReceivedEventArgs>(Assets_XferReceived);

            // return the result
            return result.ToString();
        }

        /// <summary>
        /// Handle the reply to the OnXferReceived event
        /// </summary>
        private void Assets_XferReceived(object sender, XferReceivedEventArgs e)
        {
            if (e.Xfer.Success)
            {
                // set the result message
                result.AppendFormat("Terrain file {0} ({1} bytes) downloaded successfully, written to {2}", e.Xfer.Filename, e.Xfer.Size, fileName);

                // write the file to disk
                FileStream stream = new FileStream(fileName, FileMode.Create);
                BinaryWriter w = new BinaryWriter(stream);
                w.Write(e.Xfer.AssetData);
                w.Close();

                // tell the application we've gotten the file
                xferTimeout.Set();
            }
        }
    }
}
