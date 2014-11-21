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
using CSJ2K.j2k.util;
namespace CSJ2K.Color
{
	
	/// <summary> This class is responsible for the mapping between
	/// requested components and image channels.
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.colorspace.ColorSpace">
	/// </seealso>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A. Kern
	/// </author>
	public class ChannelDefinitionMapper:ColorSpaceMapper
	{
		/// <summary> Factory method for creating instances of this class.</summary>
		/// <param name="src">-- source of image data
		/// </param>
		/// <param name="csMap">-- provides colorspace info
		/// </param>
		/// <returns> ChannelDefinitionMapper instance
		/// </returns>
		/// <exception cref="ColorSpaceException">
		/// </exception>
		public static new BlkImgDataSrc createInstance(BlkImgDataSrc src, ColorSpace csMap)
		{
			
			return new ChannelDefinitionMapper(src, csMap);
		}
		
		/// <summary> Ctor which creates an ICCProfile for the image and initializes
		/// all data objects (input, working, and output).
		/// 
		/// </summary>
		/// <param name="src">-- Source of image data
		/// </param>
		/// <param name="csm">-- provides colorspace info
		/// </param>
		protected internal ChannelDefinitionMapper(BlkImgDataSrc src, ColorSpace csMap):base(src, csMap)
		{
			/* end ChannelDefinitionMapper ctor */
		}
		
		
		/// <summary> Returns, in the blk argument, a block of image data containing the
		/// specifed rectangular area, in the specified component. The data is
		/// returned, as a copy of the internal data, therefore the returned data
		/// can be modified "in place".
		/// 
		/// <P>The rectangular area to return is specified by the 'ulx', 'uly', 'w'
		/// and 'h' members of the 'blk' argument, relative to the current
		/// tile. These members are not modified by this method. The 'offset' of
		/// the returned data is 0, and the 'scanw' is the same as the block's
		/// width. See the 'DataBlk' class.
		/// 
		/// <P>If the data array in 'blk' is 'null', then a new one is created. If
		/// the data array is not 'null' then it is reused, and it must be large
		/// enough to contain the block's data. Otherwise an 'ArrayStoreException'
		/// or an 'IndexOutOfBoundsException' is thrown by the Java system.
		/// 
		/// <P>The returned data has its 'progressive' attribute set to that of the
		/// input data.
		/// 
		/// </summary>
		/// <param name="blk">Its coordinates and dimensions specify the area to
		/// return. If it contains a non-null data array, then it must have the
		/// correct dimensions. If it contains a null data array a new one is
		/// created. The fields in this object are modified to return the data.
		/// 
		/// </param>
		/// <param name="c">The index of the component from which to get the data. Only 0
		/// and 3 are valid.
		/// 
		/// </param>
		/// <returns> The requested DataBlk
		/// 
		/// </returns>
		/// <seealso cref="getInternCompData">
		/// 
		/// </seealso>
		public override DataBlk getCompData(DataBlk out_Renamed, int c)
		{
			return src.getCompData(out_Renamed, csMap.getChannelDefinition(c));
		}
		
		/// <summary> Returns, in the blk argument, a block of image data containing the
		/// specifed rectangular area, in the specified component. The data is
		/// returned, as a copy of the internal data, therefore the returned data
		/// can be modified "in place".
		/// 
		/// <P>The rectangular area to return is specified by the 'ulx', 'uly', 'w'
		/// and 'h' members of the 'blk' argument, relative to the current
		/// tile. These members are not modified by this method. The 'offset' of
		/// the returned data is 0, and the 'scanw' is the same as the block's
		/// width. See the 'DataBlk' class.
		/// 
		/// <P>This method, in general, is less efficient than the
		/// 'getInternCompData()' method since, in general, it copies the
		/// data. However if the array of returned data is to be modified by the
		/// caller then this method is preferable.
		/// 
		/// <P>If the data array in 'blk' is 'null', then a new one is created. If
		/// the data array is not 'null' then it is reused, and it must be large
		/// enough to contain the block's data. Otherwise an 'ArrayStoreException'
		/// or an 'IndexOutOfBoundsException' is thrown by the Java system.
		/// 
		/// <P>The returned data may have its 'progressive' attribute set. In this
		/// case the returned data is only an approximation of the "final" data.
		/// 
		/// </summary>
		/// <param name="blk">Its coordinates and dimensions specify the area to return,
		/// relative to the current tile. If it contains a non-null data array,
		/// then it must be large enough. If it contains a null data array a new
		/// one is created. Some fields in this object are modified to return the
		/// data.
		/// 
		/// </param>
		/// <param name="c">The index of the component from which to get the data.
		/// 
		/// </param>
		/// <seealso cref="getCompData">
		/// 
		/// </seealso>
		public override DataBlk getInternCompData(DataBlk out_Renamed, int c)
		{
			return src.getInternCompData(out_Renamed, csMap.getChannelDefinition(c));
		}
		
		/// <summary> Returns the number of bits, referred to as the "range bits",
		/// corresponding to the nominal range of the data in the specified
		/// component. If this number is <i>b</b> then for unsigned data the
		/// nominal range is between 0 and 2^b-1, and for signed data it is between
		/// -2^(b-1) and 2^(b-1)-1. For floating point data this value is not
		/// applicable.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The number of bits corresponding to the nominal range of the
		/// data. Fro floating-point data this value is not applicable and the
		/// return value is undefined.
		/// </returns>
		public override int getFixedPoint(int c)
		{
			return src.getFixedPoint(csMap.getChannelDefinition(c));
		}
		
		public override int getNomRangeBits(int c)
		{
			return src.getNomRangeBits(csMap.getChannelDefinition(c));
		}
		
		public override int getCompImgHeight(int c)
		{
			return src.getCompImgHeight(csMap.getChannelDefinition(c));
		}
		
		public override int getCompImgWidth(int c)
		{
			return src.getCompImgWidth(csMap.getChannelDefinition(c));
		}
		
		public override int getCompSubsX(int c)
		{
			return src.getCompSubsX(csMap.getChannelDefinition(c));
		}
		
		public override int getCompSubsY(int c)
		{
			return src.getCompSubsY(csMap.getChannelDefinition(c));
		}
		
		public override int getCompULX(int c)
		{
			return src.getCompULX(csMap.getChannelDefinition(c));
		}
		
		public override int getCompULY(int c)
		{
			return src.getCompULY(csMap.getChannelDefinition(c));
		}
		
		public override int getTileCompHeight(int t, int c)
		{
			return src.getTileCompHeight(t, csMap.getChannelDefinition(c));
		}
		
		public override int getTileCompWidth(int t, int c)
		{
			return src.getTileCompWidth(t, csMap.getChannelDefinition(c));
		}
		
		public override System.String ToString()
		{
			int i;
			System.Text.StringBuilder rep = new System.Text.StringBuilder("[ChannelDefinitionMapper nchannels= ").Append(ncomps);
			
			for (i = 0; i < ncomps; ++i)
			{
				rep.Append(eol).Append("  component[").Append(i).Append("] mapped to channel[").Append(csMap.getChannelDefinition(i)).Append("]");
			}
			
			return rep.Append("]").ToString();
		}
		
		/* end class ChannelDefinitionMapper */
	}
}