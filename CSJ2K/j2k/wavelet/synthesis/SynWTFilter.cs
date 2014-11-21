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
using CSJ2K.j2k.codestream;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.io;
using CSJ2K.j2k;
namespace CSJ2K.j2k.wavelet.synthesis
{
	
	/// <summary> This abstract class defines the methods of all synthesis wavelet
	/// filters. Specialized abstract classes that work on particular data types
	/// (int, float) provide more specific method calls while retaining the
	/// generality of this one. See the SynWTFilterInt and SynWTFilterFloat
	/// classes. Implementations of snythesis filters should inherit from one of
	/// those classes.
	/// 
	/// <p>The length of the output signal is always the sum of the length of the
	/// low-pass and high-pass input signals.</p>
	/// 
	/// <p>All synthesis wavelet filters should follow the following conventions:
	/// 
	/// <ul> 
	/// 
	/// <li>The first sample of the output corresponds to the low-pass one. As a
	/// consequence, if the output signal is of odd-length then the low-pass input
	/// signal is one sample longer than the high-pass input one. Therefore, if the
	/// length of output signal is N, the low-pass input signal is of length N/2 if
	/// N is even and N/2+1/2 if N is odd, while the high-pass input signal is of
	/// length N/2 if N is even and N/2-1/2 if N is odd.</li>
	/// 
	/// <li>The normalization of the analysis filters is 1 for the DC gain and 2
	/// for the Nyquist gain (Type I normalization), for both reversible and
	/// non-reversible filters. The normalization of the synthesis filters should
	/// ensure prefect reconstruction according to this normalization of the
	/// analysis wavelet filters.</li>
	/// 
	/// </ul>
	/// 
	/// <p>The synthetize method may seem very complicated, but is designed to
	/// minimize the amount of data copying and redundant calculations when used
	/// for block-based or line-based wavelet transform implementations, while
	/// being applicable to full-frame transforms as well.</p>
	/// 
	/// </summary>
	/// <seealso cref="SynWTFilterInt">
	/// </seealso>
	/// <seealso cref="SynWTFilterFloat">
	/// 
	/// </seealso>
	public abstract class SynWTFilter : WaveletFilter
	{
		public abstract int AnHighPosSupport{get;}
		public abstract int AnLowNegSupport{get;}
		public abstract int AnLowPosSupport{get;}
		public abstract bool Reversible{get;}
		public abstract int ImplType{get;}
		public abstract int SynHighNegSupport{get;}
		public abstract int SynHighPosSupport{get;}
		public abstract int AnHighNegSupport{get;}
		public abstract int DataType{get;}
		public abstract int SynLowNegSupport{get;}
		public abstract int SynLowPosSupport{get;}
		
		/// <summary> Reconstructs the output signal by the synthesis filter, recomposing the
		/// low-pass and high-pass input signals in one output signal. This method
		/// performs the upsampling and fitering with the low pass first filtering
		/// convention.
		/// 
		/// <p>The input low-pass (high-pass) signal resides in the lowSig
		/// array. The index of the first sample to filter (i.e. that will generate
		/// the first (second) output sample). is given by lowOff (highOff). This
		/// array must be of the same type as the one for which the particular
		/// implementation works with (which is returned by the getDataType()
		/// method).</p>
		/// 
		/// <p>The low-pass (high-pass) input signal can be interleaved with other
		/// signals in the same lowSig (highSig) array, and this is determined by
		/// the lowStep (highStep) argument. This means that the first sample of
		/// the low-pass (high-pass) input signal is lowSig[lowOff]
		/// (highSig[highOff]), the second is lowSig[lowOff+lowStep]
		/// (highSig[highOff+highStep]), the third is lowSig[lowOff+2*lowStep]
		/// (highSig[highOff+2*highStep]), and so on. Therefore if lowStep
		/// (highStep) is 1 there is no interleaving. This feature allows to filter
		/// columns of a 2-D signal, when it is stored in a line by line order in
		/// lowSig (highSig), without having to copy the data, in this case the
		/// lowStep (highStep) argument should be the line width of the low-pass
		/// (high-pass) signal.</p>
		/// 
		/// <p>The output signal is placed in the outSig array. The outOff and
		/// outStep arguments are analogous to the lowOff and lowStep ones, but
		/// they apply to the outSig array. The outSig array must be long enough to
		/// hold the low-pass output signal.</p>
		/// 
		/// </summary>
		/// <param name="lowSig">This is the array that contains the low-pass input
		/// signal. It must be of the correct type (e.g., it must be int[] if
		/// getDataType() returns TYPE_INT).
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="lowLen">This is the number of samples in the low-pass input
		/// signal to filter.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the low-pass
		/// input signal samples in the lowSig array. See above.
		/// 
		/// </param>
		/// <param name="highSig">This is the array that contains the high-pass input
		/// signal. It must be of the correct type (e.g., it must be int[] if
		/// getDataType() returns TYPE_INT).
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="highLen">This is the number of samples in the high-pass input
		/// signal to filter.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass input signal samples in the highSig array. See above.
		/// 
		/// </param>
		/// <param name="outSig">This is the array where the output signal is placed. It
		/// must be of the same type as lowSig and it should be long enough to
		/// contain the output signal.
		/// 
		/// </param>
		/// <param name="outOff">This is the index in outSig of the element where to put
		/// the first output sample.
		/// 
		/// </param>
		/// <param name="outStep">This is the step, or interleave factor, of the output
		/// samples in the outSig array. See above.
		/// 
		/// </param>
		public abstract void  synthetize_lpf(System.Object lowSig, int lowOff, int lowLen, int lowStep, System.Object highSig, int highOff, int highLen, int highStep, System.Object outSig, int outOff, int outStep);
		
		/// <summary> Reconstructs the output signal by the synthesis filter, recomposing the
		/// low-pass and high-pass input signals in one output signal. This method
		/// performs the upsampling and fitering with the high pass first filtering
		/// convention.
		/// 
		/// <p>The input low-pass (high-pass) signal resides in the lowSig
		/// array. The index of the first sample to filter (i.e. that will generate
		/// the first (second) output sample). is given by lowOff (highOff). This
		/// array must be of the same type as the one for which the particular
		/// implementation works with (which is returned by the getDataType()
		/// method).</p>
		/// 
		/// <p>The low-pass (high-pass) input signal can be interleaved with other
		/// signals in the same lowSig (highSig) array, and this is determined by
		/// the lowStep (highStep) argument. This means that the first sample of
		/// the low-pass (high-pass) input signal is lowSig[lowOff]
		/// (highSig[highOff]), the second is lowSig[lowOff+lowStep]
		/// (highSig[highOff+highStep]), the third is lowSig[lowOff+2*lowStep]
		/// (highSig[highOff+2*highStep]), and so on. Therefore if lowStep
		/// (highStep) is 1 there is no interleaving. This feature allows to filter
		/// columns of a 2-D signal, when it is stored in a line by line order in
		/// lowSig (highSig), without having to copy the data, in this case the
		/// lowStep (highStep) argument should be the line width of the low-pass
		/// (high-pass) signal.</p>
		/// 
		/// <p>The output signal is placed in the outSig array. The outOff and
		/// outStep arguments are analogous to the lowOff and lowStep ones, but
		/// they apply to the outSig array. The outSig array must be long enough to
		/// hold the low-pass output signal.</p>
		/// 
		/// </summary>
		/// <param name="lowSig">This is the array that contains the low-pass input
		/// signal. It must be of the correct type (e.g., it must be int[] if
		/// getDataType() returns TYPE_INT).
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="lowLen">This is the number of samples in the low-pass input
		/// signal to filter.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the low-pass
		/// input signal samples in the lowSig array. See above.
		/// 
		/// </param>
		/// <param name="highSig">This is the array that contains the high-pass input
		/// signal. It must be of the correct type (e.g., it must be int[] if
		/// getDataType() returns TYPE_INT).
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="highLen">This is the number of samples in the high-pass input
		/// signal to filter.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass input signal samples in the highSig array. See above.
		/// 
		/// </param>
		/// <param name="outSig">This is the array where the output signal is placed. It
		/// must be of the same type as lowSig and it should be long enough to
		/// contain the output signal.
		/// 
		/// </param>
		/// <param name="outOff">This is the index in outSig of the element where to put
		/// the first output sample.
		/// 
		/// </param>
		/// <param name="outStep">This is the step, or interleave factor, of the output
		/// samples in the outSig array. See above.
		/// 
		/// </param>
		public abstract void  synthetize_hpf(System.Object lowSig, int lowOff, int lowLen, int lowStep, System.Object highSig, int highOff, int highLen, int highStep, System.Object outSig, int outOff, int outStep);
		public abstract bool isSameAsFullWT(int param1, int param2, int param3);
	}
}