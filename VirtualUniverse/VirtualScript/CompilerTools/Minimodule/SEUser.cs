/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */


using System;
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    internal class SEUser : MarshalByRefObject, ISocialEntity
    {
        private readonly string m_name;
        private readonly UUID m_uuid;

        public SEUser(UUID uuid, string name)
        {
            m_uuid = uuid;
            m_name = name;
        }

        #region ISocialEntity Members

        public UUID GlobalID
        {
            get { return m_uuid; }
        }

        public string Name
        {
            get { return m_name; }
        }

        public bool IsUser
        {
            get { return true; }
        }

        #endregion
    }
}