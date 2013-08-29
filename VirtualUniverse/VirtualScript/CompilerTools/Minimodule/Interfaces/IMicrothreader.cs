/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */


using System.Collections;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public interface IMicrothreader
    {
        void Run(IEnumerable microthread);
    }
}