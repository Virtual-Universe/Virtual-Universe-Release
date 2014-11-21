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

using Aurora.Framework.ConsoleFramework;
using Aurora.Framework.Modules;
using Aurora.Framework.Servers;
using Aurora.Framework.Servers.HttpServer.Implementation;
using Aurora.Framework.Services;
using Nini.Config;
using Nwc.XmlRpc;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;

namespace Aurora.Services
{
    public class GridInfoHandlers : IGridInfo
    {
        readonly Hashtable _info = new Hashtable();

        public string GridName { get; protected set; }
        public string GridNick { get; protected set; }
        public string GridLoginURI { get; protected set; }
        public string GridWelcomeURI { get; protected set; }
        public string GridEconomyURI { get; protected set; }
        public string GridAboutURI { get; protected set; }
        public string GridHelpURI { get; protected set; }
        public string GridRegisterURI { get; protected set; }
        public string GridForgotPasswordURI { get; protected set; }
        public string GridMapTileURI { get; set; }
        public string AgentAppearanceURI { get; set; }
        public string GridWebProfileURI { get; protected set; }
        public string GridSearchURI { get; protected set; }
        public string GridDestinationURI { get; protected set; }
        public string GridMarketplaceURI { get; protected set; }
        public string GridTutorialURI { get; protected set; }
        public string GridSnapshotConfigURI { get; protected set; }
        protected IConfigSource m_config;
        protected IRegistryCore m_registry;

        /// <summary>
        ///     Instantiate a GridInfo object.
        /// </summary>
        /// <param name="configSource">path to config path containing grid information</param>
        /// <param name="registry"></param>
        /// <remarks>
        ///     GridInfo uses the [GridInfo] section of the
        ///     standard Planets.ini file --- which is not optimal, but
        ///     anything else requires a general redesign of the config
        ///     system.
        /// </remarks>
        public GridInfoHandlers(IConfigSource configSource, IRegistryCore registry)
        {
            m_config = configSource;
            m_registry = registry;
            UpdateGridInfo();
        }

        public void UpdateGridInfo()
        {
            IConfig gridCfg = m_config.Configs["GridInfo"];
            if (gridCfg == null)
                return;
            _info["platform"] = "VirtualReality";
            try
            {
                IConfig configCfg = m_config.Configs["Handlers"];
                IWebInterfaceModule webInterface = m_registry.RequestModuleInterface<IWebInterfaceModule>();
                IMoneyModule moneyModule = m_registry.RequestModuleInterface<IMoneyModule>();
                IGridServerInfoService serverInfoService = m_registry.RequestModuleInterface<IGridServerInfoService>();


                // login
                GridLoginURI = GetConfig(m_config, "login");
                if (GridLoginURI == "")
                {
                    GridLoginURI = MainServer.Instance.ServerURI + "/";

                    if (configCfg != null && configCfg.GetString("LLLoginHandlerPort", "") != "")
                    {
                        var port = configCfg.GetString("LLLoginHandlerPort", "");
                        if (port == "" || port == "0")
                            port = MainServer.Instance.Port.ToString();
                        GridLoginURI = MainServer.Instance.FullHostName + ":" + port + "/";
                    }
                }
                else if (!GridLoginURI.EndsWith ("/", StringComparison.Ordinal))
                    GridLoginURI += "/";
                _info["login"] = GridLoginURI;

                // welcome
                _info["welcome"] = GridWelcomeURI = GetConfig(m_config, "welcome");
                if (GridWelcomeURI == "" && webInterface != null)
                    _info["welcome"] = GridWelcomeURI = webInterface.LoginScreenURL;

                // registration
                _info["register"] = GridRegisterURI = GetConfig(m_config, "register");
                if (GridRegisterURI == "" && webInterface != null)
                    _info["register"] = GridRegisterURI = webInterface.RegistrationScreenURL;

                // grid details
                _info["gridname"] = GridName = GetConfig(m_config, "gridname");
                _info["gridnick"] = GridNick = GetConfig(m_config, "gridnick");

                _info["about"] = GridAboutURI = GetConfig(m_config, "about");

                _info["help"] = GridHelpURI = GetConfig(m_config, "help");
                if (GridHelpURI == "" && webInterface != null)
                    GridHelpURI = webInterface.HelpScreenURL;

                _info["password"] = GridForgotPasswordURI = GetConfig(m_config, "forgottenpassword");
                if (GridForgotPasswordURI == "" && webInterface != null)
                    GridForgotPasswordURI = webInterface.ForgotPasswordScreenURL;

                // mapping
                GridMapTileURI = GetConfig(m_config, "map");
                if (GridMapTileURI == "" && serverInfoService != null)
                    GridMapTileURI = serverInfoService.GetGridURI("MapService");

                // Agent
                AgentAppearanceURI = GetConfig(m_config, "AgentAppearanceURI");
                if (AgentAppearanceURI == "" && serverInfoService != null)
                    AgentAppearanceURI = serverInfoService.GetGridURI("SSAService");

                // profile
                GridWebProfileURI = GetConfig(m_config, "webprofile");
                if (GridWebProfileURI == "" && webInterface != null)
                    GridWebProfileURI = webInterface.WebProfileURL;

                // economy
                GridEconomyURI = GetConfig(m_config, "economy");
                if (GridEconomyURI == "")
                {
                    GridEconomyURI = MainServer.Instance.ServerURI + "/";           // assume default... 

                    if (moneyModule != null)
                    {
                        int port = moneyModule.ClientPort;
                        if (port == 0)
                            port = (int) MainServer.Instance.Port;

                        GridEconomyURI = MainServer.Instance.FullHostName + ":" + port + "/";
                    }
                }

                if (GridEconomyURI != "" && !GridEconomyURI.EndsWith("/"))
                    GridEconomyURI += "/";
                _info["economy"] = _info["helperuri"] = GridEconomyURI;


                // misc.. these must be set to be used
                GridSearchURI = GetConfig(m_config, "search");
                GridDestinationURI = GetConfig(m_config, "destination");
                GridMarketplaceURI = GetConfig(m_config, "marketplace");
                GridTutorialURI = GetConfig(m_config, "tutorial");
                GridSnapshotConfigURI = GetConfig(m_config, "snapshotconfig");
            }
            catch (Exception)
            {
                MainConsole.Instance.Warn(
                    "[GRID INFO]: Cannot get grid info from config source, using minimal defaults");
            }

            MainConsole.Instance.DebugFormat("[GRID INFO]: Grid info service initialized with {0} keys",
                                             _info.Count);
        }

        static string GetConfig (IConfigSource config, string p)
		{
			IConfig gridCfg = config.Configs ["GridInfo"];
			return gridCfg.GetString (p, "");
		}

        void IssueWarning()
        {
            MainConsole.Instance.Warn("[GRID INFO]: found no [GridInfo] section in your configuration files");
            MainConsole.Instance.Warn(
                "[GRID INFO]: trying to guess sensible defaults, you might want to provide better ones:");

            foreach (string k in _info.Keys)
            {
                MainConsole.Instance.WarnFormat("[GRID INFO]: {0}: {1}", k, _info[k]);
            }
        }

        public XmlRpcResponse XmlRpcGridInfoMethod(XmlRpcRequest request, IPEndPoint remoteClient)
        {
            XmlRpcResponse response = new XmlRpcResponse();
            Hashtable responseData = new Hashtable();

            MainConsole.Instance.Debug("[GRID INFO]: Request for grid info");
            UpdateGridInfo();

            foreach (string k in _info.Keys)
            {
                responseData[k] = _info[k];
            }
            response.Value = responseData;

            return response;
        }

        public byte[] RestGetGridInfoMethod(string path, Stream request, OSHttpRequest httpRequest,
                                            OSHttpResponse httpResponse)
        {
            StringBuilder sb = new StringBuilder();
            UpdateGridInfo();

            sb.Append("<gridinfo>\n");
            foreach (string k in _info.Keys)
            {
                sb.AppendFormat("<{0}>{1}</{0}>\n", k, _info[k]);
            }
            sb.Append("</gridinfo>\n");

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}