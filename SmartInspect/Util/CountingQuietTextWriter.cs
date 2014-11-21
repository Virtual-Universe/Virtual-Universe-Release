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

using SmartInspect.Core;

namespace SmartInspect.Util
{
	/// <summary>
	/// Subclass of <see cref="QuietTextWriter"/> that maintains a count of 
	/// the number of bytes written.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This writer counts the number of bytes written.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class CountingQuietTextWriter : QuietTextWriter 
	{
		#region Public Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="writer">The <see cref="TextWriter" /> to actually write to.</param>
		/// <param name="errorHandler">The <see cref="IErrorHandler" /> to report errors to.</param>
		/// <remarks>
		/// <para>
		/// Creates a new instance of the <see cref="CountingQuietTextWriter" /> class 
		/// with the specified <see cref="TextWriter" /> and <see cref="IErrorHandler" />.
		/// </para>
		/// </remarks>
		public CountingQuietTextWriter(TextWriter writer, IErrorHandler errorHandler) : base(writer, errorHandler)
		{
			m_countBytes = 0;
		}

		#endregion Public Instance Constructors

		#region Override implementation of QuietTextWriter
  
		/// <summary>
		/// Writes a character to the underlying writer and counts the number of bytes written.
		/// </summary>
		/// <param name="value">the char to write</param>
		/// <remarks>
		/// <para>
		/// Overrides implementation of <see cref="QuietTextWriter"/>. Counts
		/// the number of bytes written.
		/// </para>
		/// </remarks>
		public override void Write(char value) 
		{
			try 
			{
				base.Write(value);

				// get the number of bytes needed to represent the 
				// char using the supplied encoding.
				m_countBytes += this.Encoding.GetByteCount(new char[] { value });
			} 
			catch(Exception e) 
			{
				this.ErrorHandler.Error("Failed to write [" + value + "].", e, ErrorCode.WriteFailure);
			}
		}
    
		/// <summary>
		/// Writes a buffer to the underlying writer and counts the number of bytes written.
		/// </summary>
		/// <param name="buffer">the buffer to write</param>
		/// <param name="index">the start index to write from</param>
		/// <param name="count">the number of characters to write</param>
		/// <remarks>
		/// <para>
		/// Overrides implementation of <see cref="QuietTextWriter"/>. Counts
		/// the number of bytes written.
		/// </para>
		/// </remarks>
		public override void Write(char[] buffer, int index, int count) 
		{
			if (count > 0)
			{
				try 
				{
					base.Write(buffer, index, count);

					// get the number of bytes needed to represent the 
					// char array using the supplied encoding.
					m_countBytes += this.Encoding.GetByteCount(buffer, index, count);
				} 
				catch(Exception e) 
				{
					this.ErrorHandler.Error("Failed to write buffer.", e, ErrorCode.WriteFailure);
				}
			}
		}

		/// <summary>
		/// Writes a string to the output and counts the number of bytes written.
		/// </summary>
		/// <param name="str">The string data to write to the output.</param>
		/// <remarks>
		/// <para>
		/// Overrides implementation of <see cref="QuietTextWriter"/>. Counts
		/// the number of bytes written.
		/// </para>
		/// </remarks>
		override public void Write(string str) 
		{
			if (str != null && str.Length > 0)
			{
				try 
				{
					base.Write(str);

					// get the number of bytes needed to represent the 
					// string using the supplied encoding.
					m_countBytes += this.Encoding.GetByteCount(str);
				}
				catch(Exception e) 
				{
					this.ErrorHandler.Error("Failed to write [" + str + "].", e, ErrorCode.WriteFailure);
				}
			}
		}
		
		#endregion Override implementation of QuietTextWriter

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets the total number of bytes written.
		/// </summary>
		/// <value>
		/// The total number of bytes written.
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets or sets the total number of bytes written.
		/// </para>
		/// </remarks>
		public long Count 
		{
			get { return m_countBytes; }
			set { m_countBytes = value; }
		}

		#endregion Public Instance Properties
  
		#region Private Instance Fields

		/// <summary>
		/// Total number of bytes written.
		/// </summary>
		private long m_countBytes;

		#endregion Private Instance Fields
	}
}
