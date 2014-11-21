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
	
	/// <summary> This class implements the MsgLogger interface for streams. Streams can
	/// be simple files, terminals, stdout, stderr, etc. The messages or simple
	/// strings are formatted using the linewidth given to the constructor.
	/// 
	/// <P>Messages are printed to the 'err' stream if they are of severity WARNING
	/// or ERROR, otherwise they are printed to the 'out' stream. Simple strings
	/// are always printed the 'out' stream.
	/// 
	/// </summary>
	public class StreamMsgLogger : MsgLogger
	{
		
		/// <summary>The 'out' stream </summary>
		private System.IO.StreamWriter out_Renamed;
		
		/// <summary>The 'err' stream </summary>
		private System.IO.StreamWriter err;
		
		/// <summary>The printer that formats the text </summary>
		private MsgPrinter mp;
		
		/// <summary> Constructs a StreamMsgLogger that uses 'outstr' as the 'out' stream,
		/// and 'errstr' as the 'err' stream. Note that 'outstr' and 'errstr' can
		/// be System.out and System.err.
		/// 
		/// </summary>
		/// <param name="outstr">Where to print simple strings and LOG and INFO messages.
		/// 
		/// </param>
		/// <param name="errstr">Where to print WARNING and ERROR messages
		/// 
		/// </param>
		/// <param name="lw">The line width to use in formatting
		/// 
		/// 
		/// 
		/// </param>
		public StreamMsgLogger(System.IO.Stream outstr, System.IO.Stream errstr, int lw)
		{
			System.IO.StreamWriter temp_writer;
			temp_writer = new System.IO.StreamWriter(outstr, System.Text.Encoding.Default);
			temp_writer.AutoFlush = true;
			out_Renamed = temp_writer;
			System.IO.StreamWriter temp_writer2;
			temp_writer2 = new System.IO.StreamWriter(errstr, System.Text.Encoding.Default);
			temp_writer2.AutoFlush = true;
			err = temp_writer2;
			mp = new MsgPrinter(lw);
		}
		
		/// <summary> Constructs a StreamMsgLogger that uses 'outstr' as the 'out' stream,
		/// and 'errstr' as the 'err' stream. Note that 'outstr' and 'errstr' can
		/// be System.out and System.err.
		/// 
		/// </summary>
		/// <param name="outstr">Where to print simple strings and LOG and INFO messages.
		/// 
		/// </param>
		/// <param name="errstr">Where to print WARNING and ERROR messages
		/// 
		/// </param>
		/// <param name="lw">The line width to use in formatting
		/// 
		/// 
		/// 
		/// </param>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public StreamMsgLogger(System.IO.StreamWriter outstr, System.IO.StreamWriter errstr, int lw)
		{
			System.IO.StreamWriter temp_writer;
			temp_writer = new System.IO.StreamWriter(outstr.BaseStream, outstr.Encoding);
			temp_writer.AutoFlush = true;
			out_Renamed = temp_writer;
			System.IO.StreamWriter temp_writer2;
			temp_writer2 = new System.IO.StreamWriter(errstr.BaseStream, errstr.Encoding);
			temp_writer2.AutoFlush = true;
			err = temp_writer2;
			mp = new MsgPrinter(lw);
		}
		
		/// <summary> Constructs a StreamMsgLogger that uses 'outstr' as the 'out' stream,
		/// and 'errstr' as the 'err' stream. Note that 'outstr' and 'errstr' can
		/// be System.out and System.err.
		/// 
		/// </summary>
		/// <param name="outstr">Where to print simple strings and LOG and INFO messages.
		/// 
		/// </param>
		/// <param name="errstr">Where to print WARNING and ERROR messages
		/// 
		/// </param>
		/// <param name="lw">The line width to use in formatting
		/// 
		/// 
		/// 
		/// </param>
        /// 
        /*
		public StreamMsgLogger(System.IO.StreamWriter outstr, System.IO.StreamWriter errstr, int lw)
		{
			out_Renamed = outstr;
			err = errstr;
			mp = new MsgPrinter(lw);
		}
		*/
		/// <summary> Prints the message 'msg' to the output device, appending a newline,
		/// with severity 'sev'. The severity of the message is prepended to the
		/// message.
		/// 
		/// </summary>
		/// <param name="sev">The message severity (LOG, INFO, etc.)
		/// 
		/// </param>
		/// <param name="msg">The message to display
		/// 
		/// 
		/// 
		/// </param>
		public virtual void  printmsg(int sev, System.String msg)
		{
			System.IO.StreamWriter lout;
			//int ind;
			System.String prefix;
			
			switch (sev)
			{
				
				case CSJ2K.j2k.util.MsgLogger_Fields.LOG: 
					prefix = "[LOG]: ";
					lout = out_Renamed;
					break;
				
				case CSJ2K.j2k.util.MsgLogger_Fields.INFO: 
					prefix = "[INFO]: ";
					lout = out_Renamed;
					break;
				
				case CSJ2K.j2k.util.MsgLogger_Fields.WARNING: 
					prefix = "[WARNING]: ";
					lout = err;
					break;
				
				case CSJ2K.j2k.util.MsgLogger_Fields.ERROR: 
					prefix = "[ERROR]: ";
					lout = err;
					break;
				
				default: 
					throw new System.ArgumentException("Severity " + sev + " not valid.");
				
			}
			
			mp.print(lout, 0, prefix.Length, prefix + msg);
			lout.Flush();
		}
		
		/// <summary> Prints the string 'str' to the 'out' stream, appending a newline. The
		/// message is reformatted to the line width given to the constructors and
		/// using 'flind' characters to indent the first line and 'ind' characters
		/// to indent the second line. However, any newlines appearing in 'str' are
		/// respected. The output device may or may not display the string until
		/// flush() is called, depending on the autoflush state of the PrintWriter,
		/// to be sure flush() should be called to write the string to the
		/// device. This method just prints the string, the string does not make
		/// part of a "message" in the sense that noe severity is associated to it.
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
		/// 
		/// 
		/// </param>
		public virtual void  println(System.String str, int flind, int ind)
		{
			mp.print(out_Renamed, flind, ind, str);
		}
		
		/// <summary> Writes any buffered data from the print() and println() methods to the
		/// device.
		/// 
		/// 
		/// 
		/// </summary>
		public virtual void  flush()
		{
			out_Renamed.Flush();
		}
	}
}