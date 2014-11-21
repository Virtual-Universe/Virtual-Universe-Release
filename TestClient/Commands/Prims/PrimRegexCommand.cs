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
using System.Text.RegularExpressions;
using OpenMetaverse;

namespace OpenMetaverse.TestClient
{
    public class PrimRegexCommand : Command
    {
        public PrimRegexCommand(TestClient testClient)
        {
            Name = "primregex";
            Description = "Find prim by text predicat. " +
                "Usage: primregex [text predicat] (eg findprim .away.)";
            Category = CommandCategory.Objects;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length < 1)
                return "Usage: primregex [text predicat]";

            try
            {
                // Build the predicat from the args list
                string predicatPrim = string.Empty;
                for (int i = 0; i < args.Length; i++)
                    predicatPrim += args[i] + " ";
                predicatPrim = predicatPrim.TrimEnd();

                // Build Regex
                Regex regexPrimName = new Regex(predicatPrim.ToLower());

                // Print result
                Logger.Log(string.Format("Searching prim for [{0}] ({1} prims loaded in simulator)\n", predicatPrim,
                    Client.Network.CurrentSim.ObjectsPrimitives.Count), Helpers.LogLevel.Info, Client);

                Client.Network.CurrentSim.ObjectsPrimitives.ForEach(
                    delegate(Primitive prim)
                    {
                        bool match = false;
                        string name = "(unknown)";
                        string description = "(unknown)";


                        match = (prim.Text != null && regexPrimName.IsMatch(prim.Text.ToLower()));

                        if (prim.Properties != null && !match)
                        {
                            match = regexPrimName.IsMatch(prim.Properties.Name.ToLower());
                            if (!match)
                                match = regexPrimName.IsMatch(prim.Properties.Description.ToLower());
                        }

                        if (match)
                        {
                            if (prim.Properties != null)
                            {
                                name = prim.Properties.Name;
                                description = prim.Properties.Description;
                            }
                            Logger.Log(string.Format("\nNAME={0}\nID = {1}\nFLAGS = {2}\nTEXT = '{3}'\nDESC='{4}'", name,
                                prim.ID, prim.Flags.ToString(), prim.Text, description), Helpers.LogLevel.Info, Client);
                        }
                    }
                );
            }
            catch (System.Exception e)
            {
                Logger.Log(e.Message, Helpers.LogLevel.Error, Client, e);
                return "Error searching";
            }

            return "Done searching";
        }
    }
}
