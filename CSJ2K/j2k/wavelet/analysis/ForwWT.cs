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
using CSJ2K.j2k.wavelet;
namespace CSJ2K.j2k.wavelet.analysis
{
	
	/// <summary> This interface extends the WaveletTransform with the specifics of forward
	/// wavelet transforms. Classes that implement forward wavelet transfoms should
	/// implement this interface.
	/// 
	/// <p>This class does not define the methods to transfer data, just the
	/// specifics to forward wavelet transform. Different data transfer methods are 
	/// evisageable for different transforms.</p>
	/// 
	/// </summary>
	public interface ForwWT:WaveletTransform, ForwWTDataProps
	{
		
		/// <summary> Returns the horizontal analysis wavelet filters used in each level, for
		/// the specified tile-component. The first element in the array is the
		/// filter used to obtain the lowest resolution (resolution level 0)
		/// subbands (i.e. lowest frequency LL subband), the second element is the
		/// one used to generate the resolution level 1 subbands, and so on. If
		/// there are less elements in the array than the number of resolution
		/// levels, then the last one is assumed to repeat itself.
		/// 
		/// <p>The returned filters are applicable only to the specified component
		/// and in the current tile.</p>
		/// 
		/// <p>The resolution level of a subband is the resolution level to which a
		/// subband contributes, which is different from its decomposition
		/// level.</p>
		/// 
		/// </summary>
		/// <param name="t">The index of the tile for which to return the filters.
		/// 
		/// </param>
		/// <param name="c">The index of the component for which to return the filters.
		/// 
		/// </param>
		/// <returns> The horizontal analysis wavelet filters used in each level.
		/// 
		/// </returns>
		AnWTFilter[] getHorAnWaveletFilters(int t, int c);
		
		/// <summary> Returns the vertical analysis wavelet filters used in each level, for
		/// the specified tile-component. The first element in the array is the
		/// filter used to obtain the lowest resolution (resolution level 0)
		/// subbands (i.e. lowest frequency LL subband), the second element is the
		/// one used to generate the resolution level 1 subbands, and so on. If
		/// there are less elements in the array than the number of resolution
		/// levels, then the last one is assumed to repeat itself.
		/// 
		/// <p>The returned filters are applicable only to the specified component
		/// and in the current tile.</p>
		/// 
		/// <p>The resolution level of a subband is the resolution level to which a
		/// subband contributes, which is different from its decomposition
		/// level.</p>
		/// 
		/// </summary>
		/// <param name="t">The index of the tile for which to return the filters.
		/// 
		/// </param>
		/// <param name="c">The index of the component for which to return the filters.
		/// 
		/// </param>
		/// <returns> The vertical analysis wavelet filters used in each level.
		/// 
		/// </returns>
		AnWTFilter[] getVertAnWaveletFilters(int t, int c);
		
		/// <summary> Returns the number of decomposition levels that are applied to obtain
		/// the LL band, in the specified tile-component. A value of 0 means that
		/// no wavelet transform is applied.
		/// 
		/// </summary>
		/// <param name="t">The tile index
		/// 
		/// </param>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The number of decompositions applied to obtain the LL subband
		/// (0 for no wavelet transform).
		/// 
		/// </returns>
		int getDecompLevels(int t, int c);
		
		/// <summary> Returns the wavelet tree decomposition. Only WT_DECOMP_DYADIC is
		/// supported by JPEG 2000 part I.
		/// 
		/// </summary>
		/// <param name="t">The tile index
		/// 
		/// </param>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The wavelet decomposition.
		/// 
		/// </returns>
		int getDecomp(int t, int c);
	}
}