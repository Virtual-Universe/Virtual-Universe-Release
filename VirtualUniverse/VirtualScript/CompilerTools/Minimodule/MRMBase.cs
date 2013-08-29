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
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public abstract class MRMBase : IScript
    {
        private IHost m_host;
        private UUID m_id;
        private IWorld m_world;

        protected IWorld World
        {
            get { return m_world; }
        }

        protected IHost Host
        {
            get { return m_host; }
        }

        public UUID ID
        {
            get { return m_id; }
        }

        #region IScript Members

        public void InitApi(IScriptApi data)
        {
        }

        public ISponsor Sponsor
        {
            get { return null; }
        }

        public long GetStateEventFlags(string state)
        {
            return 0;
        }

        public EnumeratorInfo ExecuteEvent(string state, string FunctionName, object[] args, EnumeratorInfo Start,
                                           out Exception ex)
        {
            ex = null;
            return null;
        }

        public Dictionary<string, object> GetVars()
        {
            return new Dictionary<string, object>();
        }

        public void SetVars(Dictionary<string, object> vars)
        {
        }

        public Dictionary<string, object> GetStoreVars()
        {
            return new Dictionary<string, object>();
        }

        public void SetStoreVars(Dictionary<string, object> vars)
        {
        }

        public void ResetVars()
        {
        }

        public void UpdateInitialValues()
        {
        }

        public void Close()
        {
            Stop();
        }

        public string Name
        {
            get { return "MRMBase"; }
        }

        public void Dispose()
        {
        }

        public void SetSceneRefs(IScene iScene, ISceneChildEntity iSceneChildEntity, bool useStateSaves)
        {
        }

        public bool NeedsStateSaved
        {
            get { return false; }
            set { }
        }

        public IEnumerator FireEvent(string evName, object[] parameters)
        {
            yield break;
        }

        #endregion

        public void InitMiniModule(IWorld world, IHost host, UUID uniqueID)
        {
            m_world = world;
            m_host = host;
            m_id = uniqueID;
        }

        public abstract void Start();
        public abstract void Stop();
    }
}