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

using SmartInspect.Core;

namespace SmartInspect.Repository.Hierarchy
{
	/// <summary>
	/// Default implementation of <see cref="ILoggerFactory"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// This default implementation of the <see cref="ILoggerFactory"/>
	/// interface is used to create the default subclass
	/// of the <see cref="Logger"/> object.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	internal class DefaultLoggerFactory : ILoggerFactory
	{
		#region Internal Instance Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="DefaultLoggerFactory" /> class. 
		/// </para>
		/// </remarks>
		internal DefaultLoggerFactory()
		{
		}

		#endregion Internal Instance Constructors

		#region Implementation of ILoggerFactory

		/// <summary>
		/// Create a new <see cref="Logger" /> instance
		/// </summary>
		/// <param name="repository">The <see cref="ILoggerRepository" /> that will own the <see cref="Logger" />.</param>
		/// <param name="name">The name of the <see cref="Logger" />.</param>
		/// <returns>The <see cref="Logger" /> instance for the specified name.</returns>
		/// <remarks>
		/// <para>
		/// Create a new <see cref="Logger" /> instance with the 
		/// specified name.
		/// </para>
		/// <para>
		/// Called by the <see cref="Hierarchy"/> to create
		/// new named <see cref="Logger"/> instances.
		/// </para>
		/// <para>
		/// If the <paramref name="name"/> is <c>null</c> then the root logger
		/// must be returned.
		/// </para>
		/// </remarks>
		public Logger CreateLogger(ILoggerRepository repository, string name) 
		{
			if (name == null)
			{
				return new RootLogger(repository.LevelMap.LookupWithDefault(Level.Debug));
			}
			return new LoggerImpl(name);
		}

		#endregion

		/// <summary>
		/// Default internal subclass of <see cref="Logger"/>
		/// </summary>
		/// <remarks>
		/// <para>
		/// This subclass has no additional behavior over the
		/// <see cref="Logger"/> class but does allow instances
		/// to be created.
		/// </para>
		/// </remarks>
		internal sealed class LoggerImpl : Logger
		{
			/// <summary>
			/// Construct a new Logger
			/// </summary>
			/// <param name="name">the name of the logger</param>
			/// <remarks>
			/// <para>
			/// Initializes a new instance of the <see cref="LoggerImpl" /> class
			/// with the specified name. 
			/// </para>
			/// </remarks>
			internal LoggerImpl(string name) : base(name)
			{
			}
		}
	}
}
