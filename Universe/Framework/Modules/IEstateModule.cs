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

using OpenMetaverse;

namespace Universe.Framework.Modules
{
    public delegate void ChangeDelegate(UUID regionID);

    public delegate void MessageDelegate(UUID regionID, UUID fromID, string fromName, string message);

    public interface IEstateModule : INonSharedRegionModule
    {
        event ChangeDelegate OnRegionInfoChange;
        event ChangeDelegate OnEstateInfoChange;
        event MessageDelegate OnEstateMessage;

        ulong GetRegionFlags();
        bool IsManager(UUID avatarID);

        /// <summary>
        ///     Tell all clients about the current state of the region (terrain textures, water height, etc.).
        /// </summary>
        void sendRegionHandshakeToAll();

        void setEstateTerrainBaseTexture(int level, UUID texture);
        void setEstateTerrainTextureHeights(int corner, float lowValue, float highValue);

        void TriggerEstateSunUpdate();

        /// <summary>
        ///     Disable/Enable the scripting engine, the collision events, and the physics engine
        /// </summary>
        /// <param name="ScriptEngine"></param>
        /// <param name="CollisionEvents"></param>
        /// <param name="PhysicsEngine"></param>
        void SetSceneCoreDebug(bool ScriptEngine, bool CollisionEvents, bool PhysicsEngine);
    }
}