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
using OpenMetaverse;

namespace OpenMetaverse.TestClient
{
    public class ShowEffectsCommand : Command
    {
        bool ShowEffects = false;

        public ShowEffectsCommand(TestClient testClient)
        {
            Name = "showeffects";
            Description = "Prints out information for every viewer effect that is received. Usage: showeffects [on/off]";
            Category = CommandCategory.Other;

            testClient.Avatars.ViewerEffect += new EventHandler<ViewerEffectEventArgs>(Avatars_ViewerEffect);
            testClient.Avatars.ViewerEffectPointAt += new EventHandler<ViewerEffectPointAtEventArgs>(Avatars_ViewerEffectPointAt);
            testClient.Avatars.ViewerEffectLookAt += new EventHandler<ViewerEffectLookAtEventArgs>(Avatars_ViewerEffectLookAt);            
        }

        void Avatars_ViewerEffectLookAt(object sender, ViewerEffectLookAtEventArgs e)
        {
            if (ShowEffects)
                Console.WriteLine(
                "ViewerEffect [LookAt]: SourceID: {0} TargetID: {1} TargetPos: {2} Type: {3} Duration: {4} ID: {5}",
                e.SourceID.ToString(), e.TargetID.ToString(), e.TargetPosition, e.LookType, e.Duration,
                e.EffectID.ToString());
        }

        void Avatars_ViewerEffectPointAt(object sender, ViewerEffectPointAtEventArgs e)
        {
            if (ShowEffects)
                Console.WriteLine(
                "ViewerEffect [PointAt]: SourceID: {0} TargetID: {1} TargetPos: {2} Type: {3} Duration: {4} ID: {5}",
                e.SourceID.ToString(), e.TargetID.ToString(), e.TargetPosition, e.PointType, e.Duration,
                e.EffectID.ToString());
        }

        void Avatars_ViewerEffect(object sender, ViewerEffectEventArgs e)
        {
            if (ShowEffects)
                Console.WriteLine(
                "ViewerEffect [{0}]: SourceID: {1} TargetID: {2} TargetPos: {3} Duration: {4} ID: {5}",
                e.Type, e.SourceID.ToString(), e.TargetID.ToString(), e.TargetPosition, e.Duration,
                e.EffectID.ToString());
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length == 0)
            {
                ShowEffects = true;
                return "Viewer effects will be shown on the console";
            }
            else if (args.Length == 1)
            {
                if (args[0] == "on")
                {
                    ShowEffects = true;
                    return "Viewer effects will be shown on the console";
                }
                else
                {
                    ShowEffects = false;
                    return "Viewer effects will not be shown";
                }
            }
            else
            {
                return "Usage: showeffects [on/off]";
            }
        }        

    }
}
