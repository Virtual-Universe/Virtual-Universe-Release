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
namespace CSJ2K.j2k.codestream
{
	
	/// <summary> This interface defines the identifiers for the different codestream
	/// profiles and progression types.
	/// 
	/// <p>Each progressive type has a different number: 'LY_RES_COMP_POS_PROG',
	/// 'RES_LY_COMP_POS_PROG', 'RES_POS_COMP_LY_PROG', 'POS_COMP_RES_LY_PROG' or
	/// 'COMP_POS_RES_LY_PROG'.  These are the same identifiers are used in the
	/// codestream syntax.
	/// 
	/// <p>This interface defines the constants only. In order to use the constants
	/// in any other class you can either use the fully qualified name (e.g.,
	/// <tt>ProgressionType.LY_RES_COMP_POS_PROG</tt>) or declare this interface in
	/// the implements clause of the class and then access the identifier
	/// directly.</p>
	/// 
	/// </summary>
	public struct ProgressionType
    {
		/// <summary>The codestream is Layer/Resolution/Component/Position progressive : 0
		/// 
		/// </summary>
		public const int LY_RES_COMP_POS_PROG = 0;
		/// <summary>The codestream is Resolution/Layer/Component/Position progressive : 1
		/// 
		/// </summary>
		public const int RES_LY_COMP_POS_PROG = 1;
		/// <summary>The codestream is Resolution/Position/Component/Layer progressive : 2
		/// 
		/// </summary>
		public const int RES_POS_COMP_LY_PROG = 2;
		/// <summary>The codestream is Position/Component/Resolution/Layer progressive : 3
		/// 
		/// </summary>
		public const int POS_COMP_RES_LY_PROG = 3;
		/// <summary>The codestream is Component/Position/Resolution/Layer progressive : 4
		/// 
		/// </summary>
		public const int COMP_POS_RES_LY_PROG = 4;
	}
}