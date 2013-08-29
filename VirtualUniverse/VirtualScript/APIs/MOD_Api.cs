/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Framework;
using VirtualUniverse.Framework.ClientInterfaces;
using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Framework.SceneInfo;
using VirtualUniverse.ScriptEngine.VirtualScript.Runtime;
using OpenMetaverse;
using System;
using System.Runtime.Remoting.Lifetime;
using LSL_Float = VirtualUniverse.ScriptEngine.VirtualUniverse.LSL_Types.LSLFloat;
using LSL_Integer = VirtualUniverse.ScriptEngine.VirtualUniverse.LSL_Types.LSLInteger;
using LSL_Key = VirtualUniverse.ScriptEngine.VirtualUniverse.LSL_Types.LSLString;
using LSL_List = VirtualUniverse.ScriptEngine.VirtualUniverse.LSL_Types.list;
using LSL_Rotation = VirtualUniverse.ScriptEngine.VirtualUniverse.LSL_Types.Quaternion;
using LSL_String = VirtualUniverse.ScriptEngine.VirtualUniverse.LSL_Types.LSLString;
using LSL_Vector = VirtualUniverse.ScriptEngine.VirtualUniverse.LSL_Types.Vector3;

namespace VirtualUniverse.ScriptEngine.VirtualScript.APIs
{
    public class MOD_Api : MarshalByRefObject, IScriptApi
    {
        internal ScriptProtectionModule ScriptProtection;
        internal IScriptModulePlugin m_ScriptEngine;
        internal IScriptModuleComms m_comms;
        internal ISceneChildEntity m_host;
        internal UUID m_itemID;
        internal uint m_localID;

        public IScene World
        {
            get { return m_host.ParentEntity.Scene; }
        }

        #region IMOD_Api Members

        public string modSendCommand(string module, string command, string k)
        {
            if (!ScriptProtection.CheckThreatLevel(ThreatLevel.Moderate, "modSendCommand", m_host, "MOD", m_itemID))
                return "";

            UUID req = UUID.Random();

            m_comms.RaiseEvent(m_itemID, req.ToString(), module, command, k);

            return req.ToString();
        }

        #endregion

        #region IScriptApi Members

        public void Initialize(IScriptModulePlugin ScriptEngine, ISceneChildEntity host, uint localID, UUID itemID,
                               ScriptProtectionModule module)
        {
            m_ScriptEngine = ScriptEngine;
            m_host = host;
            m_localID = localID;
            m_itemID = itemID;
            ScriptProtection = module;

            m_comms = World.RequestModuleInterface<IScriptModuleComms>();
        }

        public IScriptApi Copy()
        {
            return new MOD_Api();
        }

        public string Name
        {
            get { return "mod"; }
        }

        public string InterfaceName
        {
            get { return "IMOD_Api"; }
        }

        /// <summary>
        ///     We don't have to add any assemblies here
        /// </summary>
        public string[] ReferencedAssemblies
        {
            get { return new string[0]; }
        }

        /// <summary>
        ///     We use the default namespace, so we don't have any to add
        /// </summary>
        public string[] NamespaceAdditions
        {
            get { return new string[0]; }
        }

        #endregion

        public void Dispose()
        {
        }

        public override Object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();

            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromMinutes(0);
                //                lease.RenewOnCallTime = TimeSpan.FromSeconds(10.0);
                //                lease.SponsorshipTimeout = TimeSpan.FromMinutes(1.0);
            }
            return lease;
        }

        internal void modError(string msg)
        {
            throw new Exception("MOD Runtime Error: " + msg);
        }

        //
        //Dumps an error message on the debug console.
        //

        internal void modShoutError(string message)
        {
            if (message.Length > 1023)
                message = message.Substring(0, 1023);

            IChatModule chatModule = World.RequestModuleInterface<IChatModule>();
            if (chatModule != null)
                chatModule.SimChat(message, ChatTypeEnum.Shout, ScriptBaseClass.DEBUG_CHANNEL,
                                   m_host.ParentEntity.RootChild.AbsolutePosition, m_host.Name, m_host.UUID, true, World);

            IWorldComm wComm = World.RequestModuleInterface<IWorldComm>();
            wComm.DeliverMessage(ChatTypeEnum.Shout, ScriptBaseClass.DEBUG_CHANNEL, m_host.Name, m_host.UUID, message);
        }
    }
}