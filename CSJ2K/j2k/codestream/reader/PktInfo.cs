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
	
	/// <summary> This class defines an object used to countain informations about a packet
	/// to which the current code-block belongs.
	/// 
	/// </summary>
	/// <seealso cref="CBlkInfo">
	/// 
	/// </seealso>
	public class PktInfo
	{
		
		/// <summary>Index of the packet </summary>
		public int packetIdx;
		
		/// <summary>The layer associated with the current code-block in this packet. </summary>
		public int layerIdx;
		
		/// <summary>The code-block offset in the codestream (for this packet) </summary>
		public int cbOff = 0;
		
		/// <summary>The length of the code-block in this packet (in bytes) </summary>
		public int cbLength;
		
		/// <summary> The length of each terminated segment in the packet. The total is the
		/// same as 'cbLength'. It can be null if there is only one terminated
		/// segment, in which case 'cbLength' holds the legth of that segment 
		/// 
		/// </summary>
		public int[] segLengths;
		
		/// <summary> The number of truncation points that appear in this packet, and all
		/// previous packets, for this code-block. This is the number of passes
		/// that can be decoded with the information in this packet and all
		/// previous ones. 
		/// 
		/// </summary>
		public int numTruncPnts;
		
		/// <summary> Classe's constructor.
		/// 
		/// </summary>
		/// <param name="lyIdx">The layer index for the code-block in this packet
		/// 
		/// </param>
		/// <param name="pckIdx">The packet index
		/// 
		/// </param>
		public PktInfo(int lyIdx, int pckIdx)
		{
			layerIdx = lyIdx;
			packetIdx = pckIdx;
		}
		
		/// <summary> Object information in a string.
		/// 
		/// </summary>
		/// <returns> Object information
		/// 
		/// </returns>
		public override System.String ToString()
		{
			return "packet " + packetIdx + " (lay:" + layerIdx + ", off:" + cbOff + ", len:" + cbLength + ", numTruncPnts:" + numTruncPnts + ")\n";
		}
	}
}