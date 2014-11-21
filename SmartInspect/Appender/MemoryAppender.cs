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

using SmartInspect.Core;

namespace SmartInspect.Appender
{
	/// <summary>
	/// Stores logging events in an array.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The memory appender stores all the logging events
	/// that are appended in an in-memory array.
	/// </para>
	/// <para>
	/// Use the <see cref="GetEvents"/> method to get
	/// the current list of events that have been appended.
	/// </para>
	/// <para>
	/// Use the <see cref="M:Clear()"/> method to clear the
	/// current list of events.
	/// </para>
	/// </remarks>
	/// <author>Julian Biddle</author>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class MemoryAppender : AppenderSkeleton
	{
		#region Public Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MemoryAppender" /> class.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Default constructor.
		/// </para>
		/// </remarks>
		public MemoryAppender() : base()
		{
			m_eventsList = new ArrayList();
		}

		#endregion Protected Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets the events that have been logged.
		/// </summary>
		/// <returns>The events that have been logged</returns>
		/// <remarks>
		/// <para>
		/// Gets the events that have been logged.
		/// </para>
		/// </remarks>
		virtual public LoggingEvent[] GetEvents()
		{
            lock (m_eventsList.SyncRoot)
            {
                return (LoggingEvent[]) m_eventsList.ToArray(typeof(LoggingEvent));
            }
		}

		/// <summary>
		/// Gets or sets a value indicating whether only part of the logging event 
		/// data should be fixed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the appender should only fix part of the logging event 
		/// data, otherwise <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// <para>
		/// Setting this property to <c>true</c> will cause only part of the event 
		/// data to be fixed and stored in the appender, hereby improving performance. 
		/// </para>
		/// <para>
		/// See <see cref="M:LoggingEvent.FixVolatileData(bool)"/> for more information.
		/// </para>
		/// </remarks>
		[Obsolete("Use Fix property")]
		virtual public bool OnlyFixPartialEventData
		{
			get { return (Fix == FixFlags.Partial); }
			set 
			{ 
				if (value)
				{
					Fix = FixFlags.Partial;
				}
				else
				{
					Fix = FixFlags.All;
				}
			}
		}

		/// <summary>
		/// Gets or sets the fields that will be fixed in the event
		/// </summary>
		/// <remarks>
		/// <para>
		/// The logging event needs to have certain thread specific values 
		/// captured before it can be buffered. See <see cref="LoggingEvent.Fix"/>
		/// for details.
		/// </para>
		/// </remarks>
		virtual public FixFlags Fix
		{
			get { return m_fixFlags; }
			set { m_fixFlags = value; }
		}

		#endregion Public Instance Properties

		#region Override implementation of AppenderSkeleton

		/// <summary>
		/// This method is called by the <see cref="M:AppenderSkeleton.DoAppend(LoggingEvent)"/> method. 
		/// </summary>
		/// <param name="loggingEvent">the event to log</param>
		/// <remarks>
		/// <para>Stores the <paramref name="loggingEvent"/> in the events list.</para>
		/// </remarks>
		override protected void Append(LoggingEvent loggingEvent) 
		{
			// Because we are caching the LoggingEvent beyond the
			// lifetime of the Append() method we must fix any
			// volatile data in the event.
			loggingEvent.Fix = this.Fix;

            lock (m_eventsList.SyncRoot)
            {
                m_eventsList.Add(loggingEvent);
            }
		} 

		#endregion Override implementation of AppenderSkeleton

		#region Public Instance Methods

		/// <summary>
		/// Clear the list of events
		/// </summary>
		/// <remarks>
		/// Clear the list of events
		/// </remarks>
		virtual public void Clear()
		{
            lock (m_eventsList.SyncRoot)
            {
                m_eventsList.Clear();
            }
		}

		#endregion Public Instance Methods

		#region Protected Instance Fields

		/// <summary>
		/// The list of events that have been appended.
		/// </summary>
		protected ArrayList m_eventsList;

		/// <summary>
		/// Value indicating which fields in the event should be fixed
		/// </summary>
		/// <remarks>
		/// By default all fields are fixed
		/// </remarks>
		protected FixFlags m_fixFlags = FixFlags.All;

		#endregion Protected Instance Fields
	}
}
