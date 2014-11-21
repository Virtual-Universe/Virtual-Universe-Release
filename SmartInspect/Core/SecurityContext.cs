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

namespace SmartInspect.Core
{
	/// <summary>
	/// A SecurityContext used by SmartInspect when interacting with protected resources
	/// </summary>
	/// <remarks>
	/// <para>
	/// A SecurityContext used by SmartInspect when interacting with protected resources
	/// for example with operating system services. This can be used to impersonate
	/// a principal that has been granted privileges on the system resources.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public abstract class SecurityContext
	{
		/// <summary>
		/// Impersonate this SecurityContext
		/// </summary>
		/// <param name="state">State supplied by the caller</param>
		/// <returns>An <see cref="IDisposable"/> instance that will
		/// revoke the impersonation of this SecurityContext, or <c>null</c></returns>
		/// <remarks>
		/// <para>
		/// Impersonate this security context. Further calls on the current
		/// thread should now be made in the security context provided
		/// by this object. When the <see cref="IDisposable"/> result 
		/// <see cref="IDisposable.Dispose"/> method is called the security
		/// context of the thread should be reverted to the state it was in
		/// before <see cref="Impersonate"/> was called.
		/// </para>
		/// </remarks>
		public abstract IDisposable Impersonate(object state);
	}
}
