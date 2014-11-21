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
namespace CSJ2K.j2k.image
{
	
	/// <summary> This interface defines methods to access image attributes (width, height,
	/// number of components, etc.). The image can be tiled or not (i.e. if the
	/// image is not tiled then there is only 1 tile). It should be implemented by
	/// all classes that provide image data, such as image file readers, color
	/// transforms, wavelet transforms, etc. This interface, however, does not
	/// define methods to transfer image data (i.e. pixel data), that is defined by
	/// other interfaces, such as 'BlkImgDataSrc'.
	/// 
	/// </summary>
	/// <seealso cref="BlkImgDataSrc">
	/// 
	/// </seealso>
	public interface ImgData
	{
		/// <summary> Returns the overall width of the current tile in pixels. This is the
		/// tile's width without accounting for any component subsampling. This is
		/// also referred as the reference grid width in the current tile.
		/// 
		/// </summary>
		/// <returns> The total current tile's width in pixels.
		/// 
		/// </returns>
		int TileWidth
		{
			get;
			
		}
		/// <summary> Returns the overall height of the current tile in pixels. This is the
		/// tile's height without accounting for any component subsampling. This is
		/// also referred as the reference grid height in the current tile.
		/// 
		/// </summary>
		/// <returns> The total current tile's height in pixels.
		/// 
		/// </returns>
		int TileHeight
		{
			get;
			
		}
		/// <summary>Returns the nominal tiles width </summary>
		int NomTileWidth
		{
			get;
			
		}
		/// <summary>Returns the nominal tiles height </summary>
		int NomTileHeight
		{
			get;
			
		}
		/// <summary> Returns the overall width of the image in pixels. This is the image's
		/// width without accounting for any component subsampling or tiling.
		/// 
		/// </summary>
		/// <returns> The total image's width in pixels.
		/// 
		/// </returns>
		int ImgWidth
		{
			get;
			
		}
		/// <summary> Returns the overall height of the image in pixels. This is the image's
		/// height without accounting for any component subsampling or tiling.
		/// 
		/// </summary>
		/// <returns> The total image's height in pixels.
		/// 
		/// </returns>
		int ImgHeight
		{
			get;
			
		}
		/// <summary> Returns the number of components in the image.
		/// 
		/// </summary>
		/// <returns> The number of components in the image.
		/// 
		/// </returns>
		int NumComps
		{
			get;
			
		}
		/// <summary> Returns the index of the current tile, relative to a standard scan-line
		/// order.
		/// 
		/// </summary>
		/// <returns> The current tile's index (starts at 0).
		/// 
		/// </returns>
		int TileIdx
		{
			get;
			
		}
		/// <summary>Returns the horizontal tile partition offset in the reference grid </summary>
		int TilePartULX
		{
			get;
			
		}
		/// <summary>Returns the vertical tile partition offset in the reference grid </summary>
		int TilePartULY
		{
			get;
			
		}
		/// <summary> Returns the horizontal coordinate of the image origin, the top-left
		/// corner, in the canvas system, on the reference grid.
		/// 
		/// </summary>
		/// <returns> The horizontal coordinate of the image origin in the canvas
		/// system, on the reference grid.
		/// 
		/// </returns>
		int ImgULX
		{
			get;
			
		}
		/// <summary> Returns the vertical coordinate of the image origin, the top-left
		/// corner, in the canvas system, on the reference grid.
		/// 
		/// </summary>
		/// <returns> The vertical coordinate of the image origin in the canvas
		/// system, on the reference grid.
		/// 
		/// </returns>
		int ImgULY
		{
			get;
			
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
		int getCompSubsX(int c);
		
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
		int getCompSubsY(int c);
		
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
		int getTileCompWidth(int t, int c);
		
		/// <summary> Returns the height in pixels of the specified tile-component.
		/// 
		/// </summary>
		/// <param name="t">The tile index.
		/// 
		/// </param>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <returns> The height in pixels of component <tt>c</tt> in tile
		/// <tt>t</tt>.
		/// 
		/// </returns>
		int getTileCompHeight(int t, int c);
		
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
		int getCompImgWidth(int c);
		
		/// <summary> Returns the height in pixels of the specified component in the overall
		/// image.
		/// 
		/// </summary>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <returns> The height in pixels of component <tt>n</tt> in the overall
		/// image.
		/// 
		/// </returns>
		int getCompImgHeight(int c);
		
		/// <summary> Returns the number of bits, referred to as the "range bits",
		/// corresponding to the nominal range of the image data in the specified
		/// component. If this number is <i>n</b> then for unsigned data the
		/// nominal range is between 0 and 2^b-1, and for signed data it is between
		/// -2^(b-1) and 2^(b-1)-1. In the case of transformed data which is not in
		/// the image domain (e.g., wavelet coefficients), this method returns the
		/// "range bits" of the image data that generated the coefficients.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		/// <returns> The number of bits corresponding to the nominal range of the
		/// image data (in the image domain).
		/// 
		/// </returns>
		int getNomRangeBits(int c);
		
		/// <summary> Changes the current tile, given the new indices. An
		/// IllegalArgumentException is thrown if the coordinates do not correspond
		/// to a valid tile.
		/// 
		/// </summary>
		/// <param name="x">The horizontal index of the tile.
		/// 
		/// </param>
		/// <param name="y">The vertical index of the new tile.
		/// 
		/// </param>
		void  setTile(int x, int y);
		
		/// <summary> Advances to the next tile, in standard scan-line order (by rows then
		/// columns). An NoNextElementException is thrown if the current tile is
		/// the last one (i.e. there is no next tile).
		/// 
		/// </summary>
		void  nextTile();
		
		/// <summary> Returns the indixes of the current tile. These are the horizontal and
		/// vertical indexes of the current tile.
		/// 
		/// </summary>
		/// <param name="co">If not null this object is used to return the information. If
		/// null a new one is created and returned.
		/// 
		/// </param>
		/// <returns> The current tile's indices (vertical and horizontal indexes).
		/// 
		/// </returns>
		Coord getTile(Coord co);
		
		/// <summary> Returns the horizontal coordinate of the upper-left corner of the
		/// specified component in the current tile.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		int getCompULX(int c);
		
		/// <summary> Returns the vertical coordinate of the upper-left corner of the
		/// specified component in the current tile.
		/// 
		/// </summary>
		/// <param name="c">The index of the component.
		/// 
		/// </param>
		int getCompULY(int c);
		
		/// <summary> Returns the number of tiles in the horizontal and vertical directions.
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
		Coord getNumTiles(Coord co);
		
		/// <summary> Returns the total number of tiles in the image.
		/// 
		/// </summary>
		/// <returns> The total number of tiles in the image.
		/// 
		/// </returns>
		int getNumTiles();
	}
}