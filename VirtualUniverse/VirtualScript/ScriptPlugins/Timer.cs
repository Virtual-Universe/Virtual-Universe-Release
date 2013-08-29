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
using VirtualUniverse.Framework.SceneInfo;
using OpenMetaverse;
using OpenMetaverse.StructuredData;

namespace VirtualUniverse.ScriptEngine.VirtualScript.Plugins
{
    public class TimerPlugin : IScriptPlugin
    {
        private readonly object TimerListLock = new object();
        private readonly Dictionary<string, TimerClass> Timers = new Dictionary<string, TimerClass>();
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

        //
        // TIMER
        //

        public void RemoveScript(UUID m_ID, UUID m_itemID)
        {
            // Remove from timer
            string key = MakeTimerKey(m_ID, m_itemID);
            lock (TimerListLock)
            {
                Timers.Remove(key);
            }
        }

        public bool Check()
        {
            // Nothing to do here?
            if (Timers.Count == 0)
                return false;

            lock (TimerListLock)
            {
                // Go through all timers
                int TickCount = Environment.TickCount;
                foreach (TimerClass ts in Timers.Values.Where(ts => ts.next < TickCount))
                {
                    // Add it to queue
                    m_ScriptEngine.PostScriptEvent(ts.itemID, ts.ID,
                                                   new EventParams("timer", new Object[0],
                                                                   new DetectParams[0]), EventPriority.Continued);
                    // set next interval

                    ts.next = TickCount + ts.interval;
                }
            }
            return Timers.Count > 0;
        }

        public OSD GetSerializationData(UUID itemID, UUID primID)
        {
            OSDMap data = new OSDMap();
            string key = MakeTimerKey(primID, itemID);
            TimerClass timer;
            if (Timers.TryGetValue(key, out timer))
            {
                data.Add("Interval", timer.interval);
                data.Add("Next", timer.next - Environment.TickCount);
            }
            return data;
        }

        public void CreateFromData(UUID itemID, UUID objectID,
                                   OSD data)
        {
            OSDMap save = (OSDMap)data;
            TimerClass ts = new TimerClass { ID = objectID, itemID = itemID, interval = save["Interval"].AsLong() };

            if (ts.interval == 0) // Disabling timer
                return;

            ts.next = Environment.TickCount + save["Next"].AsLong();

            lock (TimerListLock)
            {
                Timers[MakeTimerKey(objectID, itemID)] = ts;
            }

            //Make sure that the cmd handler thread is running
            m_ScriptEngine.MaintenanceThread.PokeThreads(ts.itemID);
        }

        public string Name
        {
            get { return "Timer"; }
        }

        #endregion

        private static string MakeTimerKey(UUID ID, UUID itemID)
        {
            return ID.ToString() + itemID.ToString();
        }

        public void SetTimerEvent(UUID m_ID, UUID m_itemID, double sec)
        {
            if (sec == 0) // Disabling timer
            {
                RemoveScript(m_ID, m_itemID);
                return;
            }

            // Add to timer
            TimerClass ts = new TimerClass { ID = m_ID, itemID = m_itemID, interval = (long)(sec * 1000) };

            //ts.next = DateTime.Now.ToUniversalTime().AddSeconds(ts.interval);
            ts.next = Environment.TickCount + ts.interval;

            string key = MakeTimerKey(m_ID, m_itemID);
            lock (TimerListLock)
            {
                // Adds if timer doesn't exist, otherwise replaces with new timer
                Timers[key] = ts;
            }

            //Make sure that the cmd handler thread is running
            m_ScriptEngine.MaintenanceThread.PokeThreads(ts.itemID);
        }

        public void Dispose()
        {
        }

        #region Nested type: TimerClass

        private class TimerClass
        {
            public UUID ID;
            public long interval;
            public UUID itemID;
            public long next;
        }

        #endregion
    }
}