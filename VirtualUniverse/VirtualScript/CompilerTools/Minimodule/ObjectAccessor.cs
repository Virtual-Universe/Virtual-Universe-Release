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
using VirtualUniverse.Framework;
using VirtualUniverse.Framework.SceneInfo;
using VirtualUniverse.Framework.SceneInfo.Entities;
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    internal class IObjEnum : MarshalByRefObject, IEnumerator<IObject>
    {
        private readonly List<ISceneEntity> m_entities;
        private readonly IScene m_scene;
        private readonly ISecurityCredential m_security;
        private readonly IEnumerator<ISceneEntity> m_sogEnum;

        public IObjEnum(IScene scene, ISecurityCredential security)
        {
            m_scene = scene;
            m_security = security;
            m_entities = new List<ISceneEntity>(m_scene.Entities.GetEntities());
            m_sogEnum = m_entities.GetEnumerator();
        }

        #region IEnumerator<IObject> Members

        public void Dispose()
        {
            m_sogEnum.Dispose();
        }

        public bool MoveNext()
        {
            return m_sogEnum.MoveNext();
        }

        public void Reset()
        {
            m_sogEnum.Reset();
        }

        public IObject Current
        {
            get { return new SOPObject(m_scene, m_sogEnum.Current.LocalId, m_security); }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion
    }

    public class ObjectAccessor : MarshalByRefObject, IObjectAccessor
    {
        private readonly IScene m_scene;
        private readonly ISecurityCredential m_security;

        public ObjectAccessor(IScene scene, ISecurityCredential security)
        {
            m_scene = scene;
            m_security = security;
        }

        #region IObjectAccessor Members

        public IObject this[int index]
        {
            get { return new SOPObject(m_scene, (uint)index, m_security); }
        }

        public IObject this[uint index]
        {
            get { return new SOPObject(m_scene, index, m_security); }
        }

        public IObject this[UUID index]
        {
            get
            {
                Framework.SceneInfo.Entities.IEntity ent;
                if (m_scene.Entities.TryGetValue(index, out ent))
                    return new SOPObject(m_scene, ent.LocalId, m_security);
                return null;
            }
        }

        public IObject Create(Vector3 position)
        {
            return Create(position, Quaternion.Identity);
        }

        public IObject Create(Vector3 position, Quaternion rotation)
        {
            ISceneEntity sog = m_scene.SceneGraph.AddNewPrim(m_security.owner.GlobalID,
                                                             UUID.Zero,
                                                             position,
                                                             rotation,
                                                             PrimitiveBaseShape.CreateBox());

            IObject ret = new SOPObject(m_scene, sog.LocalId, m_security);

            return ret;
        }

        public IEnumerator<IObject> GetEnumerator()
        {
            return new IObjEnum(m_scene, m_security);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IObject item)
        {
            throw new NotSupportedException(
                "Collection is read-only. This is an API TODO FIX, creation of objects is presently impossible.");
        }

        public void Clear()
        {
            throw new NotSupportedException("Collection is read-only. TODO FIX.");
        }

        public bool Contains(IObject item)
        {
            Framework.SceneInfo.Entities.IEntity ent;
            return m_scene.Entities.TryGetValue(item.GlobalID, out ent);
        }

        public void CopyTo(IObject[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < Count + arrayIndex; i++)
            {
                array[i] = this[i - arrayIndex];
            }
        }

        public bool Remove(IObject item)
        {
            throw new NotSupportedException("Collection is read-only. TODO FIX.");
        }

        public int Count
        {
            get { return m_scene.Entities.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        #endregion
    }
}