/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Framework;
using VirtualUniverse.Framework.SceneInfo;
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    internal class SecurityCredential : ISecurityCredential
    {
        private readonly ISocialEntity m_owner;
        private readonly IScene m_scene;

        public SecurityCredential(ISocialEntity m_owner, IScene m_scene)
        {
            this.m_owner = m_owner;
            this.m_scene = m_scene;
        }

        #region ISecurityCredential Members

        public ISocialEntity owner
        {
            get { return m_owner; }
        }

        public bool CanEditObject(IObject target)
        {
            return m_scene.Permissions.CanEditObject(target.GlobalID, m_owner.GlobalID);
        }

        public bool CanEditTerrain(int x, int y)
        {
            return m_scene.Permissions.CanTerraformLand(m_owner.GlobalID, new Vector3(x, y, 0));
        }

        #endregion
    }
}