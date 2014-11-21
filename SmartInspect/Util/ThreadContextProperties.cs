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
#if NETCF
using System.Collections;
#endif

namespace SmartInspect.Util
{
	/// <summary>
	/// Implementation of Properties collection for the <see cref="SmartInspect.ThreadContext"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Class implements a collection of properties that is specific to each thread.
	/// The class is not synchronized as each thread has its own <see cref="PropertiesDictionary"/>.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class ThreadContextProperties : ContextPropertiesBase
	{
		#region Private Instance Fields

#if NETCF
		/// <summary>
		/// The thread local data slot to use to store a PropertiesDictionary.
		/// </summary>
		private readonly static LocalDataStoreSlot s_threadLocalSlot = System.Threading.Thread.AllocateDataSlot();
#else
		/// <summary>
		/// Each thread will automatically have its instance.
		/// </summary>
		[ThreadStatic]
		private static PropertiesDictionary _dictionary;
#endif

		#endregion Private Instance Fields

		#region Public Instance Constructors

		/// <summary>
		/// Internal constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="ThreadContextProperties" /> class.
		/// </para>
		/// </remarks>
		internal ThreadContextProperties()
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
		/// Gets or sets the value of a property
		/// </para>
		/// </remarks>
		override public object this[string key]
		{
			get
			{
#if NETCF
				PropertiesDictionary _dictionary = GetProperties(false);
#endif
				if (_dictionary != null)
				{
					return _dictionary[key];
				}
				return null;
			}
			set
			{
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
		/// Remove a property
		/// </para>
		/// </remarks>
		public void Remove(string key)
		{
#if NETCF
			PropertiesDictionary _dictionary = GetProperties(false);
#endif
			if (_dictionary != null)
			{
				_dictionary.Remove(key);
			}
		}

		/// <summary>
		/// Get the keys stored in the properties.
		/// </summary>
		/// <para>
		/// Gets the keys stored in the properties.
		/// </para>
		/// <returns>a set of the defined keys</returns>
		public string[] GetKeys()
		{
#if NETCF
			PropertiesDictionary _dictionary = GetProperties(false);
#endif
			if (_dictionary != null)
			{
				return _dictionary.GetKeys();
			}
			return null;
		}

		/// <summary>
		/// Clear all properties
		/// </summary>
		/// <remarks>
		/// <para>
		/// Clear all properties
		/// </para>
		/// </remarks>
		public void Clear()
		{
#if NETCF
			PropertiesDictionary _dictionary = GetProperties(false);
#endif
			if (_dictionary != null)
			{
				_dictionary.Clear();
			}
		}

		#endregion Public Instance Methods

		#region Internal Instance Methods

		/// <summary>
		/// Get the <c>PropertiesDictionary</c> for this thread.
		/// </summary>
		/// <param name="create">create the dictionary if it does not exist, otherwise return null if does not exist</param>
		/// <returns>the properties for this thread</returns>
		/// <remarks>
		/// <para>
		/// The collection returned is only to be used on the calling thread. If the
		/// caller needs to share the collection between different threads then the 
		/// caller must clone the collection before doing so.
		/// </para>
		/// </remarks>
		internal PropertiesDictionary GetProperties(bool create)
		{
#if NETCF
			PropertiesDictionary _dictionary = (PropertiesDictionary)System.Threading.Thread.GetData(s_threadLocalSlot);
#endif
			if (_dictionary == null && create)
			{
				_dictionary  = new PropertiesDictionary();
#if NETCF
				System.Threading.Thread.SetData(s_threadLocalSlot, _dictionary);
#endif
			}
			return _dictionary;
		}

		#endregion Internal Instance Methods
	}
}

