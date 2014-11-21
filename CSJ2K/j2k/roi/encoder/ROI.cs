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
using CSJ2K.j2k.image.input;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k.roi;
namespace CSJ2K.j2k.roi.encoder
{
	
	/// <summary> This class contains the shape of a single ROI. In the current 
	/// implementation only rectangles and circles are supported.
	/// 
	/// </summary>
	/// <seealso cref="ROIMaskGenerator">
	/// </seealso>
	public class ROI
	{
		
		/// <summary>ImgReaderPGM object with the arbrtrary ROI </summary>
		public ImgReaderPGM maskPGM = null;
		
		/// <summary>Where or not the ROI shape is arbitrary </summary>
		public bool arbShape;
		
		/// <summary>Flag indicating whether the ROI is rectangular or not </summary>
		public bool rect;
		
		/// <summary>The components for which the ROI is relevant </summary>
		public int comp;
		
		/// <summary>x coordinate of upper left corner of rectangular ROI </summary>
		public int ulx;
		
		/// <summary>y coordinate of upper left corner of rectangular ROI </summary>
		public int uly;
		
		/// <summary>width of rectangular ROI  </summary>
		public int w;
		
		/// <summary>height of rectangular ROI </summary>
		public int h;
		
		/// <summary>x coordinate of center of circular ROI </summary>
		public int x;
		
		/// <summary>y coordinate of center of circular ROI </summary>
		public int y;
		
		/// <summary>radius of circular ROI  </summary>
		public int r;
		
		
		/// <summary> Constructor for ROI with arbitrary shape
		/// 
		/// </summary>
		/// <param name="comp">The component the ROI belongs to
		/// 
		/// </param>
		/// <param name="maskPGM">ImgReaderPGM containing the ROI
		/// </param>
		public ROI(int comp, ImgReaderPGM maskPGM)
		{
			arbShape = true;
			rect = false;
			this.comp = comp;
			this.maskPGM = maskPGM;
		}
		
		/// <summary> Constructor for rectangular ROIs
		/// 
		/// </summary>
		/// <param name="comp">The component the ROI belongs to
		/// 
		/// </param>
		/// <param name="x">x-coordinate of upper left corner of ROI
		/// 
		/// </param>
		/// <param name="y">y-coordinate of upper left corner of ROI
		/// 
		/// </param>
		/// <param name="w">width of ROI
		/// 
		/// </param>
		/// <param name="h">height of ROI
		/// </param>
		public ROI(int comp, int ulx, int uly, int w, int h)
		{
			arbShape = false;
			this.comp = comp;
			this.ulx = ulx;
			this.uly = uly;
			this.w = w;
			this.h = h;
			rect = true;
		}
		
		/// <summary> Constructor for circular ROIs
		/// 
		/// </summary>
		/// <param name="comp">The component the ROI belongs to
		/// 
		/// </param>
		/// <param name="x">x-coordinate of center of ROI
		/// 
		/// </param>
		/// <param name="y">y-coordinate of center of ROI
		/// 
		/// </param>
		/// <param name="w">radius of ROI
		/// </param>
		public ROI(int comp, int x, int y, int rad)
		{
			arbShape = false;
			this.comp = comp;
			this.x = x;
			this.y = y;
			this.r = rad;
		}
		
		/// <summary> This function prints all relevant data for the ROI</summary>
		public override System.String ToString()
		{
			if (arbShape)
			{
				return "ROI with arbitrary shape, PGM file= " + maskPGM;
			}
			else if (rect)
				return "Rectangular ROI, comp=" + comp + " ulx=" + ulx + " uly=" + uly + " w=" + w + " h=" + h;
			else
				return "Circular ROI,  comp=" + comp + " x=" + x + " y=" + y + " radius=" + r;
		}
	}
}