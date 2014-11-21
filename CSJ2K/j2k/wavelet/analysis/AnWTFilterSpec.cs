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
using CSJ2K.j2k.quantization;
using CSJ2K.j2k.util;
using CSJ2K.j2k;
namespace CSJ2K.j2k.wavelet.analysis
{
	
	/// <summary> This class extends ModuleSpec class for analysis filters specification
	/// holding purpose.
	/// 
	/// </summary>
	/// <seealso cref="ModuleSpec">
	/// 
	/// </seealso>
	public class AnWTFilterSpec:ModuleSpec
	{
		
		/// <summary>The reversible default filter </summary>
		private const System.String REV_FILTER_STR = "w5x3";
		
		/// <summary>The non-reversible default filter </summary>
		private const System.String NON_REV_FILTER_STR = "w9x7";
		
		/// <summary> Constructs a new 'AnWTFilterSpec' for the specified number of
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
		/// <param name="qts">Quantization specifications
		/// 
		/// </param>
		/// <param name="pl">The ParameterList
		/// 
		/// </param>
		public AnWTFilterSpec(int nt, int nc, byte type, QuantTypeSpec qts, ParameterList pl):base(nt, nc, type)
		{
			
			// Check parameters
			pl.checkList(AnWTFilter.OPT_PREFIX, CSJ2K.j2k.util.ParameterList.toNameArray(AnWTFilter.ParameterInfo));
			
			System.String param = pl.getParameter("Ffilters");
			bool isFilterSpecified = true;
			
			// No parameter specified
			if (param == null)
			{
				isFilterSpecified = false;
				
				// If lossless compression, uses the reversible filters in each
				// tile-components 
				if (pl.getBooleanParameter("lossless"))
				{
					setDefault(parseFilters(REV_FILTER_STR));
					return ;
				}
				
				// If no filter is specified through the command-line, use
				// REV_FILTER_STR or NON_REV_FILTER_STR according to the
				// quantization type
				for (int t = nt - 1; t >= 0; t--)
				{
					for (int c = nc - 1; c >= 0; c--)
					{
						switch (qts.getSpecValType(t, c))
						{
							
							case SPEC_DEF: 
								if (getDefault() == null)
								{
									if (pl.getBooleanParameter("lossless"))
										setDefault(parseFilters(REV_FILTER_STR));
									if (((System.String) qts.getDefault()).Equals("reversible"))
									{
										setDefault(parseFilters(REV_FILTER_STR));
									}
									else
									{
										setDefault(parseFilters(NON_REV_FILTER_STR));
									}
								}
								specValType[t][c] = SPEC_DEF;
								break;
							
							case SPEC_COMP_DEF: 
								if (!isCompSpecified(c))
								{
									if (((System.String) qts.getCompDef(c)).Equals("reversible"))
									{
										setCompDef(c, parseFilters(REV_FILTER_STR));
									}
									else
									{
										setCompDef(c, parseFilters(NON_REV_FILTER_STR));
									}
								}
								specValType[t][c] = SPEC_COMP_DEF;
								break;
							
							case SPEC_TILE_DEF: 
								if (!isTileSpecified(t))
								{
									if (((System.String) qts.getTileDef(t)).Equals("reversible"))
									{
										setTileDef(t, parseFilters(REV_FILTER_STR));
									}
									else
									{
										setTileDef(t, parseFilters(NON_REV_FILTER_STR));
									}
								}
								specValType[t][c] = SPEC_TILE_DEF;
								break;
							
							case SPEC_TILE_COMP: 
								if (!isTileCompSpecified(t, c))
								{
									if (((System.String) qts.getTileCompVal(t, c)).Equals("reversible"))
									{
										setTileCompVal(t, c, parseFilters(REV_FILTER_STR));
									}
									else
									{
										setTileCompVal(t, c, parseFilters(NON_REV_FILTER_STR));
									}
								}
								specValType[t][c] = SPEC_TILE_COMP;
								break;
							
							default: 
								throw new System.ArgumentException("Unsupported " + "specification " + "type");
							
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
			bool[] tileSpec = null; // Tiles concerned by the specification
			bool[] compSpec = null; // Components concerned by the specification
			AnWTFilter[][] filter;
			
			while (stk.HasMoreTokens())
			{
				word = stk.NextToken();
				
				switch (word[0])
				{
					
					case 't': 
					// Tiles specification
					case 'T':  // Tiles specification
						tileSpec = parseIdx(word, nTiles);
						if (curSpecType == SPEC_COMP_DEF)
							curSpecType = SPEC_TILE_COMP;
						else
							curSpecType = SPEC_TILE_DEF;
						break;
					
					case 'c': 
					// Components specification
					case 'C':  // Components specification
						compSpec = parseIdx(word, nComp);
						if (curSpecType == SPEC_TILE_DEF)
							curSpecType = SPEC_TILE_COMP;
						else
							curSpecType = SPEC_COMP_DEF;
						break;
					
					case 'w': 
					// WT filters specification
					case 'W':  // WT filters specification
						if (pl.getBooleanParameter("lossless") && word.ToUpper().Equals("w9x7".ToUpper()))
						{
							throw new System.ArgumentException("Cannot use non " + "reversible " + "wavelet transform with" + " '-lossless' option");
						}
						
						filter = parseFilters(word);
						if (curSpecType == SPEC_DEF)
						{
							setDefault(filter);
						}
						else if (curSpecType == SPEC_TILE_DEF)
						{
							for (int i = tileSpec.Length - 1; i >= 0; i--)
								if (tileSpec[i])
								{
									setTileDef(i, filter);
								}
						}
						else if (curSpecType == SPEC_COMP_DEF)
						{
							for (int i = compSpec.Length - 1; i >= 0; i--)
								if (compSpec[i])
								{
									setCompDef(i, filter);
								}
						}
						else
						{
							for (int i = tileSpec.Length - 1; i >= 0; i--)
							{
								for (int j = compSpec.Length - 1; j >= 0; j--)
								{
									if (tileSpec[i] && compSpec[j])
									{
										setTileCompVal(i, j, filter);
									}
								}
							}
						}
						
						// Re-initialize
						curSpecType = SPEC_DEF;
						tileSpec = null;
						compSpec = null;
						break;
					
					
					default: 
						throw new System.ArgumentException("Bad construction for " + "parameter: " + word);
					
				}
			}
			
			// Check that default value has been specified
			if (getDefault() == null)
			{
				int ndefspec = 0;
				for (int t = nt - 1; t >= 0; t--)
				{
					for (int c = nc - 1; c >= 0; c--)
					{
						if (specValType[t][c] == SPEC_DEF)
						{
							ndefspec++;
						}
					}
				}
				
				// If some tile-component have received no specification, it takes
				// the default value defined in ParameterList
				if (ndefspec != 0)
				{
					if (((System.String) qts.getDefault()).Equals("reversible"))
						setDefault(parseFilters(REV_FILTER_STR));
					else
						setDefault(parseFilters(NON_REV_FILTER_STR));
				}
				else
				{
					// All tile-component have been specified, takes the first
					// tile-component value as default.
					setDefault(getTileCompVal(0, 0));
					switch (specValType[0][0])
					{
						
						case SPEC_TILE_DEF: 
							for (int c = nc - 1; c >= 0; c--)
							{
								if (specValType[0][c] == SPEC_TILE_DEF)
									specValType[0][c] = SPEC_DEF;
							}
							tileDef[0] = null;
							break;
						
						case SPEC_COMP_DEF: 
							for (int t = nt - 1; t >= 0; t--)
							{
								if (specValType[t][0] == SPEC_COMP_DEF)
									specValType[t][0] = SPEC_DEF;
							}
							compDef[0] = null;
							break;
						
						case SPEC_TILE_COMP: 
							specValType[0][0] = SPEC_DEF;
							tileCompVal["t0c0"] = null;
							break;
						}
				}
			}
			
			// Check consistency between filter and quantization type
			// specification
			for (int t = nt - 1; t >= 0; t--)
			{
				for (int c = nc - 1; c >= 0; c--)
				{
					// Reversible quantization
					if (((System.String) qts.getTileCompVal(t, c)).Equals("reversible"))
					{
						// If filter is reversible, it is OK
						if (isReversible(t, c))
							continue;
						
						// If no filter has been defined, use reversible filter
						if (!isFilterSpecified)
						{
							setTileCompVal(t, c, parseFilters(REV_FILTER_STR));
						}
						else
						{
							// Non reversible filter specified -> Error
							throw new System.ArgumentException("Filter of " + "tile-component" + " (" + t + "," + c + ") does" + " not allow " + "reversible " + "quantization. " + "Specify '-Qtype " + "expounded' or " + "'-Qtype derived'" + "in " + "the command line.");
						}
					}
					else
					{
						// No reversible quantization
						// No reversible filter -> OK
						if (!isReversible(t, c))
							continue;
						
						// If no filter has been specified, use non-reversible
						// filter
						if (!isFilterSpecified)
						{
							setTileCompVal(t, c, parseFilters(NON_REV_FILTER_STR));
						}
						else
						{
							// Reversible filter specified -> Error
							throw new System.ArgumentException("Filter of " + "tile-component" + " (" + t + "," + c + ") does" + " not allow " + "non-reversible " + "quantization. " + "Specify '-Qtype " + "reversible' in " + "the command line");
						}
					}
				}
			}
		}
		
		/// <summary> Parse filters from the given word
		/// 
		/// </summary>
		/// <param name="word">String to parse
		/// 
		/// </param>
		/// <returns> Analysis wavelet filter (first dimension: by direction,
		/// second dimension: by decomposition levels)
		/// </returns>
		private AnWTFilter[][] parseFilters(System.String word)
		{
			AnWTFilter[][] filt = new AnWTFilter[2][];
			for (int i = 0; i < 2; i++)
			{
				filt[i] = new AnWTFilter[1];
			}
			if (word.ToUpper().Equals("w5x3".ToUpper()))
			{
				filt[0][0] = new AnWTFilterIntLift5x3();
				filt[1][0] = new AnWTFilterIntLift5x3();
				return filt;
			}
			else if (word.ToUpper().Equals("w9x7".ToUpper()))
			{
				filt[0][0] = new AnWTFilterFloatLift9x7();
				filt[1][0] = new AnWTFilterFloatLift9x7();
				return filt;
			}
			else
			{
				throw new System.ArgumentException("Non JPEG 2000 part I filter: " + word);
			}
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
			AnWTFilter[][] an = (AnWTFilter[][]) getSpec(t, c);
			return an[0][0].DataType;
		}
		
		/// <summary> Returns the horizontal analysis filters to be used in component 'n' and 
		/// tile 't'.
		/// 
		/// <P>The horizontal analysis filters are returned in an array of
		/// AnWTFilter. Each element contains the horizontal filter for each
		/// resolution level starting with resolution level 1 (i.e. the analysis
		/// filter to go from resolution level 1 to resolution level 0). If there
		/// are less elements than the maximum resolution level, then the last
		/// element is assumed to be repeated.
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
		public virtual AnWTFilter[] getHFilters(int t, int c)
		{
			AnWTFilter[][] an = (AnWTFilter[][]) getSpec(t, c);
			return an[0];
		}
		
		/// <summary> Returns the vertical analysis filters to be used in component 'n' and 
		/// tile 't'.
		/// 
		/// <P>The vertical analysis filters are returned in an array of
		/// AnWTFilter. Each element contains the vertical filter for each
		/// resolution level starting with resolution level 1 (i.e. the analysis
		/// filter to go from resolution level 1 to resolution level 0). If there
		/// are less elements than the maximum resolution level, then the last
		/// element is assumed to be repeated.
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
		public virtual AnWTFilter[] getVFilters(int t, int c)
		{
			AnWTFilter[][] an = (AnWTFilter[][]) getSpec(t, c);
			return an[1];
		}
		
		/// <summary>Debugging method </summary>
		public override System.String ToString()
		{
			System.String str = "";
			AnWTFilter[][] an;
			
			str += ("nTiles=" + nTiles + "\nnComp=" + nComp + "\n\n");
			
			for (int t = 0; t < nTiles; t++)
			{
				for (int c = 0; c < nComp; c++)
				{
					an = (AnWTFilter[][]) getSpec(t, c);
					
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
			// Note: no need to buffer the result since this method is
			// normally called once per tile-component.
			AnWTFilter[] hfilter = getHFilters(t, c), vfilter = getVFilters(t, c);
			
			// As soon as a filter is not reversible, false can be returned
			for (int i = hfilter.Length - 1; i >= 0; i--)
				if (!hfilter[i].Reversible || !vfilter[i].Reversible)
					return false;
			return true;
		}
	}
}