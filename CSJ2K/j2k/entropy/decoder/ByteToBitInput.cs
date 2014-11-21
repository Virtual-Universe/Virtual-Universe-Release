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
using CSJ2K.j2k.io;
namespace CSJ2K.j2k.entropy.decoder
{
	
	/// <summary> This class provides an adapter to perform bit based input on byte based
	/// output obejcts that inherit from a 'ByteInputBuffer' class. This class also
	/// performs the bit unstuffing procedure specified for the 'selective
	/// arithmetic coding bypass' mode of the JPEG 2000 entropy coder.
	/// 
	/// </summary>
	public class ByteToBitInput
	{
		
		/// <summary>The byte based input </summary>
		internal ByteInputBuffer in_Renamed;
		
		/// <summary>The bit buffer </summary>
		internal int bbuf;
		
		/// <summary>The position of the next bit to get from the byte buffer. When it is
		/// -1 the bit buffer is empty. 
		/// </summary>
		internal int bpos = - 1;
		
		/// <summary> Instantiates a new 'ByteToBitInput' object that uses 'in' as the
		/// underlying byte based input.
		/// 
		/// </summary>
		/// <param name="in">The underlying byte based input.
		/// 
		/// </param>
		public ByteToBitInput(ByteInputBuffer in_Renamed)
		{
			this.in_Renamed = in_Renamed;
		}
		
		/// <summary> Reads from the bit stream one bit. If 'bpos' is -1 then a byte is read
		/// and loaded into the bit buffer, from where the bit is read. If
		/// necessary the bit unstuffing will be applied.
		/// 
		/// </summary>
		/// <returns> The read bit (0 or 1).
		/// 
		/// </returns>
		public int readBit()
		{
			if (bpos < 0)
			{
				if ((bbuf & 0xFF) != 0xFF)
				{
					// Normal byte to read
					bbuf = in_Renamed.read();
					bpos = 7;
				}
				else
				{
					// Previous byte is 0xFF => there was bit stuffing
					bbuf = in_Renamed.read();
					bpos = 6;
				}
			}
			return (bbuf >> bpos--) & 0x01;
		}
		
		/// <summary> Checks for past errors in the decoding process by verifying the byte
		/// padding with an alternating sequence of 0's and 1's. If an error is
		/// detected it means that the raw bit stream has been wrongly decoded or
		/// that the raw terminated segment length is too long. If no errors are
		/// detected it does not necessarily mean that the raw bit stream has been
		/// correctly decoded.
		/// 
		/// </summary>
		/// <returns> True if errors are found, false otherwise.
		/// 
		/// </returns>
		public virtual bool checkBytePadding()
		{
			int seq; // Byte padding sequence in last byte
			
			// If there are no spare bits and bbuf is 0xFF (not EOF), then there
			// is a next byte with bit stuffing that we must load.
			if (bpos < 0 && (bbuf & 0xFF) == 0xFF)
			{
				bbuf = in_Renamed.read();
				bpos = 6;
			}
			
			// 1) Not yet read bits in the last byte must be an alternating
			// sequence of 0s and 1s, starting with 0.
			if (bpos >= 0)
			{
				seq = bbuf & ((1 << (bpos + 1)) - 1);
				if (seq != (0x55 >> (7 - bpos)))
					return true;
			}
			
			// 2) We must have already reached the last byte in the terminated
			// segment, unless last bit read is LSB of FF in which case an encoder
			// can output an extra byte which is smaller than 0x80.
			if (bbuf != - 1)
			{
				if (bbuf == 0xFF && bpos == 0)
				{
					if ((in_Renamed.read() & 0xFF) >= 0x80)
						return true;
				}
				else
				{
					if (in_Renamed.read() != - 1)
						return true;
				}
			}
			
			// Nothing detected
			return false;
		}
		
		/// <summary> Flushes (i.e. empties) the bit buffer, without loading any new
		/// bytes. This realigns the input at the next byte boundary, if not
		/// already at one.
		/// 
		/// </summary>
		internal void  flush()
		{
			bbuf = 0; // reset any bit stuffing state
			bpos = - 1;
		}
		
		/// <summary> Resets the underlying byte input to start a new segment. The bit buffer
		/// is flushed.
		/// 
		/// </summary>
		/// <param name="buf">The byte array containing the byte data. If null the
		/// current byte array is assumed.
		/// 
		/// </param>
		/// <param name="off">The index of the first element in 'buf' to be decoded. If
		/// negative the byte just after the previous segment is assumed, only
		/// valid if 'buf' is null.
		/// 
		/// </param>
		/// <param name="len">The number of bytes in 'buf' to be decoded. Any subsequent
		/// bytes are taken to be 0xFF.
		/// 
		/// </param>
		internal void  setByteArray(byte[] buf, int off, int len)
		{
			in_Renamed.setByteArray(buf, off, len);
			bbuf = 0; // reset any bit stuffing state
			bpos = - 1;
		}
	}
}