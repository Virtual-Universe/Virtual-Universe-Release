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
namespace CSJ2K.j2k.wavelet.synthesis
{
	
	/// <summary> This class represents a subband in a tree structure that describes the
	/// subband decomposition for a wavelet transform, specifically for the
	/// syhthesis side.
	/// 
	/// <p>The element can be either a node or a leaf of the tree. If it is a node
	/// then ther are 4 descendants (LL, HL, LH and HH). If it is a leaf there are
	/// no descendants.</p>
	/// 
	/// <p>The tree is bidirectional. Each element in the tree structure has a
	/// "parent", which is the subband from which the element was obtained by
	/// decomposition. The only exception is the root element which has no parent
	/// (i.e.it's null), for obvious reasons.</p>
	/// 
	/// </summary>
	public class SubbandSyn:Subband
	{
		/// <summary> Returns the parent of this subband. The parent of a subband is the
		/// subband from which this one was obtained by decomposition. The root
		/// element has no parent subband (null).
		/// 
		/// </summary>
		/// <returns> The parent subband, or null for the root one.
		/// 
		/// </returns>
		override public Subband Parent
		{
			get
			{
				return parent;
			}
			
		}
		/// <summary> Returns the LL child subband of this subband.
		/// 
		/// </summary>
		/// <returns> The LL child subband, or null if there are no childs.
		/// 
		/// </returns>
		override public Subband LL
		{
			get
			{
				return subb_LL;
			}
			
		}
		/// <summary> Returns the HL (horizontal high-pass) child subband of this subband.
		/// 
		/// </summary>
		/// <returns> The HL child subband, or null if there are no childs.
		/// 
		/// </returns>
		override public Subband HL
		{
			get
			{
				return subb_HL;
			}
			
		}
		/// <summary> Returns the LH (vertical high-pass) child subband of this subband.
		/// 
		/// </summary>
		/// <returns> The LH child subband, or null if there are no childs.
		/// 
		/// </returns>
		override public Subband LH
		{
			get
			{
				return subb_LH;
			}
			
		}
		/// <summary> Returns the HH child subband of this subband.
		/// 
		/// </summary>
		/// <returns> The HH child subband, or null if there are no childs.
		/// 
		/// </returns>
		override public Subband HH
		{
			get
			{
				return subb_HH;
			}
			
		}
		/// <summary> This function returns the horizontal wavelet filter relevant to this
		/// subband
		/// 
		/// </summary>
		/// <returns> The horizontal wavelet filter
		/// 
		/// </returns>
		override public WaveletFilter HorWFilter
		{
			get
			{
				return hFilter;
			}
			
		}
		/// <summary> This function returns the vertical wavelet filter relevant to this
		/// subband
		/// 
		/// </summary>
		/// <returns> The vertical wavelet filter
		/// 
		/// </returns>
		override public WaveletFilter VerWFilter
		{
			get
			{
				return hFilter;
			}
			
		}
		
		/// <summary>The reference to the parent of this subband. It is null for the root
		/// element. It is null by default.  
		/// </summary>
		private SubbandSyn parent;
		
		/// <summary>The reference to the LL subband resulting from the decomposition of
		/// this subband. It is null by default.  
		/// </summary>
		private SubbandSyn subb_LL;
		
		/// <summary>The reference to the HL subband (horizontal high-pass) resulting from
		/// the decomposition of this subband. It is null by default.  
		/// </summary>
		private SubbandSyn subb_HL;
		
		/// <summary>The reference to the LH subband (vertical high-pass) resulting from
		/// the decomposition of this subband. It is null by default.
		/// 
		/// </summary>
		private SubbandSyn subb_LH;
		
		/// <summary>The reference to the HH subband resulting from the decomposition of
		/// this subband. It is null by default.  
		/// </summary>
		private SubbandSyn subb_HH;
		
		/// <summary>The horizontal analysis filter used to recompose this subband, from
		/// its childs. This is applicable to "node" elements only. The default
		/// value is null. 
		/// </summary>
		public SynWTFilter hFilter;
		
		/// <summary>The vertical analysis filter used to decompose this subband, from its
		/// childs. This is applicable to "node" elements only. The default value
		/// is null. 
		/// </summary>
		public SynWTFilter vFilter;
		
		/// <summary>The number of magnitude bits </summary>
		public int magbits = 0;
		
		/// <summary> Creates a SubbandSyn element with all the default values. The
		/// dimensions are (0,0) and the upper left corner is (0,0).
		/// 
		/// </summary>
		public SubbandSyn()
		{
		}
		
		/// <summary> Creates the top-level node and the entire subband tree, with the
		/// top-level dimensions, the number of decompositions, and the
		/// decomposition tree as specified.
		/// 
		/// <p>This constructor just calls the same constructor of the super
		/// class.</p>
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
		/// <param name="hfilters">The horizontal wavelet synthesis filters for each
		/// resolution level, starting at resolution level 0.
		/// 
		/// </param>
		/// <param name="vfilters">The vertical wavelet synthesis filters for each
		/// resolution level, starting at resolution level 0.
		/// 
		/// </param>
		/// <seealso cref="Subband.Subband(int,int,int,int,int,">
		/// WaveletFilter[],WaveletFilter[])
		/// 
		/// </seealso>
		public SubbandSyn(int w, int h, int ulcx, int ulcy, int lvls, WaveletFilter[] hfilters, WaveletFilter[] vfilters):base(w, h, ulcx, ulcy, lvls, hfilters, vfilters)
		{
		}
		
		/// <summary> Splits the current subband in its four subbands. It changes the status
		/// of this element (from a leaf to a node, and sets the filters), creates
		/// the childs and initializes them. An IllegalArgumentException is thrown
		/// if this subband is not a leaf.
		/// 
		/// <p>It uses the initChilds() method to initialize the childs.</p>
		/// 
		/// </summary>
		/// <param name="hfilter">The horizontal wavelet filter used to decompose this
		/// subband. It has to be a SynWTFilter object.
		/// 
		/// </param>
		/// <param name="vfilter">The vertical wavelet filter used to decompose this
		/// subband. It has to be a SynWTFilter object.
		/// 
		/// </param>
		/// <returns> A reference to the LL leaf (subb_LL).
		/// 
		/// </returns>
		/// <seealso cref="Subband.initChilds">
		/// 
		/// </seealso>
		protected internal override Subband split(WaveletFilter hfilter, WaveletFilter vfilter)
		{
			// Test that this is a node
			if (isNode)
			{
				throw new System.ArgumentException();
			}
			
			// Modify this element into a node and set the filters
			isNode = true;
			this.hFilter = (SynWTFilter) hfilter;
			this.vFilter = (SynWTFilter) vfilter;
			
			// Create childs
			subb_LL = new SubbandSyn();
			subb_LH = new SubbandSyn();
			subb_HL = new SubbandSyn();
			subb_HH = new SubbandSyn();
			
			// Assign parent
			subb_LL.parent = this;
			subb_HL.parent = this;
			subb_LH.parent = this;
			subb_HH.parent = this;
			
			// Initialize childs
			initChilds();
			
			// Return reference to LL subband
			return subb_LL;
		}
	}
}