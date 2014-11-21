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
using System.Threading;
using OpenMetaverse;
using OpenMetaverse.Assets;

namespace OpenMetaverse.TestClient
{
    public class DownloadCommand : Command
    {
        UUID AssetID;
        AssetType assetType;
        AutoResetEvent DownloadHandle = new AutoResetEvent(false);
        bool Success;
        string usage = "Usage: download [uuid] [assetType]";

        public DownloadCommand(TestClient testClient)
        {
            Name = "download";
            Description = "Downloads the specified asset. Usage: download [uuid] [assetType]";
            Category = CommandCategory.Inventory;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length != 2)
                return usage;

            Success = false;
            AssetID = UUID.Zero;
            assetType = AssetType.Unknown;
            DownloadHandle.Reset();

            if (!UUID.TryParse(args[0], out AssetID))
                return usage;

            try {
                assetType = (AssetType)Enum.Parse(typeof(AssetType), args[1], true);
            } catch (ArgumentException) {
                return usage;
            }
            if (!Enum.IsDefined(typeof(AssetType), assetType))
                return usage;

            // Start the asset download
            Client.Assets.RequestAsset(AssetID, assetType, true, Assets_OnAssetReceived);

            if (DownloadHandle.WaitOne(120 * 1000, false))
            {
                if (Success)
                    return String.Format("Saved {0}.{1}", AssetID, assetType.ToString().ToLower());
                else
                    return String.Format("Failed to download asset {0}, perhaps {1} is the incorrect asset type?",
                        AssetID, assetType);
            }
            else
            {
                return "Timed out waiting for texture download";
            }
        }

        private void Assets_OnAssetReceived(AssetDownload transfer, Asset asset)
        {
            if (transfer.AssetID == AssetID)
            {
                if (transfer.Success)
                {
                    try
                    {
                        File.WriteAllBytes(String.Format("{0}.{1}", AssetID,
                            assetType.ToString().ToLower()), asset.AssetData);
                        Success = true;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message, Helpers.LogLevel.Error, ex);
                    }
                }

                DownloadHandle.Set();
            }
        }
    }
}
