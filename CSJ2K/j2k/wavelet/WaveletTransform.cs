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
using CSJ2K.j2k.image;
namespace CSJ2K.j2k.wavelet
{
	
	/// <summary> This interface defines how a forward or inverse wavelet transform should
	/// present itself. As specified in the ImgData interface, from which this
	/// class inherits, all operations are confined to the current tile, and all
	/// coordinates are relative to it.
	/// 
	/// <p>The definition of the methods in this interface allows for different
	/// types of implementation, reversibility and levels of decompositions for
	/// each component and each tile. An implementation of this interface does not
	/// need to support all this flexibility (e.g., it may provide the same
	/// implementation type and decomposition levels for all tiles and
	/// components).</p>
	/// 
	/// </summary>
	public struct WaveletTransform_Fields{
		/// <summary> ID for line based implementations of wavelet transforms.
		/// 
		/// </summary>
		public readonly static int WT_IMPL_LINE = 0;
		/// <summary> ID for full-page based implementations of wavelet transforms. Full-page
		/// based implementations should be avoided since they require large
		/// amounts of memory.
		/// 
		/// </summary>
		public readonly static int WT_IMPL_FULL = 2;
	}
	public interface WaveletTransform:ImgData
	{
		//UPGRADE_NOTE: Members of interface 'WaveletTransform' were extracted into structure 'WaveletTransform_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		
		
		/// <summary> Returns the reversibility of the wavelet transform for the specified
		/// component and tile. A wavelet transform is reversible when it is
		/// suitable for lossless and lossy-to-lossless compression.
		/// 
		/// </summary>
		/// <param name="t">The index of the tile.
		/// 
		/// </param>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> true is the wavelet transform is reversible, false if not.
		/// 
		/// </returns>
		bool isReversible(int t, int c);
		
		/// <summary> Returns the implementation type of this wavelet transform (WT_IMPL_LINE
		/// or WT_IMPL_FRAME) for the specified component, in the current tile.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> WT_IMPL_LINE or WT_IMPL_FULL for line, block or full-page based
		/// transforms.
		/// 
		/// </returns>
		int getImplementationType(int c);
	}
}