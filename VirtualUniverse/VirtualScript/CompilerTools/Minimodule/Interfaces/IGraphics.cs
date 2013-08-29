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
    public interface IGraphics
    {
        UUID SaveBitmap(Bitmap data);
        UUID SaveBitmap(Bitmap data, bool lossless, bool temporary);
        Bitmap LoadBitmap(UUID assetID);
    }
}