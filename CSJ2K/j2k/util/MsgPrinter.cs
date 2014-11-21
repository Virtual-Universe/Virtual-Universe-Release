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
namespace CSJ2K.j2k.util
{
	
	/// <summary> This utility class formats messages to the specified line width, by
	/// inserting line-breaks between words, and printing the resulting
	/// lines.
	/// 
	/// </summary>
	public class MsgPrinter
	{
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the line width that is used for formatting.
		/// 
		/// </summary>
		/// <returns> The line width used for formatting
		/// 
		/// 
		/// 
		/// </returns>
		/// <summary> Sets the line width to the specified value. This new value will 
		/// be used in subsequent calls to the print() message.
		/// 
		/// </summary>
		/// <param name="linewidth">The new line width to use (in cahracters)
		/// 
		/// 
		/// 
		/// </param>
		virtual public int LineWidth
		{
			get
			{
				return lw;
			}
			
			set
			{
				if (value < 1)
				{
					throw new System.ArgumentException();
				}
				lw = value;
			}
			
		}
		
		/// <summary>The line width to use </summary>
		public int lw;
		
		/// <summary>Signals that a newline was found </summary>
		private const int IS_NEWLINE = - 2;
		
		/// <summary>Signals that the end-of-string was reached </summary>
		private const int IS_EOS = - 1;
		
		/// <summary> Creates a new message printer with the specified line width and
		/// with the default locale.
		/// 
		/// </summary>
		/// <param name="linewidth">The line width for which to format (in
		/// characters)
		/// 
		/// 
		/// 
		/// </param>
		public MsgPrinter(int linewidth)
		{
			lw = linewidth;
		}
		
		/// <summary> Formats the message to print in the current line width, by
		/// breaking the message into lines between words. The number of
		/// spaces to indent the first line is specified by 'flind' and the
		/// number of spaces to indent each of the following lines is
		/// specified by 'ind'. Newlines in 'msg' are respected. A newline is
		/// always printed at the end.
		/// 
		/// </summary>
		/// <param name="out">Where to print the message.
		/// 
		/// </param>
		/// <param name="flind">The indentation for the first line.
		/// 
		/// </param>
		/// <param name="ind">The indentation for the other lines.
		/// 
		/// </param>
		/// <param name="msg">The message to format and print.
		/// 
		/// 
		/// 
		/// </param>
		public virtual void  print(System.IO.StreamWriter out_Renamed, int flind, int ind, System.String msg)
		{
			int start, end, pend, efflw, lind, i;
			
			start = 0;
			end = 0;
			pend = 0;
			efflw = lw - flind;
			lind = flind;
			while ((end = nextLineEnd(msg, pend)) != IS_EOS)
			{
				if (end == IS_NEWLINE)
				{
					// Forced line break
					for (i = 0; i < lind; i++)
					{
						out_Renamed.Write(" ");
					}
					//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
					out_Renamed.WriteLine(msg.Substring(start, (pend) - (start)));
					if (nextWord(msg, pend) == msg.Length)
					{
						// Traling newline => print it and done
						//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
						out_Renamed.WriteLine("");
						start = pend;
						break;
					}
				}
				else
				{
					if (efflw > end - pend)
					{
						// Room left on current line
						efflw -= (end - pend);
						pend = end;
						continue;
					}
					else
					{
						// Filled-up current line => print it
						for (i = 0; i < lind; i++)
						{
							out_Renamed.Write(" ");
						}
						if (start == pend)
						{
							// Word larger than line width
							// Print anyways
							//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
							out_Renamed.WriteLine(msg.Substring(start, (end) - (start)));
							pend = end;
						}
						else
						{
							//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
							out_Renamed.WriteLine(msg.Substring(start, (pend) - (start)));
						}
					}
				}
				// Initialize for next line
				lind = ind;
				efflw = lw - ind;
				start = nextWord(msg, pend);
				pend = start;
				if (start == IS_EOS)
				{
					break; // Did all the string
				}
			}
			if (pend != start)
			{
				// Part of a line left => print it
				for (i = 0; i < lind; i++)
				{
					out_Renamed.Write(" ");
				}
				//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
				out_Renamed.WriteLine(msg.Substring(start, (pend) - (start)));
			}
		}
		
		/// <summary> Returns the index of the last character of the next word, plus 1, or
		/// IS_NEWLINE if a newline character is encountered before the next word,
		/// or IS_EOS if the end of the string is ecnounterd before the next
		/// word. The method first skips all whitespace characters at or after
		/// 'from', except newlines. If a newline is found IS_NEWLINE is
		/// returned. Then it skips all non-whitespace characters and returns the
		/// position of the last non-whitespace character, plus 1. The returned
		/// index may be greater than the last valid index in the tsring, but it is 
		/// always suitable to be used in the String.substring() method.
		/// 
		/// <P>Non-whitespace characters are defined as in the
		/// Character.isWhitespace method (that method is used).
		/// 
		/// </summary>
		/// <param name="str">The string to parse
		/// 
		/// </param>
		/// <param name="from">The index of the first position to search from
		/// 
		/// </param>
		/// <returns> The index of the last character in the next word, plus 1,
		/// IS_NEWLINE, or IS_EOS if there are no more words.
		/// 
		/// 
		/// 
		/// </returns>
		private int nextLineEnd(System.String str, int from)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'len '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int len = str.Length;
			char c = '\x0000';
			// First skip all whitespace, except new line
			while (from < len && (c = str[from]) != '\n' && System.Char.IsWhiteSpace(c))
			{
				from++;
			}
			if (c == '\n')
			{
				return IS_NEWLINE;
			}
			if (from >= len)
			{
				return IS_EOS;
			}
			// Now skip word characters
			while (from < len && !System.Char.IsWhiteSpace(str[from]))
			{
				from++;
			}
			return from;
		}
		
		/// <summary> Returns the position of the first character in the next word, starting
		/// from 'from', if a newline is encountered first then the index of the
		/// newline character plus 1 is returned. If the end of the string is
		/// encountered then IS_EOS is returned. Words are defined as any
		/// concatenation of 1 or more characters which are not
		/// whitespace. Whitespace characters are those for which
		/// Character.isWhitespace() returns true (that method is used).
		/// 
		/// <P>Non-whitespace characters are defined as in the
		/// Character.isWhitespace method (that method is used).
		/// 
		/// </summary>
		/// <param name="str">The string to parse
		/// 
		/// </param>
		/// <param name="from">The index where to start parsing
		/// 
		/// </param>
		/// <returns> The index of the first character of the next word, or the index 
		/// of the newline plus 1, or IS_EOS.
		/// 
		/// 
		/// 
		/// </returns>
		private int nextWord(System.String str, int from)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'len '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int len = str.Length;
			char c = '\x0000';
			// First skip all whitespace, but new lines
			while (from < len && (c = str[from]) != '\n' && System.Char.IsWhiteSpace(c))
			{
				from++;
			}
			if (from >= len)
			{
				return IS_EOS;
			}
			else if (c == '\n')
			{
				return from + 1;
			}
			else
			{
				return from;
			}
		}
	}
}