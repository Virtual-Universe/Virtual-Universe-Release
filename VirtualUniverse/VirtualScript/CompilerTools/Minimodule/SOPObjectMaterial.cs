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
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    internal class SOPObjectMaterial : MarshalByRefObject, IObjectMaterial
    {
        private readonly int m_face;
        private readonly ISceneChildEntity m_parent;

        public SOPObjectMaterial(int m_face, ISceneChildEntity m_parent)
        {
            this.m_face = m_face;
            this.m_parent = m_parent;
        }

        #region IObjectMaterial Members

        public Color Color
        {
            get
            {
                Color4 res = GetTexface().RGBA;
                return Color.FromArgb((int)(res.A * 255), (int)(res.R * 255), (int)(res.G * 255), (int)(res.B * 255));
            }
            set
            {
                Primitive.TextureEntry tex = m_parent.Shape.Textures;
                Primitive.TextureEntryFace texface = tex.CreateFace((uint)m_face);
                texface.RGBA = new Color4(value.R, value.G, value.B, value.A);
                tex.FaceTextures[m_face] = texface;
                m_parent.UpdateTexture(tex, false);
            }
        }

        public UUID Texture
        {
            get
            {
                Primitive.TextureEntryFace texface = GetTexface();
                return texface.TextureID;
            }
            set
            {
                Primitive.TextureEntry tex = m_parent.Shape.Textures;
                Primitive.TextureEntryFace texface = tex.CreateFace((uint)m_face);
                texface.TextureID = value;
                tex.FaceTextures[m_face] = texface;
                m_parent.UpdateTexture(tex, false);
            }
        }

        public TextureMapping Mapping
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool Bright
        {
            get { return GetTexface().Fullbright; }
            set
            {
                Primitive.TextureEntry tex = m_parent.Shape.Textures;
                Primitive.TextureEntryFace texface = tex.CreateFace((uint)m_face);
                texface.Fullbright = value;
                tex.FaceTextures[m_face] = texface;
                m_parent.UpdateTexture(tex, false);
            }
        }

        public double Bloom
        {
            get { return GetTexface().Glow; }
            set
            {
                Primitive.TextureEntry tex = m_parent.Shape.Textures;
                Primitive.TextureEntryFace texface = tex.CreateFace((uint)m_face);
                texface.Glow = (float)value;
                tex.FaceTextures[m_face] = texface;
                m_parent.UpdateTexture(tex, false);
            }
        }

        public bool Shiny
        {
            get { return GetTexface().Shiny != Shininess.None; }
            set
            {
                Primitive.TextureEntry tex = m_parent.Shape.Textures;
                Primitive.TextureEntryFace texface = tex.CreateFace((uint)m_face);
                texface.Shiny = value ? Shininess.High : Shininess.None;
                tex.FaceTextures[m_face] = texface;
                m_parent.UpdateTexture(tex, false);
            }
        }

        public bool BumpMap
        {
            get { return GetTexface().Bump == Bumpiness.None; }
            set { throw new NotImplementedException(); }
        }

        #endregion

        private Primitive.TextureEntryFace GetTexface()
        {
            Primitive.TextureEntry tex = m_parent.Shape.Textures;
            return tex.GetFace((uint)m_face);
        }
    }
}