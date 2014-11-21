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
namespace CSJ2K.j2k.codestream.writer
{
	
	/// <summary> This is the abstract class for writing to a codestream. A codestream
	/// corresponds to headers (main and tile-parts) and packets. Each packet has a
	/// head and a body. The codestream always has a maximum number of bytes that
	/// can be written to it. After that many number of bytes no more data is
	/// written to the codestream but the number of bytes is counted so that the
	/// value returned by getMaxAvailableBytes() is negative. If the number of
	/// bytes is unlimited a ridicoulosly large value, such as Integer.MAX_VALUE,
	/// is equivalent.
	/// 
	/// <p>Data writting to the codestream can be simulated. In this case, no byto
	/// is effectively written to the codestream but the resulting number of bytes
	/// is calculated and returned (although it is not accounted in the bit
	/// stream). This can be used in rate control loops.</p>
	/// 
	/// <p>Implementing classes should write the header of the bit stream before
	/// writing any packets. The bit stream header can be written with the help of
	/// the HeaderEncoder class.</p>
	/// 
	/// </summary>
	/// <seealso cref="HeaderEncoder">
	/// 
	/// </seealso>
	public abstract class CodestreamWriter
	{
		/// <summary> Returns the number of bytes remaining available in the codestream. This
		/// is the maximum allowed number of bytes minus the number of bytes that
		/// have already been written to the bit stream. If more bytes have been
		/// written to the bit stream than the maximum number of allowed bytes,
		/// then a negative value is returned.
		/// 
		/// </summary>
		/// <returns> The number of bytes remaining available in the bit stream.
		/// 
		/// </returns>
		public abstract int MaxAvailableBytes{get;}
		/// <summary> Returns the current length of the entire codestream.
		/// 
		/// </summary>
		/// <returns> the current length of the codestream
		/// 
		/// </returns>
		public abstract int Length{get;}
		/// <summary> Gives the offset of the end of last packet containing ROI information 
		/// 
		/// </summary>
		/// <returns> End of last ROI packet 
		/// 
		/// </returns>
		public abstract int OffLastROIPkt{get;}
		
		/// <summary>The number of bytes already written to the bit stream </summary>
		protected internal int ndata = 0;
		
		/// <summary>The maximum number of bytes that can be written to the bit stream </summary>
		protected internal int maxBytes;
		
		/// <summary> Allocates this object and initializes the maximum number of bytes.
		/// 
		/// </summary>
		/// <param name="mb">The maximum number of bytes that can be written to the
		/// codestream.
		/// 
		/// </param>
		protected internal CodestreamWriter(int mb)
		{
			maxBytes = mb;
		}
		
		/// <summary> Writes a packet head into the codestream and returns the number of
		/// bytes used by this header. If in simulation mode then no data is
		/// effectively written to the codestream but the number of bytes is
		/// calculated. This can be used for iterative rate allocation.
		/// 
		/// <p>If the number of bytes that has to be written to the codestream is
		/// more than the space left (as returned by getMaxAvailableBytes()), only
		/// the data that does not exceed the allowed length is effectively written
		/// and the rest is discarded. However the value returned by the method is
		/// the total length of the packet, as if all of it was written to the bit
		/// stream.</p>
		/// 
		/// <p>If the codestream header has not been commited yet and if 'sim' is
		/// false, then the bit stream header is automatically commited (see
		/// commitBitstreamHeader() method) before writting the packet.
		/// 
		/// </summary>
		/// <param name="head">The packet head data.
		/// 
		/// </param>
		/// <param name="hlen">The number of bytes in the packet head.
		/// 
		/// </param>
		/// <param name="sim">Simulation mode flag. If true nothing is written to the bit
		/// stream, but the number of bytes that would be written is returned.
		/// 
		/// </param>
		/// <param name="sop">Start of packet header marker flag. This flag indicates
		/// whether or not SOP markers should be written. If true, SOP markers
		/// should be written, if false, they should not.
		/// 
		/// </param>
		/// <param name="eph">End of Packet Header marker flag. This flag indicates
		/// whether or not EPH markers should be written. If true, EPH markers
		/// should be written, if false, they should not.
		/// 
		/// </param>
		/// <returns> The number of bytes spent by the packet head.
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error occurs while writing to the
		/// output stream.
		/// 
		/// </exception>
		/// <seealso cref="commitBitstreamHeader">
		/// 
		/// </seealso>
		public abstract int writePacketHead(byte[] head, int hlen, bool sim, bool sop, bool eph);
		
		/// <summary> Writes a packet body to the codestream and returns the number of bytes
		/// used by this body. If in simulation mode then no data is written to the
		/// bit stream but the number of bytes is calculated. This can be used for
		/// iterative rate allocation.
		/// 
		/// <p>If the number of bytes that has to be written to the codestream is
		/// more than the space left (as returned by getMaxAvailableBytes()), only
		/// the data that does not exceed the allowed length is effectively written
		/// and the rest is discarded. However the value returned by the method is
		/// the total length of the packet, as if all of it was written to the bit
		/// stream.</p>
		/// 
		/// </summary>
		/// <param name="body">The packet body data.
		/// 
		/// </param>
		/// <param name="blen">The number of bytes in the packet body.
		/// 
		/// </param>
		/// <param name="sim">Simulation mode flag. If true nothing is written to the bit
		/// stream, but the number of bytes that would be written is returned.
		/// 
		/// </param>
		/// <param name="roiInPkt">Whether or not there is ROI information in this packet
		/// 
		/// </param>
		/// <param name="roiLen">Number of byte to read in packet body to get all the ROI
		/// information 
		/// 
		/// </param>
		/// <returns> The number of bytes spent by the packet body.
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error occurs while writing to the
		/// output stream.
		/// 
		/// </exception>
		/// <seealso cref="commitBitstreamHeader">
		/// 
		/// </seealso>
		public abstract int writePacketBody(byte[] body, int blen, bool sim, bool roiInPkt, int roiLen);
		
		
		/// <summary> Closes the underlying resource (file, stream, network connection,
		/// etc.). After a CodestreamWriter is closed no more data can be written
		/// to it.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs while closing the
		/// resource.
		/// 
		/// </exception>
		public abstract void  close();
		
		/// <summary> Writes the header data to the bit stream, if it has not been already
		/// done. In some implementations this method can be called only once, and
		/// an IllegalArgumentException is thrown if called more than once.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs while writing the data.
		/// 
		/// </exception>
		/// <exception cref="IllegalArgumentException">If this method has already been
		/// called.
		/// 
		/// </exception>
		public abstract void  commitBitstreamHeader(HeaderEncoder he);
	}
}