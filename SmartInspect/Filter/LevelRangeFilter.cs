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

using SmartInspect;
using SmartInspect.Core;
using SmartInspect.Util;

namespace SmartInspect.Filter
{
	/// <summary>
	/// This is a simple filter based on <see cref="Level"/> matching.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The filter admits three options <see cref="LevelMin"/> and <see cref="LevelMax"/>
	/// that determine the range of priorities that are matched, and
	/// <see cref="AcceptOnMatch"/>. If there is a match between the range
	/// of priorities and the <see cref="Level"/> of the <see cref="LoggingEvent"/>, then the 
	/// <see cref="Decide"/> method returns <see cref="FilterDecision.Accept"/> in case the <see cref="AcceptOnMatch"/> 
	/// option value is set to <c>true</c>, if it is <c>false</c>
	/// then <see cref="FilterDecision.Deny"/> is returned. If there is no match, <see cref="FilterDecision.Deny"/> is returned.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class LevelRangeFilter : FilterSkeleton
	{
		#region Member Variables

		/// <summary>
		/// Flag to indicate the behavior when matching a <see cref="Level"/>
		/// </summary>
		private bool m_acceptOnMatch = true;

		/// <summary>
		/// the minimum <see cref="Level"/> value to match
		/// </summary>
		private Level m_levelMin;

		/// <summary>
		/// the maximum <see cref="Level"/> value to match
		/// </summary>
		private Level m_levelMax;

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public LevelRangeFilter()
		{
		}

		#endregion

		/// <summary>
		/// <see cref="FilterDecision.Accept"/> when matching <see cref="LevelMin"/> and <see cref="LevelMax"/>
		/// </summary>
		/// <remarks>
		/// <para>
		/// The <see cref="AcceptOnMatch"/> property is a flag that determines
		/// the behavior when a matching <see cref="Level"/> is found. If the
		/// flag is set to true then the filter will <see cref="FilterDecision.Accept"/> the 
		/// logging event, otherwise it will <see cref="FilterDecision.Neutral"/> the event.
		/// </para>
		/// <para>
		/// The default is <c>true</c> i.e. to <see cref="FilterDecision.Accept"/> the event.
		/// </para>
		/// </remarks>
		public bool AcceptOnMatch
		{
			get { return m_acceptOnMatch; }
			set { m_acceptOnMatch = value; }
		}

		/// <summary>
		/// Set the minimum matched <see cref="Level"/>
		/// </summary>
		/// <remarks>
		/// <para>
		/// The minimum level that this filter will attempt to match against the 
		/// <see cref="LoggingEvent"/> level. If a match is found then
		/// the result depends on the value of <see cref="AcceptOnMatch"/>.
		/// </para>
		/// </remarks>
		public Level LevelMin
		{
			get { return m_levelMin; }
			set { m_levelMin = value; }
		}

		/// <summary>
		/// Sets the maximum matched <see cref="Level"/>
		/// </summary>
		/// <remarks>
		/// <para>
		/// The maximum level that this filter will attempt to match against the 
		/// <see cref="LoggingEvent"/> level. If a match is found then
		/// the result depends on the value of <see cref="AcceptOnMatch"/>.
		/// </para>
		/// </remarks>
		public Level LevelMax
		{
			get { return m_levelMax; }
			set { m_levelMax = value; }
		}

		#region Override implementation of FilterSkeleton

		/// <summary>
		/// Check if the event should be logged.
		/// </summary>
		/// <param name="loggingEvent">the logging event to check</param>
		/// <returns>see remarks</returns>
		/// <remarks>
		/// <para>
		/// If the <see cref="Level"/> of the logging event is outside the range
		/// matched by this filter then <see cref="FilterDecision.Deny"/>
		/// is returned. If the <see cref="Level"/> is matched then the value of
		/// <see cref="AcceptOnMatch"/> is checked. If it is true then
		/// <see cref="FilterDecision.Accept"/> is returned, otherwise
		/// <see cref="FilterDecision.Neutral"/> is returned.
		/// </para>
		/// </remarks>
		override public FilterDecision Decide(LoggingEvent loggingEvent) 
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}

			if (m_levelMin != null) 
			{
				if (loggingEvent.Level < m_levelMin) 
				{
					// level of event is less than minimum
					return FilterDecision.Deny;
				}
			}

			if (m_levelMax != null) 
			{
				if (loggingEvent.Level > m_levelMax) 
				{
					// level of event is greater than maximum
					return FilterDecision.Deny;
				}
			}

			if (m_acceptOnMatch) 
			{
				// this filter set up to bypass later filters and always return
				// accept if level in range
				return FilterDecision.Accept;
			}
			else 
			{
				// event is ok for this filter; allow later filters to have a look..
				return FilterDecision.Neutral;
			}
		}

		#endregion
	}
}
