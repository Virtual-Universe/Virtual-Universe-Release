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
using System.Text;
using System.IO;
using System.Collections;

using SmartInspect.Core;
using SmartInspect.Util;
using SmartInspect.Repository;

namespace SmartInspect.Layout.Pattern
{
	/// <summary>
	/// Abstract class that provides the formatting functionality that 
	/// derived classes need.
	/// </summary>
	/// <remarks>
	/// Conversion specifiers in a conversion patterns are parsed to
	/// individual PatternConverters. Each of which is responsible for
	/// converting a logging event in a converter specific manner.
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public abstract class PatternLayoutConverter : PatternConverter
	{
		#region Protected Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PatternLayoutConverter" /> class.
		/// </summary>
		protected PatternLayoutConverter() 
		{  
		}

		#endregion Protected Instance Constructors

		#region Public Properties

		/// <summary>
		/// Flag indicating if this converter handles the logging event exception
		/// </summary>
		/// <value><c>false</c> if this converter handles the logging event exception</value>
		/// <remarks>
		/// <para>
		/// If this converter handles the exception object contained within
		/// <see cref="LoggingEvent"/>, then this property should be set to
		/// <c>false</c>. Otherwise, if the layout ignores the exception
		/// object, then the property should be set to <c>true</c>.
		/// </para>
		/// <para>
		/// Set this value to override a this default setting. The default
		/// value is <c>true</c>, this converter does not handle the exception.
		/// </para>
		/// </remarks>
		virtual public bool IgnoresException 
		{ 
			get { return m_ignoresException; }
			set { m_ignoresException = value; }
		}

		#endregion Public Properties

		#region Protected Abstract Methods

		/// <summary>
		/// Derived pattern converters must override this method in order to
		/// convert conversion specifiers in the correct way.
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="loggingEvent">The <see cref="LoggingEvent" /> on which the pattern converter should be executed.</param>
		abstract protected void Convert(TextWriter writer, LoggingEvent loggingEvent);

		#endregion Protected Abstract Methods

		#region Protected Methods

		/// <summary>
		/// Derived pattern converters must override this method in order to
		/// convert conversion specifiers in the correct way.
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="state">The state object on which the pattern converter should be executed.</param>
		override protected void Convert(TextWriter writer, object state)
		{
			LoggingEvent loggingEvent = state as LoggingEvent;
			if (loggingEvent != null)
			{
				Convert(writer, loggingEvent);
			}
			else
			{
				throw new ArgumentException("state must be of type ["+typeof(LoggingEvent).FullName+"]", "state");
			}
		}

		#endregion Protected Methods

		/// <summary>
		/// Flag indicating if this converter handles exceptions
		/// </summary>
		/// <remarks>
		/// <c>false</c> if this converter handles exceptions
		/// </remarks>
		private bool m_ignoresException = true;
	}
}
