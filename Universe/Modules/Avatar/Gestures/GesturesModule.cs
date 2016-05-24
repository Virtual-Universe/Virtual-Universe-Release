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


using Universe.Framework.ConsoleFramework;
using Universe.Framework.Modules;
using Universe.Framework.PresenceInfo;
using Universe.Framework.SceneInfo;
using Universe.Framework.Services;
using Universe.Framework.Services.ClassHelpers.Inventory;
using Universe.Framework.Utilities;
using Nini.Config;
using OpenMetaverse;
using System;

namespace Universe.Modules.Gestures
{
    public class GesturesModule : INonSharedRegionModule
    {
        protected IScene m_scene;

        #region INonSharedRegionModule Members

        public void Initialize(IConfigSource source)
        {
        }

        public void AddRegion(IScene scene)
        {
            m_scene = scene;

            m_scene.EventManager.OnNewClient += OnNewClient;
            m_scene.EventManager.OnClosingClient += OnClosingClient;
        }

        public void RemoveRegion(IScene scene)
        {
            m_scene.EventManager.OnNewClient -= OnNewClient;
            m_scene.EventManager.OnClosingClient -= OnClosingClient;
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
            get { return "Gestures Module"; }
        }

        #endregion

        private void OnNewClient(IClientAPI client)
        {
            client.OnActivateGesture += ActivateGesture;
            client.OnDeactivateGesture += DeactivateGesture;
        }

        private void OnClosingClient(IClientAPI client)
        {
            client.OnActivateGesture -= ActivateGesture;
            client.OnDeactivateGesture -= DeactivateGesture;
        }

        public virtual void ActivateGesture(IClientAPI client, UUID assetId, UUID gestureId)
        {
            IInventoryService invService = m_scene.InventoryService;
			UUID libOwner = new UUID (Constants.LibraryOwner);

            InventoryItemBase item = invService.GetItem(client.AgentId, gestureId);
            if (item != null)
            {
                item.Flags |= (uint) 1;
                invService.UpdateItem(item);
            }
            else {
				if(invService.GetItem(libOwner, gestureId) == null) {
					MainConsole.Instance.WarnFormat(
						"[GESTURES]: Unable to find gesture {0} to activate for {1}", gestureId, client.Name);
				}
			}
        }

        public virtual void DeactivateGesture(IClientAPI client, UUID gestureId)
        {
            IInventoryService invService = m_scene.InventoryService;
			UUID libOwner = new UUID (Constants.LibraryOwner);

            InventoryItemBase item = invService.GetItem(client.AgentId, gestureId);
            if (item != null)
            {
                item.Flags &= ~(uint) 1;
                invService.UpdateItem(item);
            }
            else
				if(invService.GetItem(libOwner, gestureId) == null) {
					MainConsole.Instance.ErrorFormat(
						"[GESTURES]: Unable to find gesture to deactivate {0} for {1}", gestureId, client.Name);
				}
        }
    }
}