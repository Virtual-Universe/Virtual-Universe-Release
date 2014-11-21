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
namespace CSJ2K.j2k.codestream.reader
{
	
	/// <summary> This class contains location of code-blocks' piece of codewords (there is
	/// one piece per layer) and some other information.
	/// 
	/// </summary>
	public class CBlkInfo
	{
		
		/// <summary>Upper-left x-coordinate of the code-block (relative to the tile) </summary>
		public int ulx;
		
		/// <summary>Upper-left y-coordinate of the code-block (relative to the tile) </summary>
		public int uly;
		
		/// <summary>Width of the code-block </summary>
		public int w;
		
		/// <summary>Height of the code-block </summary>
		public int h;
		
		/// <summary>The number of most significant bits which are skipped for this
		/// code-block (= Mb-1-bitDepth). 
		/// </summary>
		public int msbSkipped;
		
		/// <summary>Length of each piece of code-block's codewords </summary>
		public int[] len;
		
		/// <summary>Offset of each piece of code-block's codewords in the file </summary>
		public int[] off;
		
		/// <summary>The number of truncation point for each layer </summary>
		public int[] ntp;
		
		/// <summary>The cumulative number of truncation points </summary>
		public int ctp;
		
		/// <summary>The length of each segment (used with regular termination or in
		/// selective arithmetic bypass coding mode) 
		/// </summary>
		public int[][] segLen;
		
		/// <summary>Index of the packet where each layer has been found </summary>
		public int[] pktIdx;
		
		/// <summary> Constructs a new instance with specified number of layers and
		/// code-block coordinates. The number corresponds to the maximum piece of
		/// codeword for one code-block.
		/// 
		/// </summary>
		/// <param name="ulx">The uper-left x-coordinate
		/// 
		/// </param>
		/// <param name="uly">The uper-left y-coordinate
		/// 
		/// </param>
		/// <param name="w">Width of the code-block
		/// 
		/// </param>
		/// <param name="h">Height of the code-block
		/// 
		/// </param>
		/// <param name="nl">The number of layers
		/// 
		/// </param>
		public CBlkInfo(int ulx, int uly, int w, int h, int nl)
		{
			this.ulx = ulx;
			this.uly = uly;
			this.w = w;
			this.h = h;
			off = new int[nl];
			len = new int[nl];
			ntp = new int[nl];
			segLen = new int[nl][];
			pktIdx = new int[nl];
			for (int i = nl - 1; i >= 0; i--)
			{
				pktIdx[i] = - 1;
			}
		}
		
		/// <summary> Adds the number of new truncation for specified layer.
		/// 
		/// </summary>
		/// <param name="l">layer index
		/// 
		/// </param>
		/// <param name="newtp">Number of new truncation points 
		/// 
		/// </param>
		public virtual void  addNTP(int l, int newtp)
		{
			ntp[l] = newtp;
			ctp = 0;
			for (int lIdx = 0; lIdx <= l; lIdx++)
			{
				ctp += ntp[lIdx];
			}
		}
		
		/// <summary> Object information in a string.
		/// 
		/// </summary>
		/// <returns> Object information
		/// 
		/// </returns>
		public override System.String ToString()
		{
			System.String string_Renamed = "(ulx,uly,w,h)= (" + ulx + "," + uly + "," + w + "," + h;
			string_Renamed += (") " + msbSkipped + " MSB bit(s) skipped\n");
			if (len != null)
				for (int i = 0; i < len.Length; i++)
				{
					string_Renamed += ("\tl:" + i + ", start:" + off[i] + ", len:" + len[i] + ", ntp:" + ntp[i] + ", pktIdx=" + pktIdx[i]);
					if (segLen != null && segLen[i] != null)
					{
						string_Renamed += " { ";
						for (int j = 0; j < segLen[i].Length; j++)
							string_Renamed += (segLen[i][j] + " ");
						string_Renamed += "}";
					}
					string_Renamed += "\n";
				}
			string_Renamed += ("\tctp=" + ctp);
			return string_Renamed;
		}
	}
}