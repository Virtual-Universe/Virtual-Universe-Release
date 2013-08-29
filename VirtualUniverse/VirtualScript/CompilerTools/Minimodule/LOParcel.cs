/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using VirtualUniverse.Framework;
using VirtualUniverse.Framework.Modules;
using VirtualUniverse.Framework.SceneInfo;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    internal class LOParcel : MarshalByRefObject, IParcel
    {
        private readonly int m_parcelID;
        private readonly IScene m_scene;

        public LOParcel(IScene m_scene, int m_parcelID)
        {
            this.m_scene = m_scene;
            this.m_parcelID = m_parcelID;
        }

        #region IParcel Members

        public string Name
        {
            get { return GetLO().LandData.Name; }
            set { GetLO().LandData.Name = value; }
        }

        public string Description
        {
            get { return GetLO().LandData.Description; }
            set { GetLO().LandData.Description = value; }
        }

        public ISocialEntity Owner
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion

        private ILandObject GetLO()
        {
            IParcelManagementModule parcelManagement = m_scene.RequestModuleInterface<IParcelManagementModule>();
            if (parcelManagement != null)
            {
                return parcelManagement.GetLandObject(m_parcelID);
            }
            return null;
        }
    }
}