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
	/// Assembly level attribute that specifies the logging domain for the assembly.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>DomainAttribute is obsolete. Use RepositoryAttribute instead of DomainAttribute.</b>
	/// </para>
	/// <para>
	/// Assemblies are mapped to logging domains. Each domain has its own
	/// logging repository. This attribute specified on the assembly controls
	/// the configuration of the domain. The <see cref="RepositoryAttribute.Name"/> property specifies the name
	/// of the domain that this assembly is a part of. The <see cref="RepositoryAttribute.RepositoryType"/>
	/// specifies the type of the repository objects to create for the domain. If 
	/// this attribute is not specified and a <see cref="RepositoryAttribute.Name"/> is not specified
	/// then the assembly will be part of the default shared logging domain.
	/// </para>
	/// <para>
	/// This attribute can only be specified on the assembly and may only be used
	/// once per assembly.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	[Obsolete("Use RepositoryAttribute instead of DomainAttribute")]
	public sealed class DomainAttribute : RepositoryAttribute
	{
		#region Public Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DomainAttribute" /> class.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Obsolete. Use RepositoryAttribute instead of DomainAttribute.
		/// </para>
		/// </remarks>
		public DomainAttribute() : base()
		{
		}

		/// <summary>
		/// Initialize a new instance of the <see cref="DomainAttribute" /> class 
		/// with the name of the domain.
		/// </summary>
		/// <param name="name">The name of the domain.</param>
		/// <remarks>
		/// <para>
		/// Obsolete. Use RepositoryAttribute instead of DomainAttribute.
		/// </para>
		/// </remarks>
		public DomainAttribute(string name) : base(name)
		{
		}

		#endregion Public Instance Constructors
	}
}

#endif // !NETCF