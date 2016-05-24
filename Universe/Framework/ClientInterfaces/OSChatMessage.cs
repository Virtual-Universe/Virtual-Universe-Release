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

using System;
using System.Collections.Generic;
using Universe.Framework.PresenceInfo;
using Universe.Framework.SceneInfo;
using OpenMetaverse;

namespace Universe.Framework.ClientInterfaces
{
    public interface IEventArgs
    {
        IScene Scene { get; set; }
        IClientAPI Sender { get; set; }
    }

    /// <summary>
    ///     ChatFromViewer Arguments
    /// </summary>
    public class OSChatMessage : EventArgs, IEventArgs
    {
        protected int m_channel;
        protected string m_from;
        protected UUID m_fromID;
        protected string m_message;
        protected Vector3 m_position;
        protected float m_range;

        protected IScene m_scene;
        protected IClientAPI m_sender;
        protected ISceneChildEntity m_senderObject;
        protected UUID m_toAgentID;
        protected ChatTypeEnum m_type;

        public OSChatMessage()
        {
            m_position = new Vector3();
        }

        /// <summary>
        ///     The message sent by the user
        /// </summary>
        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }

        /// <summary>
        ///     The type of message, e.g. say, shout, broadcast.
        /// </summary>
        public ChatTypeEnum Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        /// <summary>
        ///     Which channel was this message sent on? Different channels may have different listeners. Public chat is on channel zero.
        /// </summary>
        public int Channel
        {
            get { return m_channel; }
            set { m_channel = value; }
        }

        /// <summary>
        ///     How far should this chat go? -1 is default range for the type
        /// </summary>
        public float Range
        {
            get { return m_range; }
            set { m_range = value; }
        }

        /// <summary>
        ///     The position of the sender at the time of the message broadcast.
        /// </summary>
        public Vector3 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        /// <summary>
        ///     The name of the sender (needed for scripts)
        /// </summary>
        public string From
        {
            get { return m_from; }
            set { m_from = value; }
        }

        /// <summary>
        ///     The object responsible for sending the message, or null.
        /// </summary>
        public ISceneChildEntity SenderObject
        {
            get { return m_senderObject; }
            set { m_senderObject = value; }
        }

        public UUID SenderUUID
        {
            get { return m_fromID; }
            set { m_fromID = value; }
        }

        public UUID ToAgentID
        {
            get { return m_toAgentID; }
            set { m_toAgentID = value; }
        }

        #region IEventArgs Members

        /// TODO: Sender and SenderObject should just be Sender and of
        /// type IChatSender
        /// <summary>
        ///     The client responsible for sending the message, or null.
        /// </summary>
        public IClientAPI Sender
        {
            get { return m_sender; }
            set { m_sender = value; }
        }

        /// <summary>
        /// </summary>
        public IScene Scene
        {
            get { return m_scene; }
            set { m_scene = value; }
        }

        #endregion

        public OSChatMessage Copy()
        {
            OSChatMessage message = new OSChatMessage
                                        {
                                            Channel = Channel,
                                            From = From,
                                            Message = Message,
                                            Position = Position,
                                            Range = Range,
                                            Scene = Scene,
                                            Sender = Sender,
                                            SenderObject = SenderObject,
                                            SenderUUID = SenderUUID,
                                            Type = Type,
                                            ToAgentID = ToAgentID
                                        };
            return message;
        }

        public override string ToString()
        {
            return m_message;
        }


        public Dictionary<string, object> ToKVP()
        {
            Dictionary<string, object> kvp = new Dictionary<string, object>();
            kvp["Message"] = Message;
            kvp["Type"] = (int) Type;
            kvp["Channel"] = Channel;
            kvp["Range"] = Range;
            kvp["Position"] = Position;
            kvp["From"] = From;
            kvp["SenderUUID"] = SenderUUID;
            kvp["ToAgentID"] = ToAgentID;
            return kvp;
        }

        public void FromKVP(Dictionary<string, object> kvp)
        {
            Message = kvp["Message"].ToString();
            Type = ((ChatTypeEnum) int.Parse(kvp["Type"].ToString()));
            Channel = int.Parse(kvp["Channel"].ToString());
            Range = float.Parse(kvp["Range"].ToString());
            Position = Vector3.Parse(kvp["Position"].ToString());
            From = kvp["From"].ToString();
            SenderUUID = UUID.Parse(kvp["SenderUUID"].ToString());
            ToAgentID = UUID.Parse(kvp["ToAgentID"].ToString());
        }
    }
}