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

namespace SmartInspect
{
	/// <summary>
	/// Implementation of Mapped Diagnostic Contexts.
	/// </summary>
	/// <remarks>
	/// <note>
	/// <para>
	/// The MDC is deprecated and has been replaced by the <see cref="ThreadContext.Properties"/>.
	/// The current MDC implementation forwards to the <c>ThreadContext.Properties</c>.
	/// </para>
	/// </note>
	/// <para>
	/// The MDC class is similar to the <see cref="NDC"/> class except that it is
	/// based on a map instead of a stack. It provides <i>mapped
	/// diagnostic contexts</i>. A <i>Mapped Diagnostic Context</i>, or
	/// MDC in short, is an instrument for distinguishing interleaved log
	/// output from different sources. Log output is typically interleaved
	/// when a server handles multiple clients near-simultaneously.
	/// </para>
	/// <para>
	/// The MDC is managed on a per thread basis.
	/// </para>
	/// </remarks>
	/// <threadsafety static="true" instance="true" />
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	/*[Obsolete("MDC has been replaced by ThreadContext.Properties")]*/
	public sealed class MDC
	{
		#region Private Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MDC" /> class. 
		/// </summary>
		/// <remarks>
		/// Uses a private access modifier to prevent instantiation of this class.
		/// </remarks>
		private MDC()
		{
		}

		#endregion Private Instance Constructors

		#region Public Static Methods

		/// <summary>
		/// Gets the context value identified by the <paramref name="key" /> parameter.
		/// </summary>
		/// <param name="key">The key to lookup in the MDC.</param>
		/// <returns>The string value held for the key, or a <c>null</c> reference if no corresponding value is found.</returns>
		/// <remarks>
		/// <note>
		/// <para>
		/// The MDC is deprecated and has been replaced by the <see cref="ThreadContext.Properties"/>.
		/// The current MDC implementation forwards to the <c>ThreadContext.Properties</c>.
		/// </para>
		/// </note>
		/// <para>
		/// If the <paramref name="key" /> parameter does not look up to a
		/// previously defined context then <c>null</c> will be returned.
		/// </para>
		/// </remarks>
		/*[Obsolete("MDC has been replaced by ThreadContext.Properties")]*/
		public static string Get(string key)
		{
			object obj = ThreadContext.Properties[key];
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		/// <summary>
		/// Add an entry to the MDC
		/// </summary>
		/// <param name="key">The key to store the value under.</param>
		/// <param name="value">The value to store.</param>
		/// <remarks>
		/// <note>
		/// <para>
		/// The MDC is deprecated and has been replaced by the <see cref="ThreadContext.Properties"/>.
		/// The current MDC implementation forwards to the <c>ThreadContext.Properties</c>.
		/// </para>
		/// </note>
		/// <para>
		/// Puts a context value (the <paramref name="value" /> parameter) as identified
		/// with the <paramref name="key" /> parameter into the current thread's
		/// context map.
		/// </para>
		/// <para>
		/// If a value is already defined for the <paramref name="key" />
		/// specified then the value will be replaced. If the <paramref name="value" /> 
		/// is specified as <c>null</c> then the key value mapping will be removed.
		/// </para>
		/// </remarks>
		/*[Obsolete("MDC has been replaced by ThreadContext.Properties")]*/
		public static void Set(string key, string value)
		{
			ThreadContext.Properties[key] = value;
		}

		/// <summary>
		/// Removes the key value mapping for the key specified.
		/// </summary>
		/// <param name="key">The key to remove.</param>
		/// <remarks>
		/// <note>
		/// <para>
		/// The MDC is deprecated and has been replaced by the <see cref="ThreadContext.Properties"/>.
		/// The current MDC implementation forwards to the <c>ThreadContext.Properties</c>.
		/// </para>
		/// </note>
		/// <para>
		/// Remove the specified entry from this thread's MDC
		/// </para>
		/// </remarks>
		/*[Obsolete("MDC has been replaced by ThreadContext.Properties")]*/
		public static void Remove(string key)
		{
			ThreadContext.Properties.Remove(key);
		}

		/// <summary>
		/// Clear all entries in the MDC
		/// </summary>
		/// <remarks>
		/// <note>
		/// <para>
		/// The MDC is deprecated and has been replaced by the <see cref="ThreadContext.Properties"/>.
		/// The current MDC implementation forwards to the <c>ThreadContext.Properties</c>.
		/// </para>
		/// </note>
		/// <para>
		/// Remove all the entries from this thread's MDC
		/// </para>
		/// </remarks>
		/*[Obsolete("MDC has been replaced by ThreadContext.Properties")]*/
		public static void Clear()
		{
			ThreadContext.Properties.Clear();
		}

		#endregion Public Static Methods
	}
}
