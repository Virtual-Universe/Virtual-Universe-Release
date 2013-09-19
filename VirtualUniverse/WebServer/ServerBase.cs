/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Framework;
using VirtualUniverse.Framework.ConsoleFramework;
using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Simulation.Base;
using System;

namespace VirtualUniverse.Server
{
    public class VirtualUniverseBase : SimulationBase
    {
        /// <summary>
        ///     Performs initialisation of the scene, such as loading configuration from disk.
        /// </summary>
        public override void Startup()
        {
            base.Startup();

            //Fix the default prompt
            if (MainConsole.Instance != null)
            {
                MainConsole.Instance.DefaultPrompt = "VirtualUniverse.WebServer ";
                MainConsole.Instance.Info("[VIRTUALUNIVERSESTARTUP]: Startup completed in " +
                                          (DateTime.Now - this.StartupTime).TotalSeconds);
            }
        }

        public override ISimulationBase Copy()
        {
            return new VirtualUniverseBase();
        }
    }
}