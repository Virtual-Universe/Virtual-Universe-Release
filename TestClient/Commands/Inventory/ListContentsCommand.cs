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
using System.Text;
using OpenMetaverse;

namespace OpenMetaverse.TestClient.Commands.Inventory.Shell
{
    public class ListContentsCommand : Command
    {
        private InventoryManager Manager;
        private OpenMetaverse.Inventory Inventory;
        public ListContentsCommand(TestClient client)
        {
            Name = "ls";
            Description = "Lists the contents of the current working inventory folder.";
            Category = CommandCategory.Inventory;
        }
        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length > 1)
                return "Usage: ls [-l]";
            bool longDisplay = false;
            if (args.Length > 0 && args[0] == "-l")
                longDisplay = true;

            Manager = Client.Inventory;
            Inventory = Manager.Store;
            // WARNING: Uses local copy of inventory contents, need to download them first.
            List<InventoryBase> contents = Inventory.GetContents(Client.CurrentDirectory);
            string displayString = "";
            string nl = "\n"; // New line character
            // Pretty simple, just print out the contents.
            foreach (InventoryBase b in contents)
            {
                if (longDisplay)
                {
                    // Generate a nicely formatted description of the item.
                    // It kinda looks like the output of the unix ls.
                    // starts with 'd' if the inventory is a folder, '-' if not.
                    // 9 character permissions string
                    // UUID of object
                    // Name of object
                    if (b is InventoryFolder)
                    {
                        InventoryFolder folder = b as InventoryFolder;
                        displayString += "d--------- ";
                        displayString += folder.UUID;
                        displayString += " " + folder.Name;
                    }
                    else if (b is InventoryItem)
                    {
                        InventoryItem item = b as InventoryItem;
                        displayString += "-";
                        displayString += PermMaskString(item.Permissions.OwnerMask);
                        displayString += PermMaskString(item.Permissions.GroupMask);
                        displayString += PermMaskString(item.Permissions.EveryoneMask);
                        displayString += " " + item.UUID;
                        displayString += " " + item.Name;
                        displayString += nl;
                        displayString += "  AssetID: " + item.AssetUUID;
                    }
                }
                else
                {
                    displayString += b.Name;
                }
                displayString += nl;
            }
            return displayString;
        }

        /// <summary>
        /// Returns a 3-character summary of the PermissionMask
        /// CMT if the mask allows copy, mod and transfer
        /// -MT if it disallows copy
        /// --T if it only allows transfer
        /// --- if it disallows everything
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        private static string PermMaskString(PermissionMask mask)
        {
            string str = "";
            if (((uint)mask | (uint)PermissionMask.Copy) == (uint)PermissionMask.Copy)
                str += "C";
            else
                str += "-";
            if (((uint)mask | (uint)PermissionMask.Modify) == (uint)PermissionMask.Modify)
                str += "M";
            else
                str += "-";
            if (((uint)mask | (uint)PermissionMask.Transfer) == (uint)PermissionMask.Transfer)
                str += "T";
            else
                str += "-";
            return str;
        }
    }
}
