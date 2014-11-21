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

namespace SmartInspect.Util
{
	/// <summary>
	/// Implements SmartInspect's default error handling policy which consists 
	/// of emitting a message for the first error in an appender and 
	/// ignoring all subsequent errors.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The error message is processed using the LogLog sub-system by default.
	/// </para>
	/// <para>
	/// This policy aims at protecting an otherwise working application
	/// from being flooded with error messages when logging fails.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	/// <author>Ron Grabowski</author>
	public class OnlyOnceErrorHandler : IErrorHandler
	{
		#region Public Instance Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="OnlyOnceErrorHandler" /> class.
		/// </para>
		/// </remarks>
		public OnlyOnceErrorHandler()
		{
			m_prefix = "";
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="prefix">The prefix to use for each message.</param>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="OnlyOnceErrorHandler" /> class
		/// with the specified prefix.
		/// </para>
		/// </remarks>
		public OnlyOnceErrorHandler(string prefix)
		{
			m_prefix = prefix;
		}

		#endregion Public Instance Constructors

		#region Public Instance Methods

		/// <summary>
		/// Reset the error handler back to its initial disabled state.
		/// </summary>
		public void Reset()
		{
			m_enabledDate = DateTime.MinValue;
			m_errorCode = ErrorCode.GenericFailure;
			m_exception = null;
			m_message = null;
			m_firstTime = true;
		}

		#region Implementation of IErrorHandler

		/// <summary>
		/// Log an Error
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="e">The exception.</param>
		/// <param name="errorCode">The internal error code.</param>
		/// <remarks>
		/// <para>
		/// Invokes <see cref="FirstError"/> if and only if this is the first error or the first error after <see cref="Reset"/> has been called.
		/// </para>
		/// </remarks>
		public void Error(string message, Exception e, ErrorCode errorCode) 
		{
			if (m_firstTime)
			{
                FirstError(message, e, errorCode);
			}
		}

        /// <summary>
        /// Log the very first error
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="e">The exception.</param>
        /// <param name="errorCode">The internal error code.</param>
        /// <remarks>
        /// <para>
        /// Sends the error information to <see cref="LogLog"/>'s Error method.
        /// </para>
        /// </remarks>
        public virtual void FirstError(string message, Exception e, ErrorCode errorCode) {
            m_enabledDate = DateTime.Now;
            m_errorCode = errorCode;
            m_exception = e;
            m_message = message;
            m_firstTime = false;

            if (LogLog.InternalDebugging && !LogLog.QuietMode) {
                LogLog.Error(declaringType, "[" + m_prefix + "] ErrorCode: " + errorCode.ToString() + ". " + message, e);
            }
        }

        /// <summary>
		/// Log an Error
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="e">The exception.</param>
		/// <remarks>
        /// <para>
        /// Invokes <see cref="FirstError"/> if and only if this is the first error or the first error after <see cref="Reset"/> has been called.
        /// </para>
        /// </remarks>
		public void Error(string message, Exception e) 
		{
			Error(message, e, ErrorCode.GenericFailure);
		}

		/// <summary>
		/// Log an error
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <remarks>
        /// <para>
        /// Invokes <see cref="FirstError"/> if and only if this is the first error or the first error after <see cref="Reset"/> has been called.
        /// </para>
        /// </remarks>
		public void Error(string message) 
		{
			Error(message, null, ErrorCode.GenericFailure);
		}

		#endregion Implementation of IErrorHandler

		#endregion

		#region Public Instance Properties

		/// <summary>
		/// Is error logging enabled
		/// </summary>
		/// <remarks>
		/// <para>
		/// Is error logging enabled. Logging is only enabled for the
		/// first error delivered to the <see cref="OnlyOnceErrorHandler"/>.
		/// </para>
		/// </remarks>
		public bool IsEnabled
		{
			get { return m_firstTime; }
		}

		/// <summary>
		/// The date the first error that trigged this error handler occured.
		/// </summary>
		public DateTime EnabledDate
		{
			get { return m_enabledDate; }
		}

		/// <summary>
		/// The message from the first error that trigged this error handler.
		/// </summary>
		public string ErrorMessage
		{
			get { return m_message; }
		}

		/// <summary>
		/// The exception from the first error that trigged this error handler.
		/// </summary>
		/// <remarks>
		/// May be <see langword="null" />.
		/// </remarks>
		public Exception Exception
		{
			get { return m_exception; }
		}

		/// <summary>
		/// The error code from the first error that trigged this error handler.
		/// </summary>
		/// <remarks>
		/// Defaults to <see cref="SmartInspect.Core.ErrorCode.GenericFailure"/>
		/// </remarks>
		public ErrorCode ErrorCode
		{
			get { return m_errorCode; }
		}

		#endregion

		#region Private Instance Fields

		/// <summary>
		/// The date the error was recorded.
		/// </summary>
		private DateTime m_enabledDate;

		/// <summary>
		/// Flag to indicate if it is the first error
		/// </summary>
		private bool m_firstTime = true;

		/// <summary>
		/// The message recorded during the first error.
		/// </summary>
		private string m_message = null;

		/// <summary>
		/// The exception recorded during the first error.
		/// </summary>
		private Exception m_exception = null;

		/// <summary>
		/// The error code recorded during the first error.
		/// </summary>
		private ErrorCode m_errorCode = ErrorCode.GenericFailure;

		/// <summary>
		/// String to prefix each message with
		/// </summary>
		private readonly string m_prefix;

		#endregion Private Instance Fields

		#region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the OnlyOnceErrorHandler class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
		private readonly static Type declaringType = typeof(OnlyOnceErrorHandler);

		#endregion
	}
}
