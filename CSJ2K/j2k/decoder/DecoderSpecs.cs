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
using CSJ2K.j2k.codestream.reader;
using CSJ2K.j2k.wavelet.synthesis;
using CSJ2K.j2k.quantization;
using CSJ2K.j2k.entropy;
using CSJ2K.j2k.wavelet;
using CSJ2K.j2k.image;
using CSJ2K.j2k.util;
using CSJ2K.j2k.roi;
using CSJ2K.j2k;
namespace CSJ2K.j2k.decoder
{
	
	/// <summary> This class holds references to each module specifications used in the
	/// decoding chain. This avoid big amount of arguments in method calls. A
	/// specification contains values of each tile-component for one module. All
	/// members must be instance of ModuleSpec class (or its children).
	/// 
	/// </summary>
	/// <seealso cref="ModuleSpec">
	/// 
	/// </seealso>
	public class DecoderSpecs : System.ICloneable
	{
		/// <summary> Returns a copy of the current object.
		/// 
		/// </summary>
		virtual public DecoderSpecs Copy
		{
			get
			{
				DecoderSpecs decSpec2;
				try
				{
					decSpec2 = (DecoderSpecs) this.Clone();
				}
				//UPGRADE_NOTE: Exception 'java.lang.CloneNotSupportedException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception)
				{
					throw new System.ApplicationException("Cannot clone the DecoderSpecs instance");
				}
				// Quantization
				decSpec2.qts = (QuantTypeSpec) qts.Copy;
				decSpec2.qsss = (QuantStepSizeSpec) qsss.Copy;
				decSpec2.gbs = (GuardBitsSpec) gbs.Copy;
				// Wavelet transform
				decSpec2.wfs = (SynWTFilterSpec) wfs.Copy;
				decSpec2.dls = (IntegerSpec) dls.Copy;
				// Component transformation
				decSpec2.cts = (CompTransfSpec) cts.Copy;
				// ROI
				if (rois != null)
				{
					decSpec2.rois = (MaxShiftSpec) rois.Copy;
				}
				return decSpec2;
			}
			
		}
		
		/// <summary>ICC Profiling specifications </summary>
		public ModuleSpec iccs;
		
		/// <summary>ROI maxshift value specifications </summary>
		public MaxShiftSpec rois;
		
		/// <summary>Quantization type specifications </summary>
		public QuantTypeSpec qts;
		
		/// <summary>Quantization normalized base step size specifications </summary>
		public QuantStepSizeSpec qsss;
		
		/// <summary>Number of guard bits specifications </summary>
		public GuardBitsSpec gbs;
		
		/// <summary>Analysis wavelet filters specifications </summary>
		public SynWTFilterSpec wfs;
		
		/// <summary>Number of decomposition levels specifications </summary>
		public IntegerSpec dls;
		
		/// <summary>Number of layers specifications </summary>
		public IntegerSpec nls;
		
		/// <summary>Progression order specifications </summary>
		public IntegerSpec pos;
		
		/// <summary>The Entropy decoder options specifications </summary>
		public ModuleSpec ecopts;
		
		/// <summary>The component transformation specifications </summary>
		public CompTransfSpec cts;
		
		/// <summary>The progression changes specifications </summary>
		public ModuleSpec pcs;
		
		/// <summary>The error resilience specifications concerning the entropy
		/// decoder 
		/// </summary>
		public ModuleSpec ers;
		
		/// <summary>Precinct partition specifications </summary>
		public PrecinctSizeSpec pss;
		
		/// <summary>The Start Of Packet (SOP) markers specifications </summary>
		public ModuleSpec sops;
		
		/// <summary>The End of Packet Headers (EPH) markers specifications </summary>
		public ModuleSpec ephs;
		
		/// <summary>Code-blocks sizes specification </summary>
		public CBlkSizeSpec cblks;
		
		/// <summary>Packed packet header specifications </summary>
		public ModuleSpec pphs;
		
		/// <summary> Initialize all members with the given number of tiles and components.
		/// 
		/// </summary>
		/// <param name="nt">Number of tiles
		/// 
		/// </param>
		/// <param name="nc">Number of components
		/// 
		/// </param>
		public DecoderSpecs(int nt, int nc)
		{
			// Quantization
			qts = new QuantTypeSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			qsss = new QuantStepSizeSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			gbs = new GuardBitsSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			
			// Wavelet transform
			wfs = new SynWTFilterSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			dls = new IntegerSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			
			// Component transformation
			cts = new CompTransfSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			
			// Entropy decoder
			ecopts = new ModuleSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			ers = new ModuleSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			cblks = new CBlkSizeSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP);
			
			// Precinct partition
			pss = new PrecinctSizeSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE_COMP, dls);
			
			// Codestream
			nls = new IntegerSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE);
			pos = new IntegerSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE);
			pcs = new ModuleSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE);
			sops = new ModuleSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE);
			ephs = new ModuleSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE);
			pphs = new ModuleSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE);
			iccs = new ModuleSpec(nt, nc, ModuleSpec.SPEC_TYPE_TILE);
			pphs.setDefault((System.Object) false);
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		virtual public System.Object Clone()
		{
			return null;
		}
	}
}