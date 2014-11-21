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
using OpenMetaverse;

namespace OpenMetaverse.TestClient
{
    public class RegionInfoCommand : Command
    {
        public RegionInfoCommand(TestClient testClient)
		{
			Name = "regioninfo";
			Description = "Prints out info about all the current region";
            Category = CommandCategory.Simulator;
		}

        public override string Execute(string[] args, UUID fromAgentID)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine(Client.Network.CurrentSim.ToString());
            output.Append("UUID: ");
            output.AppendLine(Client.Network.CurrentSim.ID.ToString());
            uint x, y;
            Utils.LongToUInts(Client.Network.CurrentSim.Handle, out x, out y);
            output.AppendLine(String.Format("Handle: {0} (X: {1} Y: {2})", Client.Network.CurrentSim.Handle, x, y));
            output.Append("Access: ");
            output.AppendLine(Client.Network.CurrentSim.Access.ToString());
            output.Append("Flags: ");
            output.AppendLine(Client.Network.CurrentSim.Flags.ToString());
            output.Append("TerrainBase0: ");
            output.AppendLine(Client.Network.CurrentSim.TerrainBase0.ToString());
            output.Append("TerrainBase1: ");
            output.AppendLine(Client.Network.CurrentSim.TerrainBase1.ToString());
            output.Append("TerrainBase2: ");
            output.AppendLine(Client.Network.CurrentSim.TerrainBase2.ToString());
            output.Append("TerrainBase3: ");
            output.AppendLine(Client.Network.CurrentSim.TerrainBase3.ToString());
            output.Append("TerrainDetail0: ");
            output.AppendLine(Client.Network.CurrentSim.TerrainDetail0.ToString());
            output.Append("TerrainDetail1: ");
            output.AppendLine(Client.Network.CurrentSim.TerrainDetail1.ToString());
            output.Append("TerrainDetail2: ");
            output.AppendLine(Client.Network.CurrentSim.TerrainDetail2.ToString());
            output.Append("TerrainDetail3: ");
            output.AppendLine(Client.Network.CurrentSim.TerrainDetail3.ToString());
            output.Append("Water Height: ");
            output.AppendLine(Client.Network.CurrentSim.WaterHeight.ToString());
            output.Append("Datacenter:");
            output.AppendLine(Client.Network.CurrentSim.ColoLocation);
            output.Append("CPU Ratio:");
            output.AppendLine(Client.Network.CurrentSim.CPURatio.ToString());
            output.Append("CPU Class:");
            output.AppendLine(Client.Network.CurrentSim.CPUClass.ToString());
            output.Append("Region SKU/Type:");
            output.AppendLine(Client.Network.CurrentSim.ProductSku + " " + Client.Network.CurrentSim.ProductName);

            return output.ToString();
        }
    }
}
