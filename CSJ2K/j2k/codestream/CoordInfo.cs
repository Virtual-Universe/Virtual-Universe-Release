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
using Coord = CSJ2K.j2k.image.Coord;
namespace CSJ2K.j2k.codestream
{
	
	/// <summary> This class is used to store the coordinates of objects such as code-blocks
	/// or precincts. As this is an abstract class, it cannot be used directly but
	/// derived classes have been created for code-blocks and packets
	/// (CBlkCoordInfo and PrecCoordInfo).
	/// 
	/// </summary>
	/// <seealso cref="PrecCoordInfo">
	/// </seealso>
	/// <seealso cref="CBlkCoordInfo">
	/// 
	/// </seealso>
	public abstract class CoordInfo
	{
		
		/// <summary>Horizontal upper left coordinate in the subband </summary>
		public int ulx;
		
		/// <summary>Vertical upper left coordinate in the subband </summary>
		public int uly;
		
		/// <summary>Object's width </summary>
		public int w;
		
		/// <summary>Object's height </summary>
		public int h;
		
		/// <summary> Constructor. Creates a CoordInfo object.
		/// 
		/// </summary>
		/// <param name="ulx">The horizontal upper left coordinate in the subband
		/// 
		/// </param>
		/// <param name="uly">The vertical upper left coordinate in the subband
		/// 
		/// </param>
		/// <param name="w">The width
		/// 
		/// </param>
		/// <param name="h">The height
		/// 
		/// </param>
		/// <param name="idx">The object's index
		/// 
		/// </param>
		public CoordInfo(int ulx, int uly, int w, int h)
		{
			this.ulx = ulx;
			this.uly = uly;
			this.w = w;
			this.h = h;
		}
		
		/// <summary>Empty contructor </summary>
		public CoordInfo()
		{
		}
		
		/// <summary> Returns object's information in a String 
		/// 
		/// </summary>
		/// <returns> String with object's information
		/// 
		/// </returns>
		public override System.String ToString()
		{
			return "ulx=" + ulx + ",uly=" + uly + ",w=" + w + ",h=" + h;
		}
	}
}