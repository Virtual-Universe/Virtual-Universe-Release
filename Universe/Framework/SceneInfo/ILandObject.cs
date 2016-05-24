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

using System.Collections.Generic;
using OpenMetaverse;
using Universe.Framework.ClientInterfaces;
using Universe.Framework.PresenceInfo;
using Universe.Framework.SceneInfo.Entities;

namespace Universe.Framework.SceneInfo
{
    public interface ILandObject
    {
        LandData LandData { get; set; }
        UUID RegionUUID { get; }
        bool ContainsPoint(int x, int y);
        ILandObject Copy();

        void SendLandUpdateToAvatarsOverMe();

        void SendLandProperties(int sequence_id, bool snap_selection, int request_result, IClientAPI remote_client);
        void UpdateLandProperties(LandUpdateArgs args, IClientAPI remote_client);
        bool IsEitherBannedOrRestricted(UUID avatar);
        bool IsBannedFromLand(UUID avatar);
        bool IsRestrictedFromLand(UUID avatar);
        void SendLandUpdateToClient(IClientAPI remote_client);
        void SendLandUpdateToClient(bool snap_selection, IClientAPI remote_client);
        List<List<UUID>> CreateAccessListArrayByFlag(AccessList flag);
        void SendAccessList(UUID agentID, UUID sessionID, uint flags, int sequenceID, IClientAPI remote_client);
        void UpdateAccessList(uint flags, List<ParcelManager.ParcelAccessEntry> entries, IClientAPI remote_client);
        void ForceUpdateLandInfo();

        void SendForceObjectSelect(int local_id, int request_type, List<UUID> returnIDs, IClientAPI remote_client);
        void SendLandObjectOwners(IClientAPI remote_client);
        void ReturnLandObjects(uint type, UUID[] owners, UUID[] tasks, IClientAPI remote_client);
        void DisableLandObjects(uint type, UUID[] owners, UUID[] tasks, IClientAPI remote_client);
        void UpdateLandSold(UUID avatarID, UUID groupID, bool groupOwned, uint AuctionID, int claimprice, int area);

        void DeedToGroup(UUID groupID);

        /// <summary>
        ///     Set the media url for this land parcel
        /// </summary>
        /// <param name="url"></param>
        void SetMediaUrl(string url);

        /// <summary>
        ///     Set the music url for this land parcel
        /// </summary>
        /// <param name="url"></param>
        void SetMusicUrl(string url);

        List<ISceneEntity> GetPrimsOverByOwner(UUID targetID, int flags);
    }
}