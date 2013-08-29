/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using LSL_Float = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLFloat;
using LSL_Integer = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLInteger;
using LSL_Key = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLString;
using LSL_List = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.list;
using LSL_Rotation = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.Quaternion;
using LSL_String = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLString;
using LSL_Vector = VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.Vector3;

namespace VirtualUniverse.ScriptEngine.VirtualScript.APIs.Interfaces
{
    public interface IMOD_Api
    {
        //Module functions
        string modSendCommand(string modules, string command, string k);
    }
}