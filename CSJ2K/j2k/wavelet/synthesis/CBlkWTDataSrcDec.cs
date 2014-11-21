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
using CSJ2K.j2k.wavelet;
namespace CSJ2K.j2k.wavelet.synthesis
{
	
	/// <summary> This abstract class defines methods to transfer wavelet data in a
	/// code-block by code-block basis, for the decoder side. In each call to
	/// 'getCodeBlock()' or 'getInternCodeBlock()' a new code-block is
	/// returned. The code-blocks are returned in no specific order.
	/// 
	/// <p>This class is the source of data, in general, for the inverse wavelet
	/// transforms. See the 'InverseWT' class.</p>
	/// 
	/// </summary>
	/// <seealso cref="InvWTData">
	/// </seealso>
	/// <seealso cref="WaveletTransform">
	/// </seealso>
	/// <seealso cref="jj2000.j2k.quantization.dequantizer.CBlkQuantDataSrcDec">
	/// </seealso>
	/// <seealso cref="InverseWT">
	/// 
	/// </seealso>
	public interface CBlkWTDataSrcDec:InvWTData
	{
		
		/// <summary> Returns the number of bits, referred to as the "range bits",
		/// corresponding to the nominal range of the data in the specified
		/// component.
		/// 
		/// <p>The returned value corresponds to the nominal dynamic range of the
		/// reconstructed image data, not of the wavelet coefficients
		/// themselves. This is because different subbands have different gains and
		/// thus different nominal ranges. To have an idea of the nominal range in
		/// each subband the subband analysis gain value from the subband tree
		/// structure, returned by the 'getSynSubbandTree()' method, can be
		/// used. See the 'Subband' class for more details.</p>
		/// 
		/// <p>If this number is <i>b</b> then for unsigned data the nominal range
		/// is between 0 and 2^b-1, and for signed data it is between -2^(b-1) and
		/// 2^(b-1)-1.</p>
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The number of bits corresponding to the nominal range of the
		/// data.
		/// 
		/// </returns>
		/// <seealso cref="Subband">
		/// 
		/// </seealso>
		int getNomRangeBits(int c);
		
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
		
		/// <summary> Returns the specified code-block in the current tile for the specified
		/// component, as a copy (see below).
		/// 
		/// <p>The returned code-block may be progressive, which is indicated by
		/// the 'progressive' variable of the returned 'DataBlk' object. If a
		/// code-block is progressive it means that in a later request to this
		/// method for the same code-block it is possible to retrieve data which is
		/// a better approximation, since meanwhile more data to decode for the
		/// code-block could have been received. If the code-block is not
		/// progressive then later calls to this method for the same code-block
		/// will return the exact same data values.</p>
		/// 
		/// <p>The data returned by this method is always a copy of the internal
		/// data of this object, if any, and it can be modified "in place" without
		/// any problems after being returned. The 'offset' of the returned data is 
		/// 0, and the 'scanw' is the same as the code-block width. See the
		/// 'DataBlk' class.</p>
		/// 
		/// </summary>
		/// <param name="c">The component for which to return the next code-block.
		/// 
		/// </param>
		/// <param name="m">The vertical index of the code-block to return,
		/// in the specified subband.
		/// 
		/// </param>
		/// <param name="n">The horizontal index of the code-block to return,
		/// in the specified subband.
		/// 
		/// </param>
		/// <param name="sb">The subband in which the code-block to return is.
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
		/// <seealso cref="DataBlk">
		/// 
		/// </seealso>
		DataBlk getCodeBlock(int c, int m, int n, SubbandSyn sb, DataBlk cblk);
		
		/// <summary> Returns the specified code-block in the current tile for the specified
		/// component (as a reference or copy).
		/// 
		/// <p>The returned code-block may be progressive, which is indicated by
		/// the 'progressive' variable of the returned 'DataBlk' object. If a
		/// code-block is progressive it means that in a later request to this
		/// method for the same code-block it is possible to retrieve data which is
		/// a better approximation, since meanwhile more data to decode for the
		/// code-block could have been received. If the code-block is not
		/// progressive then later calls to this method for the same code-block
		/// will return the exact same data values.</p>
		/// 
		/// <p>The data returned by this method can be the data in the internal
		/// buffer of this object, if any, and thus can not be modified by the
		/// caller. The 'offset' and 'scanw' of the returned data can be
		/// arbitrary. See the 'DataBlk' class.</p>
		/// 
		/// </summary>
		/// <param name="c">The component for which to return the next code-block.
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
		/// <param name="sb">The subband in which the code-block to return is.
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
		/// <seealso cref="DataBlk">
		/// 
		/// </seealso>
		DataBlk getInternCodeBlock(int c, int m, int n, SubbandSyn sb, DataBlk cblk);
	}
}