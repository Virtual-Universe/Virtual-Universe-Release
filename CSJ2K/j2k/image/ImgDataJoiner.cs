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
using CSJ2K.j2k;
namespace CSJ2K.j2k.image
{
	
	/// <summary> This class implements the ImgData interface and allows to obtain data from
	/// different sources. Here, one source is represented by an ImgData and a
	/// component index. The typical use of this class is when the encoder needs
	/// different components (Red, Green, Blue, alpha, ...) from different input
	/// files (i.e. from different ImgReader objects).
	/// 
	/// <p>All input ImgData must not be tiled (i.e. must have only 1 tile) and the
	/// image origin must be the canvas origin. The different inputs can have
	/// different dimensions though (this will lead to different subsampling
	/// factors for each component).</p>
	/// 
	/// <p>The input ImgData and component index list must be defined when
	/// constructing this class and can not be modified later.</p>
	/// 
	/// </summary>
	/// <seealso cref="ImgData">
	/// </seealso>
	/// <seealso cref="jj2000.j2k.image.input.ImgReader">
	/// 
	/// </seealso>
	public class ImgDataJoiner : BlkImgDataSrc
	{
		/// <summary> Returns the overall width of the current tile in pixels. This is the
		/// tile's width without accounting for any component subsampling.
		/// 
		/// </summary>
		/// <returns> The total current tile's width in pixels.
		/// 
		/// </returns>
		virtual public int TileWidth
		{
			get
			{
				return w;
			}
			
		}
		/// <summary> Returns the overall height of the current tile in pixels. This is the
		/// tile's height without accounting for any component subsampling.
		/// 
		/// </summary>
		/// <returns> The total current tile's height in pixels.
		/// 
		/// </returns>
		virtual public int TileHeight
		{
			get
			{
				return h;
			}
			
		}
		/// <summary>Returns the nominal tiles width </summary>
		virtual public int NomTileWidth
		{
			get
			{
				return w;
			}
			
		}
		/// <summary>Returns the nominal tiles height </summary>
		virtual public int NomTileHeight
		{
			get
			{
				return h;
			}
			
		}
		/// <summary> Returns the overall width of the image in pixels. This is the image's
		/// width without accounting for any component subsampling or tiling.
		/// 
		/// </summary>
		/// <returns> The total image's width in pixels.
		/// 
		/// </returns>
		virtual public int ImgWidth
		{
			get
			{
				return w;
			}
			
		}
		/// <summary> Returns the overall height of the image in pixels. This is the image's
		/// height without accounting for any component subsampling or tiling.
		/// 
		/// </summary>
		/// <returns> The total image's height in pixels.
		/// 
		/// </returns>
		virtual public int ImgHeight
		{
			get
			{
				return h;
			}
			
		}
		/// <summary> Returns the number of components in the image.
		/// 
		/// </summary>
		/// <returns> The number of components in the image.
		/// 
		/// </returns>
		virtual public int NumComps
		{
			get
			{
				return nc;
			}
			
		}
		/// <summary> Returns the index of the current tile, relative to a standard scan-line
		/// order. This default implementations assumes no tiling, so 0 is always
		/// returned.
		/// 
		/// </summary>
		/// <returns> The current tile's index (starts at 0).
		/// 
		/// </returns>
		virtual public int TileIdx
		{
			get
			{
				return 0;
			}
			
		}
		/// <summary>Returns the horizontal tile partition offset in the reference grid </summary>
		virtual public int TilePartULX
		{
			get
			{
				return 0;
			}
			
		}
		/// <summary>Returns the vertical tile partition offset in the reference grid </summary>
		virtual public int TilePartULY
		{
			get
			{
				return 0;
			}
			
		}
		/// <summary> Returns the horizontal coordinate of the image origin, the top-left
		/// corner, in the canvas system, on the reference grid.
		/// 
		/// </summary>
		/// <returns> The horizontal coordinate of the image origin in the canvas
		/// system, on the reference grid.
		/// 
		/// </returns>
		virtual public int ImgULX
		{
			get
			{
				return 0;
			}
			
		}
		/// <summary> Returns the vertical coordinate of the image origin, the top-left
		/// corner, in the canvas system, on the reference grid.
		/// 
		/// </summary>
		/// <returns> The vertical coordinate of the image origin in the canvas
		/// system, on the reference grid.
		/// 
		/// </returns>
		virtual public int ImgULY
		{
			get
			{
				return 0;
			}
			
		}
		
		/// <summary>The width of the image </summary>
		private int w;
		
		/// <summary>The height of the image </summary>
		private int h;
		
		/// <summary>The number of components in the image </summary>
		private int nc;
		
		/// <summary>The list of input ImgData </summary>
		private BlkImgDataSrc[] imageData;
		
		/// <summary>The component index associated with each ImgData </summary>
		private int[] compIdx;
		
		/// <summary>The subsampling factor along the horizontal direction, for every
		/// component 
		/// </summary>
		private int[] subsX;
		
		/// <summary>The subsampling factor along the vertical direction, for every
		/// component 
		/// </summary>
		private int[] subsY;
		
		/// <summary> Class constructor. Each input BlkImgDataSrc and its component index
		/// must appear in the order wanted for the output components.<br>
		/// 
		/// <u>Example:</u> Reading R,G,B components from 3 PGM files.<br>
		/// <tt>
		/// BlkImgDataSrc[] idList = <br>
		/// {<br>
		/// new ImgReaderPGM(new BEBufferedRandomAccessFile("R.pgm", "r")),<br>
		/// new ImgReaderPGM(new BEBufferedRandomAccessFile("G.pgm", "r")),<br>
		/// new ImgReaderPGM(new BEBufferedRandomAccessFile("B.pgm", "r"))<br>
		/// };<br>
		/// int[] compIdx = {0,0,0};<br>
		/// ImgDataJoiner idj = new ImgDataJoiner(idList, compIdx);
		/// </tt>
		/// 
		/// <p>Of course, the 2 arrays must have the same length (This length is
		/// the number of output components). The image width and height are
		/// definded to be the maximum values of all the input ImgData.
		/// 
		/// </summary>
		/// <param name="imD">The list of input BlkImgDataSrc in an array.
		/// 
		/// </param>
		/// <param name="cIdx">The component index associated with each ImgData.
		/// 
		/// </param>
		public ImgDataJoiner(BlkImgDataSrc[] imD, int[] cIdx)
		{
			int i;
			int maxW, maxH;
			
			// Initializes
			imageData = imD;
			compIdx = cIdx;
			if (imageData.Length != compIdx.Length)
				throw new System.ArgumentException("imD and cIdx must have the" + " same length");
			
			nc = imD.Length;
			
			subsX = new int[nc];
			subsY = new int[nc];
			
			// Check that no source is tiled and that the image origin is at the
			// canvas origin.
			for (i = 0; i < nc; i++)
			{
				if (imD[i].getNumTiles() != 1 || imD[i].getCompULX(cIdx[i]) != 0 || imD[i].getCompULY(cIdx[i]) != 0)
				{
					throw new System.ArgumentException("All input components must, " + "not use tiles and must " + "have " + "the origin at the canvas " + "origin");
				}
			}
			
			// Guess component subsampling factors based on the fact that the
			// ceil() operation relates the reference grid size to the component's
			// size, through the subsampling factor.
			
			// Mhhh, difficult problem. For now just assume that one of the
			// subsampling factors is always 1 and that the component width is
			// always larger than its subsampling factor, which covers most of the
			// cases. We check the correctness of the solution once found to chek
			// out hypothesis.
			
			// Look for max width and height.
			maxW = 0;
			maxH = 0;
			for (i = 0; i < nc; i++)
			{
				if (imD[i].getCompImgWidth(cIdx[i]) > maxW)
					maxW = imD[i].getCompImgWidth(cIdx[i]);
				if (imD[i].getCompImgHeight(cIdx[i]) > maxH)
					maxH = imD[i].getCompImgHeight(cIdx[i]);
			}
			// Set the image width and height as the maximum ones
			w = maxW;
			h = maxH;
			
			// Now get the sumsampling factors and check the subsampling factors,
			// just to see if above hypothesis were correct.
			for (i = 0; i < nc; i++)
			{
				// This calculation only holds if the subsampling factor is less
				// than the component width
				subsX[i] = (maxW + imD[i].getCompImgWidth(cIdx[i]) - 1) / imD[i].getCompImgWidth(cIdx[i]);
				subsY[i] = (maxH + imD[i].getCompImgHeight(cIdx[i]) - 1) / imD[i].getCompImgHeight(cIdx[i]);
				if ((maxW + subsX[i] - 1) / subsX[i] != imD[i].getCompImgWidth(cIdx[i]) || (maxH + subsY[i] - 1) / subsY[i] != imD[i].getCompImgHeight(cIdx[i]))
				{
					throw new System.ApplicationException("Can not compute component subsampling " + "factors: strange subsampling.");
				}
			}
		}
		
		/// <summary> Returns the component subsampling factor in the horizontal direction,
		/// for the specified component. This is, approximately, the ratio of
		/// dimensions between the reference grid and the component itself, see the
		/// 'ImgData' interface desription for details.
		/// 
		/// </summary>
		/// <param name="c">The index of the component (between 0 and N-1)
		/// 
		/// </param>
		/// <returns> The horizontal subsampling factor of component 'c'
		/// 
		/// </returns>
		/// <seealso cref="ImgData">
		/// 
		/// </seealso>
		public virtual int getCompSubsX(int c)
		{
			return subsX[c];
		}
		
		/// <summary> Returns the component subsampling factor in the vertical direction, for
		/// the specified component. This is, approximately, the ratio of
		/// dimensions between the reference grid and the component itself, see the
		/// 'ImgData' interface desription for details.
		/// 
		/// </summary>
		/// <param name="c">The index of the component (between 0 and N-1)
		/// 
		/// </param>
		/// <returns> The vertical subsampling factor of component 'c'
		/// 
		/// </returns>
		/// <seealso cref="ImgData">
		/// 
		/// </seealso>
		public virtual int getCompSubsY(int c)
		{
			return subsY[c];
		}
		
		
		/// <summary> Returns the width in pixels of the specified tile-component
		/// 
		/// </summary>
		/// <param name="t">Tile index
		/// 
		/// </param>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <returns> The width in pixels of component <tt>c</tt> in tile<tt>t</tt>.
		/// 
		/// </returns>
		public virtual int getTileCompWidth(int t, int c)
		{
			return imageData[c].getTileCompWidth(t, compIdx[c]);
		}
		
		/// <summary> Returns the height in pixels of the specified tile-component.
		/// 
		/// </summary>
		/// <param name="t">The tile index.
		/// 
		/// </param>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <returns> The height in pixels of component <tt>c</tt> in the current
		/// tile.
		/// 
		/// </returns>
		public virtual int getTileCompHeight(int t, int c)
		{
			return imageData[c].getTileCompHeight(t, compIdx[c]);
		}
		
		/// <summary> Returns the width in pixels of the specified component in the overall
		/// image.
		/// 
		/// </summary>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <returns> The width in pixels of component <tt>c</tt> in the overall
		/// image.
		/// 
		/// </returns>
		public virtual int getCompImgWidth(int c)
		{
			return imageData[c].getCompImgWidth(compIdx[c]);
		}
		
		/// <summary> Returns the height in pixels of the specified component in the
		/// overall image.
		/// 
		/// </summary>
		/// <param name="n">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <returns> The height in pixels of component <tt>n</tt> in the overall
		/// image.
		/// 
		/// 
		/// 
		/// </returns>
		public virtual int getCompImgHeight(int n)
		{
			return imageData[n].getCompImgHeight(compIdx[n]);
		}
		
		/// <summary> Returns the number of bits, referred to as the "range bits",
		/// corresponding to the nominal range of the data in the specified
		/// component. If this number is <i>b</b> then for unsigned data the
		/// nominal range is between 0 and 2^b-1, and for signed data it is between
		/// -2^(b-1) and 2^(b-1)-1. For floating point data this value is not
		/// applicable.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The number of bits corresponding to the nominal range of the
		/// data. Fro floating-point data this value is not applicable and the
		/// return value is undefined.
		/// 
		/// </returns>
		public virtual int getNomRangeBits(int c)
		{
			return imageData[c].getNomRangeBits(compIdx[c]);
		}
		
		/// <summary> Returns the position of the fixed point in the specified
		/// component. This is the position of the least significant integral
		/// (i.e. non-fractional) bit, which is equivalent to the number of
		/// fractional bits. For instance, for fixed-point values with 2 fractional
		/// bits, 2 is returned. For floating-point data this value does not apply
		/// and 0 should be returned. Position 0 is the position of the least
		/// significant bit in the data.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The position of the fixed-point, which is the same as the
		/// number of fractional bits. For floating-point data 0 is returned.
		/// 
		/// </returns>
		public virtual int getFixedPoint(int c)
		{
			return imageData[c].getFixedPoint(compIdx[c]);
		}
		
		/// <summary> Returns, in the blk argument, a block of image data containing the
		/// specifed rectangular area, in the specified component. The data is
		/// returned, as a reference to the internal data, if any, instead of as a
		/// copy, therefore the returned data should not be modified.
		/// 
		/// <P>The rectangular area to return is specified by the 'ulx', 'uly', 'w'
		/// and 'h' members of the 'blk' argument, relative to the current
		/// tile. These members are not modified by this method. The 'offset' and
		/// 'scanw' of the returned data can be arbitrary. See the 'DataBlk' class.
		/// 
		/// <P>This method, in general, is more efficient than the 'getCompData()'
		/// method since it may not copy the data. However if the array of returned
		/// data is to be modified by the caller then the other method is probably
		/// preferable.
		/// 
		/// <P>If the data array in <tt>blk</tt> is <tt>null</tt>, then a new one
		/// is created if necessary. The implementation of this interface may
		/// choose to return the same array or a new one, depending on what is more
		/// efficient. Therefore, the data array in <tt>blk</tt> prior to the
		/// method call should not be considered to contain the returned data, a
		/// new array may have been created. Instead, get the array from
		/// <tt>blk</tt> after the method has returned.
		/// 
		/// <P>The returned data may have its 'progressive' attribute set. In this
		/// case the returned data is only an approximation of the "final" data.
		/// 
		/// </summary>
		/// <param name="blk">Its coordinates and dimensions specify the area to return,
		/// relative to the current tile. Some fields in this object are modified
		/// to return the data.
		/// 
		/// </param>
		/// <param name="c">The index of the component from which to get the data.
		/// 
		/// </param>
		/// <returns> The requested DataBlk
		/// 
		/// </returns>
		/// <seealso cref="getCompData">
		/// 
		/// </seealso>
		public virtual DataBlk getInternCompData(DataBlk blk, int c)
		{
			return imageData[c].getInternCompData(blk, compIdx[c]);
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
		/// <P>This method, in general, is less efficient than the
		/// 'getInternCompData()' method since, in general, it copies the
		/// data. However if the array of returned data is to be modified by the
		/// caller then this method is preferable.
		/// 
		/// <P>If the data array in 'blk' is 'null', then a new one is created. If
		/// the data array is not 'null' then it is reused, and it must be large
		/// enough to contain the block's data. Otherwise an 'ArrayStoreException'
		/// or an 'IndexOutOfBoundsException' is thrown by the Java system.
		/// 
		/// <P>The returned data may have its 'progressive' attribute set. In this
		/// case the returned data is only an approximation of the "final" data.
		/// 
		/// </summary>
		/// <param name="blk">Its coordinates and dimensions specify the area to return,
		/// relative to the current tile. If it contains a non-null data array,
		/// then it must be large enough. If it contains a null data array a new
		/// one is created. Some fields in this object are modified to return the
		/// data.
		/// 
		/// </param>
		/// <param name="c">The index of the component from which to get the data.
		/// 
		/// </param>
		/// <returns> The requested DataBlk
		/// 
		/// </returns>
		/// <seealso cref="getInternCompData">
		/// 
		/// </seealso>
		public virtual DataBlk getCompData(DataBlk blk, int c)
		{
			return imageData[c].getCompData(blk, compIdx[c]);
		}
		
		/// <summary> Changes the current tile, given the new coordinates. An
		/// IllegalArgumentException is thrown if the coordinates do not correspond
		/// to a valid tile.
		/// 
		/// </summary>
		/// <param name="x">The horizontal coordinate of the tile.
		/// 
		/// </param>
		/// <param name="y">The vertical coordinate of the new tile.
		/// 
		/// </param>
		public virtual void  setTile(int x, int y)
		{
			if (x != 0 || y != 0)
			{
				throw new System.ArgumentException();
			}
		}
		
		/// <summary> Advances to the next tile, in standard scan-line order (by rows then
		/// columns). A NoNextElementException is thrown if the current tile is the
		/// last one (i.e. there is no next tile). This default implementation
		/// assumes no tiling, so NoNextElementException() is always thrown.
		/// 
		/// </summary>
		public virtual void  nextTile()
		{
			throw new NoNextElementException();
		}
		
		/// <summary> Returns the coordinates of the current tile. This default
		/// implementation assumes no-tiling, so (0,0) is returned.
		/// 
		/// </summary>
		/// <param name="co">If not null this object is used to return the information. If
		/// null a new one is created and returned.
		/// 
		/// </param>
		/// <returns> The current tile's coordinates.
		/// 
		/// </returns>
		public virtual Coord getTile(Coord co)
		{
			if (co != null)
			{
				co.x = 0;
				co.y = 0;
				return co;
			}
			else
			{
				return new Coord(0, 0);
			}
		}
		
		/// <summary> Returns the horizontal coordinate of the upper-left corner of the
		/// specified component in the current tile.
		/// 
		/// </summary>
		/// <param name="c">The component index.
		/// 
		/// </param>
		public virtual int getCompULX(int c)
		{
			return 0;
		}
		
		/// <summary> Returns the vertical coordinate of the upper-left corner of the
		/// specified component in the current tile.
		/// 
		/// </summary>
		/// <param name="c">The component index.
		/// 
		/// </param>
		public virtual int getCompULY(int c)
		{
			return 0;
		}
		
		/// <summary> Returns the number of tiles in the horizontal and vertical
		/// directions. This default implementation assumes no tiling, so (1,1) is
		/// always returned.
		/// 
		/// </summary>
		/// <param name="co">If not null this object is used to return the information. If
		/// null a new one is created and returned.
		/// 
		/// </param>
		/// <returns> The number of tiles in the horizontal (Coord.x) and vertical
		/// (Coord.y) directions.
		/// 
		/// </returns>
		public virtual Coord getNumTiles(Coord co)
		{
			if (co != null)
			{
				co.x = 1;
				co.y = 1;
				return co;
			}
			else
			{
				return new Coord(1, 1);
			}
		}
		
		/// <summary> Returns the total number of tiles in the image. This default
		/// implementation assumes no tiling, so 1 is always returned.
		/// 
		/// </summary>
		/// <returns> The total number of tiles in the image.
		/// 
		/// </returns>
		public virtual int getNumTiles()
		{
			return 1;
		}
		
		/// <summary> Returns a string of information about the object, more than 1 line
		/// long. The information string includes information from the several
		/// input ImgData (their toString() method are called one after the other).
		/// 
		/// </summary>
		/// <returns> A string of information about the object.
		/// 
		/// </returns>
		public override System.String ToString()
		{
			System.String string_Renamed = "ImgDataJoiner: WxH = " + w + "x" + h;
			for (int i = 0; i < nc; i++)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				string_Renamed += ("\n- Component " + i + " " + imageData[i]);
			}
			return string_Renamed;
		}
	}
}