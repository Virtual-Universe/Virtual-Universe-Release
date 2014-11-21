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
using System.Collections;
using System.Reflection;
using System.IO;

using SmartInspect.Util;
using SmartInspect.Repository;
using SmartInspect.Repository.Hierarchy;

namespace SmartInspect.Config
{
	/// <summary>
	/// Assembly level attribute to configure the <see cref="XmlConfigurator"/>.
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
	/// <para>
	/// If neither of the <see cref="ConfigFile"/> or <see cref="ConfigFileExtension"/>
	/// properties are set the configuration is loaded from the application's .config file.
	/// If set the <see cref="ConfigFile"/> property takes priority over the
	/// <see cref="ConfigFileExtension"/> property. The <see cref="ConfigFile"/> property
	/// specifies a path to a file to load the config from. The path is relative to the
	/// application's base directory; <see cref="AppDomain.BaseDirectory"/>.
	/// The <see cref="ConfigFileExtension"/> property is used as a postfix to the assembly file name.
	/// The config file must be located in the  application's base directory; <see cref="AppDomain.BaseDirectory"/>.
	/// For example in a console application setting the <see cref="ConfigFileExtension"/> to
	/// <c>config</c> has the same effect as not specifying the <see cref="ConfigFile"/> or 
	/// <see cref="ConfigFileExtension"/> properties.
	/// </para>
	/// <para>
	/// The <see cref="Watch"/> property can be set to cause the <see cref="XmlConfigurator"/>
	/// to watch the configuration file for changes.
	/// </para>
	/// <note>
	/// <para>
	/// SmartInspect will only look for assembly level configuration attributes once.
	/// When using the SmartInspect assembly level attributes to control the configuration 
	/// of SmartInspect you must ensure that the first call to any of the 
	/// <see cref="SmartInspect.Core.LoggerManager"/> methods is made from the assembly with the configuration
	/// attributes. 
	/// </para>
	/// <para>
	/// If you cannot guarantee the order in which SmartInspect calls will be made from 
	/// different assemblies you must use programmatic configuration instead, i.e.
	/// call the <see cref="M:XmlConfigurator.Configure()"/> method directly.
	/// </para>
	/// </note>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public /*sealed*/ class XmlConfiguratorAttribute : ConfiguratorAttribute
	{
		//
		// Class is not sealed because DOMConfiguratorAttribute extends it while it is obsoleted
		// 

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Default constructor
		/// </para>
		/// </remarks>
		public XmlConfiguratorAttribute() : base(0) /* configurator priority 0 */
		{
		}

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets the filename of the configuration file.
		/// </summary>
		/// <value>
		/// The filename of the configuration file.
		/// </value>
		/// <remarks>
		/// <para>
		/// If specified, this is the name of the configuration file to use with
		/// the <see cref="XmlConfigurator"/>. This file path is relative to the
		/// <b>application base</b> directory (<see cref="AppDomain.BaseDirectory"/>).
		/// </para>
		/// <para>
		/// The <see cref="ConfigFile"/> takes priority over the <see cref="ConfigFileExtension"/>.
		/// </para>
		/// </remarks>
		public string ConfigFile
		{
			get { return m_configFile; }
			set { m_configFile = value; }
		}

		/// <summary>
		/// Gets or sets the extension of the configuration file.
		/// </summary>
		/// <value>
		/// The extension of the configuration file.
		/// </value>
		/// <remarks>
		/// <para>
		/// If specified this is the extension for the configuration file.
		/// The path to the config file is built by using the <b>application 
		/// base</b> directory (<see cref="AppDomain.BaseDirectory"/>),
		/// the <b>assembly file name</b> and the config file extension.
		/// </para>
		/// <para>
		/// If the <see cref="ConfigFileExtension"/> is set to <c>MyExt</c> then
		/// possible config file names would be: <c>MyConsoleApp.exe.MyExt</c> or
		/// <c>MyClassLibrary.dll.MyExt</c>.
		/// </para>
		/// <para>
		/// The <see cref="ConfigFile"/> takes priority over the <see cref="ConfigFileExtension"/>.
		/// </para>
		/// </remarks>
		public string ConfigFileExtension
		{
			get { return m_configFileExtension; }
			set { m_configFileExtension = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to watch the configuration file.
		/// </summary>
		/// <value>
		/// <c>true</c> if the configuration should be watched, <c>false</c> otherwise.
		/// </value>
		/// <remarks>
		/// <para>
		/// If this flag is specified and set to <c>true</c> then the framework
		/// will watch the configuration file and will reload the config each time 
		/// the file is modified.
		/// </para>
		/// <para>
		/// The config file can only be watched if it is loaded from local disk.
		/// In a No-Touch (Smart Client) deployment where the application is downloaded
		/// from a web server the config file may not reside on the local disk
		/// and therefore it may not be able to watch it.
		/// </para>
		/// </remarks>
		public bool Watch
		{
			get { return m_configureAndWatch; }
			set { m_configureAndWatch = value; }
		}

		#endregion Public Instance Properties

		#region Override ConfiguratorAttribute

		/// <summary>
		/// Configures the <see cref="ILoggerRepository"/> for the specified assembly.
		/// </summary>
		/// <param name="sourceAssembly">The assembly that this attribute was defined on.</param>
		/// <param name="targetRepository">The repository to configure.</param>
		/// <remarks>
		/// <para>
		/// Configure the repository using the <see cref="XmlConfigurator"/>.
		/// The <paramref name="targetRepository"/> specified must extend the <see cref="Hierarchy"/>
		/// class otherwise the <see cref="XmlConfigurator"/> will not be able to
		/// configure it.
		/// </para>
		/// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="targetRepository" /> does not extend <see cref="Hierarchy"/>.</exception>
		override public void Configure(Assembly sourceAssembly, ILoggerRepository targetRepository)
		{
            IList configurationMessages = new ArrayList();

            using (new LogLog.LogReceivedAdapter(configurationMessages))
            {
                string applicationBaseDirectory = null;
                try
                {
                    applicationBaseDirectory = SystemInfo.ApplicationBaseDirectory;
                }
                catch
                {
                    // Ignore this exception because it is only thrown when ApplicationBaseDirectory is a file
                    // and the application does not have PathDiscovery permission
                }

                if (applicationBaseDirectory == null || (new Uri(applicationBaseDirectory)).IsFile)
                {
                    ConfigureFromFile(sourceAssembly, targetRepository);
                }
                else
                {
                    ConfigureFromUri(sourceAssembly, targetRepository);
                }
            }

            targetRepository.ConfigurationMessages = configurationMessages;
		}

		#endregion

		/// <summary>
		/// Attempt to load configuration from the local file system
		/// </summary>
		/// <param name="sourceAssembly">The assembly that this attribute was defined on.</param>
		/// <param name="targetRepository">The repository to configure.</param>
		private void ConfigureFromFile(Assembly sourceAssembly, ILoggerRepository targetRepository)
		{
			// Work out the full path to the config file
			string fullPath2ConfigFile = null;
			
			// Select the config file
			if (m_configFile == null || m_configFile.Length == 0)
			{
				if (m_configFileExtension == null || m_configFileExtension.Length == 0)
				{
					// Use the default .config file for the AppDomain
					try
					{
						fullPath2ConfigFile = SystemInfo.ConfigurationFileLocation;
					}
					catch(Exception ex)
					{
						LogLog.Error(declaringType, "XmlConfiguratorAttribute: Exception getting ConfigurationFileLocation. Must be able to resolve ConfigurationFileLocation when ConfigFile and ConfigFileExtension properties are not set.", ex);
					}
				}
				else
				{
					// Force the extension to start with a '.'
					if (m_configFileExtension[0] != '.')
					{
						m_configFileExtension = "." + m_configFileExtension;
					}

					string applicationBaseDirectory = null;
					try
					{
						applicationBaseDirectory = SystemInfo.ApplicationBaseDirectory;
					}
					catch(Exception ex)
					{
						LogLog.Error(declaringType, "Exception getting ApplicationBaseDirectory. Must be able to resolve ApplicationBaseDirectory and AssemblyFileName when ConfigFileExtension property is set.", ex);
					}

					if (applicationBaseDirectory != null)
					{
						fullPath2ConfigFile = Path.Combine(applicationBaseDirectory, SystemInfo.AssemblyFileName(sourceAssembly) + m_configFileExtension);
					}
				}
			}
			else
			{
				string applicationBaseDirectory = null;
				try
				{
					applicationBaseDirectory = SystemInfo.ApplicationBaseDirectory;
				}
				catch(Exception ex)
				{
					LogLog.Warn(declaringType, "Exception getting ApplicationBaseDirectory. ConfigFile property path ["+m_configFile+"] will be treated as an absolute path.", ex);
				}

				if (applicationBaseDirectory != null)
				{
					// Just the base dir + the config file
					fullPath2ConfigFile = Path.Combine(applicationBaseDirectory, m_configFile);
				}
				else
				{
					fullPath2ConfigFile = m_configFile;
				}
			}

			if (fullPath2ConfigFile != null)
			{
				ConfigureFromFile(targetRepository, new FileInfo(fullPath2ConfigFile));
			}
		}

		/// <summary>
		/// Configure the specified repository using a <see cref="FileInfo"/>
		/// </summary>
		/// <param name="targetRepository">The repository to configure.</param>
		/// <param name="configFile">the FileInfo pointing to the config file</param>
		private void ConfigureFromFile(ILoggerRepository targetRepository, FileInfo configFile)
		{
			// Do we configure just once or do we configure and then watch?
			if (m_configureAndWatch)
			{
				XmlConfigurator.ConfigureAndWatch(targetRepository, configFile);
			}
			else
			{
				XmlConfigurator.Configure(targetRepository, configFile);
			}
		}

		/// <summary>
		/// Attempt to load configuration from a URI
		/// </summary>
		/// <param name="sourceAssembly">The assembly that this attribute was defined on.</param>
		/// <param name="targetRepository">The repository to configure.</param>
		private void ConfigureFromUri(Assembly sourceAssembly, ILoggerRepository targetRepository)
		{
			// Work out the full path to the config file
			Uri fullPath2ConfigFile = null;
			
			// Select the config file
			if (m_configFile == null || m_configFile.Length == 0)
			{
				if (m_configFileExtension == null || m_configFileExtension.Length == 0)
				{
					string systemConfigFilePath = null;
					try
					{
						systemConfigFilePath = SystemInfo.ConfigurationFileLocation;
					}
					catch(Exception ex)
					{
						LogLog.Error(declaringType, "XmlConfiguratorAttribute: Exception getting ConfigurationFileLocation. Must be able to resolve ConfigurationFileLocation when ConfigFile and ConfigFileExtension properties are not set.", ex);
					}

					if (systemConfigFilePath != null)
					{
						Uri systemConfigFileUri = new Uri(systemConfigFilePath);

						// Use the default .config file for the AppDomain
						fullPath2ConfigFile = systemConfigFileUri;
					}
				}
				else
				{
					// Force the extension to start with a '.'
					if (m_configFileExtension[0] != '.')
					{
						m_configFileExtension = "." + m_configFileExtension;
					}

					string systemConfigFilePath = null;
					try
					{
						systemConfigFilePath = SystemInfo.ConfigurationFileLocation;
					}
					catch(Exception ex)
					{
						LogLog.Error(declaringType, "XmlConfiguratorAttribute: Exception getting ConfigurationFileLocation. Must be able to resolve ConfigurationFileLocation when the ConfigFile property are not set.", ex);
					}

					if (systemConfigFilePath != null)
					{
						UriBuilder builder = new UriBuilder(new Uri(systemConfigFilePath));

						// Remove the current extension from the systemConfigFileUri path
						string path = builder.Path;
						int startOfExtension = path.LastIndexOf(".");
						if (startOfExtension >= 0)
						{
							path = path.Substring(0, startOfExtension);
						}
						path += m_configFileExtension;

						builder.Path = path;
						fullPath2ConfigFile = builder.Uri;
					}
				}
			}
			else
			{
				string applicationBaseDirectory = null;
				try
				{
					applicationBaseDirectory = SystemInfo.ApplicationBaseDirectory;
				}
				catch(Exception ex)
				{
					LogLog.Warn(declaringType, "Exception getting ApplicationBaseDirectory. ConfigFile property path ["+m_configFile+"] will be treated as an absolute URI.", ex);
				}

				if (applicationBaseDirectory != null)
				{
					// Just the base dir + the config file
					fullPath2ConfigFile = new Uri(new Uri(applicationBaseDirectory), m_configFile);
				}
				else
				{
					fullPath2ConfigFile = new Uri(m_configFile);
				}
			}

			if (fullPath2ConfigFile != null)
			{
				if (fullPath2ConfigFile.IsFile)
				{
					// The m_configFile could be an absolute local path, therefore we have to be
					// prepared to switch back to using FileInfos here
					ConfigureFromFile(targetRepository, new FileInfo(fullPath2ConfigFile.LocalPath));
				}
				else
				{
					if (m_configureAndWatch)
					{
						LogLog.Warn(declaringType, "XmlConfiguratorAttribute: Unable to watch config file loaded from a URI");
					}
					XmlConfigurator.Configure(targetRepository, fullPath2ConfigFile);
				}
			}
		}

		#region Private Instance Fields

		private string m_configFile = null;
		private string m_configFileExtension = null;
		private bool m_configureAndWatch = false;

		#endregion Private Instance Fields

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the XmlConfiguratorAttribute class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(XmlConfiguratorAttribute);

	    #endregion Private Static Fields
	}
}

#endif // !NETCF
