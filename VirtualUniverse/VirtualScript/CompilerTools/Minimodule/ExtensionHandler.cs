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

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    internal class ExtensionHandler : IExtension
    {
        private readonly Dictionary<Type, object> m_instances;

        public ExtensionHandler(Dictionary<Type, object> instances)
        {
            m_instances = instances;
        }

        #region IExtension Members

        public T Get<T>()
        {
            return (T)m_instances[typeof(T)];
        }

        public bool TryGet<T>(out T extension)
        {
            if (!m_instances.ContainsKey(typeof(T)))
            {
                extension = default(T);
                return false;
            }

            extension = Get<T>();
            return true;
        }

        public bool Has<T>()
        {
            return m_instances.ContainsKey(typeof(T));
        }

        #endregion
    }
}