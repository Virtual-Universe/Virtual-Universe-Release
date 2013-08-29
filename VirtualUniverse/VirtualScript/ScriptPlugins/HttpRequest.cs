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
using VirtualUniverse.Framework.Servers.HttpServer.Implementation;
using OpenMetaverse;
using OpenMetaverse.StructuredData;
using System.Collections.Generic;

namespace VirtualUniverse.ScriptEngine.VirtualScript.Plugins
{
    public class HttpRequestPlugin : IScriptPlugin
    {
        private readonly List<IHttpRequestModule> m_modules = new List<IHttpRequestModule>();
        public ScriptEngine m_ScriptEngine;

        #region IScriptPlugin Members

        public bool RemoveOnStateChange
        {
            get { return false; }
        }

        public void Initialize(ScriptEngine engine)
        {
            m_ScriptEngine = engine;
        }

        public void AddRegion(IScene scene)
        {
            m_modules.Add(scene.RequestModuleInterface<IHttpRequestModule>());
        }

        public bool Check()
        {
            bool needToContinue = false;
            foreach (IHttpRequestModule iHttpReq in m_modules)
            {
                IServiceRequest httpInfo = null;

                if (iHttpReq != null)
                {
                    httpInfo = iHttpReq.GetNextCompletedRequest();
                    if (!needToContinue)
                        needToContinue = iHttpReq.GetRequestCount() > 0;
                }

                if (httpInfo == null)
                    continue;

                while (httpInfo != null)
                {
                    IHttpRequestClass info = (IHttpRequestClass)httpInfo;
                    //MainConsole.Instance.Debug("[AsyncLSL]:" + httpInfo.response_body + httpInfo.status);

                    // Deliver data to prim's remote_data handler

                    iHttpReq.RemoveCompletedRequest(info);

                    object[] resobj = new object[]
                                          {
                                              new LSL_Types.LSLString(info.ReqID.ToString()),
                                              new LSL_Types.LSLInteger(info.Status),
                                              new LSL_Types.list(info.Metadata),
                                              new LSL_Types.LSLString(info.ResponseBody)
                                          };

                    m_ScriptEngine.AddToObjectQueue(info.PrimID, "http_response", new DetectParams[0], resobj);
                    if (info.Status == (int)499 && //Too many for this prim
                        info.VerbroseThrottle)
                    {
                        ISceneChildEntity part = m_ScriptEngine.Scene.GetSceneObjectPart(info.PrimID);
                        if (part != null)
                        {
                            IChatModule chatModule = m_ScriptEngine.Scene.RequestModuleInterface<IChatModule>();
                            if (chatModule != null)
                                chatModule.SimChat(
                                    part.Name + "(" + part.AbsolutePosition +
                                    ") http_response error: Too many outgoing requests.", ChatTypeEnum.DebugChannel,
                                    2147483647, part.AbsolutePosition, part.Name, part.UUID, false, m_ScriptEngine.Scene);
                        }
                    }
                    httpInfo = iHttpReq.GetNextCompletedRequest();
                }
            }
            return needToContinue;
        }

        public string Name
        {
            get { return "HttpRequest"; }
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
            foreach (IHttpRequestModule iHttpReq in m_modules)
            {
                iHttpReq.StopHttpRequest(primID, itemID);
            }
        }

        #endregion

        public void Dispose()
        {
        }
    }
}