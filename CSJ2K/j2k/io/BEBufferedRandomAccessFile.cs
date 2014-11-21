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
	
	/// <summary> This class defines a Buffered Random Access File, where all I/O is
	/// considered to be big-endian. It extends the
	/// <tt>BufferedRandomAccessFile</tt> class.
	/// 
	/// </summary>
	/// <seealso cref="RandomAccessIO">
	/// </seealso>
	/// <seealso cref="BinaryDataOutput">
	/// </seealso>
	/// <seealso cref="BinaryDataInput">
	/// </seealso>
	/// <seealso cref="BufferedRandomAccessFile">
	/// 
	/// </seealso>
	public class BEBufferedRandomAccessFile:BufferedRandomAccessFile, RandomAccessIO, EndianType
	{
		
		/// <summary> Constructor. Always needs a size for the buffer.
		/// 
		/// </summary>
		/// <param name="file">The file associated with the buffer
		/// 
		/// </param>
		/// <param name="mode">"r" for read, "rw" or "rw+" for read and write mode ("rw+"
		/// opens the file for update whereas "rw" removes it
		/// before. So the 2 modes are different only if the file
		/// already exists).
		/// 
		/// </param>
		/// <param name="bufferSize">The number of bytes to buffer
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public BEBufferedRandomAccessFile(System.IO.FileInfo file, System.String mode, int bufferSize):base(file, mode, bufferSize)
		{
			byte_Ordering = CSJ2K.j2k.io.EndianType_Fields.BIG_ENDIAN;
		}
		
		/// <summary> Constructor. Uses the default value for the byte-buffer size (512
		/// bytes).
		/// 
		/// </summary>
		/// <param name="file">The file associated with the buffer
		/// 
		/// </param>
		/// <param name="mode">"r" for read, "rw" or "rw+" for read and write mode ("rw+"
		/// opens the file for update whereas "rw" removes it
		/// before. So the 2 modes are different only if the file
		/// already exists).
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public BEBufferedRandomAccessFile(System.IO.FileInfo file, System.String mode):base(file, mode)
		{
			byte_Ordering = CSJ2K.j2k.io.EndianType_Fields.BIG_ENDIAN;
		}
		
		/// <summary> Constructor. Always needs a size for the buffer.
		/// 
		/// </summary>
		/// <param name="name">The name of the file associated with the buffer
		/// 
		/// </param>
		/// <param name="mode">"r" for read, "rw" or "rw+" for read and write mode ("rw+"
		/// opens the file for update whereas "rw" removes it
		/// before. So the 2 modes are different only if the file
		/// already exists).
		/// 
		/// </param>
		/// <param name="bufferSize">The number of bytes to buffer
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public BEBufferedRandomAccessFile(System.String name, System.String mode, int bufferSize):base(name, mode, bufferSize)
		{
			byte_Ordering = CSJ2K.j2k.io.EndianType_Fields.BIG_ENDIAN;
		}
		
		/// <summary> Constructor. Uses the default value for the byte-buffer size (512
		/// bytes).
		/// 
		/// </summary>
		/// <param name="name">The name of the file associated with the buffer
		/// 
		/// </param>
		/// <param name="mode">"r" for read, "rw" or "rw+" for read and write mode ("rw+"
		/// opens the file for update whereas "rw" removes it
		/// before. So the 2 modes are different only if the file
		/// already exists).
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public BEBufferedRandomAccessFile(System.String name, System.String mode):base(name, mode)
		{
			byte_Ordering = CSJ2K.j2k.io.EndianType_Fields.BIG_ENDIAN;
		}
		
		/// <summary> Writes the short value of <tt>v</tt> (i.e., 16 least significant bits)
		/// to the output. Prior to writing, the output should be realigned at the
		/// byte level.
		/// 
		/// <p>Signed or unsigned data can be written. To write a signed value just
		/// pass the <tt>short</tt> value as an argument. To write unsigned data
		/// pass the <tt>int</tt> value as an argument (it will be automatically
		/// casted, and only the 16 least significant bits will be written).</p>
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override void  writeShort(int v)
		{
			write(SupportClass.URShift(v, 8));
			write(v);
		}
		
		/// <summary> Writes the int value of <tt>v</tt> (i.e., the 32 bits) to the
		/// output. Prior to writing, the output should be realigned at the byte
		/// level.
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override void  writeInt(int v)
		{
			write(SupportClass.URShift(v, 24));
			write(SupportClass.URShift(v, 16));
			write(SupportClass.URShift(v, 8));
			write(v);
		}
		
		/// <summary> Writes the long value of <tt>v</tt> (i.e., the 64 bits) to the
		/// output. Prior to writing, the output should be realigned at the byte
		/// level.
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override void  writeLong(long v)
		{
			write((int) (SupportClass.URShift(v, 56)));
			write((int) (SupportClass.URShift(v, 48)));
			write((int) (SupportClass.URShift(v, 40)));
			write((int) (SupportClass.URShift(v, 32)));
			write((int) (SupportClass.URShift(v, 24)));
			write((int) (SupportClass.URShift(v, 16)));
			write((int) (SupportClass.URShift(v, 8)));
			write((int) v);
		}
		
		/// <summary> Writes the IEEE float value <tt>v</tt> (i.e., 32 bits) to the
		/// output. Prior to writing, the output should be realigned at the byte
		/// level.
		/// 
		/// </summary>
		/// <param name="v">The value to write to the output
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override void  writeFloat(float v)
		{
            // CONVERSION PROBLEM? OPTIMIZE!!!
            //byte[] floatbytes = BitConverter.GetBytes(v);
            //for (int i = floatbytes.Length-1; i >= 0 ; i--) write(floatbytes[i]);

			//UPGRADE_ISSUE: Method 'java.lang.Float.floatToIntBits' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangFloatfloatToIntBits_float'"
            //int intV = Float.floatToIntBits(v);
            int intV = BitConverter.ToInt32(BitConverter.GetBytes(v), 0);
			write(SupportClass.URShift(intV, 24));
			write(SupportClass.URShift(intV, 16));
			write(SupportClass.URShift(intV, 8));
			write(intV);
		}

        /// <summary> Writes the IEEE double value <tt>v</tt> (i.e., 64 bits) to the
        /// output. Prior to writing, the output should be realigned at the byte
        /// level.
        /// 
        /// </summary>
        /// <param name="v">The value to write to the output
        /// 
        /// </param>
        /// <exception cref="java.io.IOException">If an I/O error ocurred.
        /// 
        /// </exception>
        public override void writeDouble(double v)
		{
            //byte[] doublebytes = BitConverter.GetBytes(v);
            //for (int i = doublebytes.Length-1; i >= 0 ; i--) write(doublebytes[i]);
           
			//UPGRADE_ISSUE: Method 'java.lang.Double.doubleToLongBits' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangDoubledoubleToLongBits_double'"
			//long longV = Double.doubleToLongBits(v);
			long longV = BitConverter.ToInt64(BitConverter.GetBytes(v), 0);
			write((int) (SupportClass.URShift(longV, 56)));
			write((int) (SupportClass.URShift(longV, 48)));
			write((int) (SupportClass.URShift(longV, 40)));
			write((int) (SupportClass.URShift(longV, 32)));
			write((int) (SupportClass.URShift(longV, 24)));
			write((int) (SupportClass.URShift(longV, 16)));
			write((int) (SupportClass.URShift(longV, 8)));
			write((int) (longV));
            
		}

        /// <summary> Reads a signed short (i.e. 16 bit) from the input. Prior to reading,
        /// the input should be realigned at the byte level.
        /// 
        /// </summary>
        /// <returns> The next byte-aligned signed short (16 bit) from the input.
        /// 
        /// </returns>
        /// <exception cref="java.io.EOFException">If the end-of file was reached before
        /// getting all the necessary data.
        /// 
        /// </exception>
        /// <exception cref="java.io.IOException">If an I/O error ocurred.
        /// 
        /// </exception>
        public override short readShort()
		{
			return (short) ((read() << 8) | (read()));
		}
		
		/// <summary> Reads an unsigned short (i.e., 16 bit) from the input. It is returned
		/// as an <tt>int</tt> since Java does not have an unsigned short
		/// type. Prior to reading, the input should be realigned at the byte
		/// level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned unsigned short (16 bit) from the input,
		/// as an <tt>int</tt>.
		/// 
		/// </returns>
		/// <exception cref="java.io.EOFException">If the end-of file was reached before
		/// getting all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override int readUnsignedShort()
		{
			return ((read() << 8) | read());
		}
		
		/// <summary> Reads a signed int (i.e., 32 bit) from the input. Prior to reading, the
		/// input should be realigned at the byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned signed int (32 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="java.io.EOFException">If the end-of file was reached before
		/// getting all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override int readInt()
		{
			return ((read() << 24) | (read() << 16) | (read() << 8) | read());
		}
		
		/// <summary> Reads an unsigned int (i.e., 32 bit) from the input. It is returned as
		/// a <tt>long</tt> since Java does not have an unsigned short type. Prior
		/// to reading, the input should be realigned at the byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned unsigned int (32 bit) from the input, as
		/// a <tt>long</tt>.
		/// 
		/// </returns>
		/// <exception cref="java.io.EOFException">If the end-of file was reached before
		/// getting all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override long readUnsignedInt()
		{
			return (long) ((read() << 24) | (read() << 16) | (read() << 8) | read());
		}
		
		/// <summary> Reads a signed long (i.e., 64 bit) from the input. Prior to reading,
		/// the input should be realigned at the byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned signed long (64 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="java.io.EOFException">If the end-of file was reached before
		/// getting all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override long readLong()
		{
            //byte[] longbytes = new byte[8];
            //for (int i = longbytes.Length-1; i >= 0; i--) longbytes[i] = read();
            //return BitConverter.ToInt64(longbytes, 0);
			return ((long)(((ulong) read() << 56) | ((ulong) read() << 48) | ((ulong) read() << 40) | ((ulong) read() << 32) | ((ulong) read() << 24) | ((ulong) read() << 16) | ((ulong) read() << 8) | ((ulong) read())));
		}
		
		/// <summary> Reads an IEEE single precision (i.e., 32 bit) floating-point number
		/// from the input. Prior to reading, the input should be realigned at the
		/// byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned IEEE float (32 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="java.io.EOFException">If the end-of file was reached before
		/// getting all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override float readFloat()
		{
            // CONVERSION PROBLEM?  OPTIMIZE!!!
            //byte[] floatbytes = new byte[4];
            //for (int i = floatbytes.Length-1; i >= 0 ; i--) floatbytes[i] = (byte)read();
            //return BitConverter.ToSingle(floatbytes, 0);

			//UPGRADE_ISSUE: Method 'java.lang.Float.intBitsToFloat' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangFloatintBitsToFloat_int'"
            //return Float.intBitsToFloat((read() << 24) | (read() << 16) | (read() << 8) | (read()));
            return BitConverter.ToSingle(BitConverter.GetBytes((read() << 24) | (read() << 16) | (read() << 8) | (read())), 0);

		}
		
		/// <summary> Reads an IEEE double precision (i.e., 64 bit) floating-point number
		/// from the input. Prior to reading, the input should be realigned at the
		/// byte level.
		/// 
		/// </summary>
		/// <returns> The next byte-aligned IEEE double (64 bit) from the input.
		/// 
		/// </returns>
		/// <exception cref="java.io.EOFException">If the end-of file was reached before
		/// getting all the necessary data.
		/// 
		/// </exception>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public override double readDouble()
		{
            // CONVERSION PROBLEM?  OPTIMIZE!!!
            //byte[] doublebytes = new byte[8];
            //for (int i = doublebytes.Length-1; i >=0 ; i--) doublebytes[i] = (byte)read();
            //return BitConverter.ToDouble(doublebytes, 0);

			//UPGRADE_ISSUE: Method 'java.lang.Double.longBitsToDouble' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangDoublelongBitsToDouble_long'"
			//return Double.longBitsToDouble(((long) read() << 56) | ((long) read() << 48) | ((long) read() << 40) | ((long) read() << 32) | ((long) read() << 24) | ((long) read() << 16) | ((long) read() << 8) | ((long) read()));
            
            return BitConverter.ToDouble(BitConverter.GetBytes(((long) read() << 56) | ((long) read() << 48) | ((long) read() << 40) | ((long) read() << 32) | ((long) read() << 24) | ((long) read() << 16) | ((long) read() << 8) | ((long) read())), 0);

		}
		
		/// <summary> Returns a string of information about the file and the endianess 
		/// 
		/// </summary>
		public override System.String ToString()
		{
			return base.ToString() + "\nBig-Endian ordering";
		}
	}
}