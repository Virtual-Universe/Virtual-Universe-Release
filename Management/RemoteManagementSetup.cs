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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nini.Config;
using Aurora.Framework;

namespace Management
{
    public partial class RemoteManagementSetup : Form
    {
        private IConfigSource _config;
        private RemoteHistory _historyLog;

        public RemoteManagementSetup(IConfigSource config)
        {
            InitializeComponent();

            _config = config;
            _historyLog = RemoteHistory.LoadFromFile<RemoteHistory>("history.xml");
            
            _history.ContextMenu = new System.Windows.Forms.ContextMenu();
            _history.ContextMenu.MenuItems.Add(new MenuItem("Remove", removeHistoryItem));
            
            if (_historyLog != null && _historyLog.IPAddresses != null)
                UpdateHistoryGUI();

            if (_historyLog == null)
                _historyLog = new RemoteHistory();
        }

        private void UpdateHistoryGUI()
        {
            _history.Items.Clear();
            for (int i = 0; i < _historyLog.IPAddresses.Count; i++)
            {
                _history.Items.Add(_historyLog.IPAddresses[i] + ":" + _historyLog.Ports[i]);
            }
        }

        private void removeHistoryItem(object sender, EventArgs args)
        {
            _historyLog.Remove(_history.SelectedItem.ToString());
            _history.Items.Remove(_history.SelectedItem);
        }

        private void connect_Click(object sender, EventArgs e)
        {
            string IPAddress = _ipaddress.Text.StartsWith("http://") ?
                _ipaddress.Text :
                "http://" + _ipaddress.Text;
            IPAddress += ":" + _port.Text;
            IRegionManagement management = new RegionManagement(IPAddress + "/regionmanagement",
                        _password.Text);
            if (!management.ConnectionIsWorking())
            {
                MessageBox.Show("Failed to connect to remote instance, check the IP and password and try again");
                return;
            }
            _historyLog.Add(_ipaddress.Text, _port.Text, _password.Text);
            UpdateHistoryGUI();
            RegionManagerHelper.StartAsynchronously(false,
                RegionManagerPage.CreateRegion,
                _config,
                management, null);
        }

        private void _history_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_history.SelectedItem == null)
                return;
            for (int i = 0; i < _historyLog.IPAddresses.Count; i++)
            {
                if (_history.SelectedItem.ToString() == (_historyLog.IPAddresses[i] + ":" + _historyLog.Ports[i]))
                {
                    _ipaddress.Text = _historyLog.IPAddresses[i];
                    _port.Text = _historyLog.Ports[i];
                    _password.Text = _historyLog.Passwords[i];
                }
            }
        }
    }

    public class RemoteHistory : FileSaving
    {
        public RemoteHistory()
            :base("history.xml")
        {
        }
        [System.Xml.Serialization.XmlArray]
        [System.Xml.Serialization.XmlArrayItem(ElementName = "IPAddress")]
        public List<string> IPAddresses = null;

        [System.Xml.Serialization.XmlArray]
        [System.Xml.Serialization.XmlArrayItem(ElementName = "Port")]
        public List<string> Ports = null;

        [System.Xml.Serialization.XmlArray]
        [System.Xml.Serialization.XmlArrayItem(ElementName = "Password")]
        public List<string> Passwords = null;

        public void Add(string ip, string port, string password)
        {
            if (IPAddresses == null)
                IPAddresses = new List<string>();
            if (Ports == null)
                Ports = new List<string>();
            if (Passwords == null)
                Passwords = new List<string>();
            for (int i = 0; i < IPAddresses.Count; i++)
            {
                if (ip == IPAddresses[i] &&
                    port == Ports[i])
                    return;
            }
            IPAddresses.Add(ip);
            Ports.Add(port);
            Passwords.Add(password);
            Save();
        }

        public void Remove(string ipPort)
        {
            int space = -1;
            for (int i = 0; i < IPAddresses.Count; i++)
                if (ipPort == IPAddresses[i] + ":" + Ports[i])
                    space = i;
            if (space >= 0)
            {
                IPAddresses.RemoveAt(space);
                Ports.RemoveAt(space);
                Passwords.RemoveAt(space);
            }
            Save();
        }
    }
}
