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
using System.Text;
using System.IO;
using System.Collections;

using SmartInspect.Core;
using SmartInspect.Util;
using SmartInspect.Repository;

namespace SmartInspect.Util.PatternStringConverters
{
	/// <summary>
	/// Property pattern converter
	/// </summary>
	/// <remarks>
	/// <para>
	/// This pattern converter reads the thread and global properties.
	/// The thread properties take priority over global properties.
	/// See <see cref="ThreadContext.Properties"/> for details of the 
	/// thread properties. See <see cref="GlobalContext.Properties"/> for
	/// details of the global properties.
	/// </para>
	/// <para>
	/// If the <see cref="PatternConverter.Option"/> is specified then that will be used to
	/// lookup a single property. If no <see cref="PatternConverter.Option"/> is specified
	/// then all properties will be dumped as a list of key value pairs.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	internal sealed class PropertyPatternConverter : PatternConverter 
	{
		/// <summary>
		/// Write the property value to the output
		/// </summary>
		/// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
		/// <param name="state">null, state is not set</param>
		/// <remarks>
		/// <para>
		/// Writes out the value of a named property. The property name
		/// should be set in the <see cref="SmartInspect.Util.PatternConverter.Option"/>
		/// property.
		/// </para>
		/// <para>
		/// If the <see cref="SmartInspect.Util.PatternConverter.Option"/> is set to <c>null</c>
		/// then all the properties are written as key value pairs.
		/// </para>
		/// </remarks>
		override protected void Convert(TextWriter writer, object state) 
		{
			CompositeProperties compositeProperties = new CompositeProperties();

#if !NETCF
			PropertiesDictionary logicalThreadProperties = LogicalThreadContext.Properties.GetProperties(false);
			if (logicalThreadProperties != null)
			{
				compositeProperties.Add(logicalThreadProperties);
			}
#endif
			PropertiesDictionary threadProperties = ThreadContext.Properties.GetProperties(false);
			if (threadProperties != null)
			{
				compositeProperties.Add(threadProperties);
			}

			// TODO: Add Repository Properties
			compositeProperties.Add(GlobalContext.Properties.GetReadOnlyProperties());

			if (Option != null)
			{
				// Write the value for the specified key
				WriteObject(writer, null, compositeProperties[Option]);
			}
			else
			{
				// Write all the key value pairs
				WriteDictionary(writer, null, compositeProperties.Flatten());
			}
		}
	}
}
