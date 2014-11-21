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

using SmartInspect.Appender;

namespace SmartInspect.Core
{
	/// <summary>
	/// Interface for attaching appenders to objects.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Interface for attaching, removing and retrieving appenders.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public interface IAppenderAttachable
	{
		/// <summary>
		/// Attaches an appender.
		/// </summary>
		/// <param name="appender">The appender to add.</param>
		/// <remarks>
		/// <para>
		/// Add the specified appender. The implementation may
		/// choose to allow or deny duplicate appenders.
		/// </para>
		/// </remarks>
		void AddAppender(IAppender appender);

		/// <summary>
		/// Gets all attached appenders.
		/// </summary>
		/// <value>
		/// A collection of attached appenders.
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets a collection of attached appenders.
		/// If there are no attached appenders the
		/// implementation should return an empty 
		/// collection rather than <c>null</c>.
		/// </para>
		/// </remarks>
		AppenderCollection Appenders {get;}

		/// <summary>
		/// Gets an attached appender with the specified name.
		/// </summary>
		/// <param name="name">The name of the appender to get.</param>
		/// <returns>
		/// The appender with the name specified, or <c>null</c> if no appender with the
		/// specified name is found.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Returns an attached appender with the <paramref name="name"/> specified.
		/// If no appender with the specified name is found <c>null</c> will be
		/// returned.
		/// </para>
		/// </remarks>
		IAppender GetAppender(string name);

		/// <summary>
		/// Removes all attached appenders.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Removes and closes all attached appenders
		/// </para>
		/// </remarks>
		void RemoveAllAppenders();

		/// <summary>
		/// Removes the specified appender from the list of attached appenders.
		/// </summary>
		/// <param name="appender">The appender to remove.</param>
		/// <returns>The appender removed from the list</returns>
		/// <remarks>
		/// <para>
		/// The appender removed is not closed.
		/// If you are discarding the appender you must call
		/// <see cref="IAppender.Close"/> on the appender removed.
		/// </para>
		/// </remarks>
		IAppender RemoveAppender(IAppender appender);

		/// <summary>
		/// Removes the appender with the specified name from the list of appenders.
		/// </summary>
		/// <param name="name">The name of the appender to remove.</param>
		/// <returns>The appender removed from the list</returns>
		/// <remarks>
		/// <para>
		/// The appender removed is not closed.
		/// If you are discarding the appender you must call
		/// <see cref="IAppender.Close"/> on the appender removed.
		/// </para>
		/// </remarks>
		IAppender RemoveAppender(string name);   	
	}
}
