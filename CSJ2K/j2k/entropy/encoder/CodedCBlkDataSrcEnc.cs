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
using CSJ2K.j2k.image;
namespace CSJ2K.j2k.entropy.encoder
{
	
	/// <summary> This interface defines a source of entropy coded data and methods to
	/// transfer it in a code-block by code-block basis. In each call to
	/// 'getNextCodeBlock()' a new coded code-block is returned. The code-block are
	/// retruned in no specific-order.
	/// 
	/// <p>This interface is the source of data for the rate allocator. See the
	/// 'PostCompRateAllocator' class.</p>
	/// 
	/// <p>For each coded-code-block the entropy-coded data is returned along with
	/// the rate-distortion statistics in a 'CBlkRateDistStats' object.</p>
	/// 
	/// </summary>
	/// <seealso cref="PostCompRateAllocator">
	/// </seealso>
	/// <seealso cref="CBlkRateDistStats">
	/// </seealso>
	/// <seealso cref="EntropyCoder">
	/// 
	/// </seealso>
	public interface CodedCBlkDataSrcEnc:ForwWTDataProps
	{
		
		/// <summary> Returns the next coded code-block in the current tile for the specified
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
		/// any problems after being returned.</p>
		/// 
		/// </summary>
		/// <param name="c">The component for which to return the next code-block.
		/// 
		/// </param>
		/// <param name="ccb">If non-null this object might be used in returning the coded
		/// code-block in this or any subsequent call to this method. If null a new
		/// one is created and returned. If the 'data' array of 'cbb' is not null
		/// it may be reused to return the compressed data.
		/// 
		/// </param>
		/// <returns> The next coded code-block in the current tile for component
		/// 'c', or null if all code-blocks for the current tile have been
		/// returned.
		/// 
		/// </returns>
		/// <seealso cref="CBlkRateDistStats">
		/// 
		/// </seealso>
		CBlkRateDistStats getNextCodeBlock(int c, CBlkRateDistStats ccb);
		
		/// <summary> Returns the width of a packet for the specified tile-component and
		/// resolution level.
		/// 
		/// </summary>
		/// <param name="t">The tile
		/// 
		/// </param>
		/// <param name="c">The component
		/// 
		/// </param>
		/// <param name="r">The resolution level
		/// 
		/// </param>
		/// <returns> The width of a packet for the specified tile- component and
		/// resolution level.
		/// 
		/// </returns>
		int getPPX(int t, int c, int r);
		
		/// <summary> Returns the height of a packet for the specified tile-component and
		/// resolution level.
		/// 
		/// </summary>
		/// <param name="t">The tile
		/// 
		/// </param>
		/// <param name="c">The component
		/// 
		/// </param>
		/// <param name="r">The resolution level
		/// 
		/// </param>
		/// <returns> The height of a packet for the specified tile- component and
		/// resolution level.
		/// 
		/// </returns>
		int getPPY(int t, int c, int r);
		
		/// <summary> Returns true if the precinct partition is used for the specified
		/// component and tile, returns false otherwise
		/// 
		/// </summary>
		/// <param name="c">The component
		/// 
		/// </param>
		/// <param name="t">The tile
		/// 
		/// </param>
		bool precinctPartitionUsed(int c, int t);
	}
}