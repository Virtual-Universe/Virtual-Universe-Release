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

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public class MicroScheduler : MarshalByRefObject, IMicrothreader
    {
        private readonly List<IEnumerator> m_threads = new List<IEnumerator>();

        #region IMicrothreader Members

        public void Run(IEnumerable microthread)
        {
            lock (m_threads)
                m_threads.Add(microthread.GetEnumerator());
        }

        #endregion

        public void Tick(int count)
        {
            lock (m_threads)
            {
                if (m_threads.Count == 0)
                    return;

                int i = 0;
                while (m_threads.Count > 0 && i < count)
                {
                    i++;

                    bool running = m_threads[i % m_threads.Count].MoveNext();


                    if (!running)
                        m_threads.Remove(m_threads[i % m_threads.Count]);
                }
            }
        }
    }
}