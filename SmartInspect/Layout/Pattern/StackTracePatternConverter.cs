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

#if !NETCF
using System;
using System.IO;
using System.Diagnostics;

using SmartInspect.Util;
using SmartInspect.Core;

namespace SmartInspect.Layout.Pattern
{
	/// <summary>
	/// Write the caller stack frames to the output
	/// </summary>
	/// <remarks>
	/// <para>
	/// Writes the <see cref="LocationInfo.StackFrames"/> to the output writer, using format:
	/// type3.MethodCall3 > type2.MethodCall2 > type1.MethodCall1
	/// </para>
	/// </remarks>
	/// <author>Michael Cromwell</author>
	internal class StackTracePatternConverter : PatternLayoutConverter, IOptionHandler
	{
		private int m_stackFrameLevel = 1;
		
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
			if (Option == null)
				return;
			
			string optStr = Option.Trim();
			if (optStr.Length != 0)
			{
				int stackLevelVal;
				if (SystemInfo.TryParse(optStr, out stackLevelVal))
				{
					if (stackLevelVal <= 0) 
					{
						LogLog.Error(declaringType, "StackTracePatternConverter: StackeFrameLevel option (" + optStr + ") isn't a positive integer.");
					}
					else
					{
						m_stackFrameLevel = stackLevelVal;
					}
				} 
				else
				{
					LogLog.Error(declaringType, "StackTracePatternConverter: StackFrameLevel option \"" + optStr + "\" not a decimal integer.");
				}
			}
		}
		
		/// <summary>
		/// Write the strack frames to the output
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="loggingEvent">the event being logged</param>
		/// <remarks>
		/// <para>
		/// Writes the <see cref="LocationInfo.StackFrames"/> to the output writer.
		/// </para>
		/// </remarks>
		override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			StackFrameItem[] stackframes = loggingEvent.LocationInformation.StackFrames;
			if ((stackframes == null) || (stackframes.Length <= 0))
			{
				LogLog.Error(declaringType, "loggingEvent.LocationInformation.StackFrames was null or empty.");
				return;
			}
			
			int stackFrameIndex = m_stackFrameLevel - 1;
			while (stackFrameIndex >= 0)
			{
				if (stackFrameIndex >= stackframes.Length)
				{
					stackFrameIndex--;
					continue;
				}
				
				StackFrameItem stackFrame = stackframes[stackFrameIndex];
                writer.Write("{0}.{1}", stackFrame.ClassName, GetMethodInformation(stackFrame.Method));
				if (stackFrameIndex > 0)
				{
                    // TODO: make this user settable?
					writer.Write(" > ");
				}
				stackFrameIndex--;
			}
		}

                /// <summary>
        /// Returns the Name of the method
        /// </summary>
        /// <param name="method"></param>
        /// <remarks>This method was created, so this class could be used as a base class for StackTraceDetailPatternConverter</remarks>
        /// <returns>string</returns>
        internal virtual string GetMethodInformation(MethodItem method)
        {
            return method.Name;
        }
		
		#region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the StackTracePatternConverter class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(StackTracePatternConverter);

	    #endregion Private Static Fields
	}
}
#endif
