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

using SmartInspect.Core;
using SmartInspect.Appender;

namespace SmartInspect.Util
{
	/// <summary>
	/// A straightforward implementation of the <see cref="IAppenderAttachable"/> interface.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This is the default implementation of the <see cref="IAppenderAttachable"/>
	/// interface. Implementors of the <see cref="IAppenderAttachable"/> interface
	/// should aggregate an instance of this type.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class AppenderAttachedImpl : IAppenderAttachable
	{
		#region Public Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="AppenderAttachedImpl"/> class.
		/// </para>
		/// </remarks>
		public AppenderAttachedImpl()
		{
		}

		#endregion Public Instance Constructors

		#region Public Instance Methods

		/// <summary>
		/// Append on on all attached appenders.
		/// </summary>
		/// <param name="loggingEvent">The event being logged.</param>
		/// <returns>The number of appenders called.</returns>
		/// <remarks>
		/// <para>
		/// Calls the <see cref="IAppender.DoAppend" /> method on all 
		/// attached appenders.
		/// </para>
		/// </remarks>
		public int AppendLoopOnAppenders(LoggingEvent loggingEvent) 
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}

			// m_appenderList is null when empty
			if (m_appenderList == null) 
			{
				return 0;
			}

			if (m_appenderArray == null)
			{
				m_appenderArray = m_appenderList.ToArray();
			}

			foreach(IAppender appender in m_appenderArray)
			{
				try
				{
					appender.DoAppend(loggingEvent);
				}
				catch(Exception ex)
				{
					LogLog.Error(declaringType, "Failed to append to appender [" + appender.Name + "]", ex);
				}
			}
			return m_appenderList.Count;
		}

		/// <summary>
		/// Append on on all attached appenders.
		/// </summary>
		/// <param name="loggingEvents">The array of events being logged.</param>
		/// <returns>The number of appenders called.</returns>
		/// <remarks>
		/// <para>
		/// Calls the <see cref="IAppender.DoAppend" /> method on all 
		/// attached appenders.
		/// </para>
		/// </remarks>
		public int AppendLoopOnAppenders(LoggingEvent[] loggingEvents) 
		{
			if (loggingEvents == null)
			{
				throw new ArgumentNullException("loggingEvents");
			}
			if (loggingEvents.Length == 0)
			{
				throw new ArgumentException("loggingEvents array must not be empty", "loggingEvents");
			}
			if (loggingEvents.Length == 1)
			{
				// Fall back to single event path
				return AppendLoopOnAppenders(loggingEvents[0]);
			}

			// m_appenderList is null when empty
			if (m_appenderList == null) 
			{
				return 0;
			}

			if (m_appenderArray == null)
			{
				m_appenderArray = m_appenderList.ToArray();
			}

			foreach(IAppender appender in m_appenderArray)
			{
				try
				{
					CallAppend(appender, loggingEvents);
				}
				catch(Exception ex)
				{
					LogLog.Error(declaringType, "Failed to append to appender [" + appender.Name + "]", ex);
				}
			}
			return m_appenderList.Count;
		}

		#endregion Public Instance Methods

        #region Private Static Methods

        /// <summary>
		/// Calls the DoAppende method on the <see cref="IAppender"/> with 
		/// the <see cref="LoggingEvent"/> objects supplied.
		/// </summary>
		/// <param name="appender">The appender</param>
		/// <param name="loggingEvents">The events</param>
		/// <remarks>
		/// <para>
		/// If the <paramref name="appender" /> supports the <see cref="IBulkAppender"/>
		/// interface then the <paramref name="loggingEvents" /> will be passed 
		/// through using that interface. Otherwise the <see cref="LoggingEvent"/>
		/// objects in the array will be passed one at a time.
		/// </para>
		/// </remarks>
		private static void CallAppend(IAppender appender, LoggingEvent[] loggingEvents)
		{
			IBulkAppender bulkAppender = appender as IBulkAppender;
			if (bulkAppender != null)
			{
				bulkAppender.DoAppend(loggingEvents);
			}
			else
			{
				foreach(LoggingEvent loggingEvent in loggingEvents)
				{
					appender.DoAppend(loggingEvent);
				}
			}
        }

        #endregion

        #region Implementation of IAppenderAttachable

        /// <summary>
		/// Attaches an appender.
		/// </summary>
		/// <param name="newAppender">The appender to add.</param>
		/// <remarks>
		/// <para>
		/// If the appender is already in the list it won't be added again.
		/// </para>
		/// </remarks>
		public void AddAppender(IAppender newAppender) 
		{
			// Null values for newAppender parameter are strictly forbidden.
			if (newAppender == null)
			{
				throw new ArgumentNullException("newAppender");
			}
	
			m_appenderArray = null;
			if (m_appenderList == null) 
			{
				m_appenderList = new AppenderCollection(1);
			}
			if (!m_appenderList.Contains(newAppender))
			{
				m_appenderList.Add(newAppender);
			}
		}

		/// <summary>
		/// Gets all attached appenders.
		/// </summary>
		/// <returns>
		/// A collection of attached appenders, or <c>null</c> if there
		/// are no attached appenders.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The read only collection of all currently attached appenders.
		/// </para>
		/// </remarks>
		public AppenderCollection Appenders 
		{
			get
			{
				if (m_appenderList == null)
				{
					// We must always return a valid collection
					return AppenderCollection.EmptyCollection;
				}
				else 
				{
					return AppenderCollection.ReadOnly(m_appenderList);
				}
			}
		}

		/// <summary>
		/// Gets an attached appender with the specified name.
		/// </summary>
		/// <param name="name">The name of the appender to get.</param>
		/// <returns>
		/// The appender with the name specified, or <c>null</c> if no appender with the
		/// specified name is found.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Lookup an attached appender by name.
		/// </para>
		/// </remarks>
		public IAppender GetAppender(string name) 
		{
			if (m_appenderList != null && name != null)
			{
				foreach(IAppender appender in m_appenderList)
				{
					if (name == appender.Name)
					{
						return appender;
					}
				}
			}
			return null;   
		}

		/// <summary>
		/// Removes all attached appenders.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Removes and closes all attached appenders
		/// </para>
		/// </remarks>
		public void RemoveAllAppenders() 
		{
			if (m_appenderList != null) 
			{
				foreach(IAppender appender in m_appenderList)
				{
					try
					{
						appender.Close();
					}
					catch(Exception ex)
					{
						LogLog.Error(declaringType, "Failed to Close appender ["+appender.Name+"]", ex);
					}
				}
				m_appenderList = null;	  
				m_appenderArray = null;
			}
		}

		/// <summary>
		/// Removes the specified appender from the list of attached appenders.
		/// </summary>
		/// <param name="appender">The appender to remove.</param>
		/// <returns>The appender removed from the list</returns>
		/// <remarks>
		/// <para>
		/// The appender removed is not closed.
		/// If you are discarding the appender you must call
		/// <see cref="IAppender.Close"/> on the appender removed.
		/// </para>
		/// </remarks>
		public IAppender RemoveAppender(IAppender appender) 
		{
			if (appender != null && m_appenderList != null) 
			{
				m_appenderList.Remove(appender);
				if (m_appenderList.Count == 0)
				{
					m_appenderList = null;
				}
				m_appenderArray = null;
			}
			return appender;
		}

		/// <summary>
		/// Removes the appender with the specified name from the list of appenders.
		/// </summary>
		/// <param name="name">The name of the appender to remove.</param>
		/// <returns>The appender removed from the list</returns>
		/// <remarks>
		/// <para>
		/// The appender removed is not closed.
		/// If you are discarding the appender you must call
		/// <see cref="IAppender.Close"/> on the appender removed.
		/// </para>
		/// </remarks>
		public IAppender RemoveAppender(string name) 
		{
			return RemoveAppender(GetAppender(name));
		}

		#endregion

		#region Private Instance Fields

		/// <summary>
		/// List of appenders
		/// </summary>
		private AppenderCollection m_appenderList;

		/// <summary>
		/// Array of appenders, used to cache the m_appenderList
		/// </summary>
		private IAppender[] m_appenderArray;

		#endregion Private Instance Fields

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the AppenderAttachedImpl class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(AppenderAttachedImpl);

	    #endregion Private Static Fields
	}
}
