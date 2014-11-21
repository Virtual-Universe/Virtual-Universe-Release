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
    public class ImCommand : Command
    {
        string ToAvatarName = String.Empty;
        ManualResetEvent NameSearchEvent = new ManualResetEvent(false);
        Dictionary<string, UUID> Name2Key = new Dictionary<string, UUID>();

        public ImCommand(TestClient testClient)
        {
            testClient.Avatars.AvatarPickerReply += Avatars_AvatarPickerReply;

            Name = "im";
            Description = "Instant message someone. Usage: im [firstname] [lastname] [message]";
            Category = CommandCategory.Communication;
        }
        
        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length < 3)
                return "Usage: im [firstname] [lastname] [message]";

            ToAvatarName = args[0] + " " + args[1];

            // Build the message
            string message = String.Empty;
            for (int ct = 2; ct < args.Length; ct++)
                message += args[ct] + " ";
            message = message.TrimEnd();
            if (message.Length > 1023) message = message.Remove(1023);

            if (!Name2Key.ContainsKey(ToAvatarName.ToLower()))
            {
                // Send the Query
                Client.Avatars.RequestAvatarNameSearch(ToAvatarName, UUID.Random());

                NameSearchEvent.WaitOne(6000, false);
            }

            if (Name2Key.ContainsKey(ToAvatarName.ToLower()))
            {
                UUID id = Name2Key[ToAvatarName.ToLower()];

                Client.Self.InstantMessage(id, message);
                return "Instant Messaged " + id.ToString() + " with message: " + message;
            }
            else
            {
                return "Name lookup for " + ToAvatarName + " failed";
            }
        }

        void Avatars_AvatarPickerReply(object sender, AvatarPickerReplyEventArgs e)
        {
            foreach (KeyValuePair<UUID, string> kvp in e.Avatars)
            {
                if (kvp.Value.ToLower() == ToAvatarName.ToLower())
                {
                    Name2Key[ToAvatarName.ToLower()] = kvp.Key;
                    NameSearchEvent.Set();
                    return;
                }
            }
        }
    }
}
