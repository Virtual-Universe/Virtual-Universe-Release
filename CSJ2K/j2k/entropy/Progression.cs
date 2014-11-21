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
namespace CSJ2K.j2k.entropy
{
	
	/// <summary> This class holds one of the different progression orders defined in
	/// the bit stream. The type(s) of progression order are defined in the
	/// ProgressionType interface. A Progression object is totally defined
	/// by its component start and end, resolution level start and end and
	/// layer start and end indexes. If no progression order change is
	/// defined, there is only Progression instance. 
	/// 
	/// </summary>
	/// <seealso cref="ProgressionType">
	/// 
	/// </seealso>
	public class Progression
	{
		
		/// <summary>Progression type as defined in ProgressionType interface </summary>
		public int type;
		
		/// <summary>Component index for the start of a progression </summary>
		public int cs;
		
		/// <summary>Component index for the end of a progression. </summary>
		public int ce;
		
		/// <summary>Resolution index for the start of a progression </summary>
		public int rs;
		
		/// <summary>Resolution index for the end of a progression. </summary>
		public int re;
		
		/// <summary>The index of the last layer. </summary>
		public int lye;
		
		/// <summary> Constructor. 
		/// 
		/// Builds a new Progression object with specified type and bounds
		/// of progression.
		/// 
		/// </summary>
		/// <param name="type">The progression type
		/// 
		/// </param>
		/// <param name="cs">The component index start
		/// 
		/// </param>
		/// <param name="ce">The component index end
		/// 
		/// </param>
		/// <param name="rs">The resolution level index start
		/// 
		/// </param>
		/// <param name="re">The resolution level index end
		/// 
		/// </param>
		/// <param name="lye">The layer index end
		/// 
		/// </param>
		public Progression(int type, int cs, int ce, int rs, int re, int lye)
		{
			this.type = type;
			this.cs = cs;
			this.ce = ce;
			this.rs = rs;
			this.re = re;
			this.lye = lye;
		}
		
		public override System.String ToString()
		{
			System.String str = "type= ";
			switch (type)
			{
				
				case CSJ2K.j2k.codestream.ProgressionType.LY_RES_COMP_POS_PROG: 
					str += "layer, ";
					break;
				
				case CSJ2K.j2k.codestream.ProgressionType.RES_LY_COMP_POS_PROG: 
					str += "res, ";
					break;
				
				case CSJ2K.j2k.codestream.ProgressionType.RES_POS_COMP_LY_PROG: 
					str += "res-pos, ";
					break;
				
				case CSJ2K.j2k.codestream.ProgressionType.POS_COMP_RES_LY_PROG: 
					str += "pos-comp, ";
					break;
				
				case CSJ2K.j2k.codestream.ProgressionType.COMP_POS_RES_LY_PROG: 
					str += "pos-comp, ";
					break;
				
				default: 
					throw new System.ApplicationException("Unknown progression type");
				
			}
			str += ("comp.: " + cs + "-" + ce + ", ");
			str += ("res.: " + rs + "-" + re + ", ");
			str += ("layer: up to " + lye);
			return str;
		}
	}
}