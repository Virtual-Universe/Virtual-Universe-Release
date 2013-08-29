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
    public class SPAvatarAttachment : IAvatarAttachment
    {
        //private readonly UUID m_itemId;
        private readonly UUID m_assetId;
        private readonly int m_location;
        private readonly IScene m_rootScene;

        private readonly ISecurityCredential m_security;

        public SPAvatarAttachment(IScene rootScene, IAvatar self, int location, UUID itemId, UUID assetId,
                                  ISecurityCredential security)
        {
            m_rootScene = rootScene;
            m_security = security;
            //m_parent = self;
            m_location = location;
            //m_itemId = itemId;
            m_assetId = assetId;
        }

        #region IAvatarAttachment Members

        public int Location
        {
            get { return m_location; }
        }

        public IObject Asset
        {
            get { return new SOPObject(m_rootScene, m_rootScene.GetSceneObjectPart(m_assetId).LocalId, m_security); }
        }

        #endregion
    }
}