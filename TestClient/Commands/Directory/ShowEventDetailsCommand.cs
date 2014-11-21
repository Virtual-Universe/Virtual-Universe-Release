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
    class ShowEventDetailsCommand : Command
    {
        public ShowEventDetailsCommand(TestClient testClient)
        {
            Name = "showevent";
            Description = "Shows an Events details. Usage: showevent [eventID]";
            Category = CommandCategory.Other;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length < 1)
                return "Usage: showevent [eventID] (use searchevents to get ID)";
            
            Client.Directory.EventInfoReply += Directory_EventDetails;
            uint eventID;

            if (UInt32.TryParse(args[0], out eventID))
            {
                Client.Directory.EventInfoRequest(eventID);
                return "Sent query for Event " + eventID;
            }
            else
            {
                return "Usage: showevent [eventID] (use searchevents to get ID)";
            }
        }

        void Directory_EventDetails(object sender, EventInfoReplyEventArgs e)
        {
            float x, y;
            Helpers.GlobalPosToRegionHandle((float)e.MatchedEvent.GlobalPos.X, (float)e.MatchedEvent.GlobalPos.Y, out x, out y);
            StringBuilder sb = new StringBuilder("secondlife://" + e.MatchedEvent.SimName + "/" + x + "/" + y + "/0" + System.Environment.NewLine);
            sb.AppendLine(e.MatchedEvent.ToString());
            
            //sb.AppendFormat("       Name: {0} ({1})" + System.Environment.NewLine, e.MatchedEvent.Name, e.MatchedEvent.ID);
            //sb.AppendFormat("   Location: {0}/{1}/{2}" + System.Environment.NewLine, e.MatchedEvent.SimName, x, y);
            //sb.AppendFormat("       Date: {0}" + System.Environment.NewLine, e.MatchedEvent.Date);
            //sb.AppendFormat("Description: {0}" + System.Environment.NewLine, e.MatchedEvent.Desc);
            Console.WriteLine(sb.ToString());
            Client.Directory.EventInfoReply -= Directory_EventDetails;
        }
    }
}
