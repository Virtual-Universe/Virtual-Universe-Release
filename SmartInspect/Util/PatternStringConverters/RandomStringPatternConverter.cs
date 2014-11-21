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
using SmartInspect.DateFormatter;
using SmartInspect.Core;

namespace SmartInspect.Util.PatternStringConverters
{
	/// <summary>
	/// A Pattern converter that generates a string of random characters
	/// </summary>
	/// <remarks>
	/// <para>
	/// The converter generates a string of random characters. By default
	/// the string is length 4. This can be changed by setting the <see cref="PatternConverter.Option"/>
	/// to the string value of the length required.
	/// </para>
	/// <para>
	/// The random characters in the string are limited to uppercase letters
	/// and numbers only.
	/// </para>
	/// <para>
	/// The random number generator used by this class is not cryptographically secure.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	internal sealed class RandomStringPatternConverter : PatternConverter, IOptionHandler
	{
		/// <summary>
		/// Shared random number generator
		/// </summary>
		private static readonly Random s_random = new Random();

		/// <summary>
		/// Length of random string to generate. Default length 4.
		/// </summary>
		private int m_length = 4;

		#region Implementation of IOptionHandler

		/// <summary>
		/// Initialize the converter options
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
			string optionStr = Option;
			if (optionStr != null && optionStr.Length > 0)
			{
				int lengthVal;
				if (SystemInfo.TryParse(optionStr, out lengthVal))
				{
					m_length = lengthVal;
				}
				else
				{
					LogLog.Error(declaringType, "RandomStringPatternConverter: Could not convert Option ["+optionStr+"] to Length Int32");
				}	
			}
		}

		#endregion

		/// <summary>
		/// Write a randoim string to the output
		/// </summary>
		/// <param name="writer">the writer to write to</param>
		/// <param name="state">null, state is not set</param>
		/// <remarks>
		/// <para>
		/// Write a randoim string to the output <paramref name="writer"/>.
		/// </para>
		/// </remarks>
		override protected void Convert(TextWriter writer, object state) 
		{
			try 
			{
				lock(s_random)
				{
					for(int i=0; i<m_length; i++)
					{
						int randValue = s_random.Next(36);

						if (randValue < 26)
						{
							// Letter
							char ch = (char)('A' + randValue);
							writer.Write(ch);
						}
						else if (randValue < 36)
						{
							// Number
							char ch = (char)('0' + (randValue - 26));
							writer.Write(ch);
						}
						else
						{
							// Should not get here
							writer.Write('X');
						}
					}
				}
			}
			catch (Exception ex) 
			{
				LogLog.Error(declaringType, "Error occurred while converting.", ex);
			}
		}

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the RandomStringPatternConverter class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(RandomStringPatternConverter);

	    #endregion Private Static Fields
	}
}
