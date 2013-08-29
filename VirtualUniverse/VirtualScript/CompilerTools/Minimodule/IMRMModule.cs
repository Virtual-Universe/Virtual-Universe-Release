/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public interface IMRMModule
    {
        void RegisterExtension<T>(T instance);
        void InitializeMRM(MRMBase mmb, uint localID, UUID itemID);
        void GetGlobalEnvironment(uint localID, out IWorld world, out IHost host);
    }
}