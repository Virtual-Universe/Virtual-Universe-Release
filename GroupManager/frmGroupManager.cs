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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenMetaverse;
using OpenMetaverse.Packets;

namespace groupmanager
{
    public partial class frmGroupManager : Form
    {
        GridClient Client;
        Dictionary<UUID, Group> Groups;

        public frmGroupManager()
        {
            Client = new GridClient();

            Client.Settings.MULTIPLE_SIMS = false;

            // Throttle unnecessary things down
            Client.Throttle.Land = 0;
            Client.Throttle.Wind = 0;
            Client.Throttle.Cloud = 0;

            Client.Network.LoginProgress += Network_OnLogin;
            Client.Network.EventQueueRunning += Network_OnEventQueueRunning;
            Client.Groups.CurrentGroups += Groups_CurrentGroups;
            
            InitializeComponent();
        }

        void Groups_CurrentGroups(object sender, CurrentGroupsEventArgs e)
        {
            Groups = e.Groups;

            Invoke(new MethodInvoker(UpdateGroups));
        }

        private void UpdateGroups()
        {
            lock (lstGroups)
            {
                Invoke((MethodInvoker)delegate() { lstGroups.Items.Clear(); });

                foreach (Group group in Groups.Values)
                {
                    Logger.Log(String.Format("Adding group {0} ({1})", group.Name, group.ID), Helpers.LogLevel.Info, Client);

                    Invoke((MethodInvoker)delegate() { lstGroups.Items.Add(group); });
                }
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            frmGroupManager frm = new frmGroupManager();
            frm.ShowDialog();
        }

        #region GUI Callbacks

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            if (cmdConnect.Text == "Connect")
            {
                cmdConnect.Text = "Disconnect";
                txtFirstName.Enabled = txtLastName.Enabled = txtPassword.Enabled = false;

                LoginParams loginParams = Client.Network.DefaultLoginParams(txtFirstName.Text, txtLastName.Text,
                    txtPassword.Text, "GroupManager", "1.0.0");
                Client.Network.BeginLogin(loginParams);
            }
			else
			{
				Client.Network.Logout();
				cmdConnect.Text = "Connect";
				txtFirstName.Enabled = txtLastName.Enabled = txtPassword.Enabled = true;
                groupBox.Enabled = false;
                lstGroups.Items.Clear();
			}
        }

        private void lstGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGroups.SelectedIndex >= 0)
            {
                cmdActivate.Enabled = cmdInfo.Enabled = cmdLeave.Enabled = true;
            }
            else
            {
                cmdActivate.Enabled = cmdInfo.Enabled = cmdLeave.Enabled = false;
            }
        }

        private void cmdInfo_Click(object sender, EventArgs e)
        {
            if (lstGroups.SelectedIndex >= 0 && lstGroups.Items[lstGroups.SelectedIndex].ToString() != "none")
            {
                Group group = (Group)lstGroups.Items[lstGroups.SelectedIndex];

                frmGroupInfo frm = new frmGroupInfo(group, Client);
                frm.ShowDialog();
            }
        }

        #endregion GUI Callbacks

        #region Network Callbacks

        private void Network_OnLogin(object sender, LoginProgressEventArgs e)
        {
            if (e.Status == LoginStatus.Success)
            {
                BeginInvoke(
                    (MethodInvoker)delegate()
                    {
                        groupBox.Enabled = true;
                    });
            }
            else if (e.Status == LoginStatus.Failed)
            {
                BeginInvoke(
                    (MethodInvoker)delegate()
                    {
                        MessageBox.Show(this, "Error logging in: " + Client.Network.LoginMessage);
                        cmdConnect.Text = "Connect";
                        txtFirstName.Enabled = txtLastName.Enabled = txtPassword.Enabled = true;
                        groupBox.Enabled = false;
                        lstGroups.Items.Clear();
                    });
            }
        }

        private void Network_OnEventQueueRunning(object sender, EventQueueRunningEventArgs e)
        {
            if (e.Simulator == Client.Network.CurrentSim)
            {
                Console.WriteLine("Event queue connected for the primary simulator, requesting group info");

                Client.Groups.RequestCurrentGroups();
            }
        }

        #endregion
    }
}
