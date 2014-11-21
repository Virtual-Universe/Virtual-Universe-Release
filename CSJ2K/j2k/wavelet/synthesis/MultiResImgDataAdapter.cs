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
	
	/// <summary> This class provides a default implementation for the methods of the
	/// 'MultiResImgData' interface. The default implementation consists just in
	/// returning the value of the source, where the source is another
	/// 'MultiResImgData' object.
	/// 
	/// <p>This abstract class can be used to facilitate the development of other
	/// classes that implement 'MultiResImgData'. For example a dequantizer can
	/// inherit from this class and all the trivial methods do not have to be
	/// reimplemented.</p>
	/// 
	/// <p>If the default implementation of a method provided in this class does
	/// not suit a particular implementation of the 'MultiResImgData' interface,
	/// the method can be overriden to implement the proper behaviour.</p>
	/// 
	/// </summary>
	/// <seealso cref="MultiResImgData">
	/// 
	/// </seealso>
	public abstract class MultiResImgDataAdapter : MultiResImgData
	{
		/// <summary>Returns the nominal tiles width </summary>
		virtual public int NomTileWidth
		{
			get
			{
				return mressrc.NomTileWidth;
			}
			
		}
		/// <summary>Returns the nominal tiles height </summary>
		virtual public int NomTileHeight
		{
			get
			{
				return mressrc.NomTileHeight;
			}
			
		}
		/// <summary> Returns the number of components in the image.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <returns> The number of components in the image.
		/// 
		/// </returns>
		virtual public int NumComps
		{
			get
			{
				return mressrc.NumComps;
			}
			
		}
		/// <summary> Returns the index of the current tile, relative to a standard scan-line
		/// order.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <returns> The current tile's index (starts at 0).
		/// 
		/// </returns>
		virtual public int TileIdx
		{
			get
			{
				return mressrc.TileIdx;
			}
			
		}
		/// <summary>Returns the horizontal tile partition offset in the reference grid </summary>
		virtual public int TilePartULX
		{
			get
			{
				return mressrc.TilePartULX;
			}
			
		}
		/// <summary>Returns the vertical tile partition offset in the reference grid </summary>
		virtual public int TilePartULY
		{
			get
			{
				return mressrc.TilePartULY;
			}
			
		}
		
		/// <summary>Index of the current tile </summary>
		protected internal int tIdx = 0;
		
		/// <summary>The MultiResImgData source </summary>
		protected internal MultiResImgData mressrc;
		
		/// <summary> Instantiates the MultiResImgDataAdapter object specifying the
		/// MultiResImgData source.
		/// 
		/// </summary>
		/// <param name="src">From where to obrtain the MultiResImgData values.
		/// 
		/// </param>
		protected internal MultiResImgDataAdapter(MultiResImgData src)
		{
			mressrc = src;
		}
		
		/// <summary> Returns the overall width of the current tile in pixels, for the given
		/// resolution level. This is the tile's width without accounting for any
		/// component subsampling.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The total current tile's width in pixels.
		/// 
		/// </returns>
		public virtual int getTileWidth(int rl)
		{
			return mressrc.getTileWidth(rl);
		}
		
		/// <summary> Returns the overall height of the current tile in pixels, for the given
		/// resolution level. This is the tile's height without accounting for any
		/// component subsampling.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The total current tile's height in pixels.
		/// 
		/// </returns>
		public virtual int getTileHeight(int rl)
		{
			return mressrc.getTileHeight(rl);
		}
		
		/// <summary> Returns the overall width of the image in pixels, for the given
		/// resolution level. This is the image's width without accounting for any
		/// component subsampling or tiling.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The total image's width in pixels.
		/// 
		/// </returns>
		public virtual int getImgWidth(int rl)
		{
			return mressrc.getImgWidth(rl);
		}
		
		/// <summary> Returns the overall height of the image in pixels, for the given
		/// resolution level. This is the image's height without accounting for any
		/// component subsampling or tiling.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The total image's height in pixels.
		/// 
		/// </returns>
		public virtual int getImgHeight(int rl)
		{
			return mressrc.getImgHeight(rl);
		}
		
		/// <summary> Returns the component subsampling factor in the horizontal direction,
		/// for the specified component. This is, approximately, the ratio of
		/// dimensions between the reference grid and the component itself, see the
		/// 'ImgData' interface desription for details.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
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
		public virtual int getCompSubsX(int c)
		{
			return mressrc.getCompSubsX(c);
		}
		
		/// <summary> Returns the component subsampling factor in the vertical direction, for
		/// the specified component. This is, approximately, the ratio of
		/// dimensions between the reference grid and the component itself, see the
		/// 'ImgData' interface desription for details.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
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
		public virtual int getCompSubsY(int c)
		{
			return mressrc.getCompSubsY(c);
		}
		
		/// <summary> Returns the width in pixels of the specified tile-component for the
		/// given resolution level.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="t">Tile index.
		/// 
		/// </param>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The width in pixels of component <tt>c</tt> in tile <tt>t</tt>
		/// for resolution level <tt>rl</tt>.
		/// 
		/// </returns>
		public virtual int getTileCompWidth(int t, int c, int rl)
		{
			return mressrc.getTileCompWidth(t, c, rl);
		}
		
		/// <summary> Returns the height in pixels of the specified tile-component for the
		/// given resolution level.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
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
		public virtual int getTileCompHeight(int t, int c, int rl)
		{
			return mressrc.getTileCompHeight(t, c, rl);
		}
		
		/// <summary> Returns the width in pixels of the specified component in the overall
		/// image, for the given resolution level.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
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
		public virtual int getCompImgWidth(int c, int rl)
		{
			return mressrc.getCompImgWidth(c, rl);
		}
		
		/// <summary> Returns the height in pixels of the specified component in the overall
		/// image, for the given resolution level.
		/// 
		/// <P>This default implementation returns the value of the source.
		/// 
		/// </summary>
		/// <param name="c">The index of the component, from 0 to N-1.
		/// 
		/// </param>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The height in pixels of component <tt>c</tt> in the overall
		/// image.
		/// 
		/// </returns>
		public virtual int getCompImgHeight(int c, int rl)
		{
			return mressrc.getCompImgHeight(c, rl);
		}
		
		/// <summary> Changes the current tile, given the new indexes. An
		/// IllegalArgumentException is thrown if the indexes do not correspond to
		/// a valid tile.
		/// 
		/// <p>This default implementation just changes the tile in the source.</p>
		/// 
		/// </summary>
		/// <param name="x">The horizontal indexes the tile.
		/// 
		/// </param>
		/// <param name="y">The vertical indexes of the new tile.
		/// 
		/// </param>
		public virtual void  setTile(int x, int y)
		{
			mressrc.setTile(x, y);
			tIdx = TileIdx;
		}
		
		/// <summary> Advances to the next tile, in standard scan-line order (by rows then
		/// columns). An NoNextElementException is thrown if the current tile is
		/// the last one (i.e. there is no next tile).
		/// 
		/// <p>This default implementation just changes the tile in the source.</p>
		/// 
		/// </summary>
		public virtual void  nextTile()
		{
			mressrc.nextTile();
			tIdx = TileIdx;
		}
		
		/// <summary> Returns the indexes of the current tile. These are the horizontal and
		/// vertical indexes of the current tile.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="co">If not null this object is used to return the information. If
		/// null a new one is created and returned.
		/// 
		/// </param>
		/// <returns> The current tile's indexes (vertical and horizontal indexes).
		/// 
		/// </returns>
		public virtual Coord getTile(Coord co)
		{
			return mressrc.getTile(co);
		}
		
		/// <summary> Returns the horizontal coordinate of the upper-left corner of the
		/// specified resolution level in the given component of the current tile. 
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="c">The component index.
		/// 
		/// </param>
		/// <param name="rl">The resolution level index.
		/// 
		/// </param>
		public virtual int getResULX(int c, int rl)
		{
			return mressrc.getResULX(c, rl);
		}
		
		/// <summary> Returns the vertical coordinate of the upper-left corner of the
		/// specified resolution in the given component of the current tile. 
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="c">The component index.
		/// 
		/// </param>
		/// <param name="rl">The resolution level index.
		/// 
		/// </param>
		public virtual int getResULY(int c, int rl)
		{
			return mressrc.getResULY(c, rl);
		}
		
		/// <summary> Returns the horizontal coordinate of the image origin, the top-left
		/// corner, in the canvas system, on the reference grid at the specified
		/// resolution level.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The horizontal coordinate of the image origin in the canvas
		/// system, on the reference grid.
		/// 
		/// </returns>
		public virtual int getImgULX(int rl)
		{
			return mressrc.getImgULX(rl);
		}
		
		/// <summary> Returns the vertical coordinate of the image origin, the top-left
		/// corner, in the canvas system, on the reference grid at the specified
		/// resolution level.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <param name="rl">The resolution level, from 0 to L.
		/// 
		/// </param>
		/// <returns> The vertical coordinate of the image origin in the canvas
		/// system, on the reference grid.
		/// 
		/// </returns>
		public virtual int getImgULY(int rl)
		{
			return mressrc.getImgULY(rl);
		}
		
		/// <summary> Returns the number of tiles in the horizontal and vertical directions.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
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
			return mressrc.getNumTiles(co);
		}
		
		/// <summary> Returns the total number of tiles in the image.
		/// 
		/// <p>This default implementation returns the value of the source.</p>
		/// 
		/// </summary>
		/// <returns> The total number of tiles in the image.
		/// 
		/// </returns>
		public virtual int getNumTiles()
		{
			return mressrc.getNumTiles();
		}
		public abstract CSJ2K.j2k.wavelet.synthesis.SubbandSyn getSynSubbandTree(int param1, int param2);
	}
}