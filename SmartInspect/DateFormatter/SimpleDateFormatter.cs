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
using System.IO;

namespace SmartInspect.DateFormatter
{
	/// <summary>
	/// Formats the <see cref="DateTime"/> using the <see cref="M:DateTime.ToString(string, IFormatProvider)"/> method.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Formats the <see cref="DateTime"/> using the <see cref="DateTime"/> <see cref="M:DateTime.ToString(string, IFormatProvider)"/> method.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class SimpleDateFormatter : IDateFormatter
	{
		#region Public Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="SimpleDateFormatter" /> class 
		/// with the specified format string.
		/// </para>
		/// <para>
		/// The format string must be compatible with the options
		/// that can be supplied to <see cref="M:DateTime.ToString(string, IFormatProvider)"/>.
		/// </para>
		/// </remarks>
		public SimpleDateFormatter(string format)
		{
			m_formatString = format;
		}

		#endregion Public Instance Constructors

		#region Implementation of IDateFormatter

		/// <summary>
		/// Formats the date using <see cref="M:DateTime.ToString(string, IFormatProvider)"/>.
		/// </summary>
		/// <param name="dateToFormat">The date to convert to a string.</param>
		/// <param name="writer">The writer to write to.</param>
		/// <remarks>
		/// <para>
		/// Uses the date format string supplied to the constructor to call
		/// the <see cref="M:DateTime.ToString(string, IFormatProvider)"/> method to format the date.
		/// </para>
		/// </remarks>
		virtual public void FormatDate(DateTime dateToFormat, TextWriter writer)
		{
			writer.Write(dateToFormat.ToString(m_formatString, System.Globalization.DateTimeFormatInfo.InvariantInfo));
		}

		#endregion

		#region Private Instance Fields

		/// <summary>
		/// The format string used to format the <see cref="DateTime" />.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The format string must be compatible with the options
		/// that can be supplied to <see cref="M:DateTime.ToString(string, IFormatProvider)"/>.
		/// </para>
		/// </remarks>
		private readonly string m_formatString;

		#endregion Private Instance Fields
	}
}
