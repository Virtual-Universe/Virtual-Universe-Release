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

using SmartInspect.Core;
using SmartInspect.Util;
using SmartInspect.DateFormatter;

namespace SmartInspect.Layout.Pattern
{
	/// <summary>
	/// Write the TimeStamp to the output
	/// </summary>
	/// <remarks>
	/// <para>
	/// Date pattern converter, uses a <see cref="IDateFormatter"/> to format 
	/// the date of a <see cref="LoggingEvent"/>.
	/// </para>
	/// <para>
	/// Uses a <see cref="IDateFormatter"/> to format the <see cref="LoggingEvent.TimeStamp"/> 
	/// in Universal time.
	/// </para>
	/// <para>
	/// See the <see cref="DatePatternConverter"/> for details on the date pattern syntax.
	/// </para>
	/// </remarks>
	/// <seealso cref="DatePatternConverter"/>
	/// <author>Nicko Cadell</author>
	internal class UtcDatePatternConverter : DatePatternConverter
	{
		/// <summary>
		/// Write the TimeStamp to the output
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="loggingEvent">the event being logged</param>
		/// <remarks>
		/// <para>
		/// Pass the <see cref="LoggingEvent.TimeStamp"/> to the <see cref="IDateFormatter"/>
		/// for it to render it to the writer.
		/// </para>
		/// <para>
		/// The <see cref="LoggingEvent.TimeStamp"/> passed is in the local time zone, this is converted
		/// to Universal time before it is rendered.
		/// </para>
		/// </remarks>
		/// <seealso cref="DatePatternConverter"/>
		override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			try 
			{
				m_dateFormatter.FormatDate(loggingEvent.TimeStamp.ToUniversalTime(), writer);
			}
			catch (Exception ex) 
			{
				LogLog.Error(declaringType, "Error occurred while converting date.", ex);
			}
		}

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the UtcDatePatternConverter class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(UtcDatePatternConverter);

	    #endregion Private Static Fields
	}
}
