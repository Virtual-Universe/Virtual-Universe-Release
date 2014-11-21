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
	
	/// <summary> This is a generic abstract class to store a code-block of wavelet data,
	/// quantized or not. This class does not have the notion of
	/// components. Therefore, it should be used for data from a single
	/// component. Subclasses should implement the different types of storage
	/// (<tt>int</tt>, <tt>float</tt>, etc.).
	/// 
	/// <p>The data is always stored in one array, of the type matching the data
	/// type (i.e. for 'int' it's an 'int[]'). The data should be stored in the
	/// array in standard scan-line order. That is the samples go from the top-left
	/// corner of the code-block to the lower-right corner by line and then
	/// column.</p>
	/// 
	/// <p>The member variable 'offset' gives the index in the array of the first
	/// data element (i.e. the top-left coefficient). The member variable 'scanw'
	/// gives the width of the scan that is used to store the data, that can be
	/// different from the width of the block. Element '(x,y)' of the code-block
	/// (i.e. '(0,0)' is the top-left coefficient), will appear at position
	/// 'offset+y*scanw+x' in the array of data.</p>
	/// 
	/// <p>The classes <tt>CBlkWTDataInt</tt> and <tt>CBlkWTDataFloat</tt> provide
	/// implementations for <tt>int</tt> and <tt>float</tt> types respectively.</p>
	/// 
	/// <p>The types of data are the same as those defined by the 'DataBlk'
	/// class.</p>
	/// 
	/// </summary>
	/// <seealso cref="CBlkWTDataSrc">
	/// </seealso>
	/// <seealso cref="jj2000.j2k.quantization.quantizer.CBlkQuantDataSrcEnc">
	/// </seealso>
	/// <seealso cref="DataBlk">
	/// </seealso>
	/// <seealso cref="CBlkWTDataInt">
	/// </seealso>
	/// <seealso cref="CBlkWTDataFloat">
	/// 
	/// </seealso>
	public abstract class CBlkWTData
	{
		/// <summary> Returns the data type of the <tt>CBlkWTData</tt> object, as defined in
		/// the DataBlk class.
		/// 
		/// </summary>
		/// <returns> The data type of the object, as defined in the DataBlk class.
		/// 
		/// </returns>
		/// <seealso cref="DataBlk">
		/// 
		/// </seealso>
		public abstract int DataType{get;}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the array containing the data, or null if there is no data. The
		/// returned array is of the type returned by <tt>getDataType()</tt> (e.g.,
		/// for <tt>TYPE_INT</tt>, it is a <tt>int[]</tt>).
		/// 
		/// <p>Each implementing class should provide a type specific equivalent
		/// method (e.g., <tt>getDataInt()</tt> in <tt>DataBlkInt</tt>) which
		/// returns an array of the correct type explicitely and not through an
		/// <tt>Object</tt>.</p>
		/// 
		/// </summary>
		/// <returns> The array containing the data, or <tt>null</tt> if there is no
		/// data.
		/// 
		/// </returns>
		/// <seealso cref="getDataType">
		/// 
		/// </seealso>
		/// <summary> Sets the data array to the specified one. The type of the specified
		/// data array must match the one returned by <tt>getDataType()</tt> (e.g.,
		/// for <tt>TYPE_INT</tt>, it should be a <tt>int[]</tt>). If the wrong
		/// type of array is given a <tt>ClassCastException</tt> will be thrown.
		/// 
		/// <p>The size of the array is not necessarily checked for consistency
		/// with <tt>w</tt> and <tt>h</tt> or any other fields.</p>
		/// 
		/// <p>Each implementing class should provide a type specific equivalent
		/// method (e.g., <tt>setDataInt()</tt> in <tt>DataBlkInt</tt>) which takes
		/// an array of the correct type explicetely and not through an
		/// <tt>Object</tt>.</p>
		/// 
		/// </summary>
		/// <param name="arr">The new data array to use
		/// 
		/// </param>
		/// <seealso cref="getDataType">
		/// 
		/// </seealso>
		public abstract System.Object Data{get;set;}
		
		/// <summary>The horizontal coordinate of the upper-left corner of the code-block </summary>
		public int ulx;
		
		/// <summary>The vertical coordinate of the upper left corner of the code-block </summary>
		public int uly;
		
		/// <summary>The horizontal index of the code-block, within the subband </summary>
		public int n;
		
		/// <summary>The vertical index of the code-block, within the subband </summary>
		public int m;
		
		/// <summary>The subband in which this code-block is found </summary>
		public SubbandAn sb;
		
		/// <summary>The width of the code-block </summary>
		public int w;
		
		/// <summary>The height of the code-block </summary>
		public int h;
		
		/// <summary>The offset in the array of the top-left coefficient </summary>
		public int offset;
		
		/// <summary>The width of the scanlines used to store the data in the array </summary>
		public int scanw;
		
		/// <summary>The number of magnitude bits in the integer representation. This is
		/// only used for quantized wavelet data. 
		/// </summary>
		public int magbits;
		
		/// <summary>The WMSE scaling factor (multiplicative) to apply to the distortion
		/// measures of the data of this code-block. By default it is 1.
		/// </summary>
		public float wmseScaling = 1f;
		
		/// <summary>The value by which the absolute value of the data has to be divided in
		/// order to get the real absolute value. This value is useful to obtain
		/// the complement of 2 representation of a coefficient that is currently
		/// using the sign-magnitude representation. 
		/// </summary>
		public double convertFactor = 1.0;
		
		/// <summary>The quantization step size of the code-block. The value is updated by
		/// the quantizer module 
		/// </summary>
		public double stepSize = 1.0;
		
		/// <summary>Number of ROI coefficients in the code-block </summary>
		public int nROIcoeff = 0;
		
		/// <summary>Number of ROI magnitude bit-planes </summary>
		public int nROIbp = 0;
		
		/// <summary> Returns a string of informations about the DataBlk
		/// 
		/// </summary>
		/// <returns> Block dimensions and progressiveness in a string
		/// 
		/// </returns>
		public override System.String ToString()
		{
			System.String typeString = "";
			switch (DataType)
			{
				
				case DataBlk.TYPE_BYTE: 
					typeString = "Unsigned Byte";
					break;
				
				case DataBlk.TYPE_SHORT: 
					typeString = "Short";
					break;
				
				case DataBlk.TYPE_INT: 
					typeString = "Integer";
					break;
				
				case DataBlk.TYPE_FLOAT: 
					typeString = "Float";
					break;
				}
			
			return "ulx=" + ulx + ", uly=" + uly + ", idx=(" + m + "," + n + "), w=" + w + ", h=" + h + ", off=" + offset + ", scanw=" + scanw + ", wmseScaling=" + wmseScaling + ", convertFactor=" + convertFactor + ", stepSize=" + stepSize + ", type=" + typeString + ", magbits=" + magbits + ", nROIcoeff=" + nROIcoeff + ", nROIbp=" + nROIbp;
		}
	}
}