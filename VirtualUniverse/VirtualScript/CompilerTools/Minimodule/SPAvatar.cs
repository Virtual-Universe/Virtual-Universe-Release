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
using System.Security;
using VirtualUniverse.Framework;
using VirtualUniverse.Framework.ClientInterfaces;
using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Framework.PresenceInfo;
using VirtualUniverse.Framework.SceneInfo;
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    internal class SPAvatar : MarshalByRefObject, IAvatar
    {
        private readonly UUID m_ID;
        private readonly IScene m_rootScene;
        private readonly ISecurityCredential m_security;
        //private static readonly ILog MainConsole.Instance = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SPAvatar(IScene scene, UUID ID, ISecurityCredential security)
        {
            m_rootScene = scene;
            m_security = security;
            m_ID = ID;
        }

        #region IAvatar Members

        public string Name
        {
            get { return GetSP().Name; }
            set { throw new SecurityException("Avatar Names are a read-only property."); }
        }

        public UUID GlobalID
        {
            get { return m_ID; }
        }

        public Vector3 WorldPosition
        {
            get { return GetSP().AbsolutePosition; }
            set { GetSP().TeleportWithMomentum(value); }
        }

        public bool IsChildAgent
        {
            get { return GetSP().IsChildAgent; }
        }

        #endregion

        #region IAvatar implementation

        public IAvatarAttachment[] Attachments
        {
            get
            {
                IAvatarAppearanceModule appearance = GetSP().RequestModuleInterface<IAvatarAppearanceModule>();
                List<AvatarAttachment> internalAttachments = appearance.Appearance.GetAttachments();

                return
                    internalAttachments.Select(
                        attach =>
                        new SPAvatarAttachment(m_rootScene, this, attach.AttachPoint, new UUID(attach.ItemID),
                                               new UUID(attach.AssetID), m_security))
                                       .Cast<IAvatarAttachment>()
                                       .ToArray();
            }
        }

        public void LoadUrl(IObject sender, string message, string url)
        {
            IDialogModule dm = m_rootScene.RequestModuleInterface<IDialogModule>();
            if (dm != null)
                dm.SendUrlToUser(GetSP().UUID, sender.Name, sender.GlobalID, GetSP().UUID, false, message, url);
        }

        #endregion

        private IScenePresence GetSP()
        {
            return m_rootScene.GetScenePresence(m_ID);
        }
    }
}