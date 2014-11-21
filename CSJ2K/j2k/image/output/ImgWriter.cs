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
using CSJ2K.j2k.util;
using CSJ2K.j2k;
namespace CSJ2K.j2k.image.output
{
	
	/// <summary> This is the generic interface to be implemented by all image file (or other
	/// resource) writers for different formats.
	/// 
	/// <p>Each object inheriting from this class should have a source ImgData
	/// object associated with it. The image data to write to the file is obtained
	/// from the associated ImgData object. In general this object would be
	/// specified at construction time.</p>
	/// 
	/// <p>Depending on the actual type of file that is written a call to any
	/// write() or writeAll() method will write data from one component, several
	/// components or all components. For example, a PGM writer will write data
	/// from only one component (defined in the constructor) while a PPM writer
	/// will write 3 components (normally R,G,B).</p>
	/// 
	/// </summary>
	public abstract class ImgWriter
	{
		
		/// <summary>The defaukt height used when writing strip by strip in the 'write()'
		/// method. It is 64. 
		/// </summary>
		public const int DEF_STRIP_HEIGHT = 64;
		
		/// <summary>The source ImagaData object, from where to get the image data </summary>
		protected internal BlkImgDataSrc src;
		
		/// <summary>The width of the image </summary>
		protected internal int w;
		
		/// <summary>The height of the image </summary>
		protected internal int h;
		
		/// <summary> Closes the underlying file or netwrok connection to where the data is
		/// written. The implementing class must write all buffered data before
		/// closing the file or resource. Any call to other methods of the class
		/// become illegal after a call to this one.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs.
		/// 
		/// </exception>
		public abstract void  close();
		
		/// <summary> Writes all buffered data to the file or resource. If the implementing
		/// class does onot use buffering nothing should be done.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs.
		/// 
		/// </exception>
		public abstract void  flush();
		
		/// <summary> Flushes the buffered data before the object is garbage collected. If an
		/// exception is thrown the object finalization is halted, but is otherwise
		/// ignored.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs. It halts the
		/// finalization of the object, but is otherwise ignored.
		/// 
		/// </exception>
		/// <seealso cref="Object.finalize">
		/// 
		/// </seealso>
		~ImgWriter()
		{
			flush();
		}
		
		/// <summary> Writes the source's current tile to the output. The requests of data
		/// issued by the implementing class to the source ImgData object should be
		/// done by blocks or strips, in order to reduce memory usage.
		/// 
		/// <p>The implementing class should only write data that is not
		/// "progressive" (in other words that it is final), see DataBlk for
		/// details.</p>
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs.
		/// 
		/// </exception>
		/// <seealso cref="DataBlk">
		/// 
		/// </seealso>
		public abstract void  write();
		
		/// <summary> Writes the entire image or only specified tiles to the output. The
		/// implementation in this class calls the write() method for each tile
		/// starting with the upper-left one and proceding in standard scanline
		/// order. It changes the current tile of the source data.
		/// 
		/// </summary>
		/// <exception cref="IOException">If an I/O error occurs.
		/// 
		/// </exception>
		/// <seealso cref="DataBlk">
		/// 
		/// </seealso>
		public virtual void  writeAll()
		{
			// Find the list of tile to decode.
			Coord nT = src.getNumTiles(null);
			
			// Loop on vertical tiles
			for (int y = 0; y < nT.y; y++)
			{
				// Loop on horizontal tiles
				for (int x = 0; x < nT.x; x++)
				{
					src.setTile(x, y);
					write();
				} // End loop on horizontal tiles            
			} // End loop on vertical tiles
		}
		
		/// <summary> Writes the data of the specified area to the file, coordinates are
		/// relative to the current tile of the source.
		/// 
		/// <p>The implementing class should only write data that is not
		/// "progressive" (in other words that is final), see DataBlk for
		/// details.</p>
		/// 
		/// </summary>
		/// <param name="ulx">The horizontal coordinate of the upper-left corner of the
		/// area to write, relative to the current tile.
		/// 
		/// </param>
		/// <param name="uly">The vertical coordinate of the upper-left corner of the area
		/// to write, relative to the current tile.
		/// 
		/// </param>
		/// <param name="width">The width of the area to write.
		/// 
		/// </param>
		/// <param name="height">The height of the area to write.
		/// 
		/// </param>
		/// <exception cref="IOException">If an I/O error occurs.
		/// 
		/// </exception>
		public abstract void  write(int ulx, int uly, int w, int h);
	}
}