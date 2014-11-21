/***************************************************************************
 *	                VIRTUAL REALITY PUBLIC SOURCE LICENSE
 * 
 * Date				: Sun January 1, 2006
 * Copyright		: (c) 2006-2014 by Virtual Reality Development Team. 
 *                    All Rights Reserved.
 * Website			: http://www.syndarveruleiki.is
 *
 * Product Name		: Virtual Reality
 * License Text     : packages/docs/VRLICENSE.txt
 * 
 * Planetary Info   : Information about the Planetary code
 * 
 * Copyright        : (c) 2014-2024 by Second Galaxy Development Team
 *                    All Rights Reserved.
 * 
 * Website          : http://www.secondgalaxy.com
 * 
 * Product Name     : Virtual Reality
 * License Text     : packages/docs/SGLICENSE.txt
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the WhiteCore-Sim Project nor the
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
***************************************************************************/

using System;
using System.Collections.Generic;
using OpenMetaverse;
using OpenMetaverse.Assets;

namespace OpenMetaverse.TestClient
{
    public class TexturesCommand : Command
    {
        Dictionary<UUID, UUID> alreadyRequested = new Dictionary<UUID, UUID>();
        bool enabled = false;

        public TexturesCommand(TestClient testClient)
        {
            enabled = testClient.ClientManager.GetTextures;

            Name = "textures";
            Description = "Turns automatic texture downloading on or off. Usage: textures [on/off]";
            Category = CommandCategory.Objects;
            
            testClient.Objects.ObjectUpdate += new EventHandler<PrimEventArgs>(Objects_OnNewPrim);            
            testClient.Objects.AvatarUpdate += Objects_OnNewAvatar;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length != 1)
                return "Usage: textures [on/off]";

            if (args[0].ToLower() == "on")
            {
                Client.ClientManager.GetTextures = enabled = true;
                return "Texture downloading is on";
            }
            else if (args[0].ToLower() == "off")
            {
                Client.ClientManager.GetTextures = enabled = false;
                return "Texture downloading is off";
            }
            else
            {
                return "Usage: textures [on/off]";
            }
        }

        void Objects_OnNewAvatar(object sender, AvatarUpdateEventArgs e)
        {
            Avatar avatar = e.Avatar;
            if (enabled)
            {
                // Search this avatar for textures
                for (int i = 0; i < avatar.Textures.FaceTextures.Length; i++)
                {
                    Primitive.TextureEntryFace face = avatar.Textures.FaceTextures[i];

                    if (face != null)
                    {
                        if (!alreadyRequested.ContainsKey(face.TextureID))
                        {
                            alreadyRequested[face.TextureID] = face.TextureID;

                            // Determine if this is a baked outfit texture or a normal texture
                            ImageType type = ImageType.Normal;
                            AvatarTextureIndex index = (AvatarTextureIndex)i;
                            switch (index)
                            {
                                case AvatarTextureIndex.EyesBaked:
                                case AvatarTextureIndex.HeadBaked:
                                case AvatarTextureIndex.LowerBaked:
                                case AvatarTextureIndex.SkirtBaked:
                                case AvatarTextureIndex.UpperBaked:
                                    type = ImageType.Baked;
                                    break;
                            }

                            Client.Assets.RequestImage(face.TextureID, type, Assets_OnImageReceived);
                        }
                    }
                }
            }
        }

        void Objects_OnNewPrim(object sender, PrimEventArgs e)
        {
            Primitive prim = e.Prim;

            if (enabled)
            {
                // Search this prim for textures
                for (int i = 0; i < prim.Textures.FaceTextures.Length; i++)
                {
                    Primitive.TextureEntryFace face = prim.Textures.FaceTextures[i];

                    if (face != null)
                    {
                        if (!alreadyRequested.ContainsKey(face.TextureID))
                        {
                            alreadyRequested[face.TextureID] = face.TextureID;
                            Client.Assets.RequestImage(face.TextureID, ImageType.Normal, Assets_OnImageReceived);
                        }
                    }
                }
            }
        }

        private void Assets_OnImageReceived(TextureRequestState state, AssetTexture asset)
        {
            if (state == TextureRequestState.Finished && enabled && alreadyRequested.ContainsKey(asset.AssetID))
            {
                if (state == TextureRequestState.Finished)
                    Logger.DebugLog(String.Format("Finished downloading texture {0} ({1} bytes)", asset.AssetID, asset.AssetData.Length));
                else
                    Logger.Log("Failed to download texture " + asset.AssetID + ": " + state, Helpers.LogLevel.Warning);
            }
        }
    }
}
