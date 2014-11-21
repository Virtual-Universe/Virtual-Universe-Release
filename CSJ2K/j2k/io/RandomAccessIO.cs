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
namespace CSJ2K.j2k.io
{
	
	/// <summary> This abstract class defines the interface to perform random access I/O. It
	/// implements the <tt>BinaryDataInput</tt> and <tt>BinaryDataOutput</tt>
	/// interfaces so that binary data input/output can be performed.
	/// 
	/// <p>This interface supports streams of up to 2 GB in length.</p>
	/// 
	/// </summary>
	/// <seealso cref="BinaryDataInput">
	/// </seealso>
	/// <seealso cref="BinaryDataOutput">
	/// 
	/// </seealso>
	public interface RandomAccessIO:BinaryDataInput, BinaryDataOutput
	{
		/// <summary> Returns the current position in the stream, which is the position from
		/// where the next byte of data would be read. The first byte in the stream
		/// is in position <tt>0</tt>.
		/// 
		/// </summary>
		/// <returns> The offset of the current position, in bytes.
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		int Pos
		{
			get;
			
		}
		
		/// <summary> Closes the I/O stream. Prior to closing the stream, any buffered data
		/// (at the bit and byte level) should be written.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error ocurred. 
		/// 
		/// </exception>
		void  close();
		
		/// <summary> Returns the current length of the stream, in bytes, taking into account
		/// any buffering.
		/// 
		/// </summary>
		/// <returns> The length of the stream, in bytes.
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error ocurred. 
		/// 
		/// </exception>
		int length();
		
		/// <summary> Moves the current position for the next read or write operation to
		/// offset. The offset is measured from the beginning of the stream. The
		/// offset may be set beyond the end of the file, if in write mode. Setting
		/// the offset beyond the end of the file does not change the file
		/// length. The file length will change only by writing after the offset
		/// has been set beyond the end of the file.
		/// 
		/// </summary>
		/// <param name="off">The offset where to move to.
		/// 
		/// </param>
		/// <exception cref="EOFException">If in read-only and seeking beyond EOF.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		void  seek(int off);
		
		/// <summary> Reads a byte of data from the stream. Prior to reading, the stream is
		/// realigned at the byte level.
		/// 
		/// </summary>
		/// <returns> The byte read, as an int.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		byte read();
		
		/// <summary> Reads up to len bytes of data from this file into an array of
		/// bytes. This method reads repeatedly from the stream until all the bytes
		/// are read. This method blocks until all the bytes are read, the end of
		/// the stream is detected, or an exception is thrown.
		/// 
		/// </summary>
		/// <param name="b">The buffer into which the data is to be read. It must be long
		/// enough.
		/// 
		/// </param>
		/// <param name="off">The index in 'b' where to place the first byte read.
		/// 
		/// </param>
		/// <param name="len">The number of bytes to read.
		/// 
		/// </param>
		/// <exception cref="EOFException">If the end-of file was reached before
		/// getting all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		void  readFully(byte[] b, int off, int len);
		
		/// <summary> Writes a byte to the stream. Prior to writing, the stream is realigned
		/// at the byte level.
		/// 
		/// </summary>
		/// <param name="b">The byte to write. The lower 8 bits of <tt>b</tt> are
		/// written.
		/// 
		/// </param>
		/// <exception cref="IOException">If an I/O error ocurred. 
		/// 
		/// </exception>
		void  write(byte b);
	}
}