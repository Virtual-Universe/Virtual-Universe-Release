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
	/// Adapts any <see cref="ILayout"/> to a <see cref="IRawLayout"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Where an <see cref="IRawLayout"/> is required this adapter
	/// allows a <see cref="ILayout"/> to be specified.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class Layout2RawLayoutAdapter : IRawLayout
	{
		#region Member Variables

		/// <summary>
		/// The layout to adapt
		/// </summary>
		private ILayout m_layout;

		#endregion

		#region Constructors

		/// <summary>
		/// Construct a new adapter
		/// </summary>
		/// <param name="layout">the layout to adapt</param>
		/// <remarks>
		/// <para>
		/// Create the adapter for the specified <paramref name="layout"/>.
		/// </para>
		/// </remarks>
		public Layout2RawLayoutAdapter(ILayout layout)
		{
			m_layout = layout;
		}

		#endregion

		#region Implementation of IRawLayout

		/// <summary>
		/// Format the logging event as an object.
		/// </summary>
		/// <param name="loggingEvent">The event to format</param>
		/// <returns>returns the formatted event</returns>
		/// <remarks>
		/// <para>
		/// Format the logging event as an object.
		/// </para>
		/// <para>
		/// Uses the <see cref="ILayout"/> object supplied to 
		/// the constructor to perform the formatting.
		/// </para>
		/// </remarks>
		virtual public object Format(LoggingEvent loggingEvent)
		{
			StringWriter writer = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
			m_layout.Format(writer, loggingEvent);
			return writer.ToString();
		}

		#endregion
	}
}
