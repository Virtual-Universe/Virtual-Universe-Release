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
using CSJ2K.j2k;
namespace CSJ2K.j2k.wavelet.synthesis
{
	
	/// <summary> This class extends ModuleSpec class for synthesis filters specification
	/// holding purpose.
	/// 
	/// </summary>
	/// <seealso cref="ModuleSpec">
	/// 
	/// </seealso>
	public class SynWTFilterSpec:ModuleSpec
	{
		
		/// <summary> Constructs a new 'SynWTFilterSpec' for the specified number of
		/// components and tiles.
		/// 
		/// </summary>
		/// <param name="nt">The number of tiles
		/// 
		/// </param>
		/// <param name="nc">The number of components
		/// 
		/// </param>
		/// <param name="type">the type of the specification module i.e. tile specific,
		/// component specific or both.
		/// 
		/// </param>
		public SynWTFilterSpec(int nt, int nc, byte type):base(nt, nc, type)
		{
		}
		
		/// <summary> Returns the data type used by the filters in this object, as defined in
		/// the 'DataBlk' interface for specified tile-component.
		/// 
		/// </summary>
		/// <param name="t">Tile index
		/// 
		/// </param>
		/// <param name="c">Component index
		/// 
		/// </param>
		/// <returns> The data type of the filters in this object
		/// 
		/// </returns>
		/// <seealso cref="jj2000.j2k.image.DataBlk">
		/// 
		/// </seealso>
		public virtual int getWTDataType(int t, int c)
		{
			SynWTFilter[][] an = (SynWTFilter[][]) getSpec(t, c);
			return an[0][0].DataType;
		}
		
		/// <summary> Returns the horizontal analysis filters to be used in component 'n' and
		/// tile 't'.
		/// 
		/// <p>The horizontal analysis filters are returned in an array of
		/// SynWTFilter. Each element contains the horizontal filter for each
		/// resolution level starting with resolution level 1 (i.e. the analysis
		/// filter to go from resolution level 1 to resolution level 0). If there
		/// are less elements than the maximum resolution level, then the last
		/// element is assumed to be repeated.</p>
		/// 
		/// </summary>
		/// <param name="t">The tile index, in raster scan order
		/// 
		/// </param>
		/// <param name="c">The component index.
		/// 
		/// </param>
		/// <returns> The array of horizontal analysis filters for component 'n' and
		/// tile 't'.
		/// 
		/// </returns>
		public virtual SynWTFilter[] getHFilters(int t, int c)
		{
			SynWTFilter[][] an = (SynWTFilter[][]) getSpec(t, c);
			return an[0];
		}
		
		/// <summary> Returns the vertical analysis filters to be used in component 'n' and
		/// tile 't'.
		/// 
		/// <p>The vertical analysis filters are returned in an array of
		/// SynWTFilter. Each element contains the vertical filter for each
		/// resolution level starting with resolution level 1 (i.e. the analysis
		/// filter to go from resolution level 1 to resolution level 0). If there
		/// are less elements than the maximum resolution level, then the last
		/// element is assumed to be repeated.</p>
		/// 
		/// </summary>
		/// <param name="t">The tile index, in raster scan order
		/// 
		/// </param>
		/// <param name="c">The component index.
		/// 
		/// </param>
		/// <returns> The array of horizontal analysis filters for component 'n' and
		/// tile 't'.
		/// 
		/// </returns>
		public virtual SynWTFilter[] getVFilters(int t, int c)
		{
			SynWTFilter[][] an = (SynWTFilter[][]) getSpec(t, c);
			return an[1];
		}
		
		/// <summary>Debugging method </summary>
		public override System.String ToString()
		{
			System.String str = "";
			SynWTFilter[][] an;
			
			str += ("nTiles=" + nTiles + "\nnComp=" + nComp + "\n\n");
			
			for (int t = 0; t < nTiles; t++)
			{
				for (int c = 0; c < nComp; c++)
				{
					an = (SynWTFilter[][]) getSpec(t, c);
					
					str += ("(t:" + t + ",c:" + c + ")\n");
					
					// Horizontal filters
					str += "\tH:";
					for (int i = 0; i < an[0].Length; i++)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						str += (" " + an[0][i]);
					}
					// Horizontal filters
					str += "\n\tV:";
					for (int i = 0; i < an[1].Length; i++)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						str += (" " + an[1][i]);
					}
					str += "\n";
				}
			}
			
			return str;
		}
		
		/// <summary> Check the reversibility of filters contained is the given
		/// tile-component.
		/// 
		/// </summary>
		/// <param name="t">The index of the tile
		/// 
		/// </param>
		/// <param name="c">The index of the component
		/// 
		/// </param>
		public virtual bool isReversible(int t, int c)
		{
			// Note: no need to buffer the result since this method is normally
			// called once per tile-component.
			SynWTFilter[] hfilter = getHFilters(t, c), vfilter = getVFilters(t, c);
			
			// As soon as a filter is not reversible, false can be returned
			for (int i = hfilter.Length - 1; i >= 0; i--)
				if (!hfilter[i].Reversible || !vfilter[i].Reversible)
					return false;
			return true;
		}
	}
}