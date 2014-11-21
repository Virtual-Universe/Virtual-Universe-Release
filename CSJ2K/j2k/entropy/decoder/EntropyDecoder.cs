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
using CSJ2K.j2k.quantization.dequantizer;
using CSJ2K.j2k.codestream.reader;
using CSJ2K.j2k.wavelet.synthesis;
using CSJ2K.j2k.codestream;
using CSJ2K.j2k.entropy;
using CSJ2K.j2k.image;
using CSJ2K.j2k.io;
using CSJ2K.j2k;
namespace CSJ2K.j2k.entropy.decoder
{
	
	/// <summary> This is the abstract class from which all entropy decoders must
	/// inherit. This class implements the 'MultiResImgData', therefore it has the
	/// concept of a current tile and all operations are performed on the current
	/// tile.
	/// 
	/// <p>Default implementations of the methods in 'MultiResImgData' are provided
	/// through the 'MultiResImgDataAdapter' abstract class.</p>
	/// 
	/// <p>Sign magnitude representation is used (instead of two's complement) for
	/// the output data. The most significant bit is used for the sign (0 if
	/// positive, 1 if negative). Then the magnitude of the quantized coefficient
	/// is stored in the next most significat bits. The most significant magnitude
	/// bit corresponds to the most significant bit-plane and so on.</p
	/// 
	/// </summary>
	/// <seealso cref="MultiResImgData">
	/// </seealso>
	/// <seealso cref="MultiResImgDataAdapter">
	/// 
	/// </seealso>
	public abstract class EntropyDecoder:MultiResImgDataAdapter, CBlkQuantDataSrcDec
	{
		/// <summary> Returns the horizontal code-block partition origin. Allowable values
		/// are 0 and 1, nothing else.
		/// 
		/// </summary>
		virtual public int CbULX
		{
			get
			{
				return src.CbULX;
			}
			
		}
		/// <summary> Returns the vertical code-block partition origin. Allowable values are
		/// 0 and 1, nothing else.
		/// 
		/// </summary>
		virtual public int CbULY
		{
			get
			{
				return src.CbULY;
			}
			
		}
		/// <summary> Returns the parameters that are used in this class and
		/// implementing classes. It returns a 2D String array. Each of the
		/// 1D arrays is for a different option, and they have 3
		/// elements. The first element is the option name, the second one
		/// is the synopsis and the third one is a long description of what
		/// the parameter is. The synopsis or description may be 'null', in
		/// which case it is assumed that there is no synopsis or
		/// description of the option, respectively. Null may be returned
		/// if no options are supported.
		/// 
		/// </summary>
		/// <returns> the options name, their synopsis and their explanation, 
		/// or null if no options are supported.
		/// 
		/// </returns>
		public static System.String[][] ParameterInfo
		{
			get
			{
				return pinfo;
			}
			
		}
		
		/// <summary>The prefix for entropy decoder optiojns: 'C' </summary>
		public const char OPT_PREFIX = 'C';
		
		/// <summary>The list of parameters that is accepted by the entropy
		/// decoders. They start with 'C'. 
		/// </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'pinfo'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.String[][] pinfo = new System.String[][]{new System.String[]{"Cverber", "[on|off]", "Specifies if the entropy decoder should be verbose about detected " + "errors. If 'on' a message is printed whenever an error is detected.", "on"}, new System.String[]{"Cer", "[on|off]", "Specifies if error detection should be performed by the entropy " + "decoder engine. If errors are detected they will be concealed and " + "the resulting distortion will be less important. Note that errors " + "can only be detected if the encoder that generated the data " + "included error resilience information.", "on"}};
		
		/// <summary>The bit stream transport from where to get the compressed data
		/// (the source) 
		/// </summary>
		protected internal CodedCBlkDataSrcDec src;
		
		/// <summary> Initializes the source of compressed data.
		/// 
		/// </summary>
		/// <param name="src">From where to obtain the compressed data.
		/// 
		/// </param>
		public EntropyDecoder(CodedCBlkDataSrcDec src):base(src)
		{
			this.src = src;
		}
		
		/// <summary> Returns the subband tree, for the specified tile-component. This method
		/// returns the root element of the subband tree structure, see Subband and
		/// SubbandSyn. The tree comprises all the available resolution levels.
		/// 
		/// <P>The number of magnitude bits ('magBits' member variable) for
		/// each subband is not initialized.
		/// 
		/// </summary>
		/// <param name="t">The index of the tile, from 0 to T-1.
		/// 
		/// </param>
		/// <param name="c">The index of the component, from 0 to C-1.
		/// 
		/// </param>
		/// <returns> The root of the tree structure.
		/// 
		/// </returns>
		public override SubbandSyn getSynSubbandTree(int t, int c)
		{
			return src.getSynSubbandTree(t, c);
		}
		public abstract CSJ2K.j2k.image.DataBlk getCodeBlock(int param1, int param2, int param3, CSJ2K.j2k.wavelet.synthesis.SubbandSyn param4, CSJ2K.j2k.image.DataBlk param5);
		public abstract CSJ2K.j2k.image.DataBlk getInternCodeBlock(int param1, int param2, int param3, CSJ2K.j2k.wavelet.synthesis.SubbandSyn param4, CSJ2K.j2k.image.DataBlk param5);
	}
}