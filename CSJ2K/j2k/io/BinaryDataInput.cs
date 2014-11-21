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
	
	/// <summary> This interface defines the input of binary data from streams and/or files.
	/// 
	/// <p>Byte level input (i.e., for byte, int, long, float, etc.) should always
	/// be byte aligned. For example, a request to read an <tt>int</tt> should
	/// always realign the input at the byte level.</p>
	/// 
	/// <p>The implementation of this interface should clearly define if multi-byte
	/// input data is read in little- or big-endian byte ordering (least
	/// significant byte first or most significant byte first, respectively).</p>
	/// 
	/// </summary>
	/// <seealso cref="EndianType">
	/// 
	/// </seealso>
	public interface BinaryDataInput
	{
		/// <summary> Returns the endianess (i.e., byte ordering) of the implementing
		/// class. Note that an implementing class may implement only one type of
		/// endianness or both, which would be decided at creatiuon time.
		/// 
		/// </summary>
		/// <returns> Either <tt>EndianType.BIG_ENDIAN</tt> or
		/// <tt>EndianType.LITTLE_ENDIAN</tt>
		/// 
		/// </returns>
		/// <seealso cref="EndianType">
		/// 
		/// </seealso>
		int ByteOrdering
		{
			get;
			
		}
		
		/// <summary> Should read a signed byte (i.e., 8 bit) from the input.  reading, the
		/// input should be realigned at the byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned signed byte (8 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached before getting
		/// all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		byte readByte();
		
		/// <summary> Should read an unsigned byte (i.e., 8 bit) from the input. It is
		/// returned as an <tt>int</tt> since Java does not have an unsigned byte
		/// type. Prior to reading, the input should be realigned at the byte
		/// level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned unsigned byte (8 bit) from the input, as
		/// an <tt>int</tt>.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached before getting
		/// all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		byte readUnsignedByte();
		
		/// <summary> Should read a signed short (i.e., 16 bit) from the input. Prior to
		/// reading, the input should be realigned at the byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned signed short (16 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached before getting
		/// all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		short readShort();
		
		/// <summary> Should read an unsigned short (i.e., 16 bit) from the input. It is
		/// returned as an <tt>int</tt> since Java does not have an unsigned short
		/// type. Prior to reading, the input should be realigned at the byte
		/// level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned unsigned short (16 bit) from the input,
		/// as an <tt>int</tt>.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached before getting
		/// all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		int readUnsignedShort();
		
		/// <summary> Should read a signed int (i.e., 32 bit) from the input. Prior to
		/// reading, the input should be realigned at the byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned signed int (32 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached before getting
		/// all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		int readInt();
		
		/// <summary> Should read an unsigned int (i.e., 32 bit) from the input. It is
		/// returned as a <tt>long</tt> since Java does not have an unsigned short
		/// type. Prior to reading, the input should be realigned at the byte
		/// level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned unsigned int (32 bit) from the input, as
		/// a <tt>long</tt>.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached before getting
		/// all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		long readUnsignedInt();
		
		/// <summary> Should read a signed long (i.e., 64 bit) from the input. Prior to
		/// reading, the input should be realigned at the byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned signed long (64 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached before getting
		/// all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		long readLong();
		
		/// <summary> Should read an IEEE single precision (i.e., 32 bit) floating-point
		/// number from the input. Prior to reading, the input should be realigned
		/// at the byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned IEEE float (32 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached before getting
		/// all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		float readFloat();
		
		/// <summary> Should read an IEEE double precision (i.e., 64 bit) floating-point
		/// number from the input. Prior to reading, the input should be realigned
		/// at the byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned IEEE double (64 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="EOFException">If the end-of file was reached before getting
		/// all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		double readDouble();
		
		/// <summary> Skips <tt>n</tt> bytes from the input. Prior to skipping, the input
		/// should be realigned at the byte level.
		/// 
		/// </summary>
		/// <param name="n">The number of bytes to skip
		/// 
		/// </param>
		/// <exception cref="EOFException">If the end-of file was reached before all the
		/// bytes could be skipped.
		/// 
		/// </exception>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		int skipBytes(int n);
	}
}