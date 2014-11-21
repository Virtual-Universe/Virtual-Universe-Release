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

// the Namespace used for all TestClient commands
namespace OpenMetaverse.TestClient
{
    /// <summary>
    /// Shows a list of friends
    /// </summary>
    public class FriendsCommand : Command
    {        
        /// <summary>
        /// Constructor for FriendsCommand class
        /// </summary>
        /// <param name="testClient">A reference to the TestClient object</param>
        public FriendsCommand(TestClient testClient)
        {
            // The name of the command
            Name = "friends";
            // A short description of the command with usage instructions
            Description = "List avatar friends. Usage: friends";
            Category = CommandCategory.Friends;
        }

        /// <summary>
        /// Get a list of current friends
        /// </summary>
        /// <param name="args">optional testClient command arguments</param>
        /// <param name="fromAgentID">The <seealso cref="OpenMetaverse.UUID"/> 
        /// of the agent making the request</param>
        /// <returns></returns>
        public override string Execute(string[] args, UUID fromAgentID)
        {
            // initialize a StringBuilder object used to return the results
            StringBuilder sb = new StringBuilder();

            // Only iterate the Friends dictionary if we actually have friends!
            if (Client.Friends.FriendList.Count > 0)
            {
                // iterate over the InternalDictionary using a delegate to populate
                // our StringBuilder output string
                sb.AppendFormat("has {0} friends:", Client.Friends.FriendList.Count).AppendLine();
                Client.Friends.FriendList.ForEach(delegate(FriendInfo friend)
                {
                    // append the name of the friend to our output
                    sb.AppendFormat("{0}, {1}", friend.UUID, friend.Name).AppendLine();
                });
            }
            else
            {
                // we have no friends :(
                sb.AppendLine("No Friends");   
            }

            // return the result
            return sb.ToString();
        }
    }
}
