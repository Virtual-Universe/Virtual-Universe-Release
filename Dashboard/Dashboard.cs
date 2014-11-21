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

using OpenMetaverse;
using OpenMetaverse.GUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class Dashboard : Form
    {

        GridClient Client;
        LoginParams ClientLogin;
        bool ShuttingDown = false;

        /// <summary>
        /// Provides a full representation of OpenMetaverse.GUI
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        public Dashboard(string firstName, string lastName, string password)
        {
            InitializeComponent();

            //force logout and exit when form is closed
            this.FormClosing += new FormClosingEventHandler(Dashboard_FormClosing);

            //initialize the client object and related controls
            InitializeClient(true);

            //double-click events
            avatarList1.OnAvatarDoubleClick += new AvatarList.AvatarCallback(avatarList1_OnAvatarDoubleClick);
            friendsList1.OnFriendDoubleClick += new FriendList.FriendDoubleClickCallback(friendsList1_OnFriendDoubleClick);
            groupList1.OnGroupDoubleClick += new GroupList.GroupDoubleClickCallback(groupList1_OnGroupDoubleClick);

            //login
            ClientLogin = Client.Network.DefaultLoginParams(firstName, lastName, password, "OpenMetaverse Dashboard", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            loginPanel1.LoginParams = ClientLogin;

            ClientLogin.Start = "last";
            
            if (firstName != String.Empty && lastName != String.Empty && password != String.Empty)
                Client.Network.BeginLogin(ClientLogin);
        }

        private void InitializeClient(bool initialize)
        {
            if (Client != null)
            {
                if (Client.Network.Connected)
                    Client.Network.Logout();

                Client = null;
            }

            if (!initialize) return;

            //initialize client object
            Client = new GridClient();
            Client.Settings.USE_LLSD_LOGIN = true;
            Client.Settings.USE_ASSET_CACHE = true;

            Client.Network.Disconnected += Network_OnDisconnected;
            Client.Self.IM += Self_IM;

            //define the client object for each GUI element
            avatarList1.Client = Client;
            friendsList1.Client = Client;
            groupList1.Client = Client;
            inventoryTree1.Client = Client;
            localChat1.Client = Client;
            loginPanel1.Client = Client;
            messageBar1.Client = Client;
            miniMap1.Client = Client;
            statusOutput1.Client = Client;
        }

        void Self_IM(object sender, InstantMessageEventArgs e)
        {
            if (e.IM.Dialog == InstantMessageDialog.RequestTeleport)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    DialogResult result = MessageBox.Show(this, e.IM.FromAgentName + " has offered you a teleport request:" + Environment.NewLine + e.IM.Message, this.Text, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                        Client.Self.TeleportLureRespond(e.IM.FromAgentID, e.IM.IMSessionID, true);
                });
            }
        }

        void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShuttingDown = true;
            InitializeClient(false);
            Environment.Exit(0);
        }

        void avatarList1_OnAvatarDoubleClick(TrackedAvatar trackedAvatar)
        {
            messageBar1.CreateSession(trackedAvatar.Name, trackedAvatar.ID, trackedAvatar.ID, true);
        }

        void friendsList1_OnFriendDoubleClick(FriendInfo friend)
        {
            messageBar1.CreateSession(friend.Name, friend.UUID, friend.UUID, true);
        }

        void groupList1_OnGroupDoubleClick(Group group)
        {
            MessageBox.Show(group.Name + " = " + group.ID);
        }

        void Network_OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            InitializeClient(!ShuttingDown);
        }        
    }
}