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
using OpenMetaverse;
using OpenMetaverse.Packets;
namespace OpenMetaverse.TestClient
{
    public class UploadRawTerrainCommand : Command
    {
        System.Threading.AutoResetEvent WaitForUploadComplete = new System.Threading.AutoResetEvent(false);

        public UploadRawTerrainCommand(TestClient testClient)
        {
            Name = "uploadterrain";
            Description = "Upload a raw terrain file to a simulator. usage: uploadterrain filename";
            Category = CommandCategory.Simulator;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            string fileName = String.Empty;

            if (args.Length != 1)
                return "Usage: uploadterrain filename";


            fileName = args[0];

            if (!System.IO.File.Exists(fileName))
            {
                return String.Format("File {0} Does not exist", fileName);
            }

            // Setup callbacks for upload request reply and progress indicator 
            // so we can detect when the upload is complete
            Client.Assets.UploadProgress += new EventHandler<AssetUploadEventArgs>(Assets_UploadProgress);
            byte[] fileData = File.ReadAllBytes(fileName);

            Client.Estate.UploadTerrain(fileData, fileName);

            // Wait for upload to complete. Upload request is fired in callback from first request
            if (!WaitForUploadComplete.WaitOne(120000, false))
            {
                Cleanup();
                return "Timeout waiting for terrain file upload";
            }
            else
            {
                Cleanup();
                return "Terrain raw file uploaded and applied";
            }
        }

        /// <summary>
        /// Unregister previously subscribed event handlers
        /// </summary>
        private void Cleanup()
        {
            Client.Assets.UploadProgress -= new EventHandler<AssetUploadEventArgs>(Assets_UploadProgress);
        }


        void Assets_UploadProgress(object sender, AssetUploadEventArgs e)
        {
            if (e.Upload.Transferred == e.Upload.Size)
            {
                WaitForUploadComplete.Set();
            }
            else
            {
                //Console.WriteLine("Progress: {0}/{1} {2}/{3} {4}", upload.XferID, upload.ID, upload.Transferred, upload.Size, upload.Success);
                Console.Write(".");
            }
        }


    }
}
