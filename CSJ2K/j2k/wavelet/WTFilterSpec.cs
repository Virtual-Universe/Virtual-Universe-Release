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
namespace CSJ2K.j2k.wavelet
{
	
	/// <summary> This is the generic class from which the ones that hold the analysis or
	/// synthesis filters to be used in each part of the image derive. See
	/// AnWTFilterSpec and SynWTFilterSpec.
	/// 
	/// <P>The filters to use are defined by a hierarchy. The hierarchy is:
	/// 
	/// <P>- Tile and component specific filters<br>
	/// - Tile specific default filters<br>
	/// - Component main default filters<br>
	/// - Main default filters<br>
	/// 
	/// <P>At the moment tiles are not supported by this class.
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.wavelet.analysis.AnWTFilterSpec">
	/// 
	/// </seealso>
	/// <seealso cref="jj2000.j2k.wavelet.synthesis.SynWTFilterSpec">
	/// 
	/// </seealso>
	
	public abstract class WTFilterSpec
	{
		/// <summary> Returns the data type used by the filters in this object, as defined in 
		/// the 'DataBlk' interface.
		/// 
		/// </summary>
		/// <returns> The data type of the filters in this object
		/// 
		/// </returns>
		/// <seealso cref="jj2000.j2k.image.DataBlk">
		/// 
		/// 
		/// 
		/// </seealso>
		public abstract int WTDataType{get;}
		
		/// <summary>The identifier for "main default" specified filters </summary>
		public const byte FILTER_SPEC_MAIN_DEF = 0;
		
		/// <summary>The identifier for "component default" specified filters </summary>
		public const byte FILTER_SPEC_COMP_DEF = 1;
		
		/// <summary>The identifier for "tile specific default" specified filters </summary>
		public const byte FILTER_SPEC_TILE_DEF = 2;
		
		/// <summary>The identifier for "tile and component specific" specified filters </summary>
		public const byte FILTER_SPEC_TILE_COMP = 3;
		
		/// <summary>The spec type for each tile and component. The first index is the
		/// component index, the second is the tile index. NOTE: The tile specific
		/// things are not supported yet. 
		/// </summary>
		// Use byte to save memory (no need for speed here).
		protected internal byte[] specValType;
		
		/// <summary> Constructs a 'WTFilterSpec' object, initializing all the components and
		/// tiles to the 'FILTER_SPEC_MAIN_DEF' spec type, for the specified number
		/// of components and tiles.
		/// 
		/// <P>NOTE: The tile specific things are not supported yet
		/// 
		/// </summary>
		/// <param name="nc">The number of components
		/// 
		/// </param>
		/// <param name="nt">The number of tiles
		/// 
		/// 
		/// 
		/// </param>
		protected internal WTFilterSpec(int nc)
		{
			specValType = new byte[nc];
		}
		
		/// <summary> Returns the type of specification for the filters in the specified
		/// component and tile. The specification type is one of:
		/// 'FILTER_SPEC_MAIN_DEF', 'FILTER_SPEC_COMP_DEF', 'FILTER_SPEC_TILE_DEF',
		/// 'FILTER_SPEC_TILE_COMP'.
		/// 
		/// <P>NOTE: The tile specific things are not supported yet
		/// 
		/// </summary>
		/// <param name="n">The component index
		/// 
		/// </param>
		/// <param name="t">The tile index, in raster scan order.
		/// 
		/// </param>
		/// <returns> The specification type for component 'n' and tile 't'.
		/// 
		/// 
		/// 
		/// </returns>
		public virtual byte getKerSpecType(int n)
		{
			return specValType[n];
		}
	}
}