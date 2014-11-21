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

using SmartInspect.Core;
using SmartInspect.Repository;

namespace SmartInspect.Core
{
	/// <summary>
	/// Interface that all loggers implement
	/// </summary>
	/// <remarks>
	/// <para>
	/// This interface supports logging events and testing if a level
	/// is enabled for logging.
	/// </para>
	/// <para>
	/// These methods will not throw exceptions. Note to implementor, ensure
	/// that the implementation of these methods cannot allow an exception
	/// to be thrown to the caller.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public interface ILogger
	{
		/// <summary>
		/// Gets the name of the logger.
		/// </summary>
		/// <value>
		/// The name of the logger.
		/// </value>
		/// <remarks>
		/// <para>
		/// The name of this logger
		/// </para>
		/// </remarks>
		string Name { get; }

		/// <summary>
		/// This generic form is intended to be used by wrappers.
		/// </summary>
		/// <param name="callerStackBoundaryDeclaringType">The declaring type of the method that is
		/// the stack boundary into the logging system for this call.</param>
		/// <param name="level">The level of the message to be logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">the exception to log, including its stack trace. Pass <c>null</c> to not log an exception.</param>
		/// <remarks>
		/// <para>
		/// Generates a logging event for the specified <paramref name="level"/> using
		/// the <paramref name="message"/> and <paramref name="exception"/>.
		/// </para>
		/// </remarks>
		void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception);
  
		/// <summary>
		/// This is the most generic printing method that is intended to be used 
		/// by wrappers.
		/// </summary>
		/// <param name="logEvent">The event being logged.</param>
		/// <remarks>
		/// <para>
		/// Logs the specified logging event through this logger.
		/// </para>
		/// </remarks>
		void Log(LoggingEvent logEvent);

		/// <summary>
		/// Checks if this logger is enabled for a given <see cref="Level"/> passed as parameter.
		/// </summary>
		/// <param name="level">The level to check.</param>
		/// <returns>
		/// <c>true</c> if this logger is enabled for <c>level</c>, otherwise <c>false</c>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Test if this logger is going to log events of the specified <paramref name="level"/>.
		/// </para>
		/// </remarks>
		bool IsEnabledFor(Level level);

		/// <summary>
		/// Gets the <see cref="ILoggerRepository"/> where this 
		/// <c>Logger</c> instance is attached to.
		/// </summary>
		/// <value>
		/// The <see cref="ILoggerRepository" /> that this logger belongs to.
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets the <see cref="ILoggerRepository"/> where this 
		/// <c>Logger</c> instance is attached to.
		/// </para>
		/// </remarks>
		ILoggerRepository Repository { get; }
	}
}
