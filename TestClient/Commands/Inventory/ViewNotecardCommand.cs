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
using System.Collections.Generic;
using OpenMetaverse;
using OpenMetaverse.Packets;
using OpenMetaverse.Assets;

namespace OpenMetaverse.TestClient
{
    public class ViewNotecardCommand : Command
    {
        /// <summary>
        /// TestClient command to download and display a notecard asset
        /// </summary>
        /// <param name="testClient"></param>
        public ViewNotecardCommand(TestClient testClient)
        {
            Name = "viewnote";
            Description = "Downloads and displays a notecard asset";
            Category = CommandCategory.Inventory;
        }

        /// <summary>
        /// Exectute the command
        /// </summary>
        /// <param name="args"></param>
        /// <param name="fromAgentID"></param>
        /// <returns></returns>
        public override string Execute(string[] args, UUID fromAgentID)
        {

            if (args.Length < 1)
            {
                return "Usage: viewnote [notecard asset uuid]";
            }
            UUID note;
            if (!UUID.TryParse(args[0], out note))
            {
                return "First argument expected agent UUID.";
            }

            System.Threading.AutoResetEvent waitEvent = new System.Threading.AutoResetEvent(false);

            System.Text.StringBuilder result = new System.Text.StringBuilder();

            // verify asset is loaded in store
            if (Client.Inventory.Store.Contains(note))
            {
                // retrieve asset from store
                InventoryItem ii = (InventoryItem)Client.Inventory.Store[note];

                // make request for asset
                Client.Assets.RequestInventoryAsset(ii, true,
                    delegate(AssetDownload transfer, Asset asset)
                    {
                        if (transfer.Success)
                        {
                            result.AppendFormat("Raw Notecard Data: " + System.Environment.NewLine + " {0}", Utils.BytesToString(asset.AssetData));
                            waitEvent.Set();
                        }
                    }
                );

                // wait for reply or timeout
                if (!waitEvent.WaitOne(10000, false))
                {
                    result.Append("Timeout waiting for notecard to download.");
                }
            }
            else
            {
                result.Append("Cannot find asset in inventory store, use 'i' to populate store");
            }

            // return results
            return result.ToString();
        }
    }
}
