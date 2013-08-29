/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public interface IHeightmap
    {
        /// <summary>
        ///     Returns [or sets] the heightmap value at specified coordinates.
        /// </summary>
        /// <param name="x">X Coordinate</param>
        /// <param name="y">Y Coordinate</param>
        /// <returns>A value in meters representing height. Can be negative. Value correlates with Z parameter in world coordinates</returns>
        /// <example>
        ///     double heightVal = World.Heightmap[128,128];
        ///     World.Heightmap[128,128] *= 5.0;
        ///     World.Heightmap[128,128] = 25;
        /// </example>
        float this[int x, int y] { get; set; }

        /// <summary>
        ///     The maximum length of the region (Y axis), exclusive. (eg Height = 256, max Y = 255). Minimum is always 0 inclusive.
        /// </summary>
        /// <example>
        ///     Host.Console.Info("The terrain length of this region is " + World.Heightmap.Length);
        /// </example>
        int Length { get; }

        /// <summary>
        ///     The maximum width of the region (X axis), exclusive. (eg Width = 256, max X = 255). Minimum is always 0 inclusive.
        /// </summary>
        /// <example>
        ///     Host.Console.Info("The terrain width of this region is " + World.Heightmap.Width);
        /// </example>
        int Width { get; }
    }
}