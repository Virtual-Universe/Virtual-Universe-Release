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
	
	/// <summary> This interface defines the methods to transfer image data in blocks,
	/// without following any particular order (random access). This interface does
	/// not define the methods to access the image characteristics, such as width,
	/// height, number of components, tiles, etc., or to change the current
	/// tile. That is provided by other interfaces such as ImgData.
	/// 
	/// <P>This interface has the notion of a current tile. All data, coordinates
	/// and dimensions are always relative to the current tile. If there is only
	/// one tile then it is equivalent as having no tiles.
	/// 
	/// <P>A block of requested data may never cross tile boundaries. This should
	/// be enforced by the implementing class, or the source of image data.
	/// 
	/// <P>This interface defines the methods that can be used to retrieve image
	/// data. Implementing classes need not buffer all the image data, they can ask
	/// their source to load the data they need.
	/// 
	/// </summary>
	/// <seealso cref="ImgData">
	/// 
	/// </seealso>
	public interface BlkImgDataSrc:ImgData
	{
		
		/// <summary> Returns the position of the fixed point in the specified component, or
		/// equivalently the number of fractional bits. This is the position of the
		/// least significant integral (i.e. non-fractional) bit, which is
		/// equivalent to the number of fractional bits. For instance, for
		/// fixed-point values with 2 fractional bits, 2 is returned. For
		/// floating-point data this value does not apply and 0 should be
		/// returned. Position 0 is the position of the least significant bit in
		/// the data.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The position of the fixed-point, which is the same as the
		/// number of fractional bits. For floating-point data 0 is returned.
		/// 
		/// </returns>
		int getFixedPoint(int c);
		
		/// <summary> Returns, in the blk argument, a block of image data containing the
		/// specifed rectangular area, in the specified component. The data is
		/// returned, as a reference to the internal data, if any, instead of as a
		/// copy, therefore the returned data should not be modified.
		/// 
		/// <P>The rectangular area to return is specified by the 'ulx', 'uly', 'w'
		/// and 'h' members of the 'blk' argument, relative to the current
		/// tile. These members are not modified by this method. The 'offset' and
		/// 'scanw' of the returned data can be arbitrary. See the 'DataBlk' class.
		/// 
		/// <P>This method, in general, is more efficient than the 'getCompData()'
		/// method since it may not copy the data. However if the array of returned
		/// data is to be modified by the caller then the other method is probably
		/// preferable.
		/// 
		/// <P>If possible, the data in the returned 'DataBlk' should be the
		/// internal data itself, instead of a copy, in order to increase the data
		/// transfer efficiency. However, this depends on the particular
		/// implementation (it may be more convenient to just return a copy of the
		/// data). This is the reason why the returned data should not be modified.
		/// 
		/// <P>If the data array in <tt>blk</tt> is <tt>null</tt>, then a new one
		/// is created if necessary. The implementation of this interface may
		/// choose to return the same array or a new one, depending on what is more
		/// efficient. Therefore, the data array in <tt>blk</tt> prior to the
		/// method call should not be considered to contain the returned data, a
		/// new array may have been created. Instead, get the array from
		/// <tt>blk</tt> after the method has returned.
		/// 
		/// <P>The returned data may have its 'progressive' attribute set. In this
		/// case the returned data is only an approximation of the "final" data.
		/// 
		/// </summary>
		/// <param name="blk">Its coordinates and dimensions specify the area to return,
		/// relative to the current tile. Some fields in this object are modified
		/// to return the data.
		/// 
		/// </param>
		/// <param name="c">The index of the component from which to get the data.
		/// 
		/// </param>
		/// <returns> The requested DataBlk
		/// 
		/// </returns>
		/// <seealso cref="getCompData">
		/// 
		/// </seealso>
		DataBlk getInternCompData(DataBlk blk, int c);
		
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
		/// <seealso cref="getInternCompData">
		/// 
		/// </seealso>
		DataBlk getCompData(DataBlk blk, int c);
	}
}