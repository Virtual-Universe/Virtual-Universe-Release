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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Baker
{
	public static class Oven
	{
		public static Bitmap ModifyAlphaMask(Bitmap alpha, byte weight, float ramp)
		{
			// Create the new modifiable image (our canvas)
			int width = alpha.Width;
			int height = alpha.Height;
			int pixelFormatSize = Image.GetPixelFormatSize(alpha.PixelFormat) / 8;
			int stride = width * pixelFormatSize;
			byte[] data = new byte[stride * height];
			//GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
			IntPtr pointer = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
			Bitmap modified = new Bitmap(width, height, stride, alpha.PixelFormat, pointer);
			
			// Copy the existing alpha mask to the canvas
			Graphics g = Graphics.FromImage(modified);
			g.DrawImageUnscaledAndClipped(alpha, new Rectangle(0, 0, width, height));
			g.Dispose();
			
			// Modify the canvas based on the input weight and ramp values
			// TODO: use the ramp
			// TODO: only bother with the alpha values
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] < weight) data[i] = 0;
			}
			
			return modified;
		}
		
		public static Bitmap ApplyAlphaMask(Bitmap source, Bitmap alpha)
		{
			// Create the new modifiable image (our canvas)
			int width = source.Width;
			int height = source.Height;
			
			if (alpha.Width != width || alpha.Height != height ||
			    alpha.PixelFormat != source.PixelFormat)
			{
				throw new Exception("Source image and alpha mask formats do not match");
			}
			
			int pixelFormatSize = Image.GetPixelFormatSize(source.PixelFormat) / 8;
			int stride = width * pixelFormatSize;
			byte[] data = new byte[stride * height];
			//GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
			IntPtr pointer = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
			Bitmap modified = new Bitmap(width, height, stride, source.PixelFormat, pointer);
			
			// Copy the source image to the canvas
			Graphics g = Graphics.FromImage(modified);
			g.DrawImageUnscaledAndClipped(source, new Rectangle(0, 0, width, height));
			g.Dispose();
			
			// Get access to the pixel data for the alpha mask (probably using lockbits)
			
			// Combine the alpha mask alpha bytes in to the canvas
			
			return modified;
		}
	}
}
