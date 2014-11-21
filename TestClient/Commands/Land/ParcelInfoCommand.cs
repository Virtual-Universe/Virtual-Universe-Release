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

namespace OpenMetaverse.TestClient
{
    public class ParcelInfoCommand : Command
    {
        private AutoResetEvent ParcelsDownloaded = new AutoResetEvent(false);

        public ParcelInfoCommand(TestClient testClient)
        {
            Name = "parcelinfo";
            Description = "Prints out info about all the parcels in this simulator";
            Category = CommandCategory.Parcel;

            testClient.Network.Disconnected += Network_OnDisconnected;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            StringBuilder sb = new StringBuilder();
            string result;
            EventHandler<SimParcelsDownloadedEventArgs> del = delegate(object sender, SimParcelsDownloadedEventArgs e)
            {
                ParcelsDownloaded.Set();
            };
            

            ParcelsDownloaded.Reset();
            Client.Parcels.SimParcelsDownloaded += del;
            Client.Parcels.RequestAllSimParcels(Client.Network.CurrentSim);

            if (Client.Network.CurrentSim.IsParcelMapFull())
                ParcelsDownloaded.Set();

            if (ParcelsDownloaded.WaitOne(30000, false) && Client.Network.Connected)
            {
                sb.AppendFormat("Downloaded {0} Parcels in {1} " + System.Environment.NewLine, 
                    Client.Network.CurrentSim.Parcels.Count, Client.Network.CurrentSim.Name);

                Client.Network.CurrentSim.Parcels.ForEach(delegate(Parcel parcel)
                {
                    sb.AppendFormat("Parcel[{0}]: Name: \"{1}\", Description: \"{2}\" ACLBlacklist Count: {3}, ACLWhiteList Count: {5} Traffic: {4}" + System.Environment.NewLine,
                        parcel.LocalID, parcel.Name, parcel.Desc, parcel.AccessBlackList.Count, parcel.Dwell, parcel.AccessWhiteList.Count);
                });

                result = sb.ToString();
            }
            else
                result = "Failed to retrieve information on all the simulator parcels";

            Client.Parcels.SimParcelsDownloaded -= del;
            return result;
        }

        void Network_OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            ParcelsDownloaded.Set();
        }
    }
}
