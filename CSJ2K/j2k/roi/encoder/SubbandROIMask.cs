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
	
	/// <summary> This abstract class describes the ROI mask for a single subband. Each
	/// object of the class contains the mask for a particular subband and also has
	/// references to the masks of the children subbands of the subband
	/// corresponding to this mask.  
	/// </summary>
	public abstract class SubbandROIMask
	{
		
		/// <summary>The subband masks of the child LL </summary>
		protected internal SubbandROIMask ll;
		
		/// <summary>The subband masks of the child LH </summary>
		protected internal SubbandROIMask lh;
		
		/// <summary>The subband masks of the child HL </summary>
		protected internal SubbandROIMask hl;
		
		/// <summary>The subband masks of the child HH </summary>
		protected internal SubbandROIMask hh;
		
		/// <summary>Flag indicating whether this subband mask is a node or not </summary>
		protected internal bool isNode;
		
		/// <summary>Horizontal uper-left coordinate of the subband mask </summary>
		public int ulx;
		
		/// <summary>Vertical uper-left coordinate of the subband mask </summary>
		public int uly;
		
		/// <summary>Width of the subband mask </summary>
		public int w;
		
		/// <summary>Height of the subband mask </summary>
		public int h;
		
		/// <summary> The constructor of the SubbandROIMask takes the dimensions of the
		/// subband as parameters
		/// 
		/// </summary>
		/// <param name="ulx">The upper left x coordinate of corresponding subband
		/// 
		/// </param>
		/// <param name="uly">The upper left y coordinate of corresponding subband
		/// 
		/// </param>
		/// <param name="w">The width of corresponding subband
		/// 
		/// </param>
		/// <param name="h">The height of corresponding subband
		/// 
		/// </param>
		public SubbandROIMask(int ulx, int uly, int w, int h)
		{
			this.ulx = ulx;
			this.uly = uly;
			this.w = w;
			this.h = h;
		}
		
		/// <summary> Returns a reference to the Subband mask element to which the specified
		/// point belongs. The specified point must be inside this (i.e. the one
		/// defined by this object) subband mask. This method searches through the
		/// tree.
		/// 
		/// </summary>
		/// <param name="x">horizontal coordinate of the specified point.
		/// 
		/// </param>
		/// <param name="y">horizontal coordinate of the specified point.
		/// 
		/// </param>
		public virtual SubbandROIMask getSubbandRectROIMask(int x, int y)
		{
			SubbandROIMask cur, hhs;
			
			// Check that we are inside this subband
			if (x < ulx || y < uly || x >= ulx + w || y >= uly + h)
			{
				throw new System.ArgumentException();
			}
			
			cur = this;
			while (cur.isNode)
			{
				hhs = cur.hh;
				// While we are still at a node -> continue
				if (x < hhs.ulx)
				{
					// Is the result of horizontal low-pass
					if (y < hhs.uly)
					{
						// Vertical low-pass
						cur = cur.ll;
					}
					else
					{
						// Vertical high-pass
						cur = cur.lh;
					}
				}
				else
				{
					// Is the result of horizontal high-pass
					if (y < hhs.uly)
					{
						// Vertical low-pass
						cur = cur.hl;
					}
					else
					{
						// Vertical high-pass
						cur = cur.hh;
					}
				}
			}
			return cur;
		}
	}
}