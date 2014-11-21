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
	
	/// <summary> This is the generic class to store coded (compressed) code-block. It stores
	/// the compressed data as well as the necessary side-information.
	/// 
	/// <p>This class is normally not used. Instead the EncRDCBlk, EncLyrdCBlk and
	/// the DecLyrdCBlk subclasses are used.</p>
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.entropy.encoder.CBlkRateDistStats">
	/// </seealso>
	/// <seealso cref="jj2000.j2k.entropy.decoder.DecLyrdCBlk">
	/// 
	/// </seealso>
	public class CodedCBlk
	{
		
		/// <summary>The horizontal index of the code-block, within the subband. </summary>
		public int n;
		
		/// <summary>The vertical index of the code-block, within the subband. </summary>
		public int m;
		
		/// <summary>The number of skipped most significant bit-planes. </summary>
		public int skipMSBP;
		
		/// <summary>The compressed data </summary>
		public byte[] data;
		
		/// <summary> Creates a new CodedCBlk object wit the default values and without
		/// allocating any space for its members.
		/// 
		/// </summary>
		public CodedCBlk()
		{
		}
		
		/// <summary> Creates a new CodedCBlk object with the specified values.
		/// 
		/// </summary>
		/// <param name="m">The horizontal index of the code-block, within the subband.
		/// 
		/// </param>
		/// <param name="n">The vertical index of the code-block, within the subband.
		/// 
		/// </param>
		/// <param name="skipMSBP">The number of skipped most significant bit-planes for
		/// this code-block.
		/// 
		/// </param>
		/// <param name="data">The compressed data. This array is referenced by this
		/// object so it should not be modified after.
		/// 
		/// </param>
		public CodedCBlk(int m, int n, int skipMSBP, byte[] data)
		{
			this.m = m;
			this.n = n;
			this.skipMSBP = skipMSBP;
			this.data = data;
		}
		
		/// <summary> Returns the contents of the object in a string. The string contains the
		/// following data: 'm', 'n', 'skipMSBP' and 'data.length. This is used for
		/// debugging.
		/// 
		/// </summary>
		/// <returns> A string with the contents of the object
		/// 
		/// </returns>
		public override System.String ToString()
		{
			return "m=" + m + ", n=" + n + ", skipMSBP=" + skipMSBP + ", data.length=" + ((data != null)?"" + data.Length:"(null)");
		}
	}
}