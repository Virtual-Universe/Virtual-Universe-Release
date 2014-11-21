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
    /// An evaluator that triggers after specified number of seconds.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This evaluator will trigger if the specified time period 
    /// <see cref="Interval"/> has passed since last check.
    /// </para>
    /// </remarks>
    /// <author>Robert Sevcik</author>
    public class TimeEvaluator : ITriggeringEventEvaluator
    {
        /// <summary>
        /// The time threshold for triggering in seconds. Zero means it won't trigger at all.
        /// </summary>
        private int m_interval;

        /// <summary>
        /// The time of last check. This gets updated when the object is created and when the evaluator triggers.
        /// </summary>
        private DateTime m_lasttime;

        /// <summary>
        /// The default time threshold for triggering in seconds. Zero means it won't trigger at all.
        /// </summary>
        const int DEFAULT_INTERVAL = 0;

        /// <summary>
        /// Create a new evaluator using the <see cref="DEFAULT_INTERVAL"/> time threshold in seconds.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Create a new evaluator using the <see cref="DEFAULT_INTERVAL"/> time threshold in seconds.
        /// </para>
        /// <para>
        /// This evaluator will trigger if the specified time period 
        /// <see cref="Interval"/> has passed since last check.
        /// </para>
        /// </remarks>
        public TimeEvaluator()
            : this(DEFAULT_INTERVAL)
        {
        }

        /// <summary>
        /// Create a new evaluator using the specified time threshold in seconds.
        /// </summary>
        /// <param name="interval">
        /// The time threshold in seconds to trigger after.
        /// Zero means it won't trigger at all.
        /// </param>
        /// <remarks>
        /// <para>
        /// Create a new evaluator using the specified time threshold in seconds.
        /// </para>
        /// <para>
        /// This evaluator will trigger if the specified time period 
        /// <see cref="Interval"/> has passed since last check.
        /// </para>
        /// </remarks>
        public TimeEvaluator(int interval)
        {
            m_interval = interval;
            m_lasttime = DateTime.Now;
        }

        /// <summary>
        /// The time threshold in seconds to trigger after
        /// </summary>
        /// <value>
        /// The time threshold in seconds to trigger after.
        /// Zero means it won't trigger at all.
        /// </value>
        /// <remarks>
        /// <para>
        /// This evaluator will trigger if the specified time period 
        /// <see cref="Interval"/> has passed since last check.
        /// </para>
        /// </remarks>
        public int Interval
        {
            get { return m_interval; }
            set { m_interval = value; }
        }

        /// <summary>
        /// Is this <paramref name="loggingEvent"/> the triggering event?
        /// </summary>
        /// <param name="loggingEvent">The event to check</param>
        /// <returns>This method returns <c>true</c>, if the specified time period 
        /// <see cref="Interval"/> has passed since last check.. 
        /// Otherwise it returns <c>false</c></returns>
        /// <remarks>
        /// <para>
        /// This evaluator will trigger if the specified time period 
        /// <see cref="Interval"/> has passed since last check.
        /// </para>
        /// </remarks>
        public bool IsTriggeringEvent(LoggingEvent loggingEvent)
        {
            if (loggingEvent == null)
            {
                throw new ArgumentNullException("loggingEvent");
            }

            // disable the evaluator if threshold is zero
            if (m_interval == 0) return false;

            lock (this) // avoid triggering multiple times
            {
                TimeSpan passed = DateTime.Now.Subtract(m_lasttime);

                if (passed.TotalSeconds > m_interval)
                {
                    m_lasttime = DateTime.Now;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
