/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Framework;
using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Framework.Services;
using VirtualUniverse.Simulation.Base;
using System;
using System.Collections.Generic;
using System.Reflection;

[assembly: AssemblyVersion("0.1.0")]
[assembly: AssemblyFileVersion("0.1.0")]

namespace VirtualUniverse.Servers.AssetServer
{
    /// <summary>
    ///     Starting class for the VirtualUniverse Server
    /// </summary>
    public class Application
    {
        public static void Main(string[] args)
        {
            BaseApplication.BaseMain(args, "VirtualUniverse.GridServer.ini",
                                     new MinimalSimulationBase("VirtualUniverse.GridServer ",
                                                               new List<Type>
                                                                   {
                                                                       typeof (IRegionData),
                                                                       typeof (IAgentInfoConnector),
                                                                       typeof (IUserAccountData),
                                                                       typeof (IAssetDataPlugin)
                                                                   },
                                                               new List<Type>
                                                                   {
                                                                       typeof (IGridService),
                                                                       typeof (IAssetService),
                                                                       typeof (IAgentInfoService),
                                                                       typeof (IConfigurationService),
                                                                       typeof (ISyncMessagePosterService),
                                                                       typeof (ISyncMessageRecievedService),
                                                                       typeof (IExternalCapsHandler),
                                                                       typeof (IUserAccountService),
                                                                       typeof (IGridServerInfoService),
                                                                       typeof (IMapService),
                                                                       typeof (IJ2KDecoder)
                                                                   }));
        }
    }
}