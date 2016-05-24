/*
 * Copyright (c) Contributors, http://virtual-planets.org/, http://whitecore-sim.org/, http://aurora-sim.org, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual Universe Project nor the
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
 */


using Universe.Framework.Modules;
using Universe.Framework.PresenceInfo;
using Universe.Framework.SceneInfo;

namespace Universe.Modules.Monitoring.Monitors
{
    public class NetworkMonitor : INetworkMonitor
    {
        private volatile float inPacketsPerSecond;
        private volatile float outPacketsPerSecond;
        private volatile float pendingDownloads;
        private volatile float pendingUploads;
        private volatile float unackedBytes;

        public NetworkMonitor(IScene scene)
        {
            scene.EventManager.OnNewClient += OnNewClient;
            scene.EventManager.OnClosingClient += OnClosingClient;
        }

        #region IMonitor Members

        public void ResetStats()
        {
            inPacketsPerSecond = 0;
            outPacketsPerSecond = 0;
            unackedBytes = 0;
            pendingDownloads = 0;
            pendingUploads = 0;
        }

        #endregion

        #region Implementation of IMonitor

        public double GetValue()
        {
            return 0;
        }

        public string GetName()
        {
            return "Network Monitor";
        }

        public string GetInterfaceName()
        {
            return "INetworkMonitor";
        }

        public string GetFriendlyValue()
        {
            return "InPackets: " + inPacketsPerSecond + " p/sec \n"
                   + "OutPackets: " + outPacketsPerSecond + " p/sec \n"
                   + "UnackedBytes: " + unackedBytes + " bytes \n"
                   + "PendingDownloads: " + pendingDownloads + " \n"
                   + "PendingUploads: " + pendingUploads + " \n";
        }

        #endregion

        #region Client Handling

        protected void OnNewClient(IClientAPI client)
        {
            client.OnNetworkStatsUpdate += AddPacketsStats;
        }

        protected void OnClosingClient(IClientAPI client)
        {
            client.OnNetworkStatsUpdate -= AddPacketsStats;
        }

        #endregion

        #region INetworkMonitor Members

        public float InPacketsPerSecond
        {
            get { return inPacketsPerSecond; }
        }

        public float OutPacketsPerSecond
        {
            get { return outPacketsPerSecond; }
        }

        public float UnackedBytes
        {
            get { return unackedBytes; }
        }

        public float PendingDownloads
        {
            get { return pendingDownloads; }
        }

        public float PendingUploads
        {
            get { return pendingUploads; }
        }

        public void AddInPackets(int numPackets)
        {
            inPacketsPerSecond += numPackets;
        }

        public void AddOutPackets(int numPackets)
        {
            outPacketsPerSecond += numPackets;
        }

        public void AddUnackedBytes(int numBytes)
        {
            unackedBytes += numBytes;
        }

        public void AddPendingDownloads(int count)
        {
            pendingDownloads += count;
        }

        public void AddPendingUploads(int count)
        {
            pendingUploads += count;
        }

        #endregion

        public void AddPacketsStats(int inPackets, int outPackets, int unAckedBytes)
        {
            AddInPackets(inPackets);
            AddOutPackets(outPackets);
            AddUnackedBytes(unAckedBytes);
        }
    }
}