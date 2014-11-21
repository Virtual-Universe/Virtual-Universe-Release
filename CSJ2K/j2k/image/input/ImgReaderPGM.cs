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
using CSJ2K.j2k.image;
using CSJ2K.j2k;
namespace CSJ2K.j2k.image.input
{
	
	/// <summary> This class implements the ImgData interface for reading 8 bit unsigned data
	/// from a binary PGM file.
	/// 
	/// <p>After being read the coefficients are level shifted by subtracting
	/// 2^(nominal bit range-1)</p>
	/// 
	/// <p>The TransferType (see ImgData) of this class is TYPE_INT.</p>
	/// 
	/// <P>NOTE: This class is not thread safe, for reasons of internal buffering.
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.image.ImgData">
	/// 
	/// </seealso>
	public class ImgReaderPGM:ImgReader
	{
		
		/// <summary>DC offset value used when reading image </summary>
		public static int DC_OFFSET = 128;
		
		/// <summary>Where to read the data from </summary>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		private System.IO.FileStream in_Renamed;
		
		/// <summary>The offset of the raw pixel data in the PGM file </summary>
		private int offset;
		
		/// <summary>The number of bits that determine the nominal dynamic range </summary>
		private int rb;
		
		/// <summary>The line buffer. </summary>
		// This makes the class not thrad safe
		// (but it is not the only one making it so)
		private byte[] buf;
		
		/// <summary>Temporary DataBlkInt object (needed when encoder uses floating-point
		/// filters). This avoid allocating new DataBlk at each time 
		/// </summary>
		private DataBlkInt intBlk;
		
		/// <summary> Creates a new PGM file reader from the specified file.
		/// 
		/// </summary>
		/// <param name="file">The input file.
		/// 
		/// </param>
		/// <exception cref="IOException">If an error occurs while opening the file.
		/// 
		/// </exception>
		public ImgReaderPGM(System.IO.FileInfo file):this(SupportClass.RandomAccessFileSupport.CreateRandomAccessFile(file, "r"))
		{
		}
		
		/// <summary> Creates a new PGM file reader from the specified file name.
		/// 
		/// </summary>
		/// <param name="fname">The input file name.
		/// 
		/// </param>
		/// <exception cref="IOException">If an error occurs while opening the file.
		/// 
		/// </exception>
		public ImgReaderPGM(System.String fname):this(SupportClass.RandomAccessFileSupport.CreateRandomAccessFile(fname, "r"))
		{
		}
		
		/// <summary> Creates a new PGM file reader from the specified RandomAccessFile
		/// object. The file header is read to acquire the image size.
		/// 
		/// </summary>
		/// <param name="in">From where to read the data 
		/// 
		/// </param>
		/// <exception cref="EOFException">if an EOF is read
		/// </exception>
		/// <exception cref="IOException">if an error occurs when opening the file
		/// 
		/// </exception>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public ImgReaderPGM(System.IO.FileStream in_Renamed)
		{
			this.in_Renamed = in_Renamed;
			
			confirmFileType();
			skipCommentAndWhiteSpace();
			this.w = readHeaderInt();
			skipCommentAndWhiteSpace();
			this.h = readHeaderInt();
			skipCommentAndWhiteSpace();
			/*Read the highest pixel value from header (not used)*/
			readHeaderInt();
			this.nc = 1;
			this.rb = 8;
		}
		
		
		/// <summary> Closes the underlying RandomAccessFile from where the image data is
		/// being read. No operations are possible after a call to this method.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs.
		/// 
		/// </exception>
		public override void  close()
		{
			in_Renamed.Close();
			in_Renamed = null;
		}
		
		/// <summary> Returns the number of bits corresponding to the nominal range of the
		/// data in the specified component. This is the value rb (range bits) that
		/// was specified in the constructor, which normally is 8 for non bilevel
		/// data, and 1 for bilevel data.
		/// 
		/// <P>If this number is <i>b</b> then the nominal range is between
		/// -2^(b-1) and 2^(b-1)-1, since unsigned data is level shifted to have a
		/// nominal average of 0.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The number of bits corresponding to the nominal range of the
		/// data. Fro floating-point data this value is not applicable and the
		/// return value is undefined.
		/// 
		/// </returns>
		public override int getNomRangeBits(int c)
		{
			// Check component index
			if (c != 0)
				throw new System.ArgumentException();
			
			return rb;
		}
		
		
		/// <summary> Returns the position of the fixed point in the specified component
		/// (i.e. the number of fractional bits), which is always 0 for this
		/// ImgReader.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The position of the fixed-point (i.e. the number of fractional
		/// bits). Always 0 for this ImgReader.
		/// 
		/// </returns>
		public override int getFixedPoint(int c)
		{
			// Check component index
			if (c != 0)
				throw new System.ArgumentException();
			return 0;
		}
		
		
		/// <summary> Returns, in the blk argument, the block of image data containing the
		/// specifed rectangular area, in the specified component. The data is
		/// returned, as a reference to the internal data, if any, instead of as a
		/// copy, therefore the returned data should not be modified.
		/// 
		/// <P> After being read the coefficients are level shifted by subtracting
		/// 2^(nominal bit range - 1)
		/// 
		/// <P>The rectangular area to return is specified by the 'ulx', 'uly', 'w'
		/// and 'h' members of the 'blk' argument, relative to the current
		/// tile. These members are not modified by this method. The 'offset' and
		/// 'scanw' of the returned data can be arbitrary. See the 'DataBlk' class.
		/// 
		/// <P>If the data array in <tt>blk</tt> is <tt>null</tt>, then a new one
		/// is created if necessary. The implementation of this interface may
		/// choose to return the same array or a new one, depending on what is more
		/// efficient. Therefore, the data array in <tt>blk</tt> prior to the
		/// method call should not be considered to contain the returned data, a
		/// new array may have been created. Instead, get the array from
		/// <tt>blk</tt> after the method has returned.
		/// 
		/// <P>The returned data always has its 'progressive' attribute unset
		/// (i.e. false).
		/// 
		/// <P>When an I/O exception is encountered the JJ2KExceptionHandler is
		/// used. The exception is passed to its handleException method. The action
		/// that is taken depends on the action that has been registered in
		/// JJ2KExceptionHandler. See JJ2KExceptionHandler for details.
		/// 
		/// </summary>
		/// <param name="blk">Its coordinates and dimensions specify the area to
		/// return. Some fields in this object are modified to return the data.
		/// 
		/// </param>
		/// <param name="c">The index of the component from which to get the data. Only 0
		/// is valid.
		/// 
		/// </param>
		/// <returns> The requested DataBlk
		/// 
		/// </returns>
		/// <seealso cref="getCompData">
		/// 
		/// </seealso>
		/// <seealso cref="JJ2KExceptionHandler">
		/// 
		/// </seealso>
		public override DataBlk getInternCompData(DataBlk blk, int c)
		{
			int k, j, i, mi;
			int[] barr;
			
			// Check component index
			if (c != 0)
				throw new System.ArgumentException();
			
			// Check type of block provided as an argument
			if (blk.DataType != DataBlk.TYPE_INT)
			{
				if (intBlk == null)
					intBlk = new DataBlkInt(blk.ulx, blk.uly, blk.w, blk.h);
				else
				{
					intBlk.ulx = blk.ulx;
					intBlk.uly = blk.uly;
					intBlk.w = blk.w;
					intBlk.h = blk.h;
				}
				blk = intBlk;
			}
			
			// Get data array
			barr = (int[]) blk.Data;
			if (barr == null || barr.Length < blk.w * blk.h)
			{
				barr = new int[blk.w * blk.h];
				blk.Data = barr;
			}
			
			// Check line buffer
			if (buf == null || buf.Length < blk.w)
			{
				buf = new byte[blk.w];
			}
			
			try
			{
				// Read line by line
				mi = blk.uly + blk.h;
				for (i = blk.uly; i < mi; i++)
				{
					// Reposition in input
					in_Renamed.Seek(offset + i * w + blk.ulx, System.IO.SeekOrigin.Begin);
					in_Renamed.Read(buf, 0, blk.w);
					for (k = (i - blk.uly) * blk.w + blk.w - 1, j = blk.w - 1; j >= 0; j--, k--)
					{
						barr[k] = (((int) buf[j]) & 0xFF) - DC_OFFSET;
					}
				}
			}
			catch (System.IO.IOException e)
			{
				JJ2KExceptionHandler.handleException(e);
			}
			
			// Turn off the progressive attribute
			blk.progressive = false;
			// Set buffer attributes
			blk.offset = 0;
			blk.scanw = blk.w;
			return blk;
		}
		
		/// <summary> Returns, in the blk argument, a block of image data containing the
		/// specifed rectangular area, in the specified component. The data is
		/// returned, as a copy of the internal data, therefore the returned data
		/// can be modified "in place".
		/// 
		/// <P> After being read the coefficients are level shifted by subtracting
		/// 2^(nominal bit range - 1)
		/// 
		/// <P>The rectangular area to return is specified by the 'ulx', 'uly', 'w'
		/// and 'h' members of the 'blk' argument, relative to the current
		/// tile. These members are not modified by this method. The 'offset' of
		/// the returned data is 0, and the 'scanw' is the same as the block's
		/// width. See the 'DataBlk' class.
		/// 
		/// <P>If the data array in 'blk' is 'null', then a new one is created. If
		/// the data array is not 'null' then it is reused, and it must be large
		/// enough to contain the block's data. Otherwise an 'ArrayStoreException'
		/// or an 'IndexOutOfBoundsException' is thrown by the Java system.
		/// 
		/// <P>The returned data has its 'progressive' attribute unset
		/// (i.e. false).
		/// 
		/// <P>This method just calls 'getInternCompData(blk, n)'.
		/// 
		/// <P>When an I/O exception is encountered the JJ2KExceptionHandler is
		/// used. The exception is passed to its handleException method. The action
		/// that is taken depends on the action that has been registered in
		/// JJ2KExceptionHandler. See JJ2KExceptionHandler for details.
		/// 
		/// </summary>
		/// <param name="blk">Its coordinates and dimensions specify the area to
		/// return. If it contains a non-null data array, then it must have the
		/// correct dimensions. If it contains a null data array a new one is
		/// created. The fields in this object are modified to return the data.
		/// 
		/// </param>
		/// <param name="c">The index of the component from which to get the data. Only 0
		/// is valid.
		/// 
		/// </param>
		/// <returns> The requested DataBlk
		/// 
		/// </returns>
		/// <seealso cref="getInternCompData">
		/// 
		/// </seealso>
		/// <seealso cref="JJ2KExceptionHandler">
		/// 
		/// </seealso>
		public override DataBlk getCompData(DataBlk blk, int c)
		{
			return getInternCompData(blk, c);
		}
		
		/// <summary> Returns a byte read from the RandomAccessIO. The number of read byted
		/// are counted to keep track of the offset of the pixel data in the PGM
		/// file
		/// 
		/// </summary>
		/// <returns> One byte read from the header of the PGM file.
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error occurs.
		/// 
		/// </exception>
		/// <exception cref="EOFException">If an EOF is read 
		/// 
		/// </exception>
		private byte countedByteRead()
		{
			offset++;
			return (byte) in_Renamed.ReadByte();
		}
		
		/// <summary> Checks that the RandomAccessIO begins with 'P5'
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs.
		/// </exception>
		/// <exception cref="EOFException">If an EOF is read
		/// 
		/// </exception>
		private void  confirmFileType()
		{
			byte[] type = new byte[]{80, 53}; // 'P5'
			int i;
			byte b;
			
			for (i = 0; i < 2; i++)
			{
				b = countedByteRead();
				if (b != type[i])
				{
					if (i == 1 && b == 50)
					{
						//i.e 'P2'
						throw new System.ArgumentException("JJ2000 does not support" + " ascii-PGM files. Use " + " raw-PGM file instead. ");
					}
					else
					{
						throw new System.ArgumentException("Not a raw-PGM file");
					}
				}
			}
		}
		
		/// <summary> Skips any line in the header starting with '#' and any space, tab, line
		/// feed or carriage return.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs.  
		/// </exception>
		/// <exception cref="EOFException">if an EOF is read
		/// 
		/// </exception>
		private void  skipCommentAndWhiteSpace()
		{
			
			bool done = false;
			byte b;
			
			while (!done)
			{
				b = countedByteRead();
				if (b == 35)
				{
					// Comment start
					while (b != 10 && b != 13)
					{
						// Comment ends in end of line
						b = countedByteRead();
					}
				}
				else if (!(b == 9 || b == 10 || b == 13 || b == 32))
				{
					// If not whitespace
					done = true;
				}
			}
			// Put last valid byte in
			offset--;
			in_Renamed.Seek(offset, System.IO.SeekOrigin.Begin);
		}
		
		
		/// <summary> Returns an int read from the header of the PGM file.
		/// 
		/// </summary>
		/// <returns> One int read from the header of the PGM file.
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error occurs.
		/// </exception>
		/// <exception cref="EOFException">If an EOF is read 
		/// 
		/// </exception>
		private int readHeaderInt()
		{
			int res = 0;
			byte b = 0;
			
			b = countedByteRead();
			while (b != 32 && b != 10 && b != 9 && b != 13)
			{
				// While not whitespace
				res = res * 10 + b - 48; // Covert ASCII to numerical value
				b = countedByteRead();
			}
			return res;
		}
		
		/// <summary> Returns true if the data read was originally signed in the specified
		/// component, false if not. This method returns always false since PGM
		/// data is always unsigned.
		/// 
		/// </summary>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <returns> always false, since PGM data is always unsigned.
		/// 
		/// </returns>
		public override bool isOrigSigned(int c)
		{
			// Check component index
			if (c != 0)
				throw new System.ArgumentException();
			return false;
		}
		
		/// <summary> Returns a string of information about the object, more than 1 line
		/// long. The information string includes information from the underlying
		/// RandomAccessIO (its toString() method is called in turn).
		/// 
		/// </summary>
		/// <returns> A string of information about the object.  
		/// 
		/// </returns>
		public override System.String ToString()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return "ImgReaderPGM: WxH = " + w + "x" + h + ", Component = 0" + "\nUnderlying RandomAccessIO:\n" + in_Renamed.ToString();
		}
	}
}