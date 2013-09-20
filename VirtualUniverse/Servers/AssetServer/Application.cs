/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using System.Collections.Generic;
using VirtualUniverse.Framework;
using VirtualUniverse.Framework.Services;
using VirtualUniverse.Simulation.Base;
using VirtualUniverse.Framework.Modules;
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
            BaseApplication.BaseMain(args, "VirtualUniverse.AssetServer.ini",
                                     new MinimalSimulationBase("VirtualUniverse.AssetServer ",
                                                               new List<Type> { typeof(IAssetDataPlugin) },
                                                               new List<Type> 
                                                               {
                                                                    typeof (IAssetService),
                                                                    typeof (ISyncMessagePosterService),
                                                                    typeof (ISyncMessageRecievedService),
                                                                    typeof (IExternalCapsHandler),
                                                                    typeof (IConfigurationService),
                                                                    typeof (IGridServerInfoService),
                                                                    typeof (IJ2KDecoder)
                                                               }));
        }
    }
}