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
using FileFormatBoxes = CSJ2K.j2k.fileformat.FileFormatBoxes;
using ICCProfile = CSJ2K.Icc.ICCProfile;
using ParameterList = CSJ2K.j2k.util.ParameterList;
using RandomAccessIO = CSJ2K.j2k.io.RandomAccessIO;
namespace CSJ2K.Color.Boxes
{
	
	/// <summary> The abstract super class modeling the aspects of
	/// a JP2 box common to all such boxes.
	/// 
	/// </summary>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A. Kern
	/// </author>
	public abstract class JP2Box
	{
		/// <summary>Platform dependant line terminator </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'eol '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String eol = System.Environment.NewLine;
		/// <summary>Box type                           </summary>
		public static int type;
		
		/// <summary>Return a String representation of the Box type. </summary>
		public static System.String getTypeString(int t)
		{
			return BoxType.get_Renamed(t);
		}
		
		/// <summary>Length of the box.             </summary>
		public int length;
		/// <summary>input file                     </summary>
		protected internal RandomAccessIO in_Renamed;
		/// <summary>offset to start of box         </summary>
		protected internal int boxStart;
		/// <summary>offset to end of box           </summary>
		protected internal int boxEnd;
		/// <summary>offset to start of data in box </summary>
		protected internal int dataStart;
		
		public JP2Box()
		{
			try
			{
				throw new ColorSpaceException("JP2Box empty ctor called!!");
			}
			catch (ColorSpaceException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error); throw e;
			}
		}
		
		/// <summary> Construct a JP2Box from an input image.</summary>
		/// <param name="in">RandomAccessIO jp2 image
		/// </param>
		/// <param name="boxStart">offset to the start of the box in the image
		/// </param>
		/// <exception cref="IOException,">ColorSpaceException 
		/// </exception>
		public JP2Box(RandomAccessIO in_Renamed, int boxStart)
		{
			byte[] boxHeader = new byte[16];
			
			this.in_Renamed = in_Renamed;
			this.boxStart = boxStart;
			
			this.in_Renamed.seek(this.boxStart);
			this.in_Renamed.readFully(boxHeader, 0, 8);
			
			this.dataStart = boxStart + 8;
            this.length = ICCProfile.getInt(boxHeader, 0);
			this.boxEnd = boxStart + length;
			if (length == 1)
				throw new ColorSpaceException("extended length boxes not supported");
		}
		
		
		/// <summary>Return the box type as a String. </summary>
		public virtual System.String getTypeString()
		{
			return BoxType.get_Renamed(JP2Box.type);
		}
		
		
		/// <summary>JP2 Box structure analysis help </summary>
		[Serializable]
		protected internal class BoxType:System.Collections.Hashtable
		{
			
			private static System.Collections.Hashtable map = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			
			private static void  put(int type, System.String desc)
			{
				map[(System.Int32) type] = desc;
			}
			
			public static System.String get_Renamed(int type)
			{
				return (System.String) map[(System.Int32) type];
			}
			
			/* end class BoxType */
			static BoxType()
			{
				{
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.BITS_PER_COMPONENT_BOX, "BITS_PER_COMPONENT_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.CAPTURE_RESOLUTION_BOX, "CAPTURE_RESOLUTION_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.CHANNEL_DEFINITION_BOX, "CHANNEL_DEFINITION_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.COLOUR_SPECIFICATION_BOX, "COLOUR_SPECIFICATION_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.COMPONENT_MAPPING_BOX, "COMPONENT_MAPPING_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.CONTIGUOUS_CODESTREAM_BOX, "CONTIGUOUS_CODESTREAM_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.DEFAULT_DISPLAY_RESOLUTION_BOX, "DEFAULT_DISPLAY_RESOLUTION_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.FILE_TYPE_BOX, "FILE_TYPE_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.IMAGE_HEADER_BOX, "IMAGE_HEADER_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.INTELLECTUAL_PROPERTY_BOX, "INTELLECTUAL_PROPERTY_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.JP2_HEADER_BOX, "JP2_HEADER_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.JP2_SIGNATURE_BOX, "JP2_SIGNATURE_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.PALETTE_BOX, "PALETTE_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.RESOLUTION_BOX, "RESOLUTION_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.URL_BOX, "URL_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.UUID_BOX, "UUID_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.UUID_INFO_BOX, "UUID_INFO_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.UUID_LIST_BOX, "UUID_LIST_BOX");
					put(CSJ2K.j2k.fileformat.FileFormatBoxes.XML_BOX, "XML_BOX");
				}
			}
		}
		
		/* end class JP2Box */
	}
}