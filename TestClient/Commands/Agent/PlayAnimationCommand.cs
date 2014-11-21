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
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using OpenMetaverse;
using OpenMetaverse.Packets;

namespace OpenMetaverse.TestClient
{
    public class PlayAnimationCommand : Command
    {        
        private Dictionary<UUID, string> m_BuiltInAnimations = new Dictionary<UUID, string>(Animations.ToDictionary());
        public PlayAnimationCommand(TestClient testClient)
        {
            Name = "play";
            Description = "Attempts to play an animation";
            Category = CommandCategory.Appearance;                        
        }

        private string Usage()
        {
            String usage = "Usage:\n" +
                "\tplay list - list the built in animations\n" +
                "\tplay show - show any currently playing animations\n" +
                "\tplay UUID - play an animation asset\n" +
                "\tplay ANIMATION - where ANIMATION is one of the values returned from \"play list\"\n";
            return usage;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {            
            StringBuilder result = new StringBuilder();
            if (args.Length != 1)
                return Usage();

            UUID animationID;
            string arg = args[0].Trim();

            if (UUID.TryParse(args[0], out animationID))
            {
                Client.Self.AnimationStart(animationID, true);
            }
            else if (arg.ToLower().Equals("list"))
            {
                foreach (string key in m_BuiltInAnimations.Values)
                {
                    result.AppendLine(key);
                }
            }
            else if (arg.ToLower().Equals("show"))
            {
                Client.Self.SignaledAnimations.ForEach(delegate(KeyValuePair<UUID, int> kvp) {
                    if (m_BuiltInAnimations.ContainsKey(kvp.Key))
                    {
                        result.AppendFormat("The {0} System Animation is being played, sequence is {1}", m_BuiltInAnimations[kvp.Key], kvp.Value);
                    }
                    else
                    {
                        result.AppendFormat("The {0} Asset Animation is being played, sequence is {0}", kvp.Key, kvp.Value);
                    }
                });                                
            }
            else if (m_BuiltInAnimations.ContainsValue(args[0].Trim().ToUpper()))
            {
                foreach (var kvp in m_BuiltInAnimations)
                {
                    if (kvp.Value.Equals(arg.ToUpper()))
                    {
                        Client.Self.AnimationStart(kvp.Key, true);
                        break;
                    }
                }
            }
            else
            {
                return Usage();
            }

            return result.ToString();
        }
    }
}
