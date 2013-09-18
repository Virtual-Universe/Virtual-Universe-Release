/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System.Collections.Generic;

namespace VirtualUniverse.ClientStack
{
    /// <summary>
    ///     A circular buffer and hashset for tracking incoming packet sequence
    ///     numbers
    /// </summary>
    public sealed class IncomingPacketHistoryCollection
    {
        private readonly int m_capacity;
        private readonly HashSet<uint> m_hashSet;
        private readonly uint[] m_items;
        private int m_first;
        private int m_next;

        public IncomingPacketHistoryCollection(int capacity)
        {
            m_capacity = capacity;
            m_items = new uint[capacity];
            m_hashSet = new HashSet<uint>();
        }

        public bool TryEnqueue(uint ack)
        {
            lock (m_hashSet)
            {
                if (m_hashSet.Add(ack))
                {
                    m_items[m_next] = ack;
                    m_next = (m_next + 1) % m_capacity;
                    if (m_next == m_first)
                    {
                        m_hashSet.Remove(m_items[m_first]);
                        m_first = (m_first + 1) % m_capacity;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}