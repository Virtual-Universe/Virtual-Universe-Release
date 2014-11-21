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
using CSJ2K.j2k;
namespace CSJ2K.j2k.wavelet
{
	
	/// <summary> This class holds the decomposition type to be used in each part of the
	/// image; the default one, the component specific ones, the tile default ones 
	/// and the component-tile specific ones.
	/// 
	/// <P>The decomposition type identifiers values are the same as in the
	/// codestream.
	/// 
	/// <P>The hierarchy is:<br>
	/// - Tile and component specific decomposition<br>
	/// - Tile specific default decomposition<br>
	/// - Component main default decomposition<br>
	/// - Main default decomposition<br>
	/// 
	/// <P>At the moment tiles are not supported by this class.
	/// 
	/// </summary>
	public class WTDecompSpec
	{
		/// <summary> Returns the main default decomposition type.
		/// 
		/// </summary>
		/// <returns> The main default decomposition type.
		/// 
		/// 
		/// 
		/// </returns>
		virtual public int MainDefDecompType
		{
			get
			{
				return mainDefDecompType;
			}
			
		}
		/// <summary> Returns the main default decomposition number of levels.
		/// 
		/// </summary>
		/// <returns> The main default decomposition number of levels.
		/// 
		/// 
		/// 
		/// </returns>
		virtual public int MainDefLevels
		{
			get
			{
				return mainDefLevels;
			}
			
		}
		/// <summary> ID for the dyadic wavelet tree decomposition (also called
		/// "Mallat" in JPEG 2000): 0x00.
		/// </summary>
		public const int WT_DECOMP_DYADIC = 0;
		
		/// <summary> ID for the SPACL (as defined in JPEG 2000) wavelet tree
		/// decomposition (1 level of decomposition in the high bands and
		/// some specified number for the lowest LL band): 0x02.  
		/// </summary>
		public const int WT_DECOMP_SPACL = 2;
		
		/// <summary> ID for the PACKET (as defined in JPEG 2000) wavelet tree
		/// decomposition (2 levels of decomposition in the high bands and
		/// some specified number for the lowest LL band): 0x01. 
		/// </summary>
		public const int WT_DECOMP_PACKET = 1;
		
		/// <summary>The identifier for "main default" specified decomposition </summary>
		public const byte DEC_SPEC_MAIN_DEF = 0;
		
		/// <summary>The identifier for "component default" specified decomposition </summary>
		public const byte DEC_SPEC_COMP_DEF = 1;
		
		/// <summary>The identifier for "tile specific default" specified decomposition </summary>
		public const byte DEC_SPEC_TILE_DEF = 2;
		
		/// <summary>The identifier for "tile and component specific" specified
		/// decomposition 
		/// </summary>
		public const byte DEC_SPEC_TILE_COMP = 3;
		
		/// <summary>The spec type for each tile and component. The first index is the
		/// component index, the second is the tile index. NOTE: The tile specific
		/// things are not supported yet. 
		/// </summary>
		// Use byte to save memory (no need for speed here).
		private byte[] specValType;
		
		/// <summary>The main default decomposition </summary>
		private int mainDefDecompType;
		
		/// <summary>The main default number of decomposition levels </summary>
		private int mainDefLevels;
		
		/// <summary>The component main default decomposition, for each component. </summary>
		private int[] compMainDefDecompType;
		
		/// <summary>The component main default decomposition levels, for each component </summary>
		private int[] compMainDefLevels;
		
		/// <summary> Constructs a new 'WTDecompSpec' for the specified number of components
		/// and tiles, with the given main default decomposition type and number of 
		/// levels.
		/// 
		/// <P>NOTE: The tile specific things are not supported yet
		/// 
		/// </summary>
		/// <param name="nc">The number of components
		/// 
		/// </param>
		/// <param name="nt">The number of tiles
		/// 
		/// </param>
		/// <param name="dec">The main default decomposition type
		/// 
		/// </param>
		/// <param name="lev">The main default number of decomposition levels
		/// 
		/// 
		/// 
		/// </param>
		public WTDecompSpec(int nc, int dec, int lev)
		{
			mainDefDecompType = dec;
			mainDefLevels = lev;
			specValType = new byte[nc];
		}
		
		/// <summary> Sets the "component main default" decomposition type and number of
		/// levels for the specified component. Both 'dec' and 'lev' can not be
		/// negative at the same time.
		/// 
		/// </summary>
		/// <param name="n">The component index
		/// 
		/// </param>
		/// <param name="dec">The decomposition type. If negative then the main default is
		/// used.
		/// 
		/// </param>
		/// <param name="lev">The number of levels. If negative then the main defaul is
		/// used.
		/// 
		/// 
		/// 
		/// </param>
		public virtual void  setMainCompDefDecompType(int n, int dec, int lev)
		{
			if (dec < 0 && lev < 0)
			{
				throw new System.ArgumentException();
			}
			// Set spec type and decomp
			specValType[n] = DEC_SPEC_COMP_DEF;
			if (compMainDefDecompType == null)
			{
				compMainDefDecompType = new int[specValType.Length];
				compMainDefLevels = new int[specValType.Length];
			}
			compMainDefDecompType[n] = (dec >= 0)?dec:mainDefDecompType;
			compMainDefLevels[n] = (lev >= 0)?lev:mainDefLevels;
			// For the moment disable it since other parts of JJ2000 do not
			// support this
			throw new NotImplementedException("Currently, in JJ2000, all components " + "and tiles must have the same " + "decomposition type and number of " + "levels");
		}
		
		/// <summary> Returns the type of specification for the decomposition in the
		/// specified component and tile. The specification type is one of:
		/// 'DEC_SPEC_MAIN_DEF', 'DEC_SPEC_COMP_DEF', 'DEC_SPEC_TILE_DEF',
		/// 'DEC_SPEC_TILE_COMP'.
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
		public virtual byte getDecSpecType(int n)
		{
			return specValType[n];
		}
		
		/// <summary> Returns the decomposition type to be used in component 'n' and tile
		/// 't'.
		/// 
		/// <P>NOTE: The tile specific things are not supported yet
		/// 
		/// </summary>
		/// <param name="n">The component index.
		/// 
		/// </param>
		/// <param name="t">The tile index, in raster scan order
		/// 
		/// </param>
		/// <returns> The decomposition type to be used.
		/// 
		/// 
		/// 
		/// </returns>
		public virtual int getDecompType(int n)
		{
			switch (specValType[n])
			{
				
				case DEC_SPEC_MAIN_DEF: 
					return mainDefDecompType;
				
				case DEC_SPEC_COMP_DEF: 
					return compMainDefDecompType[n];
				
				case DEC_SPEC_TILE_DEF:
                    throw new NotImplementedException();
				
				case DEC_SPEC_TILE_COMP:
                    throw new NotImplementedException();
				
				default: 
					throw new System.ApplicationException("Internal JJ2000 error");
				
			}
		}
		
		/// <summary> Returns the decomposition number of levels in component 'n' and tile
		/// 't'.
		/// 
		/// <P>NOTE: The tile specific things are not supported yet
		/// 
		/// </summary>
		/// <param name="n">The component index.
		/// 
		/// </param>
		/// <param name="t">The tile index, in raster scan order
		/// 
		/// </param>
		/// <returns> The decomposition number of levels.
		/// 
		/// 
		/// 
		/// </returns>
		public virtual int getLevels(int n)
		{
			switch (specValType[n])
			{
				
				case DEC_SPEC_MAIN_DEF: 
					return mainDefLevels;
				
				case DEC_SPEC_COMP_DEF: 
					return compMainDefLevels[n];
				
				case DEC_SPEC_TILE_DEF:
                    throw new NotImplementedException();
				
				case DEC_SPEC_TILE_COMP:
                    throw new NotImplementedException();
				
				default: 
					throw new System.ApplicationException("Internal JJ2000 error");
				
			}
		}
	}
}