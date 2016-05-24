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


using Universe.Framework.Modules;
using Universe.Framework.PresenceInfo;
using Universe.Framework.SceneInfo;
using Universe.Framework.Servers;
using Universe.Framework.Servers.HttpServer;
using Universe.Framework.Servers.HttpServer.Implementation;
using Universe.Framework.Servers.HttpServer.Interfaces;
using Universe.Framework.Utilities;
using Nini.Config;
using OpenMetaverse;
using OpenMetaverse.StructuredData;
using System;
using System.IO;
using Universe.Framework.SceneInfo.Entities;
using Universe.Region;


namespace Universe.Modules.Caps
{
    public class ResourceCostSelected : INonSharedRegionModule
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
            get { return "ResourceCostSelected"; }
        }

        #endregion

        public OSDMap RegisterCaps(UUID agentID, IHttpServer server)
        {
            OSDMap retVal = new OSDMap();
            retVal["ResourceCostSelected"] = CapsUtil.CreateCAPS("ResourceCostSelected", "");

            server.AddStreamHandler(new GenericStreamHandler("POST", retVal["ResourceCostSelected"],
                                                             delegate(string path, Stream request,
                                                                      OSHttpRequest httpRequest,
                                                                      OSHttpResponse httpResponse)
                                                             { return ProcessResourceCostSelected(request, agentID); }));
            return retVal;
        }

        public byte[] ProcessResourceCostSelected(Stream request, UUID AgentId)
        {
            IScenePresence avatar;

            if (!m_scene.TryGetScenePresence(AgentId, out avatar))
                return MainServer.BadRequest;
            
            OSD r = OSDParser.DeserializeLLSDXml(HttpServerHandlerHelpers.ReadFully(request));

            if (r.Type != OSDType.Map) // not a proper request
                return MainServer.BadRequest;

			// This module gets the root of the prim(set)
			// What needs to be done is to check how many prims there are selected (multiple selected_roots)
            // and if they are part of a link-set
			//
            // update check:
            //  http://wiki.secondlife.com/wiki/Mesh/Mesh_Server_Weight
            //  http://wiki.secondlife.com/wiki/Mesh/Mesh_physics
            //  http://wiki.secondlife.com/wiki/Mesh/Mesh_Streaming_Cost
            //
			// Each prim has a standard amount of details
			//
			// physics = 0.1 x amount of prims
            // prim_equiv = 1 x amount of prims // update to // MIN{ (0.5*num_prims) + (0.25 * num_scripts), num_prims }
			// simulation = 0.5 x amount of prims
			// streaming = 0.06 x amount of prims
			//
			// These values need to be returned with the return that's underneath here

            int primCount = 0;

            ISceneEntity[] entityList = m_scene.Entities.GetEntities ();
            foreach (ISceneEntity ent in entityList)
            {
                if (ent.IsSelected)
                {
                    primCount++;
                    if (ent is SceneObjectGroup)
                    {
                        var entParts = ent.ChildrenEntities ();
                        //MainConsole.Instance.Info ("Object: " + ent.Name + " at position " + ent.AbsolutePosition + ", comprised of " + entParts.Count + " parts");
                        primCount += entParts.Count;
                    }
                }
            }

            OSDMap map = new OSDMap();
            map["prim_equiv"] = primCount;
            map["physics"] = 0.1f * primCount;
            map["simulation"] = 0.5f * primCount;
            map["streaming"] = 0.06f * primCount;

            return OSDParser.SerializeLLSDXmlBytes(map);
        }
    }
}