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
using System.IO;

namespace SmartInspect.Util.PatternStringConverters
{
    /// <summary>
    /// Write an <see cref="System.Environment.SpecialFolder" /> folder path to the output
    /// </summary>
    /// <remarks>
    /// <para>
    /// Write an special path environment folder path to the output writer.
    /// The value of the <see cref="SmartInspect.Util.PatternConverter.Option"/> determines 
    /// the name of the variable to output. <see cref="SmartInspect.Util.PatternConverter.Option"/>
    /// should be a value in the <see cref="System.Environment.SpecialFolder" /> enumeration.
    /// </para>
    /// </remarks>
    /// <author>Ron Grabowski</author>
    internal sealed class EnvironmentFolderPathPatternConverter : PatternConverter
    {
        /// <summary>
        /// Write an special path environment folder path to the output
        /// </summary>
        /// <param name="writer">the writer to write to</param>
        /// <param name="state">null, state is not set</param>
        /// <remarks>
        /// <para>
        /// Writes the special path environment folder path to the output <paramref name="writer"/>.
        /// The name of the special path environment folder path to output must be set
        /// using the <see cref="SmartInspect.Util.PatternConverter.Option"/>
        /// property.
        /// </para>
        /// </remarks>
        override protected void Convert(TextWriter writer, object state)
        {
            try
            {
                if (Option != null && Option.Length > 0)
                {
                    Environment.SpecialFolder specialFolder =
                        (Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), Option, true);

                    string envFolderPathValue = Environment.GetFolderPath(specialFolder);
                    if (envFolderPathValue != null && envFolderPathValue.Length > 0)
                    {
                        writer.Write(envFolderPathValue);
                    }
                }
            }
            catch (System.Security.SecurityException secEx)
            {
                // This security exception will occur if the caller does not have 
                // unrestricted environment permission. If this occurs the expansion 
                // will be skipped with the following warning message.
                LogLog.Debug(declaringType, "Security exception while trying to expand environment variables. Error Ignored. No Expansion.", secEx);
            }
            catch (Exception ex)
            {
                LogLog.Error(declaringType, "Error occurred while converting environment variable.", ex);
            }
        }

        #region Private Static Fields

        /// <summary>
        /// The fully qualified type of the EnvironmentFolderPathPatternConverter class.
        /// </summary>
        /// <remarks>
        /// Used by the internal logger to record the Type of the
        /// log message.
        /// </remarks>
        private readonly static Type declaringType = typeof(EnvironmentFolderPathPatternConverter);

        #endregion Private Static Fields
    }
}

#endif // !NETCF