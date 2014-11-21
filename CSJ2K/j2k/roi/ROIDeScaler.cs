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
using CSJ2K.j2k.quantization.dequantizer;
using CSJ2K.j2k.codestream.reader;
using CSJ2K.j2k.wavelet.synthesis;
using CSJ2K.j2k.codestream;
using CSJ2K.j2k.entropy;
using CSJ2K.j2k.decoder;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k.io;
using CSJ2K.j2k;
namespace CSJ2K.j2k.roi
{
	
	/// <summary> This class takes care of the de-scaling of ROI coefficients. The de-scaler
	/// works on a tile basis and any mask that is generated is for the current
	/// mask only
	/// 
	/// <p>Default implementations of the methods in 'MultiResImgData' are provided
	/// through the 'MultiResImgDataAdapter' abstract class.</p>
	/// 
	/// <p>Sign-magnitude representation is used (instead of two's complement) for
	/// the output data. The most significant bit is used for the sign (0 if
	/// positive, 1 if negative). Then the magnitude of the quantized coefficient
	/// is stored in the next most significat bits. The most significant magnitude
	/// bit corresponds to the most significant bit-plane and so on.</p>
	/// 
	/// </summary>
	public class ROIDeScaler:MultiResImgDataAdapter, CBlkQuantDataSrcDec
	{
		/// <summary> Returns the horizontal code-block partition origin. Allowable values
		/// are 0 and 1, nothing else.
		/// 
		/// </summary>
		virtual public int CbULX
		{
			get
			{
				return src.CbULX;
			}
			
		}
		/// <summary> Returns the vertical code-block partition origin. Allowable values are
		/// 0 and 1, nothing else.
		/// 
		/// </summary>
		virtual public int CbULY
		{
			get
			{
				return src.CbULY;
			}
			
		}
		/// <summary> Returns the parameters that are used in this class and implementing
		/// classes. It returns a 2D String array. Each of the 1D arrays is for a
		/// different option, and they have 3 elements. The first element is the
		/// option name, the second one is the synopsis and the third one is a long
		/// description of what the parameter is. The synopsis or description may
		/// be 'null', in which case it is assumed that there is no synopsis or
		/// description of the option, respectively. Null may be returned if no
		/// options are supported.
		/// 
		/// </summary>
		/// <returns> the options name, their synopsis and their explanation, or null
		/// if no options are supported.
		/// 
		/// </returns>
		public static System.String[][] ParameterInfo
		{
			get
			{
				return pinfo;
			}
			
		}
		
		/// <summary>The MaxShiftSpec containing the scaling values for all tile-components
		/// 
		/// </summary>
		private MaxShiftSpec mss;
		
		/// <summary>The prefix for ROI decoder options: 'R' </summary>
		public const char OPT_PREFIX = 'R';
		
		/// <summary>The list of parameters that is accepted by the entropy decoders. They
		/// start with 'R'. 
		/// </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'pinfo'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.String[][] pinfo = new System.String[][]{new System.String[]{"Rno_roi", null, "This argument makes sure that the no ROI de-scaling is performed. " + "Decompression is done like there is no ROI in the image", null}};
		
		/// <summary>The entropy decoder from where to get the compressed data (the source)
		/// 
		/// </summary>
		private CBlkQuantDataSrcDec src;
		
		/// <summary> Constructor of the ROI descaler, takes EntropyDEcoder as source of data
		/// to de-scale.
		/// 
		/// </summary>
		/// <param name="src">The EntropyDecoder that is the source of data.
		/// 
		/// </param>
		/// <param name="mss">The MaxShiftSpec containing the scaling values for all
		/// tile-components
		/// 
		/// </param>
		public ROIDeScaler(CBlkQuantDataSrcDec src, MaxShiftSpec mss):base(src)
		{
			this.src = src;
			this.mss = mss;
		}
		
		/// <summary> Returns the subband tree, for the specified tile-component. This method
		/// returns the root element of the subband tree structure, see Subband and
		/// SubbandSyn. The tree comprises all the available resolution levels.
		/// 
		/// <P>The number of magnitude bits ('magBits' member variable) for each
		/// subband is not initialized.
		/// 
		/// </summary>
		/// <param name="t">The index of the tile, from 0 to T-1.
		/// 
		/// </param>
		/// <param name="c">The index of the component, from 0 to C-1.
		/// 
		/// </param>
		/// <returns> The root of the tree structure.
		/// 
		/// </returns>
		public override SubbandSyn getSynSubbandTree(int t, int c)
		{
			return src.getSynSubbandTree(t, c);
		}
		
		/// <summary> Returns the specified code-block in the current tile for the specified
		/// component, as a copy (see below).
		/// 
		/// <p>The returned code-block may be progressive, which is indicated by
		/// the 'progressive' variable of the returned 'DataBlk' object. If a
		/// code-block is progressive it means that in a later request to this
		/// method for the same code-block it is possible to retrieve data which is
		/// a better approximation, since meanwhile more data to decode for the
		/// code-block could have been received. If the code-block is not
		/// progressive then later calls to this method for the same code-block
		/// will return the exact same data values.</p>
		/// 
		/// <p>The data returned by this method is always a copy of the internal
		/// data of this object, if any, and it can be modified "in place" without
		/// any problems after being returned. The 'offset' of the returned data is
		/// 0, and the 'scanw' is the same as the code-block width. See the
		/// 'DataBlk' class.</p>
		/// 
		/// <p>The 'ulx' and 'uly' members of the returned 'DataBlk' object contain
		/// the coordinates of the top-left corner of the block, with respect to
		/// the tile, not the subband.</p>
		/// 
		/// </summary>
		/// <param name="c">The component for which to return the next code-block.
		/// 
		/// </param>
		/// <param name="m">The vertical index of the code-block to return, in the
		/// specified subband.
		/// 
		/// </param>
		/// <param name="n">The horizontal index of the code-block to return, in the
		/// specified subband.
		/// 
		/// </param>
		/// <param name="sb">The subband in which the code-block to return is.
		/// 
		/// </param>
		/// <param name="cblk">If non-null this object will be used to return the new
		/// code-block. If null a new one will be allocated and returned. If the
		/// "data" array of the object is non-null it will be reused, if possible,
		/// to return the data.
		/// 
		/// </param>
		/// <returns> The next code-block in the current tile for component 'c', or
		/// null if all code-blocks for the current tile have been returned.
		/// 
		/// </returns>
		/// <seealso cref="DataBlk">
		/// 
		/// </seealso>
		public virtual DataBlk getCodeBlock(int c, int m, int n, SubbandSyn sb, DataBlk cblk)
		{
			return getInternCodeBlock(c, m, n, sb, cblk);
		}
		
		/// <summary> Returns the specified code-block in the current tile for the specified
		/// component (as a reference or copy).
		/// 
		/// <p>The returned code-block may be progressive, which is indicated by
		/// the 'progressive' variable of the returned 'DataBlk' object. If a
		/// code-block is progressive it means that in a later request to this
		/// method for the same code-block it is possible to retrieve data which is
		/// a better approximation, since meanwhile more data to decode for the
		/// code-block could have been received. If the code-block is not
		/// progressive then later calls to this method for the same code-block
		/// will return the exact same data values.</p>
		/// 
		/// <p>The data returned by this method can be the data in the internal
		/// buffer of this object, if any, and thus can not be modified by the
		/// caller. The 'offset' and 'scanw' of the returned data can be
		/// arbitrary. See the 'DataBlk' class.</p>
		/// 
		/// <p>The 'ulx' and 'uly' members of the returned 'DataBlk' object contain
		/// the coordinates of the top-left corner of the block, with respect to
		/// the tile, not the subband.</p>
		/// 
		/// </summary>
		/// <param name="c">The component for which to return the next code-block.
		/// 
		/// </param>
		/// <param name="m">The vertical index of the code-block to return, in the
		/// specified subband.
		/// 
		/// </param>
		/// <param name="n">The horizontal index of the code-block to return, in the
		/// specified subband.
		/// 
		/// </param>
		/// <param name="sb">The subband in which the code-block to return is.
		/// 
		/// </param>
		/// <param name="cblk">If non-null this object will be used to return the new
		/// code-block. If null a new one will be allocated and returned. If the
		/// "data" array of the object is non-null it will be reused, if possible,
		/// to return the data.
		/// 
		/// </param>
		/// <returns> The requested code-block in the current tile for component 'c'.
		/// 
		/// </returns>
		/// <seealso cref="DataBlk">
		/// 
		/// </seealso>
		public virtual DataBlk getInternCodeBlock(int c, int m, int n, SubbandSyn sb, DataBlk cblk)
		{
			int i, j, k, wrap; // mi removed
			int ulx, uly, w, h;
			int[] data; // local copy of quantized data
			int tmp;
			//int limit;
			
			// Get data block from entropy decoder
			cblk = src.getInternCodeBlock(c, m, n, sb, cblk);
			
			// If there are no ROIs in the tile, Or if we already got all blocks
			bool noRoiInTile = false;
			if (mss == null || mss.getTileCompVal(TileIdx, c) == null)
				noRoiInTile = true;
			
			if (noRoiInTile || cblk == null)
			{
				return cblk;
			}
			data = (int[]) cblk.Data;
			ulx = cblk.ulx;
			uly = cblk.uly;
			w = cblk.w;
			h = cblk.h;
			
			// Scale coefficients according to magnitude. If the magnitude of a
			// coefficient is lower than 2 pow 31-magbits then it is a background
			// coeff and should be up-scaled
			int boost = ((System.Int32) mss.getTileCompVal(TileIdx, c));
			int mask = ((1 << sb.magbits) - 1) << (31 - sb.magbits);
			int mask2 = (~ mask) & 0x7FFFFFFF;
			
			wrap = cblk.scanw - w;
			i = cblk.offset + cblk.scanw * (h - 1) + w - 1;
			for (j = h; j > 0; j--)
			{
				for (k = w; k > 0; k--, i--)
				{
					tmp = data[i];
					if ((tmp & mask) == 0)
					{
						// BG
						data[i] = (tmp & unchecked((int) 0x80000000)) | (tmp << boost);
					}
					else
					{
						// ROI
						if ((tmp & mask2) != 0)
						{
							// decoded more than magbits bit-planes, set
							// quantization mid-interval approx. bit just after
							// the magbits.
							data[i] = (tmp & (~ mask2)) | (1 << (30 - sb.magbits));
						}
					}
				}
				i -= wrap;
			}
			return cblk;
		}
		
		/// <summary> Creates a ROIDeScaler object. The information needed to create the
		/// object is the Entropy decoder used and the parameters.
		/// 
		/// </summary>
		/// <param name="src">The source of data that is to be descaled
		/// 
		/// </param>
		/// <param name="pl">The parameter list (or options).
		/// 
		/// </param>
		/// <param name="decSpec">The decoding specifications
		/// 
		/// </param>
		/// <exception cref="IllegalArgumentException">If an error occurs while parsing
		/// the options in 'pl'
		/// 
		/// </exception>
		public static ROIDeScaler createInstance(CBlkQuantDataSrcDec src, ParameterList pl, DecoderSpecs decSpec)
		{
			System.String noRoi;
			//int i;
			
			// Check parameters
			pl.checkList(OPT_PREFIX, CSJ2K.j2k.util.ParameterList.toNameArray(pinfo));
			
			// Check if no_roi specified in command line or no roi signalled
			// in bit stream
			noRoi = pl.getParameter("Rno_roi");
			if (noRoi != null || decSpec.rois == null)
			{
				// no_roi specified in commandline!
				return new ROIDeScaler(src, null);
			}
			
			return new ROIDeScaler(src, decSpec.rois);
		}
	}
}