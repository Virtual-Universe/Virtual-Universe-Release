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
using OpenMetaverse;
using OpenMetaverse.StructuredData;

namespace VirtualUniverse.ScriptEngine.VirtualScript.Plugins
{
    public class XmlRequestPlugin : IScriptPlugin
    {
        private readonly List<IXMLRPC> m_modules = new List<IXMLRPC>();
        public ScriptEngine m_ScriptEngine;

        #region IScriptPlugin Members

        public bool RemoveOnStateChange
        {
            get { return false; }
        }

        public void Initialize(ScriptEngine ScriptEngine)
        {
            m_ScriptEngine = ScriptEngine;
        }

        public void AddRegion(IScene scene)
        {
            m_modules.Add(scene.RequestModuleInterface<IXMLRPC>());
        }

        public bool Check()
        {
            bool needToContinue = false;
            foreach (IXMLRPC xmlrpc in m_modules)
            {
                if (xmlrpc == null)
                    continue;
                IXmlRpcRequestInfo rInfo = xmlrpc.GetNextCompletedRequest();
                ISendRemoteDataRequest srdInfo = (ISendRemoteDataRequest)xmlrpc.GetNextCompletedSRDRequest();

                if (!needToContinue)
                    needToContinue = xmlrpc.hasRequests();

                if (rInfo == null && srdInfo == null)
                    continue;

                while (rInfo != null)
                {
                    xmlrpc.RemoveCompletedRequest(rInfo.GetMessageID());

                    //Deliver data to prim's remote_data handler
                    object[] resobj = new object[]
                                          {
                                              new LSL_Types.LSLInteger(2),
                                              new LSL_Types.LSLString(
                                                  rInfo.GetChannelKey().ToString()),
                                              new LSL_Types.LSLString(
                                                  rInfo.GetMessageID().ToString()),
                                              new LSL_Types.LSLString(String.Empty),
                                              new LSL_Types.LSLInteger(rInfo.GetIntValue()),
                                              new LSL_Types.LSLString(rInfo.GetStrVal())
                                          };

                    m_ScriptEngine.PostScriptEvent(
                        rInfo.GetItemID(), rInfo.GetPrimID(), new EventParams(
                                                                  "remote_data", resobj,
                                                                  new DetectParams[0]), EventPriority.Suspended);

                    rInfo = xmlrpc.GetNextCompletedRequest();
                }

                while (srdInfo != null)
                {
                    xmlrpc.RemoveCompletedSRDRequest(srdInfo.GetReqID());

                    //Deliver data to prim's remote_data handler
                    object[] resobj = new object[]
                                          {
                                              new LSL_Types.LSLInteger(3),
                                              new LSL_Types.LSLString(srdInfo.Channel),
                                              new LSL_Types.LSLString(srdInfo.GetReqID().ToString()),
                                              new LSL_Types.LSLString(String.Empty),
                                              new LSL_Types.LSLInteger(srdInfo.Idata),
                                              new LSL_Types.LSLString(srdInfo.Sdata)
                                          };

                    m_ScriptEngine.PostScriptEvent(
                        srdInfo.ItemID, srdInfo.PrimID, new EventParams(
                                                            "remote_data", resobj,
                                                            new DetectParams[0]), EventPriority.Suspended);

                    srdInfo = (ISendRemoteDataRequest)xmlrpc.GetNextCompletedSRDRequest();
                }
            }
            return needToContinue;
        }

        public string Name
        {
            get { return "XmlRequest"; }
        }

        public OSD GetSerializationData(UUID itemID, UUID primID)
        {
            return "";
        }

        public void CreateFromData(UUID itemID, UUID objectID, OSD data)
        {
        }

        public void RemoveScript(UUID primID, UUID itemID)
        {
            foreach (IXMLRPC xmlrpc in m_modules)
            {
                xmlrpc.DeleteChannels(itemID);
                xmlrpc.CancelSRDRequests(itemID);
            }
        }

        #endregion

        public void Dispose()
        {
        }
    }
}