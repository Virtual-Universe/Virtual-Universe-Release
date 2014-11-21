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
	
	/// <summary> This interface defines the output of binary data to streams and/or files.
	/// 
	/// <P>Byte level output (i.e., for byte, int, long, float, etc.) should
	/// always be byte aligned. For example, a request to write an
	/// <tt>int</tt> should always realign the output at the byte level.
	/// 
	/// <P>The implementation of this interface should clearly define if
	/// multi-byte output data is written in little- or big-endian byte
	/// ordering (least significant byte first or most significant byte
	/// first, respectively).
	/// 
	/// </summary>
	/// <seealso cref="EndianType">
	/// 
	/// </seealso>
	public interface BinaryDataOutput
	{
		/// <summary> Returns the endianness (i.e., byte ordering) of the implementing
		/// class. Note that an implementing class may implement only one
		/// type of endianness or both, which would be decided at creatiuon
		/// time.
		/// 
		/// </summary>
		/// <returns> Either <tt>EndianType.BIG_ENDIAN</tt> or
		/// <tt>EndianType.LITTLE_ENDIAN</tt>
		/// 
		/// </returns>
		/// <seealso cref="EndianType">
		/// 
		/// 
		/// 
		/// </seealso>
		int ByteOrdering
		{
			get;
			
		}
		
		/// <summary> Should write the byte value of <tt>v</tt> (i.e., 8 least
		/// significant bits) to the output. Prior to writing, the output
		/// should be realigned at the byte level.
		/// 
		/// <P>Signed or unsigned data can be written. To write a signed
		/// value just pass the <tt>byte</tt> value as an argument. To
		/// write unsigned data pass the <tt>int</tt> value as an argument
		/// (it will be automatically casted, and only the 8 least
		/// significant bits will be written).
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// 
		/// 
		/// </exception>
		void  writeByte(int v);
		
		/// <summary> Should write the short value of <tt>v</tt> (i.e., 16 least
		/// significant bits) to the output. Prior to writing, the output
		/// should be realigned at the byte level.
		/// 
		/// <P>Signed or unsigned data can be written. To write a signed
		/// value just pass the <tt>short</tt> value as an argument. To
		/// write unsigned data pass the <tt>int</tt> value as an argument
		/// (it will be automatically casted, and only the 16 least
		/// significant bits will be written).
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// 
		/// 
		/// </exception>
		void  writeShort(int v);
		
		/// <summary> Should write the int value of <tt>v</tt> (i.e., the 32 bits) to
		/// the output. Prior to writing, the output should be realigned at
		/// the byte level.
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// 
		/// 
		/// </exception>
		void  writeInt(int v);
		
		/// <summary> Should write the long value of <tt>v</tt> (i.e., the 64 bits)
		/// to the output. Prior to writing, the output should be realigned
		/// at the byte level.
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// 
		/// 
		/// </exception>
		void  writeLong(long v);
		
		/// <summary> Should write the IEEE float value <tt>v</tt> (i.e., 32 bits) to
		/// the output. Prior to writing, the output should be realigned at
		/// the byte level.
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// 
		/// 
		/// </exception>
		void  writeFloat(float v);
		
		/// <summary> Should write the IEEE double value <tt>v</tt> (i.e., 64 bits)
		/// to the output. Prior to writing, the output should be realigned
		/// at the byte level.
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// 
		/// 
		/// </exception>
		void  writeDouble(double v);
		
		/// <summary> Any data that has been buffered must be written, and the stream should
		/// be realigned at the byte level.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error ocurred.
		/// 
		/// 
		/// 
		/// </exception>
		void  flush();
	}
}