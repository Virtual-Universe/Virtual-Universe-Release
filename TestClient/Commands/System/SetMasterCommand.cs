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
using System.Threading;
using OpenMetaverse;
using OpenMetaverse.Packets;

namespace OpenMetaverse.TestClient
{
    public class SetMasterCommand: Command
    {
		public DateTime Created = DateTime.Now;
        private UUID resolvedMasterKey = UUID.Zero;
        private ManualResetEvent keyResolution = new ManualResetEvent(false);
        private UUID query = UUID.Zero;

        public SetMasterCommand(TestClient testClient)
		{
			Name = "setmaster";
            Description = "Sets the user name of the master user. The master user can IM to run commands. Usage: setmaster [name]";
            Category = CommandCategory.TestClient;
		}

        public override string Execute(string[] args, UUID fromAgentID)
		{
			string masterName = String.Empty;
			for (int ct = 0; ct < args.Length;ct++)
				masterName = masterName + args[ct] + " ";
            masterName = masterName.TrimEnd();

            if (masterName.Length == 0)
                return "Usage: setmaster [name]";

            EventHandler<DirPeopleReplyEventArgs> callback = KeyResolvHandler;
            Client.Directory.DirPeopleReply += callback;

            query = Client.Directory.StartPeopleSearch(masterName, 0);

            if (keyResolution.WaitOne(TimeSpan.FromMinutes(1), false))
            {
                Client.MasterKey = resolvedMasterKey;
                keyResolution.Reset();
                Client.Directory.DirPeopleReply -= callback;
            }
            else
            {
                keyResolution.Reset();
                Client.Directory.DirPeopleReply -= callback;
                return "Unable to obtain UUID for \"" + masterName + "\". Master unchanged.";
            }
            
            // Send an Online-only IM to the new master
            Client.Self.InstantMessage(
                Client.MasterKey, "You are now my master.  IM me with \"help\" for a command list.");

            return String.Format("Master set to {0} ({1})", masterName, Client.MasterKey.ToString());
		}

        private void KeyResolvHandler(object sender, DirPeopleReplyEventArgs e)
        {
            if (query != e.QueryID)
                return;

            resolvedMasterKey = e.MatchedPeople[0].AgentID;
            keyResolution.Set();
            query = UUID.Zero;
        }
    }
}
