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

#define TRACE

using SmartInspect.Layout;
using SmartInspect.Core;

namespace SmartInspect.Appender
{
	/// <summary>
	/// Appends log events to the <see cref="System.Diagnostics.Trace"/> system.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The application configuration file can be used to control what listeners 
	/// are actually used. See the MSDN documentation for the 
	/// <see cref="System.Diagnostics.Trace"/> class for details on configuring the
	/// trace system.
	/// </para>
	/// <para>
	/// Events are written using the <c>System.Diagnostics.Trace.Write(string,string)</c>
	/// method. The event's logger name is the default value for the category parameter 
    /// of the Write method. 
	/// </para>
	/// <para>
	/// <b>Compact Framework</b><br />
	/// The Compact Framework does not support the <see cref="System.Diagnostics.Trace"/>
	/// class for any operation except <c>Assert</c>. When using the Compact Framework this
	/// appender will write to the <see cref="System.Diagnostics.Debug"/> system rather than
	/// the Trace system. This appender will therefore behave like the <see cref="DebugAppender"/>.
	/// </para>
	/// </remarks>
	/// <author>Douglas de la Torre</author>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
    /// <author>Ron Grabowski</author>
	public class TraceAppender : AppenderSkeleton
	{
		#region Public Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TraceAppender" />.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Default constructor.
		/// </para>
		/// </remarks>
		public TraceAppender()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TraceAppender" /> 
		/// with a specified layout.
		/// </summary>
		/// <param name="layout">The layout to use with this appender.</param>
		/// <remarks>
		/// <para>
		/// Obsolete constructor.
		/// </para>
		/// </remarks>
		[System.Obsolete("Instead use the default constructor and set the Layout property")]
		public TraceAppender(ILayout layout)
		{
			Layout = layout;
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets a value that indicates whether the appender will 
		/// flush at the end of each write.
		/// </summary>
		/// <remarks>
		/// <para>The default behavior is to flush at the end of each 
		/// write. If the option is set to<c>false</c>, then the underlying 
		/// stream can defer writing to physical medium to a later time. 
		/// </para>
		/// <para>
		/// Avoiding the flush operation at the end of each append results 
		/// in a performance gain of 10 to 20 percent. However, there is safety
		/// trade-off involved in skipping flushing. Indeed, when flushing is
		/// skipped, then it is likely that the last few log events will not
		/// be recorded on disk when the application exits. This is a high
		/// price to pay even for a 20% performance gain.
		/// </para>
		/// </remarks>
		public bool ImmediateFlush
		{
			get { return m_immediateFlush; }
			set { m_immediateFlush = value; }
		}

        /// <summary>
        /// The category parameter sent to the Trace method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Defaults to %logger which will use the logger name of the current 
        /// <see cref="LoggingEvent"/> as the category parameter.
        /// </para>
        /// <para>
        /// </para> 
        /// </remarks>
	    public PatternLayout Category
	    {
	        get { return m_category; }
	        set { m_category = value; }
	    }

	    #endregion Public Instance Properties

		#region Override implementation of AppenderSkeleton

		/// <summary>
		/// Writes the logging event to the <see cref="System.Diagnostics.Trace"/> system.
		/// </summary>
		/// <param name="loggingEvent">The event to log.</param>
		/// <remarks>
		/// <para>
		/// Writes the logging event to the <see cref="System.Diagnostics.Trace"/> system.
		/// </para>
		/// </remarks>
		override protected void Append(LoggingEvent loggingEvent) 
		{
			//
			// Write the string to the Trace system
			//
#if NETCF
			System.Diagnostics.Debug.Write(RenderLoggingEvent(loggingEvent), m_category.Format(loggingEvent));
#else
            System.Diagnostics.Trace.Write(RenderLoggingEvent(loggingEvent), m_category.Format(loggingEvent));
#endif
	 
			//
			// Flush the Trace system if needed
			//
			if (m_immediateFlush) 
			{
#if NETCF
				System.Diagnostics.Debug.Flush();
#else
				System.Diagnostics.Trace.Flush();
#endif
			} 
		}

		/// <summary>
		/// This appender requires a <see cref="Layout"/> to be set.
		/// </summary>
		/// <value><c>true</c></value>
		/// <remarks>
		/// <para>
		/// This appender requires a <see cref="Layout"/> to be set.
		/// </para>
		/// </remarks>
		override protected bool RequiresLayout
		{
			get { return true; }
		}

		#endregion Override implementation of AppenderSkeleton

		#region Private Instance Fields

		/// <summary>
		/// Immediate flush means that the underlying writer or output stream
		/// will be flushed at the end of each append operation.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Immediate flush is slower but ensures that each append request is 
		/// actually written. If <see cref="ImmediateFlush"/> is set to
		/// <c>false</c>, then there is a good chance that the last few
		/// logs events are not actually written to persistent media if and
		/// when the application crashes.
		/// </para>
		/// <para>
		/// The default value is <c>true</c>.</para>
		/// </remarks>
		private bool m_immediateFlush = true;

        /// <summary>
        /// Defaults to %logger
        /// </summary>
        private PatternLayout m_category = new PatternLayout("%logger");

		#endregion Private Instance Fields
	}
}
