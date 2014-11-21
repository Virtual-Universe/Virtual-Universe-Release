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

// .NET Compact Framework 1.0 has no support for System.Runtime.Remoting
#if !NETCF

using System;
using System.Runtime.Remoting;

using SmartInspect.Util;
using SmartInspect.Repository;
using SmartInspect.Core;
using IRemoteLoggingSink = SmartInspect.Appender.RemotingAppender.IRemoteLoggingSink;

namespace SmartInspect.Plugin
{
	/// <summary>
	/// Plugin that listens for events from the <see cref="SmartInspect.Appender.RemotingAppender"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// This plugin publishes an instance of <see cref="IRemoteLoggingSink"/> 
	/// on a specified <see cref="SinkUri"/>. This listens for logging events delivered from
	/// a remote <see cref="SmartInspect.Appender.RemotingAppender"/>.
	/// </para>
	/// <para>
	/// When an event is received it is relogged within the attached repository
	/// as if it had been raised locally.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class RemoteLoggingServerPlugin : PluginSkeleton
	{
		#region Public Instance Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="RemoteLoggingServerPlugin" /> class.
		/// </para>
		/// <para>
		/// The <see cref="SinkUri"/> property must be set.
		/// </para>
		/// </remarks>
		public RemoteLoggingServerPlugin() : base("RemoteLoggingServerPlugin:Unset URI")
		{
		}

		/// <summary>
		/// Construct with sink Uri.
		/// </summary>
		/// <param name="sinkUri">The name to publish the sink under in the remoting infrastructure. 
		/// See <see cref="SinkUri"/> for more details.</param>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="RemoteLoggingServerPlugin" /> class
		/// with specified name.
		/// </para>
		/// </remarks>
		public RemoteLoggingServerPlugin(string sinkUri) : base("RemoteLoggingServerPlugin:"+sinkUri)
		{
			m_sinkUri = sinkUri;
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets the URI of this sink.
		/// </summary>
		/// <value>
		/// The URI of this sink.
		/// </value>
		/// <remarks>
		/// <para>
		/// This is the name under which the object is marshaled.
		/// <see cref="M:RemotingServices.Marshal(MarshalByRefObject,String,Type)"/>
		/// </para>
		/// </remarks>
		public virtual string SinkUri 
		{ 
			get { return m_sinkUri; }
			set { m_sinkUri = value; }
		}

		#endregion Public Instance Properties

		#region Override implementation of PluginSkeleton

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
#if FRAMEWORK_4_0_OR_ABOVE
		[System.Security.SecuritySafeCritical]
#endif
		override public void Attach(ILoggerRepository repository)
		{
			base.Attach(repository);

			// Create the sink and marshal it
			m_sink = new RemoteLoggingSinkImpl(repository); 

			try
			{
				RemotingServices.Marshal(m_sink, m_sinkUri, typeof(IRemoteLoggingSink));		
			}
			catch(Exception ex)
			{
				LogLog.Error(declaringType, "Failed to Marshal remoting sink", ex);
			}
		}

		/// <summary>
		/// Is called when the plugin is to shutdown.
		/// </summary>
		/// <remarks>
		/// <para>
		/// When the plugin is shutdown the remote logging
		/// sink is disconnected.
		/// </para>
		/// </remarks>
#if FRAMEWORK_4_0_OR_ABOVE
        [System.Security.SecuritySafeCritical]
#endif
        override public void Shutdown()
		{
			// Stops the sink from receiving messages
			RemotingServices.Disconnect(m_sink);
			m_sink = null;

			base.Shutdown();
		}

		#endregion Override implementation of PluginSkeleton

		#region Private Instance Fields

		private RemoteLoggingSinkImpl m_sink;
		private string m_sinkUri;

		#endregion Private Instance Fields

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the RemoteLoggingServerPlugin class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(RemoteLoggingServerPlugin);

	    #endregion Private Static Fields

		/// <summary>
		/// Delivers <see cref="LoggingEvent"/> objects to a remote sink.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Internal class used to listen for logging events
		/// and deliver them to the local repository.
		/// </para>
		/// </remarks>
		private class RemoteLoggingSinkImpl : MarshalByRefObject, IRemoteLoggingSink
		{
			#region Public Instance Constructors

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="repository">The repository to log to.</param>
			/// <remarks>
			/// <para>
			/// Initializes a new instance of the <see cref="RemoteLoggingSinkImpl"/> for the
			/// specified <see cref="ILoggerRepository"/>.
			/// </para>
			/// </remarks>
			public RemoteLoggingSinkImpl(ILoggerRepository repository)
			{
				m_repository = repository;
			}

			#endregion Public Instance Constructors

			#region Implementation of IRemoteLoggingSink

			/// <summary>
			/// Logs the events to the repository.
			/// </summary>
			/// <param name="events">The events to log.</param>
			/// <remarks>
			/// <para>
			/// The events passed are logged to the <see cref="ILoggerRepository"/>
			/// </para>
			/// </remarks>
			public void LogEvents(LoggingEvent[] events)
			{
				if (events != null)
				{
					foreach(LoggingEvent logEvent in events)
					{
						if (logEvent != null)
						{
							m_repository.Log(logEvent);
						}
					}
				}
			}

			#endregion Implementation of IRemoteLoggingSink

			#region Override implementation of MarshalByRefObject

			/// <summary>
			/// Obtains a lifetime service object to control the lifetime 
			/// policy for this instance.
			/// </summary>
			/// <returns><c>null</c> to indicate that this instance should live forever.</returns>
			/// <remarks>
			/// <para>
			/// Obtains a lifetime service object to control the lifetime 
			/// policy for this instance. This object should live forever
			/// therefore this implementation returns <c>null</c>.
			/// </para>
			/// </remarks>
#if FRAMEWORK_4_0_OR_ABOVE
            [System.Security.SecurityCritical]
#endif
            public override object InitializeLifetimeService()
			{
				return null;
			}

			#endregion Override implementation of MarshalByRefObject

			#region Private Instance Fields

			/// <summary>
			/// The underlying <see cref="ILoggerRepository" /> that events should
			/// be logged to.
			/// </summary>
			private readonly ILoggerRepository m_repository;

			#endregion Private Instance Fields
		}
	}
}

#endif // !NETCF