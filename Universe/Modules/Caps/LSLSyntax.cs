﻿/*
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

using Nini.Config;
using OpenMetaverse;
using OpenMetaverse.StructuredData;
using System;
using System.IO;
using Universe.Framework.Modules;
using Universe.Framework.SceneInfo;
using Universe.Framework.Servers.HttpServer;
using Universe.Framework.Servers.HttpServer.Implementation;
using Universe.Framework.Servers.HttpServer.Interfaces;
using Universe.Framework.Utilities;

namespace Universe.Modules.Caps
{
    public class LSLSyntax : INonSharedRegionModule
    {
        private IScene m_scene;

        #region INonSharedRegionModule Members

        public void Initialize(IConfigSource pSource)
        {
        }

        public void AddRegion(IScene scene)
        {
            m_scene = scene;
            m_scene.EventManager.OnRegisterCaps += RegisterCaps;
        }

        public void RemoveRegion(IScene scene)
        {
            m_scene.EventManager.OnRegisterCaps -= RegisterCaps;
        }

        public void RegionLoaded(IScene scene)
        {
        }

        public Type ReplaceableInterface
        {
            get { return null; }
        }

        public void Close()
        {
        }

        public string Name
        {
            get { return "LSLSyntax"; }
        }

        #endregion

        public OSDMap RegisterCaps(UUID agentID, IHttpServer server)
        {
            OSDMap retVal = new OSDMap();
            retVal["LSLSyntax"] = CapsUtil.CreateCAPS("LSLSyntax", "");

            server.AddStreamHandler(new GenericStreamHandler("POST", retVal["LSLSyntax"],
                                                 delegate(string path, Stream request,
                                                          OSHttpRequest httpRequest,
                                                          OSHttpResponse httpResponse)
                                                 { return ProcessLSLSyntax(request, httpResponse, agentID); }));

            return retVal;
        }

        private byte[] ProcessLSLSyntax(Stream request, OSHttpResponse httpResponse, UUID agentID)
        {
            // According to the information, this CAPS sends the LSL Syntax to the viewer
            // so new functions in LSL are reported correctly.
            //
            // After determining what the LSLSyntax Caps exactly does:
            //
            // * Request comes in to see what version the currency LSLSyntax.xml file is
            //
            // * If version of LSLSyntax.xml file is older, request the new version through the Caps
            //
            // * Including a bogus file for the time being in bin/Caps
            //
            // Fly-Man- 17 May 2015
            throw new NotImplementedException();
        }
    }
}
