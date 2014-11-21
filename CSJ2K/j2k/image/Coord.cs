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
namespace CSJ2K.j2k.image
{
	
	/// <summary> This class represents 2-D coordinates.
	/// 
	/// </summary>
	public class Coord
	{
		/// <summary>The horizontal coordinate </summary>
		public int x;
		
		/// <summary>The vertical coordinate </summary>
		public int y;
		
		/// <summary> Creates a new coordinate object given with the (0,0) coordinates
		/// 
		/// </summary>
		public Coord()
		{
		}
		
		/// <summary> Creates a new coordinate object given the two coordinates.
		/// 
		/// </summary>
		/// <param name="x">The horizontal coordinate.
		/// 
		/// </param>
		/// <param name="y">The vertical coordinate.
		/// 
		/// </param>
		public Coord(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		
		/// <summary> Creates a new coordinate object given another Coord object i.e. copy 
		/// constructor
		/// 
		/// </summary>
		/// <param name="c">The Coord object to be copied.
		/// 
		/// </param>
		public Coord(Coord c)
		{
			this.x = c.x;
			this.y = c.y;
		}
		
		/// <summary> Returns a string representation of the object coordinates
		/// 
		/// </summary>
		/// <returns> The vertical and the horizontal coordinates
		/// 
		/// </returns>
		public override System.String ToString()
		{
			return "(" + x + "," + y + ")";
		}
	}
}