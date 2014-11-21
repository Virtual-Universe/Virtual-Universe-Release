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
using System;

namespace IRCGateway
{
    class Program
    {
        static GridClient _Client;
        static LoginParams _ClientLogin;
        static IRCClient _IRC;
        static string _AutoJoinChannel;
        static UUID _MasterID;

        static void Main(string[] args)
        {
            int ircPort;

            if (args.Length < 7 || !UUID.TryParse(args[3], out _MasterID) || !int.TryParse(args[5], out ircPort) || args[6].IndexOf('#') == -1)
                Console.WriteLine("Usage: ircgateway.exe <firstName> <lastName> <password> <masterUUID> <ircHost> <ircPort> <#channel>");

            else
            {
                _Client = new GridClient();
                _Client.Network.LoginProgress += Network_OnLogin;
                _Client.Self.ChatFromSimulator += Self_ChatFromSimulator;                
                _Client.Self.IM += Self_IM;
                _ClientLogin = _Client.Network.DefaultLoginParams(args[0], args[1], args[2], "", "IRCGateway");

                _AutoJoinChannel = args[6];
                _IRC = new IRCClient(args[4], ircPort, "SLGateway", "Second Life Gateway");
                _IRC.OnConnected += new IRCClient.ConnectCallback(_IRC_OnConnected);
                _IRC.OnMessage += new IRCClient.MessageCallback(_IRC_OnMessage);

                _IRC.Connect();

                string read = Console.ReadLine();
                while (read != null) read = Console.ReadLine();                
            }
        }

        static void Self_IM(object sender, InstantMessageEventArgs e)
        {
            if (e.IM.Dialog == InstantMessageDialog.RequestTeleport)
            {
                if (e.IM.FromAgentID == _MasterID)
                {
                    _Client.Self.TeleportLureRespond(e.IM.FromAgentID, e.IM.IMSessionID, true);
                }
            }
        }

        static void Self_ChatFromSimulator(object sender, ChatEventArgs e)
        {
            if (e.FromName != _Client.Self.Name && e.Type == ChatType.Normal && e.AudibleLevel == ChatAudibleLevel.Fully)
            {
                string str = "<" + e.FromName + "> " + e.Message;
                _IRC.SendMessage(_AutoJoinChannel, str);
                Console.WriteLine("[SL->IRC] " + str);
            }
        }

        static void _IRC_OnConnected()
        {
            _IRC.JoinChannel(_AutoJoinChannel);
            _Client.Network.BeginLogin(_ClientLogin);
        }

        static void _IRC_OnMessage(string target, string name, string address, string message)
        {
            if (target == _AutoJoinChannel)
            {
                string str = "<" + name + "> " + message;
                _Client.Self.Chat(str, 0, ChatType.Normal);
                Console.WriteLine("[IRC->SL] " + str);
            }
        }

        static void Network_OnLogin(object sender, LoginProgressEventArgs e)
        {
            _IRC.SendMessage(_AutoJoinChannel, e.Message);
        }
    }
}
