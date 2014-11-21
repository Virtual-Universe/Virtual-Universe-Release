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
using CSJ2K.j2k.util;
using CSJ2K.j2k.io;
namespace CSJ2K.j2k.codestream.reader
{
	
	/// <summary> This class implements the tag tree decoder. A tag tree codes a 2D matrix of
	/// integer elements in an efficient way. The decoding procedure 'update()'
	/// updates a value of the matrix from a stream of coded data, given a
	/// threshold. This procedure decodes enough information to identify whether or
	/// not the value is greater than or equal to the threshold, and updates the
	/// value accordingly.
	/// 
	/// <p>In general the decoding procedure must follow the same sequence of
	/// elements and thresholds as the encoding one. The encoder is implemented by
	/// the TagTreeEncoder class.</p>
	/// 
	/// <p>Tag trees that have one dimension, or both, as 0 are allowed for
	/// convenience. Of course no values can be set or coded in such cases.</p>
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.codestream.writer.TagTreeEncoder">
	/// 
	/// </seealso>
	public class TagTreeDecoder
	{
		/// <summary> Returns the number of leafs along the horizontal direction.
		/// 
		/// </summary>
		/// <returns> The number of leafs along the horizontal direction.
		/// 
		/// </returns>
		virtual public int Width
		{
			get
			{
				return w;
			}
			
		}
		/// <summary> Returns the number of leafs along the vertical direction.
		/// 
		/// </summary>
		/// <returns> The number of leafs along the vertical direction.
		/// 
		/// </returns>
		virtual public int Height
		{
			get
			{
				return h;
			}
			
		}
		
		/// <summary>The horizontal dimension of the base level </summary>
		protected internal int w;
		
		/// <summary>The vertical dimensions of the base level </summary>
		protected internal int h;
		
		/// <summary>The number of levels in the tag tree </summary>
		protected internal int lvls;
		
		/// <summary>The tag tree values. The first index is the level, starting at level 0
		/// (leafs). The second index is the element within the level, in
		/// lexicographical order. 
		/// </summary>
		protected internal int[][] treeV;
		
		/// <summary>The tag tree state. The first index is the level, starting at level 0
		/// (leafs). The second index is the element within the level, in
		/// lexicographical order. 
		/// </summary>
		protected internal int[][] treeS;
		
		/// <summary> Creates a tag tree decoder with 'w' elements along the horizontal
		/// dimension and 'h' elements along the vertical direction. The total
		/// number of elements is thus 'vdim' x 'hdim'.
		/// 
		/// <p>The values of all elements are initialized to Integer.MAX_VALUE
		/// (i.e. no information decoded so far). The states are initialized all to
		/// 0.</p>
		/// 
		/// </summary>
		/// <param name="h">The number of elements along the vertical direction.
		/// 
		/// </param>
		/// <param name="w">The number of elements along the horizontal direction.
		/// 
		/// </param>
		public TagTreeDecoder(int h, int w)
		{
			int i;
			
			// Check arguments
			if (w < 0 || h < 0)
			{
				throw new System.ArgumentException();
			}
			// Initialize dimensions
			this.w = w;
			this.h = h;
			// Calculate the number of levels
			if (w == 0 || h == 0)
			{
				lvls = 0; // Empty tree
			}
			else
			{
				lvls = 1;
				while (h != 1 || w != 1)
				{
					// Loop until we reach root
					w = (w + 1) >> 1;
					h = (h + 1) >> 1;
					lvls++;
				}
			}
			// Allocate tree values and states
			treeV = new int[lvls][];
			treeS = new int[lvls][];
			w = this.w;
			h = this.h;
			for (i = 0; i < lvls; i++)
			{
				treeV[i] = new int[h * w];
				// Initialize to infinite value
				ArrayUtil.intArraySet(treeV[i], System.Int32.MaxValue);
				
				// (no need to initialize to 0 since it's the default)
				treeS[i] = new int[h * w];
				w = (w + 1) >> 1;
				h = (h + 1) >> 1;
			}
		}
		
		/// <summary> Decodes information for the specified element of the tree, given the
		/// threshold, and updates its value. The information that can be decoded
		/// is whether or not the value of the element is greater than, or equal
		/// to, the value of the threshold.
		/// 
		/// </summary>
		/// <param name="m">The vertical index of the element.
		/// 
		/// </param>
		/// <param name="n">The horizontal index of the element.
		/// 
		/// </param>
		/// <param name="t">The threshold to use in decoding. It must be non-negative.
		/// 
		/// </param>
		/// <param name="in">The stream from where to read the coded information.
		/// 
		/// </param>
		/// <returns> The updated value at position (m,n).
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error occurs while reading from 'in'.
		/// 
		/// </exception>
		/// <exception cref="EOFException">If the ned of the 'in' stream is reached before
		/// getting all the necessary data.
		/// 
		/// </exception>
		public virtual int update(int m, int n, int t, PktHeaderBitReader in_Renamed)
		{
			int k, tmin;
			int idx, ts, tv;
			
			// Check arguments
			if (m >= h || n >= w || t < 0)
			{
				throw new System.ArgumentException();
			}
			
			// Initialize
			k = lvls - 1;
			tmin = treeS[k][0];
			
			// Loop on levels
			idx = (m >> k) * ((w + (1 << k) - 1) >> k) + (n >> k);
			while (true)
			{
				// Cache state and value
				ts = treeS[k][idx];
				tv = treeV[k][idx];
				if (ts < tmin)
				{
					ts = tmin;
				}
				while (t > ts)
				{
					if (tv >= ts)
					{
						// We are not done yet
						if (in_Renamed.readBit() == 0)
						{
							// '0' bit
							// We know that 'value' > treeS[k][idx]
							ts++;
						}
						else
						{
							// '1' bit
							// We know that 'value' = treeS[k][idx]
							tv = ts++;
						}
						// Increment of treeS[k][idx] done above
					}
					else
					{
						// We are done, we can set ts and get out
						ts = t;
						break; // get out of this while
					}
				}
				// Update state and value
				treeS[k][idx] = ts;
				treeV[k][idx] = tv;
				// Update tmin or terminate
				if (k > 0)
				{
					tmin = ts < tv?ts:tv;
					k--;
					// Index of element for next iteration
					idx = (m >> k) * ((w + (1 << k) - 1) >> k) + (n >> k);
				}
				else
				{
					// Return the updated value
					return tv;
				}
			}
		}
		
		/// <summary> Returns the current value of the specified element in the tag
		/// tree. This is the value as last updated by the update() method.
		/// 
		/// </summary>
		/// <param name="m">The vertical index of the element.
		/// 
		/// </param>
		/// <param name="n">The horizontal index of the element.
		/// 
		/// </param>
		/// <returns> The current value of the element.
		/// 
		/// </returns>
		/// <seealso cref="update">
		/// 
		/// </seealso>
		public virtual int getValue(int m, int n)
		{
			// Check arguments
			if (m >= h || n >= w)
			{
				throw new System.ArgumentException();
			}
			// Return value
			return treeV[0][m * w + n];
		}
	}
}