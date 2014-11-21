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

using SmartInspect.Util;

namespace SmartInspect.Util.TypeConverters
{
	/// <summary>
	/// Convert between string and <see cref="PatternString"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Supports conversion from string to <see cref="PatternString"/> type, 
	/// and from a <see cref="PatternString"/> type to a string.
	/// </para>
	/// <para>
	/// The string is used as the <see cref="PatternString.ConversionPattern"/> 
	/// of the <see cref="PatternString"/>.
	/// </para>
	/// </remarks>
	/// <seealso cref="ConverterRegistry"/>
	/// <seealso cref="IConvertFrom"/>
	/// <seealso cref="IConvertTo"/>
	/// <author>Nicko Cadell</author>
	internal class PatternStringConverter : IConvertTo, IConvertFrom
	{
		#region Implementation of IConvertTo

		/// <summary>
		/// Can the target type be converted to the type supported by this object
		/// </summary>
		/// <param name="targetType">A <see cref="Type"/> that represents the type you want to convert to</param>
		/// <returns>true if the conversion is possible</returns>
		/// <remarks>
		/// <para>
		/// Returns <c>true</c> if the <paramref name="targetType"/> is
		/// assignable from a <see cref="String"/> type.
		/// </para>
		/// </remarks>
		public bool CanConvertTo(Type targetType)
		{
			return (typeof(string).IsAssignableFrom(targetType));
		}

		/// <summary>
		/// Converts the given value object to the specified type, using the arguments
		/// </summary>
		/// <param name="source">the object to convert</param>
		/// <param name="targetType">The Type to convert the value parameter to</param>
		/// <returns>the converted object</returns>
		/// <remarks>
		/// <para>
		/// Uses the <see cref="M:PatternString.Format()"/> method to convert the
		/// <see cref="PatternString"/> argument to a <see cref="String"/>.
		/// </para>
		/// </remarks>
		/// <exception cref="ConversionNotSupportedException">
		/// The <paramref name="source"/> object cannot be converted to the
		/// <paramref name="targetType"/>. To check for this condition use the 
		/// <see cref="CanConvertTo"/> method.
		/// </exception>
		public object ConvertTo(object source, Type targetType)
		{
			PatternString patternString = source as PatternString;
			if (patternString != null && CanConvertTo(targetType))
			{
				return patternString.Format();
			}
			throw ConversionNotSupportedException.Create(targetType, source);
		}

		#endregion

		#region Implementation of IConvertFrom

		/// <summary>
		/// Can the source type be converted to the type supported by this object
		/// </summary>
		/// <param name="sourceType">the type to convert</param>
		/// <returns>true if the conversion is possible</returns>
		/// <remarks>
		/// <para>
		/// Returns <c>true</c> if the <paramref name="sourceType"/> is
		/// the <see cref="String"/> type.
		/// </para>
		/// </remarks>
		public bool CanConvertFrom(System.Type sourceType)
		{
			return (sourceType == typeof(string));
		}

		/// <summary>
		/// Overrides the ConvertFrom method of IConvertFrom.
		/// </summary>
		/// <param name="source">the object to convert to a PatternString</param>
		/// <returns>the PatternString</returns>
		/// <remarks>
		/// <para>
		/// Creates and returns a new <see cref="PatternString"/> using
		/// the <paramref name="source"/> <see cref="String"/> as the
		/// <see cref="PatternString.ConversionPattern"/>.
		/// </para>
		/// </remarks>
		/// <exception cref="ConversionNotSupportedException">
		/// The <paramref name="source"/> object cannot be converted to the
		/// target type. To check for this condition use the <see cref="CanConvertFrom"/>
		/// method.
		/// </exception>
		public object ConvertFrom(object source) 
		{
			string str = source as string;
			if (str != null)
			{
				return new PatternString(str);
			}
			throw ConversionNotSupportedException.Create(typeof(PatternString), source);
		}

		#endregion
	}
}
