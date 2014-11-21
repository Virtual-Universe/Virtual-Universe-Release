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
namespace CSJ2K.j2k.image
{
	
	/// <summary> This is an implementation of the <tt>DataBlk</tt> interface for 32 bit
	/// floating point data (float).
	/// 
	/// <p>The methods in this class are declared final, so that they can be
	/// inlined by inlining compilers.</p>
	/// 
	/// </summary>
	/// <seealso cref="DataBlk">
	/// 
	/// </seealso>
	public class DataBlkFloat:DataBlk
	{
		/// <summary> Returns the identifier of this data type, <tt>TYPE_FLOAT</tt>, as
		/// defined in <tt>DataBlk</tt>.
		/// 
		/// </summary>
		/// <returns> The type of data stored. Always <tt>DataBlk.TYPE_FLOAT</tt>
		/// 
		/// </returns>
		/// <seealso cref="DataBlk.TYPE_FLOAT">
		/// 
		/// </seealso>
		override public int DataType
		{
			get
			{
				return TYPE_FLOAT;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the array containing the data, or null if there is no data
		/// array. The returned array is a float array.
		/// 
		/// </summary>
		/// <returns> The array of data (a float[]) or null if there is no data.
		/// 
		/// </returns>
		/// <summary> Sets the data array to the specified one. The provided array must be a
		/// float array, otherwise a ClassCastException is thrown. The size of the
		/// array is not checked for consistency with the block's dimensions.
		/// 
		/// </summary>
		/// <param name="arr">The data array to use. Must be a float array.
		/// 
		/// </param>
		override public System.Object Data
		{
			get
			{
				return data;
			}
			
			set
			{
				data = (float[]) value;
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
		/// checked for consistency with the block's dimensions.
		/// 
		/// </summary>
		/// <param name="arr">The data array to use.
		/// 
		/// </param>
		virtual public float[] DataFloat
		{
			get
			{
				return data;
			}
			
			set
			{
				data = value;
			}
			
		}
		/// <summary>The array where the data is stored </summary>
		private float[] data;
		
		/// <summary> Creates a DataBlkFloat with 0 dimensions and no data array
		/// (i.e. data is null).
		/// 
		/// </summary>
		public DataBlkFloat()
		{
		}
		
		/// <summary> Creates a DataBlkFloat with the specified dimensions and position. The
		/// data array is initialized to an array of size w*h.
		/// 
		/// </summary>
		/// <param name="ulx">The horizontal coordinate of the upper-left corner of the
		/// block
		/// 
		/// </param>
		/// <param name="uly">The vertical coordinate of the upper-left corner of the
		/// block
		/// 
		/// </param>
		/// <param name="w">The width of the block (in pixels)
		/// 
		/// </param>
		/// <param name="h">The height of the block (in pixels)
		/// 
		/// </param>
		public DataBlkFloat(int ulx, int uly, int w, int h)
		{
			this.ulx = ulx;
			this.uly = uly;
			this.w = w;
			this.h = h;
			offset = 0;
			scanw = w;
			data = new float[w * h];
		}
		
		/// <summary> Copy constructor. Creates a DataBlkFloat which is the copy of the
		/// DataBlkFloat given as paramter.
		/// 
		/// </summary>
		/// <param name="DataBlkFloat">the object to be copied.
		/// 
		/// </param>
		public DataBlkFloat(DataBlkFloat src)
		{
			this.ulx = src.ulx;
			this.uly = src.uly;
			this.w = src.w;
			this.h = src.h;
			this.offset = 0;
			this.scanw = this.w;
			this.data = new float[this.w * this.h];
			for (int i = 0; i < this.h; i++)
				Array.Copy(src.data, i * src.scanw, this.data, i * this.scanw, this.w);
		}
		
		/// <summary> Returns a string of informations about the DataBlkInt.
		/// 
		/// </summary>
		public override System.String ToString()
		{
			System.String str = base.ToString();
			if (data != null)
			{
				str += (",data=" + data.Length + " bytes");
			}
			return str;
		}
	}
}