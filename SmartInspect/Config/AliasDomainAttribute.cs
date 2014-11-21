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

// .NET Compact Framework 1.0 has no support for reading assembly attributes
#if !NETCF

using System;

namespace SmartInspect.Config
{
	/// <summary>
	/// Assembly level attribute that specifies a domain to alias to this assembly's repository.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>AliasDomainAttribute is obsolete. Use AliasRepositoryAttribute instead of AliasDomainAttribute.</b>
	/// </para>
	/// <para>
	/// An assembly's logger repository is defined by its <see cref="DomainAttribute"/>,
	/// however this can be overridden by an assembly loaded before the target assembly.
	/// </para>
	/// <para>
	/// An assembly can alias another assembly's domain to its repository by
	/// specifying this attribute with the name of the target domain.
	/// </para>
	/// <para>
	/// This attribute can only be specified on the assembly and may be used
	/// as many times as necessary to alias all the required domains.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	[AttributeUsage(AttributeTargets.Assembly,AllowMultiple=true)]
	[Serializable]
	[Obsolete("Use AliasRepositoryAttribute instead of AliasDomainAttribute")]
	public sealed class AliasDomainAttribute : AliasRepositoryAttribute
	{
		#region Public Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AliasDomainAttribute" /> class with 
		/// the specified domain to alias to this assembly's repository.
		/// </summary>
		/// <param name="name">The domain to alias to this assemby's repository.</param>
		/// <remarks>
		/// <para>
		/// Obsolete. Use <see cref="AliasRepositoryAttribute"/> instead of <see cref="AliasDomainAttribute"/>.
		/// </para>
		/// </remarks>
		public AliasDomainAttribute(string name) : base(name)
		{
		}

		#endregion Public Instance Constructors
	}
}

#endif // !NETCF