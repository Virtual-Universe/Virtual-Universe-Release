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
using CSJ2K.j2k.image;
using CSJ2K.j2k.quantization;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.wavelet.analysis;
using CSJ2K.j2k.codestream.writer;
using CSJ2K.j2k.util;
using CSJ2K.j2k.roi;
namespace CSJ2K.j2k.roi.encoder
{
	
	/// <summary> This class generates the ROI masks for the ROIScaler.It gives the scaler
	/// the ROI mask for the current code-block.
	/// 
	/// <P>The values are calculated from the scaling factors of the ROIs. The
	/// values with which to scale are equal to u-umin where umin is the lowest
	/// scaling factor within the block. The umin value is sent to the entropy
	/// coder to be used for scaling the distortion values.
	/// 
	/// </summary>
	/// <seealso cref="RectROIMaskGenerator">
	/// 
	/// </seealso>
	/// <seealso cref="ArbROIMaskGenerator">
	/// 
	/// </seealso>
	public abstract class ROIMaskGenerator
	{
		/// <summary> This function returns the ROIs in the image
		/// 
		/// </summary>
		/// <returns> The ROIs in the image
		/// </returns>
		virtual public ROI[] ROIs
		{
			get
			{
				return roi_array;
			}
			
		}
		
		/// <summary>Array containing the ROIs </summary>
		protected internal ROI[] roi_array;
		
		/// <summary>Number of components </summary>
		protected internal int nrc;
		
		/// <summary>Flag indicating whether a mask has been made for the current tile </summary>
		protected internal bool[] tileMaskMade;
		
		/* Flag indicating whether there are any ROIs in this tile */
		protected internal bool roiInTile;
		
		/// <summary> The constructor of the mask generator
		/// 
		/// </summary>
		/// <param name="rois">The ROIs in the image
		/// 
		/// </param>
		/// <param name="nrc">The number of components
		/// </param>
		public ROIMaskGenerator(ROI[] rois, int nrc)
		{
			this.roi_array = rois;
			this.nrc = nrc;
			tileMaskMade = new bool[nrc];
		}
		
		/// <summary> This functions gets a DataBlk with the size of the current code-block
		/// and fills it with the ROI mask. The lowest scaling value in the mask
		/// for this code-block is returned by the function to be used for
		/// modifying the rate distortion estimations.
		/// 
		/// </summary>
		/// <param name="db">The data block that is to be filled with the mask
		/// 
		/// </param>
		/// <param name="sb">The root of the current subband tree
		/// 
		/// </param>
		/// <param name="magbits">The number of magnitude bits in this code-block
		/// 
		/// </param>
		/// <param name="c">Component number
		/// 
		/// </param>
		/// <returns> Whether or not a mask was needed for this tile 
		/// </returns>
		public abstract bool getROIMask(DataBlkInt db, Subband sb, int magbits, int c);
		
		/// <summary> This function generates the ROI mask for the entire tile. The mask is
		/// generated for one component. This method is called once for each tile
		/// and component.
		/// 
		/// </summary>
		/// <param name="sb">The root of the subband tree used in the decomposition
		/// 
		/// </param>
		/// <param name="magbits">The max number of magnitude bits in any code-block
		/// 
		/// </param>
		/// <param name="n">component number
		/// </param>
		public abstract void  makeMask(Subband sb, int magbits, int n);
		
		/// <summary> This function is called every time the tile is changed to indicate
		/// that there is need to make a new mask
		/// </summary>
		public virtual void  tileChanged()
		{
			for (int i = 0; i < nrc; i++)
				tileMaskMade[i] = false;
		}
	}
}