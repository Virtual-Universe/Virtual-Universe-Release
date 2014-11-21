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

using SmartInspect.Util;

namespace SmartInspect
{
	/// <summary>
	/// The SmartInspect Thread Context.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The <c>ThreadContext</c> provides a location for thread specific debugging 
	/// information to be stored.
	/// The <c>ThreadContext</c> properties override any <see cref="GlobalContext"/>
	/// properties with the same name.
	/// </para>
	/// <para>
	/// The thread context has a properties map and a stack.
	/// The properties and stack can 
	/// be included in the output of log messages. The <see cref="SmartInspect.Layout.PatternLayout"/>
	/// supports selecting and outputting these properties.
	/// </para>
	/// <para>
	/// The Thread Context provides a diagnostic context for the current thread. 
	/// This is an instrument for distinguishing interleaved log
	/// output from different sources. Log output is typically interleaved
	/// when a server handles multiple clients near-simultaneously.
	/// </para>
	/// <para>
	/// The Thread Context is managed on a per thread basis.
	/// </para>
	/// </remarks>
	/// <example>Example of using the thread context properties to store a username.
	/// <code lang="C#">
	/// ThreadContext.Properties["user"] = userName;
	///	log.Info("This log message has a ThreadContext Property called 'user'");
	/// </code>
	/// </example>
	/// <example>Example of how to push a message into the context stack
	/// <code lang="C#">
	///	using(ThreadContext.Stacks["NDC"].Push("my context message"))
	///	{
	///		log.Info("This log message has a ThreadContext Stack message that includes 'my context message'");
	///	
	///	} // at the end of the using block the message is automatically popped 
	/// </code>
	/// </example>
	/// <threadsafety static="true" instance="true" />
	/// <author>Nicko Cadell</author>
	public sealed class ThreadContext
	{
		#region Private Instance Constructors

		/// <summary>
		/// Private Constructor. 
		/// </summary>
		/// <remarks>
		/// <para>
		/// Uses a private access modifier to prevent instantiation of this class.
		/// </para>
		/// </remarks>
		private ThreadContext()
		{
		}

		#endregion Private Instance Constructors

		#region Public Static Properties

		/// <summary>
		/// The thread properties map
		/// </summary>
		/// <value>
		/// The thread properties map
		/// </value>
		/// <remarks>
		/// <para>
		/// The <c>ThreadContext</c> properties override any <see cref="GlobalContext"/>
		/// properties with the same name.
		/// </para>
		/// </remarks>
		public static ThreadContextProperties Properties
		{
			get { return s_properties; }
		}

		/// <summary>
		/// The thread stacks
		/// </summary>
		/// <value>
		/// stack map
		/// </value>
		/// <remarks>
		/// <para>
		/// The thread local stacks.
		/// </para>
		/// </remarks>
		public static ThreadContextStacks Stacks
		{
			get { return s_stacks; }
		}

		#endregion Public Static Properties

		#region Private Static Fields

		/// <summary>
		/// The thread context properties instance
		/// </summary>
		private readonly static ThreadContextProperties s_properties = new ThreadContextProperties();

		/// <summary>
		/// The thread context stacks instance
		/// </summary>
		private readonly static ThreadContextStacks s_stacks = new ThreadContextStacks(s_properties);

		#endregion Private Static Fields
	}
}
