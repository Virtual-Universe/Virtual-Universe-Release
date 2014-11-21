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
	/// Assembly level attribute that specifies a repository to alias to this assembly's repository.
	/// </summary>
	/// <remarks>
	/// <para>
	/// An assembly's logger repository is defined by its <see cref="RepositoryAttribute"/>,
	/// however this can be overridden by an assembly loaded before the target assembly.
	/// </para>
	/// <para>
	/// An assembly can alias another assembly's repository to its repository by
	/// specifying this attribute with the name of the target repository.
	/// </para>
	/// <para>
	/// This attribute can only be specified on the assembly and may be used
	/// as many times as necessary to alias all the required repositories.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	[AttributeUsage(AttributeTargets.Assembly,AllowMultiple=true)]
	[Serializable]
	public /*sealed*/ class AliasRepositoryAttribute : Attribute
	{
		//
		// Class is not sealed because AliasDomainAttribute extends it while it is obsoleted
		// 

		#region Public Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AliasRepositoryAttribute" /> class with 
		/// the specified repository to alias to this assembly's repository.
		/// </summary>
		/// <param name="name">The repository to alias to this assemby's repository.</param>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="AliasRepositoryAttribute" /> class with 
		/// the specified repository to alias to this assembly's repository.
		/// </para>
		/// </remarks>
		public AliasRepositoryAttribute(string name)
		{
			Name = name;
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets the repository to alias to this assemby's repository.
		/// </summary>
		/// <value>
		/// The repository to alias to this assemby's repository.
		/// </value>
		/// <remarks>
		/// <para>
		/// The name of the repository to alias to this assemby's repository.
		/// </para>
		/// </remarks>
		public string Name
		{
			get { return m_name; }
			set { m_name = value ; }
		}

		#endregion Public Instance Properties

		#region Private Instance Fields

		private string m_name = null;

		#endregion Private Instance Fields
	}
}

#endif // !NETCF