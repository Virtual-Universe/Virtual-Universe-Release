/***************************************************************************
 *	                VIRTUAL REALITY PUBLIC SOURCE LICENSE
 * 
 * Date				: Sun January 1, 2006
 * Copyright		: (c) 2006-2014 by Virtual Reality Development Team. 
 *                    All Rights Reserved.
 * Website			: http://www.syndarveruleiki.is
 *
 * Product Name		: Virtual Reality
 * License Text     : packages/docs/VRLICENSE.txt
 * 
 * Planetary Info   : Information about the Planetary code
 * 
 * Copyright        : (c) 2014-2024 by Second Galaxy Development Team
 *                    All Rights Reserved.
 * 
 * Website          : http://www.secondgalaxy.com
 * 
 * Product Name     : Virtual Reality
 * License Text     : packages/docs/SGLICENSE.txt
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the WhiteCore-Sim Project nor the
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
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using OpenMetaverse;
using OpenMetaverse.Packets;

namespace OpenMetaverse.TestClient
{
    public class VoiceAccountCommand : Command
    {
        private AutoResetEvent ProvisionEvent = new AutoResetEvent(false);
        private string VoiceAccount = null;
        private string VoicePassword = null;

        public VoiceAccountCommand(TestClient testClient)
        {
            Name = "voiceaccount";
            Description = "obtain voice account info. Usage: voiceaccount";
            Category = CommandCategory.Voice;

            Client = testClient;
        }

        private bool registered = false;

        private bool IsVoiceManagerRunning()
        {
            if (null == Client.VoiceManager) return false;

            if (!registered)
            {
                Client.VoiceManager.OnProvisionAccount += Voice_OnProvisionAccount;
                registered = true;
            }
            return true;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (!IsVoiceManagerRunning())
                return String.Format("VoiceManager not running for {0}", Client.Self.Name);

            if (!Client.VoiceManager.RequestProvisionAccount())
            {
                return "RequestProvisionAccount failed. Not available for the current grid?";
            }
            ProvisionEvent.WaitOne(30 * 1000, false);

            if (String.IsNullOrEmpty(VoiceAccount) && String.IsNullOrEmpty(VoicePassword))
            {
                return String.Format("Voice account information lookup for {0} failed.", Client.Self.Name);
            }

            return String.Format("Voice Account for {0}: user \"{1}\", password \"{2}\"",
                                 Client.Self.Name, VoiceAccount, VoicePassword);
        }

        void Voice_OnProvisionAccount(string username, string password)
        {
            VoiceAccount = username;
            VoicePassword = password;

            ProvisionEvent.Set();
        }
    }
}