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
using System.IO;
using System.Text;
using System.Threading;
using OpenMetaverse;
using OpenMetaverse.Assets;

namespace OpenMetaverse.TestClient
{
    public class CreateNotecardCommand : Command
    {
        const int NOTECARD_CREATE_TIMEOUT = 1000 * 10;
        const int NOTECARD_FETCH_TIMEOUT = 1000 * 10;
        const int INVENTORY_FETCH_TIMEOUT = 1000 * 10;

        public CreateNotecardCommand(TestClient testClient)
        {
            Name = "createnotecard";
            Description = "Creates a notecard from a local text file and optionally embed an inventory item. Usage: createnotecard filename.txt [itemid]";
            Category = CommandCategory.Inventory;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            UUID embedItemID = UUID.Zero, notecardItemID = UUID.Zero, notecardAssetID = UUID.Zero;
            string filename, fileData;
            bool success = false, finalUploadSuccess = false;
            string message = String.Empty;
            AutoResetEvent notecardEvent = new AutoResetEvent(false);

            if (args.Length == 1)
            {
                filename = args[0];
            }
            else if (args.Length == 2)
            {
                filename = args[0];
                UUID.TryParse(args[1], out embedItemID);
            }
            else
            {
                return "Usage: createnotecard filename.txt";
            }

            if (!File.Exists(filename))
                return "File \"" + filename + "\" does not exist";

            try { fileData = File.ReadAllText(filename); }
            catch (Exception ex) { return "Failed to open " + filename + ": " + ex.Message; }

            #region Notecard asset data

            AssetNotecard notecard = new AssetNotecard();
            notecard.BodyText = fileData;

            // Item embedding
            if (embedItemID != UUID.Zero)
            {
                // Try to fetch the inventory item
                InventoryItem item = FetchItem(embedItemID);
                if (item != null)
                {
                    notecard.EmbeddedItems = new List<InventoryItem> { item };
                    notecard.BodyText += (char)0xdbc0 + (char)0xdc00;
                }
                else
                {
                    return "Failed to fetch inventory item " + embedItemID;
                }
            }

            notecard.Encode();

            #endregion Notecard asset data

            Client.Inventory.RequestCreateItem(Client.Inventory.FindFolderForType(AssetType.Notecard),
                filename, filename + " created by OpenMetaverse TestClient " + DateTime.Now, AssetType.Notecard,
                UUID.Random(), InventoryType.Notecard, PermissionMask.All,
                delegate(bool createSuccess, InventoryItem item)
                {
                    if (createSuccess)
                    {
                        #region Upload an empty notecard asset first

                        AutoResetEvent emptyNoteEvent = new AutoResetEvent(false);
                        AssetNotecard empty = new AssetNotecard();
                        empty.BodyText = "\n";
                        empty.Encode();

                        Client.Inventory.RequestUploadNotecardAsset(empty.AssetData, item.UUID,
                            delegate(bool uploadSuccess, string status, UUID itemID, UUID assetID)
                            {
                                notecardItemID = itemID;
                                notecardAssetID = assetID;
                                success = uploadSuccess;
                                message = status ?? "Unknown error uploading notecard asset";
                                emptyNoteEvent.Set();
                            });

                        emptyNoteEvent.WaitOne(NOTECARD_CREATE_TIMEOUT, false);

                        #endregion Upload an empty notecard asset first

                        if (success)
                        {
                            // Upload the actual notecard asset
                            Client.Inventory.RequestUploadNotecardAsset(notecard.AssetData, item.UUID,
                                delegate(bool uploadSuccess, string status, UUID itemID, UUID assetID)
                                {
                                    notecardItemID = itemID;
                                    notecardAssetID = assetID;
                                    finalUploadSuccess = uploadSuccess;
                                    message = status ?? "Unknown error uploading notecard asset";
                                    notecardEvent.Set();
                                });
                        }
                        else
                        {
                            notecardEvent.Set();
                        }
                    }
                    else
                    {
                        message = "Notecard item creation failed";
                        notecardEvent.Set();
                    }
                }
            );

            notecardEvent.WaitOne(NOTECARD_CREATE_TIMEOUT, false);

            if (finalUploadSuccess)
            {
                Logger.Log("Notecard successfully created, ItemID " + notecardItemID + " AssetID " + notecardAssetID, Helpers.LogLevel.Info);
                return DownloadNotecard(notecardItemID, notecardAssetID);
            }
            else
                return "Notecard creation failed: " + message;
        }

        private InventoryItem FetchItem(UUID itemID)
        {
            InventoryItem fetchItem = null;
            AutoResetEvent fetchItemEvent = new AutoResetEvent(false);

            EventHandler<ItemReceivedEventArgs> itemReceivedCallback =
                delegate(object sender, ItemReceivedEventArgs e)
                {
                    if (e.Item.UUID == itemID)
                    {
                        fetchItem = e.Item;
                        fetchItemEvent.Set();
                    }
                };

            Client.Inventory.ItemReceived += itemReceivedCallback;

            Client.Inventory.RequestFetchInventory(itemID, Client.Self.AgentID);

            fetchItemEvent.WaitOne(INVENTORY_FETCH_TIMEOUT, false);

            Client.Inventory.ItemReceived -= itemReceivedCallback;

            return fetchItem;
        }

        private string DownloadNotecard(UUID itemID, UUID assetID)
        {
            AutoResetEvent assetDownloadEvent = new AutoResetEvent(false);
            byte[] notecardData = null;
            string error = "Timeout";

            Client.Assets.RequestInventoryAsset(assetID, itemID, UUID.Zero, Client.Self.AgentID, AssetType.Notecard, true,
                                delegate(AssetDownload transfer, Asset asset)
                                {
                                    if (transfer.Success)
                                        notecardData = transfer.AssetData;
                                    else
                                        error = transfer.Status.ToString();
                                    assetDownloadEvent.Set();
                                }
            );

            assetDownloadEvent.WaitOne(NOTECARD_FETCH_TIMEOUT, false);

            if (notecardData != null)
                return Encoding.UTF8.GetString(notecardData);
            else
                return "Error downloading notecard asset: " + error;
        }
    }
}