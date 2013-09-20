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

namespace VirtualUniverse.Servers.InventoryServer
{
    /// <summary>
    ///     Starting class for the VirtualUniverse Server
    /// </summary>
    public class Application
    {
        public static void Main(string[] args)
        {
            BaseApplication.BaseMain(args, "VirtualUniverse.InventoryServer.ini",
                                     new MinimalSimulationBase("VirtualUniverse.InventoryServer ",
                                                               new List<Type>
                                                                   {
                                                                       typeof (IInventoryData),
                                                                       typeof (IUserAccountData),
                                                                       typeof (IAssetDataPlugin),
                                                                       typeof (ISimpleCurrencyConnector),
                                                                       typeof (IAgentInfoConnector)
                                                                   },
                                                               new List<Type>
                                                                   {
                                                                       typeof (IInventoryService),
                                                                       typeof (ILibraryService),
                                                                       typeof (IUserAccountService),
                                                                       typeof (IAssetService),
                                                                       typeof (IMoneyModule),
                                                                       typeof (ISyncMessagePosterService),
                                                                       typeof (ISyncMessageRecievedService),
                                                                       typeof (IAgentInfoService),
                                                                       typeof (IExternalCapsHandler),
                                                                       typeof (IConfigurationService),
                                                                       typeof (IGridServerInfoService),
                                                                       typeof (IJ2KDecoder)
                                                                   }));
        }
    }
}