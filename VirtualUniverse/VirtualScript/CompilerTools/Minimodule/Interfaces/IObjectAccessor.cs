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
    public interface IObjectAccessor : ICollection<IObject>
    {
        IObject this[int index] { get; }
        IObject this[uint index] { get; }
        IObject this[UUID index] { get; }
        IObject Create(Vector3 position);
        IObject Create(Vector3 position, Quaternion rotation);
    }
}