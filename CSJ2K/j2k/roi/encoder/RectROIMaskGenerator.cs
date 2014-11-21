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
	
	/// <summary> This class generates the ROI masks when there are only rectangular ROIs in
	/// the image. The ROI mask generation can then be simplified by only
	/// calculating the boundaries of the ROI mask in the particular subbands
	/// 
	/// <P>The values are calculated from the scaling factors of the ROIs. The
	/// values with which to scale are equal to u-umin where umin is the lowest
	/// scaling factor within the block. The umin value is sent to the entropy
	/// coder to be used for scaling the distortion values.
	/// 
	/// <P> To generate and to store the boundaries of the ROIs, the class
	/// SubbandRectROIMask is used. There is one tree of SubbandMasks for each
	/// component.
	/// 
	/// </summary>
	/// <seealso cref="SubbandRectROIMask">
	/// 
	/// </seealso>
	/// <seealso cref="ROIMaskGenerator">
	/// 
	/// </seealso>
	/// <seealso cref="ArbROIMaskGenerator">
	/// 
	/// </seealso>
	public class RectROIMaskGenerator:ROIMaskGenerator
	{
		
		/// <summary>The upper left xs of the ROIs</summary>
		private int[] ulxs;
		
		/// <summary>The upper left ys of the ROIs</summary>
		private int[] ulys;
		
		/// <summary>The lower right xs of the ROIs</summary>
		private int[] lrxs;
		
		/// <summary>The lower right ys of the ROIs</summary>
		private int[] lrys;
		
		/// <summary>Number of ROIs </summary>
		private int[] nrROIs;
		
		/// <summary>The tree of subbandmask. One for each component </summary>
		private SubbandRectROIMask[] sMasks;
		
		
		/// <summary> The constructor of the mask generator. The constructor is called with
		/// the ROI data. This data is stored in arrays that are used to generate
		/// the SubbandRectROIMask trees for each component.
		/// 
		/// </summary>
		/// <param name="ROIs">The ROI info.
		/// 
		/// </param>
		/// <param name="maxShift">The flag indicating use of Maxshift method.
		/// 
		/// </param>
		/// <param name="nrc">number of components.
		/// 
		/// </param>
		public RectROIMaskGenerator(ROI[] ROIs, int nrc):base(ROIs, nrc)
		{
			int nr = ROIs.Length;
			int r; // c removed
			nrROIs = new int[nrc];
			sMasks = new SubbandRectROIMask[nrc];
			
			// Count number of ROIs per component
			for (r = nr - 1; r >= 0; r--)
			{
				nrROIs[ROIs[r].comp]++;
			}
		}
		
		
		/// <summary> This functions gets a DataBlk the size of the current code-block and
		/// fills this block with the ROI mask.
		/// 
		/// <P> In order to get the mask for a particular Subband, the subband tree
		/// is traversed and at each decomposition, the ROI masks are computed. The
		/// roi bondaries for each subband are stored in the SubbandRectROIMask
		/// tree.
		/// 
		/// </summary>
		/// <param name="db">The data block that is to be filled with the mask
		/// 
		/// </param>
		/// <param name="sb">The root of the subband tree to which db belongs
		/// 
		/// </param>
		/// <param name="magbits">The max number of magnitude bits in any code-block
		/// 
		/// </param>
		/// <param name="c">The component for which to get the mask
		/// 
		/// </param>
		/// <returns> Whether or not a mask was needed for this tile
		/// 
		/// </returns>
		public override bool getROIMask(DataBlkInt db, Subband sb, int magbits, int c)
		{
			int x = db.ulx;
			int y = db.uly;
			int w = db.w;
			int h = db.h;
			int[] mask = db.DataInt;
            int i, j, k, r, maxk, maxj; // mink, minj removed
			int ulx = 0, uly = 0, lrx = 0, lry = 0;
			int wrap;
			int maxROI;
			int[] culxs;
			int[] culys;
			int[] clrxs;
			int[] clrys;
			SubbandRectROIMask srm;
			
			// If the ROI bounds have not been calculated for this tile and 
			// component, do so now.
			if (!tileMaskMade[c])
			{
				makeMask(sb, magbits, c);
				tileMaskMade[c] = true;
			}
			
			if (!roiInTile)
			{
				return false;
			}
			
			// Find relevant subband mask and get ROI bounds
			srm = (SubbandRectROIMask) sMasks[c].getSubbandRectROIMask(x, y);
			culxs = srm.ulxs;
			culys = srm.ulys;
			clrxs = srm.lrxs;
			clrys = srm.lrys;
			maxROI = culxs.Length - 1;
			// Make sure that only parts of ROIs within the code-block are used
			// and make the bounds local to this block the LR bounds are counted
			// as the distance from the lower right corner of the block
			x -= srm.ulx;
			y -= srm.uly;
			for (r = maxROI; r >= 0; r--)
			{
				ulx = culxs[r] - x;
				if (ulx < 0)
				{
					ulx = 0;
				}
				else if (ulx >= w)
				{
					ulx = w;
				}
				
				uly = culys[r] - y;
				if (uly < 0)
				{
					uly = 0;
				}
				else if (uly >= h)
				{
					uly = h;
				}
				
				lrx = clrxs[r] - x;
				if (lrx < 0)
				{
					lrx = - 1;
				}
				else if (lrx >= w)
				{
					lrx = w - 1;
				}
				
				lry = clrys[r] - y;
				if (lry < 0)
				{
					lry = - 1;
				}
				else if (lry >= h)
				{
					lry = h - 1;
				}
				
				// Add the masks of the ROI
				i = w * lry + lrx;
				maxj = (lrx - ulx);
				wrap = w - maxj - 1;
				maxk = lry - uly;
				
				for (k = maxk; k >= 0; k--)
				{
					for (j = maxj; j >= 0; j--, i--)
						mask[i] = magbits;
					i -= wrap;
				}
			}
			return true;
		}
		
		/// <summary> This function returns the relevant data of the mask generator
		/// 
		/// </summary>
		public override System.String ToString()
		{
			return ("Fast rectangular ROI mask generator");
		}
		
		/// <summary> This function generates the ROI mask for the entire tile. The mask is
		/// generated for one component. This method is called once for each tile
		/// and component.
		/// 
		/// </summary>
		/// <param name="sb">The root of the subband tree used in the decomposition
		/// 
		/// </param>
		/// <param name="n">component number
		/// 
		/// </param>
		public override void  makeMask(Subband sb, int magbits, int n)
		{
			int nr = nrROIs[n];
			int r;
			int ulx, uly, lrx, lry;
			int tileulx = sb.ulcx;
			int tileuly = sb.ulcy;
			int tilew = sb.w;
			int tileh = sb.h;
			ROI[] ROIs = roi_array; // local copy
			
			ulxs = new int[nr];
			ulys = new int[nr];
			lrxs = new int[nr];
			lrys = new int[nr];
			
			nr = 0;
			
			for (r = ROIs.Length - 1; r >= 0; r--)
			{
				if (ROIs[r].comp == n)
				{
					ulx = ROIs[r].ulx;
					uly = ROIs[r].uly;
					lrx = ROIs[r].w + ulx - 1;
					lry = ROIs[r].h + uly - 1;
					
					if (ulx > (tileulx + tilew - 1) || uly > (tileuly + tileh - 1) || lrx < tileulx || lry < tileuly)
					// no part of ROI in tile
						continue;
					
					// Check bounds
					ulx -= tileulx;
					lrx -= tileulx;
					uly -= tileuly;
					lry -= tileuly;
					
					ulx = (ulx < 0)?0:ulx;
					uly = (uly < 0)?0:uly;
					lrx = (lrx > (tilew - 1))?tilew - 1:lrx;
					lry = (lry > (tileh - 1))?tileh - 1:lry;
					
					ulxs[nr] = ulx;
					ulys[nr] = uly;
					lrxs[nr] = lrx;
					lrys[nr] = lry;
					nr++;
				}
			}
			if (nr == 0)
			{
				roiInTile = false;
			}
			else
			{
				roiInTile = true;
			}
			sMasks[n] = new SubbandRectROIMask(sb, ulxs, ulys, lrxs, lrys, nr);
		}
	}
}