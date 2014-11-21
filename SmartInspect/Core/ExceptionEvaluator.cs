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
	/// An evaluator that triggers on an Exception type
	/// </summary>
	/// <remarks>
	/// <para>
	/// This evaluator will trigger if the type of the Exception
	/// passed to <see cref="M:IsTriggeringEvent(LoggingEvent)"/>
	/// is equal to a Type in <see cref="ExceptionType"/>.    /// 
	/// </para>
	/// </remarks>
	/// <author>Drew Schaeffer</author>
	public class ExceptionEvaluator : ITriggeringEventEvaluator
	{
		/// <summary>
		/// The type that causes the trigger to fire.
		/// </summary>
		private Type m_type;

		/// <summary>
		/// Causes subclasses of <see cref="ExceptionType"/> to cause the trigger to fire.
		/// </summary>
		private bool m_triggerOnSubclass;

		/// <summary>
		/// Default ctor to allow dynamic creation through a configurator.
		/// </summary>
		public ExceptionEvaluator()
		{
			// empty
		}

		/// <summary>
		/// Constructs an evaluator and initializes to trigger on <paramref name="exType"/>
		/// </summary>
		/// <param name="exType">the type that triggers this evaluator.</param>
		/// <param name="triggerOnSubClass">If true, this evaluator will trigger on subclasses of <see cref="ExceptionType"/>.</param>
		public ExceptionEvaluator(Type exType, bool triggerOnSubClass)
		{
			if (exType == null)
			{
				throw new ArgumentNullException("exType");
			}

			m_type = exType;
			m_triggerOnSubclass = triggerOnSubClass;
		}

		/// <summary>
		/// The type that triggers this evaluator.
		/// </summary>
		public Type ExceptionType
		{
			get { return m_type; }
			set { m_type = value; }
		}

		/// <summary>
		/// If true, this evaluator will trigger on subclasses of <see cref="ExceptionType"/>.
		/// </summary>
		public bool TriggerOnSubclass
		{
			get { return m_triggerOnSubclass; }
			set { m_triggerOnSubclass = value; }
		}

		#region ITriggeringEventEvaluator Members

		/// <summary>
		/// Is this <paramref name="loggingEvent"/> the triggering event?
		/// </summary>
		/// <param name="loggingEvent">The event to check</param>
		/// <returns>This method returns <c>true</c>, if the logging event Exception 
		/// Type is <see cref="ExceptionType"/>. 
		/// Otherwise it returns <c>false</c></returns>
		/// <remarks>
		/// <para>
		/// This evaluator will trigger if the Exception Type of the event
		/// passed to <see cref="M:IsTriggeringEvent(LoggingEvent)"/>
		/// is <see cref="ExceptionType"/>.
		/// </para>
		/// </remarks>
		public bool IsTriggeringEvent(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}

			if (m_triggerOnSubclass && loggingEvent.ExceptionObject != null)
			{
				// check if loggingEvent.ExceptionObject is of type ExceptionType or subclass of ExceptionType
				Type exceptionObjectType = loggingEvent.ExceptionObject.GetType();
				return exceptionObjectType == m_type || exceptionObjectType.IsSubclassOf(m_type);
			}
			else if (!m_triggerOnSubclass && loggingEvent.ExceptionObject != null)
			{   // check if loggingEvent.ExceptionObject is of type ExceptionType
				return loggingEvent.ExceptionObject.GetType() == m_type;
			}
			else
			{   // loggingEvent.ExceptionObject is null
				return false;
			}
		}

		#endregion
	}
}
