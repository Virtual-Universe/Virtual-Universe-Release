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

using Universe.Framework.ClientInterfaces;
using OpenMetaverse;

namespace Universe.Framework.Services
{
    public interface IAvatarService
    {
        /// <summary>
        ///     The local service (if possible)
        /// </summary>
        IAvatarService InnerService { get; }

        /// <summary>
        ///     Called by the login service
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        AvatarAppearance GetAppearance(UUID userID);

        /// <summary>
        ///     Called by everyone who can change the avatar data (so, regions)
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="appearance"></param>
        /// <returns></returns>
        void SetAppearance(UUID userID, AvatarAppearance appearance);

        /// <summary>
        ///     Not sure if it's needed
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        void ResetAvatar(UUID userID);

        /// <summary>
        ///     Gets a user's appearance, and if it does not exist, create it
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="defaultUserAvatarArchive"></param>
        /// <param name="loadedArchive"></param>
        /// <returns></returns>
        AvatarAppearance GetAndEnsureAppearance(UUID userID, string defaultUserAvatarArchive, out bool loadedArchive);
    }

    public interface IAgentAppearanceService
    {
        string ServiceURI { get; }

        AvatarAppearance BakeAppearance(UUID agentID, int cof_version);
    }
    
    public interface IAvatarData : IUniverseDataPlugin
    {
        AvatarAppearance Get(UUID PrincipalID);
        bool Store(UUID PrincipalID, AvatarAppearance data);
        bool Delete(UUID PrincipalID);
    }
}