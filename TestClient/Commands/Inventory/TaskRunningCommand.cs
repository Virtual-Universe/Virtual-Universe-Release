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
using System.Threading;
using OpenMetaverse;

namespace OpenMetaverse.TestClient
{
    public class TaskRunningCommand : Command
    {
        public TaskRunningCommand(TestClient testClient)
        {
            Name = "taskrunning";
            Description = "Retrieves or set IsRunning flag on items inside an object (task inventory). Usage: taskrunning objectID [[scriptName] true|false]";
            Category = CommandCategory.Inventory;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length != 1)
                return "Usage: taskrunning objectID [[scriptName] true|false]";

            uint objectLocalID;
            UUID objectID;

            if (!UUID.TryParse(args[0], out objectID))
                return "Usage: taskrunning objectID [[scriptName] true|false]";

            Primitive found = Client.Network.CurrentSim.ObjectsPrimitives.Find(delegate(Primitive prim) { return prim.ID == objectID; });
            if (found != null)
                objectLocalID = found.LocalID;
            else
                return String.Format("Couldn't find prim {0}", objectID);

            List<InventoryBase> items = Client.Inventory.GetTaskInventory(objectID, objectLocalID, 1000 * 30);

            //bool wantSet = false;
            bool setTaskTo = false;
            if (items != null)
            {
                string result = String.Empty;
                string matching = String.Empty;
                bool setAny = false;
                if (args.Length > 1)
                {
                    matching = args[1];

                    string tf;
                    if (args.Length > 2)
                    {
                        tf = args[2];
                    }
                    else
                    {
                        tf = matching.ToLower();
                    }
                    if (tf == "true")
                    {
                        setAny = true;
                        setTaskTo = true;
                    }
                    else if (tf == "false")
                    {
                        setAny = true;
                        setTaskTo = false;
                    }

                }
                bool wasRunning = false;

                EventHandler<ScriptRunningReplyEventArgs> callback;
                using (AutoResetEvent OnScriptRunningReset = new AutoResetEvent(false))
                {
                    callback = ((object sender, ScriptRunningReplyEventArgs e) =>
                    {
                        if (e.ObjectID == objectID)
                        {
                            result += String.Format(" IsMono: {0} IsRunning: {1}", e.IsMono, e.IsRunning);
                            wasRunning = e.IsRunning;
                            OnScriptRunningReset.Set();
                        }
                    });

                    Client.Inventory.ScriptRunningReply += callback;

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i] is InventoryFolder)
                        {
                            // this shouldn't happen this year
                            result += String.Format("[Folder] Name: {0}", items[i].Name) + Environment.NewLine;
                        }
                        else
                        {
                            InventoryItem item = (InventoryItem)items[i];
                            AssetType assetType = item.AssetType;
                            result += String.Format("[Item] Name: {0} Desc: {1} Type: {2}", item.Name, item.Description,
                                                    assetType);
                            if (assetType == AssetType.LSLBytecode || assetType == AssetType.LSLText)
                            {
                                OnScriptRunningReset.Reset();
                                Client.Inventory.RequestGetScriptRunning(objectID, item.UUID);
                                if (!OnScriptRunningReset.WaitOne(10000, true))
                                {
                                    result += " (no script info)";
                                }
                                if (setAny && item.Name.Contains(matching))
                                {
                                    if (wasRunning != setTaskTo)
                                    {
                                        OnScriptRunningReset.Reset();
                                        result += " Setting " + setTaskTo + " => ";
                                        Client.Inventory.RequestSetScriptRunning(objectID, item.UUID, setTaskTo);
                                        if (!OnScriptRunningReset.WaitOne(10000, true))
                                        {
                                            result += " (was not set)";
                                        }
                                    }
                                }
                            }

                            result += Environment.NewLine;
                        }
                    }
                }
                Client.Inventory.ScriptRunningReply -= callback;
                return result;
            }
            else
            {
                return "Failed to download task inventory for " + objectLocalID;
            }
        }
    }
}
