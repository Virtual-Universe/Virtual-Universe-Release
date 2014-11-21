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
using CSJ2K.j2k.codestream;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k;
namespace CSJ2K.j2k.entropy
{
	
	/// <summary> This class extends ModuleSpec class for code-blocks sizes holding purposes.
	/// 
	/// <P>It stores the size a of code-block. 
	/// 
	/// </summary>
	public class CBlkSizeSpec:ModuleSpec
	{
		/// <summary> Returns the maximum code-block's width
		/// 
		/// </summary>
		virtual public int MaxCBlkWidth
		{
			get
			{
				return maxCBlkWidth;
			}
			
		}
		/// <summary> Returns the maximum code-block's height
		/// 
		/// </summary>
		virtual public int MaxCBlkHeight
		{
			get
			{
				return maxCBlkHeight;
			}
			
		}
		
		/// <summary>Name of the option </summary>
		private const System.String optName = "Cblksiz";
		
		/// <summary>The maximum code-block width </summary>
		private int maxCBlkWidth = 0;
		
		/// <summary>The maximum code-block height </summary>
		private int maxCBlkHeight = 0;
		
		/// <summary> Creates a new CBlkSizeSpec object for the specified number of tiles and
		/// components.
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
		public CBlkSizeSpec(int nt, int nc, byte type):base(nt, nc, type)
		{
		}
		
		/// <summary> Creates a new CBlkSizeSpec object for the specified number of tiles and
		/// components and the ParameterList instance.
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
		/// <param name="imgsrc">The image source (used to get the image size)
		/// 
		/// </param>
		/// <param name="pl">The ParameterList instance
		/// 
		/// </param>
		public CBlkSizeSpec(int nt, int nc, byte type, ParameterList pl):base(nt, nc, type)
		{
			
			bool firstVal = true;
			System.String param = pl.getParameter(optName);
			
			// Precinct partition is used : parse arguments
			SupportClass.Tokenizer stk = new SupportClass.Tokenizer(param);
			byte curSpecType = SPEC_DEF; // Specification type of the
			// current parameter
			bool[] tileSpec = null; // Tiles concerned by the specification
			bool[] compSpec = null; // Components concerned by the specification
            int ci, ti; //  i, xIdx removed
			System.String word = null; // current word
			System.String errMsg = null;
			
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
						compSpec = parseIdx(word, nComp);
						if (curSpecType == SPEC_TILE_DEF)
						{
							curSpecType = SPEC_TILE_COMP;
						}
						else
						{
							curSpecType = SPEC_COMP_DEF;
						}
						break;
					
					
					default: 
						if (!System.Char.IsDigit(word[0]))
						{
							errMsg = "Bad construction for parameter: " + word;
							throw new System.ArgumentException(errMsg);
						}
						System.Int32[] dim = new System.Int32[2];
						// Get code-block's width
						try
						{
							dim[0] = System.Int32.Parse(word);
							// Check that width is not >
							// StdEntropyCoderOptions.MAX_CB_DIM
							if (dim[0] > CSJ2K.j2k.entropy.StdEntropyCoderOptions.MAX_CB_DIM)
							{
								errMsg = "'" + optName + "' option : the code-block's " + "width cannot be greater than " + CSJ2K.j2k.entropy.StdEntropyCoderOptions.MAX_CB_DIM;
								throw new System.ArgumentException(errMsg);
							}
							// Check that width is not <
							// StdEntropyCoderOptions.MIN_CB_DIM
							if (dim[0] < CSJ2K.j2k.entropy.StdEntropyCoderOptions.MIN_CB_DIM)
							{
								errMsg = "'" + optName + "' option : the code-block's " + "width cannot be less than " + CSJ2K.j2k.entropy.StdEntropyCoderOptions.MIN_CB_DIM;
								throw new System.ArgumentException(errMsg);
							}
							// Check that width is a power of 2
							if (dim[0] != (1 << MathUtil.log2(dim[0])))
							{
								errMsg = "'" + optName + "' option : the code-block's " + "width must be a power of 2";
								throw new System.ArgumentException(errMsg);
							}
						}
						catch (System.FormatException)
						{
							errMsg = "'" + optName + "' option : the code-block's " + "width could not be parsed.";
							throw new System.ArgumentException(errMsg);
						}
						// Get the next word in option
						try
						{
							word = stk.NextToken();
						}
						catch (System.ArgumentOutOfRangeException)
						{
							errMsg = "'" + optName + "' option : could not parse the " + "code-block's height";
							throw new System.ArgumentException(errMsg);
						}
						// Get the code-block's height
						try
						{
							dim[1] = System.Int32.Parse(word);
							// Check that height is not >
							// StdEntropyCoderOptions.MAX_CB_DIM
							if (dim[1] > CSJ2K.j2k.entropy.StdEntropyCoderOptions.MAX_CB_DIM)
							{
								errMsg = "'" + optName + "' option : the code-block's " + "height cannot be greater than " + CSJ2K.j2k.entropy.StdEntropyCoderOptions.MAX_CB_DIM;
								throw new System.ArgumentException(errMsg);
							}
							// Check that height is not <
							// StdEntropyCoderOptions.MIN_CB_DIM
							if (dim[1] < CSJ2K.j2k.entropy.StdEntropyCoderOptions.MIN_CB_DIM)
							{
								errMsg = "'" + optName + "' option : the code-block's " + "height cannot be less than " + CSJ2K.j2k.entropy.StdEntropyCoderOptions.MIN_CB_DIM;
								throw new System.ArgumentException(errMsg);
							}
							// Check that height is a power of 2
							if (dim[1] != (1 << MathUtil.log2(dim[1])))
							{
								errMsg = "'" + optName + "' option : the code-block's " + "height must be a power of 2";
								throw new System.ArgumentException(errMsg);
							}
							// Check that the code-block 'area' (i.e. width*height) is
							// not greater than StdEntropyCoderOptions.MAX_CB_AREA
							if (dim[0] * dim[1] > CSJ2K.j2k.entropy.StdEntropyCoderOptions.MAX_CB_AREA)
							{
								errMsg = "'" + optName + "' option : The " + "code-block's area (i.e. width*height) " + "cannot be greater than " + CSJ2K.j2k.entropy.StdEntropyCoderOptions.MAX_CB_AREA;
								throw new System.ArgumentException(errMsg);
							}
						}
						catch (System.FormatException)
						{
							errMsg = "'" + optName + "' option : the code-block's height " + "could not be parsed.";
							throw new System.ArgumentException(errMsg);
						}
						
						// Store the maximum dimensions if necessary
						if (dim[0] > maxCBlkWidth)
						{
							maxCBlkWidth = dim[0];
						}
						
						if (dim[1] > maxCBlkHeight)
						{
							maxCBlkHeight = dim[1];
						}
						
						if (firstVal)
						{
							// This is the first time a value is given so we set it as
							// the default one 
							setDefault((System.Object) (dim));
							firstVal = false;
						}
						
						switch (curSpecType)
						{
							
							case SPEC_DEF: 
								setDefault((System.Object) (dim));
								break;
							
							case SPEC_TILE_DEF: 
								for (ti = tileSpec.Length - 1; ti >= 0; ti--)
								{
									if (tileSpec[ti])
									{
										setTileDef(ti, (System.Object) (dim));
									}
								}
								break;
							
							case SPEC_COMP_DEF: 
								for (ci = compSpec.Length - 1; ci >= 0; ci--)
								{
									if (compSpec[ci])
									{
										setCompDef(ci, (System.Object) (dim));
									}
								}
								break;
							
							default: 
								for (ti = tileSpec.Length - 1; ti >= 0; ti--)
								{
									for (ci = compSpec.Length - 1; ci >= 0; ci--)
									{
										if (tileSpec[ti] && compSpec[ci])
										{
											setTileCompVal(ti, ci, (System.Object) (dim));
										}
									}
								}
								break;
							
						}
						break;
					
				} // end switch
			}
		}
		
		/// <summary> Returns the code-block width :
		/// 
		/// <ul>
		/// <li>for the specified tile/component</li>
		/// <li>for the specified tile</li>
		/// <li>for the specified component</li>
		/// <li>default value</li>
		/// </ul>
		/// 
		/// The value returned depends on the value of the variable 'type' which
		/// can take the following values :<br>
		/// 
		/// <ul> 
		/// <li>SPEC_DEF -> Default value is returned. t and c values are
		/// ignored</li> 
		/// <li>SPEC_COMP_DEF -> Component default value is returned. t value is
		/// ignored</li>
		/// <li>SPEC_TILE_DEF -> Tile default value is returned. c value is
		/// ignored</li>
		/// <li>SPEC_TILE_COMP -> Tile/Component value is returned.</li>
		/// </ul>
		/// 
		/// </summary>
		/// <param name="type">The type of the value we want to be returned
		/// 
		/// </param>
		/// <param name="t">The tile index
		/// 
		/// </param>
		/// <param name="c">the component index
		/// 
		/// </param>
		/// <returns> The code-block width for the specified tile and component
		/// 
		/// </returns>
		public virtual int getCBlkWidth(byte type, int t, int c)
		{
			//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			System.Int32[] dim = null;
			switch (type)
			{
				
				case SPEC_DEF: 
					dim = (System.Int32[]) getDefault();
					break;
				
				case SPEC_COMP_DEF: 
					dim = (System.Int32[]) getCompDef(c);
					break;
				
				case SPEC_TILE_DEF: 
					dim = (System.Int32[]) getTileDef(t);
					break;
				
				case SPEC_TILE_COMP: 
					dim = (System.Int32[]) getTileCompVal(t, c);
					break;
				}
			return dim[0];
		}
		
		/// <summary> Returns the code-block height:
		/// 
		/// <ul>
		/// <li>for the specified tile/component</li>
		/// <li>for the specified tile</li>
		/// <li>for the specified component</li>
		/// <li>default value</li>
		/// </ul>
		/// 
		/// The value returned depends on the value of the variable 'type' which
		/// can take the following values :
		/// 
		/// <ul> 
		/// <li>SPEC_DEF -> Default value is returned. t and c values are
		/// ignored</li> 
		/// <li>SPEC_COMP_DEF -> Component default value is returned. t value is
		/// ignored</li>
		/// <li>SPEC_TILE_DEF -> Tile default value is returned. c value is
		/// ignored</li>
		/// <li>SPEC_TILE_COMP -> Tile/Component value is returned.</li>
		/// </ul>
		/// 
		/// </summary>
		/// <param name="type">The type of the value we want to be returned
		/// 
		/// </param>
		/// <param name="t">The tile index
		/// 
		/// </param>
		/// <param name="c">the component index
		/// 
		/// </param>
		/// <returns> The code-block height for the specified tile and component
		/// 
		/// </returns>
		public virtual int getCBlkHeight(byte type, int t, int c)
		{
			//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			System.Int32[] dim = null;
			switch (type)
			{
				
				case SPEC_DEF: 
					dim = (System.Int32[]) getDefault();
					break;
				
				case SPEC_COMP_DEF: 
					dim = (System.Int32[]) getCompDef(c);
					break;
				
				case SPEC_TILE_DEF: 
					dim = (System.Int32[]) getTileDef(t);
					break;
				
				case SPEC_TILE_COMP: 
					dim = (System.Int32[]) getTileCompVal(t, c);
					break;
				}
			return dim[1];
		}
		
		/// <summary> Sets default value for this module 
		/// 
		/// </summary>
		/// <param name="value">Default value
		/// 
		/// </param>
		public override void  setDefault(System.Object value_Renamed)
		{
			base.setDefault(value_Renamed);
			
			// Store the biggest code-block dimensions
			storeHighestDims((System.Int32[]) value_Renamed);
		}
		
		/// <summary> Sets default value for specified tile and specValType tag if allowed by
		/// its priority.
		/// 
		/// </summary>
		/// <param name="c">Tile index.
		/// 
		/// </param>
		/// <param name="value">Tile's default value
		/// 
		/// </param>
		public override void  setTileDef(int t, System.Object value_Renamed)
		{
			base.setTileDef(t, value_Renamed);
			
			// Store the biggest code-block dimensions
			storeHighestDims((System.Int32[]) value_Renamed);
		}
		
		/// <summary> Sets default value for specified component and specValType tag if
		/// allowed by its priority.
		/// 
		/// </summary>
		/// <param name="c">Component index 
		/// 
		/// </param>
		/// <param name="value">Component's default value
		/// 
		/// </param>
		public override void  setCompDef(int c, System.Object value_Renamed)
		{
			base.setCompDef(c, value_Renamed);
			
			// Store the biggest code-block dimensions
			storeHighestDims((System.Int32[]) value_Renamed);
		}
		
		/// <summary> Sets value for specified tile-component.
		/// 
		/// </summary>
		/// <param name="t">Tie index 
		/// 
		/// </param>
		/// <param name="c">Component index 
		/// 
		/// </param>
		/// <param name="value">Tile-component's value
		/// 
		/// </param>
		public override void  setTileCompVal(int t, int c, System.Object value_Renamed)
		{
			base.setTileCompVal(t, c, value_Renamed);
			
			// Store the biggest code-block dimensions
			storeHighestDims((System.Int32[]) value_Renamed);
		}
		
		/// <summary> Stores the highest code-block width and height
		/// 
		/// </summary>
		/// <param name="dim">The 2 elements array that contains the code-block width and
		/// height.
		/// 
		/// </param>
		private void  storeHighestDims(System.Int32[] dim)
		{
			// Store the biggest code-block dimensions
			if (dim[0] > maxCBlkWidth)
			{
				maxCBlkWidth = dim[0];
			}
			if (dim[1] > maxCBlkHeight)
			{
				maxCBlkHeight = dim[1];
			}
		}
	}
}