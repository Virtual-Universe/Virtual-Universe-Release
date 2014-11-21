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
using System.Text;

namespace OpenMetaverse.TestClient
{
    public class JoinGroupCommand : Command
    {
        ManualResetEvent GetGroupsSearchEvent = new ManualResetEvent(false);
        private UUID queryID = UUID.Zero;
        private UUID resolvedGroupID;
        private string groupName;
        private string resolvedGroupName;
        private bool joinedGroup;

        public JoinGroupCommand(TestClient testClient)
        {
            Name = "joingroup";
            Description = "join a group. Usage: joingroup GroupName | joingroup UUID GroupId";
            Category = CommandCategory.Groups;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length < 1)
                return Description;

            groupName = String.Empty;
            resolvedGroupID = UUID.Zero;
            resolvedGroupName = String.Empty;

            if (args[0].ToLower() == "uuid")
            {
                if (args.Length < 2)
                    return Description;

                if (!UUID.TryParse((resolvedGroupName = groupName = args[1]), out resolvedGroupID))
                    return resolvedGroupName + " doesn't seem a valid UUID";
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                    groupName += args[i] + " ";
                groupName = groupName.Trim();

                Client.Directory.DirGroupsReply += Directory_DirGroups;
                                
                queryID = Client.Directory.StartGroupSearch(groupName, 0);

                GetGroupsSearchEvent.WaitOne(60000, false);

                Client.Directory.DirGroupsReply -= Directory_DirGroups;

                GetGroupsSearchEvent.Reset();
            }

            if (resolvedGroupID == UUID.Zero)
            {
                if (string.IsNullOrEmpty(resolvedGroupName))
                    return "Unable to obtain UUID for group " + groupName;
                else
                    return resolvedGroupName;
            }
            
            Client.Groups.GroupJoinedReply += Groups_OnGroupJoined;
            Client.Groups.RequestJoinGroup(resolvedGroupID);

            /* A.Biondi 
             * TODO: implement the pay to join procedure.
             */

            GetGroupsSearchEvent.WaitOne(60000, false);

            Client.Groups.GroupJoinedReply -= Groups_GroupJoined;
            GetGroupsSearchEvent.Reset();
            Client.ReloadGroupsCache();

            if (joinedGroup)
                return "Joined the group " + resolvedGroupName;
            return "Unable to join the group " + resolvedGroupName;
        }

        void Groups_GroupJoined(object sender, GroupOperationEventArgs e)
        {
            throw new NotImplementedException();
        }

        void Directory_DirGroups(object sender, DirGroupsReplyEventArgs e)
        {
            if (queryID == e.QueryID)
            {
                queryID = UUID.Zero;
                if (e.MatchedGroups.Count < 1)
                {
                    Console.WriteLine("ERROR: Got an empty reply");
                }
                else
                {
                    if (e.MatchedGroups.Count > 1)
                    {
                        /* A.Biondi 
                         * The Group search doesn't work as someone could expect...
                         * It'll give back to you a long list of groups even if the 
                         * searchText (groupName) matches esactly one of the groups 
                         * names present on the server, so we need to check each result.
                         * UUIDs of the matching groups are written on the console.
                         */
                        Console.WriteLine("Matching groups are:\n");
                        foreach (DirectoryManager.GroupSearchData groupRetrieved in e.MatchedGroups)
                        {
                            Console.WriteLine(groupRetrieved.GroupName + "\t\t\t(" +
                                Name + " UUID " + groupRetrieved.GroupID.ToString() + ")");

                            if (groupRetrieved.GroupName.ToLower() == groupName.ToLower())
                            {
                                resolvedGroupID = groupRetrieved.GroupID;
                                resolvedGroupName = groupRetrieved.GroupName;
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(resolvedGroupName))
                            resolvedGroupName = "Ambiguous name. Found " + e.MatchedGroups.Count.ToString() + " groups (UUIDs on console)";
                    }

                }
                GetGroupsSearchEvent.Set();
            }
        }

        void Groups_OnGroupJoined(object sender, GroupOperationEventArgs e)
        {
            Console.WriteLine(Client.ToString() + (e.Success ? " joined " : " failed to join ") + e.GroupID.ToString());

            /* A.Biondi 
             * This code is not necessary because it is yet present in the 
             * GroupCommand.cs as well. So the new group will be activated by 
             * the mentioned command. If the GroupCommand.cs would change, 
             * just uncomment the following two lines.
                
            if (success)
            {
                Console.WriteLine(Client.ToString() + " setting " + groupID.ToString() + " as the active group");
                Client.Groups.ActivateGroup(groupID);
            }
                
            */

            joinedGroup = e.Success;
            GetGroupsSearchEvent.Set();
        }                        
    }
}
