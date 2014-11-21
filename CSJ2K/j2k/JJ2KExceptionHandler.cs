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
namespace CSJ2K.j2k
{
	
	/// <summary> This class handles exceptions. It should be used in places where it
	/// is not known how to handle the exception, and the exception can not
	/// be thrown higher in the stack.
	/// 
	/// <P>Different options can be registered for each Thread and
	/// ThreadGroup. <i>This feature is not implemented yet</i>
	/// 
	/// </summary>
	public class JJ2KExceptionHandler
	{
		
		/// <summary> Handles the exception. If no special action is registered for
		/// the current thread, then the Exception's stack trace and a
		/// descriptive message are printed to standard error and the
		/// current thread is stopped.
		/// 
		/// <P><i>Registration of special actions is not implemented yet.</i>
		/// 
		/// </summary>
		/// <param name="e">The exception to handle
		/// 
		/// 
		/// 
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  handleException(System.Exception e)
		{
			// Test if there is an special action (not implemented yet)
			
			// If no special action
			
			// Print the Exception message and stack to standard error
			// including this method in the stack.
			//UPGRADE_ISSUE: Method 'java.lang.Throwable.fillInStackTrace' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangThrowablefillInStackTrace'"
			//e.fillInStackTrace();
			//SupportClass.WriteStackTrace(e, Console.Error);
			// Print an explicative message
			System.Console.Error.WriteLine("The Thread is being terminated bacause an " + "Exception (shown above)\n" + "has been thrown and no special action was " + "defined for this Thread.");
			// Stop the thread (do not use stop, since it's deprecated in
			// Java 1.2)
			//UPGRADE_NOTE: Exception 'java.lang.ThreadDeath' was converted to 'System.ApplicationException' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
            throw e;
		}
	}
}