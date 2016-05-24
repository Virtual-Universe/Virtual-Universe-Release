/*
 * Copyright (c) Contributors, http://virtual-planets.org/, http://whitecore-sim.org/, http://aurora-sim.org, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual Universe Project nor the
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
 */

using System;
using Universe.Framework.SceneInfo;

namespace Universe.Framework.Modules
{
    public interface ITerrainChannel
    {
        int Height { get; }
        float this[int x, int y] { get; set; }
        int Width { get; }
        IScene Scene { get; set; }

        /// <summary>
        ///     Squash the entire heightmap into a single dimensioned array
        /// </summary>
        /// <returns></returns>
        short[] GetSerialized();

        bool Tainted(int x, int y);
        ITerrainChannel MakeCopy();

        /// <summary>
        ///     Gets the average height of the area +2 in both the X and Y directions from the given position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        float GetNormalizedGroundHeight(int x, int y);

        /// <summary>
        /// Gets the average height of land above the waterline at the specified point.
        /// </summary>
        /// <returns>The normalized land height.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        float GetNormalizedLandHeight (int x, int y);

        /// <summary>
        /// Generates  new terrain based upon supplied parameters.
        /// </summary>
        /// <param name="landType">Land type.</param>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Max.</param>
        /// <param name="smoothing">Smoothing.</param>
        /// <param name="scene">Scene.</param>
        void GenerateTerrain(string terrainType, float min, float max, int smoothing, IScene scene);

        /// <summary>
        /// Recalculates land area.
        /// </summary>
        void ReCalcLandArea ();

    }
}