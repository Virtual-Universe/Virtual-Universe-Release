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
using CSJ2K.j2k.io;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.quantization;
using CSJ2K.j2k.entropy.decoder;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k.codestream;
using CSJ2K.j2k.codestream.reader;
namespace CSJ2K.j2k.quantization.dequantizer
{
	
	/// <summary> This class holds the parameters for the scalar deadzone dequantizer
	/// (StdDequantizer class) for the current tile. Its constructor decodes the
	/// parameters from the main header and tile headers.
	/// 
	/// </summary>
	/// <seealso cref="StdDequantizer">
	/// 
	/// </seealso>
	public class StdDequantizerParams:DequantizerParams
	{
		/// <summary> Returns the type of the dequantizer for which the parameters are. The
		/// types are defined in the Dequantizer class.
		/// 
		/// </summary>
		/// <returns> The type of the dequantizer for which the parameters
		/// are. Always Q_TYPE_SCALAR_DZ.
		/// 
		/// </returns>
		/// <seealso cref="Dequantizer">
		/// 
		/// </seealso>
		override public int DequantizerType
		{
			get
			{
				return CSJ2K.j2k.quantization.QuantizationType_Fields.Q_TYPE_SCALAR_DZ;
			}
			
		}
		
		/// <summary> The quantization step "exponent" value, for each resolution level and
		/// subband, as it appears in the codestream. The first index is the
		/// resolution level, and the second the subband index (within the
		/// resolution level), as specified in the Subband class. When in derived
		/// quantization mode only the first resolution level (level 0) appears.
		/// 
		/// <P>For non-reversible systems this value corresponds to ceil(log2(D')),
		/// where D' is the quantization step size normalized to data of a dynamic
		/// range of 1. The true quantization step size is (2^R)*D', where R is
		/// ceil(log2(dr)), where 'dr' is the dynamic range of the subband samples,
		/// in the corresponding subband.
		/// 
		/// <P>For reversible systems the exponent value in 'exp' is used to
		/// determine the number of magnitude bits in the quantized
		/// coefficients. It is, in fact, the dynamic range of the subband data.
		/// 
		/// <P>In general the index of the first subband in a resolution level is
		/// not 0. The exponents appear, within each resolution level, at their
		/// subband index, and not in the subband order starting from 0. For
		/// instance, resolution level 3, the first subband has the index 16, then
		/// the exponent of the subband is exp[3][16], not exp[3][0].
		/// 
		/// </summary>
		/// <seealso cref="Subband">
		/// 
		/// </seealso>
		public int[][] exp;
		
		/// <summary> The quantization step for non-reversible systems, normalized to a
		/// dynamic range of 1, for each resolution level and subband, as derived
		/// from the exponent-mantissa representation in the codestream. The first
		/// index is the resolution level, and the second the subband index (within
		/// the resolution level), as specified in the Subband class. When in
		/// derived quantization mode only the first resolution level (level 0)
		/// appears.
		/// 
		/// <P>The true step size D is obtained as follows: D=(2^R)*D', where
		/// 'R=ceil(log2(dr))' and 'dr' is the dynamic range of the subband
		/// samples, in the corresponding subband.
		/// 
		/// <P>This value is 'null' for reversible systems (i.e. there is no true
		/// quantization, 'D' is always 1).
		/// 
		/// <P>In general the index of the first subband in a resolution level is
		/// not 0. The steps appear, within each resolution level, at their subband
		/// index, and not in the subband order starting from 0. For instance, if
		/// resolution level 3, the first subband has the index 16, then the step
		/// of the subband is nStep[3][16], not nStep[3][0].
		/// 
		/// </summary>
		/// <seealso cref="Subband">
		/// 
		/// </seealso>
		public float[][] nStep;
	}
}