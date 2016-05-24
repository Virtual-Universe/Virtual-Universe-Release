/*
 * Copyright (c) Contributors, http://virtual-planets.org/, http://whitecore-sim.org/, http://aurora-sim.org, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual Universe Project nor the
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
 */


using Universe.Framework.ConsoleFramework;
using Universe.Framework.PresenceInfo;
using Universe.Framework.SceneInfo;
using Universe.Framework.Services.ClassHelpers.Assets;
using Universe.Framework.Services.ClassHelpers.Inventory;
using Universe.Framework.Utilities;
using OpenMetaverse;
using System;
using System.IO;

namespace Universe.Modules.Agent.AssetTransaction
{
    public class AssetXferUploader
    {
        /// <summary>
        ///     Upload state.
        /// </summary>
        /// <remarks>
        ///     New -> Uploading -> Complete
        /// </remarks>
        private enum UploadState
        {
            New,
            Uploading,
            Complete
        }

        /// <summary>
        ///     Reference to the object that holds this uploader.  Used to remove ourselves from it's list if we
        ///     are performing a delayed update.
        /// </summary>
        private AgentAssetTransactions m_transactions;

        private UploadState m_uploadState = UploadState.New;

        private AssetBase m_asset;
        private UUID InventFolder = UUID.Zero;
        private sbyte invType = 0;

        private bool m_createItem;
        private uint m_createItemCallback;

        private bool m_updateItem;
        private InventoryItemBase m_updateItemData;

        private bool m_updateTaskItem;
        private TaskInventoryItem m_updateTaskItemData;

        private string m_description = String.Empty;
        private bool m_dumpAssetToFile;
        private string m_name = String.Empty;
        //        private bool m_storeLocal;
        private uint nextPerm = 0;
        private IClientAPI ourClient;

        private UUID m_transactionID;

        private sbyte type = 0;
        private byte wearableType = 0;
        public ulong XferID;
        private IScene m_Scene;

        /// <summary>
        ///     AssetXferUploader constructor
        /// </summary>
        /// <param name='transactions'></param>
        /// <param name='scene'></param>
        /// <param name='transactionID'></param>
        /// <param name='dumpAssetToFile'>
        ///     If true then when the asset is uploaded it is dumped to a file with the format
        ///     String.Format("{6}_{7}_{0:d2}{1:d2}{2:d2}_{3:d2}{4:d2}{5:d2}.dat",
        ///     now.Year, now.Month, now.Day, now.Hour, now.Minute,
        ///     now.Second, m_asset.Name, m_asset.Type);
        ///     for debugging purposes.
        /// </param>
        public AssetXferUploader(
            AgentAssetTransactions transactions, IScene scene, UUID transactionID, bool dumpAssetToFile)
        {
            m_asset = new AssetBase();

            m_transactions = transactions;
            m_transactionID = transactionID;
            m_Scene = scene;
            m_dumpAssetToFile = dumpAssetToFile;
        }

        /// <summary>
        ///     Process transfer data received from the client.
        /// </summary>
        /// <param name="xferID"></param>
        /// <param name="packetID"></param>
        /// <param name="data"></param>
        /// <returns>True if the transfer is complete, false otherwise or if the xferID was not valid</returns>
        public bool HandleXferPacket(ulong xferID, uint packetID, byte[] data)
        {
            //            MainConsole.Instance.DebugFormat(
            //                "[ASSET XFER UPLOADER]: Received packet {0} for xfer {1} (data length {2})",
            //                packetID, xferID, data.Length);

            if (XferID == xferID)
            {
                if (m_asset.Data.Length > 1)
                {
                    byte[] destinationArray = new byte[m_asset.Data.Length + data.Length];
                    Array.Copy(m_asset.Data, 0, destinationArray, 0, m_asset.Data.Length);
                    Array.Copy(data, 0, destinationArray, m_asset.Data.Length, data.Length);
                    m_asset.Data = destinationArray;
                }
                else
                {
                    byte[] buffer2 = new byte[data.Length - 4];
                    Array.Copy(data, 4, buffer2, 0, data.Length - 4);
                    m_asset.Data = buffer2;
                }

                ourClient.SendConfirmXfer(xferID, packetID);

                if ((packetID & 0x80000000) != 0)
                {
                    SendCompleteMessage();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Start asset transfer from the client
        /// </summary>
        /// <param name="remoteClient"></param>
        /// <param name="assetID"></param>
        /// <param name="transaction"></param>
        /// <param name="type"></param>
        /// <param name="data">
        ///     Optional data.  If present then the asset is created immediately with this data
        ///     rather than requesting an upload from the client.  The data must be longer than 2 bytes.
        /// </param>
        /// <param name="storeLocal"></param>
        /// <param name="tempFile"></param>
        public void StartUpload(
            IClientAPI remoteClient, UUID assetID, UUID transaction, sbyte type, byte[] data, bool storeLocal,
            bool tempFile)
        {
            //            MainConsole.Instance.DebugFormat(
            //                "[ASSET XFER UPLOADER]: Initialized xfer from {0}, asset {1}, transaction {2}, type {3}, storeLocal {4}, tempFile {5}, already received data length {6}",
            //                remoteClient.Name, assetID, transaction, type, storeLocal, tempFile, data.Length);

            lock (this)
            {
                if (m_uploadState != UploadState.New)
                {
                    MainConsole.Instance.WarnFormat(
                        "[ASSET XFER UPLOADER]: Tried to start upload of asset {0}, transaction {1} for {2} but this is already in state {3}.  Aborting.",
                        assetID, transaction, remoteClient.Name, m_uploadState);

                    return;
                }

                m_uploadState = UploadState.Uploading;
            }

            ourClient = remoteClient;

            m_asset.ID = assetID;
            m_asset.Type = type;
            m_asset.CreatorID = remoteClient.AgentId;
            m_asset.Data = data;
            if (tempFile)
                m_asset.Flags |= AssetFlags.Temporary;

            //            m_storeLocal = storeLocal;

            if (m_asset.Data.Length > 2)
            {
                SendCompleteMessage();
            }
            else
            {
                RequestStartXfer();
            }
        }

        protected void RequestStartXfer()
        {
            XferID = Util.GetNextXferID();

            //            MainConsole.Instance.DebugFormat(
            //                "[ASSET XFER UPLOADER]: Requesting Xfer of asset {0}, type {1}, transfer id {2} from {3}",
            //                m_asset.FullID, m_asset.Type, XferID, ourClient.Name);

            ourClient.SendXferRequest(XferID, (short) m_asset.Type, m_asset.ID, 0, new byte[0]);
        }

        protected void SendCompleteMessage()
        {
            // We must lock in order to avoid a race with a separate thread dealing with an inventory item or create
            // message from other client UDP.
            lock (this)
            {
                m_uploadState = UploadState.Complete;

                ourClient.SendAssetUploadCompleteMessage((sbyte) m_asset.Type, true, m_asset.ID);

                if (m_createItem)
                {
                    CompleteCreateItem(m_createItemCallback);
                }
                else if (m_updateItem)
                {
                    CompleteItemUpdate(m_updateItemData);
                }
                else if (m_updateTaskItem)
                {
                    CompleteTaskItemUpdate(m_updateTaskItemData);
                }
                //                else if (m_storeLocal)
                //                {
                //                    m_Scene.AssetService.Store(m_asset);
                //                }
            }

            MainConsole.Instance.DebugFormat(
                "[ASSET XFER UPLOADER]: Uploaded asset {0} for transaction {1}",
                m_asset.ID, m_transactionID);

            if (m_dumpAssetToFile)
            {
                DateTime now = DateTime.Now;
                string filename =
                    String.Format("{6}_{7}_{0:d2}{1:d2}{2:d2}_{3:d2}{4:d2}{5:d2}.dat",
                                  now.Year, now.Month, now.Day, now.Hour, now.Minute,
                                  now.Second, m_asset.Name, m_asset.Type);
                SaveAssetToFile(filename, m_asset.Data);
            }
        }

        private void SaveAssetToFile(string filename, byte[] data)
        {
            string assetPath = "UserAssets";
            if (!Directory.Exists(assetPath))
            {
                Directory.CreateDirectory(assetPath);
            }
            FileStream fs = File.Create(Path.Combine(assetPath, filename));
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
            bw.Close();
        }

        public void RequestCreateInventoryItem(IClientAPI remoteClient,
                                               UUID folderID, uint callbackID,
                                               string description, string name, sbyte invType,
                                               sbyte type, byte wearableType, uint nextOwnerMask)
        {
            InventFolder = folderID;
            m_name = name;
            m_description = description;
            this.type = type;
            this.invType = invType;
            this.wearableType = wearableType;
            nextPerm = nextOwnerMask;
            m_asset.Name = name;
            m_asset.Description = description;
            m_asset.Type = type;

            // We must lock to avoid a race with a separate thread uploading the asset.
            lock (this)
            {
                if (m_uploadState == UploadState.Complete)
                {
                    CompleteCreateItem(callbackID);
                }
                else
                {
                    m_createItem = true; //set flag so the inventory item is created when upload is complete
                    m_createItemCallback = callbackID;
                }
            }
        }

        public void RequestUpdateInventoryItem(IClientAPI remoteClient, InventoryItemBase item)
        {
            // We must lock to avoid a race with a separate thread uploading the asset.
            lock (this)
            {
                m_asset.Name = item.Name;
                m_asset.Description = item.Description;
                m_asset.Type = (sbyte) item.AssetType;

                // We must always store the item at this point even if the asset hasn't finished uploading, in order
                // to avoid a race condition when the appearance module retrieves the item to set the asset id in
                // the AvatarAppearance structure.
                item.AssetID = m_asset.ID;
                if (item.AssetID != UUID.Zero)
                    m_Scene.InventoryService.UpdateItem(item);

                if (m_uploadState == UploadState.Complete)
                {
                    CompleteItemUpdate(item);
                }
                else
                {
                    //                    MainConsole.Instance.DebugFormat(
                    //                        "[ASSET XFER UPLOADER]: Holding update inventory item request {0} for {1} pending completion of asset xfer for transaction {2}",
                    //                        item.Name, remoteClient.Name, transactionID);

                    m_updateItem = true;
                    m_updateItemData = item;
                }
            }
        }

        public void RequestUpdateTaskInventoryItem(IClientAPI remoteClient, TaskInventoryItem taskItem)
        {
            // We must lock to avoid a race with a separate thread uploading the asset.
            lock (this)
            {
                m_asset.Name = taskItem.Name;
                m_asset.Description = taskItem.Description;
                m_asset.Type = (sbyte) taskItem.Type;
                taskItem.AssetID = m_asset.ID;

                if (m_uploadState == UploadState.Complete)
                {
                    CompleteTaskItemUpdate(taskItem);
                }
                else
                {
                    m_updateTaskItem = true;
                    m_updateTaskItemData = taskItem;
                }
            }
        }

        /// <summary>
        ///     Store the asset for the given item when it has been uploaded.
        /// </summary>
        /// <param name="item"></param>
        private void CompleteItemUpdate(InventoryItemBase item)
        {
            //            MainConsole.Instance.DebugFormat(
            //                "[ASSET XFER UPLOADER]: Storing asset {0} for earlier item update for {1} for {2}",
            //                m_asset.FullID, item.Name, ourClient.Name);

            m_Scene.AssetService.Store(m_asset);

            m_transactions.RemoveXferUploader(m_transactionID);
        }

        /// <summary>
        ///     Store the asset for the given task item when it has been uploaded.
        /// </summary>
        /// <param name="taskItem"></param>
        private void CompleteTaskItemUpdate(TaskInventoryItem taskItem)
        {
            //            MainConsole.Instance.DebugFormat(
            //                "[ASSET XFER UPLOADER]: Storing asset {0} for earlier task item update for {1} for {2}",
            //                m_asset.FullID, taskItem.Name, ourClient.Name);

            m_Scene.AssetService.Store(m_asset);

            m_transactions.RemoveXferUploader(m_transactionID);
        }

        private void CompleteCreateItem(uint callbackID)
        {
            m_Scene.AssetService.Store(m_asset);

            InventoryItemBase item = new InventoryItemBase();
            item.Owner = ourClient.AgentId;
            item.CreatorId = ourClient.AgentId.ToString();
            item.ID = UUID.Random();
            item.AssetID = m_asset.ID;
            item.Description = m_description;
            item.Name = m_name;
            item.AssetType = type;
            item.InvType = invType;
            item.Folder = InventFolder;
            item.BasePermissions = 0x7fffffff;
            item.CurrentPermissions = 0x7fffffff;
            item.GroupPermissions = 0;
            item.EveryOnePermissions = 0;
            item.NextPermissions = nextPerm;
            item.Flags = (uint) wearableType;
            item.CreationDate = Util.UnixTimeSinceEpoch();

            m_Scene.InventoryService.AddItemAsync(item,
                                                  (itm) => ourClient.SendInventoryItemCreateUpdate(itm, callbackID));
            m_transactions.RemoveXferUploader(m_transactionID);
        }
    }
}