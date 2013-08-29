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
using System.Linq;
using VirtualUniverse.Framework;
using VirtualUniverse.Framework.SceneInfo;
using OpenMetaverse;
using OpenMetaverse.StructuredData;

namespace VirtualUniverse.ScriptEngine.AuroraDotNetEngine.Plugins
{
    public class DataserverPlugin : IScriptPlugin
    {
        private readonly Dictionary<string, DataserverRequest> DataserverRequests =
            new Dictionary<string, DataserverRequest>();

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
        }

        public void RemoveScript(UUID primID, UUID itemID)
        {
            lock (DataserverRequests)
            {
                List<DataserverRequest> ToRemove = DataserverRequests.Values.Where(ds => ds.itemID == itemID).ToList();
                foreach (DataserverRequest re in ToRemove)
                {
                    DataserverRequests.Remove(re.handle);
                }
            }
        }

        public bool Check()
        {
            lock (DataserverRequests)
            {
                if (DataserverRequests.Count == 0)
                    return false;
                List<DataserverRequest> ToRemove = new List<DataserverRequest>();
                foreach (DataserverRequest ds in DataserverRequests.Values)
                {
                    if (ds.IsCompleteAt < DateTime.Now)
                    {
                        DataserverReply(ds.handle, ds.Reply);
                        ToRemove.Add(ds);
                    }

                    if (ds.startTime > DateTime.Now.AddSeconds(30))
                        ToRemove.Add(ds);
                }
                foreach (DataserverRequest re in ToRemove)
                {
                    DataserverRequests.Remove(re.handle);
                }
                return DataserverRequests.Count > 0;
            }
        }

        public string Name
        {
            get { return "Dataserver"; }
        }

        public OSD GetSerializationData(UUID itemID, UUID primID)
        {
            return "";
        }

        public void CreateFromData(UUID itemID, UUID objectID, OSD data)
        {
        }

        #endregion

        public UUID RegisterRequest(UUID primID, UUID itemID,
                                    string identifier)
        {
            DataserverRequest ds = new DataserverRequest
            {
                primID = primID,
                itemID = itemID,
                ID = UUID.Random(),
                handle = identifier,
                startTime = DateTime.Now,
                IsCompleteAt = DateTime.Now.AddHours(1),
                Reply = ""
            };


            lock (DataserverRequests)
            {
                if (DataserverRequests.ContainsKey(identifier))
                    return UUID.Zero;

                DataserverRequests[identifier] = ds;
            }

            //Make sure that the cmd handler thread is running
            m_ScriptEngine.MaintenanceThread.PokeThreads(ds.itemID);

            return ds.ID;
        }

        private void DataserverReply(string identifier, string reply)
        {
            DataserverRequest ds;

            lock (DataserverRequests)
            {
                if (!DataserverRequests.ContainsKey(identifier))
                    return;

                ds = DataserverRequests[identifier];
            }

            m_ScriptEngine.PostObjectEvent(ds.primID,
                                           "dataserver", new Object[]
                                                             {
                                                                 new LSL_Types.LSLString(ds.ID.ToString()),
                                                                 new LSL_Types.LSLString(reply)
                                                             });
        }

        internal void AddReply(string handle, string reply, int millisecondsToWait)
        {
            lock (DataserverRequests)
            {
                DataserverRequest request = null;
                if (DataserverRequests.TryGetValue(handle, out request))
                {
                    //Wait for the value to be returned in LSL_Api
                    request.IsCompleteAt = DateTime.Now.AddSeconds(millisecondsToWait / 1000 + 0.1);
                    request.Reply = reply;
                    //Make sure that the cmd handler thread is running
                    m_ScriptEngine.MaintenanceThread.PokeThreads(request.itemID);
                }
            }
        }

        public void Dispose()
        {
        }

        #region Nested type: DataserverRequest

        private class DataserverRequest
        {
            public UUID ID;
            public DateTime IsCompleteAt;
            public string Reply;
            public string handle;
            public UUID itemID;
            public UUID primID;
            public DateTime startTime;
        }

        #endregion
    }
}