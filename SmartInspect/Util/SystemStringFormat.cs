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
using System.Xml;
using System.Text.RegularExpressions;

namespace SmartInspect.Util
{
	/// <summary>
	/// Utility class that represents a format string.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Utility class that represents a format string.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class SystemStringFormat
	{
		private readonly IFormatProvider m_provider;
		private readonly string m_format;
		private readonly object[] m_args;

		#region Constructor

		/// <summary>
		/// Initialise the <see cref="SystemStringFormat"/>
		/// </summary>
		/// <param name="provider">An <see cref="System.IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">A <see cref="System.String"/> containing zero or more format items.</param>
		/// <param name="args">An <see cref="System.Object"/> array containing zero or more objects to format.</param>
		public SystemStringFormat(IFormatProvider provider, string format, params object[] args)
		{
			m_provider = provider;
			m_format = format;
			m_args = args;
		}

		#endregion Constructor

		/// <summary>
		/// Format the string and arguments
		/// </summary>
		/// <returns>the formatted string</returns>
		public override string ToString()
		{
			return StringFormat(m_provider, m_format, m_args);
		}

		#region StringFormat

		/// <summary>
		/// Replaces the format item in a specified <see cref="System.String"/> with the text equivalent 
		/// of the value of a corresponding <see cref="System.Object"/> instance in a specified array.
		/// A specified parameter supplies culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="System.IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">A <see cref="System.String"/> containing zero or more format items.</param>
		/// <param name="args">An <see cref="System.Object"/> array containing zero or more objects to format.</param>
		/// <returns>
		/// A copy of format in which the format items have been replaced by the <see cref="System.String"/> 
		/// equivalent of the corresponding instances of <see cref="System.Object"/> in args.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This method does not throw exceptions. If an exception thrown while formatting the result the
		/// exception and arguments are returned in the result string.
		/// </para>
		/// </remarks>
		private static string StringFormat(IFormatProvider provider, string format, params object[] args)
		{
			try
			{
				// The format is missing, log null value
				if (format == null)
				{
					return null;
				}

				// The args are missing - should not happen unless we are called explicitly with a null array
				if (args == null)
				{
					return format;
				}

				// Try to format the string
				return String.Format(provider, format, args);
			}
			catch(Exception ex)
			{
				SmartInspect.Util.LogLog.Warn(declaringType, "Exception while rendering format ["+format+"]", ex);
				return StringFormatError(ex, format, args);
			}
#if NETCF
			catch
			{
				SmartInspect.Util.LogLog.Warn(declaringType, "Exception while rendering format ["+format+"]");
				return StringFormatError(null, format, args);
			}
#endif
		}

		/// <summary>
		/// Process an error during StringFormat
		/// </summary>
		private static string StringFormatError(Exception formatException, string format, object[] args)
		{
			try
			{
				StringBuilder buf = new StringBuilder("<SmartInspect.Error>");

				if (formatException != null)
				{
					buf.Append("Exception during StringFormat: ").Append(formatException.Message);
				}
				else
				{
					buf.Append("Exception during StringFormat");
				}
				buf.Append(" <format>").Append(format).Append("</format>");
				buf.Append("<args>");
				RenderArray(args, buf);
				buf.Append("</args>");
				buf.Append("</SmartInspect.Error>");

				return buf.ToString();
			}
			catch(Exception ex)
			{
				SmartInspect.Util.LogLog.Error(declaringType, "INTERNAL ERROR during StringFormat error handling", ex);
				return "<SmartInspect.Error>Exception during StringFormat. See Internal Log.</SmartInspect.Error>";
			}
#if NETCF
			catch
			{
				SmartInspect.Util.LogLog.Error(declaringType, "INTERNAL ERROR during StringFormat error handling");
				return "<SmartInspect.Error>Exception during StringFormat. See Internal Log.</SmartInspect.Error>";
			}
#endif
		}

		/// <summary>
		/// Dump the contents of an array into a string builder
		/// </summary>
		private static void RenderArray(Array array, StringBuilder buffer)
		{
			if (array == null)
			{
				buffer.Append(SystemInfo.NullText);
			}
			else
			{
				if (array.Rank != 1)
				{
					buffer.Append(array.ToString());
				}
				else
				{
					buffer.Append("{");
					int len = array.Length;

					if (len > 0)
					{
						RenderObject(array.GetValue(0), buffer);
						for (int i = 1; i < len; i++)
						{
							buffer.Append(", ");
							RenderObject(array.GetValue(i), buffer);
						}
					}
					buffer.Append("}");
				}
			}
		}

		/// <summary>
		/// Dump an object to a string
		/// </summary>
		private static void RenderObject(Object obj, StringBuilder buffer)
		{
			if (obj == null)
			{
				buffer.Append(SystemInfo.NullText);
			}
			else
			{
				try
				{
					buffer.Append(obj);
				}
				catch(Exception ex)
				{
					buffer.Append("<Exception: ").Append(ex.Message).Append(">");
				}
#if NETCF
				catch
				{
					buffer.Append("<Exception>");
				}
#endif
			}
		}

		#endregion StringFormat

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the SystemStringFormat class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(SystemStringFormat);

	    #endregion Private Static Fields
	}
}
