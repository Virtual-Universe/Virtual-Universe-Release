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

using Aurora.Framework.Configuration;
using Aurora.Framework.ConsoleFramework;
using Aurora.Framework.ModuleLoader;
using Aurora.Framework.Modules;
using Aurora.Framework.SceneInfo;
using Aurora.Framework.Servers;
using Aurora.Framework.Servers.HttpServer;
using Aurora.Framework.Servers.HttpServer.Interfaces;
using Aurora.Framework.Services;
using Aurora.Framework.Services.ClassHelpers.Other;
using Aurora.Framework.Utilities;
using Aurora.Services.DataService;
using Nini.Config;
using OpenMetaverse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace Aurora.Simulator.Base
{
    public class MiniSimulator : ISimulatorBase
    {
        protected string StartupCommandsFile;
        protected string ShutdownCommandsFile;
        protected string TimerScriptFileName = "disabled";
        protected int TimerScriptTime = 20;
        protected IHttpServer BaseHTTPServer;
        protected Timer TimerScriptTimer;
        protected ConfigurationLoader ConfigurationLoader;

        /// <value>
        ///     The config information passed into the Virtual Server.
        /// </value>
        protected IConfigSource Config;

        protected IConfigSource OrginalConfig;

        public IConfigSource ConfigSource
        {
			get { return Config; }
			set { Config = value; }
        }

        /// <summary>
        ///     Server version information.  Usually VersionInfo + information about git commit, operating system, etc.
        /// </summary>
        protected string m_version;

        public string Version
        {
            get { return m_version; }
        }

        protected IRegistryCore m_applicationRegistry = new RegistryCore();

        public IRegistryCore ApplicationRegistry
        {
            get { return m_applicationRegistry; }
        }

        protected AuroraEventManager m_eventManager = new AuroraEventManager();

        public AuroraEventManager EventManager
        {
            get { return m_eventManager; }
        }

        /// <summary>
        ///     Time at which this server was started
        /// </summary>
        protected DateTime m_StartupTime;

        public DateTime StartupTime
        {
            get { return m_StartupTime; }
        }

        public IHttpServer HttpServer
        {
			get { return BaseHTTPServer; }
        }

        protected Dictionary<uint, IHttpServer> Servers =
            new Dictionary<uint, IHttpServer>();

        protected uint Port;

        public uint DefaultPort
        {
            get { return Port; }
        }

        protected string[] m_commandLineParameters;

        public string[] CommandLineParameters
        {
            get { return m_commandLineParameters; }
        }

        protected string PidFile = String.Empty;

        protected string ConsolePrompt = "";
        protected List<Type> DataPlugins;
        protected List<Type> ServicePlugins;

        public MiniSimulator(string consolePrompt, List<Type> dataPlugins, List<Type> servicePlugins)
        {
			ConsolePrompt = consolePrompt;
			DataPlugins = dataPlugins;
			ServicePlugins = servicePlugins;
        }

        /// <summary>
        ///     Do the initial setup for the application
        /// </summary>
        /// <param name="originalConfig"></param>
        /// <param name="configSource"></param>
        /// <param name="cmdParams"></param>
        /// <param name="configLoader"></param>
        public virtual void Initialize(IConfigSource originalConfig, IConfigSource configSource, string[] cmdParams,
                                       ConfigurationLoader configLoader)
        {
            m_commandLineParameters = cmdParams;
            m_StartupTime = DateTime.Now;
            m_version = VersionInfo.Version + " (" + Util.GetRuntimeInformation() + ")";
			OrginalConfig = originalConfig;
			Config = configSource;
			ConfigurationLoader = configLoader;

            // This thread will go on to become the console listening thread
            if (System.Threading.Thread.CurrentThread.Name != "ConsoleThread")
                System.Threading.Thread.CurrentThread.Name = "ConsoleThread";

            //Register the interface
            ApplicationRegistry.RegisterModuleInterface<ISimulatorBase>(this);

            Configuration(configSource);

            InitializeModules();

            RegisterConsoleCommands();
        }

        /// <summary>
        ///     Read the configuration
        /// </summary>
        /// <param name="configSource"></param>
        public virtual void Configuration(IConfigSource configSource)
        {
			IConfig startupConfig = Config.Configs["Startup"];

            int stpMaxThreads = 15;

            if (startupConfig != null)
            {
				StartupCommandsFile = startupConfig.GetString("startup_console_commands_file", "startup_commands.txt");
				ShutdownCommandsFile = startupConfig.GetString("shutdown_console_commands_file",
                                                                 "shutdown_commands.txt");

				TimerScriptFileName = startupConfig.GetString("timer_Script", "disabled");
				TimerScriptTime = startupConfig.GetInt("timer_time", TimerScriptTime);

                string pidFile = startupConfig.GetString("PIDFile", String.Empty);
                if (pidFile != String.Empty)
                    CreatePIDFile(pidFile);
            }

			IConfig SystemConfig = Config.Configs["System"];
            if (SystemConfig != null)
            {
                string asyncCallMethodStr = SystemConfig.GetString("AsyncCallMethod", String.Empty);
                FireAndForgetMethod asyncCallMethod;
                if (!String.IsNullOrEmpty(asyncCallMethodStr) &&
                    Utils.EnumTryParse(asyncCallMethodStr, out asyncCallMethod))
                    Util.FireAndForgetMethod = asyncCallMethod;

                stpMaxThreads = SystemConfig.GetInt("MaxPoolThreads", 15);
            }

            if (Util.FireAndForgetMethod == FireAndForgetMethod.SmartThreadPool)
                Util.InitThreadPool(stpMaxThreads);

            //Set up console forcefully
            Aurora.Services.BaseService consoleService = new Aurora.Services.BaseService();
            consoleService.PreStartup(this);

            //Fix the default prompt
            if (MainConsole.Instance != null)
            {
				MainConsole.Instance.DefaultPrompt = ConsolePrompt;
                MainConsole.Instance.Info(string.Format("[MINI SPACE SERVER]: STARTING MIN SPACE SERVER ({0})...",
                                                        (IntPtr.Size == 4 ? "x86" : "x64")));
                MainConsole.Instance.Info("[MINI SERVER]: Version: " + Version + "\n");
            }
        }

        /// <summary>
        ///     Performs initialisation of the application, such as loading the HTTP server and modules
        /// </summary>
        public virtual void Startup()
        {
            MainConsole.Instance.Info("[MINI SPACE SERVER]: Startup completed in " +
                                      (DateTime.Now - StartupTime).TotalSeconds);
        }

        public virtual ISimulatorBase Copy()
        {
			return new MiniSimulator(ConsolePrompt, DataPlugins, ServicePlugins);
        }

        /// <summary>
        ///     Run the console now that we are all done with startup
        /// </summary>
        public virtual void Run()
        {
            //Start the prompt
            if (MainConsole.Instance != null)
                MainConsole.Instance.ReadConsole();
        }

        /// <summary>
        ///     Get an HTTPServer on the given port. It will create one if one does not exist
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public IHttpServer GetHttpServer(uint port)
        {
            if ((port == Port || port == 0) && HttpServer != null)
                return HttpServer;

			bool useHTTPS = Config.Configs["Network"].GetBoolean("use_https", false);
            IHttpServer server;
            if (Servers.TryGetValue(port, out server) && server.Secure == useHTTPS)
                return server;

			uint threadCount = Config.Configs["Network"].GetUInt("HttpThreadCount", 5);

            // find out where we live
            string hostName;

            // been here before?
            if (Utilities.HostName == "")
            {
				hostName = Config.Configs ["Network"].GetString ("HostName", "0.0.0.0");

                if ((hostName == "") || (hostName == "0.0.0.0"))
                {
                    MainConsole.Instance.Info ("[Network]: Retrieving the external IP address");
                    hostName = "http" + (useHTTPS ? "s" : "") + "://" + Utilities.GetExternalIp ();
                }
            
                //Clean it up a bit
                if (hostName.StartsWith ("http://", StringComparison.CurrentCulture) || hostName.StartsWith ("https://", StringComparison.CurrentCulture))
                    hostName = hostName.Replace ("https://", "").Replace ("http://", "");
				if (hostName.EndsWith ("/", StringComparison.CurrentCulture))
					hostName = hostName.Remove (hostName.Length - 1, 1);
                
                // save this for posterity in case it is needed
                MainConsole.Instance.Info ("[Network]: Network IP address has been set to " + hostName);
                Utilities.HostName = hostName;
            } else
                hostName = Utilities.HostName;

            server = new BaseHttpServer(port, hostName, useHTTPS, threadCount);

            try
            {
                server.Start();
            }
            catch (Exception)
            {
                //Remove the server from the list
                Servers.Remove(port);
                //Then pass the exception upwards
                throw;
            }
            if (Servers.Count == 0)
                MainServer.Instance = server;
            return (Servers[port] = server);
        }

        public virtual void InitializeModules()
        {
            LocalDataService lds = new LocalDataService();
			lds.Initialise(ConfigSource, ApplicationRegistry, DataPlugins);

            List<dynamic> modules = new List<dynamic>();
			foreach (Type t in ServicePlugins)
            {
                var mods = ModuleLoader.PickupModules(t);
                modules.AddRange(mods);
            }

            foreach (dynamic service in modules)
            {
                if (!(service is IService)) continue;
                ((IService)service).Initialize(ConfigSource, ApplicationRegistry);
            }
            foreach (dynamic service in modules)
            {
                if (!(service is IService)) continue;
                ((IService) service).Start(ConfigSource, ApplicationRegistry);
            }
            foreach (dynamic service in modules)
            {
                if (!(service is IService)) continue;
                ((IService)service).FinishedStartup();
            }
        }

        /// <summary>
        ///     Close all the Application Plugins
        /// </summary>
        public virtual void CloseModules()
        {
        }

        /// <summary>
        ///     Run the commands given now that startup is complete
        /// </summary>
        public void RunStartupCommands()
        {
            //Draw the file on the console
            PrintFileToConsole("startuplogo.txt");
            //Run Startup Commands
			if (!String.IsNullOrEmpty(StartupCommandsFile))
				RunCommandScript(StartupCommandsFile);

            // Start timer script (run a script every xx seconds)
			if (TimerScriptFileName != "disabled")
            {
				Timer newtimername = new Timer {Enabled = true, Interval = TimerScriptTime*60*1000};
                newtimername.Elapsed += RunAutoTimerScript;
            }
        }

        /// <summary>
		///     Opens a file and uses it as input to the console command parser.
		/// </summary>
		/// <param name="fileName">name of file to use as input to the console</param>
		static void PrintFileToConsole (string fileName)
		{
			if (File.Exists (fileName)) {
				StreamReader readFile = File.OpenText (fileName);
				string currentLine;
				while ((currentLine = readFile.ReadLine ()) != null) {
					MainConsole.Instance.Info ("[!]" + currentLine);
				}
			}
		}

        /// <summary>
        ///     Timer to run a specific text file as console commands.
        ///     Configured in in the main .ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RunAutoTimerScript(object sender, EventArgs e)
        {
			RunCommandScript(TimerScriptFileName);
        }

        #region Console Commands

        /// <summary>
        ///     Register standard set of region console commands
        /// </summary>
        public virtual void RegisterConsoleCommands()
        {
            if (MainConsole.Instance == null)
                return;
            MainConsole.Instance.Commands.AddCommand("quit", "quit", "Quit the application", HandleQuit, false, true);

            MainConsole.Instance.Commands.AddCommand("shutdown", "shutdown", "Quit the application", HandleQuit, false, true);

            MainConsole.Instance.Commands.AddCommand("show info", "show info",
                                                     "Show server information (e.g. startup path)", HandleShowInfo, false, true);
            MainConsole.Instance.Commands.AddCommand("show version", "show version", "Show server version",
                                                     HandleShowVersion, false, true);

            MainConsole.Instance.Commands.AddCommand("reload setup", "reload setup", "Reloads Virtual Reality Universe Configurations",
                                                     HandleConfigRefresh, false, true);

            MainConsole.Instance.Commands.AddCommand("set timer script interval", "set timer script interval",
                                                     "Set the interval for the timer script (in minutes).",
                                                     HandleTimerScriptTime, false, true);

            MainConsole.Instance.Commands.AddCommand("force GC", "force GC", "Forces garbage collection.", HandleForceGC, false, true);
            MainConsole.Instance.Commands.AddCommand("run setup", "run setup", "Sets up Virtual Reality Universe.",
                                                     RunConfig, false, true);
        }

        void HandleQuit(IScene scene, string[] args)
        {
            Shutdown(true);
        }

        /// <summary>
        ///     Run an optional startup list of commands
        /// </summary>
        /// <param name="fileName"></param>
        public virtual void RunCommandScript(string fileName)
        {
            if (File.Exists(fileName))
            {
                MainConsole.Instance.Info("[COMMANDFILE]: Running " + fileName);
                List<string> commands = new List<string>();
                using (StreamReader readFile = File.OpenText(fileName))
                {
                    string currentCommand;
                    while ((currentCommand = readFile.ReadLine()) != null)
                    {
                        if ( (currentCommand != String.Empty) &&
                            (!currentCommand.StartsWith (";", StringComparison.CurrentCulture)) )
                        {
                            commands.Add(currentCommand);
                        }
                    }
                }
                foreach (string currentCommand in commands)
                {
                    MainConsole.Instance.Info("[COMMANDFILE]: Running '" + currentCommand + "'");
                    MainConsole.Instance.RunCommand(currentCommand);
                }
            }
        }

        public virtual void HandleForceGC(IScene scene, string[] cmd)
        {
            GC.Collect();
            MainConsole.Instance.Warn("Garbage collection finished");
        }

        public virtual void RunConfig(IScene scene, string[] cmd)
        {
            BaseApplication.Configure(true);
        }

        public virtual void HandleTimerScriptTime(IScene scene, string[] cmd)
        {
            if (cmd.Length != 5)
            {
                MainConsole.Instance.Warn("[CONSOLE]: Timer Interval command did not have enough parameters.");
                return;
            }
            MainConsole.Instance.Warn("[CONSOLE]: Set Timer Interval to " + cmd[4]);
			TimerScriptTime = int.Parse(cmd[4]);
			TimerScriptTimer.Enabled = false;
			TimerScriptTimer.Interval = TimerScriptTime*60*1000;
			TimerScriptTimer.Enabled = true;
        }

        public virtual void HandleConfigRefresh(IScene scene, string[] cmd)
        {
            //Rebuild the configuration
			Config = ConfigurationLoader.LoadConfigSettings(OrginalConfig);

            string hostName =
				Config.Configs["Network"].GetString("HostName", "http://127.0.0.1");
            //Clean it up a bit
            // these are doing nothing??
            hostName.Replace("http://", "");
            hostName.Replace("https://", "");
			if (hostName.EndsWith ("/", StringComparison.CurrentCulture))
				hostName = hostName.Remove (hostName.Length - 1, 1);
            foreach (IHttpServer server in Servers.Values)
            {
                server.HostName = hostName;
            }
            MainConsole.Instance.Info("Finished reloading configuration.");
        }

        public virtual void HandleShowInfo(IScene scene, string[] cmd)
        {
            MainConsole.Instance.Info("Version: " + m_version);
            MainConsole.Instance.Info("Startup directory: " + Environment.CurrentDirectory);
        }

        public virtual void HandleShowVersion(IScene scene, string[] cmd)
        {
            MainConsole.Instance.Info(
                String.Format(
                    "Version: {0}", m_version));
        }

        #endregion

        /// <summary>
        ///     Should be overridden and referenced by descendents if they need to perform extra shutdown processing
        ///     Performs any last-minute sanity checking and shuts down the region server
        /// </summary>
        public virtual void Shutdown(bool close)
        {
            try
            {
                try
                {
                    RemovePIDFile();
					if (ShutdownCommandsFile != String.Empty)
                    {
						RunCommandScript(ShutdownCommandsFile);
                    }
                }
                catch
                {
                    //It doesn't matter, just shut down
                }
                try
                {
                    //Close out all the modules
                    CloseModules();
                }
                catch
                {
                    //Just shut down already
                }
                try
                {
                    //Close the thread pool
                    Util.CloseThreadPool();
                }
                catch
                {
                    //Just shut down already
                }
                try
                {
                    //Stop the HTTP server(s)
                    foreach (IHttpServer server in Servers.Values)
                    {
                        server.Stop();
                    }
                }
                catch
                {
                    //Again, just shut down
                }

                if (close)
                    MainConsole.Instance.Info("[SHUTDOWN]: Terminating");

                MainConsole.Instance.Info("[SHUTDOWN]: Shutdown processing on main thread complete. " +
                                          (close ? " Exiting..." : ""));

                if (close)
                    Environment.Exit(0);
            }
            catch
            {
            }
        }

        /// <summary>
        ///     Write the PID file to the hard drive
        /// </summary>
        /// <param name="path"></param>
        protected void CreatePIDFile(string path)
        {
			System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
            try
            {
                string pidstring = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
                FileStream fs = File.Create(path);
                Byte[] buf = enc.GetBytes(pidstring);
                fs.Write(buf, 0, buf.Length);
                fs.Close();
				PidFile = path;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///     Delete the PID file now that we are done running
        /// </summary>
        protected void RemovePIDFile()
        {
			if (PidFile != String.Empty)
            {
                try
                {
					File.Delete(PidFile);
					PidFile = String.Empty;
                }
                catch (Exception)
                {
                }
            }
        }
    }
}