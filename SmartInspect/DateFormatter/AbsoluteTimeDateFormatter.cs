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
using System.IO;
using System.Text;

namespace SmartInspect.DateFormatter
{
	/// <summary>
	/// Formats a <see cref="DateTime"/> as <c>"HH:mm:ss,fff"</c>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Formats a <see cref="DateTime"/> in the format <c>"HH:mm:ss,fff"</c> for example, <c>"15:49:37,459"</c>.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class AbsoluteTimeDateFormatter : IDateFormatter
	{
		#region Protected Instance Methods

		/// <summary>
		/// Renders the date into a string. Format is <c>"HH:mm:ss"</c>.
		/// </summary>
		/// <param name="dateToFormat">The date to render into a string.</param>
		/// <param name="buffer">The string builder to write to.</param>
		/// <remarks>
		/// <para>
		/// Subclasses should override this method to render the date
		/// into a string using a precision up to the second. This method
		/// will be called at most once per second and the result will be
		/// reused if it is needed again during the same second.
		/// </para>
		/// </remarks>
		virtual protected void FormatDateWithoutMillis(DateTime dateToFormat, StringBuilder buffer)
		{
			int hour = dateToFormat.Hour;
			if (hour < 10) 
			{
				buffer.Append('0');
			}
			buffer.Append(hour);
			buffer.Append(':');

			int mins = dateToFormat.Minute;
			if (mins < 10) 
			{
				buffer.Append('0');
			}
			buffer.Append(mins);
			buffer.Append(':');
	
			int secs = dateToFormat.Second;
			if (secs < 10) 
			{
				buffer.Append('0');
			}
			buffer.Append(secs);
		}

		#endregion Protected Instance Methods

		#region Implementation of IDateFormatter

		/// <summary>
		/// Renders the date into a string. Format is "HH:mm:ss,fff".
		/// </summary>
		/// <param name="dateToFormat">The date to render into a string.</param>
		/// <param name="writer">The writer to write to.</param>
		/// <remarks>
		/// <para>
		/// Uses the <see cref="FormatDateWithoutMillis"/> method to generate the
		/// time string up to the seconds and then appends the current
		/// milliseconds. The results from <see cref="FormatDateWithoutMillis"/> are
		/// cached and <see cref="FormatDateWithoutMillis"/> is called at most once
		/// per second.
		/// </para>
		/// <para>
		/// Sub classes should override <see cref="FormatDateWithoutMillis"/>
		/// rather than <see cref="FormatDate"/>.
		/// </para>
		/// </remarks>
		virtual public void FormatDate(DateTime dateToFormat, TextWriter writer)
		{
                    lock (s_lastTimeStrings)
		    {
			// Calculate the current time precise only to the second
			long currentTimeToTheSecond = (dateToFormat.Ticks - (dateToFormat.Ticks % TimeSpan.TicksPerSecond));

                        string timeString = null;
			// Compare this time with the stored last time
			// If we are in the same second then append
			// the previously calculated time string
                        if (s_lastTimeToTheSecond != currentTimeToTheSecond)
                        {
                            s_lastTimeStrings.Clear();
                        }
                        else
                        {
                            timeString = (string) s_lastTimeStrings[GetType()];
                        }

                        if (timeString == null)
                        {
				// lock so that only one thread can use the buffer and
				// update the s_lastTimeToTheSecond and s_lastTimeStrings

				// PERF: Try removing this lock and using a new StringBuilder each time
				lock(s_lastTimeBuf)
				{
                                        timeString = (string) s_lastTimeStrings[GetType()];

                                        if (timeString == null)
                                        {
						// We are in a new second.
						s_lastTimeBuf.Length = 0;

						// Calculate the new string for this second
						FormatDateWithoutMillis(dateToFormat, s_lastTimeBuf);

						// Render the string buffer to a string
                                                timeString = s_lastTimeBuf.ToString();

						// Store the time as a string (we only have to do this once per second)
                                                s_lastTimeStrings[GetType()] = timeString;
						s_lastTimeToTheSecond = currentTimeToTheSecond;
					}
				}
			}
			writer.Write(timeString);
	
			// Append the current millisecond info
			writer.Write(',');
			int millis = dateToFormat.Millisecond;
			if (millis < 100) 
			{
				writer.Write('0');
			}
			if (millis < 10) 
			{
				writer.Write('0');
			}
			writer.Write(millis);
                    }
		}

		#endregion Implementation of IDateFormatter

		#region Public Static Fields

		/// <summary>
		/// String constant used to specify AbsoluteTimeDateFormat in layouts. Current value is <b>ABSOLUTE</b>.
		/// </summary>
		public const string AbsoluteTimeDateFormat = "ABSOLUTE";

		/// <summary>
		/// String constant used to specify DateTimeDateFormat in layouts.  Current value is <b>DATE</b>.
		/// </summary>
		public const string DateAndTimeDateFormat = "DATE";

		/// <summary>
		/// String constant used to specify ISO8601DateFormat in layouts. Current value is <b>ISO8601</b>.
		/// </summary>
		public const string Iso8601TimeDateFormat = "ISO8601";

		#endregion Public Static Fields

		#region Private Static Fields

		/// <summary>
		/// Last stored time with precision up to the second.
		/// </summary>
		private static long s_lastTimeToTheSecond = 0;

		/// <summary>
		/// Last stored time with precision up to the second, formatted
		/// as a string.
		/// </summary>
		private static StringBuilder s_lastTimeBuf = new StringBuilder();

		/// <summary>
		/// Last stored time with precision up to the second, formatted
		/// as a string.
		/// </summary>
		private static Hashtable s_lastTimeStrings = new Hashtable();

		#endregion Private Static Fields
	}
}
