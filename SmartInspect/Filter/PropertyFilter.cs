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
using System.Text.RegularExpressions;

using SmartInspect;
using SmartInspect.Core;
using SmartInspect.Util;

namespace SmartInspect.Filter
{
	/// <summary>
	/// Simple filter to match a string an event property
	/// </summary>
	/// <remarks>
	/// <para>
	/// Simple filter to match a string in the value for a
	/// specific event property
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public class PropertyFilter : StringMatchFilter
	{
		#region Member Variables

		/// <summary>
		/// The key to use to lookup the string from the event properties
		/// </summary>
		private string m_key;

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public PropertyFilter()
		{
		}

		#endregion

		/// <summary>
		/// The key to lookup in the event properties and then match against.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The key name to use to lookup in the properties map of the
		/// <see cref="LoggingEvent"/>. The match will be performed against 
		/// the value of this property if it exists.
		/// </para>
		/// </remarks>
		public string Key
		{
			get { return m_key; }
			set { m_key = value; }
		}

		#region Override implementation of FilterSkeleton

		/// <summary>
		/// Check if this filter should allow the event to be logged
		/// </summary>
		/// <param name="loggingEvent">the event being logged</param>
		/// <returns>see remarks</returns>
		/// <remarks>
		/// <para>
		/// The event property for the <see cref="Key"/> is matched against 
		/// the <see cref="StringMatchFilter.StringToMatch"/>.
		/// If the <see cref="StringMatchFilter.StringToMatch"/> occurs as a substring within
		/// the property value then a match will have occurred. If no match occurs
		/// this function will return <see cref="FilterDecision.Neutral"/>
		/// allowing other filters to check the event. If a match occurs then
		/// the value of <see cref="StringMatchFilter.AcceptOnMatch"/> is checked. If it is
		/// true then <see cref="FilterDecision.Accept"/> is returned otherwise
		/// <see cref="FilterDecision.Deny"/> is returned.
		/// </para>
		/// </remarks>
		override public FilterDecision Decide(LoggingEvent loggingEvent) 
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}

			// Check if we have a key to lookup the event property value with
			if (m_key == null)
			{
				// We cannot filter so allow the filter chain
				// to continue processing
				return FilterDecision.Neutral;
			}

			// Lookup the string to match in from the properties using 
			// the key specified.
			object msgObj = loggingEvent.LookupProperty(m_key);

			// Use an ObjectRenderer to convert the property value to a string
			string msg = loggingEvent.Repository.RendererMap.FindAndRender(msgObj);

			// Check if we have been setup to filter
			if (msg == null || (m_stringToMatch == null && m_regexToMatch == null))
			{
				// We cannot filter so allow the filter chain
				// to continue processing
				return FilterDecision.Neutral;
			}
    
			// Firstly check if we are matching using a regex
			if (m_regexToMatch != null)
			{
				// Check the regex
				if (m_regexToMatch.Match(msg).Success == false)
				{
					// No match, continue processing
					return FilterDecision.Neutral;
				} 

				// we've got a match
				if (m_acceptOnMatch) 
				{
					return FilterDecision.Accept;
				} 
				return FilterDecision.Deny;
			}
			else if (m_stringToMatch != null)
			{
				// Check substring match
				if (msg.IndexOf(m_stringToMatch) == -1) 
				{
					// No match, continue processing
					return FilterDecision.Neutral;
				} 

				// we've got a match
				if (m_acceptOnMatch) 
				{
					return FilterDecision.Accept;
				} 
				return FilterDecision.Deny;
			}
			return FilterDecision.Neutral;
		}

		#endregion
	}
}
