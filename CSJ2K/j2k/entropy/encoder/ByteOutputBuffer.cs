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
namespace CSJ2K.j2k.entropy.encoder
{
	
	/// <summary> This class provides a buffering output stream similar to
	/// ByteArrayOutputStream, with some additional methods.
	/// 
	/// <p>Once an array has been written to an output stream or to a byte array,
	/// the object can be reused as a new stream if the reset() method is
	/// called.</p>
	/// 
	/// <p>Unlike the ByteArrayOutputStream class, this class is not thread
	/// safe.</p>
	/// 
	/// </summary>
	/// <seealso cref="reset">
	/// 
	/// </seealso>
	public class ByteOutputBuffer
	{
		
		/// <summary>The buffer where the data is stored </summary>
		internal byte[] buf;
		
		/// <summary>The number of valid bytes in the buffer </summary>
		internal int count;
		
		/// <summary>The buffer increase size </summary>
		public const int BUF_INC = 512;
		
		/// <summary>The default initial buffer size </summary>
		public const int BUF_DEF_LEN = 256;
		
		/// <summary> Creates a new byte array output stream. The buffer capacity is
		/// initially BUF_DEF_LEN bytes, though its size increases if necessary.
		/// 
		/// </summary>
		public ByteOutputBuffer()
		{
			buf = new byte[BUF_DEF_LEN];
		}
		
		/// <summary> Creates a new byte array output stream, with a buffer capacity of the
		/// specified size, in bytes.
		/// 
		/// </summary>
		/// <param name="size">the initial size.
		/// 
		/// </param>
		public ByteOutputBuffer(int size)
		{
			buf = new byte[size];
		}
		
		/// <summary> Writes the specified byte to this byte array output stream. The
		/// functionality provided by this implementation is the same as for the
		/// one in the superclass, however this method is not synchronized and
		/// therefore not safe thread, but faster.
		/// 
		/// </summary>
		/// <param name="b">The byte to write
		/// 
		/// </param>
		public void  write(int b)
		{
			if (count == buf.Length)
			{
				// Resize buffer
				byte[] tmpbuf = buf;
				buf = new byte[buf.Length + BUF_INC];
				Array.Copy(tmpbuf, 0, buf, 0, count);
			}
			buf[count++] = (byte) b;
		}
		
		/// <summary> Copies the specified part of the stream to the 'outbuf' byte array.
		/// 
		/// </summary>
		/// <param name="off">The index of the first element in the stream to copy.
		/// 
		/// </param>
		/// <param name="len">The number of elements of the array to copy
		/// 
		/// </param>
		/// <param name="outbuf">The destination array
		/// 
		/// </param>
		/// <param name="outoff">The index of the first element in 'outbuf' where to write
		/// the data.
		/// 
		/// </param>
		public virtual void  toByteArray(int off, int len, byte[] outbuf, int outoff)
		{
			// Copy the data
			Array.Copy(buf, off, outbuf, outoff, len);
		}
		
		/// <summary> Returns the number of valid bytes in the output buffer (count class
		/// variable).
		/// 
		/// </summary>
		/// <returns> The number of bytes written to the buffer
		/// 
		/// </returns>
		public virtual int size()
		{
			return count;
		}
		
		/// <summary> Discards all the buffered data, by resetting the counter of written
		/// bytes to 0.
		/// 
		/// </summary>
		public virtual void  reset()
		{
			count = 0;
		}
		
		/// <summary> Returns the byte buffered at the given position in the buffer. The
		/// position in the buffer is the index of the 'write()' method call after
		/// the last call to 'reset()'.
		/// 
		/// </summary>
		/// <param name="pos">The position of the byte to return
		/// 
		/// </param>
		/// <returns> The value (betweeb 0-255) of the byte at position 'pos'.
		/// 
		/// </returns>
		public virtual int getByte(int pos)
		{
			if (pos >= count)
			{
				throw new System.ArgumentException();
			}
			return buf[pos] & 0xFF;
		}
	}
}