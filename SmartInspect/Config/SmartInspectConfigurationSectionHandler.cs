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

// .NET Compact Framework 1.0 has no support for application .config files
#if !NETCF

using System.Configuration;
using System.Xml;

namespace SmartInspect.Config
{
	/// <summary>
	/// Class to register for the SmartInspect section of the configuration file
	/// </summary>
	/// <remarks>
	/// The SmartInspect section of the configuration file needs to have a section
	/// handler registered. This is the section handler used. It simply returns
	/// the XML element that is the root of the section.
	/// </remarks>
	/// <example>
	/// Example of registering the SmartInspect section handler :
	/// <code lang="XML" escaped="true">
	/// <configuration>
	///		<configSections>
	///			<section name="SmartInspect" type="SmartInspect.Config.SmartInspectConfigurationSectionHandler, SmartInspect" />
	///		</configSections>
	///		<SmartInspect>
	///			SmartInspect configuration XML goes here
	///		</SmartInspect>
	/// </configuration>
	/// </code>
	/// </example>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class SmartInspectConfigurationSectionHandler : IConfigurationSectionHandler
	{
		#region Public Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SmartInspectConfigurationSectionHandler"/> class.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Default constructor.
		/// </para>
		/// </remarks>
		public SmartInspectConfigurationSectionHandler()
		{
		}

		#endregion Public Instance Constructors

		#region Implementation of IConfigurationSectionHandler

		/// <summary>
		/// Parses the configuration section.
		/// </summary>
		/// <param name="parent">The configuration settings in a corresponding parent configuration section.</param>
		/// <param name="configContext">The configuration context when called from the ASP.NET configuration system. Otherwise, this parameter is reserved and is a null reference.</param>
		/// <param name="section">The <see cref="XmlNode" /> for the SmartInspect section.</param>
		/// <returns>The <see cref="XmlNode" /> for the SmartInspect section.</returns>
		/// <remarks>
		/// <para>
		/// Returns the <see cref="XmlNode"/> containing the configuration data,
		/// </para>
		/// </remarks>
		public object Create(object parent, object configContext, XmlNode section)
		{
			return section;
		}

		#endregion Implementation of IConfigurationSectionHandler
	}
}

#endif // !NETCF