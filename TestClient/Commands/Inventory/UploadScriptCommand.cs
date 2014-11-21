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

namespace OpenMetaverse.TestClient
{
    /// <summary>
    /// Example of how to put a new script in your inventory
    /// </summary>
    public class UploadScriptCommand : Command
    {
        /// <summary>
        ///  The default constructor for TestClient commands
        /// </summary>
        /// <param name="testClient"></param>
        public UploadScriptCommand(TestClient testClient)
        {
            Name = "uploadscript";
            Description = "Upload a local .lsl file file into your inventory.";
            Category = CommandCategory.Inventory;
        }

        /// <summary>
        /// The default override for TestClient commands
        /// </summary>
        /// <param name="args"></param>
        /// <param name="fromAgentID"></param>
        /// <returns></returns>
        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length < 1)
                return "Usage: uploadscript filename.lsl";

            string file = String.Empty;
            for (int ct = 0; ct < args.Length; ct++)
                file = String.Format("{0}{1} ", file, args[ct]);
            file = file.TrimEnd();

            if (!File.Exists(file))
                return String.Format("Filename '{0}' does not exist", file);

            string ret = String.Format("Filename: {0}", file);

            try
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string body = reader.ReadToEnd();
                    string desc = String.Format("{0} created by OpenMetaverse TestClient {1}", file, DateTime.Now);
                    // create the asset
                    Client.Inventory.RequestCreateItem(Client.Inventory.FindFolderForType(AssetType.LSLText), file, desc, AssetType.LSLText, UUID.Random(), InventoryType.LSL, PermissionMask.All,
                    delegate(bool success, InventoryItem item)
                    {
                        if (success)
                            // upload the asset
                            Client.Inventory.RequestUpdateScriptAgentInventory(EncodeScript(body), item.UUID, true, new InventoryManager.ScriptUpdatedCallback(delegate(bool uploadSuccess, string uploadStatus, bool compileSuccess, List<string> compileMessages, UUID itemid, UUID assetid)
                            {
                                if (uploadSuccess)
                                    ret += String.Format(" Script successfully uploaded, ItemID {0} AssetID {1}", itemid, assetid);
                                if (compileSuccess)
                                    ret += " compilation successful";

                            }));
                    });
                }
                return ret;

            }
            catch (System.Exception e)
            {
                Logger.Log(e.ToString(), Helpers.LogLevel.Error, Client);
                return String.Format("Error creating script for {0}", ret);
            }
        }
        /// <summary>
        /// Encodes the script text for uploading
        /// </summary>
        /// <param name="body"></param>
        public static byte[] EncodeScript(string body)
        {
            // Assume this is a string, add 1 for the null terminator ?
            byte[] stringBytes = System.Text.Encoding.UTF8.GetBytes(body);
            byte[] assetData = new byte[stringBytes.Length]; //+ 1];
            Array.Copy(stringBytes, 0, assetData, 0, stringBytes.Length);
            return assetData;
        }
    }

}
