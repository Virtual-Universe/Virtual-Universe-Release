/***************************************************************************
 *	                VIRTUAL REALITY PUBLIC SOURCE LICENSE
 * 
 * Date				: Sun January 1, 2006
 * Copyright		: (c) 2006-2014 by Virtual Reality Development Team. 
 *                    All Rights Reserved.
 * Website			: http://www.syndarveruleiki.is
 *
 * Product Name		: Virtual Reality
 * License Text     : packages/docs/VRLICENSE.txt
 * 
 * Planetary Info   : Information about the Planetary code
 * 
 * Copyright        : (c) 2014-2024 by Second Galaxy Development Team
 *                    All Rights Reserved.
 * 
 * Website          : http://www.secondgalaxy.com
 * 
 * Product Name     : Virtual Reality
 * License Text     : packages/docs/SGLICENSE.txt
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the WhiteCore-Sim Project nor the
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
***************************************************************************/

using System;
using System.Collections;

using SmartInspect.Util;
using SmartInspect.Repository;

namespace SmartInspect.Plugin
{
	/// <summary>
	/// Map of repository plugins.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class is a name keyed map of the plugins that are
	/// attached to a repository.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public sealed class PluginMap
	{
		#region Public Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="repository">The repository that the plugins should be attached to.</param>
		/// <remarks>
		/// <para>
		/// Initialize a new instance of the <see cref="PluginMap" /> class with a 
		/// repository that the plugins should be attached to.
		/// </para>
		/// </remarks>
		public PluginMap(ILoggerRepository repository)
		{
			m_repository = repository;
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets a <see cref="IPlugin" /> by name.
		/// </summary>
		/// <param name="name">The name of the <see cref="IPlugin" /> to lookup.</param>
		/// <returns>
		/// The <see cref="IPlugin" /> from the map with the name specified, or 
		/// <c>null</c> if no plugin is found.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Lookup a plugin by name. If the plugin is not found <c>null</c>
		/// will be returned.
		/// </para>
		/// </remarks>
		public IPlugin this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}

				lock(this)
				{
					return (IPlugin)m_mapName2Plugin[name];
				}
			}
		}

		/// <summary>
		/// Gets all possible plugins as a list of <see cref="IPlugin" /> objects.
		/// </summary>
		/// <value>All possible plugins as a list of <see cref="IPlugin" /> objects.</value>
		/// <remarks>
		/// <para>
		/// Get a collection of all the plugins defined in this map.
		/// </para>
		/// </remarks>
		public PluginCollection AllPlugins
		{
			get
			{
				lock(this)
				{
					return new PluginCollection(m_mapName2Plugin.Values);
				}
			}
		}
		
		#endregion Public Instance Properties

		#region Public Instance Methods

		/// <summary>
		/// Adds a <see cref="IPlugin" /> to the map.
		/// </summary>
		/// <param name="plugin">The <see cref="IPlugin" /> to add to the map.</param>
		/// <remarks>
		/// <para>
		/// The <see cref="IPlugin" /> will be attached to the repository when added.
		/// </para>
		/// <para>
		/// If there already exists a plugin with the same name 
		/// attached to the repository then the old plugin will
		/// be <see cref="IPlugin.Shutdown"/> and replaced with
		/// the new plugin.
		/// </para>
		/// </remarks>
		public void Add(IPlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}

			IPlugin curPlugin = null;

			lock(this)
			{
				// Get the current plugin if it exists
				curPlugin = m_mapName2Plugin[plugin.Name] as IPlugin;

				// Store new plugin
				m_mapName2Plugin[plugin.Name] = plugin;
			}

			// Shutdown existing plugin with same name
			if (curPlugin != null)
			{
				curPlugin.Shutdown();
			}

			// Attach new plugin to repository
			plugin.Attach(m_repository);
		}

		/// <summary>
		/// Removes a <see cref="IPlugin" /> from the map.
		/// </summary>
		/// <param name="plugin">The <see cref="IPlugin" /> to remove from the map.</param>
		/// <remarks>
		/// <para>
		/// Remove a specific plugin from this map.
		/// </para>
		/// </remarks>
		public void Remove(IPlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			lock(this)
			{
				m_mapName2Plugin.Remove(plugin.Name);
			}
		}

		#endregion Public Instance Methods

		#region Private Instance Fields

		private readonly Hashtable m_mapName2Plugin = new Hashtable();
		private readonly ILoggerRepository m_repository;

		#endregion Private Instance Fields
	}
}
