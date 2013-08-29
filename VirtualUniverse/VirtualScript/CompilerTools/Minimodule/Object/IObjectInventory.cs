/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System.Collections.Generic;
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    /// <summary>
    ///     This implements the methods neccesary to operate on the inventory of an object
    /// </summary>
    public interface IObjectInventory : IDictionary<UUID, IInventoryItem>
    {
        IInventoryItem this[string name] { get; }
    }
}