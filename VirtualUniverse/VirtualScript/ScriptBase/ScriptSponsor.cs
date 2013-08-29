/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using System.Runtime.Remoting.Lifetime;

namespace VirtualUniverse.ScriptEngine.VirtualScript.Runtime
{
    public class ScriptSponsor : MarshalByRefObject, ISponsor
    {
        private bool m_closed;

        public TimeSpan Renewal(ILease lease)
        {
            if (!m_closed)
                return lease.InitialLeaseTime;
            return TimeSpan.FromTicks(0);
        }

        public void Close()
        {
            m_closed = true;
        }
    }
}