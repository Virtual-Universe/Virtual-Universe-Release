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
namespace CSJ2K.j2k.quantization
{
	
	/// <summary> This class extends ModuleSpec class in order to hold specifications about
	/// the quantization base normalized step size to use in each tile-component.
	/// 
	/// </summary>
	/// <seealso cref="ModuleSpec">
	/// 
	/// </seealso>
	public class QuantStepSizeSpec:ModuleSpec
	{
		
		/// <summary> Constructs an empty 'QuantStepSizeSpec' with specified number of
		/// tile and components. This constructor is called by the decoder.
		/// 
		/// </summary>
		/// <param name="nt">Number of tiles
		/// 
		/// </param>
		/// <param name="nc">Number of components
		/// 
		/// </param>
		/// <param name="type">the type of the specification module i.e. tile specific,
		/// component specific or both.
		/// 
		/// </param>
		public QuantStepSizeSpec(int nt, int nc, byte type):base(nt, nc, type)
		{
		}
		
		/// <summary> Constructs a new 'QuantStepSizeSpec' for the specified number of
		/// components and tiles and the arguments of "-Qstep" option.
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
		/// <param name="pl">The ParameterList
		/// 
		/// </param>
		public QuantStepSizeSpec(int nt, int nc, byte type, ParameterList pl):base(nt, nc, type)
		{
			
			System.String param = pl.getParameter("Qstep");
			if (param == null)
			{
				throw new System.ArgumentException("Qstep option not specified");
			}
			
			// Parse argument
			SupportClass.Tokenizer stk = new SupportClass.Tokenizer(param);
			System.String word; // current word
			byte curSpecType = SPEC_DEF; // Specification type of the
			// current parameter
			bool[] tileSpec = null; // Tiles concerned by the specification
			bool[] compSpec = null; // Components concerned by the specification
			System.Single value_Renamed; // value of the current step size
			
			while (stk.HasMoreTokens())
			{
				word = stk.NextToken().ToLower();
				
				switch (word[0])
				{
					
					case 't':  // Tiles specification
						tileSpec = parseIdx(word, nTiles);
						if (curSpecType == SPEC_COMP_DEF)
							curSpecType = SPEC_TILE_COMP;
						else
							curSpecType = SPEC_TILE_DEF;
						break;
					
					case 'c':  // Components specification
						compSpec = parseIdx(word, nComp);
						if (curSpecType == SPEC_TILE_DEF)
							curSpecType = SPEC_TILE_COMP;
						else
							curSpecType = SPEC_COMP_DEF;
						break;
					
					default:  // Step size value
						try
						{
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							value_Renamed = System.Single.Parse(word);
						}
						catch (System.FormatException)
						{
							throw new System.ArgumentException("Bad parameter for " + "-Qstep option : " + word);
						}
						
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Float.floatValue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						if ((float) value_Renamed <= 0.0f)
						{
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Float.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							throw new System.ArgumentException("Normalized base step " + "must be positive : " + value_Renamed);
						}
						
						
						if (curSpecType == SPEC_DEF)
						{
							setDefault((System.Object) value_Renamed);
						}
						else if (curSpecType == SPEC_TILE_DEF)
						{
							for (int i = tileSpec.Length - 1; i >= 0; i--)
								if (tileSpec[i])
								{
									setTileDef(i, (System.Object) value_Renamed);
								}
						}
						else if (curSpecType == SPEC_COMP_DEF)
						{
							for (int i = compSpec.Length - 1; i >= 0; i--)
								if (compSpec[i])
								{
									setCompDef(i, (System.Object) value_Renamed);
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
										setTileCompVal(i, j, (System.Object) value_Renamed);
									}
								}
							}
						}
						
						// Re-initialize
						curSpecType = SPEC_DEF;
						tileSpec = null;
						compSpec = null;
						break;
					
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
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					setDefault((System.Object) System.Single.Parse(pl.DefaultParameterList.getParameter("Qstep")));
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
		}
	}
}