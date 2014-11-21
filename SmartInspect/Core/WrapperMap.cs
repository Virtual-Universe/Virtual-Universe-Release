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

using SmartInspect.Repository;

namespace SmartInspect.Core
{
	#region WrapperCreationHandler

	/// <summary>
	/// Delegate used to handle creation of new wrappers.
	/// </summary>
	/// <param name="logger">The logger to wrap in a wrapper.</param>
	/// <remarks>
	/// <para>
	/// Delegate used to handle creation of new wrappers. This delegate
	/// is called from the <see cref="WrapperMap.CreateNewWrapperObject"/>
	/// method to construct the wrapper for the specified logger.
	/// </para>
	/// <para>
	/// The delegate to use is supplied to the <see cref="WrapperMap"/>
	/// constructor.
	/// </para>
	/// </remarks>
	public delegate ILoggerWrapper WrapperCreationHandler(ILogger logger);

	#endregion WrapperCreationHandler

	/// <summary>
	/// Maps between logger objects and wrapper objects.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class maintains a mapping between <see cref="ILogger"/> objects and
	/// <see cref="ILoggerWrapper"/> objects. Use the <see cref="GetWrapper"/> method to 
	/// lookup the <see cref="ILoggerWrapper"/> for the specified <see cref="ILogger"/>.
	/// </para>
	/// <para>
	/// New wrapper instances are created by the <see cref="CreateNewWrapperObject"/>
	/// method. The default behavior is for this method to delegate construction
	/// of the wrapper to the <see cref="WrapperCreationHandler"/> delegate supplied
	/// to the constructor. This allows specialization of the behavior without
	/// requiring subclassing of this type.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class WrapperMap
	{
		#region Public Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperMap" />
		/// </summary>
		/// <param name="createWrapperHandler">The handler to use to create the wrapper objects.</param>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="WrapperMap" /> class with 
		/// the specified handler to create the wrapper objects.
		/// </para>
		/// </remarks>
		public WrapperMap(WrapperCreationHandler createWrapperHandler) 
		{
			m_createWrapperHandler = createWrapperHandler;

			// Create the delegates for the event callbacks
			m_shutdownHandler = new LoggerRepositoryShutdownEventHandler(ILoggerRepository_Shutdown);
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets the wrapper object for the specified logger.
		/// </summary>
		/// <returns>The wrapper object for the specified logger</returns>
		/// <remarks>
		/// <para>
		/// If the logger is null then the corresponding wrapper is null.
		/// </para>
		/// <para>
		/// Looks up the wrapper it it has previously been requested and
		/// returns it. If the wrapper has never been requested before then
		/// the <see cref="CreateNewWrapperObject"/> virtual method is
		/// called.
		/// </para>
		/// </remarks>
		virtual public ILoggerWrapper GetWrapper(ILogger logger)
		{
			// If the logger is null then the corresponding wrapper is null
			if (logger == null)
			{
				return null;
			}

			lock(this)
			{
				// Lookup hierarchy in map.
				Hashtable wrappersMap = (Hashtable)m_repositories[logger.Repository];

				if (wrappersMap == null)
				{
					// Hierarchy does not exist in map.
					// Must register with hierarchy

					wrappersMap = new Hashtable();
					m_repositories[logger.Repository] = wrappersMap;

					// Register for config reset & shutdown on repository
					logger.Repository.ShutdownEvent += m_shutdownHandler;
				}

				// Look for the wrapper object in the map
				ILoggerWrapper wrapperObject = wrappersMap[logger] as ILoggerWrapper;

				if (wrapperObject == null)
				{
					// No wrapper object exists for the specified logger

					// Create a new wrapper wrapping the logger
					wrapperObject = CreateNewWrapperObject(logger);
					
					// Store wrapper logger in map
					wrappersMap[logger] = wrapperObject;
				}

				return wrapperObject;
			}
		}

		#endregion Public Instance Properties

		#region Protected Instance Properties

		/// <summary>
		/// Gets the map of logger repositories.
		/// </summary>
		/// <value>
		/// Map of logger repositories.
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets the hashtable that is keyed on <see cref="ILoggerRepository"/>. The
		/// values are hashtables keyed on <see cref="ILogger"/> with the
		/// value being the corresponding <see cref="ILoggerWrapper"/>.
		/// </para>
		/// </remarks>
		protected Hashtable Repositories 
		{
			get { return this.m_repositories; }
		}

		#endregion Protected Instance Properties

		#region Protected Instance Methods

		/// <summary>
		/// Creates the wrapper object for the specified logger.
		/// </summary>
		/// <param name="logger">The logger to wrap in a wrapper.</param>
		/// <returns>The wrapper object for the logger.</returns>
		/// <remarks>
		/// <para>
		/// This implementation uses the <see cref="WrapperCreationHandler"/>
		/// passed to the constructor to create the wrapper. This method
		/// can be overridden in a subclass.
		/// </para>
		/// </remarks>
		virtual protected ILoggerWrapper CreateNewWrapperObject(ILogger logger)
		{
			if (m_createWrapperHandler != null)
			{
				return m_createWrapperHandler(logger);
			}
			return null;
		}

		/// <summary>
		/// Called when a monitored repository shutdown event is received.
		/// </summary>
		/// <param name="repository">The <see cref="ILoggerRepository"/> that is shutting down</param>
		/// <remarks>
		/// <para>
		/// This method is called when a <see cref="ILoggerRepository"/> that this
		/// <see cref="WrapperMap"/> is holding loggers for has signaled its shutdown
		/// event <see cref="ILoggerRepository.ShutdownEvent"/>. The default
		/// behavior of this method is to release the references to the loggers
		/// and their wrappers generated for this repository.
		/// </para>
		/// </remarks>
		virtual protected void RepositoryShutdown(ILoggerRepository repository)
		{
			lock(this)
			{
				// Remove the repository from map
				m_repositories.Remove(repository);

				// Unhook events from the repository
				repository.ShutdownEvent -= m_shutdownHandler;
			}
		}

		/// <summary>
		/// Event handler for repository shutdown event.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event args.</param>
		private void ILoggerRepository_Shutdown(object sender, EventArgs e)
		{
			ILoggerRepository repository = sender as ILoggerRepository;
			if (repository != null)
			{
				// Remove all repository from map
				RepositoryShutdown(repository);
			}
		}

		#endregion Protected Instance Methods

		#region Private Instance Variables

		/// <summary>
		/// Map of logger repositories to hashtables of ILogger to ILoggerWrapper mappings
		/// </summary>
		private readonly Hashtable m_repositories = new Hashtable();

		/// <summary>
		/// The handler to use to create the extension wrapper objects.
		/// </summary>
		private readonly WrapperCreationHandler m_createWrapperHandler;

		/// <summary>
		/// Internal reference to the delegate used to register for repository shutdown events.
		/// </summary>
		private readonly LoggerRepositoryShutdownEventHandler m_shutdownHandler;
 
		#endregion Private Instance Variables
	}
}
