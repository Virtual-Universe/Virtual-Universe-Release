/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using VirtualUniverse.Framework;
using VirtualUniverse.Framework.SceneInfo;

namespace VirtualUniverse.ScriptEngine.VirtualUniverse
{
    [Serializable]
    public class EnumeratorInfo
    {
        public Guid Key = Guid.Empty;
        public DateTime SleepTo = DateTime.MinValue;
    }

    public interface IScript : IDisposable
    {
        ISponsor Sponsor { get; }
        string Name { get; }

        /// <summary>
        ///     Whether this script needs a state save performed
        /// </summary>
        bool NeedsStateSaved { get; set; }

        void InitApi(IScriptApi data);

        long GetStateEventFlags(string state);

        EnumeratorInfo ExecuteEvent(string state, string FunctionName, object[] args, EnumeratorInfo Start,
                                    out Exception ex);

        Dictionary<string, Object> GetVars();
        void SetVars(Dictionary<string, Object> vars);
        Dictionary<string, Object> GetStoreVars();
        void SetStoreVars(Dictionary<string, Object> vars);
        void ResetVars();

        /// <summary>
        ///     Find the initial variables so that we can reset the state later if needed
        /// </summary>
        void UpdateInitialValues();

        void Close();

        /// <summary>
        ///     Gives a ref to the scene the script is in and its parent object
        /// </summary>
        /// <param name="iScene"></param>
        /// <param name="iSceneChildEntity"></param>
        /// <param name="useStateSaves"></param>
        void SetSceneRefs(IScene iScene, ISceneChildEntity iSceneChildEntity, bool useStateSaves);

        /// <summary>
        ///     Fires a generic event by the given name
        /// </summary>
        /// <param name="evName"></param>
        /// <param name="parameters"></param>
        IEnumerator FireEvent(string evName, object[] parameters);
    }
}