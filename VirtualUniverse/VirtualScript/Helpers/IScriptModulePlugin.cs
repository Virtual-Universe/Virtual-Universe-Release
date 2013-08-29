/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */


using System;
using System.Collections.Generic;
using VirtualUniverse.Framework;
using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Framework.SceneInfo;
using Nini.Config;
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualUniverse
{
    public interface IScriptModulePlugin : IScriptModule
    {
        IConfig Config { get; }

        IConfigSource ConfigSource { get; }

        IScriptModule ScriptModule { get; }
        Dictionary<Type, object> Extensions { get; }

        bool PostScriptEvent(UUID m_itemID, UUID uUID, EventParams EventParams, EventPriority EventPriority);

        void SetState(UUID m_itemID, string newState);

        void SetScriptRunningState(UUID item, bool p);

        IScriptPlugin GetScriptPlugin(string p);

        DetectParams GetDetectParams(UUID uUID, UUID m_itemID, int number);

        void ResetScript(UUID uUID, UUID m_itemID, bool p);

        bool GetScriptRunningState(UUID item);

        int GetStartParameter(UUID m_itemID, UUID uUID);

        void SetMinEventDelay(UUID m_itemID, UUID uUID, double delay);

        IScriptApi GetApi(UUID m_itemID, string p);

        bool PipeEventsForScript(ISceneChildEntity m_host, Vector3 vector3);

        void RegisterExtension<T>(T instance);
    }

    public class EventInfo
    {
        public string Name;
        public string[] ArgumentTypes;
        public EventInfo(string name, string[] types) { Name = name; ArgumentTypes = types; }
    }
}