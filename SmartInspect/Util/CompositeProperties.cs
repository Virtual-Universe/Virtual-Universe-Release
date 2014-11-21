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
	/// This class aggregates several PropertiesDictionary collections together.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Provides a dictionary style lookup over an ordered list of
	/// <see cref="PropertiesDictionary"/> collections.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class CompositeProperties
	{
		#region Private Instance Fields

		private PropertiesDictionary m_flattened = null;
		private ArrayList m_nestedProperties = new ArrayList();

		#endregion Private Instance Fields

		#region Public Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="CompositeProperties" /> class.
		/// </para>
		/// </remarks>
		internal CompositeProperties()
		{
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets the value of a property
		/// </summary>
		/// <value>
		/// The value for the property with the specified key
		/// </value>
		/// <remarks>
		/// <para>
		/// Looks up the value for the <paramref name="key" /> specified.
		/// The <see cref="PropertiesDictionary"/> collections are searched
		/// in the order in which they were added to this collection. The value
		/// returned is the value held by the first collection that contains
		/// the specified key.
		/// </para>
		/// <para>
		/// If none of the collections contain the specified key then
		/// <c>null</c> is returned.
		/// </para>
		/// </remarks>
		public object this[string key]
		{
			get 
			{
				// Look in the flattened properties first
				if (m_flattened != null)
				{
					return m_flattened[key];
				}

				// Look for the key in all the nested properties
				foreach(ReadOnlyPropertiesDictionary cur in m_nestedProperties)
				{
					if (cur.Contains(key))
					{
						return cur[key];
					}
				}
				return null;
			}
		}

		#endregion Public Instance Properties

		#region Public Instance Methods

		/// <summary>
		/// Add a Properties Dictionary to this composite collection
		/// </summary>
		/// <param name="properties">the properties to add</param>
		/// <remarks>
		/// <para>
		/// Properties dictionaries added first take precedence over dictionaries added
		/// later.
		/// </para>
		/// </remarks>
		public void Add(ReadOnlyPropertiesDictionary properties)
		{
			m_flattened = null;
			m_nestedProperties.Add(properties);
		}

		/// <summary>
		/// Flatten this composite collection into a single properties dictionary
		/// </summary>
		/// <returns>the flattened dictionary</returns>
		/// <remarks>
		/// <para>
		/// Reduces the collection of ordered dictionaries to a single dictionary
		/// containing the resultant values for the keys.
		/// </para>
		/// </remarks>
		public PropertiesDictionary Flatten()
		{
			if (m_flattened == null)
			{
				m_flattened = new PropertiesDictionary();

				for(int i=m_nestedProperties.Count; --i>=0; )
				{
					ReadOnlyPropertiesDictionary cur = (ReadOnlyPropertiesDictionary)m_nestedProperties[i];

					foreach(DictionaryEntry entry in cur)
					{
						m_flattened[(string)entry.Key] = entry.Value;
					}
				}
			}
			return m_flattened;
		}

		#endregion Public Instance Methods
	}
}

