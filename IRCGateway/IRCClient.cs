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
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class IRCClient
{
    private TcpClient ircClient;
    private bool Shutdown = false;
    private string ServerHost;
    private int ServerPort;
    private string Nickname;
    private string RealName = String.Empty;
    private Thread LoopThread;
    private Thread ConnectThread;

    public delegate void ConnectCallback();
    public event ConnectCallback OnConnectFail;
    public event ConnectCallback OnConnected;
    public event ConnectCallback OnDisconnected;

    public delegate void DataCallback(string data);
    public event DataCallback OnData;

    public delegate void MessageCallback(string target, string name, string address, string message);
    public event MessageCallback OnMessage;

    /// <summary>
    /// Basic class for a threaded, sychronous TCP client with built-in functions and events for IRC connectivity
    /// </summary>
    /// <param name="serverHost"></param>
    /// <param name="port"></param>
    /// <param name="nickname"></param>
    /// <param name="realName"></param>
    public IRCClient(string serverHost, int port, string nickname, string realName)
    {
        ircClient = new TcpClient();
        ServerHost = serverHost;
        ServerPort = port;
        Nickname = nickname;
        RealName = realName;
    }

    /// <summary>
    /// Connect to IRC network
    /// </summary>
    public void Connect()
    {
        ConnectThread = new Thread(new ThreadStart(ConnectThreadStart));
        ConnectThread.Start();
    }

    /// <summary>
    /// Connect to IRC network with the specified parameters
    /// </summary>
    /// <param name="serverHost"></param>
    /// <param name="port"></param>
    /// <param name="nickname"></param>
    /// <param name="realName"></param>
    public void Connect(string serverHost, int port, string nickname, string realName)
    {
        ServerHost = serverHost;
        ServerPort = port;
        Nickname = nickname;
        RealName = realName;

        Connect();
    }

    /// <summary>
    /// Join an IRC channel
    /// </summary>
    /// <param name="channel"></param>
    public void JoinChannel(string channel)
    {
        ircClient.Client.Send(Encoding.ASCII.GetBytes("JOIN " + channel + "\r\n"));
    }

    /// <summary>
    /// Send a message to the specified nickname or channel
    /// </summary>
    /// <param name="target"></param>
    /// <param name="message"></param>
    public void SendMessage(string target, string message)
    {
        ircClient.Client.Send(Encoding.ASCII.GetBytes("PRIVMSG " + target + " :" + message + "\r\n"));
    }

    private void ConnectThreadStart()
    {
        ircClient.Connect(ServerHost, ServerPort);

        if (!ircClient.Connected)
        {
            if (OnConnectFail != null)
                OnConnectFail();

            return;
        }

        ircClient.Client.Send(Encoding.ASCII.GetBytes("USER " + Nickname + " x x :" + RealName + "\r\n"));
        ircClient.Client.Send(Encoding.ASCII.GetBytes("NICK " + Nickname + "\r\n"));

        LoopThread = new Thread(new ThreadStart(LoopThreadStart));
        LoopThread.Start();
    }

    private void LoopThreadStart()
    {
        while (!Shutdown && ircClient.Connected)
        {
            byte[] buffer = new byte[4096];
            ircClient.Client.Receive(buffer);
            if (buffer.Length == 0) break;

            string[] lines = Encoding.ASCII.GetString(buffer).Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

            for(int i=0; i<lines.Length - 1; i++)
            {
                string[] words = lines[i].Split(new char[] { ' ' });

                if (OnData != null && lines[i].Length > 0)
                    OnData(lines[i]);

                if (words.Length < 2) return;

                if (words[0].ToUpper() == "PING")
                    ircClient.Client.Send(Encoding.ASCII.GetBytes("PONG " + words[1] + "\r\n"));

                else if (words[1] == "001")
                {
                    if (OnConnected != null)
                        OnConnected();
                }

                else if (words[1].ToUpper() == "PRIVMSG")
                {
                    if (OnMessage != null)
                    {
                        int nameIndex = words[0].IndexOf('!');
                        string name = nameIndex > 0 ? words[0].Substring(1, nameIndex - 1) : words[0];
                        string address = words[0].Substring(nameIndex + 1);
                        OnMessage(words[2], name, address, lines[i].Substring(lines[i].IndexOf(":", 1) + 1));
                    }
                }
            }            
        }

        if (!ircClient.Connected)
        {
            if (OnDisconnected != null)
                OnDisconnected();
        }

        else ircClient.Close();
    }

}
