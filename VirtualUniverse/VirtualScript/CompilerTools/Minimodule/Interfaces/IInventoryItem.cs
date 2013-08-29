/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using OpenMetaverse;
using OpenMetaverse.Assets;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    /// <summary>
    ///     This implements the methods needed to operate on individual inventory items.
    /// </summary>
    public interface IInventoryItem
    {
        int Type { get; }
        UUID AssetID { get; }
        T RetrieveAsset<T>() where T : Asset, new();
    }
}