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
using CSJ2K.j2k.entropy;
namespace CSJ2K.j2k.entropy.decoder
{
	
	/// <summary> This class stores coded (compressed) code-blocks that are organized in
	/// layers. This object can contain either all code-block data (i.e. all
	/// layers), or a subset of all the layers that make up the whole compressed
	/// code-block. It is applicable to the decoder engine only. Some data of the
	/// coded-block is stored in the super class, see CodedCBlk.
	/// 
	/// <p>A code-block may have its progressive attribute set (i.e. the 'prog'
	/// flag is true). If a code-block is progressive then it means that more data
	/// for it may be obtained for an improved quality. If the progressive flag is
	/// false then no more data is available from the source for this
	/// code-block.</p>
	/// 
	/// </summary>
	/// <seealso cref="CodedCBlk">
	/// 
	/// </seealso>
	public class DecLyrdCBlk:CodedCBlk
	{
		
		/// <summary>The horizontal coordinate of the upper-left corner of the code-block </summary>
		public int ulx;
		
		/// <summary>The vertical coordinate of the upper left corner of the code-block </summary>
		public int uly;
		
		/// <summary>The width of the code-block </summary>
		public int w;
		
		/// <summary>The height of the code-block </summary>
		public int h;
		
		/// <summary>The coded (compressed) data length. The data is stored in the 'data'
		/// array (see super class).  
		/// </summary>
		public int dl;
		
		/// <summary>The progressive flag, false by default (see above). </summary>
		public bool prog;
		
		/// <summary>The number of layers in the coded data. </summary>
		public int nl;
		
		/// <summary>The index of the first truncation point returned </summary>
		public int ftpIdx;
		
		/// <summary>The total number of truncation points from layer 1 to the last one in
		/// this object. The number of truncation points in 'data' is
		/// 'nTrunc-ftpIdx'. 
		/// </summary>
		public int nTrunc;
		
		/// <summary>The length of each terminated segment. If null then there is only one
		/// terminated segment, and its length is 'dl'. The number of terminated
		/// segments is to be deduced from 'ftpIdx', 'nTrunc' and the coding
		/// options. This array contains all terminated segments from the 'ftpIdx'
		/// truncation point, upto, and including, the 'nTrunc-1' truncation
		/// point. Any data after 'nTrunc-1' is not included in any length. 
		/// </summary>
		public int[] tsLengths;
		
		/// <summary> Object information in a string
		/// 
		/// </summary>
		/// <returns> Information in a string
		/// 
		/// </returns>
		public override System.String ToString()
		{
			System.String str = "Coded code-block (" + m + "," + n + "): " + skipMSBP + " MSB skipped, " + dl + " bytes, " + nTrunc + " truncation points, " + nl + " layers, " + "progressive=" + prog + ", ulx=" + ulx + ", uly=" + uly + ", w=" + w + ", h=" + h + ", ftpIdx=" + ftpIdx;
			if (tsLengths != null)
			{
				str += " {";
				for (int i = 0; i < tsLengths.Length; i++)
					str += (" " + tsLengths[i]);
				str += " }";
			}
			return str;
		}
	}
}