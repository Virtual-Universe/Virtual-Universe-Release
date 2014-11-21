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
using ColorSpaceException = CSJ2K.Color.ColorSpaceException;
using ParameterList = CSJ2K.j2k.util.ParameterList;
using RandomAccessIO = CSJ2K.j2k.io.RandomAccessIO;
using ICCProfile = CSJ2K.Icc.ICCProfile;
namespace CSJ2K.Color.Boxes
{
	
	/// <summary> This class models the Image Header box contained in a JP2
	/// image.  It is a stub class here since for colormapping the
	/// knowlege of the existance of the box in the image is sufficient.
	/// 
	/// </summary>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A. Kern
	/// </author>
	public sealed class ImageHeaderBox:JP2Box
	{
		
		internal long height;
		internal long width;
		internal int nc;
		internal short bpc;
		internal short c;
		internal bool unk;
		internal bool ipr;
		
		
		/// <summary> Construct an ImageHeaderBox from an input image.</summary>
		/// <param name="in">RandomAccessIO jp2 image
		/// </param>
		/// <param name="boxStart">offset to the start of the box in the image
		/// </param>
		/// <exception cref="IOException,">ColorSpaceException
		/// </exception>
		public ImageHeaderBox(RandomAccessIO in_Renamed, int boxStart):base(in_Renamed, boxStart)
		{
			readBox();
		}
		
		/// <summary>Return a suitable String representation of the class instance. </summary>
		public override System.String ToString()
		{
			System.Text.StringBuilder rep = new System.Text.StringBuilder("[ImageHeaderBox ").Append(eol).Append("  ");
			rep.Append("height= ").Append(System.Convert.ToString(height)).Append(", ");
			rep.Append("width= ").Append(System.Convert.ToString(width)).Append(eol).Append("  ");
			
			rep.Append("nc= ").Append(System.Convert.ToString(nc)).Append(", ");
			rep.Append("bpc= ").Append(System.Convert.ToString(bpc)).Append(", ");
			rep.Append("c= ").Append(System.Convert.ToString(c)).Append(eol).Append("  ");
			
			rep.Append("image colorspace is ").Append(new System.Text.StringBuilder(unk == true?"known":"unknown").ToString());
			rep.Append(", the image ").Append(new System.Text.StringBuilder(ipr == true?"contains ":"does not contain ").ToString()).Append("intellectual property").Append("]");
			
			return rep.ToString();
		}
		
		/// <summary>Analyze the box content. </summary>
		internal void  readBox()
		{
			byte[] bfr = new byte[14];
			in_Renamed.seek(dataStart);
			in_Renamed.readFully(bfr, 0, 14);

            height = ICCProfile.getInt(bfr, 0);
            width = ICCProfile.getInt(bfr, 4);
            nc = ICCProfile.getShort(bfr, 8);
			bpc = (short) (bfr[10] & 0x00ff);
			c = (short) (bfr[11] & 0x00ff);
			unk = bfr[12] == 0?true:false;
			ipr = bfr[13] == 1?true:false;
		}
		
		/* end class ImageHeaderBox */
		static ImageHeaderBox()
		{
			{
				type = 69686472;
			}
		}
	}
}