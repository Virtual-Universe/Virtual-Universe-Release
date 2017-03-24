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
    public class float3x3
    {
        public float3 x = new float3();
        public float3 y = new float3();
        public float3 z = new float3();

        public float3x3()
        {
        }

        public float3x3(float xx, float xy, float xz, float yx, float yy, float yz, float zx, float zy, float zz)
        {
            x = new float3(xx, xy, xz);
            y = new float3(yx, yy, yz);
            z = new float3(zx, zy, zz);
        }

        public float3x3(float3 _x, float3 _y, float3 _z)
        {
            x = new float3(_x);
            y = new float3(_y);
            z = new float3(_z);
        }

        public float3 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        public float this[int i, int j]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        switch (j)
                        {
                            case 0: return x.x;
                            case 1: return x.y;
                            case 2: return x.z;
                        }
                        break;
                    case 1:
                        switch (j)
                        {
                            case 0: return y.x;
                            case 1: return y.y;
                            case 2: return y.z;
                        }
                        break;
                    case 2:
                        switch (j)
                        {
                            case 0: return z.x;
                            case 1: return z.y;
                            case 2: return z.z;
                        }
                        break;
                }
                throw new ArgumentOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 0:
                        switch (j)
                        {
                            case 0: x.x = value; return;
                            case 1: x.y = value; return;
                            case 2: x.z = value; return;
                        }
                        break;
                    case 1:
                        switch (j)
                        {
                            case 0: y.x = value; return;
                            case 1: y.y = value; return;
                            case 2: y.z = value; return;
                        }
                        break;
                    case 2:
                        switch (j)
                        {
                            case 0: z.x = value; return;
                            case 1: z.y = value; return;
                            case 2: z.z = value; return;
                        }
                        break;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        public static float3x3 Transpose(float3x3 m)
        {
            return new float3x3(new float3(m.x.x, m.y.x, m.z.x), new float3(m.x.y, m.y.y, m.z.y), new float3(m.x.z, m.y.z, m.z.z));
        }

        public static float3x3 operator *(float3x3 a, float3x3 b)
        {
            return new float3x3(a.x * b, a.y * b, a.z * b);
        }

        public static float3x3 operator *(float3x3 a, float s)
        {
            return new float3x3(a.x * s, a.y * s, a.z * s);
        }

        public static float3x3 operator /(float3x3 a, float s)
        {
            float t = 1f / s;
            return new float3x3(a.x * t, a.y * t, a.z * t);
        }

        public static float3x3 operator +(float3x3 a, float3x3 b)
        {
            return new float3x3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static float3x3 operator -(float3x3 a, float3x3 b)
        {
            return new float3x3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static float Determinant(float3x3 m)
        {
            return m.x.x * m.y.y * m.z.z + m.y.x * m.z.y * m.x.z + m.z.x * m.x.y * m.y.z - m.x.x * m.z.y * m.y.z - m.y.x * m.x.y * m.z.z - m.z.x * m.y.y * m.x.z;
        }

        public static float3x3 Inverse(float3x3 a)
        {
            float3x3 b = new float3x3();
            float d = Determinant(a);
            Debug.Assert(d != 0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int i1 = (i + 1) % 3;
                    int i2 = (i + 2) % 3;
                    int j1 = (j + 1) % 3;
                    int j2 = (j + 2) % 3;

                    // reverse indexs i&j to take transpose
                    b[i, j] = (a[i1][j1] * a[i2][j2] - a[i1][j2] * a[i2][j1]) / d;
                }
            }
            return b;
        }
    }
}
