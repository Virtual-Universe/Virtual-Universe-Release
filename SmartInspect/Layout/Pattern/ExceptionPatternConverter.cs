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

using System.Diagnostics;
using System.IO;
using SmartInspect.Core;

namespace SmartInspect.Layout.Pattern
{
	/// <summary>
	/// Write the exception text to the output
	/// </summary>
	/// <remarks>
	/// <para>
	/// If an exception object is stored in the logging event
	/// it will be rendered into the pattern output with a
	/// trailing newline.
	/// </para>
	/// <para>
	/// If there is no exception then nothing will be output
	/// and no trailing newline will be appended.
	/// It is typical to put a newline before the exception
	/// and to have the exception as the last data in the pattern.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	internal sealed class ExceptionPatternConverter : PatternLayoutConverter 
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ExceptionPatternConverter()
		{
			// This converter handles the exception
			IgnoresException = false;
		}

		/// <summary>
		/// Write the exception text to the output
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="loggingEvent">the event being logged</param>
		/// <remarks>
		/// <para>
		/// If an exception object is stored in the logging event
		/// it will be rendered into the pattern output with a
		/// trailing newline.
		/// </para>
		/// <para>
		/// If there is no exception or the exception property specified
		/// by the Option value does not exist then nothing will be output
		/// and no trailing newline will be appended.
		/// It is typical to put a newline before the exception
		/// and to have the exception as the last data in the pattern.
		/// </para>
		/// <para>
		/// Recognized values for the Option parameter are:
		/// </para>
		/// <list type="bullet">
		///		<item>
		///			<description>Message</description>
		///		</item>
		///		<item>
		///			<description>Source</description>
		///		</item>
		///		<item>
		///			<description>StackTrace</description>
		///		</item>
		///		<item>
		///			<description>TargetSite</description>
		///		</item>
		///		<item>
		///			<description>HelpLink</description>
		///		</item>		
		/// </list>
		/// </remarks>
		override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent.ExceptionObject != null && Option != null && Option.Length > 0)
			{
				switch (Option.ToLower())
				{
					case "message":
						WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.Message);
						break;
#if !NETCF						
					case "source":
						WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.Source);
						break;
					case "stacktrace":
						WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.StackTrace);
						break;
					case "targetsite":
						WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.TargetSite);
						break;
					case "helplink":
						WriteObject(writer, loggingEvent.Repository, loggingEvent.ExceptionObject.HelpLink);
						break;
#endif						
					default:
						// do not output SystemInfo.NotAvailableText
						break;
				}
			}
			else
			{
				string exceptionString = loggingEvent.GetExceptionString();
				if (exceptionString != null && exceptionString.Length > 0) 
				{
					writer.WriteLine(exceptionString);
				}
				else
				{
					// do not output SystemInfo.NotAvailableText
				}
			}
		}
	}
}
