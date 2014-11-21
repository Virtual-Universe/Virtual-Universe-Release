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

namespace SmartInspect.Util.TypeConverters
{
	/// <summary>
	/// Type converter for Boolean.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Supports conversion from string to <c>bool</c> type.
	/// </para>
	/// </remarks>
	/// <seealso cref="ConverterRegistry"/>
	/// <seealso cref="IConvertFrom"/>
	/// <seealso cref="IConvertTo"/>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	internal class BooleanConverter : IConvertFrom
	{
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
		public bool CanConvertFrom(Type sourceType)
		{
			return (sourceType == typeof(string));
		}

		/// <summary>
		/// Convert the source object to the type supported by this object
		/// </summary>
		/// <param name="source">the object to convert</param>
		/// <returns>the converted object</returns>
		/// <remarks>
		/// <para>
		/// Uses the <see cref="Boolean.Parse"/> method to convert the
		/// <see cref="String"/> argument to a <see cref="Boolean"/>.
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
				return bool.Parse(str);
			}
			throw ConversionNotSupportedException.Create(typeof(bool), source);
		}

		#endregion
	}
}
