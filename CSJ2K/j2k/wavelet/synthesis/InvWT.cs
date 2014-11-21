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
using CSJ2K.j2k.wavelet;
namespace CSJ2K.j2k.wavelet.synthesis
{
	
	/// <summary> This interface extends the WaveletTransform with the specifics of inverse
	/// wavelet transforms. Classes that implement inverse wavelet transfoms should
	/// implement this interface.
	/// 
	/// <p>This class does not define the methods to transfer data, just the
	/// specifics to inverse wavelet transform. Different data transfer methods are
	/// envisageable for different transforms.</p>
	/// 
	/// </summary>
	public interface InvWT:WaveletTransform
	{
		/// <summary> Sets the image reconstruction resolution level. A value of 0 means
		/// reconstruction of an image with the lowest resolution (dimension)
		/// available.
		/// 
		/// <p>Note: Image resolution level indexes may differ from tile-component
		/// resolution index. They are indeed indexed starting from the lowest
		/// number of decomposition levels of each component of each tile.</p>
		/// 
		/// <p>Example: For an image (1 tile) with 2 components (component 0 having
		/// 2 decomposition levels and component 1 having 3 decomposition levels),
		/// the first (tile-) component has 3 resolution levels and the second one
		/// has 4 resolution levels, whereas the image has only 3 resolution levels
		/// available.</p>
		/// 
		/// </summary>
		/// <param name="rl">The image resolution level.
		/// 
		/// </param>
		/// <returns> The vertical coordinate of the image origin in the canvas
		/// system, on the reference grid.
		/// 
		/// </returns>
		int ImgResLevel
		{
			set;
			
		}
	}
}