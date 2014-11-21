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
using CSJ2K.j2k.codestream;
using CSJ2K.j2k.fileformat;
using CSJ2K.j2k.io;
namespace CSJ2K.j2k.fileformat.writer
{
	
	/// <summary> This class writes the file format wrapper that may or may not exist around
	/// a valid JPEG 2000 codestream. This class writes the simple possible legal
	/// fileformat
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.fileformat.reader.FileFormatReader">
	/// 
	/// </seealso>
	public class FileFormatWriter
	{
		
		/// <summary>The file from which to read the codestream and write file</summary>
		private BEBufferedRandomAccessFile fi;
		
		/// <summary>The name of the file from which to read the codestream and to write
		/// the JP2 file
		/// </summary>
		private System.String filename;
		
		/// <summary>Image height </summary>
		private int height;
		
		/// <summary>Image width </summary>
		private int width;
		
		/// <summary>Number of components </summary>
		private int nc;
		
		/// <summary>Bits per component </summary>
		private int[] bpc;
		
		/// <summary>Flag indicating whether number of bits per component varies </summary>
		private bool bpcVaries;
		
		/// <summary>Length of codestream </summary>
		private int clength;
		
		/// <summary>Length of Colour Specification Box </summary>
		private const int CSB_LENGTH = 15;
		
		/// <summary>Length of File Type Box </summary>
		private const int FTB_LENGTH = 20;
		
		/// <summary>Length of Image Header Box </summary>
		private const int IHB_LENGTH = 22;
		
		/// <summary>base length of Bits Per Component box </summary>
		private const int BPC_LENGTH = 8;
		
		
		
		/// <summary> The constructor of the FileFormatWriter. It receives all the
		/// information necessary about a codestream to generate a legal JP2 file
		/// 
		/// </summary>
		/// <param name="filename">The name of the file that is to be made a JP2 file
		/// 
		/// </param>
		/// <param name="height">The height of the image
		/// 
		/// </param>
		/// <param name="width">The width of the image
		/// 
		/// </param>
		/// <param name="nc">The number of components
		/// 
		/// </param>
		/// <param name="bpc">The number of bits per component
		/// 
		/// </param>
		/// <param name="clength">Length of codestream 
		/// 
		/// </param>
		public FileFormatWriter(System.String filename, int height, int width, int nc, int[] bpc, int clength)
		{
			this.height = height;
			this.width = width;
			this.nc = nc;
			this.bpc = bpc;
			this.filename = filename;
			this.clength = clength;
			
			bpcVaries = false;
			int fixbpc = bpc[0];
			for (int i = nc - 1; i > 0; i--)
			{
				if (bpc[i] != fixbpc)
					bpcVaries = true;
			}
		}
		
		
		
		/// <summary> This method reads the codestream and writes the file format wrapper and
		/// the codestream to the same file
		/// 
		/// </summary>
		/// <returns> The number of bytes increases because of the file format
		/// 
		/// </returns>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public virtual int writeFileFormat()
		{
			byte[] codestream;
			
			try
			{
				// Read and buffer the codestream
				fi = new BEBufferedRandomAccessFile(filename, "rw+");
				codestream = new byte[clength];
				fi.readFully(codestream, 0, clength);
				
				// Write the JP2_SINATURE_BOX
				fi.seek(0);
				fi.writeInt(0x0000000c);
				fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.JP2_SIGNATURE_BOX);
				fi.writeInt(0x0d0a870a);
				
				// Write File Type box
				writeFileTypeBox();
				
				// Write JP2 Header box
				writeJP2HeaderBox();
				
				// Write the Codestream box 
				writeContiguousCodeStreamBox(codestream);
				
				fi.close();
			}
			catch (System.Exception e)
			{
				throw new System.ApplicationException("Error while writing JP2 file format(2): " + e.Message + "\n" + e.StackTrace);
			}
			if (bpcVaries)
				return 12 + FTB_LENGTH + 8 + IHB_LENGTH + CSB_LENGTH + BPC_LENGTH + nc + 8;
			else
				return 12 + FTB_LENGTH + 8 + IHB_LENGTH + CSB_LENGTH + 8;
		}
		
		/// <summary> This method writes the File Type box
		/// 
		/// </summary>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public virtual void  writeFileTypeBox()
		{
			// Write box length (LBox)
			// LBox(4) + TBox (4) + BR(4) + MinV(4) + CL(4) = 20
			fi.writeInt(FTB_LENGTH);
			
			// Write File Type box (TBox)
			fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.FILE_TYPE_BOX);
			
			// Write File Type data (DBox)
			// Write Brand box (BR)
			fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.FT_BR);
			
			// Write Minor Version
			fi.writeInt(0);
			
			// Write Compatibility list
			fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.FT_BR);
		}
		
		/// <summary> This method writes the JP2Header box
		/// 
		/// </summary>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public virtual void  writeJP2HeaderBox()
		{
			
			// Write box length (LBox)
			// if the number of bits per components varies, a bpcc box is written
			if (bpcVaries)
				fi.writeInt(8 + IHB_LENGTH + CSB_LENGTH + BPC_LENGTH + nc);
			else
				fi.writeInt(8 + IHB_LENGTH + CSB_LENGTH);
			
			// Write a JP2Header (TBox)
			fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.JP2_HEADER_BOX);
			
			// Write image header box 
			writeImageHeaderBox();
			
			// Write Colour Bpecification Box
			writeColourSpecificationBox();
			
			// if the number of bits per components varies write bpcc box
			if (bpcVaries)
				writeBitsPerComponentBox();
		}
		
		/// <summary> This method writes the Bits Per Component box
		/// 
		/// </summary>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public virtual void  writeBitsPerComponentBox()
		{
			
			// Write box length (LBox)
			fi.writeInt(BPC_LENGTH + nc);
			
			// Write a Bits Per Component box (TBox)
			fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.BITS_PER_COMPONENT_BOX);
			
			// Write bpc fields
			for (int i = 0; i < nc; i++)
			{
				fi.writeByte(bpc[i] - 1);
			}
		}
		
		/// <summary> This method writes the Colour Specification box
		/// 
		/// </summary>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public virtual void  writeColourSpecificationBox()
		{
			
			// Write box length (LBox)
			fi.writeInt(CSB_LENGTH);
			
			// Write a Bits Per Component box (TBox)
			fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.COLOUR_SPECIFICATION_BOX);
			
			// Write METH field
			fi.writeByte(CSJ2K.j2k.fileformat.FileFormatBoxes.CSB_METH);
			
			// Write PREC field
			fi.writeByte(CSJ2K.j2k.fileformat.FileFormatBoxes.CSB_PREC);
			
			// Write APPROX field
			fi.writeByte(CSJ2K.j2k.fileformat.FileFormatBoxes.CSB_APPROX);
			
			// Write EnumCS field
			if (nc > 1)
				fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.CSB_ENUM_SRGB);
			else
				fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.CSB_ENUM_GREY);
		}
		
		/// <summary> This method writes the Image Header box
		/// 
		/// </summary>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public virtual void  writeImageHeaderBox()
		{
			
			// Write box length
			fi.writeInt(IHB_LENGTH);
			
			// Write ihdr box name
			fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.IMAGE_HEADER_BOX);
			
			// Write HEIGHT field
			fi.writeInt(height);
			
			// Write WIDTH field
			fi.writeInt(width);
			
			// Write NC field
			fi.writeShort(nc);
			
			// Write BPC field
			// if the number of bits per component varies write 0xff else write
			// number of bits per components
			if (bpcVaries)
				fi.writeByte(0xff);
			else
				fi.writeByte(bpc[0] - 1);
			
			// Write C field
			fi.writeByte(CSJ2K.j2k.fileformat.FileFormatBoxes.IMB_C);
			
			// Write UnkC field
			fi.writeByte(CSJ2K.j2k.fileformat.FileFormatBoxes.IMB_UnkC);
			
			// Write IPR field
			fi.writeByte(CSJ2K.j2k.fileformat.FileFormatBoxes.IMB_IPR);
		}
		
		/// <summary> This method writes the Contiguous codestream box
		/// 
		/// </summary>
		/// <param name="cs">The contiguous codestream
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public virtual void  writeContiguousCodeStreamBox(byte[] cs)
		{
			
			// Write box length (LBox)
			// This value is set to 0 since in this implementation, this box is
			// always last
			fi.writeInt(clength + 8);
			
			// Write contiguous codestream box name (TBox)
			fi.writeInt(CSJ2K.j2k.fileformat.FileFormatBoxes.CONTIGUOUS_CODESTREAM_BOX);
			
			// Write codestream
			for (int i = 0; i < clength; i++)
				fi.writeByte(cs[i]);
		}
	}
}