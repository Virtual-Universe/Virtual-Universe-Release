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
	
	/// <summary> This class provides a simple common abstraction of a facility that logs
	/// and/or displays messages or simple strings. The underlying facility can be
	/// a terminal, text file, text area in a GUI display, dialog boxes in a GUI
	/// display, etc., or a combination of those.
	/// 
	/// <>Messages are short strings (a couple of lines) that indicate some state
	/// of the program, and that have a severity code associated with them (see
	/// below). Simple strings is text (can be long) that has no severity code
	/// associated with it. Typical use of simple strings is to display help
	/// texts.</p>
	/// 
	/// <p>Each message has a severity code, which can be one of the following:
	/// LOG, INFO, WARNING, ERROR. Each implementation should treat each severity
	/// code in a way which corresponds to the type of diplay used.</p>
	/// 
	/// <p>Messages are printed via the 'printmsg()' method. Simple strings are
	/// printed via the 'print()', 'println()' and 'flush()' methods, each simple
	/// string is considered to be terminated once the 'flush()' method has been
	/// called. The 'printmsg()' method should never be called before a previous
	/// simple string has been terminated.</p>
	/// 
	/// </summary>
	public struct MsgLogger_Fields{
		/// <summary>Severity of message. LOG messages are just for bookkeeping and do not
		/// need to be displayed in the majority of cases 
		/// </summary>
		public const int LOG = 0;
		/// <summary>Severity of message. INFO messages should be displayed just for user
		/// feedback. 
		/// </summary>
		public const int INFO = 1;
		/// <summary>Severity of message. WARNING messages denote that an unexpected state
		/// has been reached and should be given as feedback to the user. 
		/// </summary>
		public const int WARNING = 2;
		/// <summary>Severity of message. ERROR messages denote that something has gone
		/// wrong and probably that execution has ended. They should be definetely
		/// displayed to the user. 
		/// </summary>
		public const int ERROR = 3;
	}
	public interface MsgLogger
	{
		//UPGRADE_NOTE: Members of interface 'MsgLogger' were extracted into structure 'MsgLogger_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		
		/// <summary> Prints the message 'msg' to the output device, appending a newline,
		/// with severity 'sev'. Some implementations where the appended newline is
		/// irrelevant may not append the newline. Depending on the implementation
		/// the severity of the message may be added to it. The message is
		/// reformatted as appropriate for the output devic, but any newline
		/// characters are respected.
		/// 
		/// </summary>
		/// <param name="sev">The message severity (LOG, INFO, etc.)
		/// 
		/// </param>
		/// <param name="msg">The message to display
		/// 
		/// </param>
		void  printmsg(int sev, System.String msg);
		
		/// <summary> Prints the string 'str' to the output device, appending a line
		/// return. The message is reformatted as appropriate to the particular
		/// diplaying device, where 'flind' and 'ind' are used as hints for
		/// performing that operation. However, any newlines appearing in 'str' are
		/// respected. The output device may not display the string until flush()
		/// is called. Some implementations may automatically flush when this
		/// method is called. This method just prints the string, the string does
		/// not make part of a "message" in the sense that no severity is
		/// associated to it.
		/// 
		/// </summary>
		/// <param name="str">The string to print
		/// 
		/// </param>
		/// <param name="flind">Indentation of the first line
		/// 
		/// </param>
		/// <param name="ind">Indentation of any other lines.
		/// 
		/// </param>
		void  println(System.String str, int flind, int ind);
		
		/// <summary> Writes any buffered data from the println() method to the device.
		/// 
		/// </summary>
		void  flush();
	}
}