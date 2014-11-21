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

using SmartInspect.Core;

namespace SmartInspect.Filter
{
	/// <summary>
	/// This filter drops all <see cref="LoggingEvent"/>. 
	/// </summary>
	/// <remarks>
	/// <para>
	/// You can add this filter to the end of a filter chain to
	/// switch from the default "accept all unless instructed otherwise"
	/// filtering behavior to a "deny all unless instructed otherwise"
	/// behavior.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public sealed class DenyAllFilter : FilterSkeleton
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public DenyAllFilter()
		{
		}

		#endregion

		#region Override implementation of FilterSkeleton

		/// <summary>
		/// Always returns the integer constant <see cref="FilterDecision.Deny"/>
		/// </summary>
		/// <param name="loggingEvent">the LoggingEvent to filter</param>
		/// <returns>Always returns <see cref="FilterDecision.Deny"/></returns>
		/// <remarks>
		/// <para>
		/// Ignores the event being logged and just returns
		/// <see cref="FilterDecision.Deny"/>. This can be used to change the default filter
		/// chain behavior from <see cref="FilterDecision.Accept"/> to <see cref="FilterDecision.Deny"/>. This filter
		/// should only be used as the last filter in the chain
		/// as any further filters will be ignored!
		/// </para>
		/// </remarks>
		override public FilterDecision Decide(LoggingEvent loggingEvent) 
		{
			return FilterDecision.Deny;
		}

		#endregion
	}
}
