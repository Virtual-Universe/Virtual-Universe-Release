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

using SmartInspect;
using SmartInspect.Core;
using SmartInspect.Util.TypeConverters;

namespace SmartInspect.Layout
{
	/// <summary>
	/// Type converter for the <see cref="IRawLayout"/> interface
	/// </summary>
	/// <remarks>
	/// <para>
	/// Used to convert objects to the <see cref="IRawLayout"/> interface.
	/// Supports converting from the <see cref="ILayout"/> interface to
	/// the <see cref="IRawLayout"/> interface using the <see cref="Layout2RawLayoutAdapter"/>.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class RawLayoutConverter : IConvertFrom
	{
		#region Override Implementation of IRawLayout

		/// <summary>
		/// Can the sourceType be converted to an <see cref="IRawLayout"/>
		/// </summary>
		/// <param name="sourceType">the source to be to be converted</param>
		/// <returns><c>true</c> if the source type can be converted to <see cref="IRawLayout"/></returns>
		/// <remarks>
		/// <para>
		/// Test if the <paramref name="sourceType"/> can be converted to a
		/// <see cref="IRawLayout"/>. Only <see cref="ILayout"/> is supported
		/// as the <paramref name="sourceType"/>.
		/// </para>
		/// </remarks>
		public bool CanConvertFrom(Type sourceType) 
		{
			// Accept an ILayout object
			return (typeof(ILayout).IsAssignableFrom(sourceType));
		}

		/// <summary>
		/// Convert the value to a <see cref="IRawLayout"/> object
		/// </summary>
		/// <param name="source">the value to convert</param>
		/// <returns>the <see cref="IRawLayout"/> object</returns>
		/// <remarks>
		/// <para>
		/// Convert the <paramref name="source"/> object to a 
		/// <see cref="IRawLayout"/> object. If the <paramref name="source"/> object
		/// is a <see cref="ILayout"/> then the <see cref="Layout2RawLayoutAdapter"/>
		/// is used to adapt between the two interfaces, otherwise an
		/// exception is thrown.
		/// </para>
		/// </remarks>
		public object ConvertFrom(object source) 
		{
			ILayout layout = source as ILayout;
			if (layout != null) 
			{
				return new Layout2RawLayoutAdapter(layout);
			}
			throw ConversionNotSupportedException.Create(typeof(IRawLayout), source);
		}

		#endregion
	}
}
