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
using CSJ2K.j2k.io;
namespace CSJ2K.j2k.util
{
	
	/// <summary> This class takes a legal JPEG 2000 codestream and performs some
	/// manipulation on it. Currently the manipulations supported are: Tile-parts
	/// 
	/// </summary>
	public class CodestreamManipulator
	{
		private void  InitBlock()
		{
			ppt = new int[nt];
		}
		
		/// <summary>Flag indicating whether packed packet headers in main header is used
		/// 
		/// </summary>
		private bool ppmUsed;
		
		/// <summary>Flag indicating whether packed packet headers in tile headers is used
		/// 
		/// </summary>
		private bool pptUsed;
		
		/// <summary>Flag indicating whether SOP marker was only intended for parsing in
		/// This class and should be removed 
		/// </summary>
		private bool tempSop;
		
		/// <summary>Flag indicating whether EPH marker was only intended for parsing in
		/// This class and should be removed 
		/// </summary>
		private bool tempEph;
		
		/// <summary>The number of tiles in the image </summary>
		private int nt;
		
		/// <summary>The number of packets per tile-part </summary>
		private int pptp;
		
		/// <summary>The name of the outfile </summary>
		private System.String outname;
		
		/// <summary>The length of a SOT plus a SOD marker </summary>
		private static int TP_HEAD_LEN = 14;
		
		/// <summary>The maximum number of a tile part index (TPsot) </summary>
		//private static int MAX_TPSOT = 16;
		
		/// <summary>The maximum number of tile parts in any tile </summary>
		private int maxtp;
		
		/// <summary>The number of packets per tile </summary>
		//UPGRADE_NOTE: The initialization of  'ppt' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private int[] ppt;
		
		/// <summary>The positions of the SOT, SOP and EPH markers </summary>
		private System.Int32[] positions;
		
		/// <summary>The main header </summary>
		private byte[] mainHeader;
		
		/// <summary>Buffers containing the tile parts </summary>
		private byte[][][] tileParts;
		
		/// <summary>Buffers containing the original tile headers </summary>
		private byte[][] tileHeaders;
		
		/// <summary>Buffers contaning the packet headers </summary>
		private byte[][][] packetHeaders;
		
		/// <summary>Buffers containing the packet data </summary>
		private byte[][][] packetData;
		
		/// <summary>Buffers containing the SOP marker segments </summary>
		private byte[][][] sopMarkSeg;
		
		/// <summary> Instantiates a codestream manipulator..
		/// 
		/// </summary>
		/// <param name="outname">The name of the original outfile
		/// 
		/// </param>
		/// <param name="nt">The number of tiles in the image
		/// 
		/// </param>
		/// <param name="pptp">Packets per tile-part. If zero, no division into tileparts
		/// is performed
		/// 
		/// </param>
		/// <param name="ppm">Flag indicating that PPM marker is used
		/// 
		/// </param>
		/// <param name="ppt">Flag indicating that PPT marker is used
		/// 
		/// </param>
		/// <param name="tempSop">Flag indicating whether SOP merker should be removed
		/// 
		/// </param>
		/// <param name="tempEph">Flag indicating whether EPH merker should be removed
		/// 
		/// </param>
		public CodestreamManipulator(System.String outname, int nt, int pptp, bool ppm, bool ppt, bool tempSop, bool tempEph)
		{
			InitBlock();
			this.outname = outname;
			this.nt = nt;
			this.pptp = pptp;
			this.ppmUsed = ppm;
			this.pptUsed = ppt;
			this.tempSop = tempSop;
			this.tempEph = tempEph;
		}
		
		/// <summary> This method performs the actual manipulation of the codestream which is
		/// the reparsing for tile parts and packed packet headers
		/// 
		/// </summary>
		/// <returns> The number of bytes that the file has increased by
		/// 
		/// </returns>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		public virtual int doCodestreamManipulation()
		{
			BEBufferedRandomAccessFile fi;
			int addedHeaderBytes = 0;
			ppt = new int[nt];
			tileParts = new byte[nt][][];
			tileHeaders = new byte[nt][];
			packetHeaders = new byte[nt][][];
			packetData = new byte[nt][][];
			sopMarkSeg = new byte[nt][][];
			
			// If neither packed packet header nor tile parts are used, return 0
			if (ppmUsed == false && pptUsed == false && pptp == 0)
				return 0;
			
			// Open file for reading and writing
			fi = new BEBufferedRandomAccessFile(outname, "rw+");
			addedHeaderBytes -= fi.length();
			
			// Parse the codestream for SOT, SOP and EPH markers
			parseAndFind(fi);
			
			// Read and buffer the tile headers, packet headers and packet data
			readAndBuffer(fi);
			
			// Close file and overwrite with new file
			fi.close();
			fi = new BEBufferedRandomAccessFile(outname, "rw");
			
			// Create tile-parts
			createTileParts();
			
			// Write new codestream
			writeNewCodestream(fi);
			
			// Close file
			fi.flush();
			addedHeaderBytes += fi.length();
			fi.close();
			
			return addedHeaderBytes;
		}
		
		/// <summary> This method parses the codestream for SOT, SOP and EPH markers and
		/// removes header header bits signalling SOP and EPH markers if packed
		/// packet headers are used
		/// 
		/// </summary>
		/// <param name="fi">The file to parse the markers from
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		private void  parseAndFind(BufferedRandomAccessFile fi)
		{
			int length, pos, i, t, sop = 0, eph = 0;
			short marker;
			int halfMarker;
			int tileEnd;
			System.Collections.ArrayList markPos = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			
			// Find position of first SOT marker
			marker = (short) fi.readUnsignedShort(); // read SOC marker
			marker = (short) fi.readUnsignedShort();
			while (marker != CSJ2K.j2k.codestream.Markers.SOT)
			{
				pos = fi.Pos;
				length = fi.readUnsignedShort();
				
				// If SOP and EPH markers were only used for parsing in this
				// class remove SOP and EPH markers from Scod field
				if (marker == CSJ2K.j2k.codestream.Markers.COD)
				{
					int scod = fi.readUnsignedByte();
					if (tempSop)
						scod &= 0xfd; // Remove bits indicating SOP 
					if (tempEph)
						scod &= 0xfb; // Remove bits indicating SOP 
					fi.seek(pos + 2);
					fi.write(scod);
				}
				
				fi.seek(pos + length);
				marker = (short) fi.readUnsignedShort();
			}
			pos = fi.Pos;
			fi.seek(pos - 2);
			
			// Find all packet headers, packed data and tile headers
			for (t = 0; t < nt; t++)
			{
				// Read SOT marker
				fi.readUnsignedShort(); // Skip SOT
				pos = fi.Pos;
				markPos.Add((System.Int32) fi.Pos);
				fi.readInt(); // Skip Lsot and Isot
				length = fi.readInt(); // Read Psot
				fi.readUnsignedShort(); // Skip TPsot & TNsot
				tileEnd = pos + length - 2; // Last byte of tile
				
				// Find position of SOD marker
				marker = (short) fi.readUnsignedShort();
				while (marker != CSJ2K.j2k.codestream.Markers.SOD)
				{
					pos = fi.Pos;
					length = fi.readUnsignedShort();
					
					// If SOP and EPH markers were only used for parsing in this
					// class remove SOP and EPH markers from Scod field
					if (marker == CSJ2K.j2k.codestream.Markers.COD)
					{
						int scod = fi.readUnsignedByte();
						if (tempSop)
							scod &= 0xfd; // Remove bits indicating SOP 
						if (tempEph)
							scod &= 0xfb; // Remove bits indicating SOP 
						fi.seek(pos + 2);
						fi.write(scod);
					}
					fi.seek(pos + length);
					marker = (short) fi.readUnsignedShort();
				}
				
				// Find all SOP and EPH markers in tile
				sop = 0;
				eph = 0;
				
				i = fi.Pos;
				while (i < tileEnd)
				{
					halfMarker = (short) fi.readUnsignedByte();
					if (halfMarker == (short) 0xff)
					{
						marker = (short) ((halfMarker << 8) + fi.readUnsignedByte());
						i++;
						if (marker == CSJ2K.j2k.codestream.Markers.SOP)
						{
							markPos.Add((System.Int32) fi.Pos);
							ppt[t]++;
							sop++;
							fi.skipBytes(4);
							i += 4;
						}
						
						if (marker == CSJ2K.j2k.codestream.Markers.EPH)
						{
							markPos.Add((System.Int32) fi.Pos);
							eph++;
						}
					}
					i++;
				}
			}
			markPos.Add((System.Int32) (fi.Pos + 2));
			positions = new System.Int32[markPos.Count];
			markPos.CopyTo(positions);
		}
		
		/// <summary> This method reads and buffers the tile headers, packet headers and
		/// packet data.
		/// 
		/// </summary>
		/// <param name="fi">The file to read the headers and data from
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		private void  readAndBuffer(BufferedRandomAccessFile fi)
		{
			int p, prem, length, t, markIndex;
			
			// Buffer main header
			fi.seek(0);
			length = ((System.Int32) positions[0]) - 2;
			mainHeader = new byte[length];
			fi.readFully(mainHeader, 0, length);
			markIndex = 0;
			
			for (t = 0; t < nt; t++)
			{
				prem = ppt[t];
				
				packetHeaders[t] = new byte[prem][];
				packetData[t] = new byte[prem][];
				sopMarkSeg[t] = new byte[prem][];
				
				// Read tile header
				length = positions[markIndex + 1] - positions[markIndex];
				tileHeaders[t] = new byte[length];
				fi.readFully(tileHeaders[t], 0, length);
				markIndex++;
				
				for (p = 0; p < prem; p++)
				{
					// Read packet header 
					length = positions[markIndex + 1] - positions[markIndex];
					
					if (tempSop)
					{
						// SOP marker is skipped
						length -= CSJ2K.j2k.codestream.Markers.SOP_LENGTH;
						fi.skipBytes(CSJ2K.j2k.codestream.Markers.SOP_LENGTH);
					}
					else
					{
						// SOP marker is read and buffered
						length -= CSJ2K.j2k.codestream.Markers.SOP_LENGTH;
						sopMarkSeg[t][p] = new byte[CSJ2K.j2k.codestream.Markers.SOP_LENGTH];
						fi.readFully(sopMarkSeg[t][p], 0, CSJ2K.j2k.codestream.Markers.SOP_LENGTH);
					}
					
					if (!tempEph)
					{
						// EPH marker is kept in header
						length += CSJ2K.j2k.codestream.Markers.EPH_LENGTH;
					}
					packetHeaders[t][p] = new byte[length];
					fi.readFully(packetHeaders[t][p], 0, length);
					markIndex++;
					
					// Read packet data 
					length = positions[markIndex + 1] - positions[markIndex];
					
					length -= CSJ2K.j2k.codestream.Markers.EPH_LENGTH;
					if (tempEph)
					{
						// EPH marker is used and is skipped
						fi.skipBytes(CSJ2K.j2k.codestream.Markers.EPH_LENGTH);
					}
					
					packetData[t][p] = new byte[length];
					fi.readFully(packetData[t][p], 0, length);
					markIndex++;
				}
			}
		}
		
		/// <summary> This method creates the tileparts from the buffered tile headers,
		/// packet headers and packet data
		/// 
		/// </summary>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		private void  createTileParts()
		{
			int i, prem, t, length;
            int pIndex; // phIndex removed
			int tppStart;
			int tilePart;
			int p, np, nomnp;
			int numTileParts;
			int numPackets;
			System.IO.MemoryStream temp = new System.IO.MemoryStream();
			byte[] tempByteArr;
			
			// Create tile parts
			tileParts = new byte[nt][][];
			maxtp = 0;
			
			for (t = 0; t < nt; t++)
			{
				// Calculate number of tile parts. If tileparts are not used, 
				// put all packets in the first tilepart
				if (pptp == 0)
					pptp = ppt[t];
				prem = ppt[t];
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				numTileParts = (int) System.Math.Ceiling(((double) prem) / pptp);
				numPackets = packetHeaders[t].Length;
				maxtp = (numTileParts > maxtp)?numTileParts:maxtp;
				tileParts[t] = new byte[numTileParts][];
				
				// Create all the tile parts for tile t
				tppStart = 0;
				pIndex = 0;
				p = 0;
				//phIndex = 0;
				
				for (tilePart = 0; tilePart < numTileParts; tilePart++)
				{
					
					// Calculate number of packets in this tilepart
					nomnp = (pptp > prem)?prem:pptp;
					np = nomnp;
					
					// Write tile part header
					if (tilePart == 0)
					{
						// Write original tile part header up to SOD marker
						temp.Write(tileHeaders[t], 0, tileHeaders[t].Length - 2);
					}
					else
					{
						// Write empty header of length TP_HEAD_LEN-2
						temp.Write(new byte[TP_HEAD_LEN - 2], 0, TP_HEAD_LEN - 2);
					}
					
					// Write PPT marker segments if PPT used
					if (pptUsed)
					{
						int pptLength = 3; // Zppt and Lppt
						int pptIndex = 0;
						int phLength;
						
						p = pIndex;
						while (np > 0)
						{
							phLength = packetHeaders[t][p].Length;
							
							// If the total legth of the packet headers is greater
							// than MAX_LPPT, several PPT markers are needed
							if (pptLength + phLength > CSJ2K.j2k.codestream.Markers.MAX_LPPT)
							{
								
								temp.WriteByte((System.Byte) SupportClass.URShift(CSJ2K.j2k.codestream.Markers.PPT, 8));
								temp.WriteByte((System.Byte) (CSJ2K.j2k.codestream.Markers.PPT & 0x00FF));
								temp.WriteByte((System.Byte) SupportClass.URShift(pptLength, 8));
								temp.WriteByte((System.Byte) pptLength);
								temp.WriteByte((System.Byte) pptIndex++);
								for (i = pIndex; i < p; i++)
								{
									temp.Write(packetHeaders[t][i], 0, packetHeaders[t][i].Length);
								}
								pptLength = 3; // Zppt and Lppt
								pIndex = p;
							}
							pptLength += phLength;
							p++;
							np--;
						}
						// Write last PPT marker
						temp.WriteByte((System.Byte) SupportClass.URShift(CSJ2K.j2k.codestream.Markers.PPT, 8));
						temp.WriteByte((System.Byte) (CSJ2K.j2k.codestream.Markers.PPT & 0x00FF));
						temp.WriteByte((System.Byte) SupportClass.URShift(pptLength, 8));
						temp.WriteByte((System.Byte) pptLength);
						temp.WriteByte((System.Byte) pptIndex);
						for (i = pIndex; i < p; i++)
						{
							
							temp.Write(packetHeaders[t][i], 0, packetHeaders[t][i].Length);
						}
					}
					pIndex = p;
					np = nomnp;
					
					// Write SOD marker
					temp.WriteByte((System.Byte) SupportClass.URShift(CSJ2K.j2k.codestream.Markers.SOD, 8));
					temp.WriteByte((System.Byte) (CSJ2K.j2k.codestream.Markers.SOD & 0x00FF));
					
					// Write packet data and packet headers if PPT and PPM not used
					for (p = tppStart; p < tppStart + np; p++)
					{
						if (!tempSop)
						{
							temp.Write(sopMarkSeg[t][p], 0, CSJ2K.j2k.codestream.Markers.SOP_LENGTH);
						}
						
						if (!(ppmUsed || pptUsed))
						{
							temp.Write(packetHeaders[t][p], 0, packetHeaders[t][p].Length);
						}
						
						temp.Write(packetData[t][p], 0, packetData[t][p].Length);
					}
					tppStart += np;
					
					// Edit tile part header
					tempByteArr = temp.ToArray();
					tileParts[t][tilePart] = tempByteArr;
					length = (int)temp.Length;
					
					if (tilePart == 0)
					{
						// Edit first tile part header
						tempByteArr[6] = (byte) (SupportClass.URShift(length, 24)); // Psot
						tempByteArr[7] = (byte) (SupportClass.URShift(length, 16));
						tempByteArr[8] = (byte) (SupportClass.URShift(length, 8));
						tempByteArr[9] = (byte) (length);
						tempByteArr[10] = (byte) SupportClass.Identity((0)); // TPsot
						tempByteArr[11] = (byte) (numTileParts); // TNsot
					}
					else
					{
						// Edit tile part header
						tempByteArr[0] = (byte) (SupportClass.URShift(CSJ2K.j2k.codestream.Markers.SOT, 8)); // SOT
						tempByteArr[1] = (byte) (CSJ2K.j2k.codestream.Markers.SOT & 0x00FF);
						tempByteArr[2] = (byte) SupportClass.Identity((0)); // Lsot
						tempByteArr[3] = (byte) SupportClass.Identity((10));
						tempByteArr[4] = (byte) (SupportClass.URShift(t, 8)); // Isot
						tempByteArr[5] = (byte) (t); // 
						tempByteArr[6] = (byte) (SupportClass.URShift(length, 24)); // Psot
						tempByteArr[7] = (byte) (SupportClass.URShift(length, 16));
						tempByteArr[8] = (byte) (SupportClass.URShift(length, 8));
						tempByteArr[9] = (byte) (length);
						tempByteArr[10] = (byte) (tilePart); //TPsot
						tempByteArr[11] = (byte) (numTileParts); // TNsot
					}
					//UPGRADE_ISSUE: Method 'java.io.ByteArrayOutputStream.reset' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioByteArrayOutputStreamreset'"
					//temp.reset();
                    temp.SetLength(0);
					prem -= np;
				}
			}
			temp.Close();
		}
		
		/// <summary> This method writes the new codestream to the file. 
		/// 
		/// </summary>
		/// <param name="fi">The file to write the new codestream to
		/// 
		/// </param>
		/// <exception cref="java.io.IOException">If an I/O error ocurred.
		/// 
		/// </exception>
		private void  writeNewCodestream(BufferedRandomAccessFile fi)
		{
			int t, p, tp; // i removed
			int numTiles = tileParts.Length;
			int[][] packetHeaderLengths = new int[numTiles][];
			for (int i2 = 0; i2 < numTiles; i2++)
			{
				packetHeaderLengths[i2] = new int[maxtp];
			}
			byte[] temp;
			int length;
			
			// Write main header up to SOT marker
			fi.write(mainHeader, 0, mainHeader.Length);
			
			// If PPM used write all packet headers in PPM markers
			if (ppmUsed)
			{
				System.IO.MemoryStream ppmMarkerSegment = new System.IO.MemoryStream();
				int numPackets;
				int totNumPackets;
				int ppmIndex = 0;
				int ppmLength;
				int pStart, pStop;
				int[] prem = new int[numTiles];
				
				// Set number of remaining packets 
				for (t = 0; t < numTiles; t++)
				{
					prem[t] = packetHeaders[t].Length;
				}
				
				// Calculate Nppm values 
				for (tp = 0; tp < maxtp; tp++)
				{
					for (t = 0; t < numTiles; t++)
					{
						if (tileParts[t].Length > tp)
						{
							totNumPackets = packetHeaders[t].Length;
							// Calculate number of packets in this tilepart
							numPackets = (tp == tileParts[t].Length - 1)?prem[t]:pptp;
							
							pStart = totNumPackets - prem[t];
							pStop = pStart + numPackets;
							
							// Calculate number of packet header bytes for this
							// tile part
							for (p = pStart; p < pStop; p++)
								packetHeaderLengths[t][tp] += packetHeaders[t][p].Length;
							
							prem[t] -= numPackets;
						}
					}
				}
				
				// Write first PPM marker
				ppmMarkerSegment.WriteByte((System.Byte) SupportClass.URShift(CSJ2K.j2k.codestream.Markers.PPM, 8));
				ppmMarkerSegment.WriteByte((System.Byte) (CSJ2K.j2k.codestream.Markers.PPM & 0x00FF));
				ppmMarkerSegment.WriteByte((System.Byte) 0); // Temporary Lppm value
				ppmMarkerSegment.WriteByte((System.Byte) 0); // Temporary Lppm value
				ppmMarkerSegment.WriteByte((System.Byte) 0); // zppm
				ppmLength = 3;
				ppmIndex++;
				
				// Set number of remaining packets 
				for (t = 0; t < numTiles; t++)
					prem[t] = packetHeaders[t].Length;
				
				// Write all PPM markers and information
				for (tp = 0; tp < maxtp; tp++)
				{
					for (t = 0; t < numTiles; t++)
					{
						
						if (tileParts[t].Length > tp)
						{
							totNumPackets = packetHeaders[t].Length;
							
							// Calculate number of packets in this tilepart
							numPackets = (tp == tileParts[t].Length - 1)?prem[t]:pptp;
							
							pStart = totNumPackets - prem[t];
							pStop = pStart + numPackets;
							
							// If Nppm value wont fit in current PPM marker segment
							// write current PPM marker segment and start new
							if (ppmLength + 4 > CSJ2K.j2k.codestream.Markers.MAX_LPPM)
							{
								// Write current PPM marker
								temp = ppmMarkerSegment.ToArray();
								length = temp.Length - 2;
								temp[2] = (byte) (SupportClass.URShift(length, 8));
								temp[3] = (byte) length;
								fi.write(temp, 0, length + 2);
								
								// Start new PPM marker segment
								//UPGRADE_ISSUE: Method 'java.io.ByteArrayOutputStream.reset' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioByteArrayOutputStreamreset'"
								//ppmMarkerSegment.reset();
                                ppmMarkerSegment.SetLength(0);
								ppmMarkerSegment.WriteByte((System.Byte) SupportClass.URShift(CSJ2K.j2k.codestream.Markers.PPM, 8));
								ppmMarkerSegment.WriteByte((System.Byte) (CSJ2K.j2k.codestream.Markers.PPM & 0x00FF));
								ppmMarkerSegment.WriteByte((System.Byte) 0); // Temporary Lppm value
								ppmMarkerSegment.WriteByte((System.Byte) 0); // Temporary Lppm value
								ppmMarkerSegment.WriteByte((System.Byte) ppmIndex++); // zppm
								ppmLength = 3;
							}
							
							// Write Nppm value
							length = packetHeaderLengths[t][tp];
							ppmMarkerSegment.WriteByte((System.Byte) SupportClass.URShift(length, 24));
							ppmMarkerSegment.WriteByte((System.Byte) SupportClass.URShift(length, 16));
							ppmMarkerSegment.WriteByte((System.Byte) SupportClass.URShift(length, 8));
							ppmMarkerSegment.WriteByte((System.Byte) length);
							ppmLength += 4;
							
							// Write packet headers
							for (p = pStart; p < pStop; p++)
							{
								length = packetHeaders[t][p].Length;
								
								// If next packet header value wont fit in 
								// current PPM marker segment write current PPM 
								// marker segment and start new
								if (ppmLength + length > CSJ2K.j2k.codestream.Markers.MAX_LPPM)
								{
									// Write current PPM marker
									temp = ppmMarkerSegment.ToArray();
									length = temp.Length - 2;
									temp[2] = (byte) (SupportClass.URShift(length, 8));
									temp[3] = (byte) length;
									fi.write(temp, 0, length + 2);
									
									// Start new PPM marker segment
									//UPGRADE_ISSUE: Method 'java.io.ByteArrayOutputStream.reset' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioByteArrayOutputStreamreset'"
									//ppmMarkerSegment.reset();
                                    ppmMarkerSegment.SetLength(0);
									ppmMarkerSegment.WriteByte((System.Byte) SupportClass.URShift(CSJ2K.j2k.codestream.Markers.PPM, 8));
									ppmMarkerSegment.WriteByte((System.Byte) (CSJ2K.j2k.codestream.Markers.PPM & 0x00FF));
									ppmMarkerSegment.WriteByte((System.Byte) 0); // Temp Lppm value
									ppmMarkerSegment.WriteByte((System.Byte) 0); // Temp Lppm value
									ppmMarkerSegment.WriteByte((System.Byte) ppmIndex++); // zppm
									ppmLength = 3;
								}
								
								// write packet header 
								ppmMarkerSegment.Write(packetHeaders[t][p], 0, packetHeaders[t][p].Length);
								ppmLength += packetHeaders[t][p].Length;
							}
							prem[t] -= numPackets;
						}
					}
				}
				// Write last PPM marker segment
				temp = ppmMarkerSegment.ToArray();
				length = temp.Length - 2;
				temp[2] = (byte) (SupportClass.URShift(length, 8));
				temp[3] = (byte) length;
				fi.write(temp, 0, length + 2);
			}
			
			// Write tile parts interleaved
			for (tp = 0; tp < maxtp; tp++)
			{
				for (t = 0; t < nt; t++)
				{
					if (tileParts[t].Length > tp)
					{
						temp = tileParts[t][tp];
						length = temp.Length;
						fi.write(temp, 0, length);
					}
				}
			}
			fi.writeShort(CSJ2K.j2k.codestream.Markers.EOC);
		}
	}
}