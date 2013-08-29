/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using System.Drawing;
using VirtualUniverse.Framework;
using VirtualUniverse.Framework.SceneInfo;
using VirtualUniverse.Framework.Services.ClassHelpers.Assets;
using OpenMetaverse;
using OpenMetaverse.Imaging;
using VirtualUniverse.Framework.Modules;

namespace VirtualUniverse.ScriptEngine.VirtualUniverse.MiniModule
{
    internal class Graphics : MarshalByRefObject, IGraphics
    {
        private readonly IScene m_scene;

        public Graphics(IScene m_scene)
        {
            this.m_scene = m_scene;
        }

        #region IGraphics Members

        public UUID SaveBitmap(Bitmap data)
        {
            return SaveBitmap(data, false, true);
        }

        public UUID SaveBitmap(Bitmap data, bool lossless, bool temporary)
        {
            AssetBase asset = new AssetBase(UUID.Random(), "MRMDynamicImage", AssetType.Texture,
                                            m_scene.RegionInfo.RegionID)
            {
                Data = OpenJPEG.EncodeFromImage(data, lossless),
                Description = "MRM Image",
                Flags = (temporary) ? AssetFlags.Temporary : 0
            };
            asset.ID = m_scene.AssetService.Store(asset);

            return asset.ID;
        }

        public Bitmap LoadBitmap(UUID assetID)
        {
            byte[] bmp = m_scene.AssetService.GetData(assetID.ToString());
            Image img = m_scene.RequestModuleInterface<IJ2KDecoder>().DecodeToImage(bmp);

            return new Bitmap(img);
        }

        #endregion
    }
}