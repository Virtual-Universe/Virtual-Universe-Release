/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Simulation.Base;
using System.Reflection;

[assembly: AssemblyVersion("0.1.0")]
[assembly: AssemblyFileVersion("0.1.0")]

namespace VirtualUniverse.Server
{
    /// <summary>
    ///     Starting class for the Virtual-Universe Server
    /// </summary>
    public class Application
    {
        public static void Main(string[] args)
        {
            BaseApplication.BaseMain(args, "VirtualUniverse.WebServer.ini", new VirtualUniverseBase());
        }
    }
}