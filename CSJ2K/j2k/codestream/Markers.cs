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
namespace CSJ2K.j2k.codestream
{
	
	/// <summary> This interface defines the values of the different markers in the JPEG 2000
	/// codestream. There are 16 bit values, always appearing in big-endian (most
	/// significant byte first) and byte-aligned in the codestream. This interface
	/// also defines some other constants such as bit-masks and bit-shifts.
	/// 
	/// </summary>
	public struct Markers
    {
        /// <summary>Start of codestream (SOC): 0xFF4F </summary>
        public const short SOC = unchecked((short)0xff4f);
		/// <summary>Start of tile-part (SOT): 0xFF90 </summary>
		public const short SOT = unchecked((short)0xff90);
		/// <summary>Start of data (SOD): 0xFF93 </summary>
		public const short SOD = unchecked((short)0xff93);
		/// <summary>End of codestream (EOC): 0xFFD9 </summary>
		public const short EOC = unchecked((short)0xffd9);
		/// <summary>SIZ marker (Image and tile size): 0xFF51 </summary>
		public const short SIZ = unchecked((short)0xff51);
		/// <summary>No special capabilities (baseline) in codestream, in Rsiz field of SIZ
		/// marker: 0x00. All flag bits are turned off 
		/// </summary>
		public const int RSIZ_BASELINE = 0x00;
		/// <summary>Error resilience marker flag bit in Rsiz field in SIZ marker: 0x01 </summary>
		public const int RSIZ_ER_FLAG = 0x01;
		/// <summary>ROI present marker flag bit in Rsiz field in SIZ marker: 0x02 </summary>
		public const int RSIZ_ROI = 0x02;
		/// <summary>Component bitdepth bits in Ssiz field in SIZ marker: 7 </summary>
		public const int SSIZ_DEPTH_BITS = 7;
		/// <summary>The maximum number of component bitdepth </summary>
		public const int MAX_COMP_BITDEPTH = 38;
		/// <summary>Coding style default (COD): 0xFF52 </summary>
		public const short COD = unchecked((short)0xff52);
		/// <summary>Coding style component (COC): 0xFF53 </summary>
		public const short COC = unchecked((short)0xff53);
		/// <summary>Precinct used flag </summary>
		public const int SCOX_PRECINCT_PARTITION = 1;
		/// <summary>Use start of packet marker </summary>
		public const int SCOX_USE_SOP = 2;
		/// <summary>Use end of packet header marker </summary>
		public const int SCOX_USE_EPH = 4;
		/// <summary>Horizontal code-block partition origin is at x=1 </summary>
		public const int SCOX_HOR_CB_PART = 8;
		/// <summary>Vertical code-block partition origin is at y=1 </summary>
		public const int SCOX_VER_CB_PART = 16;
		/// <summary>The default size exponent of the precincts </summary>
		public const int PRECINCT_PARTITION_DEF_SIZE = 0xffff;
		// ** RGN marker segment **
		/// <summary>Region-of-interest (RGN): 0xFF5E </summary>
		public const short RGN = unchecked((short)0xff5e);
		/// <summary>Implicit (i.e. max-shift) ROI flag for Srgn field in RGN marker
		/// segment: 0x00 
		/// </summary>
		public const int SRGN_IMPLICIT = 0x00;
		/// <summary>Quantization default (QCD): 0xFF5C </summary>
        public const short QCD = unchecked((short)0xff5c);
		/// <summary>Quantization component (QCC): 0xFF5D </summary>
		public const short QCC = unchecked((short)0xff5d);
		/// <summary>Guard bits shift in SQCX field: 5 </summary>
		public const int SQCX_GB_SHIFT = 5;
		/// <summary>Guard bits mask in SQCX field: 7 </summary>
		public const int SQCX_GB_MSK = 7;
		/// <summary>No quantization (i.e. embedded reversible) flag for Sqcd or Sqcc
		/// (Sqcx) fields: 0x00. 
		/// </summary>
		public const int SQCX_NO_QUANTIZATION = 0x00;
		/// <summary>Scalar derived (i.e. LL values only) quantization flag for Sqcd or
		/// Sqcc (Sqcx) fields: 0x01. 
		/// </summary>
		public const int SQCX_SCALAR_DERIVED = 0x01;
		/// <summary>Scalar expounded (i.e. all values) quantization flag for Sqcd or Sqcc
		/// (Sqcx) fields: 0x02. 
		/// </summary>
		public const int SQCX_SCALAR_EXPOUNDED = 0x02;
		/// <summary>Exponent shift in SPQCX when no quantization: 3 </summary>
		public const int SQCX_EXP_SHIFT = 3;
		/// <summary>Exponent bitmask in SPQCX when no quantization: 3 </summary>
		public const int SQCX_EXP_MASK = (1 << 5) - 1;
		/// <summary>The "SOP marker segments used" flag within Sers: 1 </summary>
		public const int ERS_SOP = 1;
		/// <summary>The "segmentation symbols used" flag within Sers: 2 </summary>
		public const int ERS_SEG_SYMBOLS = 2;
		// ** Progression order change **
		public const short POC = unchecked((short)0xff5f);
		/// <summary>Tile-part lengths (TLM): 0xFF55 </summary>
		public const short TLM = unchecked((short)0xff55);
		/// <summary>Packet length, main header (PLM): 0xFF57 </summary>
		public const short PLM = unchecked((short)0xff57);
		/// <summary>Packet length, tile-part header (PLT): 0xFF58 </summary>
		public const short PLT = unchecked((short)0xff58);
		/// <summary>Packed packet headers, main header (PPM): 0xFF60 </summary>
		public const short PPM = unchecked((short)0xff60);
		/// <summary>Packed packet headers, tile-part header (PPT): 0xFF61 </summary>
		public const short PPT = unchecked((short)0xff61);
		/// <summary>Maximum length of PPT marker segment </summary>
		public const int MAX_LPPT = 65535;
		/// <summary>Maximum length of PPM marker segment </summary>
		public const int MAX_LPPM = 65535;
		/// <summary>Start pf packet (SOP): 0xFF91 </summary>
		public const short SOP = unchecked((short)0xff91);
		/// <summary>Length of SOP marker (in bytes) </summary>
		public const short SOP_LENGTH = 6;
		/// <summary>End of packet header (EPH): 0xFF92 </summary>
		public const short EPH = unchecked((short)0xff92);
		/// <summary>Length of EPH marker (in bytes) </summary>
		public const short EPH_LENGTH = 2;
		/// <summary>Component registration (CRG): 0xFF63 </summary>
		public const short CRG = unchecked((short)0xff63);
		/// <summary>Comment (COM): 0xFF64 </summary>
		public const short COM = unchecked((short)0xff64);
        /// <summary>General use registration value (binary) (COM): 0x0000 </summary>
        public const short RCOM_BINARY = 0;
		/// <summary>General use registration value (latin) (COM): 0x0001 </summary>
		public const short RCOM_LATIN = 1;
	}
}