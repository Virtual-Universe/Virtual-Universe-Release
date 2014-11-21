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
using CSJ2K.j2k.wavelet.analysis;
using CSJ2K.j2k.entropy.encoder;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.image;
namespace CSJ2K.j2k.quantization.quantizer
{
	
	/// <summary> This interface defines a source of quantized wavelet coefficients and
	/// methods to transfer them in a code-block by code-block basis. In each call
	/// to 'getNextCodeBlock()' or 'getNextInternCodeBlock()' a new code-block is
	/// returned. The code-blocks are returned in no specific order.
	/// 
	/// <p>This class is the source of data for the entropy coder. See the
	/// 'EntropyCoder' class.</p>
	/// 
	/// <p>Code-block data is returned in sign-magnitude representation, instead of
	/// the normal two's complement one. Only integral types are used. The sign
	/// magnitude representation is more adequate for entropy coding. In sign
	/// magnitude representation, the most significant bit is used for the sign (0
	/// if positive, 1 if negative) and the magnitude of the coefficient is stored
	/// in the next M most significant bits. The rest of the bits (least
	/// significant bits) can contain a fractional value of the quantized
	/// coefficient. The number 'M' of magnitude bits is communicated in the
	/// 'magbits' member variable of the 'CBlkWTData'.</p>
	/// 
	/// <p>Note that no more of one object may request data, otherwise one object
	/// would get some of the data and another one another part, in no defined
	/// manner.</p>
	/// 
	/// </summary>
	/// <seealso cref="ForwWTDataProps">
	/// </seealso>
	/// <seealso cref="CBlkWTDataSrc">
	/// </seealso>
	/// <seealso cref="Quantizer">
	/// </seealso>
	/// <seealso cref="EntropyCoder">
	/// 
	/// </seealso>
	public interface CBlkQuantDataSrcEnc:ForwWTDataProps
	{
		
		/// <summary> Returns the next code-block in the current tile for the specified
		/// component, as a copy (see below). The order in which code-blocks are
		/// returned is not specified. However each code-block is returned only
		/// once and all code-blocks will be returned if the method is called 'N'
		/// times, where 'N' is the number of code-blocks in the tile. After all
		/// the code-blocks have been returned for the current tile calls to this
		/// method will return 'null'.
		/// 
		/// <p>When changing the current tile (through 'setTile()' or 'nextTile()')
		/// this method will always return the first code-block, as if this method
		/// was never called before for the new current tile.</p>
		/// 
		/// <p>The data returned by this method is always a copy of the internal
		/// data of this object, if any, and it can be modified "in place" without
		/// any problems after being returned. The 'offset' of the returned data is
		/// 0, and the 'scanw' is the same as the code-block width. See the
		/// 'CBlkWTData' class.</p>
		/// 
		/// <p>The 'ulx' and 'uly' members of the returned 'CBlkWTData' object
		/// contain the coordinates of the top-left corner of the block, with
		/// respect to the tile, not the subband.</p>
		/// 
		/// </summary>
		/// <param name="c">The component for which to return the next code-block.
		/// 
		/// </param>
		/// <param name="cblk">If non-null this object will be used to return the new
		/// code-block. If null a new one will be allocated and returned. If the
		/// "data" array of the object is non-null it will be reused, if possible,
		/// to return the data.
		/// 
		/// </param>
		/// <returns> The next code-block in the current tile for component 'c', or
		/// null if all code-blocks for the current tile have been returned.
		/// 
		/// </returns>
		/// <seealso cref="CBlkWTData">
		/// 
		/// </seealso>
		CBlkWTData getNextCodeBlock(int c, CBlkWTData cblk);
		
		/// <summary> Returns the next code-block in the current tile for the specified
		/// component. The order in which code-blocks are returned is not
		/// specified. However each code-block is returned only once and all
		/// code-blocks will be returned if the method is called 'N' times, where
		/// 'N' is the number of code-blocks in the tile. After all the code-blocks
		/// have been returned for the current tile calls to this method will
		/// return 'null'.
		/// 
		/// <p>When changing the current tile (through 'setTile()' or 'nextTile()')
		/// this method will always return the first code-block, as if this method
		/// was never called before for the new current tile.</p>
		/// 
		/// <p>The data returned by this method can be the data in the internal
		/// buffer of this object, if any, and thus can not be modified by the
		/// caller. The 'offset' and 'scanw' of the returned data can be
		/// arbitrary. See the 'CBlkWTData' class.</p>
		/// 
		/// <p>The 'ulx' and 'uly' members of the returned 'CBlkWTData' object
		/// contain the coordinates of the top-left corner of the block, with
		/// respect to the tile, not the subband.</p>
		/// 
		/// </summary>
		/// <param name="c">The component for which to return the next code-block.
		/// 
		/// </param>
		/// <param name="cblk">If non-null this object will be used to return the new
		/// code-block. If null a new one will be allocated and returned. If the
		/// "data" array of the object is non-null it will be reused, if possible,
		/// to return the data.
		/// 
		/// </param>
		/// <returns> The next code-block in the current tile for component 'n', or
		/// null if all code-blocks for the current tile have been returned.
		/// 
		/// </returns>
		/// <seealso cref="CBlkWTData">
		/// 
		/// </seealso>
		CBlkWTData getNextInternCodeBlock(int c, CBlkWTData cblk);
	}
}