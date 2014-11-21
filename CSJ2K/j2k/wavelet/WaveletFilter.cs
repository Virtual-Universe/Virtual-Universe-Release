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
namespace CSJ2K.j2k.wavelet
{
	
	/// <summary> This interface defines how a wavelet filter implementation should present
	/// itself. This interface defines only the commonalities between the analysis
	/// and synthesis filters. The AnWTFilter and SynWTFilter classes provide the
	/// specifics of analysis and synthesis filters.
	/// 
	/// <p>Both analysis and filters must be able to return the extent of the
	/// negative and positive support for both synthesis and analysis sides. This
	/// simplifies the sue of some functionalities that need extra information
	/// about the filters.</p>
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.wavelet.analysis.AnWTFilter">
	/// 
	/// </seealso>
	/// <seealso cref="jj2000.j2k.wavelet.synthesis.SynWTFilter">
	/// 
	/// </seealso>
	public struct WaveletFilter_Fields{
		/// <summary>The ID for integer lifting spteps implementations </summary>
		public readonly static int WT_FILTER_INT_LIFT = 0;
		/// <summary>The ID for floating-point lifting spteps implementations </summary>
		public readonly static int WT_FILTER_FLOAT_LIFT = 1;
		/// <summary>The ID for floatring-poitn convolution implementations </summary>
		public readonly static int WT_FILTER_FLOAT_CONVOL = 2;
	}
	public interface WaveletFilter
	{
		//UPGRADE_NOTE: Members of interface 'WaveletFilter' were extracted into structure 'WaveletFilter_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		/// <summary> Returns the negative support of the low-pass analysis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the low-pass analysis filter in the
		/// negative direction 
		/// </returns>
		int AnLowNegSupport
		{
			get;
			
		}
		/// <summary> Returns the positive support of the low-pass analysis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the low-pass analysis filter in the
		/// positive direction 
		/// </returns>
		int AnLowPosSupport
		{
			get;
			
		}
		/// <summary> Returns the negative support of the high-pass analysis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the high-pass analysis filter in the
		/// negative direction 
		/// </returns>
		int AnHighNegSupport
		{
			get;
			
		}
		/// <summary> Returns the positive support of the high-pass analysis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the high-pass analysis filter in
		/// the positive direction 
		/// </returns>
		int AnHighPosSupport
		{
			get;
			
		}
		/// <summary> Returns the negative support of the low-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the low-pass synthesis filter in the
		/// negative direction 
		/// </returns>
		int SynLowNegSupport
		{
			get;
			
		}
		/// <summary> Returns the positive support of the low-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the low-pass synthesis filter in the
		/// positive direction 
		/// </returns>
		int SynLowPosSupport
		{
			get;
			
		}
		/// <summary> Returns the negative support of the high-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the high-pass synthesis filter in the
		/// negative direction 
		/// </returns>
		int SynHighNegSupport
		{
			get;
			
		}
		/// <summary> Returns the positive support of the high-pass synthesis filter. That is
		/// the number of taps of the filter in the negative direction.
		/// 
		/// </summary>
		/// <returns> The number of taps of the high-pass synthesis filter in the
		/// positive direction 
		/// </returns>
		int SynHighPosSupport
		{
			get;
			
		}
		/// <summary> Returns the implementation type of this filter, as defined in this
		/// class, such as WT_FILTER_INT_LIFT, WT_FILTER_FLOAT_LIFT,
		/// WT_FILTER_FLOAT_CONVOL.
		/// 
		/// </summary>
		/// <returns> The implementation type of this filter: WT_FILTER_INT_LIFT,
		/// WT_FILTER_FLOAT_LIFT, WT_FILTER_FLOAT_CONVOL.  
		/// </returns>
		int ImplType
		{
			get;
			
		}
		/// <summary> Returns the type of data on which this filter works, as defined in the
		/// DataBlk interface.
		/// 
		/// </summary>
		/// <returns> The type of data as defined in the DataBlk interface.
		/// 
		/// </returns>
		/// <seealso cref="jj2000.j2k.image.DataBlk">
		/// </seealso>
		int DataType
		{
			get;
			
		}
		/// <summary> Returns the reversibility of the filter. A filter is considered
		/// reversible if it is suitable for lossless coding.
		/// 
		/// </summary>
		/// <returns> true if the filter is reversible, false otherwise.
		/// 
		/// </returns>
		bool Reversible
		{
			get;
			
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
		/// <returns> true if the overlaps are large enough and correct processing is
		/// performed, false otherwise.
		/// 
		/// </returns>
		bool isSameAsFullWT(int tailOvrlp, int headOvrlp, int inLen);
	}
}