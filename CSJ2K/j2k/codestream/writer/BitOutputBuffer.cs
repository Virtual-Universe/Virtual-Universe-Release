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
using CSJ2K.j2k.util;
namespace CSJ2K.j2k.codestream.writer
{
	
	/// <summary> This class implements a buffer for writing bits, with the required bit
	/// stuffing policy for the packet headers. The bits are stored in a byte array
	/// in the order in which they are written. The byte array is automatically
	/// reallocated and enlarged whenever necessary. A BitOutputBuffer object may
	/// be reused by calling its 'reset()' method.
	/// 
	/// <P>NOTE: The methods implemented in this class are intended to be used only
	/// in writing packet heads, since a special bit stuffing procedure is used, as
	/// required for the packet heads.
	/// 
	/// </summary>
	public class BitOutputBuffer
	{
		/// <summary> Returns the current length of the buffer, in bytes.
		/// 
		/// <P>This method is declared final to increase performance.
		/// 
		/// </summary>
		/// <returns> The currebt length of the buffer in bytes.
		/// 
		/// </returns>
		virtual public int Length
		{
			get
			{
				if (avbits == 8)
				{
					// A integral number of bytes
					return curbyte;
				}
				else
				{
					// Some bits in last byte
					return curbyte + 1;
				}
			}
			
		}
		/// <summary> Returns the byte buffer. This is the internal byte buffer so it should
		/// not be modified. Only the first N elements have valid data, where N is
		/// the value returned by 'getLength()'
		/// 
		/// <P>This method is declared final to increase performance.
		/// 
		/// </summary>
		/// <returns> The internal byte buffer.
		/// 
		/// </returns>
		virtual public byte[] Buffer
		{
			get
			{
				return buf;
			}
			
		}
		
		/// <summary>The buffer where we store the data </summary>
		internal byte[] buf;
		
		/// <summary>The position of the current byte to write </summary>
		internal int curbyte;
		
		/// <summary>The number of available bits in the current byte </summary>
		internal int avbits = 8;
		
		/// <summary>The increment size for the buffer, 16 bytes. This is the
		/// number of bytes that are added to the buffer each time it is
		/// needed to enlarge it.
		/// </summary>
		// This must be always 6 or larger.
		public const int SZ_INCR = 16;
		
		/// <summary>The initial size for the buffer, 32 bytes. </summary>
		public const int SZ_INIT = 32;
		
		/// <summary> Creates a new BitOutputBuffer width a buffer of length
		/// 'SZ_INIT'.
		/// 
		/// </summary>
		public BitOutputBuffer()
		{
			buf = new byte[SZ_INIT];
		}
		
		/// <summary> Resets the buffer. This rewinds the current position to the start of
		/// the buffer and sets all tha data to 0. Note that no new buffer is
		/// allocated, so this will affect any data that was returned by the
		/// 'getBuffer()' method.
		/// 
		/// </summary>
		public virtual void  reset()
		{
			//int i;
			// Reinit pointers
			curbyte = 0;
			avbits = 8;
			ArrayUtil.byteArraySet(buf, (byte) 0);
		}
		
		/// <summary> Writes a bit to the buffer at the current position. The value 'bit'
		/// must be either 0 or 1, otherwise it corrupts the bits that have been
		/// already written. The buffer is enlarged, by 'SZ_INCR' bytes, if
		/// necessary.
		/// 
		/// <P>This method is declared final to increase performance.
		/// 
		/// </summary>
		/// <param name="bit">The bit to write, 0 or 1.
		/// 
		/// </param>
		public void  writeBit(int bit)
		{
			buf[curbyte] |= (byte) (bit << --avbits);
			if (avbits > 0)
			{
				// There is still place in current byte for next bit
				return ;
			}
			else
			{
				// End of current byte => goto next
				if (buf[curbyte] != (byte) SupportClass.Identity(0xFF))
				{
					// We don't need bit stuffing
					avbits = 8;
				}
				else
				{
					// We need to stuff a bit (next MSBit is 0)
					avbits = 7;
				}
				curbyte++;
				if (curbyte == buf.Length)
				{
					// We are at end of 'buf' => extend it
					byte[] oldbuf = buf;
					buf = new byte[oldbuf.Length + SZ_INCR];
					Array.Copy(oldbuf, 0, buf, 0, oldbuf.Length);
				}
			}
		}
		
		/// <summary> Writes the n least significant bits of 'bits' to the buffer at the
		/// current position. The least significant bit is written last. The 32-n
		/// most significant bits of 'bits' must be 0, otherwise corruption of the
		/// buffer will result. The buffer is enlarged, by 'SZ_INCR' bytes, if
		/// necessary.
		/// 
		/// <P>This method is declared final to increase performance.
		/// 
		/// </summary>
		/// <param name="bits">The bits to write.
		/// 
		/// </param>
		/// <param name="n">The number of LSBs in 'bits' to write.
		/// 
		/// </param>
		public void  writeBits(int bits, int n)
		{
			// Check that we have enough place in 'buf' for n bits, and that we do
			// not fill last byte, taking into account possibly stuffed bits (max
			// 2)
			if (((buf.Length - curbyte) << 3) - 8 + avbits <= n + 2)
			{
				// Not enough place, extend it
				byte[] oldbuf = buf;
				buf = new byte[oldbuf.Length + SZ_INCR];
				Array.Copy(oldbuf, 0, buf, 0, oldbuf.Length);
				// SZ_INCR is always 6 or more, so it is enough to hold all the
				// new bits plus the ones to come after
			}
			// Now write the bits
			if (n >= avbits)
			{
				// Complete the current byte
				n -= avbits;
				buf[curbyte] |= (byte) (bits >> n);
				if (buf[curbyte] != (byte) SupportClass.Identity(0xFF))
				{
					// We don't need bit stuffing
					avbits = 8;
				}
				else
				{
					// We need to stuff a bit (next MSBit is 0)
					avbits = 7;
				}
				curbyte++;
				// Write whole bytes
				while (n >= avbits)
				{
					n -= avbits;
                    // CONVERSION PROBLEM?
					buf[curbyte] |= (byte)((bits >> n) & (~ (1 << avbits)));
					if (buf[curbyte] != (byte) SupportClass.Identity(0xFF))
					{
						// We don't need bit
						// stuffing
						avbits = 8;
					}
					else
					{
						// We need to stuff a bit (next MSBit is 0)
						avbits = 7;
					}
					curbyte++;
				}
			}
			// Finish last byte (we know that now n < avbits)
			if (n > 0)
			{
				avbits -= n;
				buf[curbyte] |= (byte) ((bits & ((1 << n) - 1)) << avbits);
			}
			if (avbits == 0)
			{
				// Last byte is full
				if (buf[curbyte] != (byte) SupportClass.Identity(0xFF))
				{
					// We don't need bit stuffing
					avbits = 8;
				}
				else
				{
					// We need to stuff a bit (next MSBit is 0)
					avbits = 7;
				}
				curbyte++; // We already ensured that we have enough place
			}
		}
		
		/// <summary> Returns the byte buffer data in a new array. This is a copy of the
		/// internal byte buffer. If 'data' is non-null it is used to return the
		/// data. This array should be large enough to contain all the data,
		/// otherwise a IndexOutOfBoundsException is thrown by the Java system. The
		/// number of elements returned is what 'getLength()' returns.
		/// 
		/// </summary>
		/// <param name="data">If non-null this array is used to return the data, which
		/// mus be large enough. Otherwise a new one is created and returned.
		/// 
		/// </param>
		/// <returns> The byte buffer data.
		/// 
		/// </returns>
		public virtual byte[] toByteArray(byte[] data)
		{
			if (data == null)
			{
				data = new byte[(avbits == 8)?curbyte:curbyte + 1];
			}
			Array.Copy(buf, 0, data, 0, (avbits == 8)?curbyte:curbyte + 1);
			return data;
		}
		
		/// <summary> Prints information about this object for debugging purposes
		/// 
		/// </summary>
		/// <returns> Information about the object.
		/// 
		/// </returns>
		public override System.String ToString()
		{
			return "bits written = " + (curbyte * 8 + (8 - avbits)) + ", curbyte = " + curbyte + ", avbits = " + avbits;
		}
	}
}