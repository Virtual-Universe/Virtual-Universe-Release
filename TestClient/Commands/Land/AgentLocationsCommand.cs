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

namespace OpenMetaverse.TestClient
{
    /// <summary>
    /// Display a list of all agent locations in a specified region
    /// </summary>
    public class AgentLocationsCommand : Command
    {
        public AgentLocationsCommand(TestClient testClient)
        {
            Name = "agentlocations";
            Description = "Downloads all of the agent locations in a specified region. Usage: agentlocations [regionhandle]";
            Category = CommandCategory.Simulator;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            ulong regionHandle;

            if (args.Length == 0)
                regionHandle = Client.Network.CurrentSim.Handle;
            else if (!(args.Length == 1 && UInt64.TryParse(args[0], out regionHandle)))
                return "Usage: agentlocations [regionhandle]";

            List<MapItem> items = Client.Grid.MapItems(regionHandle, GridItemType.AgentLocations, 
                GridLayerType.Objects, 1000 * 20);

            if (items != null)
            {
                StringBuilder ret = new StringBuilder();
                ret.AppendLine("Agent locations:");

                for (int i = 0; i < items.Count; i++)
                {
                    MapAgentLocation location = (MapAgentLocation)items[i];

                    ret.AppendLine(String.Format("{0} avatar(s) at {1},{2}", location.AvatarCount, location.LocalX,
                        location.LocalY));
                }

                return ret.ToString();
            }
            else
            {
                return "Failed to fetch agent locations";
            }
        }
    }
}
