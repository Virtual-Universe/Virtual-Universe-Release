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

// .NET Compact Framework 1.0 has no support for System.Runtime.Remoting.Messaging.CallContext
#if !NETCF

using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace SmartInspect.Util
{
	/// <summary>
	/// Implementation of Properties collection for the <see cref="SmartInspect.LogicalThreadContext"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Class implements a collection of properties that is specific to each thread.
	/// The class is not synchronized as each thread has its own <see cref="PropertiesDictionary"/>.
	/// </para>
	/// <para>
	/// This class stores its properties in a slot on the <see cref="CallContext"/> named
	/// <c>SmartInspect.Util.LogicalThreadContextProperties</c>.
	/// </para>
	/// <para>
	/// The <see cref="CallContext"/> requires a link time 
	/// <see cref="System.Security.Permissions.SecurityPermission"/> for the
	/// <see cref="System.Security.Permissions.SecurityPermissionFlag.Infrastructure"/>.
	/// If the calling code does not have this permission then this context will be disabled.
	/// It will not store any property values set on it.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class LogicalThreadContextProperties : ContextPropertiesBase
	{
		private const string c_SlotName = "SmartInspect.Util.LogicalThreadContextProperties";
		
		/// <summary>
		/// Flag used to disable this context if we don't have permission to access the CallContext.
		/// </summary>
		private bool m_disabled = false;
		
		#region Public Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="LogicalThreadContextProperties" /> class.
		/// </para>
		/// </remarks>
		internal LogicalThreadContextProperties()
		{
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets the value of a property
		/// </summary>
		/// <value>
		/// The value for the property with the specified key
		/// </value>
		/// <remarks>
		/// <para>
		/// Get or set the property value for the <paramref name="key"/> specified.
		/// </para>
		/// </remarks>
		override public object this[string key]
		{
			get 
			{ 
				// Don't create the dictionary if it does not already exist
				PropertiesDictionary dictionary = GetProperties(false);
				if (dictionary != null)
				{
					return dictionary[key]; 
				}
				return null;
			}
			set 
			{ 
				// Force the dictionary to be created
				GetProperties(true)[key] = value; 
			}
		}

		#endregion Public Instance Properties

		#region Public Instance Methods

		/// <summary>
		/// Remove a property
		/// </summary>
		/// <param name="key">the key for the entry to remove</param>
		/// <remarks>
		/// <para>
		/// Remove the value for the specified <paramref name="key"/> from the context.
		/// </para>
		/// </remarks>
		public void Remove(string key)
		{
			PropertiesDictionary dictionary = GetProperties(false);
			if (dictionary != null)
			{
				dictionary.Remove(key);
			}
		}

		/// <summary>
		/// Clear all the context properties
		/// </summary>
		/// <remarks>
		/// <para>
		/// Clear all the context properties
		/// </para>
		/// </remarks>
		public void Clear()
		{
			PropertiesDictionary dictionary = GetProperties(false);
			if (dictionary != null)
			{
				dictionary.Clear();
			}
		}

		#endregion Public Instance Methods

		#region Internal Instance Methods

		/// <summary>
		/// Get the PropertiesDictionary stored in the LocalDataStoreSlot for this thread.
		/// </summary>
		/// <param name="create">create the dictionary if it does not exist, otherwise return null if is does not exist</param>
		/// <returns>the properties for this thread</returns>
		/// <remarks>
		/// <para>
		/// The collection returned is only to be used on the calling thread. If the
		/// caller needs to share the collection between different threads then the 
		/// caller must clone the collection before doings so.
		/// </para>
		/// </remarks>
		internal PropertiesDictionary GetProperties(bool create)
		{
			if (!m_disabled)
			{
				try
				{
					PropertiesDictionary properties = GetCallContextData();
					if (properties == null && create)
					{
						properties = new PropertiesDictionary();
						SetCallContextData(properties);
					}
					return properties;
				}
				catch (SecurityException secEx)
				{
					m_disabled = true;
					
					// Thrown if we don't have permission to read or write the CallContext
					LogLog.Warn(declaringType, "SecurityException while accessing CallContext. Disabling LogicalThreadContextProperties", secEx);
				}
			}
			
			// Only get here is we are disabled because of a security exception
			if (create)
			{
				return new PropertiesDictionary();
			}
			return null;
		}

		#endregion Internal Instance Methods

        #region Private Static Methods

        /// <summary>
		/// Gets the call context get data.
		/// </summary>
		/// <returns>The peroperties dictionary stored in the call context</returns>
		/// <remarks>
		/// The <see cref="CallContext"/> method <see cref="CallContext.GetData"/> has a
		/// security link demand, therfore we must put the method call in a seperate method
		/// that we can wrap in an exception handler.
		/// </remarks>
#if FRAMEWORK_4_0_OR_ABOVE
        [System.Security.SecuritySafeCritical]
#endif
        private static PropertiesDictionary GetCallContextData()
		{
#if !NETCF
            return CallContext.LogicalGetData(c_SlotName) as PropertiesDictionary;
#else
			return CallContext.GetData(c_SlotName) as PropertiesDictionary;
#endif
		}

		/// <summary>
		/// Sets the call context data.
		/// </summary>
		/// <param name="properties">The properties.</param>
		/// <remarks>
		/// The <see cref="CallContext"/> method <see cref="CallContext.SetData"/> has a
		/// security link demand, therfore we must put the method call in a seperate method
		/// that we can wrap in an exception handler.
		/// </remarks>
#if FRAMEWORK_4_0_OR_ABOVE
        [System.Security.SecuritySafeCritical]
#endif
        private static void SetCallContextData(PropertiesDictionary properties)
		{
#if !NETCF
			CallContext.LogicalSetData(c_SlotName, properties);
#else
			CallContext.SetData(c_SlotName, properties);
#endif
        }

        #endregion

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the LogicalThreadContextProperties class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(LogicalThreadContextProperties);

	    #endregion Private Static Fields
    }
}

#endif
