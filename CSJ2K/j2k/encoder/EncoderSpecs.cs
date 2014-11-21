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
using CSJ2K.j2k.quantization.quantizer;
using CSJ2K.j2k.image.forwcomptransf;
using CSJ2K.j2k.wavelet.analysis;
using CSJ2K.j2k.entropy.encoder;
using CSJ2K.j2k.quantization;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.entropy;
using CSJ2K.j2k.util;
using CSJ2K.j2k.image;
using CSJ2K.j2k.roi;
using CSJ2K.j2k;
namespace CSJ2K.j2k.encoder
{
	
	/// <summary> This class holds references to each module specifications used in the
	/// encoding chain. This avoid big amount of arguments in method calls. A
	/// specification contains values of each tile-component for one module. All
	/// members must be instance of ModuleSpec class (or its children).
	/// 
	/// </summary>
	/// <seealso cref="ModuleSpec">
	/// 
	/// </seealso>
	public class EncoderSpecs
	{
		
		/// <summary>ROI maxshift value specifications </summary>
		public MaxShiftSpec rois;
		
		/// <summary>Quantization type specifications </summary>
		public QuantTypeSpec qts;
		
		/// <summary>Quantization normalized base step size specifications </summary>
		public QuantStepSizeSpec qsss;
		
		/// <summary>Number of guard bits specifications </summary>
		public GuardBitsSpec gbs;
		
		/// <summary>Analysis wavelet filters specifications </summary>
		public AnWTFilterSpec wfs;
		
		/// <summary>Component transformation specifications </summary>
		public CompTransfSpec cts;
		
		/// <summary>Number of decomposition levels specifications </summary>
		public IntegerSpec dls;
		
		/// <summary>The length calculation specifications </summary>
		public StringSpec lcs;
		
		/// <summary>The termination type specifications </summary>
		public StringSpec tts;
		
		/// <summary>Error resilience segment symbol use specifications </summary>
		public StringSpec sss;
		
		/// <summary>Causal stripes specifications </summary>
		public StringSpec css;
		
		/// <summary>Regular termination specifications </summary>
		public StringSpec rts;
		
		/// <summary>MQ reset specifications </summary>
		public StringSpec mqrs;
		
		/// <summary>By-pass mode specifications </summary>
		public StringSpec bms;
		
		/// <summary>Precinct partition specifications </summary>
		public PrecinctSizeSpec pss;
		
		/// <summary>Start of packet (SOP) marker use specification </summary>
		public StringSpec sops;
		
		/// <summary>End of packet header (EPH) marker use specification </summary>
		public StringSpec ephs;
		
		/// <summary>Code-blocks sizes specification </summary>
		public CBlkSizeSpec cblks;
		
		/// <summary>Progression/progression changes specification </summary>
		public ProgressionSpec pocs;
		
		/// <summary>The number of tiles within the image </summary>
		public int nTiles;
		
		/// <summary>The number of components within the image </summary>
		public int nComp;
		
		/// <summary> Initialize all members with the given number of tiles and components
		/// and the command-line arguments stored in a ParameterList instance
		/// 
		/// </summary>
		/// <param name="nt">Number of tiles
		/// 
		/// </param>
		/// <param name="nc">Number of components
		/// 
		/// </param>
		/// <param name="imgsrc">The image source (used to get the image size)
		/// 
		/// </param>
		/// <param name="pl">The ParameterList instance
		/// 
		/// </param>
		public EncoderSpecs(int nt, int nc, BlkImgDataSrc imgsrc, ParameterList pl)
		{
			nTiles = nt;
			nComp = nc;
			
			// ROI
			rois = new MaxShiftSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			
			// Quantization
			pl.checkList(Quantizer.OPT_PREFIX, CSJ2K.j2k.util.ParameterList.toNameArray(Quantizer.ParameterInfo));
			qts = new QuantTypeSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, pl);
			qsss = new QuantStepSizeSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, pl);
			gbs = new GuardBitsSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, pl);
			
			// Wavelet transform
			wfs = new AnWTFilterSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, qts, pl);
			dls = new IntegerSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, pl, "Wlev");
			
			// Component transformation
			cts = new ForwCompTransfSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE, wfs, pl);
			
			// Entropy coder
			System.String[] strLcs = new System.String[]{"near_opt", "lazy_good", "lazy"};
			lcs = new StringSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, "Clen_calc", strLcs, pl);
			System.String[] strTerm = new System.String[]{"near_opt", "easy", "predict", "full"};
			tts = new StringSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, "Cterm_type", strTerm, pl);
			System.String[] strBoolean = new System.String[]{"on", "off"};
			sss = new StringSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, "Cseg_symbol", strBoolean, pl);
			css = new StringSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, "Ccausal", strBoolean, pl);
			rts = new StringSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, "Cterminate", strBoolean, pl);
			mqrs = new StringSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, "CresetMQ", strBoolean, pl);
			bms = new StringSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, "Cbypass", strBoolean, pl);
			cblks = new CBlkSizeSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, pl);
			
			// Precinct partition
			pss = new PrecinctSizeSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, imgsrc, dls, pl);
			
			// Codestream
			sops = new StringSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE, "Psop", strBoolean, pl);
			ephs = new StringSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE, "Peph", strBoolean, pl);
		}
	}
}