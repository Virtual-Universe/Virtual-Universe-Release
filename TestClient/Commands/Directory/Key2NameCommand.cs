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

namespace OpenMetaverse.TestClient.Commands
{
    class key2nameCommand : Command
    {
        System.Threading.AutoResetEvent waitQuery = new System.Threading.AutoResetEvent(false);
        StringBuilder result = new StringBuilder();
        public key2nameCommand(TestClient testClient)
        {
            Name = "key2name";
            Description = "resolve a UUID to an avatar or group name. Usage: key2name UUID";
            Category = CommandCategory.Search;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length < 1)
                return "Usage: key2name UUID";

            UUID key;
            if(!UUID.TryParse(args[0].Trim(), out key))
            {
                return "UUID " + args[0].Trim() + " appears to be invalid";
            }
            result.Remove(0, result.Length);
            waitQuery.Reset();
            
            Client.Avatars.UUIDNameReply += Avatars_OnAvatarNames;
            Client.Groups.GroupProfile += Groups_OnGroupProfile;
            Client.Avatars.RequestAvatarName(key);            
            
            Client.Groups.RequestGroupProfile(key);
            if (!waitQuery.WaitOne(10000, false))
            {
                result.AppendLine("Timeout waiting for reply, this could mean the Key is not an avatar or a group");
            }

            Client.Avatars.UUIDNameReply -= Avatars_OnAvatarNames;
            Client.Groups.GroupProfile -= Groups_OnGroupProfile;
            return result.ToString();
        }

        void Groups_OnGroupProfile(object sender, GroupProfileEventArgs e)
        {
            result.AppendLine("Group: " + e.Group.Name + " " + e.Group.ID);
            waitQuery.Set();
        }

        void Avatars_OnAvatarNames(object sender, UUIDNameReplyEventArgs e)
        {
            foreach (KeyValuePair<UUID, string> kvp in e.Names)
                result.AppendLine("Avatar: " + kvp.Value + " " + kvp.Key);
            waitQuery.Set();
        }        
    }
}
