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

using SmartInspect.Repository;

namespace SmartInspect.Plugin
{
	/// <summary>
	/// Base implementation of <see cref="IPlugin"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Default abstract implementation of the <see cref="IPlugin"/>
	/// interface. This base class can be used by implementors
	/// of the <see cref="IPlugin"/> interface.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public abstract class PluginSkeleton : IPlugin
	{
		#region Protected Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">the name of the plugin</param>
		/// <remarks>
		/// Initializes a new Plugin with the specified name.
		/// </remarks>
		protected PluginSkeleton(string name)
		{
			m_name = name;
		}

		#endregion Protected Instance Constructors

		#region Implementation of IPlugin

		/// <summary>
		/// Gets or sets the name of the plugin.
		/// </summary>
		/// <value>
		/// The name of the plugin.
		/// </value>
		/// <remarks>
		/// <para>
		/// Plugins are stored in the <see cref="PluginMap"/>
		/// keyed by name. Each plugin instance attached to a
		/// repository must be a unique name.
		/// </para>
		/// <para>
		/// The name of the plugin must not change one the 
		/// plugin has been attached to a repository.
		/// </para>
		/// </remarks>
		public virtual string Name 
		{ 
			get { return m_name; }
			set { m_name = value; }
		}

		/// <summary>
		/// Attaches this plugin to a <see cref="ILoggerRepository"/>.
		/// </summary>
		/// <param name="repository">The <see cref="ILoggerRepository"/> that this plugin should be attached to.</param>
		/// <remarks>
		/// <para>
		/// A plugin may only be attached to a single repository.
		/// </para>
		/// <para>
		/// This method is called when the plugin is attached to the repository.
		/// </para>
		/// </remarks>
		public virtual void Attach(ILoggerRepository repository)
		{
			m_repository = repository;
		}

		/// <summary>
		/// Is called when the plugin is to shutdown.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method is called to notify the plugin that 
		/// it should stop operating and should detach from
		/// the repository.
		/// </para>
		/// </remarks>
		public virtual void Shutdown()
		{
		}

		#endregion Implementation of IPlugin

		#region Protected Instance Properties

		/// <summary>
		/// The repository for this plugin
		/// </summary>
		/// <value>
		/// The <see cref="ILoggerRepository" /> that this plugin is attached to.
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets or sets the <see cref="ILoggerRepository" /> that this plugin is 
		/// attached to.
		/// </para>
		/// </remarks>
		protected virtual ILoggerRepository LoggerRepository 
		{
			get { return this.m_repository;	}
			set { this.m_repository = value; }
		}

		#endregion Protected Instance Properties

		#region Private Instance Fields

		/// <summary>
		/// The name of this plugin.
		/// </summary>
		private string m_name;

		/// <summary>
		/// The repository this plugin is attached to.
		/// </summary>
		private ILoggerRepository m_repository;

		#endregion Private Instance Fields
	}
}
