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
using CSJ2K.j2k.image;
namespace CSJ2K.j2k.wavelet.synthesis
{
	
	/// <summary> This extends the synthesis wavelet filter general definitions of
	/// SynWTFilter by adding methods that work for float data
	/// specifically. Implementations that work on float data should inherit
	/// from this class.
	/// 
	/// <P>See the SynWTFilter class for details such as
	/// normalization, how to split odd-length signals, etc.
	/// 
	/// <P>The advantage of using the specialized method is that no casts
	/// are performed.
	/// 
	/// </summary>
	/// <seealso cref="SynWTFilter">
	/// 
	/// </seealso>
	public abstract class SynWTFilterFloat:SynWTFilter
	{
		/// <summary> Returns the type of data on which this filter works, as defined
		/// in the DataBlk interface, which is always TYPE_FLOAT for this
		/// class.
		/// 
		/// </summary>
		/// <returns> The type of data as defined in the DataBlk interface.
		/// 
		/// </returns>
		/// <seealso cref="jj2000.j2k.image.DataBlk">
		/// 
		/// 
		/// 
		/// </seealso>
		override public int DataType
		{
			get
			{
				return DataBlk.TYPE_FLOAT;
			}
			
		}
		
		/// <summary> A specific version of the synthetize_lpf() method that works on float
		/// data. See the general description of the synthetize_lpf() method in the 
		/// SynWTFilter class for more details.
		/// 
		/// </summary>
		/// <param name="lowSig">This is the array that contains the low-pass
		/// input signal.
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="lowLen">This is the number of samples in the low-pass
		/// input signal to filter.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the
		/// low-pass input signal samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array that contains the high-pass
		/// input signal.
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="highLen">This is the number of samples in the high-pass
		/// input signal to filter.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass input signal samples in the highSig array.
		/// 
		/// </param>
		/// <param name="outSig">This is the array where the output signal is
		/// placed. It should be long enough to contain the output signal.
		/// 
		/// </param>
		/// <param name="outOff">This is the index in outSig of the element where
		/// to put the first output sample.
		/// 
		/// </param>
		/// <param name="outStep">This is the step, or interleave factor, of the
		/// output samples in the outSig array.
		/// 
		/// </param>
		/// <seealso cref="SynWTFilter.synthetize_lpf">
		/// 
		/// 
		/// 
		/// 
		/// 
		/// </seealso>
		public abstract void  synthetize_lpf(float[] lowSig, int lowOff, int lowLen, int lowStep, float[] highSig, int highOff, int highLen, int highStep, float[] outSig, int outOff, int outStep);
		
		/// <summary> The general version of the synthetize_lpf() method, it just calls
		/// the specialized version. See the description of the synthetize_lpf()
		/// method of the SynWTFilter class for more details.
		/// 
		/// </summary>
		/// <param name="lowSig">This is the array that contains the low-pass
		/// input signal. It must be an float[].
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="lowLen">This is the number of samples in the low-pass
		/// input signal to filter.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the
		/// low-pass input signal samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array that contains the high-pass
		/// input signal. It must be an float[].
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="highLen">This is the number of samples in the high-pass
		/// input signal to filter.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass input signal samples in the highSig array.
		/// 
		/// </param>
		/// <param name="outSig">This is the array where the output signal is
		/// placed. It should be and float[] and long enough to contain the
		/// output signal.
		/// 
		/// </param>
		/// <param name="outOff">This is the index in outSig of the element where
		/// to put the first output sample.
		/// 
		/// </param>
		/// <param name="outStep">This is the step, or interleave factor, of the
		/// output samples in the outSig array.
		/// 
		/// </param>
		/// <seealso cref="SynWTFilter.synthetize_hpf">
		/// 
		/// 
		/// 
		/// 
		/// 
		/// </seealso>
		public override void  synthetize_lpf(System.Object lowSig, int lowOff, int lowLen, int lowStep, System.Object highSig, int highOff, int highLen, int highStep, System.Object outSig, int outOff, int outStep)
		{
			
			synthetize_lpf((float[]) lowSig, lowOff, lowLen, lowStep, (float[]) highSig, highOff, highLen, highStep, (float[]) outSig, outOff, outStep);
		}
		
		/// <summary> A specific version of the synthetize_hpf() method that works on float
		/// data. See the general description of the synthetize_hpf() method in the 
		/// SynWTFilter class for more details.
		/// 
		/// </summary>
		/// <param name="lowSig">This is the array that contains the low-pass
		/// input signal.
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="lowLen">This is the number of samples in the low-pass
		/// input signal to filter.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the
		/// low-pass input signal samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array that contains the high-pass
		/// input signal.
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="highLen">This is the number of samples in the high-pass
		/// input signal to filter.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass input signal samples in the highSig array.
		/// 
		/// </param>
		/// <param name="outSig">This is the array where the output signal is
		/// placed. It should be long enough to contain the output signal.
		/// 
		/// </param>
		/// <param name="outOff">This is the index in outSig of the element where
		/// to put the first output sample.
		/// 
		/// </param>
		/// <param name="outStep">This is the step, or interleave factor, of the
		/// output samples in the outSig array.
		/// 
		/// </param>
		/// <seealso cref="SynWTFilter.synthetize_hpf">
		/// 
		/// 
		/// 
		/// 
		/// 
		/// </seealso>
		public abstract void  synthetize_hpf(float[] lowSig, int lowOff, int lowLen, int lowStep, float[] highSig, int highOff, int highLen, int highStep, float[] outSig, int outOff, int outStep);
		
		/// <summary> The general version of the synthetize_hpf() method, it just calls
		/// the specialized version. See the description of the synthetize_hpf()
		/// method of the SynWTFilter class for more details.
		/// 
		/// </summary>
		/// <param name="lowSig">This is the array that contains the low-pass
		/// input signal. It must be an float[].
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="lowLen">This is the number of samples in the low-pass
		/// input signal to filter.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the
		/// low-pass input signal samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array that contains the high-pass
		/// input signal. It must be an float[].
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="highLen">This is the number of samples in the high-pass
		/// input signal to filter.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass input signal samples in the highSig array.
		/// 
		/// </param>
		/// <param name="outSig">This is the array where the output signal is
		/// placed. It should be and float[] and long enough to contain the
		/// output signal.
		/// 
		/// </param>
		/// <param name="outOff">This is the index in outSig of the element where
		/// to put the first output sample.
		/// 
		/// </param>
		/// <param name="outStep">This is the step, or interleave factor, of the
		/// output samples in the outSig array.
		/// 
		/// </param>
		/// <seealso cref="SynWTFilter.synthetize_hpf">
		/// 
		/// 
		/// 
		/// 
		/// 
		/// </seealso>
		public override void  synthetize_hpf(System.Object lowSig, int lowOff, int lowLen, int lowStep, System.Object highSig, int highOff, int highLen, int highStep, System.Object outSig, int outOff, int outStep)
		{
			
			synthetize_hpf((float[]) lowSig, lowOff, lowLen, lowStep, (float[]) highSig, highOff, highLen, highStep, (float[]) outSig, outOff, outStep);
		}
	}
}