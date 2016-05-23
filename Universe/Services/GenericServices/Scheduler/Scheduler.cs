/*
 * Copyright (c) Contributors, http://virtual-planets.org/, http://whitecore-sim.org/, http://aurora-sim.org, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual Universe Project nor the
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
 */

using Nini.Config;
using Universe.Framework.Modules;
using Universe.Framework.Services;
using Universe.Framework.Services.ClassHelpers.Other;
using Universe.Framework.Utilities;

namespace Universe.Services
{
    public class Scheduler : ConnectorBase, IScheduleService, IService
    {
        public UniverseEventManager EventManager = new UniverseEventManager();
        ISchedulerDataPlugin m_database;

        #region Implementation of IService

        /// <summary>
        ///     Set up and register the module
        /// </summary>
        /// <param name="config">Config file</param>
        /// <param name="registry">Place to register the modules into</param>
        public void Initialize(IConfigSource config, IRegistryCore registry)
        {
            registry.RegisterModuleInterface<IScheduleService>(this);

            Init(registry, "Scheduler");
        }

        /// <summary>
        ///     Load other IService modules now that this is set up
        /// </summary>
        /// <param name="config">Config file</param>
        /// <param name="registry">Place to register and retrieve module interfaces</param>
        public void Start(IConfigSource config, IRegistryCore registry)
        {
        }

        /// <summary>
        ///     All modules have started up and it is ready to run
        /// </summary>
        public void FinishedStartup()
        {
            if (IsLocalConnector)
            {
                m_database = Framework.Utilities.DataManager.RequestPlugin<ISchedulerDataPlugin>();
            }
        }

        #endregion

        #region Implementation of IScheduleService

        [CanBeReflected(ThreatLevel = ThreatLevel.High, RenamedMethod = "SchedulerSave")]
        public string Save(SchedulerItem I)
        {
            if (m_doRemoteCalls)
                return (string)DoRemote(I);
            return m_database.SchedulerSave(I);
        }

        [CanBeReflected(ThreatLevel = ThreatLevel.High)]
        public void RemoveID(string id)
        {
            if (m_doRemoteCalls)
            {
                DoRemotePost(id);
                return;
            }

            m_database.SchedulerRemoveID(id);
        }

        [CanBeReflected(ThreatLevel = ThreatLevel.High)]
        public void RemoveFireFunction(string identifier)
        {
            if (m_doRemoteCalls)
            {
                DoRemotePost(identifier);
                return;
            }

            m_database.SchedulerRemoveFunction(identifier);
        }

        [CanBeReflected(ThreatLevel = ThreatLevel.Low, RenamedMethod = "SchedulerExist")]
        public bool Exist(string scdID)
        {
            if (m_doRemoteCalls)
                return (bool)DoRemote(scdID);
            return m_database.SchedulerExist(scdID);
        }

        [CanBeReflected(ThreatLevel = ThreatLevel.Low, RenamedMethod = "SchedulerGet")]
        public SchedulerItem Get(string ID)
        {
            if (m_doRemoteCalls)
                return (SchedulerItem)DoRemote(ID);
            return m_database.Get(ID);
        }

        [CanBeReflected(ThreatLevel = ThreatLevel.Low, RenamedMethod = "SchedulerGet")]
        public SchedulerItem Get(string scheduleFor, string fireFunction)
        {
            if (m_doRemoteCalls)
                return (SchedulerItem)DoRemote(scheduleFor, fireFunction);
            return m_database.Get(scheduleFor, fireFunction);
        }

        [CanBeReflected(ThreatLevel = ThreatLevel.Low)]
        public SchedulerItem GetFunctionItem(string fireFunction)
        {
            if (m_doRemoteCalls)
                return (SchedulerItem)DoRemote(fireFunction);
            return m_database.GetFunctionItem(fireFunction);
        }

        #endregion
    }
}