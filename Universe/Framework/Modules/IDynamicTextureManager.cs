/*
 * Copyright (c) Contributors, http://virtual-planets.org/, http://whitecore-sim.org/, http://aurora-sim.org, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual Universe Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System.IO;
using OpenMetaverse;

namespace Universe.Framework.Modules
{
    public interface IDynamicTextureManager
    {
        void RegisterRender(string handleType, IDynamicTextureRender render);
        void ReturnData(UUID id, byte[] data);

        UUID AddDynamicTextureURL(UUID simID, UUID primID, UUID oldAssetID, string contentType, string url,
                                  string extraParams,
                                  int updateTimer);

        UUID AddDynamicTextureURL(UUID simID, UUID primID, UUID oldAssetID, string contentType, string url,
                                  string extraParams,
                                  int updateTimer, bool SetBlending, byte AlphaValue);

        UUID AddDynamicTextureURL(UUID simID, UUID primID, UUID oldAssetID, string contentType, string url,
                                  string extraParams,
                                  int updateTimer, bool SetBlending, int disp, byte AlphaValue, int face);

        UUID AddDynamicTextureData(UUID simID, UUID primID, UUID oldAssetID, string contentType, string data,
                                   string extraParams,
                                   int updateTimer);

        /// <summary>
        ///     Apply a dynamically generated texture to all sides of the given prim.  The texture is not persisted to the
        ///     asset service.
        /// </summary>
        /// <param name="simID">The simulator in which the texture is being generated</param>
        /// <param name="primID">The prim to which to apply the texture.</param>
        /// <param name="oldAssetID"></param>
        /// <param name="contentType">
        ///     The content type to create.  Current choices are "vector" to create a vector
        ///     based texture or "image" to create a texture from an image at a particular URL
        /// </param>
        /// <param name="data">The data for the generator</param>
        /// <param name="extraParams">Parameters for the generator that don't form part of the main data.</param>
        /// <param name="updateTimer">
        ///     If zero, the image is never updated after the first generation.  If positive
        ///     the image is updated at the given interval.  Not implemented for
        /// </param>
        /// <param name="SetBlending">
        ///     If true, the newly generated texture is blended with the appropriate existing ones on the prim
        /// </param>
        /// <param name="AlphaValue">
        ///     The alpha value of the generated texture.
        /// </param>
        /// <returns>
        ///     The UUID of the texture updater, not the texture UUID.  If you need the texture UUID then you will need
        ///     to obtain it directly from the SceneObjectPart.  For instance, if ALL_SIDES is set then this texture
        ///     can be obtained as SceneObjectPart.Shape.Textures.DefaultTexture.TextureID
        /// </returns>
        UUID AddDynamicTextureData(UUID simID, UUID primID, UUID oldAssetID, string contentType, string data,
                                   string extraParams,
                                   int updateTimer, bool SetBlending, byte AlphaValue);

        /// <summary>
        ///     Apply a dynamically generated texture to the given prim.
        /// </summary>
        /// <param name="simID">The simulator in which the texture is being generated</param>
        /// <param name="primID">The prim to which to apply the texture.</param>
        /// <param name="oldAssetID"></param>
        /// <param name="contentType">
        ///     The content type to create.  Current choices are "vector" to create a vector
        ///     based texture or "image" to create a texture from an image at a particular URL
        /// </param>
        /// <param name="data">The data for the generator</param>
        /// <param name="extraParams">Parameters for the generator that don't form part of the main data.</param>
        /// <param name="updateTimer">
        ///     If zero, the image is never updated after the first generation.  If positive
        ///     the image is updated at the given interval.  Not implemented for
        /// </param>
        /// <param name="SetBlending">
        ///     If true, the newly generated texture is blended with the appropriate existing ones on the prim
        /// </param>
        /// <param name="disp">
        ///     Display flags.  If DISP_EXPIRE then the old texture is deleted if it is replaced by a
        ///     newer generated texture (may not currently be implemented).  If DISP_TEMP then the asset is flagged as
        ///     temporary, which often means that it is not persisted to the database.
        /// </param>
        /// <param name="AlphaValue">
        ///     The alpha value of the generated texture.
        /// </param>
        /// <param name="face">
        ///     The face of the prim on which to put the generated texture.  If ALL_SIDES then all sides of the prim are
        ///     set
        /// </param>
        /// <returns>
        ///     The UUID of the texture updater, not the texture UUID.  If you need the texture UUID then you will need
        ///     to obtain it directly from the SceneObjectPart.  For instance, if ALL_SIDES is set then this texture
        ///     can be obtained as SceneObjectPart.Shape.Textures.DefaultTexture.TextureID
        /// </returns>
        UUID AddDynamicTextureData(
            UUID simID, UUID primID, UUID oldAssetID, string contentType, string data, string extraParams,
            int updateTimer, bool SetBlending, int disp, byte AlphaValue, int face);

        void GetDrawStringSize(string contentType, string text, string fontName, int fontSize,
                               out double xSize, out double ySize);
    }

    public interface IDynamicTextureRender
    {
        string GetName();
        string GetContentType();
        bool SupportsAsynchronous();
        byte[] ConvertUrl(string url, string extraParams);
        byte[] ConvertStream(Stream data, string extraParams);
        bool AsyncConvertUrl(UUID id, string url, string extraParams);
        bool AsyncConvertData(UUID id, string bodyData, string extraParams);

        void GetDrawStringSize(string text, string fontName, int fontSize,
                               out double xSize, out double ySize);
    }
}