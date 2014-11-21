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
using System.Text;
using System.IO;
using OpenMetaverse;
using OpenMetaverse.Rendering;

namespace PrimWorkshop
{
    public static class MeshToOBJ
    {
        public static bool MeshesToOBJ(List<FacetedMesh> meshes, string filename)
        {
            StringBuilder obj = new StringBuilder();
            StringBuilder mtl = new StringBuilder();

            FileInfo objFileInfo = new FileInfo(filename);

            string mtlFilename = objFileInfo.FullName.Substring(objFileInfo.DirectoryName.Length + 1,
                objFileInfo.FullName.Length - (objFileInfo.DirectoryName.Length + 1) - 4) + ".mtl";

            obj.AppendLine("# Created by Virtual Reality Department of Research and Development");
            obj.AppendLine("mtllib ./" + mtlFilename);
            obj.AppendLine();

            mtl.AppendLine("# Created by Virtual Reality Department of Research and Development");
            mtl.AppendLine();

            for (int i = 0; i < meshes.Count; i++)
            {
                FacetedMesh mesh = meshes[i];

                for (int j = 0; j < mesh.Faces.Count; j++)
                {
                    Face face = mesh.Faces[j];

                    if (face.Vertices.Count > 2)
                    {
                        string mtlName = String.Format("material{0}-{1}", i, face.ID);
                        Primitive.TextureEntryFace tex = face.TextureFace;
                        string texName = tex.TextureID.ToString() + ".tga";

                        // FIXME: Convert the source to TGA (if needed) and copy to the destination

                        float shiny = 0.00f;
                        switch (tex.Shiny)
                        {
                            case Shininess.High:
                                shiny = 1.00f;
                                break;
                            case Shininess.Medium:
                                shiny = 0.66f;
                                break;
                            case Shininess.Low:
                                shiny = 0.33f;
                                break;
                        }

                        mtl.AppendLine("newmtl " + mtlName);
                        mtl.AppendFormat("Ka {0} {1} {2}{3}", tex.RGBA.R, tex.RGBA.G, tex.RGBA.B, Environment.NewLine);
                        mtl.AppendFormat("Kd {0} {1} {2}{3}", tex.RGBA.R, tex.RGBA.G, tex.RGBA.B, Environment.NewLine);
                        //mtl.AppendFormat("Ks {0} {1} {2}{3}");
                        mtl.AppendLine("Tr " + tex.RGBA.A);
                        mtl.AppendLine("Ns " + shiny);
                        mtl.AppendLine("illum 1");
                        if (tex.TextureID != UUID.Zero && tex.TextureID != Primitive.TextureEntry.WHITE_TEXTURE)
                            mtl.AppendLine("map_Kd ./" + texName);
                        mtl.AppendLine();

                        #region Vertices

                        for (int k = 0; k < face.Vertices.Count; k++)
                        {
                            Vertex vertex = face.Vertices[k];
                            Vector3 pos = vertex.Position;
                            Vector3 norm = vertex.Normal;
                            Vector2 texc = vertex.TexCoord;

                            // Apply scaling
                            pos *= mesh.Prim.Scale;

                            // Apply rotation
                            pos *= mesh.Prim.Rotation;

                            // The root prim position is sim-relative, while child prim positions are
                            // parent-relative. We want to apply parent-relative translations but not
                            // sim-relative ones
                            if (mesh.Prim.ParentID != 0)
                                pos += mesh.Prim.Position;

                            // Normal
                            if (vertex.Normal.IsFinite())
                                obj.AppendFormat("vn {0} {1} {2}{3}",
                                    norm.X.ToString("N6"),
                                    norm.Y.ToString("N6"),
                                    norm.Z.ToString("N6"),
                                    Environment.NewLine);
                            else
                                obj.AppendLine("vn 0.0 1.0 0.0");

                            // Texture Coord
                            obj.AppendFormat("vt {0} {1}{2}",
                                texc.X.ToString("N6"),
                                texc.Y.ToString("N6"),
                                Environment.NewLine);

                            // Position
                            obj.AppendFormat("v {0} {1} {2}{3}",
                                pos.X.ToString("N6"),
                                pos.Y.ToString("N6"),
                                pos.Z.ToString("N6"),
                                Environment.NewLine);
                        }
                        obj.AppendLine();

                        #endregion Vertices
                    }
                }
            }

            int startOffset = 0;

            for (int i = 0; i < meshes.Count; i++)
            {
                FacetedMesh mesh = meshes[i];

                for (int j = 0; j < mesh.Faces.Count; j++)
                {
                    Face face = mesh.Faces[j];

                    if (face.Vertices.Count > 2)
                    {
                        //obj.AppendFormat("g face{0}-{1}{2}", i, face.ID, Environment.NewLine);

                        string mtlName = String.Format("material{0}-{1}", i, face.ID);
                        //obj.AppendLine("usemtl " + mtlName);

                        #region Elements

                        // Write all of the faces (triangles) for this side
                        for (int k = 0; k < face.Indices.Count / 3; k++)
                        {
                            obj.AppendFormat("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}{3}",
                                startOffset + face.Indices[k * 3 + 0] + 1,
                                startOffset + face.Indices[k * 3 + 1] + 1,
                                startOffset + face.Indices[k * 3 + 2] + 1,
                                Environment.NewLine);
                        }

                        obj.AppendFormat("# {0} faces{1}", face.Indices.Count / 3, Environment.NewLine);
                        obj.AppendLine();

                        for (int k = 0; k < face.Vertices.Count; k++)
                            ++startOffset;

                        #endregion Elements
                    }
                }
            }

            try
            {
                File.WriteAllText(filename, obj.ToString());
                File.WriteAllText(mtlFilename, mtl.ToString());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
