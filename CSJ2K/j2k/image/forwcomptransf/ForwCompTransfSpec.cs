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
using CSJ2K.j2k.wavelet.analysis;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k;
namespace CSJ2K.j2k.image.forwcomptransf
{
	
	/// <summary> This class extends CompTransfSpec class in order to hold encoder specific
	/// aspects of CompTransfSpec.
	/// 
	/// </summary>
	/// <seealso cref="CompTransfSpec">
	/// 
	/// </seealso>
	public class ForwCompTransfSpec:CompTransfSpec, FilterTypes
	{
		/// <summary> Constructs a new 'ForwCompTransfSpec' for the specified number of
		/// components and tiles, the wavelet filters type and the parameter of the
		/// option 'Mct'. This constructor is called by the encoder. It also checks
		/// that the arguments belong to the recognized arguments list.
		/// 
		/// <p>This constructor chose the component transformation type depending
		/// on the wavelet filters : RCT with w5x3 filter and ICT with w9x7
		/// filter. Note: All filters must use the same data type.</p>
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
		/// <param name="wfs">The wavelet filter specifications
		/// 
		/// </param>
		/// <param name="pl">The ParameterList
		/// 
		/// </param>
		public ForwCompTransfSpec(int nt, int nc, byte type, AnWTFilterSpec wfs, ParameterList pl):base(nt, nc, type)
		{
			
			System.String param = pl.getParameter("Mct");
			
			if (param == null)
			{
				// The option has not been specified
				
				// If less than three component, do not use any component
				// transformation 
				if (nc < 3)
				{
					setDefault("none");
					return ;
				}
				// If the compression is lossless, uses RCT
				else if (pl.getBooleanParameter("lossless"))
				{
					setDefault("rct");
					return ;
				}
				else
				{
					AnWTFilter[][] anfilt;
					int[] filtType = new int[nComp];
					for (int c = 0; c < 3; c++)
					{
						anfilt = (AnWTFilter[][]) wfs.getCompDef(c);
						filtType[c] = anfilt[0][0].FilterType;
					}
					
					// Check that the three first components use the same filters
					bool reject = false;
					for (int c = 1; c < 3; c++)
					{
						if (filtType[c] != filtType[0])
							reject = true;
					}
					
					if (reject)
					{
						setDefault("none");
					}
					else
					{
						anfilt = (AnWTFilter[][]) wfs.getCompDef(0);
						if (anfilt[0][0].FilterType == CSJ2K.j2k.wavelet.FilterTypes_Fields.W9X7)
						{
							setDefault("ict");
						}
						else
						{
							setDefault("rct");
						}
					}
				}
				
				// Each tile receives a component transform specification
				// according the type of wavelet filters that are used by the
				// three first components
				for (int t = 0; t < nt; t++)
				{
					AnWTFilter[][] anfilt;
					int[] filtType = new int[nComp];
					for (int c = 0; c < 3; c++)
					{
						anfilt = (AnWTFilter[][]) wfs.getTileCompVal(t, c);
						filtType[c] = anfilt[0][0].FilterType;
					}
					
					// Check that the three components use the same filters
					bool reject = false;
					for (int c = 1; c < nComp; c++)
					{
						if (filtType[c] != filtType[0])
							reject = true;
					}
					
					if (reject)
					{
						setTileDef(t, "none");
					}
					else
					{
						anfilt = (AnWTFilter[][]) wfs.getTileCompVal(t, 0);
						if (anfilt[0][0].FilterType == CSJ2K.j2k.wavelet.FilterTypes_Fields.W9X7)
						{
							setTileDef(t, "ict");
						}
						else
						{
							setTileDef(t, "rct");
						}
					}
				}
				return ;
			}
			
			// Parse argument
			SupportClass.Tokenizer stk = new SupportClass.Tokenizer(param);
			System.String word; // current word
			byte curSpecType = SPEC_DEF; // Specification type of the
			// current parameter
			bool[] tileSpec = null; // Tiles concerned by the
			// specification
			//System.Boolean value_Renamed;
			
			while (stk.HasMoreTokens())
			{
				word = stk.NextToken();
				
				switch (word[0])
				{
					
					case 't':  // Tiles specification
						tileSpec = parseIdx(word, nTiles);
						if (curSpecType == SPEC_COMP_DEF)
						{
							curSpecType = SPEC_TILE_COMP;
						}
						else
						{
							curSpecType = SPEC_TILE_DEF;
						}
						break;
					
					case 'c':  // Components specification
						throw new System.ArgumentException("Component specific " + " parameters" + " not allowed with " + "'-Mct' option");
					
					default: 
						if (word.Equals("off"))
						{
							if (curSpecType == SPEC_DEF)
							{
								setDefault("none");
							}
							else if (curSpecType == SPEC_TILE_DEF)
							{
								for (int i = tileSpec.Length - 1; i >= 0; i--)
									if (tileSpec[i])
									{
										setTileDef(i, "none");
									}
							}
						}
						else if (word.Equals("on"))
						{
							if (nc < 3)
							{
								throw new System.ArgumentException("Cannot use component" + " transformation on a " + "image with less than " + "three components");
							}
							
							if (curSpecType == SPEC_DEF)
							{
								// Set arbitrarily the default
								// value to RCT (later will be found the suitable
								// component transform for each tile)
								setDefault("rct");
							}
							else if (curSpecType == SPEC_TILE_DEF)
							{
								for (int i = tileSpec.Length - 1; i >= 0; i--)
								{
									if (tileSpec[i])
									{
										if (getFilterType(i, wfs) == CSJ2K.j2k.wavelet.FilterTypes_Fields.W5X3)
										{
											setTileDef(i, "rct");
										}
										else
										{
											setTileDef(i, "ict");
										}
									}
								}
							}
						}
						else
						{
							throw new System.ArgumentException("Default parameter of " + "option Mct not" + " recognized: " + param);
						}
						
						// Re-initialize
						curSpecType = SPEC_DEF;
						tileSpec = null;
						break;
					
				}
			}
			
			// Check that default value has been specified
			if (getDefault() == null)
			{
				// If not, set arbitrarily the default value to 'none' but
				// specifies explicitely a default value for each tile depending
				// on the wavelet transform that is used
				setDefault("none");
				
				for (int t = 0; t < nt; t++)
				{
					if (isTileSpecified(t))
					{
						continue;
					}
					
					AnWTFilter[][] anfilt;
					int[] filtType = new int[nComp];
					for (int c = 0; c < 3; c++)
					{
						anfilt = (AnWTFilter[][]) wfs.getTileCompVal(t, c);
						filtType[c] = anfilt[0][0].FilterType;
					}
					
					// Check that the three components use the same filters
					bool reject = false;
					for (int c = 1; c < nComp; c++)
					{
						if (filtType[c] != filtType[0])
							reject = true;
					}
					
					if (reject)
					{
						setTileDef(t, "none");
					}
					else
					{
						anfilt = (AnWTFilter[][]) wfs.getTileCompVal(t, 0);
						if (anfilt[0][0].FilterType == CSJ2K.j2k.wavelet.FilterTypes_Fields.W9X7)
						{
							setTileDef(t, "ict");
						}
						else
						{
							setTileDef(t, "rct");
						}
					}
				}
			}
			
			// Check validity of component transformation of each tile compared to
			// the filter used.
			for (int t = nt - 1; t >= 0; t--)
			{
				
				if (((System.String) getTileDef(t)).Equals("none"))
				{
					// No comp. transf is used. No check is needed
					continue;
				}
				else if (((System.String) getTileDef(t)).Equals("rct"))
				{
					// Tile is using Reversible component transform
					int filterType = getFilterType(t, wfs);
					switch (filterType)
					{
						
						case CSJ2K.j2k.wavelet.FilterTypes_Fields.W5X3:  // OK
							break;
						
						case CSJ2K.j2k.wavelet.FilterTypes_Fields.W9X7:  // Must use ICT
							if (isTileSpecified(t))
							{
								// User has requested RCT -> Error
								throw new System.ArgumentException("Cannot use RCT " + "with 9x7 filter " + "in tile " + t);
							}
							else
							{
								// Specify ICT for this tile
								setTileDef(t, "ict");
							}
							break;
						
						default: 
							throw new System.ArgumentException("Default filter is " + "not JPEG 2000 part" + " I compliant");
						
					}
				}
				else
				{
					// ICT
					int filterType = getFilterType(t, wfs);
					switch (filterType)
					{
						
						case CSJ2K.j2k.wavelet.FilterTypes_Fields.W5X3:  // Must use RCT
							if (isTileSpecified(t))
							{
								// User has requested ICT -> Error
								throw new System.ArgumentException("Cannot use ICT " + "with filter 5x3 " + "in tile " + t);
							}
							else
							{
								setTileDef(t, "rct");
							}
							break;
						
						case CSJ2K.j2k.wavelet.FilterTypes_Fields.W9X7:  // OK
							break;
						
						default: 
							throw new System.ArgumentException("Default filter is " + "not JPEG 2000 part" + " I compliant");
						
					}
				}
			}
		}
		
		/// <summary> Get the filter type common to all component of a given tile. If the
		/// tile index is -1, it searches common filter type of default
		/// specifications.
		/// 
		/// </summary>
		/// <param name="t">The tile index
		/// 
		/// </param>
		/// <param name="wfs">The analysis filters specifications 
		/// 
		/// </param>
		/// <returns> The filter type common to all the components 
		/// 
		/// </returns>
		private int getFilterType(int t, AnWTFilterSpec wfs)
		{
			AnWTFilter[][] anfilt;
			int[] filtType = new int[nComp];
			for (int c = 0; c < nComp; c++)
			{
				if (t == - 1)
				{
					anfilt = (AnWTFilter[][]) wfs.getCompDef(c);
				}
				else
				{
					anfilt = (AnWTFilter[][]) wfs.getTileCompVal(t, c);
				}
				filtType[c] = anfilt[0][0].FilterType;
			}
			
			// Check that all filters are the same one
			bool reject = false;
			for (int c = 1; c < nComp; c++)
			{
				if (filtType[c] != filtType[0])
					reject = true;
			}
			if (reject)
			{
				throw new System.ArgumentException("Can not use component" + " transformation when " + "components do not use " + "the same filters");
			}
			return filtType[0];
		}
	}
}