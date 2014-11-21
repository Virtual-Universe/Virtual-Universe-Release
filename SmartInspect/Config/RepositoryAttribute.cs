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
	/// Assembly level attribute that specifies the logging repository for the assembly.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Assemblies are mapped to logging repository. This attribute specified 
	/// on the assembly controls
	/// the configuration of the repository. The <see cref="Name"/> property specifies the name
	/// of the repository that this assembly is a part of. The <see cref="RepositoryType"/>
	/// specifies the type of the <see cref="SmartInspect.Repository.ILoggerRepository"/> object 
	/// to create for the assembly. If this attribute is not specified or a <see cref="Name"/> 
	/// is not specified then the assembly will be part of the default shared logging repository.
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
	public /*sealed*/ class RepositoryAttribute : Attribute
	{
		//
		// Class is not sealed because DomainAttribute extends it while it is obsoleted
		// 

		#region Public Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RepositoryAttribute" /> class.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Default constructor.
		/// </para>
		/// </remarks>
		public RepositoryAttribute()
		{
		}

		/// <summary>
		/// Initialize a new instance of the <see cref="RepositoryAttribute" /> class 
		/// with the name of the repository.
		/// </summary>
		/// <param name="name">The name of the repository.</param>
		/// <remarks>
		/// <para>
		/// Initialize the attribute with the name for the assembly's repository.
		/// </para>
		/// </remarks>
		public RepositoryAttribute(string name)
		{
			m_name = name;
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets the name of the logging repository.
		/// </summary>
		/// <value>
		/// The string name to use as the name of the repository associated with this
		/// assembly.
		/// </value>
		/// <remarks>
		/// <para>
		/// This value does not have to be unique. Several assemblies can share the
		/// same repository. They will share the logging configuration of the repository.
		/// </para>
		/// </remarks>
		public string Name
		{
			get { return m_name; }
			set { m_name = value ; }
		}

		/// <summary>
		/// Gets or sets the type of repository to create for this assembly.
		/// </summary>
		/// <value>
		/// The type of repository to create for this assembly.
		/// </value>
		/// <remarks>
		/// <para>
		/// The type of the repository to create for the assembly.
		/// The type must implement the <see cref="SmartInspect.Repository.ILoggerRepository"/>
		/// interface.
		/// </para>
		/// <para>
		/// This will be the type of repository created when 
		/// the repository is created. If multiple assemblies reference the
		/// same repository then the repository is only created once using the
		/// <see cref="RepositoryType" /> of the first assembly to call into the 
		/// repository.
		/// </para>
		/// </remarks>
		public Type RepositoryType
		{
			get { return m_repositoryType; }
			set { m_repositoryType = value ; }
		}

		#endregion Public Instance Properties

		#region Private Instance Fields

		private string m_name = null;
		private Type m_repositoryType = null;

		#endregion Private Instance Fields
	}
}

#endif // !NETCF