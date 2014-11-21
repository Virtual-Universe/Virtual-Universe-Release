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

using SmartInspect.Core;
using SmartInspect.Util;

namespace SmartInspect.Layout
{
	/// <summary>
	/// Extract the date from the <see cref="LoggingEvent"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Extract the date from the <see cref="LoggingEvent"/>
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class RawUtcTimeStampLayout : IRawLayout
	{
		#region Constructors

		/// <summary>
		/// Constructs a RawUtcTimeStampLayout
		/// </summary>
		public RawUtcTimeStampLayout()
		{
		}

		#endregion
  
		#region Implementation of IRawLayout

		/// <summary>
		/// Gets the <see cref="LoggingEvent.TimeStamp"/> as a <see cref="DateTime"/>.
		/// </summary>
		/// <param name="loggingEvent">The event to format</param>
		/// <returns>returns the time stamp</returns>
		/// <remarks>
		/// <para>
		/// Gets the <see cref="LoggingEvent.TimeStamp"/> as a <see cref="DateTime"/>.
		/// </para>
		/// <para>
		/// The time stamp is in universal time. To format the time stamp
		/// in local time use <see cref="RawTimeStampLayout"/>.
		/// </para>
		/// </remarks>
		public virtual object Format(LoggingEvent loggingEvent)
		{
			return loggingEvent.TimeStamp.ToUniversalTime();
		}

		#endregion
	}
}
