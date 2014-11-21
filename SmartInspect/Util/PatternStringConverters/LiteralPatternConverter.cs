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

namespace SmartInspect.Util.PatternStringConverters
{
	/// <summary>
	/// Pattern converter for literal string instances in the pattern
	/// </summary>
	/// <remarks>
	/// <para>
	/// Writes the literal string value specified in the 
	/// <see cref="SmartInspect.Util.PatternConverter.Option"/> property to 
	/// the output.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	internal class LiteralPatternConverter : PatternConverter 
	{
		/// <summary>
		/// Set the next converter in the chain
		/// </summary>
		/// <param name="pc">The next pattern converter in the chain</param>
		/// <returns>The next pattern converter</returns>
		/// <remarks>
		/// <para>
		/// Special case the building of the pattern converter chain
		/// for <see cref="LiteralPatternConverter"/> instances. Two adjacent
		/// literals in the pattern can be represented by a single combined
		/// pattern converter. This implementation detects when a 
		/// <see cref="LiteralPatternConverter"/> is added to the chain
		/// after this converter and combines its value with this converter's
		/// literal value.
		/// </para>
		/// </remarks>
		public override PatternConverter SetNext(PatternConverter pc)
		{
			LiteralPatternConverter literalPc = pc as LiteralPatternConverter;
			if (literalPc != null)
			{
				// Combine the two adjacent literals together
				Option = Option + literalPc.Option;

				// We are the next converter now
				return this;
			}

			return base.SetNext(pc);
		}

		/// <summary>
		/// Write the literal to the output
		/// </summary>
		/// <param name="writer">the writer to write to</param>
		/// <param name="state">null, not set</param>
		/// <remarks>
		/// <para>
		/// Override the formatting behavior to ignore the FormattingInfo
		/// because we have a literal instead.
		/// </para>
		/// <para>
		/// Writes the value of <see cref="SmartInspect.Util.PatternConverter.Option"/>
		/// to the output <paramref name="writer"/>.
		/// </para>
		/// </remarks>
		override public void Format(TextWriter writer, object state) 
		{
			writer.Write(Option);
		}

		/// <summary>
		/// Convert this pattern into the rendered message
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="state">null, not set</param>
		/// <remarks>
		/// <para>
		/// This method is not used.
		/// </para>
		/// </remarks>
		override protected void Convert(TextWriter writer, object state) 
		{
			throw new InvalidOperationException("Should never get here because of the overridden Format method");
		}
	}
}
