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
using System.Collections;
using System.Reflection;

using SmartInspect.Appender;
using SmartInspect.Layout;
using SmartInspect.Util;
using SmartInspect.Repository;
using SmartInspect.Repository.Hierarchy;

namespace SmartInspect.Config
{
	/// <summary>
	/// Use this class to quickly configure a <see cref="Hierarchy"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Allows very simple programmatic configuration of SmartInspect.
	/// </para>
	/// <para>
	/// Only one appender can be configured using this configurator.
	/// The appender is set at the root of the hierarchy and all logging
	/// events will be delivered to that appender.
	/// </para>
	/// <para>
	/// Appenders can also implement the <see cref="SmartInspect.Core.IOptionHandler"/> interface. Therefore
	/// they would require that the <see cref="M:SmartInspect.Core.IOptionHandler.ActivateOptions()"/> method
	/// be called after the appenders properties have been configured.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public sealed class BasicConfigurator
    {
	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the BasicConfigurator class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(BasicConfigurator);

	    #endregion Private Static Fields

        #region Private Instance Constructors

        /// <summary>
		/// Initializes a new instance of the <see cref="BasicConfigurator" /> class. 
		/// </summary>
		/// <remarks>
		/// <para>
		/// Uses a private access modifier to prevent instantiation of this class.
		/// </para>
		/// </remarks>
		private BasicConfigurator()
		{
		}

		#endregion Private Instance Constructors

		#region Public Static Methods

		/// <summary>
		/// Initializes the SmartInspect system with a default configuration.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes the SmartInspect logging system using a <see cref="ConsoleAppender"/>
		/// that will write to <c>Console.Out</c>. The log messages are
		/// formatted using the <see cref="PatternLayout"/> layout object
		/// with the <see cref="PatternLayout.DetailConversionPattern"/>
		/// layout style.
		/// </para>
		/// </remarks>
        static public ICollection Configure()
		{
		    return BasicConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
		}

	    /// <summary>
		/// Initializes the SmartInspect system using the specified appender.
		/// </summary>
		/// <param name="appender">The appender to use to log all logging events.</param>
		/// <remarks>
		/// <para>
		/// Initializes the SmartInspect system using the specified appender.
		/// </para>
		/// </remarks>
		static public ICollection Configure(IAppender appender) 
		{
            return Configure(new IAppender[] { appender });
		}

        /// <summary>
        /// Initializes the SmartInspect system using the specified appenders.
        /// </summary>
        /// <param name="appenders">The appenders to use to log all logging events.</param>
        /// <remarks>
        /// <para>
        /// Initializes the SmartInspect system using the specified appenders.
        /// </para>
        /// </remarks>
        static public ICollection Configure(params IAppender[] appenders)
        {
            ArrayList configurationMessages = new ArrayList();

            ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());

            using (new LogLog.LogReceivedAdapter(configurationMessages))
            {
                InternalConfigure(repository, appenders);
            }

            repository.ConfigurationMessages = configurationMessages;

            return configurationMessages;
        }

		/// <summary>
		/// Initializes the <see cref="ILoggerRepository"/> with a default configuration.
		/// </summary>
		/// <param name="repository">The repository to configure.</param>
		/// <remarks>
		/// <para>
		/// Initializes the specified repository using a <see cref="ConsoleAppender"/>
		/// that will write to <c>Console.Out</c>. The log messages are
		/// formatted using the <see cref="PatternLayout"/> layout object
		/// with the <see cref="PatternLayout.DetailConversionPattern"/>
		/// layout style.
		/// </para>
		/// </remarks>
        static public ICollection Configure(ILoggerRepository repository) 
		{
            ArrayList configurationMessages = new ArrayList();

            using (new LogLog.LogReceivedAdapter(configurationMessages))
            {
                // Create the layout
                PatternLayout layout = new PatternLayout();
                layout.ConversionPattern = PatternLayout.DetailConversionPattern;
                layout.ActivateOptions();

                // Create the appender
                ConsoleAppender appender = new ConsoleAppender();
                appender.Layout = layout;
                appender.ActivateOptions();

                InternalConfigure(repository, appender);
            }

            repository.ConfigurationMessages = configurationMessages;

            return configurationMessages;
		}

        /// <summary>
        /// Initializes the <see cref="ILoggerRepository"/> using the specified appender.
        /// </summary>
        /// <param name="repository">The repository to configure.</param>
        /// <param name="appender">The appender to use to log all logging events.</param>
        /// <remarks>
        /// <para>
        /// Initializes the <see cref="ILoggerRepository"/> using the specified appender.
        /// </para>
        /// </remarks>
        static public ICollection Configure(ILoggerRepository repository, IAppender appender)
        {
            return Configure(repository, new IAppender[] { appender });
        }

        /// <summary>
        /// Initializes the <see cref="ILoggerRepository"/> using the specified appenders.
        /// </summary>
        /// <param name="repository">The repository to configure.</param>
        /// <param name="appenders">The appenders to use to log all logging events.</param>
        /// <remarks>
        /// <para>
        /// Initializes the <see cref="ILoggerRepository"/> using the specified appender.
        /// </para>
        /// </remarks>
        static public ICollection Configure(ILoggerRepository repository, params IAppender[] appenders)
        {
            ArrayList configurationMessages = new ArrayList();

            using (new LogLog.LogReceivedAdapter(configurationMessages))
            {
                InternalConfigure(repository, appenders);
            }

            repository.ConfigurationMessages = configurationMessages;

            return configurationMessages;
        }
	    
		static private void InternalConfigure(ILoggerRepository repository, params IAppender[] appenders) 
		{
            IBasicRepositoryConfigurator configurableRepository = repository as IBasicRepositoryConfigurator;
            if (configurableRepository != null)
            {
                configurableRepository.Configure(appenders);
            }
            else
            {
                LogLog.Warn(declaringType, "BasicConfigurator: Repository [" + repository + "] does not support the BasicConfigurator");
            }
		}

		#endregion Public Static Methods
	}
}
