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
using CSJ2K.j2k.wavelet.synthesis;
using CSJ2K.j2k.image;
namespace CSJ2K.j2k.entropy.decoder
{
	
	/// <summary> This interface defines a source of entropy coded data and methods to
	/// transfer it in a code-block by code-block basis. In each call to
	/// 'geCodeBlock()' a specified coded code-block is returned.
	/// 
	/// <p>This interface is the source of data for the entropy decoder. See the
	/// 'EntropyDecoder' class.</p>
	/// 
	/// <p>For each coded-code-block the entropy-coded data is returned along with
	/// its truncation point information in a 'DecLyrdCBlk' object.</p>
	/// 
	/// </summary>
	/// <seealso cref="EntropyDecoder">
	/// 
	/// </seealso>
	/// <seealso cref="DecLyrdCBlk">
	/// 
	/// </seealso>
	/// <seealso cref="jj2000.j2k.codestream.reader.BitstreamReaderAgent">
	/// 
	/// </seealso>
	public interface CodedCBlkDataSrcDec:InvWTData
	{
		
		/// <summary> Returns the specified coded code-block, for the specified component, in
		/// the current tile. The first layer to return is indicated by 'fl'. The
		/// number of layers that is returned depends on 'nl' and the amount of
		/// data available.
		/// 
		/// <p>The argument 'fl' is to be used by subsequent calls to this method
		/// for the same code-block. In this way supplamental data can be retrieved
		/// at a later time. The fact that data from more than one layer can be
		/// returned means that several packets from the same code-block, of the
		/// same component, and the same tile, have been concatenated.</p>
		/// 
		/// <p>The returned compressed code-block can have its progressive
		/// attribute set. If this attribute is set it means that more data can be
		/// obtained by subsequent calls to this method (subject to transmission
		/// delays, etc). If the progressive attribute is not set it means that the
		/// returned data is all the data that can be obtained for the specified
		/// subblock.</p>
		/// 
		/// <p>The compressed code-block is uniquely specified by the current tile,
		/// the component (identified by 'c'), the subband (indentified by 'sb')
		/// and the code-bock vertical and horizontal indexes 'm' and 'n'.</p>
		/// 
		/// <p>The 'ulx' and 'uly' members of the returned 'DecLyrdCBlk' object
		/// contain the coordinates of the top-left corner of the block, with
		/// respect to the tile, not the subband.</p>
		/// 
		/// </summary>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <param name="m">The vertical index of the code-block to return, in the
		/// specified subband.
		/// 
		/// </param>
		/// <param name="n">The horizontal index of the code-block to return, in the
		/// specified subband.
		/// 
		/// </param>
		/// <param name="sb">The subband in whic the requested code-block is.
		/// 
		/// </param>
		/// <param name="fl">The first layer to return.
		/// 
		/// </param>
		/// <param name="nl">The number of layers to return, if negative all available
		/// layers are returned, starting at 'fl'.
		/// 
		/// </param>
		/// <param name="ccb">If not null this object is used to return the compressed
		/// code-block. If null a new object is created and returned. If the data
		/// array in ccb is not null then it can be reused to return the compressed
		/// data.
		/// 
		/// </param>
		/// <returns> The compressed code-block, with a certain number of layers
		/// determined by the available data and 'nl'.
		/// 
		/// </returns>
		DecLyrdCBlk getCodeBlock(int c, int m, int n, SubbandSyn sb, int fl, int nl, DecLyrdCBlk ccb);
	}
}