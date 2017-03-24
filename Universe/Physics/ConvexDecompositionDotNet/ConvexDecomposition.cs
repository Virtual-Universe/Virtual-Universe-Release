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
using System.Diagnostics;

namespace Universe.Physics.ConvexDecompositionDotNet
{
    public delegate void ConvexDecompositionCallback(ConvexResult result);

    public class FaceTri
    {
        public float3 P1;
        public float3 P2;
        public float3 P3;

        public FaceTri() { }

        public FaceTri(List<float3> vertices, int i1, int i2, int i3)
        {
            P1 = new float3(vertices[i1]);
            P2 = new float3(vertices[i2]);
            P3 = new float3(vertices[i3]);
        }
    }

    public static class ConvexDecomposition
    {
        private static void addTri(VertexPool vl, List<int> list, float3 p1, float3 p2, float3 p3)
        {
            int i1 = vl.getIndex(p1);
            int i2 = vl.getIndex(p2);
            int i3 = vl.getIndex(p3);

            // do *not* process degenerate triangles!
            if ( i1 != i2 && i1 != i3 && i2 != i3 )
            {
                list.Add(i1);
                list.Add(i2);
                list.Add(i3);
            }
        }

        public static void calcConvexDecomposition(List<float3> vertices, List<int> indices, ConvexDecompositionCallback callback, float masterVolume, int depth,
            int maxDepth, float concavePercent, float mergePercent)
        {
            float4 plane = new float4();
            bool split = false;

            if (depth < maxDepth)
            {
                float volume = 0f;
                float c = Concavity.computeConcavity(vertices, indices, ref plane, ref volume);

                if (depth == 0)
                {
                    masterVolume = volume;
                }

                float percent = (c * 100.0f) / masterVolume;

                if (percent > concavePercent) // if great than 5% of the total volume is concave, go ahead and keep splitting.
                {
                    split = true;
                }
            }

            if (depth >= maxDepth || !split)
            {
                HullResult result = new HullResult();
                HullDesc desc = new HullDesc();

                desc.SetHullFlag(HullFlag.QF_TRIANGLES);

                desc.Vertices = vertices;

                HullError ret = HullUtils.CreateConvexHull(desc, ref result);

                if (ret == HullError.QE_OK)
                {
                    ConvexResult r = new ConvexResult(result.OutputVertices, result.Indices);
                    callback(r);
                }

                return;
            }

            List<int> ifront = new List<int>();
            List<int> iback = new List<int>();

            VertexPool vfront = new VertexPool();
            VertexPool vback = new VertexPool();

            // ok..now we are going to 'split' all of the input triangles against this plane!
            for (int i = 0; i < indices.Count / 3; i++)
            {
                int i1 = indices[i * 3 + 0];
                int i2 = indices[i * 3 + 1];
                int i3 = indices[i * 3 + 2];

                FaceTri t = new FaceTri(vertices, i1, i2, i3);

                float3[] front = new float3[4];
                float3[] back = new float3[4];

                int fcount = 0;
                int bcount = 0;

                PlaneTriResult result = PlaneTri.planeTriIntersection(plane, t, 0.00001f, ref front, out fcount, ref back, out bcount);

                if (fcount > 4 || bcount > 4)
                {
                    result = PlaneTri.planeTriIntersection(plane, t, 0.00001f, ref front, out fcount, ref back, out bcount);
                }

                switch (result)
                {
                    case PlaneTriResult.PTR_FRONT:
                        Debug.Assert(fcount == 3);
                        addTri(vfront, ifront, front[0], front[1], front[2]);
                        break;
                    case PlaneTriResult.PTR_BACK:
                        Debug.Assert(bcount == 3);
                        addTri(vback, iback, back[0], back[1], back[2]);
                        break;
                    case PlaneTriResult.PTR_SPLIT:
                        Debug.Assert(fcount >= 3 && fcount <= 4);
                        Debug.Assert(bcount >= 3 && bcount <= 4);

                        addTri(vfront, ifront, front[0], front[1], front[2]);
                        addTri(vback, iback, back[0], back[1], back[2]);

                        if (fcount == 4)
                        {
                            addTri(vfront, ifront, front[0], front[2], front[3]);
                        }

                        if (bcount == 4)
                        {
                            addTri(vback, iback, back[0], back[2], back[3]);
                        }

                        break;
                }
            }

            // ok... here we recursively call
            if (ifront.Count > 0)
            {
// 20131224 not used                int vcount = vfront.GetSize();
                List<float3> vertices2 = vfront.GetVertices();
                for (int i = 0; i < vertices2.Count; i++)
                    vertices2[i] = new float3(vertices2[i]);
// 20131224 not used                int tcount = ifront.Count / 3;

                calcConvexDecomposition(vertices2, ifront, callback, masterVolume, depth + 1, maxDepth, concavePercent, mergePercent);
            }

            ifront.Clear();
            vfront.Clear();

            if (iback.Count > 0)
            {
// 20131224 not used                int vcount = vback.GetSize();
                List<float3> vertices2 = vback.GetVertices();
// 20131224 not used                int tcount = iback.Count / 3;

                calcConvexDecomposition(vertices2, iback, callback, masterVolume, depth + 1, maxDepth, concavePercent, mergePercent);
            }

            iback.Clear();
            vback.Clear();
        }
    }
}
