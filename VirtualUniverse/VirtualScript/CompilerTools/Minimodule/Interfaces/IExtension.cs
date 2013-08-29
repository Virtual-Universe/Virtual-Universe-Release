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
    public interface IExtension
    {
        T Get<T>();
        bool TryGet<T>(out T extension);
        bool Has<T>();
    }
}