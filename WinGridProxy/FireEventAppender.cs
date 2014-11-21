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

namespace WinGridProxy
{
    public delegate void MessageLoggedEventHandler(object sender, MessageLoggedEventArgs e);

    public class MessageLoggedEventArgs : EventArgs
    {
        private LoggingEvent m_loggingEvent;

        public MessageLoggedEventArgs(LoggingEvent loggingEvent)
        {
            m_loggingEvent = loggingEvent;
        }
        public LoggingEvent LoggingEvent
        {
            get { return m_loggingEvent; }
        }
    }

    public class FireEventAppender : SmartInspect.Appender.AppenderSkeleton
    {
        private static FireEventAppender m_instance;

        private FixFlags m_fixFlags = FixFlags.All;

        // Event handler
        public event MessageLoggedEventHandler MessageLoggedEvent;

        // Easy singleton, gets the last instance created
        public static FireEventAppender Instance
        {
            get { return m_instance; }
        }

        public FireEventAppender()
        {
            // Store the instance created
            m_instance = this;
        }

        virtual public FixFlags Fix
        {
            get { return m_fixFlags; }
            set { m_fixFlags = value; }
        }

        override protected void Append(LoggingEvent loggingEvent)
        {
            // Because we the LoggingEvent may be used beyond the lifetime 
            // of the Append() method we must fix any volatile data in the event
            loggingEvent.Fix = this.Fix;

            // Raise the event
            MessageLoggedEventHandler handler = MessageLoggedEvent;
            if (handler != null)
            {
                handler(this, new MessageLoggedEventArgs(loggingEvent));
            }
        }

    }
}
