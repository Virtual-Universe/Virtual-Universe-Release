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
using System.Xml;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;

using SmartInspect.Appender;
using SmartInspect.Util;
using SmartInspect.Repository;

namespace SmartInspect.Config
{
	/// <summary>
	/// Use this class to initialize the SmartInspect environment using an Xml tree.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
	/// </para>
	/// <para>
	/// Configures a <see cref="ILoggerRepository"/> using an Xml tree.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	[Obsolete("Use XmlConfigurator instead of DOMConfigurator")]
	public sealed class DOMConfigurator
	{
		#region Private Instance Constructors

		/// <summary>
		/// Private constructor
		/// </summary>
		private DOMConfigurator() 
		{ 
		}

		#endregion Protected Instance Constructors

		#region Configure static methods

		/// <summary>
		/// Automatically configures the SmartInspect system based on the 
		/// application's configuration settings.
		/// </summary>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// Each application has a configuration file. This has the
		/// same name as the application with '.config' appended.
		/// This file is XML and calling this function prompts the
		/// configurator to look in that file for a section called
		/// <c>SmartInspect</c> that contains the configuration data.
		/// </remarks>
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		static public void Configure() 
		{
			XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
		}

		/// <summary>
		/// Automatically configures the <see cref="ILoggerRepository"/> using settings
		/// stored in the application's configuration file.
		/// </summary>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// Each application has a configuration file. This has the
		/// same name as the application with '.config' appended.
		/// This file is XML and calling this function prompts the
		/// configurator to look in that file for a section called
		/// <c>SmartInspect</c> that contains the configuration data.
		/// </remarks>
		/// <param name="repository">The repository to configure.</param>
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		static public void Configure(ILoggerRepository repository) 
		{
			XmlConfigurator.Configure(repository);
		}

		/// <summary>
		/// Configures SmartInspect using a <c>SmartInspect</c> element
		/// </summary>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// Loads the SmartInspect configuration from the XML element
		/// supplied as <paramref name="element"/>.
		/// </remarks>
		/// <param name="element">The element to parse.</param>
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		static public void Configure(XmlElement element) 
		{
			XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()), element);
		}

		/// <summary>
		/// Configures the <see cref="ILoggerRepository"/> using the specified XML 
		/// element.
		/// </summary>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// Loads the SmartInspect configuration from the XML element
		/// supplied as <paramref name="element"/>.
		/// </remarks>
		/// <param name="repository">The repository to configure.</param>
		/// <param name="element">The element to parse.</param>
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		static public void Configure(ILoggerRepository repository, XmlElement element) 
		{
			XmlConfigurator.Configure(repository, element);
		}

		/// <summary>
		/// Configures SmartInspect using the specified configuration file.
		/// </summary>
		/// <param name="configFile">The XML file to load the configuration from.</param>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// <para>
		/// The configuration file must be valid XML. It must contain
		/// at least one element called <c>SmartInspect</c> that holds
		/// the SmartInspect configuration data.
		/// </para>
		/// <para>
		/// The SmartInspect configuration file can possible be specified in the application's
		/// configuration file (either <c>MyAppName.exe.config</c> for a
		/// normal application on <c>Web.config</c> for an ASP.NET application).
		/// </para>
		/// <example>
		/// The following example configures SmartInspect using a configuration file, of which the 
		/// location is stored in the application's configuration file :
		/// </example>
		/// <code lang="C#">
		/// using SmartInspect.Config;
		/// using System.IO;
		/// using System.Configuration;
		/// 
		/// ...
		/// 
		/// DOMConfigurator.Configure(new FileInfo(ConfigurationSettings.AppSettings["SmartInspect-config-file"]));
		/// </code>
		/// <para>
		/// In the <c>.config</c> file, the path to the SmartInspect can be specified like this :
		/// </para>
		/// <code lang="XML" escaped="true">
		/// <configuration>
		///		<appSettings>
		///			<add key="SmartInspect-config-file" value="log.config"/>
		///		</appSettings>
		///	</configuration>
		/// </code>
		/// </remarks>
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		static public void Configure(FileInfo configFile)
		{
			XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()), configFile);
		}

		/// <summary>
		/// Configures SmartInspect using the specified configuration file.
		/// </summary>
		/// <param name="configStream">A stream to load the XML configuration from.</param>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// <para>
		/// The configuration data must be valid XML. It must contain
		/// at least one element called <c>SmartInspect</c> that holds
		/// the SmartInspect configuration data.
		/// </para>
		/// <para>
		/// Note that this method will NOT close the stream parameter.
		/// </para>
		/// </remarks>
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		static public void Configure(Stream configStream)
		{
			XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()), configStream);
		}

		/// <summary>
		/// Configures the <see cref="ILoggerRepository"/> using the specified configuration 
		/// file.
		/// </summary>
		/// <param name="repository">The repository to configure.</param>
		/// <param name="configFile">The XML file to load the configuration from.</param>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// <para>
		/// The configuration file must be valid XML. It must contain
		/// at least one element called <c>SmartInspect</c> that holds
		/// the configuration data.
		/// </para>
		/// <para>
		/// The SmartInspect configuration file can possible be specified in the application's
		/// configuration file (either <c>MyAppName.exe.config</c> for a
		/// normal application on <c>Web.config</c> for an ASP.NET application).
		/// </para>
		/// <example>
		/// The following example configures SmartInspect using a configuration file, of which the 
		/// location is stored in the application's configuration file :
		/// </example>
		/// <code lang="C#">
		/// using SmartInspect.Config;
		/// using System.IO;
		/// using System.Configuration;
		/// 
		/// ...
		/// 
		/// DOMConfigurator.Configure(new FileInfo(ConfigurationSettings.AppSettings["SmartInspect-config-file"]));
		/// </code>
		/// <para>
		/// In the <c>.config</c> file, the path to the SmartInspect can be specified like this :
		/// </para>
		/// <code lang="XML" escaped="true">
		/// <configuration>
		///		<appSettings>
		///			<add key="SmartInspect-config-file" value="log.config"/>
		///		</appSettings>
		///	</configuration>
		/// </code>
		/// </remarks>
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		static public void Configure(ILoggerRepository repository, FileInfo configFile)
		{
			XmlConfigurator.Configure(repository, configFile);
		}


		/// <summary>
		/// Configures the <see cref="ILoggerRepository"/> using the specified configuration 
		/// file.
		/// </summary>
		/// <param name="repository">The repository to configure.</param>
		/// <param name="configStream">The stream to load the XML configuration from.</param>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// <para>
		/// The configuration data must be valid XML. It must contain
		/// at least one element called <c>SmartInspect</c> that holds
		/// the configuration data.
		/// </para>
		/// <para>
		/// Note that this method will NOT close the stream parameter.
		/// </para>
		/// </remarks>
		[Obsolete("Use XmlConfigurator.Configure instead of DOMConfigurator.Configure")]
		static public void Configure(ILoggerRepository repository, Stream configStream)
		{
			XmlConfigurator.Configure(repository, configStream);
		}

		#endregion Configure static methods

		#region ConfigureAndWatch static methods

#if !NETCF

		/// <summary>
		/// Configures SmartInspect using the file specified, monitors the file for changes 
		/// and reloads the configuration if a change is detected.
		/// </summary>
		/// <param name="configFile">The XML file to load the configuration from.</param>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// <para>
		/// The configuration file must be valid XML. It must contain
		/// at least one element called <c>SmartInspect</c> that holds
		/// the configuration data.
		/// </para>
		/// <para>
		/// The configuration file will be monitored using a <see cref="FileSystemWatcher"/>
		/// and depends on the behavior of that class.
		/// </para>
		/// <para>
		/// For more information on how to configure SmartInspect using
		/// a separate configuration file, see <see cref="M:Configure(FileInfo)"/>.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Configure(FileInfo)"/>
		[Obsolete("Use XmlConfigurator.ConfigureAndWatch instead of DOMConfigurator.ConfigureAndWatch")]
		static public void ConfigureAndWatch(FileInfo configFile)
		{
			XmlConfigurator.ConfigureAndWatch(LogManager.GetRepository(Assembly.GetCallingAssembly()), configFile);
		}

		/// <summary>
		/// Configures the <see cref="ILoggerRepository"/> using the file specified, 
		/// monitors the file for changes and reloads the configuration if a change 
		/// is detected.
		/// </summary>
		/// <param name="repository">The repository to configure.</param>
		/// <param name="configFile">The XML file to load the configuration from.</param>
		/// <remarks>
		/// <para>
		/// <b>DOMConfigurator is obsolete. Use XmlConfigurator instead of DOMConfigurator.</b>
		/// </para>
		/// <para>
		/// The configuration file must be valid XML. It must contain
		/// at least one element called <c>SmartInspect</c> that holds
		/// the configuration data.
		/// </para>
		/// <para>
		/// The configuration file will be monitored using a <see cref="FileSystemWatcher"/>
		/// and depends on the behavior of that class.
		/// </para>
		/// <para>
		/// For more information on how to configure SmartInspect using
		/// a separate configuration file, see <see cref="M:Configure(FileInfo)"/>.
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Configure(FileInfo)"/>
		[Obsolete("Use XmlConfigurator.ConfigureAndWatch instead of DOMConfigurator.ConfigureAndWatch")]
		static public void ConfigureAndWatch(ILoggerRepository repository, FileInfo configFile)
		{
			XmlConfigurator.ConfigureAndWatch(repository, configFile);
		}
#endif

		#endregion ConfigureAndWatch static methods
	}
}

