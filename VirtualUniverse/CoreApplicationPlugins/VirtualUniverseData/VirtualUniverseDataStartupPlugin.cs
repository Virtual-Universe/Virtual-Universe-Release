/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Framework.Services;
using VirtualUniverse.Services.DataService;
using Nini.Config;

namespace VirtualUniverse.CoreApplicationPlugins.VirtualUniverseData
{
    public class VirtualUniverseDataStartupPlugin : IApplicationPlugin
    {
        #region IApplicationPlugin Members

        public void PreStartup(ISimulationBase simBase)
        {
        }

        public void Initialize(ISimulationBase openSim)
        {
            LocalDataService service = new LocalDataService();
            service.Initialise(openSim.ConfigSource, openSim.ApplicationRegistry);
        }

        public void PostInitialise()
        {
        }

        public void Start()
        {
        }

        public void PostStart()
        {
        }

        public void Close()
        {
        }

        public void ReloadConfiguration(IConfigSource m_config)
        {
        }

        public string Name
        {
            get { return GetType().Name; }
        }

        #endregion

        public void Dispose()
        {
        }
    }
}