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
using System.IO;

using SmartInspect;
using SmartInspect.Core;

namespace SmartInspect.Layout
{
	/// <summary>
	/// Interface implemented by layout objects
	/// </summary>
	/// <remarks>
	/// <para>
	/// An <see cref="ILayout"/> object is used to format a <see cref="LoggingEvent"/>
	/// as text. The <see cref="M:Format(TextWriter,LoggingEvent)"/> method is called by an
	/// appender to transform the <see cref="LoggingEvent"/> into a string.
	/// </para>
	/// <para>
	/// The layout can also supply <see cref="Header"/> and <see cref="Footer"/>
	/// text that is appender before any events and after all the events respectively.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public interface ILayout
	{
		/// <summary>
		/// Implement this method to create your own layout format.
		/// </summary>
		/// <param name="writer">The TextWriter to write the formatted event to</param>
		/// <param name="loggingEvent">The event to format</param>
		/// <remarks>
		/// <para>
		/// This method is called by an appender to format
		/// the <paramref name="loggingEvent"/> as text and output to a writer.
		/// </para>
		/// <para>
		/// If the caller does not have a <see cref="TextWriter"/> and prefers the
		/// event to be formatted as a <see cref="String"/> then the following
		/// code can be used to format the event into a <see cref="StringWriter"/>.
		/// </para>
		/// <code lang="C#">
		/// StringWriter writer = new StringWriter();
		/// Layout.Format(writer, loggingEvent);
		/// string formattedEvent = writer.ToString();
		/// </code>
		/// </remarks>
		void Format(TextWriter writer, LoggingEvent loggingEvent);

		/// <summary>
		/// The content type output by this layout. 
		/// </summary>
		/// <value>The content type</value>
		/// <remarks>
		/// <para>
		/// The content type output by this layout.
		/// </para>
		/// <para>
		/// This is a MIME type e.g. <c>"text/plain"</c>.
		/// </para>
		/// </remarks>
		string ContentType { get; }

		/// <summary>
		/// The header for the layout format.
		/// </summary>
		/// <value>the layout header</value>
		/// <remarks>
		/// <para>
		/// The Header text will be appended before any logging events
		/// are formatted and appended.
		/// </para>
		/// </remarks>
		string Header { get; }

		/// <summary>
		/// The footer for the layout format.
		/// </summary>
		/// <value>the layout footer</value>
		/// <remarks>
		/// <para>
		/// The Footer text will be appended after all the logging events
		/// have been formatted and appended.
		/// </para>
		/// </remarks>
		string Footer { get; }

		/// <summary>
		/// Flag indicating if this layout handle exceptions
		/// </summary>
		/// <value><c>false</c> if this layout handles exceptions</value>
		/// <remarks>
		/// <para>
		/// If this layout handles the exception object contained within
		/// <see cref="LoggingEvent"/>, then the layout should return
		/// <c>false</c>. Otherwise, if the layout ignores the exception
		/// object, then the layout should return <c>true</c>.
		/// </para>
		/// </remarks>
		bool IgnoresException { get; }
	}
}
