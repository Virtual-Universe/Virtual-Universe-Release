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
using System.Text;
using System.IO;

using SmartInspect.Util;
using SmartInspect.Core;

namespace SmartInspect.Util.PatternStringConverters
{
	/// <summary>
	/// Writes a newline to the output
	/// </summary>
	/// <remarks>
	/// <para>
	/// Writes the system dependent line terminator to the output.
	/// This behavior can be overridden by setting the <see cref="PatternConverter.Option"/>:
	/// </para>
	/// <list type="definition">
	///   <listheader>
	///     <term>Option Value</term>
	///     <description>Output</description>
	///   </listheader>
	///   <item>
	///     <term>DOS</term>
	///     <description>DOS or Windows line terminator <c>"\r\n"</c></description>
	///   </item>
	///   <item>
	///     <term>UNIX</term>
	///     <description>UNIX line terminator <c>"\n"</c></description>
	///   </item>
	/// </list>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	internal sealed class NewLinePatternConverter : LiteralPatternConverter, IOptionHandler
	{
		#region Implementation of IOptionHandler

		/// <summary>
		/// Initialize the converter
		/// </summary>
		/// <remarks>
		/// <para>
		/// This is part of the <see cref="IOptionHandler"/> delayed object
		/// activation scheme. The <see cref="ActivateOptions"/> method must 
		/// be called on this object after the configuration properties have
		/// been set. Until <see cref="ActivateOptions"/> is called this
		/// object is in an undefined state and must not be used. 
		/// </para>
		/// <para>
		/// If any of the configuration properties are modified then 
		/// <see cref="ActivateOptions"/> must be called again.
		/// </para>
		/// </remarks>
		public void ActivateOptions()
		{
			if (string.Compare(Option, "DOS", true, System.Globalization.CultureInfo.InvariantCulture) == 0)
			{
				Option = "\r\n";
			}
			else if (string.Compare(Option, "UNIX", true, System.Globalization.CultureInfo.InvariantCulture) == 0)
			{
				Option = "\n";
			}
			else
			{
				Option = SystemInfo.NewLine;
			}
		}

		#endregion
	}
}
