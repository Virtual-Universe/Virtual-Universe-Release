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
using OpenMetaverse.StructuredData;

namespace Universe.Framework.Modules
{
    public interface IWorldCommListenerInfo
    {
        OSD GetSerializationData();
        UUID GetItemID();
        UUID GetHostID();
        int GetChannel();
        int GetHandle();
        string GetMessage();
        string GetName();
        bool IsActive();
        void Deactivate();
        void Activate();
        UUID GetID();
    }

    public interface IWorldComm
    {
        /// <summary>
        ///     Create a listen event callback with the specified filters.
        ///     The parameters localID,itemID are needed to uniquely identify
        ///     the script during 'peek' time. Parameter hostID is needed to
        ///     determine the position of the script.
        /// </summary>
        /// <param name="itemID">UUID of the script engine</param>
        /// <param name="hostID">UUID of the SceneObjectPart</param>
        /// <param name="channel">channel to listen on</param>
        /// <param name="name">name to filter on</param>
        /// <param name="id">key to filter on (user given, could be totally faked)</param>
        /// <param name="msg">msg to filter on</param>
        /// <param name="regexBitfield">Bitfield indicating which strings should be processed as regex.</param>
        /// <returns>number of the scripts handle</returns>
        int Listen(UUID itemID, UUID hostID, int channel, string name, UUID id, string msg, int regexBitfield);

        /// <summary>
        ///     This method scans over the objects which registered an interest in listen callbacks.
        ///     For everyone it finds, it checks if it fits the given filter. If it does,  then
        ///     enqueue the message for delivery to the objects listen event handler.
        ///     The enqueued ListenerInfo no longer has filter values, but the actually trigged values.
        ///     Objects that do an llSay have their messages delivered here and for nearby avatars,
        ///     the OnChatFromClient event is used.
        /// </summary>
        /// <param name="type">type of delivery (whisper,say,shout or regionwide)</param>
        /// <param name="channel">channel to sent on</param>
        /// <param name="name">name of sender (object or avatar)</param>
        /// <param name="id">key of sender (object or avatar)</param>
        /// <param name="msg">msg to sent</param>
        void DeliverMessage(ChatTypeEnum type, int channel, string name, UUID id, string msg);

        void DeliverMessage(ChatTypeEnum type, int channel, string name, UUID id, UUID to, string msg);
        void DeliverMessage(ChatTypeEnum type, int channel, string name, UUID id, string msg, float Range);

        /// <summary>
        ///     Are there any listen events ready to be dispatched?
        /// </summary>
        /// <returns>Boolean indication</returns>
        bool HasMessages();

        /// <summary>
        ///     Are there any listeners currently?
        /// </summary>
        /// <returns></returns>
        bool HasListeners();

        /// <summary>
        ///     Pop the first available listen event from the queue
        /// </summary>
        /// <returns>ListenerInfo with filter filled in</returns>
        IWorldCommListenerInfo GetNextMessage();

        void ListenControl(UUID itemID, int handle, int active);
        void ListenRemove(UUID itemID, int handle);
        void DeleteListener(UUID itemID);
        OSD GetSerializationData(UUID itemID, UUID primID);

        void CreateFromData(UUID itemID, UUID hostID,
                            OSD data);

        void AddBlockedChannel(int channel);
        void RemoveBlockedChannel(int channel);
    }
}