/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System.Collections;

namespace VirtualUniverse.ScriptEngine.VirtualScript
{

    #region StartPerformanceQueue

    public class StartPerformanceQueue
    {
        private readonly Queue ContinuedQueue = new Queue(10000);
        private readonly Queue FirstStartQueue = new Queue(10000);
        private readonly Queue SuspendedQueue = new Queue(100); //Smaller, we don't get this very often
        private int ContinuedQueueCount;

        private int FirstStartQueueCount;
        private int SuspendedQueueCount;

        public bool GetNext(out object Item)
        {
            Item = null;
            lock (FirstStartQueue)
            {
                if (FirstStartQueue.Count != 0)
                {
                    FirstStartQueueCount--;
                    Item = FirstStartQueue.Dequeue();
                    return true;
                }
            }
            lock (SuspendedQueue)
            {
                if (SuspendedQueue.Count != 0)
                {
                    SuspendedQueueCount--;
                    Item = SuspendedQueue.Dequeue();
                    return true;
                }
            }
            lock (ContinuedQueue)
            {
                if (ContinuedQueue.Count != 0)
                {
                    ContinuedQueueCount--;
                    Item = ContinuedQueue.Dequeue();
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            lock (ContinuedQueue)
            {
                lock (SuspendedQueue)
                {
                    lock (FirstStartQueue)
                    {
                        ContinuedQueue.Clear();
                        SuspendedQueue.Clear();
                        FirstStartQueue.Clear();
                        ContinuedQueueCount = SuspendedQueueCount = FirstStartQueueCount = 0;
                    }
                }
            }
        }

        public int Count()
        {
            return ContinuedQueueCount + SuspendedQueueCount + FirstStartQueueCount;
        }

        public void Add(object item, LoadPriority priority)
        {
            if (priority == LoadPriority.FirstStart)
                lock (FirstStartQueue)
                {
                    FirstStartQueueCount++;
                    FirstStartQueue.Enqueue(item);
                }
            if (priority == LoadPriority.Restart)
                lock (SuspendedQueue)
                {
                    SuspendedQueueCount++;
                    SuspendedQueue.Enqueue(item);
                }
            if (priority == LoadPriority.Stop)
                lock (ContinuedQueue)
                {
                    ContinuedQueueCount++;
                    ContinuedQueue.Enqueue(item);
                }
        }
    }

    #endregion
}