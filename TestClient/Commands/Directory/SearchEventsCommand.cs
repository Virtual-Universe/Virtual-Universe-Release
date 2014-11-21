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
    class SearchEventsCommand : Command
    {
        System.Threading.AutoResetEvent waitQuery = new System.Threading.AutoResetEvent(false);
        int resultCount;

        public SearchEventsCommand(TestClient testClient)
        {
            Name = "searchevents";
            Description = "Searches Events list. Usage: searchevents [search text]";
            Category = CommandCategory.Search;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            // process command line arguments
            if (args.Length < 1)
                return "Usage: searchevents [search text]";

            string searchText = string.Empty;
            for (int i = 0; i < args.Length; i++)
                searchText += args[i] + " ";
            searchText = searchText.TrimEnd();

            waitQuery.Reset();
            
            Client.Directory.DirEventsReply += Directory_DirEvents;

            // send the request to the directory manager
            Client.Directory.StartEventsSearch(searchText, 0);
            string result;
            if (waitQuery.WaitOne(20000, false) && Client.Network.Connected)
            {
                result =  "Your query '" + searchText + "' matched " + resultCount + " Events. ";
            }
            else
            {
                result =  "Timeout waiting for simulator to respond.";
            }

            Client.Directory.DirEventsReply -= Directory_DirEvents;
            
            return result;
        }

        void Directory_DirEvents(object sender, DirEventsReplyEventArgs e)
        {
            if (e.MatchedEvents[0].ID == 0 && e.MatchedEvents.Count == 1)
            {
                Console.WriteLine("No Results matched your search string");
            }
            else
            {
                foreach (DirectoryManager.EventsSearchData ev in e.MatchedEvents)
                {                    
                    Console.WriteLine("Event ID: {0} Event Name: {1} Event Date: {2}", ev.ID, ev.Name, ev.Date);
                }
            }
            resultCount = e.MatchedEvents.Count;
            waitQuery.Set();
        }
    }
}
