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
using CSJ2K.j2k.image;
namespace CSJ2K.j2k.wavelet.analysis
{
	
	/// <summary> This is an implementation of the 'CBlkWTData' abstract class for signed 32
	/// bit integer data.
	/// 
	/// <p>The methods in this class are declared final, so that they can be
	/// inlined by inlining compilers.</p>
	/// 
	/// </summary>
	/// <seealso cref="CBlkWTData">
	/// 
	/// </seealso>
	public class CBlkWTDataInt:CBlkWTData
	{
		/// <summary> Returns the data type of this object, always DataBlk.TYPE_INT.
		/// 
		/// </summary>
		/// <returns> The data type of the object, always DataBlk.TYPE_INT
		/// 
		/// </returns>
		override public int DataType
		{
			get
			{
				return DataBlk.TYPE_INT;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the array containing the data, or null if there is no data
		/// array. The returned array is an int array.
		/// 
		/// </summary>
		/// <returns> The array of data (a int[]) or null if there is no data.
		/// 
		/// </returns>
		/// <summary> Sets the data array to the specified one. The provided array must be a
		/// int array, otherwise a ClassCastException is thrown. The size of the
		/// array is not checked for consistency with the code-block dimensions.
		/// 
		/// </summary>
		/// <param name="arr">The data array to use. Must be an int array.
		/// 
		/// </param>
		override public System.Object Data
		{
			get
			{
				return data_array;
			}
			
			set
			{
				data_array = (int[]) value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the array containing the data, or null if there is no data
		/// array.
		/// 
		/// </summary>
		/// <returns> The array of data or null if there is no data.
		/// 
		/// </returns>
		/// <summary> Sets the data array to the specified one. The size of the array is not
		/// checked for consistency with the code-block dimensions. This method is
		/// more efficient than 'setData()'.
		/// 
		/// </summary>
		/// <param name="arr">The data array to use.
		/// 
		/// </param>
		virtual public int[] DataInt
		{
			get
			{
				return data_array;
			}
			
			set
			{
				data_array = value;
			}
			
		}
		
		/// <summary>The array where the data is stored </summary>
		public int[] data_array;
	}
}