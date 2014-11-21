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
using CSJ2K.j2k.codestream.writer;
using CSJ2K.j2k.codestream;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.encoder;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k;
namespace CSJ2K.j2k.wavelet.analysis
{
	
	/// <summary> This abstract class represents the forward wavelet transform functional
	/// block. The functional block may actually be comprised of several classes
	/// linked together, but a subclass of this abstract class is the one that is
	/// returned as the functional block that performs the forward wavelet
	/// transform.
	/// 
	/// <p>This class assumes that data is transferred in code-blocks, as defined
	/// by the 'CBlkWTDataSrc' interface. The internal calculation of the wavelet
	/// transform may be done differently but a buffering class should convert to
	/// that type of transfer.</p>
	/// 
	/// </summary>
	public abstract class ForwardWT:ImgDataAdapter, ForwWT, CBlkWTDataSrc
	{
		/// <summary> Returns the parameters that are used in this class and implementing
		/// classes. It returns a 2D String array. Each of the 1D arrays is for a
		/// different option, and they have 3 elements. The first element is the
		/// option name, the second one is the synopsis and the third one is a long
		/// description of what the parameter is. The synopsis or description may
		/// be 'null', in which case it is assumed that there is no synopsis or
		/// description of the option, respectively. Null may be returned if no
		/// options are supported.
		/// 
		/// </summary>
		/// <returns> the options name, their synopsis and their explanation, or null
		/// if no options are supported.
		/// 
		/// </returns>
		public static System.String[][] ParameterInfo
		{
			get
			{
				return pinfo;
			}
			
		}
		public abstract int CbULY{get;}
		public abstract int CbULX{get;}
		
		/// <summary> ID for the dyadic wavelet tree decomposition (also called "Mallat" in
		/// JPEG 2000): 0x00.  
		/// 
		/// </summary>
		public const int WT_DECOMP_DYADIC = 0;
		
		/// <summary>The prefix for wavelet transform options: 'W' </summary>
		public const char OPT_PREFIX = 'W';
		
		/// <summary>The list of parameters that is accepted for wavelet transform. Options
		/// for the wavelet transform start with 'W'. 
		/// </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'pinfo'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.String[][] pinfo = new System.String[][]{new System.String[]{"Wlev", "<number of decomposition levels>", "Specifies the number of wavelet decomposition levels to apply to " + "the image. If 0 no wavelet transform is performed. All components " + "and all tiles have the same number of decomposition levels.", "5"}, new System.String[]{"Wwt", "[full]", "Specifies the wavelet transform to be used. Possible value is: " + "'full' (full page). The value 'full' performs a normal DWT.", "full"}, new System.String[]{"Wcboff", "<x y>", "Code-blocks partition offset in the reference grid. Allowed for " + "<x> and <y> are 0 and 1.\n" + "Note: This option is defined in JPEG 2000 part 2 and may not" + " be supported by all JPEG 2000 decoders.", "0 0"}};
		
		/// <summary> Initializes this object for the specified number of tiles 'nt' and
		/// components 'nc'.
		/// 
		/// </summary>
		/// <param name="src">The source of ImgData
		/// 
		/// </param>
		protected internal ForwardWT(ImgData src):base(src)
		{
		}
		
		/// <summary> Creates a ForwardWT object with the specified filters, and with other
		/// options specified in the parameter list 'pl'.
		/// 
		/// </summary>
		/// <param name="src">The source of data to be transformed
		/// 
		/// </param>
		/// <param name="pl">The parameter list (or options).
		/// 
		/// </param>
		/// <param name="kers">The encoder specifications.
		/// 
		/// </param>
		/// <returns> A new ForwardWT object with the specified filters and options
		/// from 'pl'.
		/// 
		/// </returns>
		/// <exception cref="IllegalArgumentException">If mandatory parameters are missing 
		/// or if invalid values are given.
		/// 
		/// </exception>
		public static ForwardWT createInstance(BlkImgDataSrc src, ParameterList pl, EncoderSpecs encSpec)
		{
            int deflev; // defdec removed
			//System.String decompstr;
			//System.String wtstr;
			//System.String pstr;
			//SupportClass.StreamTokenizerSupport stok;
			//SupportClass.Tokenizer strtok;
			//int prefx, prefy; // Partitioning reference point coordinates
			
			// Check parameters
			pl.checkList(OPT_PREFIX, CSJ2K.j2k.util.ParameterList.toNameArray(pinfo));
			
			deflev = ((System.Int32) encSpec.dls.getDefault());
			
			// Code-block partition origin
			System.String str = "";
			if (pl.getParameter("Wcboff") == null)
			{
				throw new System.ApplicationException("You must specify an argument to the '-Wcboff' " + "option. See usage with the '-u' option");
			}
			SupportClass.Tokenizer stk = new SupportClass.Tokenizer(pl.getParameter("Wcboff"));
			if (stk.Count != 2)
			{
				throw new System.ArgumentException("'-Wcboff' option needs two" + " arguments. See usage with " + "the '-u' option.");
			}
			int cb0x = 0;
			str = stk.NextToken();
			try
			{
				cb0x = (System.Int32.Parse(str));
			}
			catch (System.FormatException)
			{
				throw new System.ArgumentException("Bad first parameter for the " + "'-Wcboff' option: " + str);
			}
			if (cb0x < 0 || cb0x > 1)
			{
				throw new System.ArgumentException("Invalid horizontal " + "code-block partition origin.");
			}
			int cb0y = 0;
			str = stk.NextToken();
			try
			{
				cb0y = (System.Int32.Parse(str));
			}
			catch (System.FormatException)
			{
				throw new System.ArgumentException("Bad second parameter for the " + "'-Wcboff' option: " + str);
			}
			if (cb0y < 0 || cb0y > 1)
			{
				throw new System.ArgumentException("Invalid vertical " + "code-block partition origin.");
			}
			if (cb0x != 0 || cb0y != 0)
			{
				FacilityManager.getMsgLogger().printmsg(CSJ2K.j2k.util.MsgLogger_Fields.WARNING, "Code-blocks partition origin is " + "different from (0,0). This is defined in JPEG 2000" + " part 2 and may be not supported by all JPEG 2000 " + "decoders.");
			}
			
			return new ForwWTFull(src, encSpec, cb0x, cb0y);
		}
		public abstract bool isReversible(int param1, int param2);
		public abstract CSJ2K.j2k.wavelet.analysis.CBlkWTData getNextInternCodeBlock(int param1, CSJ2K.j2k.wavelet.analysis.CBlkWTData param2);
		public abstract int getFixedPoint(int param1);
		public abstract CSJ2K.j2k.wavelet.analysis.AnWTFilter[] getHorAnWaveletFilters(int param1, int param2);
		public abstract CSJ2K.j2k.wavelet.analysis.AnWTFilter[] getVertAnWaveletFilters(int param1, int param2);
		public abstract CSJ2K.j2k.wavelet.analysis.SubbandAn getAnSubbandTree(int param1, int param2);
		public abstract int getDecompLevels(int param1, int param2);
		public abstract CSJ2K.j2k.wavelet.analysis.CBlkWTData getNextCodeBlock(int param1, CSJ2K.j2k.wavelet.analysis.CBlkWTData param2);
		public abstract int getImplementationType(int param1);
		public abstract int getDataType(int param1, int param2);
		public abstract int getDecomp(int param1, int param2);
	}
}