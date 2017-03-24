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

namespace Universe.Physics.ConvexDecompositionDotNet
{
    public class Plane
    {
        public float3 normal = new float3();
        public float dist; // distance below origin - the D from plane equasion Ax+By+Cz+D=0

        public Plane(float3 n, float d)
        {
            normal = new float3(n);
            dist = d;
        }

        public Plane(Plane p)
        {
            normal = new float3(p.normal);
            dist = p.dist;
        }

        public Plane()
        {
            dist = 0;
        }

        public void Transform(float3 position, Quaternion orientation)
        {
            //   Transforms the plane to the space defined by the
            //   given position/orientation
            float3 newNormal = Quaternion.Inverse(orientation) * normal;
            float3 origin = Quaternion.Inverse(orientation) * (-normal * dist - position);

            normal = newNormal;
            dist = -float3.dot(newNormal, origin);
        }

        public override int GetHashCode()
        {
            return normal.GetHashCode() ^ dist.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Plane p = obj as Plane;
            if (p == null)
                return false;

            return this == p;
        }

        public static bool operator ==(Plane a, Plane b)
        {
            return (a.normal == b.normal && a.dist == b.dist);
        }

        public static bool operator !=(Plane a, Plane b)
        {
            return !(a == b);
        }

        public static Plane PlaneFlip(Plane plane)
        {
            return new Plane(-plane.normal, -plane.dist);
        }

        public static bool coplanar(Plane a, Plane b)
        {
            return (a == b || a == PlaneFlip(b));
        }
    }
}
