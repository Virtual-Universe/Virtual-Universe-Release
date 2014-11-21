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
namespace CSJ2K.j2k.entropy
{
	
	/// <summary> This interface define the constants that identify the possible options for
	/// the entropy coder, as well some fixed parameters of the JPEG 2000 entropy
	/// coder.
	/// 
	/// </summary>
	public struct StdEntropyCoderOptions{
		/// <summary>The flag bit to indicate that selective arithmetic coding bypass
		/// should be used.  In this mode, the significance propagation and
		/// magnitude refinement passes bypass the arithmetic encoder in the fourth
		/// bit-plane and latter ones (but not the cleanup pass). Note that the
		/// transition between raw and AC segments needs terminations (whether or
		/// not OPT_TERM_PASS is used). 
		/// </summary>
		public readonly static int OPT_BYPASS = 1;
		/// <summary>The flag bit to indicate that the MQ states for all contexts should be 
		/// reset at the end of each (non-bypassed) coding pass. 
		/// </summary>
		public readonly static int OPT_RESET_MQ = 1 << 1;
		/// <summary>The flag bit to indicate that a termination should be performed after
		/// each coding pass.  Note that terminations are applied to both * *
		/// arithmetically coded and bypassed (i.e. raw) passes . 
		/// </summary>
		public readonly static int OPT_TERM_PASS = 1 << 2;
		/// <summary>The flag bit to indicate the vertically stripe-causal context
		/// formation should be used. 
		/// </summary>
		public readonly static int OPT_VERT_STR_CAUSAL = 1 << 3;
		/// <summary>The flag bit to indicate that error resilience info is embedded on MQ
		/// termination. This corresponds to the predictable termination described
		/// in Annex D.4.2 of the FDIS 
		/// </summary>
		public readonly static int OPT_PRED_TERM = 1 << 4;
		/// <summary>The flag bit to indicate that an error resilience segmentation symbol
		/// is to be inserted at the end of each cleanup coding pass. The
		/// segmentation symbol is the four symbol sequence 1010 that are sent
		/// through the MQ coder using the UNIFORM context (as explained in annex
		/// D.5 of FDIS). 
		/// </summary>
		public readonly static int OPT_SEG_SYMBOLS = 1 << 5;
		/// <summary>The minimum code-block dimension. The nominal width or height of a
		/// code-block must never be less than this. It is 4. 
		/// </summary>
		public readonly static int MIN_CB_DIM = 4;
		/// <summary>The maximum code-block dimension. No code-block should be larger,
		/// either in width or height, than this value. It is 1024. 
		/// </summary>
		public readonly static int MAX_CB_DIM = 1024;
		/// <summary>The maximum code-block area (width x height). The surface covered by
		/// a nominal size block should never be larger than this. It is 4096 
		/// </summary>
		public readonly static int MAX_CB_AREA = 4096;
		/// <summary>The stripe height. This is the nominal value of the stripe height. It
		/// is 4. 
		/// </summary>
		public readonly static int STRIPE_HEIGHT = 4;
		/// <summary>The number of coding passes per bit-plane. This is the number of
		/// passes per bit-plane. It is 3. 
		/// </summary>
		public readonly static int NUM_PASSES = 3;
		/// <summary>The number of most significant bit-planes where bypass mode is not to
		/// be used, even if bypass mode is on: 4. 
		/// </summary>
		public readonly static int NUM_NON_BYPASS_MS_BP = 4;
		/// <summary>The number of empty passes in the most significant bit-plane. It is
		/// 2. 
		/// </summary>
		public readonly static int NUM_EMPTY_PASSES_IN_MS_BP = 2;
		/// <summary>The index of the first "raw" pass, if bypass mode is on. </summary>
		public readonly static int FIRST_BYPASS_PASS_IDX;
		static StdEntropyCoderOptions()
		{
			FIRST_BYPASS_PASS_IDX = CSJ2K.j2k.entropy.StdEntropyCoderOptions.NUM_PASSES * CSJ2K.j2k.entropy.StdEntropyCoderOptions.NUM_NON_BYPASS_MS_BP - CSJ2K.j2k.entropy.StdEntropyCoderOptions.NUM_EMPTY_PASSES_IN_MS_BP;
		}
	}
}