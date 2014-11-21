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
using CSJ2K.j2k.wavelet.analysis;
using CSJ2K.j2k.quantization;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k.roi;
namespace CSJ2K.j2k.roi.encoder
{
	
	/// <summary> This class describes the ROI mask for a single subband. Each object of the
	/// class contains the mask for a particular subband and also has references to
	/// the masks of the children subbands of the subband corresponding to this
	/// mask. This class describes subband masks for images containing only
	/// rectangular ROIS
	/// 
	/// </summary>
	public class SubbandRectROIMask:SubbandROIMask
	{
		
		/// <summary>The upper left x coordinates of the applicable ROIs </summary>
		public int[] ulxs;
		
		/// <summary>The upper left y coordinates of the applicable ROIs </summary>
		public int[] ulys;
		
		/// <summary>The lower right x coordinates of the applicable ROIs </summary>
		public int[] lrxs;
		
		/// <summary>The lower right y coordinates of the applicable ROIs </summary>
		public int[] lrys;
		
		/// <summary> The constructor of the SubbandROIMask takes the dimensions of the
		/// subband as parameters. A tree of masks is generated from the subband
		/// sb. Each Subband contains the boundaries of each ROI.
		/// 
		/// </summary>
		/// <param name="sb">The subband corresponding to this Subband Mask
		/// 
		/// </param>
		/// <param name="ulxs">The upper left x coordinates of the ROIs
		/// 
		/// </param>
		/// <param name="ulys">The upper left y coordinates of the ROIs
		/// 
		/// </param>
		/// <param name="lrxs">The lower right x coordinates of the ROIs
		/// 
		/// </param>
		/// <param name="lrys">The lower right y coordinates of the ROIs
		/// 
		/// </param>
		/// <param name="lrys">The lower right y coordinates of the ROIs
		/// 
		/// </param>
		/// <param name="nr">Number of ROIs that affect this tile
		/// 
		/// </param>
		public SubbandRectROIMask(Subband sb, int[] ulxs, int[] ulys, int[] lrxs, int[] lrys, int nr):base(sb.ulx, sb.uly, sb.w, sb.h)
		{
			this.ulxs = ulxs;
			this.ulys = ulys;
			this.lrxs = lrxs;
			this.lrys = lrys;
			int r;
			
			if (sb.isNode)
			{
				isNode = true;
				// determine odd/even - high/low filters
				int horEvenLow = sb.ulcx % 2;
				int verEvenLow = sb.ulcy % 2;
				
				// Get filter support lengths
				WaveletFilter hFilter = sb.HorWFilter;
				WaveletFilter vFilter = sb.VerWFilter;
				int hlnSup = hFilter.SynLowNegSupport;
				int hhnSup = hFilter.SynHighNegSupport;
				int hlpSup = hFilter.SynLowPosSupport;
				int hhpSup = hFilter.SynHighPosSupport;
				int vlnSup = vFilter.SynLowNegSupport;
				int vhnSup = vFilter.SynHighNegSupport;
				int vlpSup = vFilter.SynLowPosSupport;
				int vhpSup = vFilter.SynHighPosSupport;
				
				// Generate arrays for children
				int x, y;
				int[] lulxs = new int[nr];
				int[] lulys = new int[nr];
				int[] llrxs = new int[nr];
				int[] llrys = new int[nr];
				int[] hulxs = new int[nr];
				int[] hulys = new int[nr];
				int[] hlrxs = new int[nr];
				int[] hlrys = new int[nr];
				for (r = nr - 1; r >= 0; r--)
				{
					// For all ROI calculate ...
					// Upper left x for all children
					x = ulxs[r];
					if (horEvenLow == 0)
					{
						lulxs[r] = (x + 1 - hlnSup) / 2;
						hulxs[r] = (x - hhnSup) / 2;
					}
					else
					{
						lulxs[r] = (x - hlnSup) / 2;
						hulxs[r] = (x + 1 - hhnSup) / 2;
					}
					// Upper left y for all children
					y = ulys[r];
					if (verEvenLow == 0)
					{
						lulys[r] = (y + 1 - vlnSup) / 2;
						hulys[r] = (y - vhnSup) / 2;
					}
					else
					{
						lulys[r] = (y - vlnSup) / 2;
						hulys[r] = (y + 1 - vhnSup) / 2;
					}
					// lower right x for all children
					x = lrxs[r];
					if (horEvenLow == 0)
					{
						llrxs[r] = (x + hlpSup) / 2;
						hlrxs[r] = (x - 1 + hhpSup) / 2;
					}
					else
					{
						llrxs[r] = (x - 1 + hlpSup) / 2;
						hlrxs[r] = (x + hhpSup) / 2;
					}
					// lower right y for all children
					y = lrys[r];
					if (verEvenLow == 0)
					{
						llrys[r] = (y + vlpSup) / 2;
						hlrys[r] = (y - 1 + vhpSup) / 2;
					}
					else
					{
						llrys[r] = (y - 1 + vlpSup) / 2;
						hlrys[r] = (y + vhpSup) / 2;
					}
				}
				// Create children
				hh = new SubbandRectROIMask(sb.HH, hulxs, hulys, hlrxs, hlrys, nr);
				lh = new SubbandRectROIMask(sb.LH, lulxs, hulys, llrxs, hlrys, nr);
				hl = new SubbandRectROIMask(sb.HL, hulxs, lulys, hlrxs, llrys, nr);
				ll = new SubbandRectROIMask(sb.LL, lulxs, lulys, llrxs, llrys, nr);
			}
		}
	}
}