/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System.Collections.Generic;
using System.Linq;
using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Framework.SceneInfo;
using OpenMetaverse;
using OpenMetaverse.StructuredData;

namespace VirtualUniverse.ScriptEngine.VirtualScript.Plugins
{
    public class ListenerPlugin : IScriptPlugin
    {
        // private static readonly ILog MainConsole.Instance = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly List<IWorldComm> m_modules = new List<IWorldComm>();
        public ScriptEngine m_ScriptEngine;

        #region IScriptPlugin Members

        public bool RemoveOnStateChange
        {
            get { return true; }
        }

        public void Initialize(ScriptEngine engine)
        {
            m_ScriptEngine = engine;
        }

        public void AddRegion(IScene scene)
        {
            m_modules.Add(scene.RequestModuleInterface<IWorldComm>());
        }

        public bool Check()
        {
            bool needToContinue = false;
            foreach (IWorldComm comms in m_modules)
            {
                if (!needToContinue)
                    needToContinue = comms.HasListeners();
                if (comms.HasMessages())
                {
                    while (comms.HasMessages())
                    {
                        IWorldCommListenerInfo lInfo = comms.GetNextMessage();

                        //Deliver data to prim's listen handler
                        object[] resobj = new object[]
                                              {
                                                  new LSL_Types.LSLInteger(lInfo.GetChannel()),
                                                  new LSL_Types.LSLString(lInfo.GetName()),
                                                  new LSL_Types.LSLString(lInfo.GetID().ToString()),
                                                  new LSL_Types.LSLString(lInfo.GetMessage())
                                              };

                        m_ScriptEngine.PostScriptEvent(
                            lInfo.GetItemID(), lInfo.GetHostID(), new EventParams(
                                                                      "listen", resobj,
                                                                      new DetectParams[0]), EventPriority.Suspended);
                    }
                }
            }
            return needToContinue;
        }

        public OSD GetSerializationData(UUID itemID, UUID primID)
        {
            foreach (
                OSD r in m_modules.Select(comms => comms.GetSerializationData(itemID, primID)).Where(r => r != null))
            {
                return r;
            }
            return new OSDMap();
        }

        public void CreateFromData(UUID itemID, UUID hostID,
                                   OSD data)
        {
            foreach (IWorldComm comms in m_modules)
            {
                comms.CreateFromData(itemID, hostID, data);
            }

            //Make sure that the cmd handler thread is running
            m_ScriptEngine.MaintenanceThread.PokeThreads(itemID);
        }

        public string Name
        {
            get { return "Listener"; }
        }

        public void RemoveScript(UUID primID, UUID itemID)
        {
            foreach (IWorldComm comms in m_modules)
            {
                comms.DeleteListener(itemID);
            }
        }

        #endregion

        public void Dispose()
        {
        }
    }
}