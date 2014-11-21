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
namespace CSJ2K.j2k.codestream.writer
{
	
	/// <summary> This class implements a CodestreamWriter for Java streams. The streams can
	/// be files or network connections, or any other resource that presents itself
	/// as a OutputStream. See the CodestreamWriter abstract class for more details
	/// on the implementation of the CodestreamWriter abstract class.
	/// 
	/// <p>Before any packet data is written to the bit stream (even in simulation
	/// mode) the complete header should be written otherwise incorrect estimates
	/// are given by getMaxAvailableBytes() for rate allocation.
	/// 
	/// </summary>
	/// <seealso cref="CodestreamWriter">
	/// 
	/// </seealso>
	public class FileCodestreamWriter:CodestreamWriter
	{
		/// <summary> Returns the number of bytes remaining available in the bit stream. This
		/// is the maximum allowed number of bytes minus the number of bytes that
		/// have already been written to the bit stream. If more bytes have been
		/// written to the bit stream than the maximum number of allowed bytes,
		/// then a negative value is returned.
		/// 
		/// </summary>
		/// <returns> The number of bytes remaining available in the bit stream.
		/// 
		/// </returns>
		override public int MaxAvailableBytes
		{
			get
			{
				return maxBytes - ndata;
			}
			
		}
		/// <summary> Returns the current length of the entire bit stream.
		/// 
		/// </summary>
		/// <returns> the current length of the bit stream
		/// 
		/// </returns>
		override public int Length
		{
			get
			{
				if (MaxAvailableBytes >= 0)
				{
					return ndata;
				}
				else
				{
					return maxBytes;
				}
			}
			
		}
		/// <summary> Gives the offset of the end of last packet containing ROI information 
		/// 
		/// </summary>
		/// <returns> End of last ROI packet 
		/// 
		/// </returns>
		override public int OffLastROIPkt
		{
			get
			{
				return offLastROIPkt;
			}
			
		}
		
		/// <summary>The upper limit for the value of the Nsop field of the SOP marker </summary>
		private const int SOP_MARKER_LIMIT = 65535;
		
		/// <summary>Index of the current tile </summary>
		//private int tileIdx = 0;
		
		/// <summary>The file to write </summary>
		private System.IO.Stream out_Renamed;
		
		/// <summary>The number of bytes already written to the codestream, excluding the
		/// header length, magic number and header length info. 
		/// </summary>
		new internal int ndata = 0;
		
		/// <summary>The default buffer length, 1024 bytes </summary>
		public static int DEF_BUF_LEN = 1024;
		
		/// <summary>Array used to store the SOP markers values </summary>
		internal byte[] sopMarker;
		
		/// <summary>Array used to store the EPH markers values </summary>
		internal byte[] ephMarker;
		
		/// <summary>The packet index (when start of packet markers i.e. SOP markers) are
		/// used. 
		/// </summary>
		internal int packetIdx = 0;
		
		/// <summary>Offset of end of last packet containing ROI information </summary>
		private int offLastROIPkt = 0;
		
		/// <summary>Length of last packets containing no ROI information </summary>
		private int lenLastNoROI = 0;
		
		/// <summary> Opens the file 'file' for writing the codestream. The magic number is
		/// written to the bit stream. Normally, the header encoder must be empty
		/// (i.e. no data has been written to it yet). A BufferedOutputStream is
		/// used on top of the file to increase throughput, the length of the
		/// buffer is DEF_BUF_LEN.
		/// 
		/// </summary>
		/// <param name="file">The file where to write the bit stream
		/// 
		/// </param>
		/// <param name="mb">The maximum number of bytes that can be written to the bit
		/// stream.
		/// 
		/// </param>
		/// <exception cref="IOException">If an error occurs while trying to open the file
		/// for writing or while writing the magic number.
		/// 
		/// </exception>
		public FileCodestreamWriter(System.IO.FileInfo file, int mb):base(mb)
		{
			//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javaioFile'"
			out_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(file.FullName, System.IO.FileMode.Create), DEF_BUF_LEN);
			initSOP_EPHArrays();
		}
		
		/// <summary> Opens the file named 'fname' for writing the bit stream, using the 'he'
		/// header encoder. The magic number is written to the bit
		/// stream. Normally, the header encoder must be empty (i.e. no data has
		/// been written to it yet). A BufferedOutputStream is used on top of the
		/// file to increase throughput, the length of the buffer is DEF_BUF_LEN.
		/// 
		/// </summary>
		/// <param name="fname">The name of file where to write the bit stream
		/// 
		/// </param>
		/// <param name="mb">The maximum number of bytes that can be written to the bit
		/// stream.
		/// 
		/// </param>
		/// <param name="encSpec">The encoder's specifications
		/// 
		/// </param>
		/// <exception cref="IOException">If an error occurs while trying to open the file
		/// for writing or while writing the magic number.
		/// 
		/// </exception>
		public FileCodestreamWriter(System.String fname, int mb):base(mb)
		{
			//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
			out_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(fname, System.IO.FileMode.Create), DEF_BUF_LEN);
			initSOP_EPHArrays();
		}
		
		/// <summary> Uses the output stream 'os' for writing the bit stream, using the 'he'
		/// header encoder. The magic number is written to the bit
		/// stream. Normally, the header encoder must be empty (i.e. no data has
		/// been written to it yet). No BufferedOutputStream is used on top of the
		/// output stream 'os'.
		/// 
		/// </summary>
		/// <param name="os">The output stream where to write the bit stream.
		/// 
		/// </param>
		/// <param name="mb">The maximum number of bytes that can be written to the bit
		/// stream.
		/// 
		/// </param>
		/// <exception cref="IOException">If an error occurs while writing the magic
		/// number to the 'os' output stream.
		/// 
		/// </exception>
		public FileCodestreamWriter(System.IO.Stream os, int mb):base(mb)
		{
			out_Renamed = os;
			initSOP_EPHArrays();
		}
		
		/// <summary> Writes a packet head to the bit stream and returns the number of bytes
		/// used by this header. It returns the total number of bytes that the
		/// packet head takes in the bit stream. If in simulation mode then no data
		/// is written to the bit stream but the number of bytes is
		/// calculated. This can be used for iterative rate allocation.
		/// 
		/// <P>If the length of the data that is to be written to the bit stream is
		/// more than the space left (as returned by getMaxAvailableBytes()) only
		/// the data that does not exceed the allowed length is written, the rest
		/// is discarded. However the value returned by the method is the total
		/// length of the packet, as if all of it was written to the bit stream.
		/// 
		/// <P>If the bit stream header has not been commited yet and 'sim' is
		/// false, then the bit stream header is automatically commited (see
		/// commitBitstreamHeader() method) before writting the packet.
		/// 
		/// </summary>
		/// <param name="head">The packet head data.
		/// 
		/// </param>
		/// <param name="hlen">The number of bytes in the packet head.
		/// 
		/// </param>
		/// <param name="sim">Simulation mode flag. If true nothing is written to the bit
		/// stream, but the number of bytes that would be written is returned.
		/// 
		/// </param>
		/// <param name="sop">Start of packet header marker flag. This flag indicates
		/// whether or not SOP markers should be written. If true, SOP markers
		/// should be written, if false, they should not.
		/// 
		/// </param>
		/// <param name="eph">End of Packet Header marker flag. This flag indicates
		/// whether or not EPH markers should be written. If true, EPH markers
		/// should be written, if false, they should not.
		/// 
		/// </param>
		/// <returns> The number of bytes spent by the packet head.
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error occurs while writing to the
		/// output stream.
		/// 
		/// </exception>
		/// <seealso cref="commitBitstreamHeader">
		/// 
		/// </seealso>
		public override int writePacketHead(byte[] head, int hlen, bool sim, bool sop, bool eph)
		{
            // CONVERSION PROBLEM?
			int len = hlen + (sop ? (int)CSJ2K.j2k.codestream.Markers.SOP_LENGTH : 0) + (eph ? (int)CSJ2K.j2k.codestream.Markers.EPH_LENGTH : 0);
			
			// If not in simulation mode write the data 
			if (!sim)
			{
				// Write the head bytes 
				if (MaxAvailableBytes < len)
				{
					len = MaxAvailableBytes;
				}
				
				if (len > 0)
				{
					// Write Start Of Packet header markers if necessary 
					if (sop)
					{
						// The first 4 bytes of the array have been filled in the 
						// classe's constructor. 
						sopMarker[4] = (byte) (packetIdx >> 8);
						sopMarker[5] = (byte) (packetIdx);
						out_Renamed.Write(sopMarker, 0, CSJ2K.j2k.codestream.Markers.SOP_LENGTH);
						packetIdx++;
						if (packetIdx > SOP_MARKER_LIMIT)
						{
							// Reset SOP value as we have reached its upper limit 
							packetIdx = 0;
						}
					}
					out_Renamed.Write(head, 0, hlen);
					// Update data length 
					ndata += len;
					
					// Write End of Packet Header markers if necessary 
					if (eph)
					{
						out_Renamed.Write(ephMarker, 0, CSJ2K.j2k.codestream.Markers.EPH_LENGTH);
					}
					
					// Deal with ROI Information
					lenLastNoROI += len;
				}
			}
			return len;
		}
		
		/// <summary> Writes a packet body to the bit stream and returns the number of bytes
		/// used by this body .If in simulation mode then no data is written to the
		/// bit stream but the number of bytes is calculated. This can be used for
		/// iterative rate allocation.
		/// 
		/// <P>If the length of the data that is to be written to the bit stream is
		/// more than the space left (as returned by getMaxAvailableBytes()) only
		/// the data that does not exceed the allowed length is written, the rest
		/// is discarded. However the value returned by the method is the total
		/// length of the packet body , as if all of it was written to the bit
		/// stream.
		/// 
		/// </summary>
		/// <param name="body">The packet body data.
		/// 
		/// </param>
		/// <param name="blen">The number of bytes in the packet body.
		/// 
		/// </param>
		/// <param name="sim">Simulation mode flag. If true nothing is written to the bit
		/// stream, but the number of bytes that would be written is returned.
		/// 
		/// </param>
		/// <param name="roiInPkt">Whether or not this packet contains ROI information
		/// 
		/// </param>
		/// <param name="roiLen">Number of byte to read in packet body to get all the ROI
		/// information 
		/// 
		/// </param>
		/// <returns> The number of bytes spent by the packet body.
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error occurs while writing to the
		/// output stream.
		/// 
		/// </exception>
		/// <seealso cref="commitBitstreamHeader">
		/// 
		/// </seealso>
		public override int writePacketBody(byte[] body, int blen, bool sim, bool roiInPkt, int roiLen)
		{
			
			int len = blen;
			
			// If not in simulation mode write the data 
			if (!sim)
			{
				// Write the body bytes 
				len = blen;
				if (MaxAvailableBytes < len)
				{
					len = MaxAvailableBytes;
				}
				if (blen > 0)
				{
					out_Renamed.Write(body, 0, len);
				}
				// Update data length 
				ndata += len;
				
				// Deal with ROI information
				if (roiInPkt)
				{
					offLastROIPkt += lenLastNoROI + roiLen;
					lenLastNoROI = len - roiLen;
				}
				else
				{
					lenLastNoROI += len;
				}
			}
			return len;
		}
		
		/// <summary> Writes the EOC marker and closes the underlying stream.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an error occurs while closing the underlying
		/// stream.
		/// 
		/// </exception>
		public override void  close()
		{
			
			// Write the EOC marker and close the codestream.
            // CONVERSION PROBLEM?
			out_Renamed.WriteByte((byte) SupportClass.URShift(CSJ2K.j2k.codestream.Markers.EOC, 8));
			out_Renamed.WriteByte((byte) (CSJ2K.j2k.codestream.Markers.EOC & 0x00FF));
			
			ndata += 2; // Add two to length of codestream for EOC marker
			
			out_Renamed.Close();
		}
		
		/// <summary> Writes the header data in the codestream and actualize ndata with the
		/// header length. The header is either a MainHeaderEncoder or a
		/// TileHeaderEncoder.
		/// 
		/// </summary>
		/// <param name="he">The current header encoder.
		/// 
		/// </param>
		/// <exception cref="IOException">If an I/O error occurs while writing the data.
		/// 
		/// </exception>
		public override void  commitBitstreamHeader(HeaderEncoder he)
		{
			// Actualize ndata
			ndata += he.Length;
			he.writeTo(out_Renamed); // Write the header
			// Reset packet index used for SOP markers
			packetIdx = 0;
			
			// Deal with ROI information
			lenLastNoROI += he.Length;
		}
		
		/// <summary> Performs the initialisation of the arrays that are used to store the
		/// values used to write SOP and EPH markers
		/// 
		/// </summary>
		private void  initSOP_EPHArrays()
		{
			
			// Allocate and set first values of SOP marker as they will not be
			// modified
			sopMarker = new byte[CSJ2K.j2k.codestream.Markers.SOP_LENGTH];
			sopMarker[0] = unchecked((byte) (CSJ2K.j2k.codestream.Markers.SOP >> 8));
			sopMarker[1] = (byte) SupportClass.Identity(CSJ2K.j2k.codestream.Markers.SOP);
			sopMarker[2] = (byte) 0x00;
			sopMarker[3] = (byte) 0x04;
			
			// Allocate and set values of EPH marker as they will not be
			// modified
			ephMarker = new byte[CSJ2K.j2k.codestream.Markers.EPH_LENGTH];
			ephMarker[0] = unchecked((byte) (CSJ2K.j2k.codestream.Markers.EPH >> 8));
			ephMarker[1] = (byte) SupportClass.Identity(CSJ2K.j2k.codestream.Markers.EPH);
		}
	}
}