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

using System;
using System.Collections.Generic;
using Nini.Config;
using OpenMetaverse;
using Universe.Framework.Modules;
using Universe.Framework.SceneInfo;

namespace Universe.Modules.GlobalEnvironment.Plugins
{
    public class SimpleRandomWind : IWindModelPlugin
    {
        readonly Random m_rndnums = new Random (Environment.TickCount);
        float m_strength = 1.0f;
        Vector2[] m_windSpeeds = new Vector2[16 * 16];

        public string Version
        {
            get { return "1.0.0.0"; }
        }

        #region IWindModelPlugin Members

        public string Name
        {
            get { return "SimpleRandomWind"; }
        }

        public void Initialize ()
        {
        }

        public void WindConfig (IScene scene, IConfig windConfig)
        {
            if (windConfig != null)
            {
                if (windConfig.Contains ("strength"))
                {
                    m_strength = windConfig.GetFloat ("strength", 1.0F);
                }
            }
        }

        public void WindUpdate (uint frame)
        {
            //Make sure our object is valid (we haven't been disposed of yet)
            if (m_windSpeeds != null)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        m_windSpeeds [y * 16 + x].X = (float)(m_rndnums.NextDouble () * 2d - 1d); // -1 to 1
                        m_windSpeeds [y * 16 + x].Y = (float)(m_rndnums.NextDouble () * 2d - 1d); // -1 to 1
                        m_windSpeeds [y * 16 + x].X *= m_strength;
                        m_windSpeeds [y * 16 + x].Y *= m_strength;
                    }
                }
            }
        }

        public Vector3 WindSpeed (float fX, float fY, float fZ)
        {
            Vector3 windVector = new Vector3 (0.0f, 0.0f, 0.0f);

            int x = (int)fX / 16;
            int y = (int)fY / 16;

            if (x < 0)
                x = 0;
            if (x > 15)
                x = 15;
            if (y < 0)
                y = 0;
            if (y > 15)
                y = 15;

            if (m_windSpeeds != null)
            {
                windVector.X = m_windSpeeds [y * 16 + x].X;
                windVector.Y = m_windSpeeds [y * 16 + x].Y;
            }

            return windVector;
        }

        public Vector2[] WindLLClientArray ()
        {
            return m_windSpeeds;
        }

        public string Description
        {
            get { return "Provides a simple wind model that creates random wind of a given strength in 16m x 16m patches."; }
        }

        public Dictionary<string, string> WindParams ()
        {
            Dictionary<string, string> Params = new Dictionary<string, string> { { "strength", "wind strength" } };

            return Params;
        }

        public void WindParamSet (string param, float value)
        {
            switch (param)
            {
            case "strength":
                m_strength = value;
                break;
            }
        }

        public float WindParamGet (string param)
        {
            switch (param)
            {
            case "strength":
                return m_strength;
            default:
                throw new Exception (String.Format ("Unknown {0} parameter {1}", Name, param));
            }
        }

        #endregion

        public void Dispose ()
        {
            m_windSpeeds = null;
        }
    }
}