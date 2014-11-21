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
	
	/// <summary> Class that holds precinct coordinates and references to contained
	/// code-blocks in each subband. 
	/// 
	/// </summary>
	public class PrecInfo
	{
		
		/// <summary>Precinct horizontal upper-left coordinate in the reference grid </summary>
		public int rgulx;
		
		/// <summary>Precinct vertical upper-left coordinate in the reference grid </summary>
		public int rguly;
		
		/// <summary>Precinct width reported in the reference grid </summary>
		public int rgw;
		
		/// <summary>Precinct height reported in the reference grid </summary>
		public int rgh;
		
		/// <summary>Precinct horizontal upper-left coordinate in the corresponding
		/// resolution level
		/// </summary>
		public int ulx;
		
		/// <summary>Precinct vertical upper-left coordinate in the corresponding
		/// resolution level
		/// </summary>
		public int uly;
		
		/// <summary>Precinct width in the corresponding resolution level </summary>
		public int w;
		
		/// <summary>Precinct height in the corresponding resolution level </summary>
		public int h;
		
		/// <summary>Resolution level index </summary>
		public int r;
		
		/// <summary>Code-blocks belonging to this precinct in each subbands of the
		/// resolution level 
		/// </summary>
		public CBlkCoordInfo[][][] cblk;
		
		/// <summary>Number of code-blocks in each subband belonging to this precinct </summary>
		public int[] nblk;
		
		/// <summary> Class constructor.
		/// 
		/// </summary>
		/// <param name="r">Resolution level index.
		/// </param>
		/// <param name="ulx">Precinct horizontal offset.
		/// </param>
		/// <param name="uly">Precinct vertical offset.
		/// </param>
		/// <param name="w">Precinct width.
		/// </param>
		/// <param name="h">Precinct height.
		/// </param>
		/// <param name="rgulx">Precinct horizontal offset in the image reference grid.
		/// </param>
		/// <param name="rguly">Precinct horizontal offset in the image reference grid.
		/// </param>
		/// <param name="rgw">Precinct width in the reference grid.
		/// </param>
		/// <param name="rgh">Precinct height in the reference grid.
		/// 
		/// </param>
		public PrecInfo(int r, int ulx, int uly, int w, int h, int rgulx, int rguly, int rgw, int rgh)
		{
			this.r = r;
			this.ulx = ulx;
			this.uly = uly;
			this.w = w;
			this.h = h;
			this.rgulx = rgulx;
			this.rguly = rguly;
			this.rgw = rgw;
			this.rgh = rgh;
			
			if (r == 0)
			{
				cblk = new CBlkCoordInfo[1][][];
				nblk = new int[1];
			}
			else
			{
				cblk = new CBlkCoordInfo[4][][];
				nblk = new int[4];
			}
		}
		
		/// <summary> Returns PrecInfo object information in a String
		/// 
		/// </summary>
		/// <returns> PrecInfo information 
		/// 
		/// </returns>
		public override System.String ToString()
		{
			return "ulx=" + ulx + ",uly=" + uly + ",w=" + w + ",h=" + h + ",rgulx=" + rgulx + ",rguly=" + rguly + ",rgw=" + rgw + ",rgh=" + rgh;
		}
	}
}