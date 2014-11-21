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
using CSJ2K.j2k.io;
namespace CSJ2K.j2k.codestream.reader
{
	
	/// <summary> This class provides a bit based reading facility from a byte based one,
	/// applying the bit unstuffing procedure as required by the packet headers.
	/// 
	/// </summary>
	//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
	public class PktHeaderBitReader
	{
		
		/// <summary>The byte based source of data </summary>
		internal RandomAccessIO in_Renamed;
		
		/// <summary>The byte array that is the source of data if the PktHeaderBitReader
		/// is instantiated with a buffer instead of a RandomAccessIO
		/// </summary>
		internal System.IO.MemoryStream bais;
		
		/// <summary>Flag indicating whether the data should be read from the buffer </summary>
		internal bool usebais;
		
		/// <summary>The current bit buffer </summary>
		internal int bbuf;
		
		/// <summary>The position of the next bit to read in the bit buffer (0 means 
		/// empty, 8 full) 
		/// </summary>
		internal int bpos;
		
		/// <summary>The next bit buffer, if bit stuffing occurred (i.e. current bit 
		/// buffer holds 0xFF) 
		/// </summary>
		internal int nextbbuf;
		
		/// <summary> Instantiates a 'PktHeaderBitReader' that gets the byte data from the
		/// given source.
		/// 
		/// </summary>
		/// <param name="in">The source of byte data
		/// 
		/// </param>
		internal PktHeaderBitReader(RandomAccessIO in_Renamed)
		{
			this.in_Renamed = in_Renamed;
			usebais = false;
		}
		
		/// <summary> Instantiates a 'PktHeaderBitReader' that gets the byte data from the
		/// given source.
		/// 
		/// </summary>
		/// <param name="bais">The source of byte data
		/// 
		/// </param>
		internal PktHeaderBitReader(System.IO.MemoryStream bais)
		{
			this.bais = bais;
			usebais = true;
		}
		
		/// <summary> Reads a single bit from the input.
		/// 
		/// </summary>
		/// <returns> The read bit (0 or 1)
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error occurred
		/// </exception>
		/// <exception cref="EOFException">If teh end of file has been reached
		/// 
		/// </exception>
		internal int readBit()
		{
			if (bpos == 0)
			{
				// Is bit buffer empty?
				if (bbuf != 0xFF)
				{
					// No bit stuffing
					if (usebais)
					{
						bbuf = bais.ReadByte();
					}
					else
					{
						bbuf = in_Renamed.read();
					}
					bpos = 8;
					if (bbuf == 0xFF)
					{
						// If new bit stuffing get next byte
						if (usebais)
						{
							nextbbuf = bais.ReadByte();
						}
						else
						{
							nextbbuf = in_Renamed.read();
						}
					}
				}
				else
				{
					// We had bit stuffing, nextbuf can not be 0xFF
					bbuf = nextbbuf;
					bpos = 7;
				}
			}
			return (bbuf >> --bpos) & 0x01;
		}
		
		/// <summary> Reads a specified number of bits and returns them in a single
		/// integer. The bits are returned in the 'n' least significant bits of the
		/// returned integer. The maximum number of bits that can be read is 31.
		/// 
		/// </summary>
		/// <param name="n">The number of bits to read
		/// 
		/// </param>
		/// <returns> The read bits, packed in the 'n' LSBs.
		/// 
		/// </returns>
		/// <exception cref="IOException">If an I/O error occurred
		/// </exception>
		/// <exception cref="EOFException">If teh end of file has been reached
		/// 
		/// </exception>
		internal int readBits(int n)
		{
			int bits; // The read bits
			
			// Can we get all bits from the bit buffer?
			if (n <= bpos)
			{
				return (bbuf >> (bpos -= n)) & ((1 << n) - 1);
			}
			else
			{
				// NOTE: The implementation need not be recursive but the not
				// recursive one exploits a bug in the IBM x86 JIT and caused
				// incorrect decoding (Diego Santa Cruz).
				bits = 0;
				do 
				{
					// Get all the bits we can from the bit buffer
					bits <<= bpos;
					n -= bpos;
					bits |= readBits(bpos);
					// Get an extra bit to load next byte (here bpos is 0)
					if (bbuf != 0xFF)
					{
						// No bit stuffing
						if (usebais)
						{
							bbuf = bais.ReadByte();
						}
						else
						{
							bbuf = in_Renamed.read();
						}
						
						bpos = 8;
						if (bbuf == 0xFF)
						{
							// If new bit stuffing get next byte
							if (usebais)
							{
								nextbbuf = bais.ReadByte();
							}
							else
							{
								nextbbuf = in_Renamed.read();
							}
						}
					}
					else
					{
						// We had bit stuffing, nextbuf can not be 0xFF
						bbuf = nextbbuf;
						bpos = 7;
					}
				}
				while (n > bpos);
				// Get the last bits, if any
				bits <<= n;
				bits |= (bbuf >> (bpos -= n)) & ((1 << n) - 1);
				// Return result
				return bits;
			}
		}
		
		/// <summary> Synchronizes this object with the underlying byte based input. It
		/// discards and buffered bits and gets ready to read bits from the current 
		/// position in the underlying byte based input.
		/// 
		/// <p>This method should always be called when some data has been read
		/// directly from the underlying byte based input since the last call to
		/// 'readBits()' or 'readBit()' before a new call to any of those
		/// methods.</p>
		/// 
		/// </summary>
		internal virtual void  sync()
		{
			bbuf = 0;
			bpos = 0;
		}
		
		/// <summary> Sets the underlying byte based input to the given object. This method
		/// discards any currently buffered bits and gets ready to start reading
		/// bits from 'in'.
		/// 
		/// <p>This method is equivalent to creating a new 'PktHeaderBitReader'
		/// object.</p>
		/// 
		/// </summary>
		/// <param name="in">The source of byte data
		/// 
		/// </param>
		internal virtual void  setInput(RandomAccessIO in_Renamed)
		{
			this.in_Renamed = in_Renamed;
			bbuf = 0;
			bpos = 0;
		}
		
		/// <summary> Sets the underlying byte based input to the given object. This method
		/// discards any currently buffered bits and gets ready to start reading
		/// bits from 'in'.
		/// 
		/// <p>This method is equivalent to creating a new 'PktHeaderBitReader'
		/// object.</p>
		/// 
		/// </summary>
		/// <param name="bais">The source of byte data
		/// 
		/// </param>
		internal virtual void  setInput(System.IO.MemoryStream bais)
		{
			this.bais = bais;
			bbuf = 0;
			bpos = 0;
		}
	}
}