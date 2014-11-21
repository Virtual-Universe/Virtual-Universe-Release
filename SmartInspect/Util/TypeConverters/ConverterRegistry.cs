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
using System.Globalization;
using System.Reflection;
using System.Collections;

namespace SmartInspect.Util.TypeConverters
{
	/// <summary>
	/// Register of type converters for specific types.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Maintains a registry of type converters used to convert between
	/// types.
	/// </para>
	/// <para>
	/// Use the <see cref="M:AddConverter(Type, object)"/> and 
	/// <see cref="M:AddConverter(Type, Type)"/> methods to register new converters.
	/// The <see cref="GetConvertTo"/> and <see cref="GetConvertFrom"/> methods
	/// lookup appropriate converters to use.
	/// </para>
	/// </remarks>
	/// <seealso cref="IConvertFrom"/>
	/// <seealso cref="IConvertTo"/>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public sealed class ConverterRegistry
	{
		#region Private Constructors

		/// <summary>
		/// Private constructor
		/// </summary>
		/// <remarks>
		/// Initializes a new instance of the <see cref="ConverterRegistry" /> class.
		/// </remarks>
		private ConverterRegistry() 
		{
		}

		#endregion Private Constructors

		#region Static Constructor

		/// <summary>
		/// Static constructor.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This constructor defines the intrinsic type converters.
		/// </para>
		/// </remarks>
		static ConverterRegistry()
		{
			// Add predefined converters here
			AddConverter(typeof(bool), typeof(BooleanConverter));
			AddConverter(typeof(System.Text.Encoding), typeof(EncodingConverter));
			AddConverter(typeof(System.Type), typeof(TypeConverter));
			AddConverter(typeof(SmartInspect.Layout.PatternLayout), typeof(PatternLayoutConverter));
			AddConverter(typeof(SmartInspect.Util.PatternString), typeof(PatternStringConverter));
			AddConverter(typeof(System.Net.IPAddress), typeof(IPAddressConverter));
		}

		#endregion Static Constructor

		#region Public Static Methods

		/// <summary>
		/// Adds a converter for a specific type.
		/// </summary>
		/// <param name="destinationType">The type being converted to.</param>
		/// <param name="converter">The type converter to use to convert to the destination type.</param>
		/// <remarks>
		/// <para>
		/// Adds a converter instance for a specific type.
		/// </para>
		/// </remarks>
		public static void AddConverter(Type destinationType, object converter)
		{
			if (destinationType != null && converter != null)
			{
				lock(s_type2converter)
				{
					s_type2converter[destinationType] = converter;
				}
			}
		}

		/// <summary>
		/// Adds a converter for a specific type.
		/// </summary>
		/// <param name="destinationType">The type being converted to.</param>
		/// <param name="converterType">The type of the type converter to use to convert to the destination type.</param>
		/// <remarks>
		/// <para>
		/// Adds a converter <see cref="Type"/> for a specific type.
		/// </para>
		/// </remarks>
		public static void AddConverter(Type destinationType, Type converterType)
		{
			AddConverter(destinationType, CreateConverterInstance(converterType));
		}

		/// <summary>
		/// Gets the type converter to use to convert values to the destination type.
		/// </summary>
		/// <param name="sourceType">The type being converted from.</param>
		/// <param name="destinationType">The type being converted to.</param>
		/// <returns>
		/// The type converter instance to use for type conversions or <c>null</c> 
		/// if no type converter is found.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Gets the type converter to use to convert values to the destination type.
		/// </para>
		/// </remarks>
		public static IConvertTo GetConvertTo(Type sourceType, Type destinationType)
		{
			// TODO: Support inheriting type converters.
			// i.e. getting a type converter for a base of sourceType

			// TODO: Is destinationType required? We don't use it for anything.

			lock(s_type2converter)
			{
				// Lookup in the static registry
				IConvertTo converter = s_type2converter[sourceType] as IConvertTo;

				if (converter == null)
				{
					// Lookup using attributes
					converter = GetConverterFromAttribute(sourceType) as IConvertTo;

					if (converter != null)
					{
						// Store in registry
						s_type2converter[sourceType] = converter;
					}
				}

				return converter;
			}
		}

		/// <summary>
		/// Gets the type converter to use to convert values to the destination type.
		/// </summary>
		/// <param name="destinationType">The type being converted to.</param>
		/// <returns>
		/// The type converter instance to use for type conversions or <c>null</c> 
		/// if no type converter is found.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Gets the type converter to use to convert values to the destination type.
		/// </para>
		/// </remarks>
		public static IConvertFrom GetConvertFrom(Type destinationType)
		{
			// TODO: Support inheriting type converters.
			// i.e. getting a type converter for a base of destinationType

			lock(s_type2converter)
			{
				// Lookup in the static registry
				IConvertFrom converter = s_type2converter[destinationType] as IConvertFrom;

				if (converter == null)
				{
					// Lookup using attributes
					converter = GetConverterFromAttribute(destinationType) as IConvertFrom;

					if (converter != null)
					{
						// Store in registry
						s_type2converter[destinationType] = converter;
					}
				}

				return converter;
			}
		}
		
		/// <summary>
		/// Lookups the type converter to use as specified by the attributes on the 
		/// destination type.
		/// </summary>
		/// <param name="destinationType">The type being converted to.</param>
		/// <returns>
		/// The type converter instance to use for type conversions or <c>null</c> 
		/// if no type converter is found.
		/// </returns>
		private static object GetConverterFromAttribute(Type destinationType)
		{
			// Look for an attribute on the destination type
			object[] attributes = destinationType.GetCustomAttributes(typeof(TypeConverterAttribute), true);
			if (attributes != null && attributes.Length > 0)
			{
				TypeConverterAttribute tcAttr = attributes[0] as TypeConverterAttribute;
				if (tcAttr != null)
				{
					Type converterType = SystemInfo.GetTypeFromString(destinationType, tcAttr.ConverterTypeName, false, true);
					return CreateConverterInstance(converterType);
				}
			}

			// Not found converter using attributes
			return null;
		}

		/// <summary>
		/// Creates the instance of the type converter.
		/// </summary>
		/// <param name="converterType">The type of the type converter.</param>
		/// <returns>
		/// The type converter instance to use for type conversions or <c>null</c> 
		/// if no type converter is found.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The type specified for the type converter must implement 
		/// the <see cref="IConvertFrom"/> or <see cref="IConvertTo"/> interfaces 
		/// and must have a public default (no argument) constructor.
		/// </para>
		/// </remarks>
		private static object CreateConverterInstance(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType", "CreateConverterInstance cannot create instance, converterType is null");
			}

			// Check type is a converter
			if (typeof(IConvertFrom).IsAssignableFrom(converterType) || typeof(IConvertTo).IsAssignableFrom(converterType))
			{
				try
				{
					// Create the type converter
					return Activator.CreateInstance(converterType);
				}
				catch(Exception ex)
				{
					LogLog.Error(declaringType, "Cannot CreateConverterInstance of type ["+converterType.FullName+"], Exception in call to Activator.CreateInstance", ex);
				}
			}
			else
			{
				LogLog.Error(declaringType, "Cannot CreateConverterInstance of type ["+converterType.FullName+"], type does not implement IConvertFrom or IConvertTo");
			}
			return null;
		}

		#endregion Public Static Methods

		#region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the ConverterRegistry class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(ConverterRegistry);

		/// <summary>
		/// Mapping from <see cref="Type" /> to type converter.
		/// </summary>
		private static Hashtable s_type2converter = new Hashtable();

		#endregion
	}
}
