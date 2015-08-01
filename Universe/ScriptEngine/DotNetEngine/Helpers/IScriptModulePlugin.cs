/*
 * Copyright (c) Contributors, http://virtual-planets.org/, http://aurora-sim.org, http://opensimulator.org/, http://aurora-sim.org
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
using Universe.Framework.Modules;
using Universe.Framework.SceneInfo;
using Nini.Config;
using OpenMetaverse;

namespace Universe.ScriptEngine.DotNetEngine
{
    public interface IScriptModulePlugin : IScriptModule
    {
        IConfig Config { get; }

        IConfigSource ConfigSource { get; }

        IScriptModule ScriptModule { get; }
        Dictionary<Type, object> Extensions { get; }

        bool PostScriptEvent(UUID m_itemID, UUID uUID, EventParams EventParams, EventPriority EventPriority);

        void SetState(UUID m_itemID, string newState);

        void SetScriptRunningState(UUID item, bool p);

        IScriptPlugin GetScriptPlugin(string p);

        DetectParams GetDetectParams(UUID uUID, UUID m_itemID, int number);

        void ResetScript(UUID uUID, UUID m_itemID, bool p);

        bool GetScriptRunningState(UUID item);

        int GetStartParameter(UUID m_itemID, UUID uUID);

        void SetMinEventDelay(UUID m_itemID, UUID uUID, double delay);

        IScriptApi GetApi(UUID m_itemID, string p);

        bool PipeEventsForScript(ISceneChildEntity m_host, Vector3 vector3);

        void RegisterExtension<T>(T instance);
    }

    public class EventInfo
    {
        public string Name;
        public string[] ArgumentTypes;
        public EventInfo(string name, string[] types) { Name = name; ArgumentTypes = types; }
    }
}