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


using System;
using System.Collections.Generic;
using Nini.Config;
using Universe.Framework.ConsoleFramework;
using Universe.Framework.ModuleLoader;
using Universe.Framework.Physics;
using Universe.Framework.SceneInfo;

namespace Universe.Modules.Startup
{
    /// <summary>
    ///     Description of MyClass.
    /// </summary>
    public class PhysicsPluginManager
    {
        readonly Dictionary<string, IMeshingPlugin> _MeshPlugins = new Dictionary<string, IMeshingPlugin>();
        readonly Dictionary<string, IPhysicsPlugin> _PhysPlugins = new Dictionary<string, IPhysicsPlugin>();

        /// <summary>
        ///     Get a physics scene for the given physics engine and mesher.
        /// </summary>
        /// <param name="physEngineName"></param>
        /// <param name="meshEngineName"></param>
        /// <param name="config"></param>
        /// <param name="scene"></param>
        /// <returns></returns>
        public PhysicsScene GetPhysicsScene(string physEngineName, string meshEngineName, IConfigSource config,
                                            IScene scene)
        {
            if (String.IsNullOrEmpty(physEngineName))
            {
                return new NullPhysicsScene();
            }

            if (String.IsNullOrEmpty(meshEngineName))
            {
                return new NullPhysicsScene();
            }

            IMesher meshEngine = null;
            if (_MeshPlugins.ContainsKey(meshEngineName))
            {
                meshEngine = _MeshPlugins[meshEngineName].GetMesher(config, scene);
            }
            else
            {
                MainConsole.Instance.WarnFormat("[Physics]: Couldn't find meshing engine: {0}", meshEngineName);
                throw new ArgumentException(String.Format("couldn't find meshing engine: {0}", meshEngineName));
            }

            if (_PhysPlugins.ContainsKey(physEngineName))
            {
                MainConsole.Instance.Debug("[Physics]: Loading physics engine: " + physEngineName);
                PhysicsScene result = _PhysPlugins[physEngineName].GetScene();
                result.Initialize(meshEngine, scene);
                result.PostInitialize(config);
                return result;
            }
            else
            {
                MainConsole.Instance.WarnFormat("[Physics]: Couldn't find physics engine: {0}", physEngineName);
                throw new ArgumentException(String.Format("couldn't find physics engine: {0}", physEngineName));
            }
        }

        /// <summary>
        ///     Load all plugins in assemblies at the given path
        /// </summary>
        /// <param name="assembliesPath"></param>
        public void LoadPluginsFromAssemblies(string assembliesPath)
        {
            List<IPhysicsPlugin> physicsPlugins =
                UniverseModuleLoader.LoadModules<IPhysicsPlugin>(assembliesPath);
            List<IMeshingPlugin> meshingPlugins =
                UniverseModuleLoader.LoadModules<IMeshingPlugin>(assembliesPath);
            meshingPlugins.AddRange(UniverseModuleLoader.LoadModules<IMeshingPlugin>(""));

            foreach (IPhysicsPlugin plug in physicsPlugins)
            {
                try
                {
                    _PhysPlugins.Add(plug.GetName(), plug);
                }
                catch
                {
                }
            }
            foreach (IMeshingPlugin plug in meshingPlugins)
            {
                try
                {
                    _MeshPlugins.Add(plug.GetName(), plug);
                }
                catch
                {
                }
            }
        }
    }
}
