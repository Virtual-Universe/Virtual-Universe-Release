/*
 * Copyright (c) Contributors, http://virtual-planets.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 * For an explanation of the license of each contributor and the content it 
 * covers please see the Licenses directory.
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
using Universe.Framework.SceneInfo;

namespace Universe.Framework.Modules
{
	public interface ISharedRegionStartupModule
	{
		/// <summary>
		///     Initialize and load the configuration of the module
		///     This is used by IServices, DO NOT USE ANYTHING THAT REQUIRES IService here!
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="source"></param>
		/// <param name="simBase"></param>
		void Initialize (IScene scene, IConfigSource source, ISimulationBase simBase);

		/// <summary>
		///     PostInitialize the module
		///     This is used by IServices, DO NOT USE ANYTHING THAT REQUIRES IService here!
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="source"></param>
		/// <param name="simBase"></param>
		void PostInitialize (IScene scene, IConfigSource source, ISimulationBase simBase);

		/// <summary>
		///     Do the functions of the module and set up any necessary functions
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="source"></param>
		/// <param name="simBase"></param>
		void FinishStartup (IScene scene, IConfigSource source, ISimulationBase simBase);

		/// <summary>
		///     Do the functions of the module and set up any necessary functions
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="source"></param>
		/// <param name="simBase"></param>
		void PostFinishStartup (IScene scene, IConfigSource source, ISimulationBase simBase);

		/// <summary>
		///     Close the module and remove all references to it
		/// </summary>
		/// <param name="scene"></param>
		void Close (IScene scene);

		/// <summary>
		///     Close the module and remove all references to it
		/// </summary>
		/// <param name="scene"></param>
		void DeleteRegion (IScene scene);

		/// <summary>
		///     Fired once when the entire instance is fully started up
		/// </summary>
		void StartupComplete ();
	}
}