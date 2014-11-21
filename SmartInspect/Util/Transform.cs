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
using System.Xml;
using System.Text.RegularExpressions;

namespace SmartInspect.Util
{
	/// <summary>
	/// Utility class for transforming strings.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Utility class for transforming strings.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public sealed class Transform
	{
		#region Private Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Transform" /> class. 
		/// </summary>
		/// <remarks>
		/// <para>
		/// Uses a private access modifier to prevent instantiation of this class.
		/// </para>
		/// </remarks>
		private Transform()
		{
		}

		#endregion Private Instance Constructors

		#region XML String Methods

		/// <summary>
		/// Write a string to an <see cref="XmlWriter"/>
		/// </summary>
		/// <param name="writer">the writer to write to</param>
		/// <param name="textData">the string to write</param>
		/// <param name="invalidCharReplacement">The string to replace non XML compliant chars with</param>
		/// <remarks>
		/// <para>
		/// The test is escaped either using XML escape entities
		/// or using CDATA sections.
		/// </para>
		/// </remarks>
		public static void WriteEscapedXmlString(XmlWriter writer, string textData, string invalidCharReplacement)
		{
			string stringData = MaskXmlInvalidCharacters(textData, invalidCharReplacement);
			// Write either escaped text or CDATA sections

			int weightCData = 12 * (1 + CountSubstrings(stringData, CDATA_END));
			int weightStringEscapes = 3*(CountSubstrings(stringData, "<") + CountSubstrings(stringData, ">")) + 4*CountSubstrings(stringData, "&");

			if (weightStringEscapes <= weightCData)
			{
				// Write string using string escapes
				writer.WriteString(stringData);
			}
			else
			{
				// Write string using CDATA section

				int end = stringData.IndexOf(CDATA_END);
	
				if (end < 0) 
				{
					writer.WriteCData(stringData);
				}
				else
				{
					int start = 0;
					while (end > -1) 
					{
						writer.WriteCData(stringData.Substring(start, end - start));
						if (end == stringData.Length - 3)
						{
							start = stringData.Length;
							writer.WriteString(CDATA_END);
							break;
						}
						else
						{
							writer.WriteString(CDATA_UNESCAPABLE_TOKEN);
							start = end + 2;
							end = stringData.IndexOf(CDATA_END, start);
						}
					}
	
					if (start < stringData.Length)
					{
						writer.WriteCData(stringData.Substring(start));
					}
				}
			}
		}

		/// <summary>
		/// Replace invalid XML characters in text string
		/// </summary>
		/// <param name="textData">the XML text input string</param>
		/// <param name="mask">the string to use in place of invalid characters</param>
		/// <returns>A string that does not contain invalid XML characters.</returns>
		/// <remarks>
		/// <para>
		/// Certain Unicode code points are not allowed in the XML InfoSet, for
		/// details see: <a href="http://www.w3.org/TR/REC-xml/#charsets">http://www.w3.org/TR/REC-xml/#charsets</a>.
		/// </para>
		/// <para>
		/// This method replaces any illegal characters in the input string
		/// with the mask string specified.
		/// </para>
		/// </remarks>
		public static string MaskXmlInvalidCharacters(string textData, string mask)
		{
			return INVALIDCHARS.Replace(textData, mask);
		}

		#endregion XML String Methods

		#region Private Helper Methods

		/// <summary>
		/// Count the number of times that the substring occurs in the text
		/// </summary>
		/// <param name="text">the text to search</param>
		/// <param name="substring">the substring to find</param>
		/// <returns>the number of times the substring occurs in the text</returns>
		/// <remarks>
		/// <para>
		/// The substring is assumed to be non repeating within itself.
		/// </para>
		/// </remarks>
		private static int CountSubstrings(string text, string substring)
		{
			int count = 0;
			int offset = 0;
			int length = text.Length;
			int substringLength = substring.Length;

			if (length == 0)
			{
				return 0;
			}
			if (substringLength == 0)
			{
				return 0;
			}

			while(offset < length)
			{
				int index = text.IndexOf(substring, offset);

				if (index == -1)
				{
					break;
				}

				count++;
				offset = index + substringLength;
			}
			return count;
		}

		#endregion

		#region Private Static Fields

		private const string CDATA_END	= "]]>";
		private const string CDATA_UNESCAPABLE_TOKEN	= "]]";

        /// <summary>
        /// Characters illegal in XML 1.0
        /// </summary>
		private static Regex INVALIDCHARS=new Regex(@"[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD]",RegexOptions.Compiled);
		#endregion Private Static Fields
	}
}
