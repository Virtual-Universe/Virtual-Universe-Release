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
using CSJ2K.j2k;
namespace CSJ2K.j2k.wavelet.synthesis
{
	
	/// <summary> This class inherits from the synthesis wavelet filter definition for int
	/// data. It implements the inverse wavelet transform specifically for the 9x7
	/// filter. The implementation is based on the lifting scheme.
	/// 
	/// <P>See the SynWTFilter class for details such as normalization, how to
	/// split odd-length signals, etc. In particular, this method assumes that the
	/// low-pass coefficient is computed first.
	/// 
	/// </summary>
	/// <seealso cref="SynWTFilter">
	/// </seealso>
	/// <seealso cref="SynWTFilterFloat">
	/// 
	/// </seealso>
	public class SynWTFilterFloatLift9x7:SynWTFilterFloat
	{
		/// <summary> Returns the negative support of the low-pass analysis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> 2
		/// 
		/// </returns>
		override public int AnLowNegSupport
		{
			get
			{
				return 4;
			}
			
		}
		/// <summary> Returns the positive support of the low-pass analysis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the low-pass analysis filter in the
		/// positive direction
		/// 
		/// </returns>
		override public int AnLowPosSupport
		{
			get
			{
				return 4;
			}
			
		}
		/// <summary> Returns the negative support of the high-pass analysis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the high-pass analysis filter in
		/// the negative direction
		/// 
		/// </returns>
		override public int AnHighNegSupport
		{
			get
			{
				return 3;
			}
			
		}
		/// <summary> Returns the positive support of the high-pass analysis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the high-pass analysis filter in the
		/// positive direction
		/// 
		/// </returns>
		override public int AnHighPosSupport
		{
			get
			{
				return 3;
			}
			
		}
		/// <summary> Returns the negative support of the low-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// <P>A MORE PRECISE DEFINITION IS NEEDED
		/// 
		/// </summary>
		/// <returns> The number of taps of the low-pass synthesis filter in the
		/// negative direction
		/// 
		/// </returns>
		override public int SynLowNegSupport
		{
			get
			{
				return 3;
			}
			
		}
		/// <summary> Returns the positive support of the low-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// <P>A MORE PRECISE DEFINITION IS NEEDED
		/// 
		/// </summary>
		/// <returns> The number of taps of the low-pass synthesis filter in the
		/// positive direction
		/// 
		/// </returns>
		override public int SynLowPosSupport
		{
			get
			{
				return 3;
			}
			
		}
		/// <summary> Returns the negative support of the high-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// <P>A MORE PRECISE DEFINITION IS NEEDED
		/// 
		/// </summary>
		/// <returns> The number of taps of the high-pass synthesis filter in the
		/// negative direction
		/// 
		/// </returns>
		override public int SynHighNegSupport
		{
			get
			{
				return 4;
			}
			
		}
		/// <summary> Returns the positive support of the high-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// <P>A MORE PRECISE DEFINITION IS NEEDED
		/// 
		/// </summary>
		/// <returns> The number of taps of the high-pass synthesis filter in the
		/// positive direction
		/// 
		/// </returns>
		override public int SynHighPosSupport
		{
			get
			{
				return 4;
			}
			
		}
		/// <summary> Returns the implementation type of this filter, as defined in this
		/// class, such as WT_FILTER_INT_LIFT, WT_FILTER_FLOAT_LIFT,
		/// WT_FILTER_FLOAT_CONVOL.
		/// 
		/// </summary>
		/// <returns> WT_FILTER_INT_LIFT.
		/// 
		/// </returns>
		override public int ImplType
		{
			get
			{
				return CSJ2K.j2k.wavelet.WaveletFilter_Fields.WT_FILTER_FLOAT_LIFT;
			}
			
		}
		/// <summary> Returns the reversibility of the filter. A filter is considered
		/// reversible if it is suitable for lossless coding.
		/// 
		/// </summary>
		/// <returns> true since the 9x7 is reversible, provided the appropriate
		/// rounding is performed.
		/// 
		/// </returns>
		override public bool Reversible
		{
			get
			{
				return false;
			}
			
		}
		
		/// <summary>The value of the first lifting step coefficient </summary>
		public const float ALPHA = - 1.586134342f;
		
		/// <summary>The value of the second lifting step coefficient </summary>
		public const float BETA = - 0.05298011854f;
		
		/// <summary>The value of the third lifting step coefficient </summary>
		public const float GAMMA = 0.8829110762f;
		
		/// <summary>The value of the fourth lifting step coefficient </summary>
		public const float DELTA = 0.4435068522f;
		
		/// <summary>The value of the low-pass subband normalization factor </summary>
		public const float KL = 0.8128930655f;
		
		/// <summary>The value of the high-pass subband normalization factor </summary>
		public const float KH = 1.230174106f;
		
		/// <summary> An implementation of the synthetize_lpf() method that works on int
		/// data, for the inverse 9x7 wavelet transform using the lifting
		/// scheme. See the general description of the synthetize_lpf() method in
		/// the SynWTFilter class for more details.
		/// 
		/// <P>The low-pass and high-pass subbands are normalized by respectively a
		/// factor of 1/KL and a factor of 1/KH
		/// 
		/// <P>The coefficients of the first lifting step are [-DELTA 1 -DELTA]. 
		/// 
		/// <P>The coefficients of the second lifting step are [-GAMMA 1 -GAMMA].
		/// 
		/// <P>The coefficients of the third lifting step are [-BETA 1 -BETA]. 
		/// 
		/// <P>The coefficients of the fourth lifting step are [-ALPHA 1 -ALPHA].
		/// 
		/// </summary>
		/// <param name="lowSig">This is the array that contains the low-pass input
		/// signal.
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
		/// input signal samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array that contains the high-pass input
		/// signal.
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
		/// high-pass input signal samples in the highSig array.
		/// 
		/// </param>
		/// <param name="outSig">This is the array where the output signal is placed. It
		/// should be long enough to contain the output signal.
		/// 
		/// </param>
		/// <param name="outOff">This is the index in outSig of the element where to put
		/// the first output sample.
		/// 
		/// </param>
		/// <param name="outStep">This is the step, or interleave factor, of the output
		/// samples in the outSig array.
		/// 
		/// </param>
		/// <seealso cref="SynWTFilter.synthetize_lpf">
		/// 
		/// </seealso>
		public override void  synthetize_lpf(float[] lowSig, int lowOff, int lowLen, int lowStep, float[] highSig, int highOff, int highLen, int highStep, float[] outSig, int outOff, int outStep)
		{
			
			int i;
			int outLen = lowLen + highLen; //Length of the output signal
			int iStep = 2 * outStep; //Upsampling in outSig
			int ik; //Indexing outSig
			int lk; //Indexing lowSig
			int hk; //Indexing highSig
			
			// Generate intermediate low frequency subband
			//float sample = 0;
			
			//Initialize counters
			lk = lowOff;
			hk = highOff;
			ik = outOff;
			
			//Handle tail boundary effect. Use symmetric extension
			if (outLen > 1)
			{
				outSig[ik] = lowSig[lk] / KL - 2 * DELTA * highSig[hk] / KH;
			}
			else
			{
				outSig[ik] = lowSig[lk];
			}
			
			lk += lowStep;
			hk += highStep;
			ik += iStep;
			
			//Apply lifting step to each "inner" sample
			for (i = 2; i < outLen - 1; i += 2, ik += iStep, lk += lowStep, hk += highStep)
			{
				outSig[ik] = lowSig[lk] / KL - DELTA * (highSig[hk - highStep] + highSig[hk]) / KH;
			}
			
			//Handle head boundary effect if input signal has odd length
			if (outLen % 2 == 1)
			{
				if (outLen > 2)
				{
					outSig[ik] = lowSig[lk] / KL - 2 * DELTA * highSig[hk - highStep] / KH;
				}
			}
			
			// Generate intermediate high frequency subband
			
			//Initialize counters
			lk = lowOff;
			hk = highOff;
			ik = outOff + outStep;
			
			//Apply lifting step to each "inner" sample
			for (i = 1; i < outLen - 1; i += 2, ik += iStep, hk += highStep, lk += lowStep)
			{
				outSig[ik] = highSig[hk] / KH - GAMMA * (outSig[ik - outStep] + outSig[ik + outStep]);
			}
			
			//Handle head boundary effect if output signal has even length
			if (outLen % 2 == 0)
			{
				outSig[ik] = highSig[hk] / KH - 2 * GAMMA * outSig[ik - outStep];
			}
			
			// Generate even samples (inverse low-pass filter)
			
			//Initialize counters
			ik = outOff;
			
			//Handle tail boundary effect
			//If access the overlap then perform the lifting step.
			if (outLen > 1)
			{
				outSig[ik] -= 2 * BETA * outSig[ik + outStep];
			}
			ik += iStep;
			
			//Apply lifting step to each "inner" sample
			for (i = 2; i < outLen - 1; i += 2, ik += iStep)
			{
				outSig[ik] -= BETA * (outSig[ik - outStep] + outSig[ik + outStep]);
			}
			
			//Handle head boundary effect if input signal has odd length
			if (outLen % 2 == 1 && outLen > 2)
			{
				outSig[ik] -= 2 * BETA * outSig[ik - outStep];
			}
			
			// Generate odd samples (inverse high pass-filter)
			
			//Initialize counters
			ik = outOff + outStep;
			
			//Apply first lifting step to each "inner" sample
			for (i = 1; i < outLen - 1; i += 2, ik += iStep)
			{
				outSig[ik] -= ALPHA * (outSig[ik - outStep] + outSig[ik + outStep]);
			}
			
			//Handle head boundary effect if input signal has even length
			if (outLen % 2 == 0)
			{
				outSig[ik] -= 2 * ALPHA * outSig[ik - outStep];
			}
		}
		
		/// <summary> An implementation of the synthetize_hpf() method that works on int
		/// data, for the inverse 9x7 wavelet transform using the lifting
		/// scheme. See the general description of the synthetize_hpf() method in
		/// the SynWTFilter class for more details.
		/// 
		/// <P>The low-pass and high-pass subbands are normalized by respectively
		/// a factor of 1/KL and a factor of 1/KH   
		/// 
		/// <P>The coefficients of the first lifting step are [-DELTA 1 -DELTA]. 
		/// 
		/// <P>The coefficients of the second lifting step are [-GAMMA 1 -GAMMA].
		/// 
		/// <P>The coefficients of the third lifting step are [-BETA 1 -BETA]. 
		/// 
		/// <P>The coefficients of the fourth lifting step are [-ALPHA 1 -ALPHA].
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
		/// <param name="lowLen">This is the number of samples in the low-pass input
		/// signal to filter.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the low-pass
		/// input signal samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array that contains the high-pass input
		/// signal.
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
		/// high-pass input signal samples in the highSig array.
		/// 
		/// </param>
		/// <param name="outSig">This is the array where the output signal is placed. It
		/// should be long enough to contain the output signal.
		/// 
		/// </param>
		/// <param name="outOff">This is the index in outSig of the element where to put
		/// the first output sample.
		/// 
		/// </param>
		/// <param name="outStep">This is the step, or interleave factor, of the output
		/// samples in the outSig array.
		/// 
		/// </param>
		/// <seealso cref="SynWTFilter.synthetize_hpf">
		/// 
		/// </seealso>
		public override void  synthetize_hpf(float[] lowSig, int lowOff, int lowLen, int lowStep, float[] highSig, int highOff, int highLen, int highStep, float[] outSig, int outOff, int outStep)
		{
			
			int i;
			int outLen = lowLen + highLen; //Length of the output signal
			int iStep = 2 * outStep; //Upsampling in outSig
			int ik; //Indexing outSig
			int lk; //Indexing lowSig
			int hk; //Indexing highSig
			
			// Initialize counters
			lk = lowOff;
			hk = highOff;
			
			if (outLen != 1)
			{
				int outLen2 = outLen >> 1;
				// "Inverse normalize" each sample
				for (i = 0; i < outLen2; i++)
				{
					lowSig[lk] /= KL;
					highSig[hk] /= KH;
					lk += lowStep;
					hk += highStep;
				}
				// "Inverse normalise" last high pass coefficient
				if (outLen % 2 == 1)
				{
					highSig[hk] /= KH;
				}
			}
			else
			{
				// Normalize for Nyquist gain
				highSig[highOff] /= 2;
			}
			
			// Generate intermediate low frequency subband
			
			//Initialize counters
			lk = lowOff;
			hk = highOff;
			ik = outOff + outStep;
			
			//Apply lifting step to each "inner" sample
			for (i = 1; i < outLen - 1; i += 2)
			{
				outSig[ik] = lowSig[lk] - DELTA * (highSig[hk] + highSig[hk + highStep]);
				ik += iStep;
				lk += lowStep;
				hk += highStep;
			}
			
			if (outLen % 2 == 0 && outLen > 1)
			{
				//Use symmetric extension
				outSig[ik] = lowSig[lk] - 2 * DELTA * highSig[hk];
			}
			
			// Generate intermediate high frequency subband
			
			//Initialize counters
			hk = highOff;
			ik = outOff;
			
			if (outLen > 1)
			{
				outSig[ik] = highSig[hk] - 2 * GAMMA * outSig[ik + outStep];
			}
			else
			{
				outSig[ik] = highSig[hk];
			}
			
			ik += iStep;
			hk += highStep;
			
			//Apply lifting step to each "inner" sample
			for (i = 2; i < outLen - 1; i += 2)
			{
				outSig[ik] = highSig[hk] - GAMMA * (outSig[ik - outStep] + outSig[ik + outStep]);
				ik += iStep;
				hk += highStep;
			}
			
			//Handle head boundary effect if output signal has even length
			if (outLen % 2 == 1 && outLen > 1)
			{
				//Use symmetric extension
				outSig[ik] = highSig[hk] - 2 * GAMMA * outSig[ik - outStep];
			}
			
			// Generate even samples (inverse low-pass filter)
			
			//Initialize counters
			ik = outOff + outStep;
			
			//Apply lifting step to each "inner" sample
			for (i = 1; i < outLen - 1; i += 2)
			{
				outSig[ik] -= BETA * (outSig[ik - outStep] + outSig[ik + outStep]);
				ik += iStep;
			}
			
			if (outLen % 2 == 0 && outLen > 1)
			{
				// symmetric extension.
				outSig[ik] -= 2 * BETA * outSig[ik - outStep];
			}
			
			// Generate odd samples (inverse high pass-filter)
			
			//Initialize counters
			ik = outOff;
			
			if (outLen > 1)
			{
				// symmetric extension.
				outSig[ik] -= 2 * ALPHA * outSig[ik + outStep];
			}
			ik += iStep;
			
			//Apply first lifting step to each "inner" sample
			for (i = 2; i < outLen - 1; i += 2)
			{
				outSig[ik] -= ALPHA * (outSig[ik - outStep] + outSig[ik + outStep]);
				ik += iStep;
			}
			
			//Handle head boundary effect if input signal has even length
			if ((outLen % 2 == 1) && (outLen > 1))
			{
				//Use symmetric extension 
				outSig[ik] -= 2 * ALPHA * outSig[ik - outStep];
			}
		}
		
		/// <summary> Returns true if the wavelet filter computes or uses the
		/// same "inner" subband coefficient as the full frame wavelet transform,
		/// and false otherwise. In particular, for block based transforms with 
		/// reduced overlap, this method should return false. The term "inner"
		/// indicates that this applies only with respect to the coefficient that 
		/// are not affected by image boundaries processings such as symmetric
		/// extension, since there is not reference method for this.
		/// 
		/// <P>The result depends on the length of the allowed overlap when
		/// compared to the overlap required by the wavelet filter. It also
		/// depends on how overlap processing is implemented in the wavelet
		/// filter.
		/// 
		/// </summary>
		/// <param name="tailOvrlp">This is the number of samples in the input
		/// signal before the first sample to filter that can be used for
		/// overlap.
		/// 
		/// </param>
		/// <param name="headOvrlp">This is the number of samples in the input
		/// signal after the last sample to filter that can be used for
		/// overlap.
		/// 
		/// </param>
		/// <param name="inLen">This is the lenght of the input signal to filter.The
		/// required number of samples in the input signal after the last sample
		/// depends on the length of the input signal.
		/// 
		/// </param>
		/// <returns> true if both overlaps are greater than 2, and correct 
		/// processing is applied in the analyze() method.
		/// 
		/// 
		/// 
		/// </returns>
		public override bool isSameAsFullWT(int tailOvrlp, int headOvrlp, int inLen)
		{
			
			//If the input signal has even length.
			if (inLen % 2 == 0)
			{
				if (tailOvrlp >= 2 && headOvrlp >= 1)
					return true;
				else
					return false;
			}
			//Else if the input signal has odd length.
			else
			{
				if (tailOvrlp >= 2 && headOvrlp >= 2)
					return true;
				else
					return false;
			}
		}
		
		/// <summary> Returns a string of information about the synthesis wavelet filter
		/// 
		/// </summary>
		/// <returns> wavelet filter type.
		/// 
		/// 
		/// </returns>
		public override System.String ToString()
		{
			return "w9x7 (lifting)";
		}
	}
}