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
using System.Globalization;
using System.Text;
using System.IO;

using SmartInspect.Core;
using SmartInspect.Util;

namespace SmartInspect.Layout.Pattern
{
	/// <summary>
	/// Converter to output and truncate <c>'.'</c> separated strings
	/// </summary>
	/// <remarks>
	/// <para>
	/// This abstract class supports truncating a <c>'.'</c> separated string
	/// to show a specified number of elements from the right hand side.
	/// This is used to truncate class names that are fully qualified.
	/// </para>
	/// <para>
	/// Subclasses should override the <see cref="GetFullyQualifiedName"/> method to
	/// return the fully qualified string.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public abstract class NamedPatternConverter : PatternLayoutConverter, IOptionHandler
	{
		private int m_precision = 0;

		#region Implementation of IOptionHandler

		/// <summary>
		/// Initialize the converter 
		/// </summary>
		/// <remarks>
		/// <para>
		/// This is part of the <see cref="IOptionHandler"/> delayed object
		/// activation scheme. The <see cref="ActivateOptions"/> method must 
		/// be called on this object after the configuration properties have
		/// been set. Until <see cref="ActivateOptions"/> is called this
		/// object is in an undefined state and must not be used. 
		/// </para>
		/// <para>
		/// If any of the configuration properties are modified then 
		/// <see cref="ActivateOptions"/> must be called again.
		/// </para>
		/// </remarks>
		public void ActivateOptions()
		{
			m_precision = 0;

			if (Option != null) 
			{
				string optStr = Option.Trim();
				if (optStr.Length > 0)
				{
					int precisionVal;
					if (SystemInfo.TryParse(optStr, out precisionVal))
					{
						if (precisionVal <= 0) 
						{
							LogLog.Error(declaringType, "NamedPatternConverter: Precision option (" + optStr + ") isn't a positive integer.");
						}
						else
						{
							m_precision = precisionVal;
						}
					} 
					else
					{
						LogLog.Error(declaringType, "NamedPatternConverter: Precision option \"" + optStr + "\" not a decimal integer.");
					}
				}
			}
		}

		#endregion

		/// <summary>
		/// Get the fully qualified string data
		/// </summary>
		/// <param name="loggingEvent">the event being logged</param>
		/// <returns>the fully qualified name</returns>
		/// <remarks>
		/// <para>
		/// Overridden by subclasses to get the fully qualified name before the
		/// precision is applied to it.
		/// </para>
		/// <para>
		/// Return the fully qualified <c>'.'</c> (dot/period) separated string.
		/// </para>
		/// </remarks>
		abstract protected string GetFullyQualifiedName(LoggingEvent loggingEvent);
	
		/// <summary>
		/// Convert the pattern to the rendered message
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="loggingEvent">the event being logged</param>
		/// <remarks>
		/// Render the <see cref="GetFullyQualifiedName"/> to the precision
		/// specified by the <see cref="PatternConverter.Option"/> property.
		/// </remarks>
		sealed override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			string name = GetFullyQualifiedName(loggingEvent);
			if (m_precision <= 0 || name == null || name.Length < 2)
			{
				writer.Write(name);
			}
			else 
			{
				int len = name.Length;
                string trailingDot = string.Empty;
                if (name.EndsWith(DOT))
                {
                    trailingDot = DOT;
                    name = name.Substring(0, len - 1);
                    len--;
                }

                int end = name.LastIndexOf(DOT);
				for(int i = 1; end > 0 && i < m_precision; i++) 
				{
                    end = name.LastIndexOf('.', end - 1);
                }
                if (end == -1)
                {
                    writer.Write(name + trailingDot);
                }
                else
                {
                    writer.Write(name.Substring(end + 1, len - end - 1) + trailingDot);
                }
			}	  
		}

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the NamedPatternConverter class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(NamedPatternConverter);

        private const string DOT = ".";
	    #endregion Private Static Fields
	}
}
