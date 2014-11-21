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
using OpenMetaverse.Packets;

namespace OpenMetaverse.TestClient
{
    public class CloneCommand : Command
    {
        uint SerialNum = 2;

        public CloneCommand(TestClient testClient)
        {
            Name = "clone";
            Description = "Clone the appearance of a nearby avatar. Usage: clone [name]";
            Category = CommandCategory.Appearance;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            string targetName = String.Empty;
            List<DirectoryManager.AgentSearchData> matches;

            for (int ct = 0; ct < args.Length; ct++)
                targetName = targetName + args[ct] + " ";
            targetName = targetName.TrimEnd();

            if (targetName.Length == 0)
                return "Usage: clone [name]";

            if (Client.Directory.PeopleSearch(DirectoryManager.DirFindFlags.People, targetName, 0, 1000 * 10,
                out matches) && matches.Count > 0)
            {
                UUID target = matches[0].AgentID;
                targetName += String.Format(" ({0})", target);

                if (Client.Appearances.ContainsKey(target))
                {
                    #region AvatarAppearance to AgentSetAppearance

                    AvatarAppearancePacket appearance = Client.Appearances[target];

                    AgentSetAppearancePacket set = new AgentSetAppearancePacket();
                    set.AgentData.AgentID = Client.Self.AgentID;
                    set.AgentData.SessionID = Client.Self.SessionID;
                    set.AgentData.SerialNum = SerialNum++;
                    set.AgentData.Size = new Vector3(2f, 2f, 2f); // HACK

                    set.WearableData = new AgentSetAppearancePacket.WearableDataBlock[0];
                    set.VisualParam = new AgentSetAppearancePacket.VisualParamBlock[appearance.VisualParam.Length];

                    for (int i = 0; i < appearance.VisualParam.Length; i++)
                    {
                        set.VisualParam[i] = new AgentSetAppearancePacket.VisualParamBlock();
                        set.VisualParam[i].ParamValue = appearance.VisualParam[i].ParamValue;
                    }

                    set.ObjectData.TextureEntry = appearance.ObjectData.TextureEntry;

                    #endregion AvatarAppearance to AgentSetAppearance

                    // Detach everything we are currently wearing
                    Client.Appearance.AddAttachments(new List<InventoryItem>(), true);

                    // Send the new appearance packet
                    Client.Network.SendPacket(set);

                    return "Cloned " + targetName;
                }
                else
                {
                    return "Don't know the appearance of avatar " + targetName;
                }
            }
            else
            {
                return "Couldn't find avatar " + targetName;
            }
        }
    }
}
