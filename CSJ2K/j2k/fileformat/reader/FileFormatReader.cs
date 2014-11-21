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
using CSJ2K.j2k.util;
using CSJ2K.j2k.io;
namespace CSJ2K.j2k.fileformat.reader
{
	
	/// <summary> This class reads the file format wrapper that may or may not exist around a
	/// valid JPEG 2000 codestream. Since no information from the file format is
	/// used in the actual decoding, this class simply goes through the file and
	/// finds the first valid codestream.
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.fileformat.writer.FileFormatWriter">
	/// 
	/// </seealso>
	public class FileFormatReader
	{
		/// <summary> This method creates and returns an array of positions to contiguous
		/// codestreams in the file
		/// 
		/// </summary>
		/// <returns> The positions of the contiguous codestreams in the file
		/// 
		/// </returns>
		virtual public long[] CodeStreamPos
		{
			get
			{
				int size = codeStreamPos.Count;
				long[] pos = new long[size];
				for (int i = 0; i < size; i++)
					pos[i] = (long) ((System.Int32) (codeStreamPos[i]));
				return pos;
			}
			
		}
		/// <summary> This method returns the position of the first contiguous codestreams in
		/// the file
		/// 
		/// </summary>
		/// <returns> The position of the first contiguous codestream in the file
		/// 
		/// </returns>
		virtual public int FirstCodeStreamPos
		{
			get
			{
				return ((System.Int32) (codeStreamPos[0]));
			}
			
		}
		/// <summary> This method returns the length of the first contiguous codestreams in
		/// the file
		/// 
		/// </summary>
		/// <returns> The length of the first contiguous codestream in the file
		/// 
		/// </returns>
		virtual public int FirstCodeStreamLength
		{
			get
			{
				return ((System.Int32) (codeStreamLength[0]));
			}
			
		}
		
		/// <summary>The random access from which the file format boxes are read </summary>
		private RandomAccessIO in_Renamed;
		
		/// <summary>The positions of the codestreams in the fileformat</summary>
		private System.Collections.ArrayList codeStreamPos;
		
		/// <summary>The lengths of the codestreams in the fileformat</summary>
		private System.Collections.ArrayList codeStreamLength;
		
		/// <summary>Flag indicating whether or not the JP2 file format is used </summary>
		public bool JP2FFUsed;
		
		/// <summary> The constructor of the FileFormatReader
		/// 
		/// </summary>
		/// <param name="in">The RandomAccessIO from which to read the file format
		/// 
		/// </param>
		public FileFormatReader(RandomAccessIO in_Renamed)
		{
			this.in_Renamed = in_Renamed;
		}
		
		/// <summary> This method checks whether the given RandomAccessIO is a valid JP2 file
		/// and if so finds the first codestream in the file. Currently, the
		/// information in the codestream is not used
		/// 
		/// </summary>
		/// <param name="in">The RandomAccessIO from which to read the file format
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		/// <exception cref="java.io.EOFException">If end of file is reached
		/// 
		/// </exception>
		public virtual void  readFileFormat()
		{
			
			//int foundCodeStreamBoxes = 0;
			int box;
			int length;
			long longLength = 0;
			int pos;
			short marker;
			bool jp2HeaderBoxFound = false;
			bool lastBoxFound = false;
			
			try
			{
				
				// Go through the randomaccessio and find the first contiguous
				// codestream box. Check also that the File Format is correct
				
				// Make sure that the first 12 bytes is the JP2_SIGNATURE_BOX or
				// if not that the first 2 bytes is the SOC marker
				if (in_Renamed.readInt() != 0x0000000c || in_Renamed.readInt() != CSJ2K.j2k.fileformat.FileFormatBoxes.JP2_SIGNATURE_BOX || in_Renamed.readInt() != 0x0d0a870a)
				{
					// Not a JP2 file
					in_Renamed.seek(0);
					
					marker = (short) in_Renamed.readShort();
					if (marker != CSJ2K.j2k.codestream.Markers.SOC)
					//Standard syntax marker found
						throw new System.ApplicationException("File is neither valid JP2 file nor " + "valid JPEG 2000 codestream");
					JP2FFUsed = false;
					in_Renamed.seek(0);
					return ;
				}
				
				// The JP2 File format is being used
				JP2FFUsed = true;
				
				// Read File Type box
				if (!readFileTypeBox())
				{
					// Not a valid JP2 file or codestream
					throw new System.ApplicationException("Invalid JP2 file: File Type box missing");
				}
				
				// Read all remaining boxes 
				while (!lastBoxFound)
				{
					pos = in_Renamed.Pos;
					length = in_Renamed.readInt();
					if ((pos + length) == in_Renamed.length())
						lastBoxFound = true;
					
					box = in_Renamed.readInt();
					if (length == 0)
					{
						lastBoxFound = true;
						length = in_Renamed.length() - in_Renamed.Pos;
					}
					else if (length == 1)
					{
						longLength = in_Renamed.readLong();
						throw new System.IO.IOException("File too long.");
					}
					else
						longLength = (long) 0;
					
					switch (box)
					{
						
						case CSJ2K.j2k.fileformat.FileFormatBoxes.CONTIGUOUS_CODESTREAM_BOX: 
							if (!jp2HeaderBoxFound)
							{
								throw new System.ApplicationException("Invalid JP2 file: JP2Header box not " + "found before Contiguous codestream " + "box ");
							}
							readContiguousCodeStreamBox(pos, length, longLength);
							break;
						
						case CSJ2K.j2k.fileformat.FileFormatBoxes.JP2_HEADER_BOX: 
							if (jp2HeaderBoxFound)
								throw new System.ApplicationException("Invalid JP2 file: Multiple " + "JP2Header boxes found");
							readJP2HeaderBox(pos, length, longLength);
							jp2HeaderBoxFound = true;
							break;
						
						case CSJ2K.j2k.fileformat.FileFormatBoxes.INTELLECTUAL_PROPERTY_BOX: 
							readIntPropertyBox(length);
							break;
						
						case CSJ2K.j2k.fileformat.FileFormatBoxes.XML_BOX: 
							readXMLBox(length);
							break;
						
						case CSJ2K.j2k.fileformat.FileFormatBoxes.UUID_BOX: 
							readUUIDBox(length);
							break;
						
						case CSJ2K.j2k.fileformat.FileFormatBoxes.UUID_INFO_BOX: 
							readUUIDInfoBox(length);
							break;
                        
                        case CSJ2K.j2k.fileformat.FileFormatBoxes.READER_REQUIREMENTS_BOX:
                            readReaderRequirementsBox(length);
                            break;
						
						default: 
							FacilityManager.getMsgLogger().printmsg(CSJ2K.j2k.util.MsgLogger_Fields.WARNING, "Unknown box-type: 0x" + System.Convert.ToString(box, 16));
							break;
						
					}
					if (!lastBoxFound)
						in_Renamed.seek(pos + length);
				}
			}
			catch (System.IO.EndOfStreamException)
			{
				throw new System.ApplicationException("EOF reached before finding Contiguous " + "Codestream Box");
			}
			
			if (codeStreamPos.Count == 0)
			{
				// Not a valid JP2 file or codestream
				throw new System.ApplicationException("Invalid JP2 file: Contiguous codestream box " + "missing");
			}
			
			return ;
		}
		
		/// <summary> This method reads the File Type box.
		/// 
		/// </summary>
		/// <returns> false if the File Type box was not found or invalid else true
		/// 
		/// </returns>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// </exception>
		/// <exception cref="java.io.EOFException">If the end of file was reached
		/// 
		/// </exception>
		public virtual bool readFileTypeBox()
		{
			int length;
			long longLength = 0;
			int pos;
			int nComp;
			bool foundComp = false;
			
			// Get current position in file
			pos = in_Renamed.Pos;
			
			// Read box length (LBox)
			length = in_Renamed.readInt();
			if (length == 0)
			{
				// This can not be last box
				throw new System.ApplicationException("Zero-length of Profile Box");
			}
			
			// Check that this is a File Type box (TBox)
			if (in_Renamed.readInt() != CSJ2K.j2k.fileformat.FileFormatBoxes.FILE_TYPE_BOX)
			{
				return false;
			}
			
			// Check for XLBox
			if (length == 1)
			{
				// Box has 8 byte length;
				longLength = in_Renamed.readLong();
				throw new System.IO.IOException("File too long.");
			}
			
			// Read Brand field
			in_Renamed.readInt();
			
			// Read MinV field
			in_Renamed.readInt();
			
			// Check that there is at least one FT_BR entry in in
			// compatibility list
			nComp = (length - 16) / 4; // Number of compatibilities.
			for (int i = nComp; i > 0; i--)
			{
				if (in_Renamed.readInt() == CSJ2K.j2k.fileformat.FileFormatBoxes.FT_BR)
					foundComp = true;
			}
			if (!foundComp)
			{
				return false;
			}
			
			return true;
		}
		
		/// <summary> This method reads the JP2Header box
		/// 
		/// </summary>
		/// <param name="pos">The position in the file
		/// 
		/// </param>
		/// <param name="length">The length of the JP2Header box
		/// 
		/// </param>
		/// <param name="long">length The length of the JP2Header box if greater than
		/// 1<<32
		/// 
		/// </param>
		/// <returns> false if the JP2Header box was not found or invalid else true
		/// 
		/// </returns>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		/// <exception cref="java.io.EOFException">If the end of file was reached
		/// 
		/// </exception>
		public virtual bool readJP2HeaderBox(long pos, int length, long longLength)
		{
			
			if (length == 0)
			{
				// This can not be last box
				throw new System.ApplicationException("Zero-length of JP2Header Box");
			}
			
			// Here the JP2Header data (DBox) would be read if we were to use it
			
			return true;
		}
		
		/// <summary> This method skips the Contiguous codestream box and adds position
		/// of contiguous codestream to a vector
		/// 
		/// </summary>
		/// <param name="pos">The position in the file
		/// 
		/// </param>
		/// <param name="length">The length of the JP2Header box
		/// 
		/// </param>
		/// <param name="long">length The length of the JP2Header box if greater than 1<<32
		/// 
		/// </param>
		/// <returns> false if the Contiguous codestream box was not found or invalid
		/// else true
		/// 
		/// </returns>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		/// <exception cref="java.io.EOFException">If the end of file was reached
		/// 
		/// </exception>
		public virtual bool readContiguousCodeStreamBox(long pos, int length, long longLength)
		{
			
			// Add new codestream position to position vector
			int ccpos = in_Renamed.Pos;
			
			if (codeStreamPos == null)
				codeStreamPos = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			codeStreamPos.Add((System.Int32) ccpos);
			
			// Add new codestream length to length vector
			if (codeStreamLength == null)
				codeStreamLength = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			codeStreamLength.Add((System.Int32) length);
			
			return true;
		}
		
		/// <summary> This method reads the contents of the Intellectual property box
		/// 
		/// </summary>
		public virtual void  readIntPropertyBox(int length)
		{
		}
		
		/// <summary> This method reads the contents of the XML box
		/// 
		/// </summary>
		public virtual void  readXMLBox(int length)
		{
		}
		
		/// <summary> This method reads the contents of the Intellectual property box
		/// 
		/// </summary>
		public virtual void  readUUIDBox(int length)
		{
		}
		
		/// <summary> This method reads the contents of the Intellectual property box
		/// 
		/// </summary>
		public virtual void  readUUIDInfoBox(int length)
		{
		}

        /// <summary> This method reads the contents of the Reader requirements box
        /// 
        /// </summary>
        public virtual void readReaderRequirementsBox(int length)
        {
        }
	}
}