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

namespace SmartInspect.Core
{
	/// <summary>
	/// Interface for objects that require fixing.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Interface that indicates that the object requires fixing before it
	/// can be taken outside the context of the appender's 
	/// <see cref="SmartInspect.Appender.IAppender.DoAppend"/> method.
	/// </para>
	/// <para>
	/// When objects that implement this interface are stored 
	/// in the context properties maps <see cref="SmartInspect.GlobalContext"/>
	/// <see cref="SmartInspect.GlobalContext.Properties"/> and <see cref="SmartInspect.ThreadContext"/>
	/// <see cref="SmartInspect.ThreadContext.Properties"/> are fixed 
	/// (see <see cref="LoggingEvent.Fix"/>) the <see cref="GetFixedObject"/>
	/// method will be called.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public interface IFixingRequired
	{
		/// <summary>
		/// Get a portable version of this object
		/// </summary>
		/// <returns>the portable instance of this object</returns>
		/// <remarks>
		/// <para>
		/// Get a portable instance object that represents the current
		/// state of this object. The portable object can be stored
		/// and logged from any thread with identical results.
		/// </para>
		/// </remarks>
		object GetFixedObject();
	}
}
