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
using VirtualUniverse.Framework.SceneInfo;
using VirtualUniverse.Framework.Services.ClassHelpers.Assets;
using OpenMetaverse;
using OpenMetaverse.Assets;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public class InventoryItem : IInventoryItem
    {
        private readonly TaskInventoryItem m_privateItem;
        private readonly IScene m_rootScene;

        public InventoryItem(IScene rootScene, TaskInventoryItem internalItem)
        {
            m_rootScene = rootScene;
            m_privateItem = internalItem;
        }

        // Marked internal, to prevent scripts from accessing the internal type

        #region IInventoryItem Members

        public int Type
        {
            get { return m_privateItem.Type; }
        }

        public UUID AssetID
        {
            get { return m_privateItem.AssetID; }
        }

        // This method exposes OpenSim/OpenMetaverse internals and needs to be replaced with a IAsset specific to MRM.
        public T RetrieveAsset<T>() where T : Asset, new()
        {
            AssetBase a = m_rootScene.AssetService.Get(AssetID.ToString());
            T result = new T();

            if ((sbyte)result.AssetType != a.Type)
                throw new ApplicationException("[MRM] The supplied asset class does not match the found asset");

            result.AssetData = a.Data;
            result.Decode();
            return result;
        }

        #endregion

        internal TaskInventoryItem ToTaskInventoryItem()
        {
            return m_privateItem;
        }

        /// <summary>
        ///     This will attempt to convert from an IInventoryItem to an InventoryItem object
        /// </summary>
        /// <description>
        ///     In order for this to work the object which implements IInventoryItem must inherit from InventoryItem, otherwise
        ///     an exception is thrown.
        /// </description>
        /// <param name="i">
        ///     The interface to upcast <see cref="IInventoryItem" />
        /// </param>
        /// <returns>
        ///     The object backing the interface implementation <see cref="InventoryItem" />
        /// </returns>
        internal static InventoryItem FromInterface(IInventoryItem i)
        {
            if (typeof(InventoryItem).IsAssignableFrom(i.GetType()))
            {
                return (InventoryItem)i;
            }
            else
            {
                throw new ApplicationException("[MRM] There is no legal conversion from IInventoryItem to InventoryItem");
            }
        }
    }
}