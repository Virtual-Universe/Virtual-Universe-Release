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
using OpenMetaverse.Packets;

namespace OpenMetaverse.TestClient
{
    public class HelpCommand: Command
    {
        public HelpCommand(TestClient testClient)
		{
			Name = "help";
			Description = "Lists available commands. usage: help [command] to display information on commands";
            Category = CommandCategory.TestClient;
		}

        public override string Execute(string[] args, UUID fromAgentID)
		{
            if (args.Length > 0)
            {
                if (Client.Commands.ContainsKey(args[0]))
                    return Client.Commands[args[0]].Description;
                else
                    return "Command " + args[0] + " Does not exist. \"help\" to display all available commands.";
            }
			StringBuilder result = new StringBuilder();
            SortedDictionary<CommandCategory, List<Command>> CommandTree = new SortedDictionary<CommandCategory, List<Command>>();

            CommandCategory cc;
			foreach (Command c in Client.Commands.Values)
			{
                if (c.Category.Equals(null))
                    cc = CommandCategory.Unknown;
                else
                    cc = c.Category;

                if (CommandTree.ContainsKey(cc))
                    CommandTree[cc].Add(c);
                else
                {
                    List<Command> l = new List<Command>();
                    l.Add(c);
                    CommandTree.Add(cc, l);
                }
			}

            foreach (KeyValuePair<CommandCategory, List<Command>> kvp in CommandTree)
            {
                result.AppendFormat(System.Environment.NewLine + "* {0} Related Commands:" + System.Environment.NewLine, kvp.Key.ToString());
                int colMax = 0;
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    if (colMax >= 120)
                    {
                        result.AppendLine();
                        colMax = 0;
                    }

                    result.AppendFormat(" {0,-15}", kvp.Value[i].Name);
                    colMax += 15;
                }
                result.AppendLine();
            }
            result.AppendLine(System.Environment.NewLine + "Help [command] for usage/information");
            
            return result.ToString();
		}
    }
}
