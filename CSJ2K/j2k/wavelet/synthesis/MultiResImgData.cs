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
namespace CSJ2K.j2k.wavelet.synthesis
{
	
	/// <summary> This interface defines methods to access image attributes (width, height,
	/// number of components, etc.) of multiresolution images, such as those
	/// resulting from an inverse wavelet transform. The image can be tiled or not
	/// (i.e. if the image is not tiled then there is only 1 tile). It should be
	/// implemented by all classes that provide multi-resolution image data, such
	/// as entropy decoders, dequantizers, etc. This interface, however, does not
	/// define methods to transfer image data (i.e. pixel data), that is defined by
	/// other interfaces, such as 'CBlkQuantDataSrcDec'.
	/// 
	/// <p>This interface is very similar to the 'ImgData' one. It differs only by
	/// the fact that it handles multiple resolutions.</p>
	/// 
	/// <p>Resolution levels are counted from 0 to L. Resolution level 0 is the
	/// lower resolution, while L is the maximum resolution level, or full
	/// resolution, which is returned by 'getMaxResLvl()'. Note that there are L+1
	/// resolution levels available.</p>
	/// 
	/// <p>As in the 'ImgData' interface a multi-resolution image lies on top of a
	/// canvas. The canvas coordinates are mapped from the full resolution
	/// reference grid (i.e. resolution level 'L' reference grid) to a resolution
	/// level 'l' reference grid by '(x_l,y_l) =
	/// (ceil(x_l/2^(L-l)),ceil(y_l/2^(L-l)))', where '(x,y)' are the full
	/// resolution reference grid coordinates and '(x_l,y_l)' are the level 'l'
	/// reference grid coordinates.</p>
	/// 
	/// <p>For details on the canvas system and its implications consult the
	/// 'ImgData' interface.</p>
	/// 
	/// <p>Note that tile sizes may not be obtained by simply dividing the tile
	/// size in the reference grid by the subsampling factor.</p>
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.image.ImgData">
	/// 
	/// </seealso>
	/// <seealso cref="jj2000.j2k.quantization.dequantizer.CBlkQuantDataSrcDec">
	/// 
	/// </seealso>
	public interface MultiResImgData
	{
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
		
		/// <summary> Returns the overall width of the current tile in pixels for the given
		/// resolution level. This is the tile's width without accounting for any
		/// component subsampling. The resolution level is indexed from the lowest
		/// number of resolution levels of all components of the current tile.
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The total current tile's width in pixels.
		/// 
		/// </returns>
		int getTileWidth(int rl);
		
		/// <summary> Returns the overall height of the current tile in pixels, for the given
		/// resolution level. This is the tile's height without accounting for any
		/// component subsampling. The resolution level is indexed from the lowest
		/// number of resolution levels of all components of the current tile.
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The total current tile's height in pixels.
		/// 
		/// </returns>
		int getTileHeight(int rl);
		
		/// <summary> Returns the overall width of the image in pixels, for the given
		/// resolution level. This is the image's width without accounting for any
		/// component subsampling or tiling. The resolution level is indexed from
		/// the lowest number of resolution levels of all components of the current
		/// tile.
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The total image's width in pixels.
		/// 
		/// </returns>
		int getImgWidth(int rl);
		
		/// <summary> Returns the overall height of the image in pixels, for the given
		/// resolution level. This is the image's height without accounting for any
		/// component subsampling or tiling. The resolution level is indexed from
		/// the lowest number of resolution levels of all components of the current
		/// tile.
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The total image's height in pixels.
		/// 
		/// </returns>
		int getImgHeight(int rl);
		
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
		/// <seealso cref="jj2000.j2k.image.ImgData">
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
		/// <seealso cref="jj2000.j2k.image.ImgData">
		/// 
		/// </seealso>
		int getCompSubsY(int c);
		
		/// <summary> Returns the width in pixels of the specified tile-component for the
		/// given resolution level.
		/// 
		/// </summary>
		/// <param name="t">Tile index
		/// 
		/// </param>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The width in pixels of component <tt>c</tt> in tile <tt>t</tt>
		/// for resolution <tt>rl</tt>.
		/// 
		/// </returns>
		int getTileCompWidth(int t, int c, int rl);
		
		/// <summary> Returns the height in pixels of the specified tile-component for the
		/// given resolution level.
		/// 
		/// </summary>
		/// <param name="t">The tile index.
		/// 
		/// </param>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The height in pixels of component <tt>c</tt> in tile
		/// <tt>t</tt>.
		/// 
		/// </returns>
		int getTileCompHeight(int t, int c, int rl);
		
		/// <summary> Returns the width in pixels of the specified component in the overall
		/// image, for the given resolution level.
		/// 
		/// </summary>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The width in pixels of component <tt>c</tt> in the overall
		/// image.
		/// 
		/// </returns>
		int getCompImgWidth(int c, int rl);
		
		/// <summary> Returns the height in pixels of the specified component in the overall
		/// image, for the given resolution level.
		/// 
		/// </summary>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The height in pixels of component <tt>n</tt> in the overall
		/// image.
		/// 
		/// </returns>
		int getCompImgHeight(int n, int rl);
		
		/// <summary> Changes the current tile, given the new indexes. An
		/// IllegalArgumentException is thrown if the indexes do not correspond to
		/// a valid tile.
		/// 
		/// </summary>
		/// <param name="x">The horizontal indexes the tile.
		/// 
		/// </param>
		/// <param name="y">The vertical indexes of the new tile.
		/// 
		/// </param>
		void  setTile(int x, int y);
		
		/// <summary> Advances to the next tile, in standard scan-line order (by rows then
		/// columns). An NoNextElementException is thrown if the current tile is
		/// the last one (i.e. there is no next tile).
		/// 
		/// </summary>
		void  nextTile();
		
		/// <summary> Returns the indexes of the current tile. These are the horizontal and
		/// vertical indexes of the current tile.
		/// 
		/// </summary>
		/// <param name="co">If not null this object is used to return the information. If
		/// null a new one is created and returned.
		/// 
		/// </param>
		/// <returns> The current tile's indexes (vertical and horizontal indexes).
		/// 
		/// </returns>
		Coord getTile(Coord co);
		
		/// <summary> Returns the horizontal coordinate of the upper-left corner of the
		/// specified resolution in the given component of the current tile.
		/// 
		/// </summary>
		/// <param name="c">The component index.
		/// 
		/// </param>
		/// <param name="rl">The resolution level index.
		/// 
		/// </param>
		int getResULX(int c, int rl);
		
		/// <summary> Returns the vertical coordinate of the upper-left corner of the
		/// specified resolution in the given component of the current tile.
		/// 
		/// </summary>
		/// <param name="c">The component index.
		/// 
		/// </param>
		/// <param name="rl">The resolution level index.
		/// 
		/// </param>
		int getResULY(int c, int rl);
		
		/// <summary> Returns the horizontal coordinate of the image origin, the top-left
		/// corner, in the canvas system, on the reference grid at the specified
		/// resolution level.  The resolution level is indexed from the lowest
		/// number of resolution levels of all components of the current tile.
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The horizontal coordinate of the image origin in the canvas
		/// system, on the reference grid.
		/// 
		/// </returns>
		int getImgULX(int rl);
		
		/// <summary> Returns the vertical coordinate of the image origin, the top-left
		/// corner, in the canvas system, on the reference grid at the specified
		/// resolution level.  The resolution level is indexed from the lowest
		/// number of resolution levels of all components of the current tile.
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The vertical coordinate of the image origin in the canvas
		/// system, on the reference grid.
		/// 
		/// </returns>
		int getImgULY(int rl);
		
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
		
		/// <summary> Returns the specified synthesis subband tree 
		/// 
		/// </summary>
		/// <param name="t">Tile index.
		/// 
		/// </param>
		/// <param name="c">Component index.
		/// 
		/// </param>
		SubbandSyn getSynSubbandTree(int t, int c);
	}
}