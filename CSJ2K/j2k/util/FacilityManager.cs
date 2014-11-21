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
	
	/// <summary> This class manages common facilities for multi-threaded
	/// environments, It can register different facilities for each thread,
	/// and also a default one, so that they can be referred by static
	/// methods, while possibly having different ones for different
	/// threads. Also a default facility exists that is used for threads
	/// for which no particular facility has been registerd registered.
	/// 
	/// <p>Currently the only kind of facilities managed is MsgLogger.</p>
	/// 
	/// <P>An example use of this class is if 2 instances of a decoder are running
	/// in different threads and the messages of the 2 instances should be
	/// separated.
	/// 
	/// <P>The default MsgLogger is a StreamMsgLogger that uses System.out as
	/// the 'out' stream and System.err as the 'err' stream, and a line width of
	/// 78. This can be changed using the registerMsgLogger() method.
	/// 
	/// </summary>
	/// <seealso cref="MsgLogger">
	/// </seealso>
	/// <seealso cref="StreamMsgLogger">
	/// 
	/// </seealso>
	public class FacilityManager
	{
		/// <summary> Returns the ProgressWatch instance registered with the current
		/// thread (the thread that calls this method). If the current
		/// thread has no registered ProgressWatch, then the default one is used. 
		/// 
		/// </summary>
		public static ProgressWatch ProgressWatch
		{
			get
			{
				ProgressWatch pw = (ProgressWatch) watchProgList[SupportClass.ThreadClass.Current()];
				return (pw == null)?defWatchProg:pw;
			}
			
		}
		
		/// <summary>The loggers associated to different threads </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'loggerList '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.Collections.Hashtable loggerList = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		
		/// <summary>The default logger, for threads that have none associated with them </summary>
		private static MsgLogger defMsgLogger = new StreamMsgLogger(System.Console.OpenStandardOutput(), System.Console.OpenStandardError(), 78);
		
		/// <summary>The ProgressWatch instance associated to different threads </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'watchProgList '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.Collections.Hashtable watchProgList = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		
		/// <summary>The default ProgressWatch for threads that have none
		/// associated with them. 
		/// </summary>
		private static ProgressWatch defWatchProg = null;
		
		
		internal static void  registerProgressWatch(SupportClass.ThreadClass t, ProgressWatch pw)
		{
			if (pw == null)
			{
				throw new System.NullReferenceException();
			}
			if (t == null)
			{
				defWatchProg = pw;
			}
			else
			{
				watchProgList[t] = pw;
			}
		}
		
		/// <summary> Registers the MsgLogger 'ml' as the logging facility of the
		/// thread 't'. If any other logging facility was registered with
		/// the thread 't' it is overriden by 'ml'. If 't' is null then
		/// 'ml' is taken as the default message logger that is used for
		/// threads that have no MsgLogger registered.
		/// 
		/// </summary>
		/// <param name="t">The thread to associate with 'ml'
		/// 
		/// </param>
		/// <param name="ml">The MsgLogger to associate with therad ml
		/// 
		/// </param>
		internal static void  registerMsgLogger(SupportClass.ThreadClass t, MsgLogger ml)
		{
			if (ml == null)
			{
				throw new System.NullReferenceException();
			}
			if (t == null)
			{
				defMsgLogger = ml;
			}
			else
			{
				loggerList[t] = ml;
			}
		}
		
		/// <summary> Returns the MsgLogger registered with the current thread (the
		/// thread that calls this method). If the current thread has no
		/// registered MsgLogger then the default message logger is
		/// returned.
		/// 
		/// </summary>
		/// <returns> The MsgLogger registerd for the current thread, or the
		/// default one if there is none registered for it.
		/// 
		/// </returns>
		public static MsgLogger getMsgLogger()
		{
			MsgLogger ml = (MsgLogger) loggerList[SupportClass.ThreadClass.Current()];
			return (ml == null)?defMsgLogger:ml;
		}
		
		/// <summary> Returns the MsgLogger registered with the thread 't' (the
		/// thread that calls this method). If the thread 't' has no
		/// registered MsgLogger then the default message logger is
		/// returned.
		/// 
		/// </summary>
		/// <param name="t">The thread for which to return the MsgLogger
		/// 
		/// </param>
		/// <returns> The MsgLogger registerd for the current thread, or the
		/// default one if there is none registered for it.
		/// 
		/// </returns>
		internal static MsgLogger getMsgLogger(SupportClass.ThreadClass t)
		{
			MsgLogger ml = (MsgLogger) loggerList[t];
			return (ml == null)?defMsgLogger:ml;
		}
	}
}