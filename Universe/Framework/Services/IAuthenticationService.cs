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

using OpenMetaverse;

namespace Universe.Framework.Services
{
    /// <summary>
    ///     Genric authentication service used for identifying
    ///     and authenticating principals.
    ///     Principals may be clients acting on users' behalf,
    ///     or any other components that need verifiable identification
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        ///     Authentication
        ///     These methods will return a token, which can be used to access
        ///     various services
        /// </summary>
        string Authenticate(UUID principalID, string authType, string password, int lifetime);


        /// <summary>
        ///     Verification
        ///     Allows verification of the authenticity of a token
        ///     Tokens expire after 30 minutes and can be refreshed by
        ///     re-verifying
        /// </summary>
        bool Verify(UUID principalID, string authType, string token, int lifetime);

        /// <summary>
        ///     Teardown
        ///     A token can be returned before the timeout. This
        ///     invalidates it and it can not subsequently be used or 
        ///     refreshed
        /// </summary>
        bool Release(UUID principalID, string authType, string token);

        /// <summary>
        ///     SetPassword for a principal
        ///     This method exists for the service, but may or may not
        ///     be served remotely.  That is, the authentication
        ///     handlers may not include one handler for this,
        ///     because it's a bit risky.  Such handlers require
        ///     authentication/ authorization
        /// </summary>
        bool SetPassword(UUID principalID, string authType, string passwd);
        bool SetPasswordHashed(UUID UUID, string authType, string passwd);
        bool SetPlainPassword(UUID principalID, string authType, string passwd);
        bool SetSaltedPassword (UUID principalID, string authType, string salt, string passwd);

        /// <summary>
        ///     Check whether the given principalID has a password set
        /// </summary>
        /// <param name="principalID"></param>
        /// <param name="authType"></param>
        /// <returns></returns>
        bool CheckExists(UUID principalID, string authType);
    }
}