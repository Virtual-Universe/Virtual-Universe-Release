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

#if !NETCF && !CLIENT_PROFILE

using System.Web;

using SmartInspect.Layout;
using SmartInspect.Core;

namespace SmartInspect.Appender 
{
	/// <summary>
	/// <para>
	/// Appends log events to the ASP.NET <see cref="TraceContext"/> system.
	/// </para>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Diagnostic information and tracing messages that you specify are appended to the output 
	/// of the page that is sent to the requesting browser. Optionally, you can view this information
	/// from a separate trace viewer (Trace.axd) that displays trace information for every page in a 
	/// given application.
	/// </para>
	/// <para>
	/// Trace statements are processed and displayed only when tracing is enabled. You can control 
	/// whether tracing is displayed to a page, to the trace viewer, or both.
	/// </para>
	/// <para>
	/// The logging event is passed to the <see cref="M:TraceContext.Write(string)"/> or 
	/// <see cref="M:TraceContext.Warn(string)"/> method depending on the level of the logging event.
    /// The event's logger name is the default value for the category parameter of the Write/Warn method. 
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	/// <author>Ron Grabowski</author>
	public class AspNetTraceAppender : AppenderSkeleton 
	{
		#region Public Instances Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AspNetTraceAppender" /> class.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Default constructor.
		/// </para>
		/// </remarks>
		public AspNetTraceAppender() 
		{
		}

		#endregion // Public Instances Constructors

		#region Override implementation of AppenderSkeleton

		/// <summary>
		/// Write the logging event to the ASP.NET trace
		/// </summary>
		/// <param name="loggingEvent">the event to log</param>
		/// <remarks>
		/// <para>
		/// Write the logging event to the ASP.NET trace
		/// <c>HttpContext.Current.Trace</c> 
		/// (<see cref="TraceContext"/>).
		/// </para>
		/// </remarks>
		override protected void Append(LoggingEvent loggingEvent) 
		{
			// check if SmartInspect is running in the context of an ASP.NET application
			if (HttpContext.Current != null) 
			{
				// check if tracing is enabled for the current context
				if (HttpContext.Current.Trace.IsEnabled) 
				{
					if (loggingEvent.Level >= Level.Warn) 
					{
                        HttpContext.Current.Trace.Warn(m_category.Format(loggingEvent), RenderLoggingEvent(loggingEvent));
					}
					else 
					{
                        HttpContext.Current.Trace.Write(m_category.Format(loggingEvent), RenderLoggingEvent(loggingEvent));
					}
				}
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

		#endregion // Override implementation of AppenderSkeleton

        #region Public Instance Properties

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

	    #endregion

	    #region Private Instance Fields

	    /// <summary>
	    /// Defaults to %logger
	    /// </summary>
	    private PatternLayout m_category = new PatternLayout("%logger");

	    #endregion
	}
}

#endif // !NETCF
