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
	
	/// <summary> This class extends ModuleSpec class for progression type(s) and progression
	/// order changes holding purposes.
	/// 
	/// <p>It stores  the progression type(s) used in the  codestream. There can be
	/// several progression  type(s) if  progression order  changes are  used (POC
	/// markers).</p>
	/// 
	/// </summary>
	public class ProgressionSpec:ModuleSpec
	{
		
		/// <summary> Creates a new ProgressionSpec object for the specified number of tiles
		/// and components.
		/// 
		/// </summary>
		/// <param name="nt">The number of tiles
		/// 
		/// </param>
		/// <param name="nc">The number of components
		/// 
		/// </param>
		/// <param name="type">the type of the specification module i.e. tile specific,
		/// component specific or both. The ProgressionSpec class should only be
		/// used only with the type ModuleSpec.SPEC_TYPE_TILE.
		/// 
		/// </param>
		public ProgressionSpec(int nt, int nc, byte type):base(nt, nc, type)
		{
			if (type != ModuleSpec.SPEC_TYPE_TILE)
			{
				throw new System.ApplicationException("Illegal use of class ProgressionSpec !");
			}
		}
		
		/// <summary> Creates a new ProgressionSpec object for the specified number of tiles,
		/// components and the ParameterList instance.
		/// 
		/// </summary>
		/// <param name="nt">The number of tiles
		/// 
		/// </param>
		/// <param name="nc">The number of components
		/// 
		/// </param>
		/// <param name="nl">The number of layer
		/// 
		/// </param>
		/// <param name="dls">The number of decomposition levels specifications
		/// 
		/// </param>
		/// <param name="type">the type of the specification module. The ProgressionSpec
		/// class should only be used only with the type ModuleSpec.SPEC_TYPE_TILE.
		/// 
		/// </param>
		/// <param name="pl">The ParameterList instance
		/// 
		/// </param>
		public ProgressionSpec(int nt, int nc, int nl, IntegerSpec dls, byte type, ParameterList pl):base(nt, nc, type)
		{
			
			System.String param = pl.getParameter("Aptype");
			Progression[] prog;
			int mode = - 1;
			
			if (param == null)
			{
				// No parameter specified
				if (pl.getParameter("Rroi") == null)
				{
					mode = checkProgMode("res");
				}
				else
				{
					mode = checkProgMode("layer");
				}
				
				if (mode == - 1)
				{
					System.String errMsg = "Unknown progression type : '" + param + "'";
					throw new System.ArgumentException(errMsg);
				}
				prog = new Progression[1];
				prog[0] = new Progression(mode, 0, nc, 0, dls.Max + 1, nl);
				setDefault(prog);
				return ;
			}
			
			SupportClass.Tokenizer stk = new SupportClass.Tokenizer(param);
			byte curSpecType = SPEC_DEF; // Specification type of the
			// current parameter
			bool[] tileSpec = null; // Tiles concerned by the specification
			System.String word = null; // current word
			System.String errMsg2 = null; // Error message
			bool needInteger = false; // True if an integer value is expected
			int intType = 0; // Type of read integer value (0=index of first
			// resolution level, 1= index of first component, 2=index of first  
			// layer not included, 3= index of first resolution level not
			// included, 4= index of  first component not included
			System.Collections.ArrayList progression = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			int tmp = 0;
			Progression curProg = null;
			
			while (stk.HasMoreTokens())
			{
				word = stk.NextToken();
				
				switch (word[0])
				{
					
					case 't': 
						// If progression were previously found, store them
						if (progression.Count > 0)
						{
							// Ensure that all information has been taken
							curProg.ce = nc;
							curProg.lye = nl;
							curProg.re = dls.Max + 1;
							prog = new Progression[progression.Count];
							progression.CopyTo(prog);
							if (curSpecType == SPEC_DEF)
							{
								setDefault(prog);
							}
							else if (curSpecType == SPEC_TILE_DEF)
							{
								for (int i = tileSpec.Length - 1; i >= 0; i--)
									if (tileSpec[i])
									{
										setTileDef(i, prog);
									}
							}
						}
						progression.Clear();
						intType = - 1;
						needInteger = false;
						
						// Tiles specification
						tileSpec = parseIdx(word, nTiles);
						curSpecType = SPEC_TILE_DEF;
						break;
					
					default: 
						// Here, words is either a Integer (progression bound index)
						// or a String (progression order type). This is determined by
						// the value of needInteger.
						if (needInteger)
						{
							// Progression bound info
							try
							{
								tmp = (System.Int32.Parse(word));
							}
							catch (System.FormatException)
							{
								// Progression has missing parameters
								throw new System.ArgumentException("Progression " + "order" + " specification " + "has missing " + "parameters: " + param);
							}
							
							switch (intType)
							{
								
								case 0:  // cs
									if (tmp < 0 || tmp > (dls.Max + 1))
										throw new System.ArgumentException("Invalid res_start " + "in '-Aptype'" + " option: " + tmp);
									curProg.rs = tmp; break;
								
								case 1:  // rs
									if (tmp < 0 || tmp > nc)
									{
										throw new System.ArgumentException("Invalid comp_start " + "in '-Aptype' " + "option: " + tmp);
									}
									curProg.cs = tmp; break;
								
								case 2:  // lye
									if (tmp < 0)
										throw new System.ArgumentException("Invalid layer_end " + "in '-Aptype'" + " option: " + tmp);
									if (tmp > nl)
									{
										tmp = nl;
									}
									curProg.lye = tmp; break;
								
								case 3:  // ce
									if (tmp < 0)
										throw new System.ArgumentException("Invalid res_end " + "in '-Aptype'" + " option: " + tmp);
									if (tmp > (dls.Max + 1))
									{
										tmp = dls.Max + 1;
									}
									curProg.re = tmp; break;
								
								case 4:  // re
									if (tmp < 0)
										throw new System.ArgumentException("Invalid comp_end " + "in '-Aptype'" + " option: " + tmp);
									if (tmp > nc)
									{
										tmp = nc;
									}
									curProg.ce = tmp; break;
								}
							
							if (intType < 4)
							{
								intType++;
								needInteger = true;
								break;
							}
							else if (intType == 4)
							{
								intType = 0;
								needInteger = false;
								break;
							}
							else
							{
								throw new System.ApplicationException("Error in usage of 'Aptype' " + "option: " + param);
							}
						}
						
						if (!needInteger)
						{
							// Progression type info
							mode = checkProgMode(word);
							if (mode == - 1)
							{
								errMsg2 = "Unknown progression type : '" + word + "'";
								throw new System.ArgumentException(errMsg2);
							}
							needInteger = true;
							intType = 0;
							if (progression.Count == 0)
							{
								curProg = new Progression(mode, 0, nc, 0, dls.Max + 1, nl);
							}
							else
							{
								curProg = new Progression(mode, 0, nc, 0, dls.Max + 1, nl);
							}
							progression.Add(curProg);
						}
						break;
					
				} // switch
			} // while 
			
			if (progression.Count == 0)
			{
				// No progression defined
				if (pl.getParameter("Rroi") == null)
				{
					mode = checkProgMode("res");
				}
				else
				{
					mode = checkProgMode("layer");
				}
				if (mode == - 1)
				{
					errMsg2 = "Unknown progression type : '" + param + "'";
					throw new System.ArgumentException(errMsg2);
				}
				prog = new Progression[1];
				prog[0] = new Progression(mode, 0, nc, 0, dls.Max + 1, nl);
				setDefault(prog);
				return ;
			}
			
			// Ensure that all information has been taken
			curProg.ce = nc;
			curProg.lye = nl;
			curProg.re = dls.Max + 1;
			
			// Store found progression
			prog = new Progression[progression.Count];
			progression.CopyTo(prog);
			
			if (curSpecType == SPEC_DEF)
			{
				setDefault(prog);
			}
			else if (curSpecType == SPEC_TILE_DEF)
			{
				for (int i = tileSpec.Length - 1; i >= 0; i--)
					if (tileSpec[i])
					{
						setTileDef(i, prog);
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
				
				// If some tile-component have received no specification, they
				// receive the default progressiveness.
				if (ndefspec != 0)
				{
					if (pl.getParameter("Rroi") == null)
					{
						mode = checkProgMode("res");
					}
					else
					{
						mode = checkProgMode("layer");
					}
					if (mode == - 1)
					{
						errMsg2 = "Unknown progression type : '" + param + "'";
						throw new System.ArgumentException(errMsg2);
					}
					prog = new Progression[1];
					prog[0] = new Progression(mode, 0, nc, 0, dls.Max + 1, nl);
					setDefault(prog);
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
		
		/// <summary> Check if the progression mode exists and if so, return its integer
		/// value. It returns -1 otherwise.
		/// 
		/// </summary>
		/// <param name="mode">The progression mode stored in a string
		/// 
		/// </param>
		/// <returns> The integer value of the progression mode or -1 if the
		/// progression mode does not exist.
		/// 
		/// </returns>
		/// <seealso cref="ProgressionType">
		/// 
		/// </seealso>
		private int checkProgMode(System.String mode)
		{
			if (mode.Equals("res"))
			{
				return CSJ2K.j2k.codestream.ProgressionType.RES_LY_COMP_POS_PROG;
			}
			else if (mode.Equals("layer"))
			{
				return CSJ2K.j2k.codestream.ProgressionType.LY_RES_COMP_POS_PROG;
			}
			else if (mode.Equals("pos-comp"))
			{
				return CSJ2K.j2k.codestream.ProgressionType.POS_COMP_RES_LY_PROG;
			}
			else if (mode.Equals("comp-pos"))
			{
				return CSJ2K.j2k.codestream.ProgressionType.COMP_POS_RES_LY_PROG;
			}
			else if (mode.Equals("res-pos"))
			{
				return CSJ2K.j2k.codestream.ProgressionType.RES_POS_COMP_LY_PROG;
			}
			else
			{
				// No corresponding progression mode, we return -1.
				return - 1;
			}
		}
	}
}