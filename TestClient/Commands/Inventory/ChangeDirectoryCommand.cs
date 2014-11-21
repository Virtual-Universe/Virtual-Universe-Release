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
    public class ChangeDirectoryCommand : Command
    {
        private OpenMetaverse.Inventory Inventory;

        public ChangeDirectoryCommand(TestClient client)
        {
            Name = "cd";
            Description = "Changes the current working inventory folder.";
            Category = CommandCategory.Inventory;
        }
        public override string Execute(string[] args, UUID fromAgentID)
        {
            Inventory = Client.Inventory.Store;

            if (args.Length > 1)
                return "Usage: cd [path-to-folder]";
            string pathStr = "";
            string[] path = null;
            if (args.Length == 0)
            {
                path = new string[] { "" };
                // cd without any arguments doesn't do anything.
            }
            else if (args.Length == 1)
            {
                pathStr = args[0];
                path = pathStr.Split(new char[] { '/' });
                // Use '/' as a path seperator.
            }
            InventoryFolder currentFolder = Client.CurrentDirectory;
            if (pathStr.StartsWith("/"))
                currentFolder = Inventory.RootFolder;

            if (currentFolder == null) // We need this to be set to something. 
                return "Error: Client not logged in.";

            // Traverse the path, looking for the 
            for (int i = 0; i < path.Length; ++i)
            {
                string nextName = path[i];
                if (string.IsNullOrEmpty(nextName) || nextName == ".")
                    continue; // Ignore '.' and blanks, stay in the current directory.
                if (nextName == ".." && currentFolder != Inventory.RootFolder)
                {
                    // If we encounter .., move to the parent folder.
                    currentFolder = Inventory[currentFolder.ParentUUID] as InventoryFolder;
                }
                else
                {
                    List<InventoryBase> currentContents = Inventory.GetContents(currentFolder);
                    // Try and find an InventoryBase with the corresponding name.
                    bool found = false;
                    foreach (InventoryBase item in currentContents)
                    {
                        // Allow lookup by UUID as well as name:
                        if (item.Name == nextName || item.UUID.ToString() == nextName)
                        {
                            found = true;
                            if (item is InventoryFolder)
                            {
                                currentFolder = item as InventoryFolder;
                            }
                            else
                            {
                                return item.Name + " is not a folder.";
                            }
                        }
                    }
                    if (!found)
                        return nextName + " not found in " + currentFolder.Name;
                }
            }
            Client.CurrentDirectory = currentFolder;
            return "Current folder: " + currentFolder.Name;
        }
    }
}