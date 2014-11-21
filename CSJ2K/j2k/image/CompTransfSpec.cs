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
using CSJ2K.j2k.image.invcomptransf;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.util;
using CSJ2K.j2k;
namespace CSJ2K.j2k.image
{
	
	/// <summary> This class extends the ModuleSpec class in order to hold tile
	/// specifications for multiple component transformation
	/// 
	/// </summary>
	/// <seealso cref="ModuleSpec">
	/// 
	/// </seealso>
	public class CompTransfSpec:ModuleSpec
	{
		/// <summary> Check if component transformation is used in any of the tiles. This
		/// method must not be used by the encoder.
		/// 
		/// </summary>
		/// <returns> True if a component transformation is used in at least on
		/// tile.
		/// 
		/// </returns>
		virtual public bool CompTransfUsed
		{
			get
			{
				if (((System.Int32) def) != InvCompTransf.NONE)
				{
					return true;
				}
				
				if (tileDef != null)
				{
					for (int t = nTiles - 1; t >= 0; t--)
					{
						if (tileDef[t] != null && (((System.Int32) tileDef[t]) != InvCompTransf.NONE))
						{
							return true;
						}
					}
				}
				return false;
			}
			
		}
		
		/// <summary> Constructs an empty 'CompTransfSpec' with the specified number of tiles
		/// and components. This constructor is called by the decoder. Note: The
		/// number of component is here for symmetry purpose. It is useless since
		/// only tile specifications are meaningful.
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
		public CompTransfSpec(int nt, int nc, byte type):base(nt, nc, type)
		{
		}
	}
}