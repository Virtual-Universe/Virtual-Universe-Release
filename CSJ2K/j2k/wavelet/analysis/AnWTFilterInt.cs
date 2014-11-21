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
namespace CSJ2K.j2k.wavelet.analysis
{
	
	/// <summary> This extends the analysis wavelet filter general definitions of
	/// AnWTFilter by adding methods that work for int data
	/// specifically. Implementations that work on int data should inherit
	/// from this class.
	/// 
	/// <P>See the AnWTFilter class for details such as
	/// normalization, how to split odd-length signals, etc.
	/// 
	/// <P>The advantage of using the specialized method is that no casts
	/// are performed.
	/// 
	/// </summary>
	/// <seealso cref="AnWTFilter">
	/// 
	/// </seealso>
	public abstract class AnWTFilterInt:AnWTFilter
	{
		/// <summary> Returns the type of data on which this filter works, as defined
		/// in the DataBlk interface, which is always TYPE_INT for this
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
				return DataBlk.TYPE_INT;
			}
			
		}
		
		/// <summary> A specific version of the analyze_lpf() method that works on int
		/// data. See the general description of the analyze_lpf() method in
		/// the AnWTFilter class for more details.
		/// 
		/// </summary>
		/// <param name="inSig">This is the array that contains the input
		/// signal.
		/// 
		/// </param>
		/// <param name="inOff">This is the index in inSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="inLen">This is the number of samples in the input signal
		/// to filter.
		/// 
		/// </param>
		/// <param name="inStep">This is the step, or interleave factor, of the
		/// input signal samples in the inSig array.
		/// 
		/// </param>
		/// <param name="lowSig">This is the array where the low-pass output
		/// signal is placed.
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the element where
		/// to put the first low-pass output sample.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the
		/// low-pass output samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array where the high-pass output
		/// signal is placed.
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the element where
		/// to put the first high-pass output sample.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass output samples in the highSig array.
		/// 
		/// </param>
		/// <seealso cref="AnWTFilter.analyze_lpf">
		/// 
		/// 
		/// 
		/// 
		/// 
		/// </seealso>
		public abstract void  analyze_lpf(int[] inSig, int inOff, int inLen, int inStep, int[] lowSig, int lowOff, int lowStep, int[] highSig, int highOff, int highStep);
		
		/// <summary> The general version of the analyze_lpf() method, it just calls the
		/// specialized version. See the description of the analyze_lpf()
		/// method of the AnWTFilter class for more details.
		/// 
		/// </summary>
		/// <param name="inSig">This is the array that contains the input
		/// signal. It must be an int[].
		/// 
		/// </param>
		/// <param name="inOff">This is the index in inSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="inLen">This is the number of samples in the input signal
		/// to filter.
		/// 
		/// </param>
		/// <param name="inStep">This is the step, or interleave factor, of the
		/// input signal samples in the inSig array.
		/// 
		/// </param>
		/// <param name="lowSig">This is the array where the low-pass output
		/// signal is placed. It must be an int[].
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the element where
		/// to put the first low-pass output sample.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the
		/// low-pass output samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array where the high-pass output
		/// signal is placed. It must be an int[].
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the element where
		/// to put the first high-pass output sample.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass output samples in the highSig array.
		/// 
		/// </param>
		/// <seealso cref="AnWTFilter.analyze_lpf">
		/// 
		/// 
		/// 
		/// 
		/// 
		/// </seealso>
		
		public override void  analyze_lpf(System.Object inSig, int inOff, int inLen, int inStep, System.Object lowSig, int lowOff, int lowStep, System.Object highSig, int highOff, int highStep)
		{
			
			analyze_lpf((int[]) inSig, inOff, inLen, inStep, (int[]) lowSig, lowOff, lowStep, (int[]) highSig, highOff, highStep);
		}
		
		/// <summary> A specific version of the analyze_hpf() method that works on int
		/// data. See the general description of the analyze_hpf() method in
		/// the AnWTFilter class for more details.
		/// 
		/// </summary>
		/// <param name="inSig">This is the array that contains the input
		/// signal.
		/// 
		/// </param>
		/// <param name="inOff">This is the index in inSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="inLen">This is the number of samples in the input signal
		/// to filter.
		/// 
		/// </param>
		/// <param name="inStep">This is the step, or interleave factor, of the
		/// input signal samples in the inSig array.
		/// 
		/// </param>
		/// <param name="lowSig">This is the array where the low-pass output
		/// signal is placed.
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the element where
		/// to put the first low-pass output sample.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the
		/// low-pass output samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array where the high-pass output
		/// signal is placed.
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the element where
		/// to put the first high-pass output sample.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass output samples in the highSig array.
		/// 
		/// </param>
		/// <seealso cref="AnWTFilter.analyze_hpf">
		/// 
		/// 
		/// 
		/// 
		/// 
		/// </seealso>
		public abstract void  analyze_hpf(int[] inSig, int inOff, int inLen, int inStep, int[] lowSig, int lowOff, int lowStep, int[] highSig, int highOff, int highStep);
		/// <summary> The general version of the analyze_hpf() method, it just calls the
		/// specialized version. See the description of the analyze_hpf()
		/// method of the AnWTFilter class for more details.
		/// 
		/// </summary>
		/// <param name="inSig">This is the array that contains the input
		/// signal. It must be an int[].
		/// 
		/// </param>
		/// <param name="inOff">This is the index in inSig of the first sample to
		/// filter.
		/// 
		/// </param>
		/// <param name="inLen">This is the number of samples in the input signal
		/// to filter.
		/// 
		/// </param>
		/// <param name="inStep">This is the step, or interleave factor, of the
		/// input signal samples in the inSig array.
		/// 
		/// </param>
		/// <param name="lowSig">This is the array where the low-pass output
		/// signal is placed. It must be an int[].
		/// 
		/// </param>
		/// <param name="lowOff">This is the index in lowSig of the element where
		/// to put the first low-pass output sample.
		/// 
		/// </param>
		/// <param name="lowStep">This is the step, or interleave factor, of the
		/// low-pass output samples in the lowSig array.
		/// 
		/// </param>
		/// <param name="highSig">This is the array where the high-pass output
		/// signal is placed. It must be an int[].
		/// 
		/// </param>
		/// <param name="highOff">This is the index in highSig of the element where
		/// to put the first high-pass output sample.
		/// 
		/// </param>
		/// <param name="highStep">This is the step, or interleave factor, of the
		/// high-pass output samples in the highSig array.
		/// 
		/// </param>
		/// <seealso cref="AnWTFilter.analyze_hpf">
		/// 
		/// 
		/// 
		/// 
		/// 
		/// </seealso>
		
		public override void  analyze_hpf(System.Object inSig, int inOff, int inLen, int inStep, System.Object lowSig, int lowOff, int lowStep, System.Object highSig, int highOff, int highStep)
		{
			
			analyze_hpf((int[]) inSig, inOff, inLen, inStep, (int[]) lowSig, lowOff, lowStep, (int[]) highSig, highOff, highStep);
		}
	}
}