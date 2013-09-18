/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Framework.ModuleLoader;
using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Framework.Services;
using Nini.Config;
using System.Collections.Generic;

namespace VirtualUniverse.CoreApplicationPlugins.ServicesLoader
{
    public class ServicesLoader : IApplicationPlugin
    {
        private ISimulationBase m_openSim;

        #region IApplicationPlugin Members

        public void PreStartup(ISimulationBase simBase)
        {
        }

        public void Initialize(ISimulationBase openSim)
        {
            m_openSim = openSim;
        }

        public void ReloadConfiguration(IConfigSource config)
        {
        }

        public void PostInitialise()
        {
        }

        public void Start()
        {
            IConfig handlerConfig = m_openSim.ConfigSource.Configs["ApplicationPlugins"];
            if (handlerConfig.GetString("ServicesLoader", "") != Name)
                return;

            List<IService> serviceConnectors = VirtualUniverseModuleLoader.PickupModules<IService>();
            foreach (IService connector in serviceConnectors)
            {
                try
                {
                    connector.Initialize(m_openSim.ConfigSource, m_openSim.ApplicationRegistry);
                }
                catch
                {
                }
            }
            foreach (IService connector in serviceConnectors)
            {
                try
                {
                    connector.Start(m_openSim.ConfigSource, m_openSim.ApplicationRegistry);
                }
                catch
                {
                }
            }
            foreach (IService connector in serviceConnectors)
            {
                try
                {
                    connector.FinishedStartup();
                }
                catch
                {
                }
            }
        }

        public void PostStart()
        {
        }

        public void Close()
        {
        }

        public string Name
        {
            get { return "ServicesLoader"; }
        }

        #endregion

        public void Dispose()
        {
        }
    }
}