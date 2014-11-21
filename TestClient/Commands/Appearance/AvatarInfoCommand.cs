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
using System.Text;
using OpenMetaverse;

namespace OpenMetaverse.TestClient.Commands.Appearance
{
    public class AvatarInfoCommand : Command
    {
        public AvatarInfoCommand(TestClient testClient)
        {
            Name = "avatarinfo";
            Description = "Print out information on a nearby avatar. Usage: avatarinfo [firstname] [lastname]";
            Category = CommandCategory.Appearance;
        }

        public override string Execute(string[] args, UUID fromAgentID)
        {
            if (args.Length != 2)
                return "Usage: avatarinfo [firstname] [lastname]";

            string targetName = String.Format("{0} {1}", args[0], args[1]);

            Avatar foundAv = Client.Network.CurrentSim.ObjectsAvatars.Find(
                delegate(Avatar avatar) { return (avatar.Name == targetName); }
            );

            if (foundAv != null)
            {
                StringBuilder output = new StringBuilder();

                output.AppendFormat("{0} ({1})", targetName, foundAv.ID);
                output.AppendLine();

                for (int i = 0; i < foundAv.Textures.FaceTextures.Length; i++)
                {
                    if (foundAv.Textures.FaceTextures[i] != null)
                    {
                        Primitive.TextureEntryFace face = foundAv.Textures.FaceTextures[i];
                        AvatarTextureIndex type = (AvatarTextureIndex)i;

                        output.AppendFormat("{0}: {1}", type, face.TextureID);
                        output.AppendLine();
                    }
                }

                return output.ToString();
            }
            else
            {
                return "No nearby avatar with the name " + targetName;
            }
        }
    }
}
