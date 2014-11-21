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

#if !NETCF

using System;
using System.Collections;
using System.Text;

using SmartInspect.Util;
using SmartInspect.Core;

namespace SmartInspect.Layout.Pattern
{
    /// <summary>
    /// Write the caller stack frames to the output
    /// </summary>
    /// <remarks>
    /// <para>
    /// Writes the <see cref="LocationInfo.StackFrames"/> to the output writer, using format:
    /// type3.MethodCall3(type param,...) > type2.MethodCall2(type param,...) > type1.MethodCall1(type param,...)
    /// </para>
    /// </remarks>
    /// <author>Adam Davies</author>
    internal class StackTraceDetailPatternConverter : StackTracePatternConverter
    {
        internal override string GetMethodInformation(MethodItem method)
        {
            string returnValue="";

            try
            {
                string param = "";
                string[] names = method.Parameters;
                StringBuilder sb = new StringBuilder();
                if (names != null && names.GetUpperBound(0) > 0)
                {
                    for (int i = 0; i <= names.GetUpperBound(0); i++)
                    {
                        sb.AppendFormat("{0}, ", names[i]);
                    }
                }

                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 2, 2);
                    param = sb.ToString();
                }

                returnValue=base.GetMethodInformation(method) + "(" + param + ")";
            }
            catch (Exception ex)
            {
                LogLog.Error(declaringType, "An exception ocurred while retreiving method information.", ex);
            }

            return returnValue;
        }

        #region Private Static Fields

        /// <summary>
        /// The fully qualified type of the StackTraceDetailPatternConverter class.
        /// </summary>
        /// <remarks>
        /// Used by the internal logger to record the Type of the
        /// log message.
        /// </remarks>
        private readonly static Type declaringType = typeof(StackTracePatternConverter);

        #endregion Private Static Fields
    }
}
#endif