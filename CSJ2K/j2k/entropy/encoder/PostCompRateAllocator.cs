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
using CSJ2K.j2k.wavelet.analysis;
using CSJ2K.j2k.codestream;
using CSJ2K.j2k.entropy;
using CSJ2K.j2k.encoder;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k;
namespace CSJ2K.j2k.entropy.encoder
{
	
	/// <summary> This is the abstract class from which post-compression rate allocators
	/// which generate layers should inherit. The source of data is a
	/// 'CodedCBlkDataSrcEnc' which delivers entropy coded blocks with
	/// rate-distortion statistics.
	/// 
	/// <p>The post compression rate allocator implementation should create the
	/// layers, according to a rate allocation policy, and send the packets to a
	/// CodestreamWriter. Since the rate allocator sends the packets to the bit
	/// stream then it should output the packets to the bit stream in the order
	/// imposed by the bit stream profiles.</p>
	/// 
	/// </summary>
	/// <seealso cref="CodedCBlkDataSrcEnc">
	/// </seealso>
	/// <seealso cref="jj2000.j2k.codestream.writer.CodestreamWriter">
	/// 
	/// </seealso>
	public abstract class PostCompRateAllocator:ImgDataAdapter
	{
		/// <summary> Keep a reference to the header encoder.
		/// 
		/// </summary>
		/// <param name="headEnc">The header encoder
		/// 
		/// </param>
		virtual public HeaderEncoder HeaderEncoder
		{
			set
			{
				this.headEnc = value;
			}
			
		}
		/// <summary> Returns the number of layers that are actually generated.
		/// 
		/// </summary>
		/// <returns> The number of layers generated.
		/// 
		/// </returns>
		virtual public int NumLayers
		{
			get
			{
				return num_Layers;
			}
			
		}
		/// <summary> Returns the parameters that are used in this class and implementing
		/// classes. It returns a 2D String array. Each of the 1D arrays is for a
		/// different option, and they have 3 elements. The first element is the
		/// option name, the second one is the synopsis, the third one is a long
		/// description of what the parameter is and the fourth is its default
		/// value. The synopsis or description may be 'null', in which case it is
		/// assumed that there is no synopsis or description of the option,
		/// respectively. Null may be returned if no options are supported.
		/// 
		/// </summary>
		/// <returns> the options name, their synopsis and their explanation, 
		/// or null if no options are supported.
		/// 
		/// </returns>
		public static System.String[][] ParameterInfo
		{
			get
			{
				return pinfo;
			}
			
		}
		
		/// <summary>The prefix for rate allocation options: 'A' </summary>
		public const char OPT_PREFIX = 'A';
		
		/// <summary>The list of parameters that is accepted for entropy coding. Options 
		/// for entropy coding start with 'R'. 
		/// </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'pinfo'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.String[][] pinfo = new System.String[][]{new System.String[]{"Aptype", "[<tile idx>] res|layer|res-pos|" + "pos-comp|comp-pos [res_start comp_start layer_end res_end " + "comp_end " + "prog] [[res_start comp_start ly_end res_end comp_end prog] ...] [" + "[<tile-component idx>] ...]", "Specifies which type of progression should be used when " + "generating " + "the codestream. The 'res' value generates a resolution " + "progressive codestream with the number of layers specified by " + "'Alayers' option. The 'layer' value generates a layer progressive " + "codestream with multiple layers. In any case the rate-allocation " + "algorithm optimizes for best quality in each layer. The quality " + "measure is mean squared error (MSE) or a weighted version of it " + "(WMSE). If no progression type is specified or imposed by other " + "modules, the default value is 'layer'.\n" + "It is also possible to describe progression order changes. In " + "this case, 'res_start' is the index (from 0) of the first " + "resolution " + "level, 'comp_start' is the index (from 0) of the first component, " + "'ly_end' is the index (from 0) of the first layer not included, " + "'res_end' is the index (from 0) of the first resolution level not " + "included, 'comp_end' is index (from 0) of the first component not " + "included and 'prog' is the progression type to be used " + "for the rest of the tile/image. Several progression order changes " + "can be specified, one after the other.", null}, new System.String[]{"Alayers", "[<rate> [+<layers>] [<rate [+<layers>] [...]] | sl]", "Explicitly specifies the codestream layer formation parameters. " + "The <rate> parameter specifies the bitrate to which the first " + "layer should be optimized. The <layers> parameter, if present, " + "specifies the number of extra layers that should be added for " + "scalability. These extra layers are not optimized. " + "Any extra <rate> and <layers> parameters add more layers, in the " + 
			"same way. An additional layer is always added at the end, which" + " is " + "optimized to the overall target bitrate of the bit stream. Any " + "layers (optimized or not) whose target bitrate is higher that the " + "overall target bitrate are silently ignored. The bitrates of the " + "extra layers that are added through the <layers> parameter are " + "approximately log-spaced between the other target bitrates. If " + "several <rate> [+<layers>] constructs appear the <rate>" + " parameters " + "must appear in increasing order. The rate allocation algorithm " + "ensures that all coded layers have a minimal reasonable size, if " + "not these layers are silently ignored.\n" + "If the 'sl' (i.e. 'single layer') argument is specified, the " + "generated codestream will" + " only contain one layer (with a bit rate specified thanks to the" + " '-rate' or 'nbytes' options).", "0.015 +20 2.0 +10"}};
		
		/// <summary>The source of entropy coded data </summary>
		protected internal CodedCBlkDataSrcEnc src;
		
		/// <summary>The source of entropy coded data </summary>
		protected internal EncoderSpecs encSpec;
		
		/// <summary>The number of layers. </summary>
		protected internal int num_Layers;
		
		/// <summary>The bit-stream writer </summary>
		internal CodestreamWriter bsWriter;
		
		/// <summary>The header encoder </summary>
		internal HeaderEncoder headEnc;
		
		/// <summary> Initializes the source of entropy coded data.
		/// 
		/// </summary>
		/// <param name="src">The source of entropy coded data.
		/// 
		/// </param>
		/// <param name="ln">The number of layers to create
		/// 
		/// </param>
		/// <param name="pt">The progressive type, as defined in 'ProgressionType'.
		/// 
		/// </param>
		/// <param name="bw">The packet bit stream writer.
		/// 
		/// </param>
		/// <seealso cref="ProgressionType">
		/// 
		/// </seealso>
		public PostCompRateAllocator(CodedCBlkDataSrcEnc src, int nl, CodestreamWriter bw, EncoderSpecs encSpec):base(src)
		{
			this.src = src;
			this.encSpec = encSpec;
			num_Layers = nl;
			bsWriter = bw;
		}
		
		/// <summary> Initializes the rate allocation points, taking into account header
		/// overhead and such. This method must be called after the header has been
		/// simulated but before calling the runAndWrite() one. The header must be
		/// rewritten after a call to this method since the number of layers may
		/// change.
		/// 
		/// </summary>
		/// <param name="oldSyntax">Whether or not the old syntax is used.
		/// 
		/// </param>
		/// <seealso cref="runAndWrite">
		/// 
		/// </seealso>
		public abstract void  initialize();
		
		/// <summary> Runs the rate allocation algorithm and writes the data to the
		/// bit stream. This must be called after the initialize() method.
		/// 
		/// </summary>
		/// <seealso cref="initialize">
		/// 
		/// </seealso>
		public abstract void  runAndWrite();
		
		/// <summary> Creates a PostCompRateAllocator object for the appropriate rate
		/// allocation parameters in the parameter list 'pl', having 'src' as the
		/// source of entropy coded data, 'rate' as the target bitrate and 'bw' as
		/// the bit stream writer object.
		/// 
		/// </summary>
		/// <param name="src">The source of entropy coded data.
		/// 
		/// </param>
		/// <param name="pl">The parameter lis (or options).
		/// 
		/// </param>
		/// <param name="rate">The target bitrate for the rate allocation
		/// 
		/// </param>
		/// <param name="bw">The bit stream writer object, where the bit stream data will
		/// be written.
		/// 
		/// </param>
		public static PostCompRateAllocator createInstance(CodedCBlkDataSrcEnc src, ParameterList pl, float rate, CodestreamWriter bw, EncoderSpecs encSpec)
		{
			// Check parameters
			pl.checkList(OPT_PREFIX, CSJ2K.j2k.util.ParameterList.toNameArray(pinfo));
			
			// Construct the layer specification from the 'Alayers' option
			LayersInfo lyrs = parseAlayers(pl.getParameter("Alayers"), rate);
			
			int nTiles = encSpec.nTiles;
			int nComp = encSpec.nComp;
			int numLayers = lyrs.TotNumLayers;
			
			// Parse the progressive type
			encSpec.pocs = new ProgressionSpec(nTiles, nComp, numLayers, encSpec.dls, ModuleSpec.SPEC_TYPE_TILE_COMP, pl);
			
			return new EBCOTRateAllocator(src, lyrs, bw, encSpec, pl);
		}
		
		/// <summary> Convenience method that parses the 'Alayers' option.
		/// 
		/// </summary>
		/// <param name="params">The parameters of the 'Alayers' option
		/// 
		/// </param>
		/// <param name="rate">The overall target bitrate
		/// 
		/// </param>
		/// <returns> The layer specification.
		/// 
		/// </returns>
		private static LayersInfo parseAlayers(System.String params_Renamed, float rate)
		{
			LayersInfo lyrs;
			SupportClass.StreamTokenizerSupport stok;
			bool islayer, ratepending;
			float r;
			
			lyrs = new LayersInfo(rate);
			stok = new SupportClass.StreamTokenizerSupport(new System.IO.StringReader(params_Renamed));
			stok.EOLIsSignificant(false);
			
			try
			{
				stok.NextToken();
			}
			catch (System.IO.IOException)
			{
				throw new System.ApplicationException("An IOException has ocurred where it " + "should never occur");
			}
			ratepending = false;
			islayer = false;
			r = 0; // to keep compiler happy
			while (stok.ttype != SupportClass.StreamTokenizerSupport.TT_EOF)
			{
				switch (stok.ttype)
				{
					
					case SupportClass.StreamTokenizerSupport.TT_NUMBER: 
						if (islayer)
						{
							// layer parameter
							try
							{
								//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
								lyrs.addOptPoint(r, (int) stok.nval);
							}
							catch (System.ArgumentException e)
							{
								//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
								throw new System.ArgumentException("Error in 'Alayers' " + "option: " + e.Message);
							}
							ratepending = false;
							islayer = false;
						}
						else
						{
							// rate parameter
							if (ratepending)
							{
								// Add pending rate parameter
								try
								{
									lyrs.addOptPoint(r, 0);
								}
								catch (System.ArgumentException e)
								{
									//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
									throw new System.ArgumentException("Error in 'Alayers' " + "option: " + e.Message);
								}
							}
							// Now store new rate parameter
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							r = (float) stok.nval;
							ratepending = true;
						}
						break;
					
					case '+': 
						if (!ratepending || islayer)
						{
							throw new System.ArgumentException("Layer parameter without " + "previous rate parameter " + "in 'Alayers' option");
						}
						islayer = true; // Next number is layer parameter
						break;
					
					case SupportClass.StreamTokenizerSupport.TT_WORD: 
						try
						{
							stok.NextToken();
						}
						catch (System.IO.IOException)
						{
							throw new System.ApplicationException("An IOException has ocurred where it " + "should never occur");
						}
						if (stok.ttype != SupportClass.StreamTokenizerSupport.TT_EOF)
						{
							throw new System.ArgumentException("'sl' argument of " + "'-Alayers' option must be " + "used alone.");
						}
						break;
					
					default: 
						throw new System.ArgumentException("Error parsing 'Alayers' " + "option");
					
				}
				try
				{
					stok.NextToken();
				}
				catch (System.IO.IOException)
				{
					throw new System.ApplicationException("An IOException has ocurred where it " + "should never occur");
				}
			}
			if (islayer)
			{
				throw new System.ArgumentException("Error parsing 'Alayers' " + "option");
			}
			if (ratepending)
			{
				try
				{
					lyrs.addOptPoint(r, 0);
				}
				catch (System.ArgumentException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new System.ArgumentException("Error in 'Alayers' " + "option: " + e.Message);
				}
			}
			return lyrs;
		}
	}
}