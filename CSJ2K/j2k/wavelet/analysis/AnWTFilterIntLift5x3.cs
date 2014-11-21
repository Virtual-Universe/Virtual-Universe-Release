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
using CSJ2K.j2k.codestream.writer;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.image;
using CSJ2K.j2k;
namespace CSJ2K.j2k.wavelet.analysis
{
	
	/// <summary> This class inherits from the analysis wavelet filter definition for int
	/// data. It implements the forward wavelet transform specifically for the 5x3
	/// filter. The implementation is based on the lifting scheme.
	/// 
	/// <p>See the AnWTFilter class for details such as normalization, how to split
	/// odd-length signals, etc. In particular, this method assumes that the
	/// low-pass coefficient is computed first.</p>
	/// 
	/// </summary>
	/// <seealso cref="AnWTFilter">
	/// </seealso>
	/// <seealso cref="AnWTFilterInt">
	/// 
	/// </seealso>
	public class AnWTFilterIntLift5x3:AnWTFilterInt
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
				return 2;
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
				return 2;
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
				return 1;
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
				return 1;
			}
			
		}
		/// <summary> Returns the negative support of the low-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
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
				return 1;
			}
			
		}
		/// <summary> Returns the positive support of the low-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the low-pass synthesis filter in
		/// the positive direction
		/// 
		/// </returns>
		override public int SynLowPosSupport
		{
			get
			{
				return 1;
			}
			
		}
		/// <summary> Returns the negative support of the high-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
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
				return 2;
			}
			
		}
		/// <summary> Returns the positive support of the high-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
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
				return 2;
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
				return CSJ2K.j2k.wavelet.WaveletFilter_Fields.WT_FILTER_INT_LIFT;
			}
			
		}
		/// <summary> Returns the reversibility of the filter. A filter is considered
		/// reversible if it is suitable for lossless coding.
		/// 
		/// </summary>
		/// <returns> true since the 5x3 is reversible, provided the appropriate
		/// rounding is performed.
		/// 
		/// </returns>
		override public bool Reversible
		{
			get
			{
				return true;
			}
			
		}
		/// <summary> Returns the type of filter used according to the FilterTypes interface
		/// (W5x3).
		/// 
		/// </summary>
		/// <seealso cref="FilterTypes">
		/// 
		/// </seealso>
		/// <returns> The filter type.
		/// 
		/// </returns>
		override public int FilterType
		{
			get
			{
				return CSJ2K.j2k.wavelet.FilterTypes_Fields.W5X3;
			}
			
		}
		
		/// <summary>The low-pass synthesis filter of the 5x3 wavelet transform </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'LPSynthesisFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly float[] LPSynthesisFilter = new float[]{0.5f, 1f, 0.5f};
		
		/// <summary>The high-pass synthesis filter of the 5x3 wavelet transform </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'HPSynthesisFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly float[] HPSynthesisFilter = new float[]{- 0.125f, - 0.25f, 0.75f, - 0.25f, - 0.125f};
		
		/// <summary> An implementation of the analyze_lpf() method that works on int data,
		/// for the forward 5x3 wavelet transform using the lifting scheme. See the
		/// general description of the analyze_lpf() method in the AnWTFilter class
		/// for more details.
		/// 
		/// <p>The coefficients of the first lifting step are [-1/2 1 -1/2].</p>
		/// 
		/// <p>The coefficients of the second lifting step are [1/4 1 1/4].</p>
		/// 
		/// </summary>
		/// <param name="inSig">This is the array that contains the input signal.
		/// 
		/// </param>
		/// <param name="inOff">This is the index in inSig of the first sample to filter.
		/// 
		/// </param>
		/// <param name="inLen">This is the number of samples in the input signal to
		/// filter.
		/// 
		/// </param>
		/// <param name="inStep">This is the step, or interleave factor, of the input
		/// signal samples in the inSig array.
		/// 
		/// </param>
		/// <param name="lowSig">This is the array where the low-pass output signal is
		/// placed.
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the element where to put
		/// the first low-pass output sample.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the low-pass
		/// output samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array where the high-pass output signal is
		/// placed.
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the element where to put
		/// the first high-pass output sample.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass output samples in the highSig array.
		/// 
		/// </param>
		public override void  analyze_lpf(int[] inSig, int inOff, int inLen, int inStep, int[] lowSig, int lowOff, int lowStep, int[] highSig, int highOff, int highStep)
		{
			int i;
			int iStep = 2 * inStep; //Subsampling in inSig
			int ik; //Indexing inSig
			int lk; //Indexing lowSig
			int hk; //Indexing highSig
			
			/* Generate high frequency subband */
			
			//Initialize counters
			ik = inOff + inStep;
			hk = highOff;
			
			//Apply first lifting step to each "inner" sample.
			for (i = 1; i < inLen - 1; i += 2)
			{
				highSig[hk] = inSig[ik] - ((inSig[ik - inStep] + inSig[ik + inStep]) >> 1);
				
				ik += iStep;
				hk += highStep;
			}
			
			//Handle head boundary effect if input signal has even length.
			if (inLen % 2 == 0)
			{
				highSig[hk] = inSig[ik] - ((2 * inSig[ik - inStep]) >> 1);
			}
			
			/* Generate low frequency subband */
			
			//Initialize counters
			ik = inOff;
			lk = lowOff;
			hk = highOff;
			
			if (inLen > 1)
			{
				lowSig[lk] = inSig[ik] + ((highSig[hk] + 1) >> 1);
			}
			else
			{
				lowSig[lk] = inSig[ik];
			}
			
			ik += iStep;
			lk += lowStep;
			hk += highStep;
			
			//Apply lifting step to each "inner" sample.
			for (i = 2; i < inLen - 1; i += 2)
			{
				lowSig[lk] = inSig[ik] + ((highSig[hk - highStep] + highSig[hk] + 2) >> 2);
				
				ik += iStep;
				lk += lowStep;
				hk += highStep;
			}
			
			//Handle head boundary effect if input signal has odd length.
			if (inLen % 2 == 1)
			{
				if (inLen > 2)
				{
					lowSig[lk] = inSig[ik] + ((2 * highSig[hk - highStep] + 2) >> 2);
				}
			}
		}
		
		/// <summary> An implementation of the analyze_hpf() method that works on int data,
		/// for the forward 5x3 wavelet transform using the lifting scheme. See the
		/// general description of the analyze_hpf() method in the AnWTFilter class
		/// for more details.
		/// 
		/// <P>The coefficients of the first lifting step are [-1/2 1 -1/2].</p>
		/// 
		/// <P>The coefficients of the second lifting step are [1/4 1 1/4].</p>
		/// 
		/// </summary>
		/// <param name="inSig">This is the array that contains the input signal.
		/// 
		/// </param>
		/// <param name="inOff">This is the index in inSig of the first sample to filter.
		/// 
		/// </param>
		/// <param name="inLen">This is the number of samples in the input signal to
		/// filter.
		/// 
		/// </param>
		/// <param name="inStep">This is the step, or interleave factor, of the input
		/// signal samples in the inSig array.
		/// 
		/// </param>
		/// <param name="lowSig">This is the array where the low-pass output signal is
		/// placed.
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the element where to put
		/// the first low-pass output sample.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the low-pass
		/// output samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array where the high-pass output signal is
		/// placed.
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the element where to put
		/// the first high-pass output sample.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass output samples in the highSig array.
		/// 
		/// </param>
		/// <seealso cref="AnWTFilter.analyze_hpf">
		/// 
		/// </seealso>
		public override void  analyze_hpf(int[] inSig, int inOff, int inLen, int inStep, int[] lowSig, int lowOff, int lowStep, int[] highSig, int highOff, int highStep)
		{
			int i;
			int iStep = 2 * inStep; //Subsampling in inSig
			int ik; //Indexing inSig
			int lk; //Indexing lowSig
			int hk; //Indexing highSig
			
			/* Generate high frequency subband */
			
			//Initialize counters
			ik = inOff;
			hk = highOff;
			
			if (inLen > 1)
			{
				// apply a symmetric extension.
				highSig[hk] = inSig[ik] - inSig[ik + inStep];
			}
			else
			{
				// Normalize for Nyquist gain
				highSig[hk] = inSig[ik] << 1;
			}
			
			ik += iStep;
			hk += highStep;
			
			//Apply first lifting step to each "inner" sample.
			if (inLen > 3)
			{
				for (i = 2; i < inLen - 1; i += 2)
				{
					highSig[hk] = inSig[ik] - ((inSig[ik - inStep] + inSig[ik + inStep]) >> 1);
					ik += iStep;
					hk += highStep;
				}
			}
			
			//If input signal has odd length then we perform the lifting step
			//i.e. apply a symmetric extension.
			if (inLen % 2 == 1 && inLen > 1)
			{
				highSig[hk] = inSig[ik] - inSig[ik - inStep];
			}
			
			/* Generate low frequency subband */
			
			//Initialize counters
			ik = inOff + inStep;
			lk = lowOff;
			hk = highOff;
			
			for (i = 1; i < inLen - 1; i += 2)
			{
				
				lowSig[lk] = inSig[ik] + ((highSig[hk] + highSig[hk + highStep] + 2) >> 2);
				
				ik += iStep;
				lk += lowStep;
				hk += highStep;
			}
			
			if (inLen > 1 && inLen % 2 == 0)
			{
				// apply a symmetric extension.
				lowSig[lk] = inSig[ik] + ((2 * highSig[hk] + 2) >> 2);
			}
		}
		
		/// <summary> Returns the time-reversed low-pass synthesis waveform of the filter,
		/// which is the low-pass filter. This is the time-reversed impulse
		/// response of the low-pass synthesis filter. It is used to calculate the
		/// L2-norm of the synthesis basis functions for a particular subband (also
		/// called energy weight).
		/// 
		/// <p>The returned array may not be modified (i.e. a reference to the
		/// internal array may be returned by the implementation of this
		/// method).</p>
		/// 
		/// </summary>
		/// <returns> The time-reversed low-pass synthesis waveform of the filter.
		/// 
		/// </returns>
		public override float[] getLPSynthesisFilter()
		{
			return LPSynthesisFilter;
		}
		
		/// <summary> Returns the time-reversed high-pass synthesis waveform of the filter,
		/// which is the high-pass filter. This is the time-reversed impulse
		/// response of the high-pass synthesis filter. It is used to calculate the
		/// L2-norm of the synthesis basis functions for a particular subband (also
		/// called energy weight).
		/// 
		/// <p>The returned array may not be modified (i.e. a reference to the
		/// internal array may be returned by the implementation of this
		/// method).</p>
		/// 
		/// </summary>
		/// <returns> The time-reversed high-pass synthesis waveform of the filter.
		/// 
		/// </returns>
		public override float[] getHPSynthesisFilter()
		{
			return HPSynthesisFilter;
		}
		
		/// <summary> Returns true if the wavelet filter computes or uses the same "inner"
		/// subband coefficient as the full frame wavelet transform, and false
		/// otherwise. In particular, for block based transforms with reduced
		/// overlap, this method should return false. The term "inner" indicates
		/// that this applies only with respect to the coefficient that are not
		/// affected by image boundaries processings such as symmetric extension,
		/// since there is not reference method for this.
		/// 
		/// <p>The result depends on the length of the allowed overlap when
		/// compared to the overlap required by the wavelet filter. It also depends
		/// on how overlap processing is implemented in the wavelet filter.</p>
		/// 
		/// </summary>
		/// <param name="tailOvrlp">This is the number of samples in the input signal
		/// before the first sample to filter that can be used for overlap.
		/// 
		/// </param>
		/// <param name="headOvrlp">This is the number of samples in the input signal
		/// after the last sample to filter that can be used for overlap.
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
		
		/// <summary> Tests if the 'obj' object is the same filter as this one. Two filters
		/// are the same if the same filter code should be output for both filters
		/// by the encodeFilterCode() method.
		/// 
		/// <p>Currently the implementation of this method only tests if 'obj' is
		/// also of the class AnWTFilterIntLift5x3.</p>
		/// 
		/// </summary>
		/// <param name="The">object against which to test inequality.
		/// 
		/// </param>
		public  override bool Equals(System.Object obj)
		{
			// To speed up test, first test for reference equality
			return obj == this || obj is AnWTFilterIntLift5x3;
		}
		
		/// <summary>Debugging method </summary>
		public override System.String ToString()
		{
			return "w5x3";
		}
		//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}