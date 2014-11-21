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
using System.Collections;

namespace SmartInspect.Util
{
	/// <summary>
	/// Implementation of Properties collection for the <see cref="SmartInspect.GlobalContext"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class implements a properties collection that is thread safe and supports both
	/// storing properties and capturing a read only copy of the current propertied.
	/// </para>
	/// <para>
	/// This class is optimized to the scenario where the properties are read frequently
	/// and are modified infrequently.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class GlobalContextProperties : ContextPropertiesBase
	{
		#region Private Instance Fields

		/// <summary>
		/// The read only copy of the properties.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This variable is declared <c>volatile</c> to prevent the compiler and JIT from
		/// reordering reads and writes of this thread performed on different threads.
		/// </para>
		/// </remarks>
#if NETCF
		private ReadOnlyPropertiesDictionary m_readOnlyProperties = new ReadOnlyPropertiesDictionary();
#else
		private volatile ReadOnlyPropertiesDictionary m_readOnlyProperties = new ReadOnlyPropertiesDictionary();
#endif

		/// <summary>
		/// Lock object used to synchronize updates within this instance
		/// </summary>
		private readonly object m_syncRoot = new object();

		#endregion Private Instance Fields

		#region Public Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="GlobalContextProperties" /> class.
		/// </para>
		/// </remarks>
		internal GlobalContextProperties()
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
		/// Reading the value for a key is faster than setting the value.
		/// When the value is written a new read only copy of 
		/// the properties is created.
		/// </para>
		/// </remarks>
		override public object this[string key]
		{
			get 
			{ 
				return m_readOnlyProperties[key];
			}
			set
			{
				lock(m_syncRoot)
				{
					PropertiesDictionary mutableProps = new PropertiesDictionary(m_readOnlyProperties);

					mutableProps[key] = value;

					m_readOnlyProperties = new ReadOnlyPropertiesDictionary(mutableProps);
				}
			}
		}

		#endregion Public Instance Properties

		#region Public Instance Methods

		/// <summary>
		/// Remove a property from the global context
		/// </summary>
		/// <param name="key">the key for the entry to remove</param>
		/// <remarks>
		/// <para>
		/// Removing an entry from the global context properties is relatively expensive compared
		/// with reading a value. 
		/// </para>
		/// </remarks>
		public void Remove(string key)
		{
			lock(m_syncRoot)
			{
				if (m_readOnlyProperties.Contains(key))
				{
					PropertiesDictionary mutableProps = new PropertiesDictionary(m_readOnlyProperties);

					mutableProps.Remove(key);

					m_readOnlyProperties = new ReadOnlyPropertiesDictionary(mutableProps);
				}
			}
		}

		/// <summary>
		/// Clear the global context properties
		/// </summary>
		public void Clear()
		{
			lock(m_syncRoot)
			{
				m_readOnlyProperties = new ReadOnlyPropertiesDictionary();
			}
		}

		#endregion Public Instance Methods

		#region Internal Instance Methods

		/// <summary>
		/// Get a readonly immutable copy of the properties
		/// </summary>
		/// <returns>the current global context properties</returns>
		/// <remarks>
		/// <para>
		/// This implementation is fast because the GlobalContextProperties class
		/// stores a readonly copy of the properties.
		/// </para>
		/// </remarks>
		internal ReadOnlyPropertiesDictionary GetReadOnlyProperties()
		{
			return m_readOnlyProperties;
		}

		#endregion Internal Instance Methods
	}
}

