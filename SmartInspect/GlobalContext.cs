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
	/// The SmartInspect Global Context.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The <c>GlobalContext</c> provides a location for global debugging 
	/// information to be stored.
	/// </para>
	/// <para>
	/// The global context has a properties map and these properties can 
	/// be included in the output of log messages. The <see cref="SmartInspect.Layout.PatternLayout"/>
	/// supports selecting and outputing these properties.
	/// </para>
	/// <para>
	/// By default the <c>SmartInspect:HostName</c> property is set to the name of 
	/// the current machine.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code lang="C#">
	/// GlobalContext.Properties["hostname"] = Environment.MachineName;
	/// </code>
	/// </example>
	/// <threadsafety static="true" instance="true" />
	/// <author>Nicko Cadell</author>
	public sealed class GlobalContext
	{
		#region Private Instance Constructors

		/// <summary>
		/// Private Constructor. 
		/// </summary>
		/// <remarks>
		/// Uses a private access modifier to prevent instantiation of this class.
		/// </remarks>
		private GlobalContext()
		{
		}

		#endregion Private Instance Constructors

		static GlobalContext()
		{
			Properties[SmartInspect.Core.LoggingEvent.HostNameProperty] = SystemInfo.HostName;
		}

		#region Public Static Properties

		/// <summary>
		/// The global properties map.
		/// </summary>
		/// <value>
		/// The global properties map.
		/// </value>
		/// <remarks>
		/// <para>
		/// The global properties map.
		/// </para>
		/// </remarks>
		public static GlobalContextProperties Properties
		{
			get { return s_properties; }
		}

		#endregion Public Static Properties

		#region Private Static Fields

		/// <summary>
		/// The global context properties instance
		/// </summary>
		private readonly static GlobalContextProperties s_properties = new GlobalContextProperties();

		#endregion Private Static Fields
	}
}
