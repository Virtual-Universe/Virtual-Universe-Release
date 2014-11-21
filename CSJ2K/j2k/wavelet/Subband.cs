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
namespace CSJ2K.j2k.wavelet
{
	
	/// <summary> This abstract class represents a subband in a bidirectional tree structure
	/// that describes the subband decomposition for a wavelet transform. This
	/// class is implemented by the SubbandAn and SubbandSyn classes, which are for
	/// the analysis and synthesis sides respectively.
	/// 
	/// <p>The element can be either a node or a leaf of the tree. If it is a node,
	/// it has 4 descendants (LL, HL, LH and HH). If it is a leaf, it has no
	/// descendant.</p>
	/// 
	/// <p>The tree is bidirectional. Each element in the tree structure has a
	/// "parent", which is the subband from which the element was obtained by
	/// decomposition. The only exception is the root element which, for obvious
	/// reasons, has no parent (i.e. it is null).</p>
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.wavelet.analysis.SubbandAn">
	/// </seealso>
	/// <seealso cref="jj2000.j2k.wavelet.synthesis.SubbandSyn">
	/// 
	/// </seealso>
	public abstract class Subband
	{
		/// <summary> Returns the parent of this subband. The parent of a subband is the
		/// subband from which this one was obtained by decomposition. The root
		/// element has no parent subband (null).
		/// 
		/// </summary>
		/// <returns> The parent subband, or null for the root one.
		/// 
		/// </returns>
		public abstract Subband Parent{get;}
		/// <summary> Returns the LL child subband of this subband.
		/// 
		/// </summary>
		/// <returns> The LL child subband, or null if there are no childs.
		/// 
		/// </returns>
		public abstract Subband LL{get;}
		/// <summary> Returns the HL (horizontal high-pass) child subband of this subband.
		/// 
		/// </summary>
		/// <returns> The HL child subband, or null if there are no childs.
		/// 
		/// </returns>
		public abstract Subband HL{get;}
		/// <summary> Returns the LH (vertical high-pass) child subband of this subband.
		/// 
		/// </summary>
		/// <returns> The LH child subband, or null if there are no childs.
		/// 
		/// </returns>
		public abstract Subband LH{get;}
		/// <summary> Returns the HH child subband of this subband.
		/// 
		/// </summary>
		/// <returns> The HH child subband, or null if there are no childs.
		/// 
		/// </returns>
		public abstract Subband HH{get;}
		/// <summary> Returns the first leaf subband element in the next higher resolution
		/// level.
		/// 
		/// </summary>
		/// <returns> The first leaf element in the next higher resolution level, or
		/// null if there is no higher resolution level.
		/// 
		/// </returns>
		virtual public Subband NextResLevel
		{
			get
			{
				Subband sb;
				
				if (level == 0)
				{
					// No higher res. level
					return null;
				}
				// Go up until we get to a different resolution level
				sb = this;
				do 
				{
					sb = sb.Parent;
					if (sb == null)
					{
						// No higher resolution level
						return null;
					}
				}
				while (sb.resLvl == resLvl);
				// Now go down to HL, which is in next higher resolution level
				sb = sb.HL;
				// Now go down LL until get to a leaf
				while (sb.isNode)
				{
					sb = sb.LL;
				}
				return sb;
			}
			
		}
		/// <summary> This function returns the horizontal wavelet filter relevant to this
		/// subband
		/// 
		/// </summary>
		/// <returns> The horizontal wavelet filter
		/// 
		/// </returns>
		public abstract WaveletFilter HorWFilter{get;}
		/// <summary> This function returns the vertical wavelet filter relevant to this
		/// subband
		/// 
		/// </summary>
		/// <returns> The vertical wavelet filter
		/// 
		/// </returns>
		public abstract WaveletFilter VerWFilter{get;}
		
		/// <summary>The ID for the LL orientation </summary>
		public const int WT_ORIENT_LL = 0;
		
		/// <summary>The ID for the HL (horizontal high-pass) orientation </summary>
		public const int WT_ORIENT_HL = 1;
		
		/// <summary>The ID for the LH (vertical high-pass) orientation </summary>
		public const int WT_ORIENT_LH = 2;
		
		/// <summary>The ID for the HH orientation </summary>
		public const int WT_ORIENT_HH = 3;
		
		/// <summary>True if it is a node in the tree, false if it is a leaf. </summary>
		public bool isNode;
		
		/// <summary>The orientation of this subband (WT_ORIENT_LL, WT_ORIENT_HL,
		/// WT_ORIENT_LH, WT_ORIENT_HH). 
		/// </summary>
		public int orientation;
		
		/// <summary>The level in the tree to which this subband belongs, which is the
		/// number of performed wavelet decompositions to get this subband. It is 0
		/// for the top-level (i.e. root) node. 
		/// </summary>
		public int level;
		
		/// <summary>The resolution level to which this subband contributes. 0 is the
		/// smallest resolution level (the one with the lowest frequency LL
		/// subband). 
		/// </summary>
		public int resLvl;
		
		/// <summary>The number of code-blocks (in both directions) contained in this
		/// subband.  
		/// </summary>
		public Coord numCb = null;
		
		/// <summary> The base 2 exponent of the analysis gain of the subband. The analysis
		/// gain of a subband is defined as the gain of the previous subband
		/// (i.e. the one from which this one was obtained) multiplied by the line
		/// gain and by the column gain. The line (column) gain is the gain of the
		/// line (column) filter that was used to obtain it, which is the DC gain
		/// for a low-pass filter and the Nyquist gain for a high-pass filter. It
		/// is 0 by default.
		/// 
		/// <p>Using the base 2 exponent of the value contrains the possible gains
		/// to powers of 2. However this is perfectly compatible to the filter
		/// normalization policy assumed here. See the split() method for more
		/// details.</p>
		/// 
		/// </summary>
		/// <seealso cref="split">
		/// 
		/// </seealso>
		public int anGainExp;
		
		/// <summary> The subband index within its resolution level. This value uniquely
		/// identifies a subband within a resolution level and a decomposition
		/// level within it. Note that only leaf elements represent "real"
		/// subbands, while node elements represent only intermediate stages.
		/// 
		/// <p>It is defined recursively. The root node gets a value of 0. For a
		/// given node, with a subband index 'b', its LL descendant gets 4*b, its
		/// HL descendant 4*b+1, its LH descendant 4*b+2, and its HH descendant
		/// 4*b+3, for their subband indexes.</p>
		/// 
		/// </summary>
		public int sbandIdx = 0;
		
		/// <summary> The horizontal coordinate of the upper-left corner of the subband, with
		/// respect to the canvas origin, in the component's grid and subband's
		/// decomposition level. This is the real horizontal index of the first
		/// column of this subband. If even the horizontal decomposition of this
		/// subband should be done with the low-pass-first convention. If odd it
		/// should be done with the high-pass-first convention.
		/// 
		/// </summary>
		public int ulcx;
		
		/// <summary> The vertical coordinate of the upper-left corner of the subband, with
		/// respect to the canvas origin, in the component's grid and subband's
		/// decomposition level. This is the real vertical index of the first
		/// column of this subband. If even the vertical decomposition of this
		/// subband should be done with the low-pass-first convention. If odd it
		/// should be done with the high-pass-first convention.
		/// 
		/// </summary>
		public int ulcy;
		
		/// <summary>The horizontal coordinate of the upper-left corner of the subband </summary>
		public int ulx;
		
		/// <summary>The vertical coordinate of the upper-left corner of the subband </summary>
		public int uly;
		
		/// <summary>The width of the subband </summary>
		public int w;
		
		/// <summary>The height of the subband </summary>
		public int h;
		
		/// <summary>The nominal code-block width </summary>
		public int nomCBlkW;
		
		/// <summary>The nominal code-block height </summary>
		public int nomCBlkH;
		
		/// <summary> Splits the current subband in its four subbands. This creates the four
		/// childs (LL, HL, LH and HH) and converts the leaf in a node.
		/// 
		/// </summary>
		/// <param name="hfilter">The horizontal wavelet filter used to decompose this
		/// subband.
		/// 
		/// </param>
		/// <param name="vfilter">The vertical wavelet filter used to decompose this
		/// subband.
		/// 
		/// </param>
		/// <returns>  A reference to the LL leaf (getLL()).
		/// 
		/// </returns>
		protected internal abstract Subband split(WaveletFilter hfilter, WaveletFilter vfilter);
		
		/// <summary> Initializes the childs of this node with the correct values. The sizes
		/// of the child subbands are calculated by taking into account the
		/// position of the subband in the canvas.
		/// 
		/// <p>For the analysis subband gain calculation it is assumed that
		/// analysis filters are normalized with a DC gain of 1 and a Nyquist gain
		/// of 2.</p>
		/// 
		/// </summary>
		protected internal virtual void  initChilds()
		{
			Subband subb_LL = LL;
			Subband subb_HL = HL;
			Subband subb_LH = LH;
			Subband subb_HH = HH;
			
			// LL subband
			subb_LL.level = level + 1;
			subb_LL.ulcx = (ulcx + 1) >> 1;
			subb_LL.ulcy = (ulcy + 1) >> 1;
			subb_LL.ulx = ulx;
			subb_LL.uly = uly;
			subb_LL.w = ((ulcx + w + 1) >> 1) - subb_LL.ulcx;
			subb_LL.h = ((ulcy + h + 1) >> 1) - subb_LL.ulcy;
			// If this subband in in the all LL path (i.e. it's global orientation
			// is LL) then child LL band contributes to a lower resolution level.
			subb_LL.resLvl = (orientation == WT_ORIENT_LL)?resLvl - 1:resLvl;
			subb_LL.anGainExp = anGainExp;
			subb_LL.sbandIdx = (sbandIdx << 2);
			// HL subband
			subb_HL.orientation = WT_ORIENT_HL;
			subb_HL.level = subb_LL.level;
			subb_HL.ulcx = ulcx >> 1;
			subb_HL.ulcy = subb_LL.ulcy;
			subb_HL.ulx = ulx + subb_LL.w;
			subb_HL.uly = uly;
			subb_HL.w = ((ulcx + w) >> 1) - subb_HL.ulcx;
			subb_HL.h = subb_LL.h;
			subb_HL.resLvl = resLvl;
			subb_HL.anGainExp = anGainExp + 1;
			subb_HL.sbandIdx = (sbandIdx << 2) + 1;
			// LH subband
			subb_LH.orientation = WT_ORIENT_LH;
			subb_LH.level = subb_LL.level;
			subb_LH.ulcx = subb_LL.ulcx;
			subb_LH.ulcy = ulcy >> 1;
			subb_LH.ulx = ulx;
			subb_LH.uly = uly + subb_LL.h;
			subb_LH.w = subb_LL.w;
			subb_LH.h = ((ulcy + h) >> 1) - subb_LH.ulcy;
			subb_LH.resLvl = resLvl;
			subb_LH.anGainExp = anGainExp + 1;
			subb_LH.sbandIdx = (sbandIdx << 2) + 2;
			// HH subband
			subb_HH.orientation = WT_ORIENT_HH;
			subb_HH.level = subb_LL.level;
			subb_HH.ulcx = subb_HL.ulcx;
			subb_HH.ulcy = subb_LH.ulcy;
			subb_HH.ulx = subb_HL.ulx;
			subb_HH.uly = subb_LH.uly;
			subb_HH.w = subb_HL.w;
			subb_HH.h = subb_LH.h;
			subb_HH.resLvl = resLvl;
			subb_HH.anGainExp = anGainExp + 2;
			subb_HH.sbandIdx = (sbandIdx << 2) + 3;
		}
		
		/// <summary> Creates a Subband element with all the default values. The dimensions
		/// are (0,0), the upper left corner is (0,0) and the upper-left corner
		/// with respect to the canvas is (0,0) too.
		/// 
		/// </summary>
		public Subband()
		{
		}
		
		/// <summary> Creates the top-level node and the entire subband tree, with the
		/// top-level dimensions, the number of decompositions, and the
		/// decomposition tree as specified.
		/// 
		/// <p>For the analysis subband gain calculation it is assumed that
		/// analysis filters are normalized with a DC gain of 1 and a Nyquist gain
		/// of 2.</p>
		/// 
		/// <p>This constructor does not initialize the value of the magBits member
		/// variable. This variable is normally initialized by the quantizer, on
		/// the encoder side, or the bit stream reader, on the decoder side.</p>
		/// 
		/// </summary>
		/// <param name="w">The top-level width
		/// 
		/// </param>
		/// <param name="h">The top-level height
		/// 
		/// </param>
		/// <param name="ulcx">The horizontal coordinate of the upper-left corner with
		/// respect to the canvas origin, in the component grid.
		/// 
		/// </param>
		/// <param name="ulcy">The vertical coordinate of the upper-left corner with
		/// respect to the canvas origin, in the component grid.
		/// 
		/// </param>
		/// <param name="lvls">The number of levels (or LL decompositions) in the tree.
		/// 
		/// </param>
		/// <param name="hfilters">The horizontal wavelet filters (analysis or synthesis)
		/// for each resolution level, starting at resolution level 0. If there are
		/// less elements in the array than there are resolution levels, the last
		/// element is used for the remaining resolution levels.
		/// 
		/// </param>
		/// <param name="vfilters">The vertical wavelet filters (analysis or synthesis)
		/// for each resolution level, starting at resolution level 0. If there are
		/// less elements in the array than there are resolution levels, the last
		/// element is used for the remaining resolution levels.
		/// 
		/// </param>
		/// <seealso cref="WaveletTransform">
		/// 
		/// </seealso>
		public Subband(int w, int h, int ulcx, int ulcy, int lvls, WaveletFilter[] hfilters, WaveletFilter[] vfilters)
		{
			int i, hi, vi;
			Subband cur; // The current subband
			
			// Initialize top-level node
			this.w = w;
			this.h = h;
			this.ulcx = ulcx;
			this.ulcy = ulcy;
			this.resLvl = lvls;
			// First create dyadic decomposition.
			cur = this;
			for (i = 0; i < lvls; i++)
			{
				hi = (cur.resLvl <= hfilters.Length)?cur.resLvl - 1:hfilters.Length - 1;
				vi = (cur.resLvl <= vfilters.Length)?cur.resLvl - 1:vfilters.Length - 1;
				cur = cur.split(hfilters[hi], vfilters[vi]);
			}
		}
		
		/// <summary> Returns the next subband in the same resolution level, following the
		/// subband index order. If already at the last subband then null is
		/// returned. If this subband is not a leaf an IllegalArgumentException is
		/// thrown.
		/// 
		/// </summary>
		/// <returns> The next subband in the same resolution level, following the
		/// subband index order, or null if already at last subband.
		/// 
		/// </returns>
		public virtual Subband nextSubband()
		{
			Subband sb;
			
			if (isNode)
			{
				throw new System.ArgumentException();
			}
			
			switch (orientation)
			{
				
				case WT_ORIENT_LL: 
					sb = Parent;
					if (sb == null || sb.resLvl != resLvl)
					{
						// Already at top-level or last subband in res. level
						return null;
					}
					else
					{
						return sb.HL;
					}
					//goto case WT_ORIENT_HL;
				
				case WT_ORIENT_HL: 
					return Parent.LH;
				
				case WT_ORIENT_LH: 
					return Parent.HH;
				
				case WT_ORIENT_HH: 
					// This is the complicated one
					sb = this;
					while (sb.orientation == WT_ORIENT_HH)
					{
						sb = sb.Parent;
					}
					switch (sb.orientation)
					{
						
						case WT_ORIENT_LL: 
							sb = sb.Parent;
							if (sb == null || sb.resLvl != resLvl)
							{
								// Already at top-level or last subband in res. level
								return null;
							}
							else
							{
								sb = sb.HL;
							}
							break;
						
						case WT_ORIENT_HL: 
							sb = sb.Parent.LH;
							break;
						
						case WT_ORIENT_LH: 
							sb = sb.Parent.HH;
							break;
						
						default: 
							throw new System.ApplicationException("You have found a bug in JJ2000");
						
					}
					while (sb.isNode)
					{
						sb = sb.LL;
					}
					return sb;
				
				default: 
					throw new System.ApplicationException("You have found a bug in JJ2000");
				
			}
		}
		
		/// <summary> Returns a subband element in the tree, given its resolution level and
		/// subband index. This method searches through the tree.
		/// 
		/// </summary>
		/// <param name="rl">The resolution level.
		/// 
		/// </param>
		/// <param name="sbi">The subband index, within the resolution level.
		/// 
		/// </param>
		public virtual Subband getSubbandByIdx(int rl, int sbi)
		{
			Subband sb = this;
			
			// Find the root subband for the resolution level
			if (rl > sb.resLvl || rl < 0)
			{
				throw new System.ArgumentException("Resolution level index " + "out of range");
			}
			
			// Returns directly if it is itself
			if (rl == sb.resLvl && sbi == sb.sbandIdx)
				return sb;
			
			if (sb.sbandIdx != 0)
				sb = sb.Parent;
			
			while (sb.resLvl > rl)
				sb = sb.LL;
			while (sb.resLvl < rl)
				sb = sb.Parent;
			
			switch (sbi)
			{
				
				case 0: 
				default: 
					return sb;
				
				case 1: 
					return sb.HL;
				
				case 2: 
					return sb.LH;
				
				case 3: 
					return sb.HH;
				}
		}
		
		/// <summary> Returns a reference to the Subband element to which the specified point
		/// belongs. The specified point must be inside this (i.e. the one defined
		/// by this object) subband. This method searches through the tree.
		/// 
		/// </summary>
		/// <param name="x">horizontal coordinate of the specified point.
		/// 
		/// </param>
		/// <param name="y">horizontal coordinate of the specified point.
		/// 
		/// </param>
		public virtual Subband getSubband(int x, int y)
		{
			Subband cur, hhs;
			
			// Check that we are inside this subband
			if (x < ulx || y < uly || x >= ulx + w || y >= uly + h)
			{
				throw new System.ArgumentException();
			}
			
			cur = this;
			while (cur.isNode)
			{
				hhs = cur.HH;
				// While we are still at a node -> continue
				if (x < hhs.ulx)
				{
					// Is the result of horizontal low-pass
					if (y < hhs.uly)
					{
						// Vertical low-pass
						cur = cur.LL;
					}
					else
					{
						// Vertical high-pass
						cur = cur.LH;
					}
				}
				else
				{
					// Is the result of horizontal high-pass
					if (y < hhs.uly)
					{
						// Vertical low-pass
						cur = cur.HL;
					}
					else
					{
						// Vertical high-pass
						cur = cur.HH;
					}
				}
			}
			
			return cur;
		}
		
		/// <summary> Returns subband informations in a string.
		/// 
		/// </summary>
		/// <returns> Subband informations
		/// 
		/// </returns>
		public override System.String ToString()
		{
			System.String string_Renamed = "w=" + w + ",h=" + h + ",ulx=" + ulx + ",uly=" + uly + ",ulcx=" + ulcx + ",ulcy=" + ulcy + ",idx=" + sbandIdx + ",orient=" + orientation + ",node=" + isNode + ",level=" + level + ",resLvl=" + resLvl + ",nomCBlkW=" + nomCBlkW + ",nomCBlkH=" + nomCBlkH + ",numCb=" + numCb;
			
			return string_Renamed;
		}
	}
}