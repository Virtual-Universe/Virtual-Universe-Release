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
using System.Threading;
using System.Reflection;
using System.Xml;
using OpenMetaverse;
using OpenMetaverse.Packets;
using OpenMetaverse.Utilities;

namespace OpenMetaverse.TestClient
{
    public class TestClient : GridClient
    {
        public UUID GroupID = UUID.Zero;
        public Dictionary<UUID, GroupMember> GroupMembers;
        public Dictionary<UUID, AvatarAppearancePacket> Appearances = new Dictionary<UUID, AvatarAppearancePacket>();
        public Dictionary<string, Command> Commands = new Dictionary<string, Command>();
        public bool Running = true;
        public bool GroupCommands = false;
        public string MasterName = String.Empty;
        public UUID MasterKey = UUID.Zero;
        public bool AllowObjectMaster = false;
        public ClientManager ClientManager;
        public VoiceManager VoiceManager;
        // Shell-like inventory commands need to be aware of the 'current' inventory folder.
        public InventoryFolder CurrentDirectory = null;

        private System.Timers.Timer updateTimer;
        private UUID GroupMembersRequestID;
        public Dictionary<UUID, Group> GroupsCache = null;
        private ManualResetEvent GroupsEvent = new ManualResetEvent(false);

        /// <summary>
        /// 
        /// </summary>
        public TestClient(ClientManager manager)
        {
            ClientManager = manager;

            updateTimer = new System.Timers.Timer(500);
            updateTimer.Elapsed += new System.Timers.ElapsedEventHandler(updateTimer_Elapsed);

            RegisterAllCommands(Assembly.GetExecutingAssembly());

            Settings.LOG_LEVEL = Helpers.LogLevel.Debug;
            Settings.LOG_RESENDS = false;
            Settings.STORE_LAND_PATCHES = true;
            Settings.ALWAYS_DECODE_OBJECTS = true;
            Settings.ALWAYS_REQUEST_OBJECTS = true;
            Settings.SEND_AGENT_UPDATES = true;
            Settings.USE_ASSET_CACHE = true;

            Network.RegisterCallback(PacketType.AgentDataUpdate, AgentDataUpdateHandler);
            Network.LoginProgress += LoginHandler;
            Objects.AvatarUpdate += new EventHandler<AvatarUpdateEventArgs>(Objects_AvatarUpdate);
            Objects.TerseObjectUpdate += new EventHandler<TerseObjectUpdateEventArgs>(Objects_TerseObjectUpdate);
            Network.SimChanged += new EventHandler<SimChangedEventArgs>(Network_SimChanged);
            Self.IM += Self_IM;
            Groups.GroupMembersReply += GroupMembersHandler;
            Inventory.InventoryObjectOffered += Inventory_OnInventoryObjectReceived;            

            Network.RegisterCallback(PacketType.AvatarAppearance, AvatarAppearanceHandler);
            Network.RegisterCallback(PacketType.AlertMessage, AlertMessageHandler);

            VoiceManager = new VoiceManager(this);

            updateTimer.Start();
        }

        void Objects_TerseObjectUpdate(object sender, TerseObjectUpdateEventArgs e)
        {
            if (e.Prim.LocalID == Self.LocalID)
            {
                SetDefaultCamera();
            }
        }

        void Objects_AvatarUpdate(object sender, AvatarUpdateEventArgs e)
        {
            if (e.Avatar.LocalID == Self.LocalID)
            {
                SetDefaultCamera();
            }
        }

        void Network_SimChanged(object sender, SimChangedEventArgs e)
        {
            Self.Movement.SetFOVVerticalAngle(Utils.TWO_PI - 0.05f);
        }

        public void SetDefaultCamera()
        {
            // SetCamera 5m behind the avatar
            Self.Movement.Camera.LookAt(
                Self.SimPosition + new Vector3(-5, 0, 0) * Self.Movement.BodyRotation,
                Self.SimPosition
            );
        }


        void Self_IM(object sender, InstantMessageEventArgs e)
        {
            bool groupIM = e.IM.GroupIM && GroupMembers != null && GroupMembers.ContainsKey(e.IM.FromAgentID) ? true : false;

            if (e.IM.FromAgentID == MasterKey || (GroupCommands && groupIM))
            {
                // Received an IM from someone that is authenticated
                Console.WriteLine("<{0} ({1})> {2}: {3} (@{4}:{5})", e.IM.GroupIM ? "GroupIM" : "IM", e.IM.Dialog, e.IM.FromAgentName, e.IM.Message, 
                    e.IM.RegionID, e.IM.Position);

                if (e.IM.Dialog == InstantMessageDialog.RequestTeleport)
                {
                    Console.WriteLine("Accepting teleport lure.");
                    Self.TeleportLureRespond(e.IM.FromAgentID, e.IM.IMSessionID, true);
                }
                else if (
                    e.IM.Dialog == InstantMessageDialog.MessageFromAgent ||
                    e.IM.Dialog == InstantMessageDialog.MessageFromObject)
                {
                    ClientManager.Instance.DoCommandAll(e.IM.Message, e.IM.FromAgentID);
                }
            }
            else
            {
                // Received an IM from someone that is not the bot's master, ignore
                Console.WriteLine("<{0} ({1})> {2} (not master): {3} (@{4}:{5})", e.IM.GroupIM ? "GroupIM" : "IM", e.IM.Dialog, e.IM.FromAgentName, e.IM.Message,
                    e.IM.RegionID, e.IM.Position);
                return;
            }
        }

        /// <summary>
        /// Initialize everything that needs to be initialized once we're logged in.
        /// </summary>
        /// <param name="login">The status of the login</param>
        /// <param name="message">Error message on failure, MOTD on success.</param>
        public void LoginHandler(object sender, LoginProgressEventArgs e)
        {
            if (e.Status == LoginStatus.Success)
            {
                // Start in the inventory root folder.
                CurrentDirectory = Inventory.Store.RootFolder;
            }
        }

        public void RegisterAllCommands(Assembly assembly)
        {
            foreach (Type t in assembly.GetTypes())
            {
                try
                {
                    if (t.IsSubclassOf(typeof(Command)))
                    {
                        ConstructorInfo info = t.GetConstructor(new Type[] { typeof(TestClient) });
                        Command command = (Command)info.Invoke(new object[] { this });
                        RegisterCommand(command);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void RegisterCommand(Command command)
        {
            command.Client = this;
            if (!Commands.ContainsKey(command.Name.ToLower()))
            {
                Commands.Add(command.Name.ToLower(), command);
            }
        }

        public void ReloadGroupsCache()
        {
            Groups.CurrentGroups += Groups_CurrentGroups;            
            Groups.RequestCurrentGroups();
            GroupsEvent.WaitOne(10000, false);
            Groups.CurrentGroups -= Groups_CurrentGroups;
            GroupsEvent.Reset();
        }

        void Groups_CurrentGroups(object sender, CurrentGroupsEventArgs e)
        {
            if (null == GroupsCache)
                GroupsCache = e.Groups;
            else
                lock (GroupsCache) { GroupsCache = e.Groups; }
            GroupsEvent.Set();
        }

        public UUID GroupName2UUID(String groupName)
        {
            UUID tryUUID;
            if (UUID.TryParse(groupName,out tryUUID))
                    return tryUUID;
            if (null == GroupsCache) {
                    ReloadGroupsCache();
                if (null == GroupsCache)
                    return UUID.Zero;
            }
            lock(GroupsCache) {
                if (GroupsCache.Count > 0) {
                    foreach (Group currentGroup in GroupsCache.Values)
                        if (currentGroup.Name.ToLower() == groupName.ToLower())
                            return currentGroup.ID;
                }
            }
            return UUID.Zero;
        }      

        private void updateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (Command c in Commands.Values)
                if (c.Active)
                    c.Think();
        }

        private void AgentDataUpdateHandler(object sender, PacketReceivedEventArgs e)
        {
            AgentDataUpdatePacket p = (AgentDataUpdatePacket)e.Packet;
            if (p.AgentData.AgentID == e.Simulator.Client.Self.AgentID && p.AgentData.ActiveGroupID != UUID.Zero)
            {
                GroupID = p.AgentData.ActiveGroupID;
                
                GroupMembersRequestID = e.Simulator.Client.Groups.RequestGroupMembers(GroupID);
            }
        }

        private void GroupMembersHandler(object sender, GroupMembersReplyEventArgs e)
        {
            if (e.RequestID != GroupMembersRequestID) return;

            GroupMembers = e.Members;
        }

        private void AvatarAppearanceHandler(object sender, PacketReceivedEventArgs e)
        {
            Packet packet = e.Packet;
            
            AvatarAppearancePacket appearance = (AvatarAppearancePacket)packet;

            lock (Appearances) Appearances[appearance.Sender.ID] = appearance;
        }

        private void AlertMessageHandler(object sender, PacketReceivedEventArgs e)
        {
            Packet packet = e.Packet;
            
            AlertMessagePacket message = (AlertMessagePacket)packet;

            Logger.Log("[AlertMessage] " + Utils.BytesToString(message.AlertData.Message), Helpers.LogLevel.Info, this);
        }
       
        private void Inventory_OnInventoryObjectReceived(object sender, InventoryObjectOfferedEventArgs e)
        {
            if (MasterKey != UUID.Zero)
            {
                if (e.Offer.FromAgentID != MasterKey)
                    return;
            }
            else if (GroupMembers != null && !GroupMembers.ContainsKey(e.Offer.FromAgentID))
            {
                return;
            }

            e.Accept = true;
            return;
        }
    }
}
