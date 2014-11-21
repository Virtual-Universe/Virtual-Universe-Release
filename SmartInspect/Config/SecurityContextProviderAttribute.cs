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
using System.Reflection;

using SmartInspect.Util;
using SmartInspect.Repository;
using SmartInspect.Core;

namespace SmartInspect.Config
{
	/// <summary>
	/// Assembly level attribute to configure the <see cref="SecurityContextProvider"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This attribute may only be used at the assembly scope and can only
	/// be used once per assembly.
	/// </para>
	/// <para>
	/// Use this attribute to configure the <see cref="XmlConfigurator"/>
	/// without calling one of the <see cref="M:XmlConfigurator.Configure()"/>
	/// methods.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class SecurityContextProviderAttribute : ConfiguratorAttribute
	{
		#region Constructor

		/// <summary>
		/// Construct provider attribute with type specified
		/// </summary>
		/// <param name="providerType">the type of the provider to use</param>
		/// <remarks>
		/// <para>
		/// The provider specified must subclass the <see cref="SecurityContextProvider"/>
		/// class.
		/// </para>
		/// </remarks>
		public SecurityContextProviderAttribute(Type providerType) : base(100) /* configurator priority 100 to execute before the XmlConfigurator */
		{
			m_providerType = providerType;
		}

		#endregion

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets the type of the provider to use.
		/// </summary>
		/// <value>
		/// the type of the provider to use.
		/// </value>
		/// <remarks>
		/// <para>
		/// The provider specified must subclass the <see cref="SecurityContextProvider"/>
		/// class.
		/// </para>
		/// </remarks>
		public Type ProviderType
		{
			get { return m_providerType; }
			set { m_providerType = value; }
		}

		#endregion Public Instance Properties

		#region Override ConfiguratorAttribute

		/// <summary>
		/// Configures the SecurityContextProvider
		/// </summary>
		/// <param name="sourceAssembly">The assembly that this attribute was defined on.</param>
		/// <param name="targetRepository">The repository to configure.</param>
		/// <remarks>
		/// <para>
		/// Creates a provider instance from the <see cref="ProviderType"/> specified.
		/// Sets this as the default security context provider <see cref="SecurityContextProvider.DefaultProvider"/>.
		/// </para>
		/// </remarks>
		override public void Configure(Assembly sourceAssembly, ILoggerRepository targetRepository)
		{
			if (m_providerType == null)
			{
				LogLog.Error(declaringType, "Attribute specified on assembly ["+sourceAssembly.FullName+"] with null ProviderType.");
			}
			else
			{
				LogLog.Debug(declaringType, "Creating provider of type ["+ m_providerType.FullName +"]");

				SecurityContextProvider provider = Activator.CreateInstance(m_providerType) as SecurityContextProvider;

				if (provider == null)
				{
					LogLog.Error(declaringType, "Failed to create SecurityContextProvider instance of type ["+m_providerType.Name+"].");
				}
				else
				{
					SecurityContextProvider.DefaultProvider = provider;
				}
			}
		}

		#endregion

		#region Private Instance Fields

		private Type m_providerType = null;

		#endregion Private Instance Fields

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the SecurityContextProviderAttribute class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(SecurityContextProviderAttribute);

	    #endregion Private Static Fields
	}
}

#endif // !NETCF