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

//#define BlockUnsupportedVersions

using Aurora.Framework.Configuration;
using Aurora.Framework.ConsoleFramework;
using Aurora.Framework.Modules;
using Aurora.Framework.Utilities;
using Nini.Config;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Aurora.Simulator.Base
{
    /// <summary>
    ///     Starting class for the Server
    /// </summary>
    public class BaseApplication
    {

        /// <summary>
        ///     Save Crashes in the bin/crashes folder.  Configurable with m_crashDir
        /// </summary>
        public static bool SaveCrashDumps;

        /// <summary>
        ///     Loader of configuration files
        /// </summary>
        static readonly ConfigurationLoader m_configLoader = new ConfigurationLoader();

        /// <summary>
        ///     Directory to save crash reports to.  Relative to bin/
        /// </summary>
        public static string CrashDir = "Storage/Crashes/";

        static bool isHandlingException; // Make sure we don't go recursive on ourself

        //could move our main function into OpenSimMain and kill this class
        public static void BaseMain(string[] args, string defaultIniFile, ISimulatorBase simBase)
        {
            // First line, hook the appdomain to the crash reporter
            AppDomain.CurrentDomain.UnhandledException +=
                CurrentDomainUnhandledException;

            // Add the arguments supplied when running the application to the configuration
            ArgvConfigSource configSource = new ArgvConfigSource(args);

            if (!args.Contains("-skipconfig"))
                Configure(false);

            // Increase the number of IOCP threads available. Mono defaults to a tragically low number
            int workerThreads, iocpThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out iocpThreads);
            //MainConsole.Instance.InfoFormat("[VIRTUAL REALITY MAIN]: Runtime gave us {0} worker threads and {1} IOCP threads", workerThreads, iocpThreads);
            if (workerThreads < 500 || iocpThreads < 1000)
            {
                workerThreads = 500;
                iocpThreads = 1000;
                //MainConsole.Instance.Info("[VIRTUAL REALITY MAIN]: Bumping up to 500 worker threads and 1000 IOCP threads");
                ThreadPool.SetMaxThreads(workerThreads, iocpThreads);
            }

            BinMigratorService service = new BinMigratorService();
            service.MigrateBin();
            // Configure nIni aliases and localles
            Culture.SystemCultureInfo = CultureInfo.CurrentCulture;
            Culture.SetCurrentCulture();
            configSource.Alias.AddAlias("On", true);
            configSource.Alias.AddAlias("Off", false);
            configSource.Alias.AddAlias("True", true);
            configSource.Alias.AddAlias("False", false);

            //Command line switches
            configSource.AddSwitch("Startup", "inifile");
            configSource.AddSwitch("Startup", "inimaster");
            configSource.AddSwitch("Startup", "inigrid");
            configSource.AddSwitch("Startup", "inisim");
            configSource.AddSwitch("Startup", "inidirectory");
            configSource.AddSwitch("Startup", "oldoptions");
            configSource.AddSwitch("Startup", "inishowfileloading");
            configSource.AddSwitch("Startup", "mainIniDirectory");
            configSource.AddSwitch("Startup", "mainIniFileName");
            configSource.AddSwitch("Startup", "secondaryIniFileName");
            configSource.AddSwitch("Startup", "RegionDataFileName");
            configSource.AddSwitch("Console", "Console");
            configSource.AddSwitch("Console", "LogAppendName");
            configSource.AddSwitch("Console", "LogPath");
            configSource.AddSwitch("Network", "http_listener_port");

            IConfigSource m_configSource = Configuration(configSource, defaultIniFile);

            // Check if we're saving crashes
            SaveCrashDumps = m_configSource.Configs["Startup"].GetBoolean("save_crashes", SaveCrashDumps);

            // load Crash directory config
            CrashDir = m_configSource.Configs["Startup"].GetString("crash_dir", CrashDir);

            //Initialize the sim base now
            Startup(configSource, m_configSource, simBase.Copy(), args);
        }

        public static void Configure(bool requested)
        {
            string Aurora_ConfigDir = Constants.DEFAULT_CONFIG_DIR;
            bool isPlanetsxe = AppDomain.CurrentDomain.FriendlyName == "Planets.exe" ||
                               AppDomain.CurrentDomain.FriendlyName == "Host.exe";

             bool existingConfig = (
                File.Exists(Path.Combine(Aurora_ConfigDir,"World.ini")) ||
                File.Exists(Path.Combine(Aurora_ConfigDir,"Planets.ini")) ||
                File.Exists(Path.Combine(Aurora_ConfigDir,"Galaxy.ini"))
                );

            if ( requested || !existingConfig )
            {
                string resp = "no";
                if (!requested)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n*************Required Setup files not found.*************");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        "\n\n   This appears to be your first time running Virtual Reality Universe.\n"+
                        "If you have already configured your *ini.example files, please ignore this warning and press enter;\n" +
                        "Otherwise type 'yes' and Virtual Reality Universe will guide you through the setup process.\n\n"+
                        "Remember, these file names are Case Sensitive in Linux and Proper Cased.\n"+
                        "1. " + Aurora_ConfigDir + "Planets.ini\nand\n" +
                        "2. " + Aurora_ConfigDir + "Settings/Planets/Standalone/StandaloneCommon.ini \nor\n" +
                        "3. " + Aurora_ConfigDir + "Settings/Galaxy/GalaxyCommon.ini\n" +
                        "\nAlso, you will want to examine these files in great detail because only the basic system will " +
                        "load by default. Virtual Reality Universe can do a LOT more if you spend a little time going through these files.\n\n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This will overwrite any existing settings");
                    Console.ResetColor();
                    resp = ReadLine("Do you want to setup Virtual Reality Universe now", resp);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This will overwrite any existing settings");
                    Console.ResetColor();
                    resp = ReadLine("Do you want to setup Virtual Reality Universe now", resp);
                }
                if (resp == "yes")
                {
                    string dbSource = "localhost";
                    string dbPasswd = "VRU";
                    string dbSchema = "VirtualRealityUniverse";
                    string dbUser = "VirtualRealityUniverse";
                    string dbPort = "3306";
                    string gridIPAddress = Utilities.GetExternalIp();
                    bool isStandalone = true;
                    string dbType = "1";
                    string gridName = "Virtual Reality Universe Simulator";
                    string welcomeMessage = "<USERNAME> You will find an outlet for your creative genius and accomplish a great deal. Get your mind set...Confidence will lead you on!";
                    string allowAnonLogin = "true";
                    uint port = 80;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("====================================================================");
                    Console.WriteLine("========================= VIRTUAL REALITY UNIVERSE SETUP ======================");
                    Console.WriteLine("====================================================================");
                    Console.ResetColor();

                    if (isPlanetsxe)
                    {
                        Console.WriteLine("This installation is going to run in");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("[1] Standalone Mode \n[2] Grid Mode");
                        Console.ResetColor();
                        isStandalone = ReadLine("Choose 1 or 2", "1") == "1";


                        Console.WriteLine("Http Port for the server");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Default is 80");
                        Console.ResetColor();
                        port = uint.Parse(ReadLine("Choose the port", "80"));
                    }

                    if (isStandalone)
                    {
                        Console.WriteLine("Which database do you want to use?");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("[1] SQLite \n[2] MySQL");
                        Console.ResetColor();
                        dbType = ReadLine("Choose 1 or 2", dbType);
                        if (dbType == "2")
                        {
                            Console.WriteLine(
                                "Note: this setup does not automatically create a MySQL installation for you.\n" +
                                " This will setup Virtual Reality Universe but you must install MySQL as well");

                            dbSource = ReadLine("MySQL database IP", dbSource);
                            dbPort = ReadLine("MySQL database port (if not default)", dbPort);
                            dbSchema = ReadLine("MySQL database name for your region", dbSchema);
                            dbUser = ReadLine("MySQL database user account", dbUser);

                            Console.Write("MySQL database password for that account: ");
                            dbPasswd = Console.ReadLine();
                        }
                    }

                    if (isStandalone)
                    {
                        gridName = ReadLine("Name of your Virtual Reality Universe Simulator", gridName);

                        welcomeMessage = "Welcome to " + gridName + ", <USERNAME>!";
                        welcomeMessage = ReadLine("Welcome Message to show during login\n" +
                            "  (putting <USERNAME> into the welcome message will insert the user's name)", welcomeMessage);

                        allowAnonLogin = ReadLine("Create accounts automatically when users log in\n" +
                            "  (This means you don't have to create all accounts manually\n" +
                            "   using the console or web interface): ",
                            allowAnonLogin);
                    }

                    if (!isStandalone)
                        gridIPAddress =
                            ReadLine("The external domain name or IP address of the galaxy server you wish to connect to",
                                     gridIPAddress);

                    //Data.ini setup
                    if (isStandalone)
                    {
                        string folder = isPlanetsxe ? Aurora_ConfigDir + "Settings/Planets/" : Aurora_ConfigDir + "Settings/Galaxy/";
                        MakeSureExists(folder + "Data/Data.ini");
                        IniConfigSource data_ini = new IniConfigSource(folder + "Data/Data.ini",
                                                                       Nini.Ini.IniFileType.AuroraStyle);
                        IConfig conf = data_ini.AddConfig("DataFile");
                        if (dbType == "1")
                            conf.Set("Include-SQLite", folder + "Data/SQLite.ini");
                        else
                            conf.Set("Include-MySQL", folder + "Data/MySQL.ini");

                        if (isPlanetsxe)
                            conf.Set("Include-FileBased", "Settings/Planets/Data/FileBased.ini");

                        conf = data_ini.AddConfig("Connectors");
                        conf.Set("ValidateTables", true);

                        data_ini.Save();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your Data.ini has been successfully setup");
                        Console.ResetColor();

                        if (dbType == "2") //MySQL setup
                        {
                            MakeSureExists(folder + "Data/MySQL.ini");
                            IniConfigSource mysql_ini = new IniConfigSource(folder + "Data/MySQL.ini",
                                                                            Nini.Ini.IniFileType.AuroraStyle);
                            IniConfigSource mysql_ini_example = new IniConfigSource(folder + "Data/MySQL.ini.example",
                                                                                    Nini.Ini.IniFileType.AuroraStyle);
                            foreach (IConfig config in mysql_ini_example.Configs)
                            {
                                IConfig newConfig = mysql_ini.AddConfig(config.Name);
                                foreach (string key in config.GetKeys())
                                {
                                    if (key == "ConnectionString")
                                        newConfig.Set(key,
                                                      string.Format(
                                                "\"Data Source={0};Port={1};Database={2};User ID={3};Password={4};\"",
                                                dbSource, dbPort, dbSchema, dbUser, dbPasswd));
                                    else
                                        newConfig.Set(key, config.Get(key));
                                }
                            }
                            mysql_ini.Save();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Your MySQL.ini has been successfully setup");
                            Console.ResetColor();
                        }
                    }

                    if (isPlanetsxe)
                    {
						string folder = Aurora_ConfigDir;
						MakeSureExists(folder + "Planets.ini");

						IniConfigSource planets_ini = new IniConfigSource(folder 
							+ "Planets.ini", Nini.Ini.IniFileType.AuroraStyle);
						IniConfigSource planets_ini_example = new IniConfigSource(folder 
							+ "Planets.ini.example", Nini.Ini.IniFileType.AuroraStyle);

						foreach (IConfig config in planets_ini_example.Configs)
                        {
							IConfig newConfig = planets_ini.AddConfig(config.Name);
                            foreach (string key in config.GetKeys())
                            {
                                if (key == "http_listener_port")
                                    newConfig.Set(key, port);
                                else
                                    newConfig.Set(key, config.Get(key));
                            }
                        }

						planets_ini.Save();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your Planets.ini has been successfully setup");
                        Console.ResetColor();

						MakeSureExists(folder + "Settings/Planets/Main.ini");

						IniConfigSource main_ini = new IniConfigSource(folder + "Settings/Planets/Main.ini", 
							Nini.Ini.IniFileType.AuroraStyle);
						//IniConfigSource main_ini_example = new IniConfigSource(folder 
						//	+ "Settings/Region/Main.ini.example", Nini.Ini.IniFileType.AuroraStyle);

                        IConfig conf = main_ini.AddConfig("Architecture");
                        if (isStandalone)
                            conf.Set("Include-Standalone", "Settings/Planets/Standalone/StandaloneCommon.ini");
                        else
                            conf.Set("Include-Galaxy", "Settings/Planets/Galaxy/GalaxyCommon.ini");
                        conf.Set("Include-Includes", "Settings/Planets/Includes.ini");

                        main_ini.Save();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your Main.ini has been successfully setup");
                        Console.ResetColor();

                        if (isStandalone)
                        {
							MakeSureExists(folder + "Settings/Planets/Standalone/StandaloneCommon.ini");

							IniConfigSource standalone_ini = new IniConfigSource(folder 
								+ "Settings/Planets/Standalone/StandaloneCommon.ini", Nini.Ini.IniFileType.AuroraStyle);
							IniConfigSource standalone_ini_example = new IniConfigSource(folder 
								+ "Settings/Planets/Standalone/StandaloneCommon.ini.example", Nini.Ini.IniFileType.AuroraStyle);

                            foreach (IConfig config in standalone_ini_example.Configs)
                            {
                                IConfig newConfig = standalone_ini.AddConfig(config.Name);
                                if (newConfig.Name == "GalaxyInfo")
                                {
                                    newConfig.Set("GalaxyInfoInHandlerPort", 0);
                                    newConfig.Set("login", "http://" + gridIPAddress + ":80/");
                                    newConfig.Set("gridname", gridName);
                                    newConfig.Set("gridnick", gridName);
                                }
                                else
                                {
                                    foreach (string key in config.GetKeys())
                                    {
                                        if (key == "WelcomeMessage")
                                            newConfig.Set(key, welcomeMessage);
                                        else if (key == "AllowAnonymousLogin")
                                            newConfig.Set(key, allowAnonLogin);
                                        else
                                            newConfig.Set(key, config.Get(key));
                                    }
                                }
                            }

                            standalone_ini.Save();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Your StandaloneCommon.ini has been successfully setup");
                            Console.ResetColor();
                        }
                        else
                        {
                            MakeSureExists("Settings/Planets/Galaxy/GalaxyCommon.ini");
                            IniConfigSource galaxy_ini = new IniConfigSource("Settings/Planets/Galaxy/GalaxyCommon.ini",
                                                                           Nini.Ini.IniFileType.AuroraStyle);

                            conf = galaxy_ini.AddConfig("Includes");
                            conf.Set("Include-Galaxy", "Settings/Region/Galaxy/Galaxy.ini");
                            conf = galaxy_ini.AddConfig("Configuration");
                            conf.Set("GalaxyServerURI", "http://" + gridIPAddress + ":80/galaxy/");

                            galaxy_ini.Save();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Your Galaxy.ini has been successfully setup");
                            Console.ResetColor();
                        }
                    }
                    if (!isPlanetsxe)
                    {
                        MakeSureExists("Settings/Galaxy/Login.ini");
                        IniConfigSource login_ini = new IniConfigSource("Settings/Galaxy/Login.ini",
                                                                        Nini.Ini.IniFileType.AuroraStyle);
                        IniConfigSource login_ini_example =
                            new IniConfigSource("Settings/Galaxy/Login.ini.example",
                                                Nini.Ini.IniFileType.AuroraStyle);
                        foreach (IConfig config in login_ini_example.Configs)
                        {
                            IConfig newConfig = login_ini.AddConfig(config.Name);
                            foreach (string key in config.GetKeys())
                            {
                                if (key == "WelcomeMessage")
                                    newConfig.Set(key, welcomeMessage);
                                else if (key == "AllowAnonymousLogin")
                                    newConfig.Set(key, allowAnonLogin);
                                else
                                    newConfig.Set(key, config.Get(key));
                            }
                        }
                        login_ini.Save();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your Login.ini has been successfully configured");
                        Console.ResetColor();

                        MakeSureExists("Settings/Galaxy/GalaxyInfo.ini");
                        IniConfigSource galaxyinfo_ini =
                            new IniConfigSource("Settings/Galaxy/GalaxyInfo.ini",
                                                Nini.Ini.IniFileType.AuroraStyle);
                        IConfig conf = galaxyinfo_ini.AddConfig("GalaxyInfo");
                        conf.Set("GalaxyInfoInHandlerPort", 80);
                        conf.Set("login", "http://" + gridIPAddress + ":80");
                        conf.Set("gridname", gridName);
                        conf.Set("gridnick", gridName);

                        galaxyinfo_ini.Save();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your GalaxyInfo.ini has been successfully setup");
                        Console.ResetColor();
                    }

                    Console.WriteLine("\n====================================================================\n");
                    Console.ResetColor();
                    Console.WriteLine("Your galaxies name is ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(gridName);
                    Console.ResetColor();
                    if (isStandalone)
                    {
                        Console.WriteLine("\nYour loginuri is ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("http://" + gridIPAddress + (":80/"));
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("\nConnected Galaxy URL: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("http://" + gridIPAddress + ":80/");
                        Console.ResetColor();
                    }
                    Console.WriteLine("\n====================================================================\n");
                    Console.WriteLine(
                        "If you ever need to reconfigure Virtual Reality Universe, you can type \"run setup\" into the console to bring this setup back up.");
                }
            }
        }

        static void MakeSureExists(string file)
        {
            if (File.Exists(file))
                File.Delete(file);
            File.Create(file).Close();
        }

        static string ReadLine(string log, string defaultReturn)
        {
            Console.WriteLine(log + ": [" + defaultReturn + "]");
            string mode = Console.ReadLine();
            if (mode == string.Empty)
                mode = defaultReturn;
            if (mode != null)
                mode = mode.Trim();
            return mode;
        }

        public static void Startup(IConfigSource originalConfigSource, IConfigSource configSource,
                                   ISimulatorBase simBase, string[] cmdParameters)
        {
            //Get it ready to run
            simBase.Initialize(originalConfigSource, configSource, cmdParameters, m_configLoader);
            try
            {
                //Start it. This starts ALL modules and completes the startup of the application
                simBase.Startup();
                //Run the console now that we are done
                simBase.Run();
            }
            catch (Exception ex)
            {
                UnhandledException(false, ex);
                //Just clean it out as good as we can
                simBase.Shutdown(false);
            }
        }

        /// <summary>
        ///     Load the configuration for the Application
        /// </summary>
        /// <param name="configSource"></param>
        /// <param name="defaultIniFile"></param>
        /// <returns></returns>
        static IConfigSource Configuration(IConfigSource configSource, string defaultIniFile)
        {
            if (defaultIniFile != "")
                m_configLoader.defaultIniFile = defaultIniFile;
            return m_configLoader.LoadConfigSettings(configSource);
        }

        /// <summary>
        ///     Global exception handler -- all unhandled exceptions end up here :)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (isHandlingException)
                return;

            isHandlingException = true;
            Exception ex = (Exception) e.ExceptionObject;

            UnhandledException(e.IsTerminating, ex);

            isHandlingException = false;
        }

        static void UnhandledException(bool isTerminating, Exception ex)
        {
            string msg = String.Empty;
            msg += "\r\n";
            msg += "APPLICATION EXCEPTION DETECTED" + "\r\n";
            msg += "\r\n";

            msg += "Exception: " + ex + "\r\n";
            if (ex.InnerException != null)
            {
                msg += "InnerException: " + ex.InnerException + "\r\n";
            }

            msg += "\r\n";
            msg += "Application is terminating: " + isTerminating.ToString(CultureInfo.InvariantCulture) + "\r\n";

            MainConsole.Instance.ErrorFormat("[APPLICATION]: {0}", msg);

            HandleException(msg, ex);
        }

        /// <summary>
        ///     Deal with sending the error to the error reporting service and saving the dump to the harddrive if needed
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void HandleException(string msg, Exception ex)
        {
            if (SaveCrashDumps && Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // Log exception to disk
                try
                {
                    if (!Directory.Exists(CrashDir))
                        Directory.CreateDirectory(CrashDir);

                    string log = Path.Combine(CrashDir, Util.GetUniqueFilename("crashDump" +
                                                                                 DateTime.Now.Day + DateTime.Now.Month +
                                                                                 DateTime.Now.Year + ".mdmp"));
                    using (FileStream fs = new FileStream(log, FileMode.Create, FileAccess.ReadWrite, FileShare.Write))
                    {
                        MiniDump.Write(fs.SafeFileHandle,
                                       MiniDump.Option.WithThreadInfo | MiniDump.Option.WithProcessThreadData |
                                       MiniDump.Option.WithUnloadedModules | MiniDump.Option.WithHandleData |
                                       MiniDump.Option.WithDataSegs | MiniDump.Option.WithCodeSegs,
                                       MiniDump.ExceptionInfo.Present);
                    }
                }
                catch (Exception e2)
                {
                    MainConsole.Instance.ErrorFormat("[CRASH LOGGER CRASHED]: {0}", e2);
                }
            }
        }
    }

    public static class MiniDump
    {
        // Taken almost verbatim from http://blog.kalmbach-software.de/2008/12/13/writing-minidumps-in-c/ 

        #region ExceptionInfo enum

        public enum ExceptionInfo
        {
            None,
            Present
        }

        #endregion

        #region Option enum

        [Flags]
        public enum Option : uint
        {
            // From dbghelp.h: 
            Normal = 0x00000000,
            WithDataSegs = 0x00000001,
            WithFullMemory = 0x00000002,
            WithHandleData = 0x00000004,
            FilterMemory = 0x00000008,
            ScanMemory = 0x00000010,
            WithUnloadedModules = 0x00000020,
            WithIndirectlyReferencedMemory = 0x00000040,
            FilterModulePaths = 0x00000080,
            WithProcessThreadData = 0x00000100,
            WithPrivateReadWriteMemory = 0x00000200,
            WithoutOptionalData = 0x00000400,
            WithFullMemoryInfo = 0x00000800,
            WithThreadInfo = 0x00001000,
            WithCodeSegs = 0x00002000,
            WithoutAuxiliaryState = 0x00004000,
            WithFullAuxiliaryState = 0x00008000,
            WithPrivateWriteCopyMemory = 0x00010000,
            IgnoreInaccessibleMemory = 0x00020000,
            ValidTypeFlags = 0x0003ffff,
        };

        #endregion

        //typedef struct _MINIDUMP_EXCEPTION_INFORMATION { 
        //    DWORD ThreadId; 
        //    PEXCEPTION_POINTERS ExceptionPointers; 
        //    BOOL ClientPointers; 
        //} MINIDUMP_EXCEPTION_INFORMATION, *PMINIDUMP_EXCEPTION_INFORMATION; 

        //BOOL 
        //WINAPI 
        //MiniDumpWriteDump( 
        //    __in HANDLE hProcess, 
        //    __in DWORD ProcessId, 
        //    __in HANDLE hFile, 
        //    __in MINIDUMP_TYPE DumpType, 
        //    __in_opt PMINIDUMP_EXCEPTION_INFORMATION ExceptionParam, 
        //    __in_opt PMINIDUMP_USER_STREAM_INFORMATION UserStreamParam, 
        //    __in_opt PMINIDUMP_CALLBACK_INFORMATION CallbackParam 
        //    ); 

        // Overload requiring MiniDumpExceptionInformation 
        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType,
                                                     ref MiniDumpExceptionInformation expParam, IntPtr userStreamParam,
                                                     IntPtr callbackParam);

        // Overload supporting MiniDumpExceptionInformation == NULL 
        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType,
                                                     IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        static extern uint GetCurrentThreadId();

        public static bool Write(SafeHandle fileHandle, Option options, ExceptionInfo exceptionInfo)
        {
            Process currentProcess = Process.GetCurrentProcess();
            IntPtr currentProcessHandle = currentProcess.Handle;
            uint currentProcessId = (uint) currentProcess.Id;
            MiniDumpExceptionInformation exp;
            exp.ThreadId = GetCurrentThreadId();
            exp.ClientPointers = false;
            exp.ExceptionPointers = IntPtr.Zero;
            if (exceptionInfo == ExceptionInfo.Present)
            {
                exp.ExceptionPointers = Marshal.GetExceptionPointers();
            }
			bool bRet;
            if (exp.ExceptionPointers == IntPtr.Zero)
            {
                bRet = MiniDumpWriteDump(currentProcessHandle, currentProcessId, fileHandle, (uint) options, IntPtr.Zero,
                                         IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                bRet = MiniDumpWriteDump(currentProcessHandle, currentProcessId, fileHandle, (uint) options, ref exp,
                                         IntPtr.Zero, IntPtr.Zero);
            }
            return bRet;
        }

        public static bool Write(SafeHandle fileHandle, Option dumpType)
        {
            return Write(fileHandle, dumpType, ExceptionInfo.None);
        }

        #region Nested type: MiniDumpExceptionInformation

        [StructLayout(LayoutKind.Sequential, Pack = 4)] // Pack=4 is important! So it works also for x64! 
        public struct MiniDumpExceptionInformation
        {
            public uint ThreadId;
            public IntPtr ExceptionPointers;
            [MarshalAs(UnmanagedType.Bool)] public bool ClientPointers;
        }

        #endregion
    }
}