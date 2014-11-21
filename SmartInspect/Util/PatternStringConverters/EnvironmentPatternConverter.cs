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

// .NET Compact Framework 1.0 has no support for Environment.GetEnvironmentVariable()
#if !NETCF

using System;
using System.Text;
using System.IO;

using SmartInspect.Util;
using SmartInspect.DateFormatter;
using SmartInspect.Core;

namespace SmartInspect.Util.PatternStringConverters
{
	/// <summary>
	/// Write an environment variable to the output
	/// </summary>
	/// <remarks>
	/// <para>
	/// Write an environment variable to the output writer.
	/// The value of the <see cref="SmartInspect.Util.PatternConverter.Option"/> determines 
	/// the name of the variable to output.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	internal sealed class EnvironmentPatternConverter : PatternConverter
	{
		/// <summary>
		/// Write an environment variable to the output
		/// </summary>
		/// <param name="writer">the writer to write to</param>
		/// <param name="state">null, state is not set</param>
		/// <remarks>
		/// <para>
		/// Writes the environment variable to the output <paramref name="writer"/>.
		/// The name of the environment variable to output must be set
		/// using the <see cref="SmartInspect.Util.PatternConverter.Option"/>
		/// property.
		/// </para>
		/// </remarks>
		override protected void Convert(TextWriter writer, object state) 
		{
			try 
			{
				if (this.Option != null && this.Option.Length > 0)
				{
					// Lookup the environment variable
					string envValue = Environment.GetEnvironmentVariable(this.Option);

                    // If we didn't see it for the process, try a user level variable.
				    if (envValue == null)
				    {
				        envValue = Environment.GetEnvironmentVariable(this.Option, EnvironmentVariableTarget.User);
				    }

                    // If we still didn't find it, try a system level one.
				    if (envValue == null)
				    {
				        envValue = Environment.GetEnvironmentVariable(this.Option, EnvironmentVariableTarget.Machine);
				    }

					if (envValue != null && envValue.Length > 0)
					{
						writer.Write(envValue);
					}
				}
			}
			catch(System.Security.SecurityException secEx)
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
	    /// The fully qualified type of the EnvironmentPatternConverter class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(EnvironmentPatternConverter);

	    #endregion Private Static Fields
	}
}

#endif // !NETCF
