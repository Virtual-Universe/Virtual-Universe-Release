/*
 * Copyright (c) Contributors, http://virtual-planets.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 * For an explanation of the license of each contributor and the content it 
 * covers please see the Licenses directory.
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

using System;
using System.Collections.Generic;

namespace Universe.Physics.ConvexDecompositionDotNet
{
    public class HullResult
    {
        public bool Polygons = true; // true if indices represents polygons, false indices are triangles
        public List<float3> OutputVertices = new List<float3>();
        public List<int> Indices;

        // If triangles, then indices are array indexes into the vertex list.
        // If polygons, indices are in the form (number of points in face) (p1, p2, p3, ..) etc..
    }

    public class PHullResult
    {
        public List<float3> Vertices = new List<float3>();
        public List<int> Indices = new List<int>();
    }

    [Flags]
    public enum HullFlag : int
    {
        QF_DEFAULT = 0,
        QF_TRIANGLES = (1 << 0), // report results as triangles, not polygons.
        QF_SKIN_WIDTH = (1 << 2) // extrude hull based on this skin width
    }

    public enum HullError : int
    {
        QE_OK, // success!
        QE_FAIL // failed.
    }

    public class HullDesc
    {
        public HullFlag Flags; // flags to use when generating the convex hull.
        public List<float3> Vertices;
        public float NormalEpsilon; // the epsilon for removing duplicates. This is a normalized value, if normalized bit is on.
        public float SkinWidth;
        public uint MaxVertices; // maximum number of vertices to be considered for the hull!
        public uint MaxFaces;

        public HullDesc()
        {
            Flags = HullFlag.QF_DEFAULT;
            Vertices = new List<float3>();
            NormalEpsilon = 0.001f;
            MaxVertices = 4096;
            MaxFaces = 4096;
            SkinWidth = 0.01f;
        }

        public HullDesc(HullFlag flags, List<float3> vertices)
        {
            Flags = flags;
            Vertices = new List<float3>(vertices);
            NormalEpsilon = 0.001f;
            MaxVertices = 4096;
            MaxFaces = 4096;
            SkinWidth = 0.01f;
        }

        public bool HasHullFlag(HullFlag flag)
        {
            return (Flags & flag) != 0;
        }

        public void SetHullFlag(HullFlag flag)
        {
            Flags |= flag;
        }

        public void ClearHullFlag(HullFlag flag)
        {
            Flags &= ~flag;
        }
    }

    public class ConvexH
    {
        public struct HalfEdge
        {
            public short ea; // the other half of the edge (index into edges list)
            public byte v; // the vertex at the start of this edge (index into vertices list)
            public byte p; // the facet on which this edge lies (index into facets list)

            public HalfEdge(short _ea, byte _v, byte _p)
            {
                ea = _ea;
                v = _v;
                p = _p;
            }

            public HalfEdge(HalfEdge e)
            {
                ea = e.ea;
                v = e.v;
                p = e.p;
            }
        }

        public List<float3> vertices = new List<float3>();
        public List<HalfEdge> edges = new List<HalfEdge>();
        public List<Plane> facets = new List<Plane>();

        public ConvexH(int vertices_size, int edges_size, int facets_size)
        {
            vertices = new List<float3>(vertices_size);
            edges = new List<HalfEdge>(edges_size);
            facets = new List<Plane>(facets_size);
        }
    }

    public class VertFlag
    {
        public byte planetest;
        public byte junk;
        public byte undermap;
        public byte overmap;
    }

    public class EdgeFlag
    {
        public byte planetest;
        public byte fixes;
        public short undermap;
        public short overmap;
    }

    public class PlaneFlag
    {
        public byte undermap;
        public byte overmap;
    }

    public class Coplanar
    {
        public ushort ea;
        public byte v0;
        public byte v1;
    }
}
