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

// .NET Compact Framework has no support for ASP.NET
#if !NETCF && !CLIENT_PROFILE

using System.IO;
using System.Web;
using SmartInspect.Core;
using SmartInspect.Util;

namespace SmartInspect.Layout.Pattern
{
	/// <summary>
	/// Converter for items in the ASP.Net Cache.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Outputs an item from the <see cref="HttpRuntime.Cache" />.
	/// </para>
	/// </remarks>
	/// <author>Ron Grabowski</author>
	internal sealed class AspNetCachePatternConverter : AspNetPatternLayoutConverter
	{
		/// <summary>
		/// Write the ASP.Net Cache item to the output
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="loggingEvent">The <see cref="LoggingEvent" /> on which the pattern converter should be executed.</param>
		/// <param name="httpContext">The <see cref="HttpContext" /> under which the ASP.Net request is running.</param>
		/// <remarks>
		/// <para>
		/// Writes out the value of a named property. The property name
		/// should be set in the <see cref="SmartInspect.Util.PatternConverter.Option"/>
		/// property. If no property has been set, all key value pairs from the Cache will
		/// be written to the output.
		/// </para>
		/// </remarks>
		protected override void Convert(TextWriter writer, LoggingEvent loggingEvent, HttpContext httpContext)
		{
			if (HttpRuntime.Cache != null)
			{
				if (Option != null)
				{
					WriteObject(writer, loggingEvent.Repository, HttpRuntime.Cache[Option]);
				}
				else
				{
					WriteObject(writer, loggingEvent.Repository, HttpRuntime.Cache.GetEnumerator());
				}
			}
			else
			{
				writer.Write(SystemInfo.NotAvailableText);
			}
		}
	}
}

#endif