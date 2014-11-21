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
namespace CSJ2K.j2k
{
	
	/// <summary> This class extends ModuleSpec class in order to hold tile-component
	/// specifications using Strings.
	/// 
	/// </summary>
	/// <seealso cref="ModuleSpec">
	/// 
	/// </seealso>
	public class StringSpec:ModuleSpec
	{
		
		/// <summary> Constructs an empty 'StringSpec' with specified number of
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
		public StringSpec(int nt, int nc, byte type):base(nt, nc, type)
		{
		}
		
		/// <summary> Constructs a new 'StringSpec' for the specified number of
		/// components:tiles and the arguments of <tt>optName</tt>
		/// option. This constructor is called by the encoder. It also
		/// checks that the arguments belongs to the recognized arguments
		/// list.
		/// 
		/// <P><u>Note:</u> The arguments must not start with 't' or 'c'
		/// since it is reserved for respectively tile and components
		/// indexes specification.
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
		/// <param name="name">of the option using boolean spec.
		/// 
		/// </param>
		/// <param name="list">The list of all recognized argument in a String array
		/// 
		/// </param>
		/// <param name="pl">The ParameterList
		/// 
		/// </param>
		public StringSpec(int nt, int nc, byte type, System.String optName, System.String[] list, ParameterList pl):base(nt, nc, type)
		{
			
			System.String param = pl.getParameter(optName);
			bool recognized = false;
			
			if (param == null)
			{
				param = pl.DefaultParameterList.getParameter(optName);
				for (int i = list.Length - 1; i >= 0; i--)
					if (param.ToUpper().Equals(list[i].ToUpper()))
						recognized = true;
				if (!recognized)
					throw new System.ArgumentException("Default parameter of " + "option -" + optName + " not" + " recognized: " + param);
				setDefault(param);
				return ;
			}
			
			// Parse argument
			SupportClass.Tokenizer stk = new SupportClass.Tokenizer(param);
			System.String word; // current word
			byte curSpecType = SPEC_DEF; // Specification type of the
			// current parameter
			bool[] tileSpec = null; // Tiles concerned by the
			// specification
			bool[] compSpec = null; // Components concerned by the specification
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
						compSpec = parseIdx(word, nComp);
						if (curSpecType == SPEC_TILE_DEF)
						{
							curSpecType = SPEC_TILE_COMP;
						}
						else
							curSpecType = SPEC_COMP_DEF;
						break;
					
					default: 
						recognized = false;
						
						for (int i = list.Length - 1; i >= 0; i--)
							if (word.ToUpper().Equals(list[i].ToUpper()))
								recognized = true;
						if (!recognized)
							throw new System.ArgumentException("Default parameter of " + "option -" + optName + " not" + " recognized: " + word);
						
						if (curSpecType == SPEC_DEF)
						{
							setDefault(word);
						}
						else if (curSpecType == SPEC_TILE_DEF)
						{
							for (int i = tileSpec.Length - 1; i >= 0; i--)
								if (tileSpec[i])
								{
									setTileDef(i, word);
								}
						}
						else if (curSpecType == SPEC_COMP_DEF)
						{
							for (int i = compSpec.Length - 1; i >= 0; i--)
								if (compSpec[i])
								{
									setCompDef(i, word);
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
										setTileCompVal(i, j, word);
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
					param = pl.DefaultParameterList.getParameter(optName);
					for (int i = list.Length - 1; i >= 0; i--)
						if (param.ToUpper().Equals(list[i].ToUpper()))
							recognized = true;
					if (!recognized)
						throw new System.ArgumentException("Default parameter of " + "option -" + optName + " not" + " recognized: " + param);
					setDefault(param);
				}
				else
				{
					// All tile-component have been specified, takes the first
					// tile-component value as default.
					setDefault(getSpec(0, 0));
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