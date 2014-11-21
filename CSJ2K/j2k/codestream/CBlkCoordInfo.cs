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
using CSJ2K.j2k.image;
namespace CSJ2K.j2k.codestream
{
	
	/// <summary> This class is used to store the coordinates of code-blocks.
	/// 
	/// </summary>
	public class CBlkCoordInfo:CoordInfo
	{
		
		/// <summary>The code-block horizontal and vertical indexes </summary>
		public Coord idx;
		
		/// <summary> Constructor. Creates a CBlkCoordInfo object.
		/// 
		/// </summary>
		public CBlkCoordInfo()
		{
			this.idx = new Coord();
		}
		
		/// <summary> Constructor. Creates a CBlkCoordInfo object width specified code-block
		/// vertical and horizontal indexes.
		/// 
		/// </summary>
		/// <param name="m">Code-block vertical index.
		/// 
		/// </param>
		/// <param name="n">Code-block horizontal index.
		/// 
		/// </param>
		public CBlkCoordInfo(int m, int n)
		{
			this.idx = new Coord(n, m);
		}
		
		/// <summary> Returns code-block's information in a String 
		/// 
		/// </summary>
		/// <returns> String with code-block's information
		/// 
		/// </returns>
		public override System.String ToString()
		{
			return base.ToString() + ",idx=" + idx;
		}
	}
}