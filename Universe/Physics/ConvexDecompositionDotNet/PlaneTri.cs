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
using System.Diagnostics;

namespace Universe.Physics.ConvexDecompositionDotNet
{
    public enum PlaneTriResult : int
    {
        PTR_FRONT,
        PTR_BACK,
        PTR_SPLIT
    }

    public static class PlaneTri
    {
        private static float DistToPt(float3 p, float4 plane)
        {
            return p.x * plane.x + p.y * plane.y + p.z * plane.z + plane.w;
        }

        private static PlaneTriResult getSidePlane(float3 p, float4 plane, float epsilon)
        {
            float d = DistToPt(p, plane);

            if ((d + epsilon) > 0f)
                return PlaneTriResult.PTR_FRONT; // it is 'in front' within the provided epsilon value.

            return PlaneTriResult.PTR_BACK;
        }

        private static void add(float3 p, float3[] dest, ref int pcount)
        {
            dest[pcount++] = new float3(p);
            Debug.Assert(pcount <= 4);
        }

        // assumes that the points are on opposite sides of the plane!
        private static void intersect(float3 p1, float3 p2, float3 split, float4 plane)
        {
            float dp1 = DistToPt(p1, plane);
            float[] dir = new float[3];

            dir[0] = p2[0] - p1[0];
            dir[1] = p2[1] - p1[1];
            dir[2] = p2[2] - p1[2];

            float dot1 = dir[0] * plane[0] + dir[1] * plane[1] + dir[2] * plane[2];
            float dot2 = dp1 - plane[3];

            float t = -(plane[3] + dot2) / dot1;

            split.x = (dir[0] * t) + p1[0];
            split.y = (dir[1] * t) + p1[1];
            split.z = (dir[2] * t) + p1[2];
        }

        public static PlaneTriResult planeTriIntersection(float4 plane, FaceTri triangle, float epsilon, ref float3[] front, out int fcount, ref float3[] back, out int bcount)
        {
            fcount = 0;
            bcount = 0;

            // get the three vertices of the triangle.
            float3 p1 = triangle.P1;
            float3 p2 = triangle.P2;
            float3 p3 = triangle.P3;

            PlaneTriResult r1 = getSidePlane(p1, plane, epsilon); // compute the side of the plane each vertex is on
            PlaneTriResult r2 = getSidePlane(p2, plane, epsilon);
            PlaneTriResult r3 = getSidePlane(p3, plane, epsilon);

            if (r1 == r2 && r1 == r3) // if all three vertices are on the same side of the plane.
            {
                if (r1 == PlaneTriResult.PTR_FRONT) // if all three are in front of the plane, then copy to the 'front' output triangle.
                {
                    add(p1, front, ref fcount);
                    add(p2, front, ref fcount);
                    add(p3, front, ref fcount);
                }
                else
                {
                    add(p1, back, ref bcount); // if all three are in 'back' then copy to the 'back' output triangle.
                    add(p2, back, ref bcount);
                    add(p3, back, ref bcount);
                }
                return r1; // if all three points are on the same side of the plane return result
            }

            // ok.. we need to split the triangle at the plane.

            // First test ray segment P1 to P2
            if (r1 == r2) // if these are both on the same side...
            {
                if (r1 == PlaneTriResult.PTR_FRONT)
                {
                    add(p1, front, ref fcount);
                    add(p2, front, ref fcount);
                }
                else
                {
                    add(p1, back, ref bcount);
                    add(p2, back, ref bcount);
                }
            }
            else
            {
                float3 split = new float3();
                intersect(p1, p2, split, plane);

                if (r1 == PlaneTriResult.PTR_FRONT)
                {

                    add(p1, front, ref fcount);
                    add(split, front, ref fcount);

                    add(split, back, ref bcount);
                    add(p2, back, ref bcount);

                }
                else
                {
                    add(p1, back, ref bcount);
                    add(split, back, ref bcount);

                    add(split, front, ref fcount);
                    add(p2, front, ref fcount);
                }

            }

            // Next test ray segment P2 to P3
            if (r2 == r3) // if these are both on the same side...
            {
                if (r3 == PlaneTriResult.PTR_FRONT)
                {
                    add(p3, front, ref fcount);
                }
                else
                {
                    add(p3, back, ref bcount);
                }
            }
            else
            {
                float3 split = new float3(); // split the point
                intersect(p2, p3, split, plane);

                if (r3 == PlaneTriResult.PTR_FRONT)
                {
                    add(split, front, ref fcount);
                    add(split, back, ref bcount);

                    add(p3, front, ref fcount);
                }
                else
                {
                    add(split, front, ref fcount);
                    add(split, back, ref bcount);

                    add(p3, back, ref bcount);
                }
            }

            // Next test ray segment P3 to P1
            if (r3 != r1) // if both these are not on the same side...
            {
                float3 split = new float3(); // split the point
                intersect(p3, p1, split, plane);

                if (r1 == PlaneTriResult.PTR_FRONT)
                {
                    add(split, front, ref fcount);
                    add(split, back, ref bcount);

                    add (p3, back, ref bcount);
                }
                else
                {
                    add(split, front, ref fcount);
                    add(split, back, ref bcount);

                    add (p3, front, ref fcount);
                }
            }

            return PlaneTriResult.PTR_SPLIT;
        }
    }
}
