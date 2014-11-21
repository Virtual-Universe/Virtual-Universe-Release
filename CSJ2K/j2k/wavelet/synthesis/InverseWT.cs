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
using CSJ2K.j2k.quantization.dequantizer;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.decoder;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k;
namespace CSJ2K.j2k.wavelet.synthesis
{
	
	/// <summary> This abstract class extends the WaveletTransform one with the specifics of
	/// inverse wavelet transforms.
	/// 
	/// <p>The image can be reconstructed at different resolution levels. This is
	/// controlled by the setResLevel() method. All the image, tile and component
	/// dimensions are relative the the resolution level being used. The number of
	/// resolution levels indicates the number of wavelet recompositions that will
	/// be used, if it is equal as the number of decomposition levels then the full
	/// resolution image is reconstructed.</p>
	/// 
	/// <p>It is assumed in this class that all tiles and components the same
	/// reconstruction resolution level. If that where not the case the
	/// implementing class should have additional data structures to store those
	/// values for each tile. However, the 'recResLvl' member variable always
	/// contain the values applicable to the current tile, since many methods
	/// implemented here rely on them.</p>
	/// 
	/// </summary>
	public abstract class InverseWT:InvWTAdapter, BlkImgDataSrc
	{
		
		/// <summary> Initializes this object with the given source of wavelet
		/// coefficients. It initializes the resolution level for full resolutioin
		/// reconstruction (i.e. the maximum resolution available from the 'src'
		/// source).
		/// 
		/// <p>It is assumed here that all tiles and components have the same
		/// reconstruction resolution level. If that was not the case it should be
		/// the value for the current tile of the source.</p>
		/// 
		/// </summary>
		/// <param name="src">from where the wavelet coefficinets should be obtained.
		/// 
		/// </param>
		/// <param name="decSpec">The decoder specifications
		/// 
		/// </param>
		public InverseWT(MultiResImgData src, DecoderSpecs decSpec):base(src, decSpec)
		{
		}
		
		/// <summary> Creates an InverseWT object that works on the data type of the source,
		/// with the special additional parameters from the parameter
		/// list. Currently the parameter list is ignored since no special
		/// parameters can be specified for the inverse wavelet transform yet.
		/// 
		/// </summary>
		/// <param name="src">The source of data for the inverse wavelet
		/// transform.
		/// 
		/// </param>
		/// <param name="pl">The parameter list containing parameters applicable to the
		/// inverse wavelet transform (other parameters can also be present).
		/// 
		/// </param>
		public static InverseWT createInstance(CBlkWTDataSrcDec src, DecoderSpecs decSpec)
		{
			
			// full page wavelet transform
			return new InvWTFull(src, decSpec);
		}
		public abstract int getFixedPoint(int param1);
		public abstract CSJ2K.j2k.image.DataBlk getInternCompData(CSJ2K.j2k.image.DataBlk param1, int param2);
		public abstract CSJ2K.j2k.image.DataBlk getCompData(CSJ2K.j2k.image.DataBlk param1, int param2);
	}
}