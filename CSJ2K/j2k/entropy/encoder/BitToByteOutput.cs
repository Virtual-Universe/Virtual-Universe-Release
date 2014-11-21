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
namespace CSJ2K.j2k.entropy.encoder
{
	
	/// <summary> This class provides an adapter to perform bit based output on byte based
	/// output objects that inherit from a 'ByteOutputBuffer' class. This class
	/// implements the bit stuffing policy needed for the 'selective arithmetic
	/// coding bypass' mode of the entropy coder. This class also delays the output
	/// of a trailing 0xFF, since they are synthetized be the decoder.
	/// 
	/// </summary>
	class BitToByteOutput
	{
		/// <summary> Set the flag according to whether or not the predictable termination is
		/// requested.
		/// 
		/// </summary>
		/// <param name="isPredTerm">Whether or not predictable termination is requested.
		/// 
		/// </param>
		virtual internal bool PredTerm
		{
			set
			{
				this.isPredTerm = value;
			}
			
		}
		
		/// <summary>Whether or not predictable termination is requested. This value is
		/// important when the last byte before termination is an 0xFF  
		/// </summary>
		private bool isPredTerm = false;
		
		/// <summary>The alternating sequence of 0's and 1's used for byte padding </summary>
		internal const int PAD_SEQ = 0x2A;
		
		/// <summary>Flag that indicates if an FF has been delayed </summary>
		internal bool delFF = false;
		
		/// <summary>The byte based output </summary>
		internal ByteOutputBuffer out_Renamed;
		
		/// <summary>The bit buffer </summary>
		internal int bbuf;
		
		/// <summary>The position of the next bit to put in the bit buffer. When it is 7
		/// the bit buffer 'bbuf' is empty. The value should always be between 7
		/// and 0 (i.e. if it gets to -1, the bit buffer should be immediately
		/// written to the byte output). 
		/// </summary>
		internal int bpos = 7;
		
		/// <summary>The number of written bytes (excluding the bit buffer) </summary>
		internal int nb = 0;
		
		/// <summary> Instantiates a new 'BitToByteOutput' object that uses 'out' as the
		/// underlying byte based output.
		/// 
		/// </summary>
		/// <param name="out">The underlying byte based output
		/// 
		/// </param>
		internal BitToByteOutput(ByteOutputBuffer out_Renamed)
		{
			this.out_Renamed = out_Renamed;
		}
		
		/// <summary> Writes to the bit stream the symbols contained in the 'symbuf'
		/// buffer. The least significant bit of each element in 'symbuf'is
		/// written.
		/// 
		/// </summary>
		/// <param name="symbuf">The symbols to write
		/// 
		/// </param>
		/// <param name="nsym">The number of symbols in symbuf
		/// 
		/// </param>
		internal void  writeBits(int[] symbuf, int nsym)
		{
			int i;
			int bbuf, bpos;
			bbuf = this.bbuf;
			bpos = this.bpos;
			// Write symbol by symbol to bit buffer
			for (i = 0; i < nsym; i++)
			{
				bbuf |= (symbuf[i] & 0x01) << (bpos--);
				if (bpos < 0)
				{
					// Bit buffer is full, write it
					if (bbuf != 0xFF)
					{
						// No bit-stuffing needed
						if (delFF)
						{
							// Output delayed 0xFF if any
							out_Renamed.write(0xFF);
							nb++;
							delFF = false;
						}
						out_Renamed.write(bbuf);
						nb++;
						bpos = 7;
					}
					else
					{
						// We need to do bit stuffing on next byte
						delFF = true;
						bpos = 6; // One less bit in next byte
					}
					bbuf = 0;
				}
			}
			this.bbuf = bbuf;
			this.bpos = bpos;
		}
		
		/// <summary> Write a bit to the output. The least significant bit of 'bit' is
		/// written to the output.
		/// 
		/// </summary>
		/// <param name="bit">
		/// </param>
		internal void  writeBit(int bit)
		{
			bbuf |= (bit & 0x01) << (bpos--);
			if (bpos < 0)
			{
				if (bbuf != 0xFF)
				{
					// No bit-stuffing needed
					if (delFF)
					{
						// Output delayed 0xFF if any
						out_Renamed.write(0xFF);
						nb++;
						delFF = false;
					}
					// Output the bit buffer
					out_Renamed.write(bbuf);
					nb++;
					bpos = 7;
				}
				else
				{
					// We need to do bit stuffing on next byte
					delFF = true;
					bpos = 6; // One less bit in next byte
				}
				bbuf = 0;
			}
		}
		
		/// <summary> Writes the contents of the bit buffer and byte aligns the output by
		/// filling bits with an alternating sequence of 0's and 1's.
		/// 
		/// </summary>
		internal virtual void  flush()
		{
			if (delFF)
			{
				// There was a bit stuffing
				if (bpos != 6)
				{
					// Bit buffer is not empty
					// Output delayed 0xFF
					out_Renamed.write(0xFF);
					nb++;
					delFF = false;
					// Pad to byte boundary with an alternating sequence of 0's
					// and 1's.
					bbuf |= (SupportClass.URShift(PAD_SEQ, (6 - bpos)));
					// Output the bit buffer
					out_Renamed.write(bbuf);
					nb++;
					bpos = 7;
					bbuf = 0;
				}
				else if (isPredTerm)
				{
					out_Renamed.write(0xFF);
					nb++;
					out_Renamed.write(0x2A);
					nb++;
					bpos = 7;
					bbuf = 0;
					delFF = false;
				}
			}
			else
			{
				// There was no bit stuffing
				if (bpos != 7)
				{
					// Bit buffer is not empty
					// Pad to byte boundary with an alternating sequence of 0's and
					// 1's.
					bbuf |= (SupportClass.URShift(PAD_SEQ, (6 - bpos)));
					// Output the bit buffer (bbuf can not be 0xFF)
					out_Renamed.write(bbuf);
					nb++;
					bpos = 7;
					bbuf = 0;
				}
			}
		}
		
		/// <summary> Terminates the bit stream by calling 'flush()' and then
		/// 'reset()'. Finally, it returns the number of bytes effectively written.
		/// 
		/// </summary>
		/// <returns> The number of bytes effectively written.
		/// 
		/// </returns>
		public virtual int terminate()
		{
			flush();
			int savedNb = nb;
			reset();
			return savedNb;
		}
		
		/// <summary> Resets the bit buffer to empty, without writing anything to the
		/// underlying byte output, and resets the byte count. The underlying byte
		/// output is NOT reset.
		/// 
		/// </summary>
		internal virtual void  reset()
		{
			delFF = false;
			bpos = 7;
			bbuf = 0;
			nb = 0;
		}
		
		/// <summary> Returns the length, in bytes, of the output bit stream as written by
		/// this object. If the output bit stream does not have an integer number
		/// of bytes in length then it is rounded to the next integer.
		/// 
		/// </summary>
		/// <returns> The length, in bytes, of the output bit stream.
		/// 
		/// </returns>
		internal virtual int length()
		{
			if (delFF)
			{
				// If bit buffer is empty we just need 'nb' bytes. If not we need
				// the delayed FF and the padded bit buffer.
				return nb + 2;
			}
			else
			{
				// If the bit buffer is empty, we just need 'nb' bytes. If not, we
				// add length of the padded bit buffer
				return nb + ((bpos == 7)?0:1);
			}
		}
	}
}