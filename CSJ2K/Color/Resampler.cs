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
using CSJ2K.j2k.util;
using CSJ2K.j2k.image;
namespace CSJ2K.Color
{
	
	/// <summary> This class resamples the components of an image so that
	/// all have the same number of samples.  The current implementation
	/// only handles the case of 2:1 upsampling.
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.colorspace.ColorSpace">
	/// </seealso>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A. Kern
	/// </author>
	public class Resampler:ColorSpaceMapper
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'minCompSubsX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int minCompSubsX;
		//UPGRADE_NOTE: Final was removed from the declaration of 'minCompSubsY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int minCompSubsY;
		//UPGRADE_NOTE: Final was removed from the declaration of 'maxCompSubsX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int maxCompSubsX;
		//UPGRADE_NOTE: Final was removed from the declaration of 'maxCompSubsY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int maxCompSubsY;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'wspan '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: Final was removed from the declaration of 'hspan '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal int wspan = 0;
		internal int hspan = 0;
		
		/// <summary> Factory method for creating instances of this class.</summary>
		/// <param name="src">-- source of image data
		/// </param>
		/// <param name="csMap">-- provides colorspace info
		/// </param>
		/// <returns> Resampler instance
		/// </returns>
		public static new BlkImgDataSrc createInstance(BlkImgDataSrc src, ColorSpace csMap)
		{
			return new Resampler(src, csMap);
		}
		
		/// <summary> Ctor resamples a BlkImgDataSrc so that all components
		/// have the same number of samples.
		/// 
		/// Note the present implementation does only two to one
		/// respampling in either direction (row, column).
		/// 
		/// </summary>
		/// <param name="src">-- Source of image data
		/// </param>
		/// <param name="csm">-- provides colorspace info
		/// </param>
		protected internal Resampler(BlkImgDataSrc src, ColorSpace csMap):base(src, csMap)
		{
			
			int c;
			
			// Calculate the minimum and maximum subsampling factor
			// across all channels.
			
			int minX = src.getCompSubsX(0);
			int minY = src.getCompSubsY(0);
			int maxX = minX;
			int maxY = minY;
			
			for (c = 1; c < ncomps; ++c)
			{
				minX = System.Math.Min(minX, src.getCompSubsX(c));
				minY = System.Math.Min(minY, src.getCompSubsY(c));
				maxX = System.Math.Max(maxX, src.getCompSubsX(c));
				maxY = System.Math.Max(maxY, src.getCompSubsY(c));
			}
			
			// Throw an exception for other than 2:1 sampling.
			if ((maxX != 1 && maxX != 2) || (maxY != 1 && maxY != 2))
			{
				throw new ColorSpaceException("Upsampling by other than 2:1 not" + " supported");
			}
			
			minCompSubsX = minX;
			minCompSubsY = minY;
			maxCompSubsX = maxX;
			maxCompSubsY = maxY;
			
			/* end Resampler ctor */
		}
		
		/// <summary> Return a DataBlk containing the requested component
		/// upsampled by the scale factor applied to the particular
		/// scaling direction
		/// 
		/// Returns, in the blk argument, a block of image data containing the
		/// specifed rectangular area, in the specified component. The data is
		/// returned, as a copy of the internal data, therefore the returned data
		/// can be modified "in place".
		/// 
		/// <P>The rectangular area to return is specified by the 'ulx', 'uly', 'w'
		/// and 'h' members of the 'blk' argument, relative to the current
		/// tile. These members are not modified by this method. The 'offset' of
		/// the returned data is 0, and the 'scanw' is the same as the block's
		/// width. See the 'DataBlk' class.
		/// 
		/// <P>If the data array in 'blk' is 'null', then a new one is created. If
		/// the data array is not 'null' then it is reused, and it must be large
		/// enough to contain the block's data. Otherwise an 'ArrayStoreException'
		/// or an 'IndexOutOfBoundsException' is thrown by the Java system.
		/// 
		/// <P>The returned data has its 'progressive' attribute set to that of the
		/// input data.
		/// 
		/// </summary>
		/// <param name="blk">Its coordinates and dimensions specify the area to
		/// return. If it contains a non-null data array, then it must have the
		/// correct dimensions. If it contains a null data array a new one is
		/// created. The fields in this object are modified to return the data.
		/// 
		/// </param>
		/// <param name="c">The index of the component from which to get the data. Only 0
		/// and 3 are valid.
		/// 
		/// </param>
		/// <returns> The requested DataBlk
		/// 
		/// </returns>
		/// <seealso cref="getCompData">
		/// </seealso>
		public override DataBlk getInternCompData(DataBlk outblk, int c)
		{
			
			// If the scaling factor of this channel is 1 in both
			// directions, simply return the source DataBlk.
			
			if (src.getCompSubsX(c) == 1 && src.getCompSubsY(c) == 1)
				return src.getInternCompData(outblk, c);
			
			int wfactor = src.getCompSubsX(c);
			int hfactor = src.getCompSubsY(c);
			if ((wfactor != 2 && wfactor != 1) || (hfactor != 2 && hfactor != 1))
				throw new System.ArgumentException("Upsampling by other than 2:1" + " not supported");
			
			int leftedgeOut = - 1; // offset to the start of the output scanline
			int rightedgeOut = - 1; // offset to the end of the output
			// scanline + 1
			int leftedgeIn = - 1; // offset to the start of the input scanline  
			int rightedgeIn = - 1; // offset to the end of the input scanline + 1
			
			
			int y0In, y1In, y0Out, y1Out;
			int x0In, x1In, x0Out, x1Out;
			
			y0Out = outblk.uly;
			y1Out = y0Out + outblk.h - 1;
			
			x0Out = outblk.ulx;
			x1Out = x0Out + outblk.w - 1;
			
			y0In = y0Out / hfactor;
			y1In = y1Out / hfactor;
			
			x0In = x0Out / wfactor;
			x1In = x1Out / wfactor;
			
			
			// Calculate the requested height and width, requesting an extra
			// row and or for upsampled channels.
			int reqW = x1In - x0In + 1;
			int reqH = y1In - y0In + 1;
			
			// Initialize general input and output indexes
			int kOut = - 1;
			int kIn = - 1;
			int yIn;
			
			switch (outblk.DataType)
			{
				
				
				case DataBlk.TYPE_INT: 
					
					DataBlkInt inblkInt = new DataBlkInt(x0In, y0In, reqW, reqH);
					inblkInt = (DataBlkInt) src.getInternCompData(inblkInt, c);
					dataInt[c] = inblkInt.DataInt;
					
					// Reference the working array   
					int[] outdataInt = (int[]) outblk.Data;
					
					// Create data array if necessary
					if (outdataInt == null || outdataInt.Length != outblk.w * outblk.h)
					{
						outdataInt = new int[outblk.h * outblk.w];
						outblk.Data = outdataInt;
					}
					
					// The nitty-gritty.
					
					for (int yOut = y0Out; yOut <= y1Out; ++yOut)
					{
						
						yIn = yOut / hfactor;
						
						
						leftedgeIn = inblkInt.offset + (yIn - y0In) * inblkInt.scanw;
						rightedgeIn = leftedgeIn + inblkInt.w;
						leftedgeOut = outblk.offset + (yOut - y0Out) * outblk.scanw;
						rightedgeOut = leftedgeOut + outblk.w;
						
						kIn = leftedgeIn;
						kOut = leftedgeOut;
						
						if ((x0Out & 0x1) == 1)
						{
							// first is odd do the pixel once.
							outdataInt[kOut++] = dataInt[c][kIn++];
						}
						
						if ((x1Out & 0x1) == 0)
						{
							// last is even adjust loop bounds
							rightedgeOut--;
						}
						
						while (kOut < rightedgeOut)
						{
							outdataInt[kOut++] = dataInt[c][kIn];
							outdataInt[kOut++] = dataInt[c][kIn++];
						}
						
						if ((x1Out & 0x1) == 0)
						{
							// last is even do the pixel once.
							outdataInt[kOut++] = dataInt[c][kIn];
						}
					}
					
					outblk.progressive = inblkInt.progressive;
					break;
				
				
				case DataBlk.TYPE_FLOAT: 
					
					DataBlkFloat inblkFloat = new DataBlkFloat(x0In, y0In, reqW, reqH);
					inblkFloat = (DataBlkFloat) src.getInternCompData(inblkFloat, c);
					dataFloat[c] = inblkFloat.DataFloat;
					
					// Reference the working array   
					float[] outdataFloat = (float[]) outblk.Data;
					
					// Create data array if necessary
					if (outdataFloat == null || outdataFloat.Length != outblk.w * outblk.h)
					{
						outdataFloat = new float[outblk.h * outblk.w];
						outblk.Data = outdataFloat;
					}
					
					// The nitty-gritty.
					
					for (int yOut = y0Out; yOut <= y1Out; ++yOut)
					{
						
						yIn = yOut / hfactor;
						
						
						leftedgeIn = inblkFloat.offset + (yIn - y0In) * inblkFloat.scanw;
						rightedgeIn = leftedgeIn + inblkFloat.w;
						leftedgeOut = outblk.offset + (yOut - y0Out) * outblk.scanw;
						rightedgeOut = leftedgeOut + outblk.w;
						
						kIn = leftedgeIn;
						kOut = leftedgeOut;
						
						if ((x0Out & 0x1) == 1)
						{
							// first is odd do the pixel once.
							outdataFloat[kOut++] = dataFloat[c][kIn++];
						}
						
						if ((x1Out & 0x1) == 0)
						{
							// last is even adjust loop bounds
							rightedgeOut--;
						}
						
						while (kOut < rightedgeOut)
						{
							outdataFloat[kOut++] = dataFloat[c][kIn];
							outdataFloat[kOut++] = dataFloat[c][kIn++];
						}
						
						if ((x1Out & 0x1) == 0)
						{
							// last is even do the pixel once.
							outdataFloat[kOut++] = dataFloat[c][kIn];
						}
					}
					
					outblk.progressive = inblkFloat.progressive;
					break;
				
				
				case DataBlk.TYPE_SHORT: 
				case DataBlk.TYPE_BYTE: 
				default: 
					// Unsupported output type. 
					throw new System.ArgumentException("invalid source datablock " + "type");
				}
			
			return outblk;
		}
		
		
		/// <summary> Return an appropriate String representation of this Resampler instance.</summary>
		public override System.String ToString()
		{
			System.Text.StringBuilder rep = new System.Text.StringBuilder("[Resampler: ncomps= " + ncomps);
			System.Text.StringBuilder body = new System.Text.StringBuilder("  ");
			for (int i = 0; i < ncomps; ++i)
			{
				body.Append(eol);
				body.Append("comp[");
				body.Append(i);
				body.Append("] xscale= ");
				body.Append(imgdatasrc.getCompSubsX(i));
				body.Append(", yscale= ");
				body.Append(imgdatasrc.getCompSubsY(i));
			}
			
			rep.Append(ColorSpace.indent("  ", body));
			return rep.Append("]").ToString();
		}
		
		
		
		/// <summary> Returns, in the blk argument, a block of image data containing the
		/// specifed rectangular area, in the specified component. The data is
		/// returned, as a copy of the internal data, therefore the returned data
		/// can be modified "in place".
		/// 
		/// <P>The rectangular area to return is specified by the 'ulx', 'uly', 'w'
		/// and 'h' members of the 'blk' argument, relative to the current
		/// tile. These members are not modified by this method. The 'offset' of
		/// the returned data is 0, and the 'scanw' is the same as the block's
		/// width. See the 'DataBlk' class.
		/// 
		/// <P>If the data array in 'blk' is 'null', then a new one is created. If
		/// the data array is not 'null' then it is reused, and it must be large
		/// enough to contain the block's data. Otherwise an 'ArrayStoreException'
		/// or an 'IndexOutOfBoundsException' is thrown by the Java system.
		/// 
		/// <P>The returned data has its 'progressive' attribute set to that of the
		/// input data.
		/// 
		/// </summary>
		/// <param name="blk">Its coordinates and dimensions specify the area to
		/// return. If it contains a non-null data array, then it must have the
		/// correct dimensions. If it contains a null data array a new one is
		/// created. The fields in this object are modified to return the data.
		/// 
		/// </param>
		/// <param name="c">The index of the component from which to get the data. Only 0
		/// and 3 are valid.
		/// 
		/// </param>
		/// <returns> The requested DataBlk
		/// 
		/// </returns>
		/// <seealso cref="getInternCompData">
		/// 
		/// </seealso>
		public override DataBlk getCompData(DataBlk outblk, int c)
		{
			return getInternCompData(outblk, c);
		}
		
		
		/// <summary> Returns the height in pixels of the specified component in the
		/// overall image.
		/// </summary>
		public override int getCompImgHeight(int c)
		{
			return src.getCompImgHeight(c) * src.getCompSubsY(c);
		}
		
		/// <summary> Returns the width in pixels of the specified component in the
		/// overall image.
		/// </summary>
		public override int getCompImgWidth(int c)
		{
			return src.getCompImgWidth(c) * src.getCompSubsX(c);
		}
		
		/// <summary> Returns the component subsampling factor in the horizontal
		/// direction, for the specified component.
		/// </summary>
		public override int getCompSubsX(int c)
		{
			return 1;
		}
		
		/// <summary> Returns the component subsampling factor in the vertical
		/// direction, for the specified component.
		/// </summary>
		public override int getCompSubsY(int c)
		{
			return 1;
		}
		
		/// <summary> Returns the height in pixels of the specified tile-component.</summary>
		public override int getTileCompHeight(int t, int c)
		{
			return src.getTileCompHeight(t, c) * src.getCompSubsY(c);
		}
		
		/// <summary> Returns the width in pixels of the specified tile-component..</summary>
		public override int getTileCompWidth(int t, int c)
		{
			return src.getTileCompWidth(t, c) * src.getCompSubsX(c);
		}
		
		/* end class Resampler */
	}
}