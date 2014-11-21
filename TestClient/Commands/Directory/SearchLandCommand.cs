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
    /// <summary>
    /// 
    /// </summary>
    public class SearchLandCommand : Command
    {
        private System.Threading.AutoResetEvent waitQuery = new System.Threading.AutoResetEvent(false);
        private StringBuilder result = new StringBuilder();

        /// <summary>
        /// Construct a new instance of the SearchLandCommand
        /// </summary>
        /// <param name="testClient"></param>
        public SearchLandCommand(TestClient testClient)
        {
            Name = "searchland";
            Description = "Searches for land for sale. for usage information type: searchland";
            Category = CommandCategory.Search;
        }

        /// <summary>
        /// Show commandusage
        /// </summary>
        /// <returns>A string containing the parameter usage instructions</returns>
        public string ShowUsage()
        {
            return "Usage: searchland [type] [max price] [min size]\n" +
                "\twhere [type] is one of: mainland, auction, estate, all\n" +
                "\tif [max price] or [min size] are 0 that parameter will be ignored\n\n" +
                "example: \"searchland mainland 0 512\" // shows the lowest priced mainland that is larger than 512/m2\n\n";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="fromAgentID"></param>
        /// <returns></returns>
        public override string Execute(string[] args, UUID fromAgentID)
        {
            // process command line arguments
            if (args.Length < 3)
                return ShowUsage();

            string searchType = args[0].Trim().ToLower();
            int maxPrice;
            int minSize;

            DirectoryManager.SearchTypeFlags searchTypeFlags = DirectoryManager.SearchTypeFlags.Any;

            if (searchType.StartsWith("au"))
                searchTypeFlags = DirectoryManager.SearchTypeFlags.Auction;
            else if (searchType.StartsWith("m"))
                searchTypeFlags = DirectoryManager.SearchTypeFlags.Mainland;
            else if (searchType.StartsWith("e"))
                searchTypeFlags = DirectoryManager.SearchTypeFlags.Estate;
            else if (searchType.StartsWith("al"))
                searchTypeFlags = DirectoryManager.SearchTypeFlags.Any;
            else
                return ShowUsage();

            // initialize some default flags we'll use in the search
            DirectoryManager.DirFindFlags queryFlags = DirectoryManager.DirFindFlags.SortAsc | DirectoryManager.DirFindFlags.PerMeterSort
                | DirectoryManager.DirFindFlags.IncludeAdult | DirectoryManager.DirFindFlags.IncludePG | DirectoryManager.DirFindFlags.IncludeMature;

            // validate the parameters passed
            if (int.TryParse(args[1], out maxPrice) && int.TryParse(args[2], out minSize))
            {
                // if the [max price] parameter is greater than 0, we'll enable the flag to limit by price
                if (maxPrice > 0)
                    queryFlags |= DirectoryManager.DirFindFlags.LimitByPrice;

                // if the [min size] parameter is greater than 0, we'll enable the flag to limit by area
                if (minSize > 0)
                    queryFlags |= DirectoryManager.DirFindFlags.LimitByArea;
            }
            else
            {
                return ShowUsage();
            }

            //waitQuery.Reset();

            // subscribe to the event that returns the search results
            Client.Directory.DirLandReply += Directory_DirLand;

            // send the request to the directory manager
            Client.Directory.StartLandSearch(queryFlags, searchTypeFlags, maxPrice, minSize, 0);

            if (!waitQuery.WaitOne(20000, false) && Client.Network.Connected)
            {
                result.AppendLine("Timeout waiting for simulator to respond.");
            }

            // unsubscribe to the event that returns the search results
            Client.Directory.DirLandReply -= Directory_DirLand;

            // return the results
            return result.ToString();
        }

        /// <summary>
        /// Process the search reply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Directory_DirLand(object sender, DirLandReplyEventArgs e)
        {

            foreach (DirectoryManager.DirectoryParcel searchResult in e.DirParcels)
            {
                // add the results to the StringBuilder object that contains the results
                result.AppendLine(searchResult.ToString());
            }
            result.AppendFormat("{0} results" + System.Environment.NewLine, e.DirParcels.Count);
            // let the calling method know we have data
            waitQuery.Set();
        }
    }
}
