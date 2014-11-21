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
namespace CSJ2K.j2k.entropy.decoder
{
	
	/// <summary> This class provides a byte input facility from byte buffers. It is similar
	/// to the ByteArrayInputStream class, but adds the possibility to add data to
	/// the stream after the creation of the object.
	/// 
	/// <p>Unlike the ByteArrayInputStream this class is not thread safe (i.e. no
	/// two threads can use the same object at the same time, but different objects
	/// may be used in different threads).</p>
	/// 
	/// <p>This class can modify the contents of the buffer given to the
	/// constructor, when the addByteArray() method is called.</p>
	/// 
	/// </summary>
	/// <seealso cref="InputStream">
	/// 
	/// </seealso>
	public class ByteInputBuffer
	{
		
		/// <summary>The byte array containing the data </summary>
		private byte[] buf;
		
		/// <summary>The index one greater than the last valid character in the input
		/// stream buffer 
		/// </summary>
		private int count;
		
		/// <summary>The index of the next character to read from the input stream buffer
		/// 
		/// </summary>
		private int pos;
		
		/// <summary> Creates a new byte array input stream that reads data from the
		/// specified byte array. The byte array is not copied.
		/// 
		/// </summary>
		/// <param name="buf">the input buffer.
		/// 
		/// </param>
		public ByteInputBuffer(byte[] buf)
		{
			this.buf = buf;
			count = buf.Length;
		}
		
		/// <summary> Creates a new byte array input stream that reads data from the
		/// specified byte array. Up to length characters are to be read from the
		/// byte array, starting at the indicated offset.
		/// 
		/// <p>The byte array is not copied.</p>
		/// 
		/// </summary>
		/// <param name="buf">the input buffer.
		/// 
		/// </param>
		/// <param name="offset">the offset in the buffer of the first byte to read.
		/// 
		/// </param>
		/// <param name="length">the maximum number of bytes to read from the buffer.
		/// 
		/// </param>
		public ByteInputBuffer(byte[] buf, int offset, int length)
		{
			this.buf = buf;
			pos = offset;
			count = offset + length;
		}
		
		/// <summary> Sets the underlying buffer byte array to the given one, with the given
		/// offset and length. If 'buf' is null then the current byte buffer is
		/// assumed. If 'offset' is negative, then it will be assumed to be
		/// 'off+len', where 'off' and 'len' are the offset and length of the
		/// current byte buffer.
		/// 
		/// <p>The byte array is not copied.</p>
		/// 
		/// </summary>
		/// <param name="buf">the input buffer. If null it is the current input buffer.
		/// 
		/// </param>
		/// <param name="offset">the offset in the buffer of the first byte to read. If
		/// negative it is assumed to be the byte just after the end of the current
		/// input buffer, only permitted if 'buf' is null.
		/// 
		/// </param>
		/// <param name="length">the maximum number of bytes to read frmo the buffer.
		/// 
		/// </param>
		public virtual void  setByteArray(byte[] buf, int offset, int length)
		{
			// In same buffer?
			if (buf == null)
			{
				if (length < 0 || count + length > this.buf.Length)
				{
					throw new System.ArgumentException();
				}
				if (offset < 0)
				{
					pos = count;
					count += length;
				}
				else
				{
					count = offset + length;
					pos = offset;
				}
			}
			else
			{
				// New input buffer
				if (offset < 0 || length < 0 || offset + length > buf.Length)
				{
					throw new System.ArgumentException();
				}
				this.buf = buf;
				count = offset + length;
				pos = offset;
			}
		}
		
		/// <summary> Adds the specified data to the end of the byte array stream. This
		/// method modifies the byte array buffer. It can also discard the already
		/// read input.
		/// 
		/// </summary>
		/// <param name="data">The data to add. The data is copied.
		/// 
		/// </param>
		/// <param name="off">The index, in data, of the first element to add to the
		/// stream.
		/// 
		/// </param>
		/// <param name="len">The number of elements to add to the array.
		/// 
		/// </param>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'addByteArray'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  addByteArray(byte[] data, int off, int len)
		{
			lock (this)
			{
				// Check integrity
				if (len < 0 || off < 0 || len + off > buf.Length)
				{
					throw new System.ArgumentException();
				}
				// Copy new data
				if (count + len <= buf.Length)
				{
					// Enough place in 'buf'
					Array.Copy(data, off, buf, count, len);
					count += len;
				}
				else
				{
					if (count - pos + len <= buf.Length)
					{
						// Enough place in 'buf' if we move input data
						// Move buffer
						Array.Copy(buf, pos, buf, 0, count - pos);
					}
					else
					{
						// Not enough place in 'buf', use new buffer
						byte[] oldbuf = buf;
						buf = new byte[count - pos + len];
						// Copy buffer
						Array.Copy(oldbuf, count, buf, 0, count - pos);
					}
					count -= pos;
					pos = 0;
					// Copy new data
					Array.Copy(data, off, buf, count, len);
					count += len;
				}
			}
		}
		
		/// <summary> Reads the next byte of data from this input stream. The value byte is
		/// returned as an int in the range 0 to 255. If no byte is available
		/// because the end of the stream has been reached, the EOFException
		/// exception is thrown.
		/// 
		/// <p>This method is not synchronized, so it is not thread safe.</p>
		/// 
		/// </summary>
		/// <returns> The byte read in the range 0-255.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end of the stream is reached.
		/// 
		/// </exception>
		public virtual int readChecked()
		{
			if (pos < count)
			{
				return (int) buf[pos++] & 0xFF;
			}
			else
			{
				throw new System.IO.EndOfStreamException();
			}
		}
		
		/// <summary> Reads the next byte of data from this input stream. The value byte is
		/// returned as an int in the range 0 to 255. If no byte is available
		/// because the end of the stream has been reached, -1 is returned.
		/// 
		/// <p>This method is not synchronized, so it is not thread safe.</p>
		/// 
		/// </summary>
		/// <returns> The byte read in the range 0-255, or -1 if the end of stream
		/// has been reached.
		/// 
		/// </returns>
		public virtual int read()
		{
			if (pos < count)
			{
				return (int) buf[pos++] & 0xFF;
			}
			else
			{
				return - 1;
			}
		}
	}
}