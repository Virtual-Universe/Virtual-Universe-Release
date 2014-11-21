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

using SmartInspect.Core;

namespace SmartInspect.Util
{
	/// <summary>
	/// Manages a mapping from levels to <see cref="LevelMappingEntry"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Manages an ordered mapping from <see cref="Level"/> instances 
	/// to <see cref="LevelMappingEntry"/> subclasses.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class LevelMapping : IOptionHandler
	{
		#region Public Instance Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initialise a new instance of <see cref="LevelMapping"/>.
		/// </para>
		/// </remarks>
		public LevelMapping() 
		{
		}

		#endregion // Public Instance Constructors

		#region Public Instance Methods
	
		/// <summary>
		/// Add a <see cref="LevelMappingEntry"/> to this mapping
		/// </summary>
		/// <param name="entry">the entry to add</param>
		/// <remarks>
		/// <para>
		/// If a <see cref="LevelMappingEntry"/> has previously been added
		/// for the same <see cref="Level"/> then that entry will be 
		/// overwritten.
		/// </para>
		/// </remarks>
		public void Add(LevelMappingEntry entry)
		{
			if (m_entriesMap.ContainsKey(entry.Level))
			{
				m_entriesMap.Remove(entry.Level);
			}
			m_entriesMap.Add(entry.Level, entry);
		}

		/// <summary>
		/// Lookup the mapping for the specified level
		/// </summary>
		/// <param name="level">the level to lookup</param>
		/// <returns>the <see cref="LevelMappingEntry"/> for the level or <c>null</c> if no mapping found</returns>
		/// <remarks>
		/// <para>
		/// Lookup the value for the specified level. Finds the nearest
		/// mapping value for the level that is equal to or less than the
		/// <paramref name="level"/> specified.
		/// </para>
		/// <para>
		/// If no mapping could be found then <c>null</c> is returned.
		/// </para>
		/// </remarks>
		public LevelMappingEntry Lookup(Level level)
		{
			if (m_entries != null)
			{
				foreach(LevelMappingEntry entry in m_entries)
				{
					if (level >= entry.Level)
					{
						return entry;
					}
				}
			}
			return null;
		}

		#endregion // Public Instance Methods

		#region IOptionHandler Members

		/// <summary>
		/// Initialize options
		/// </summary>
		/// <remarks>
		/// <para>
		/// Caches the sorted list of <see cref="LevelMappingEntry"/> in an array
		/// </para>
		/// </remarks>
		public void ActivateOptions()
		{
			Level[] sortKeys = new Level[m_entriesMap.Count];
			LevelMappingEntry[] sortValues = new LevelMappingEntry[m_entriesMap.Count];

			m_entriesMap.Keys.CopyTo(sortKeys, 0);
			m_entriesMap.Values.CopyTo(sortValues, 0);

			// Sort in level order
			Array.Sort(sortKeys, sortValues, 0, sortKeys.Length, null);

			// Reverse list so that highest level is first
			Array.Reverse(sortValues, 0, sortValues.Length);

			foreach(LevelMappingEntry entry in sortValues)
			{
				entry.ActivateOptions();
			}

			 m_entries = sortValues;
		}

		#endregion // IOptionHandler Members

		#region Private Instance Fields

		private Hashtable m_entriesMap = new Hashtable();
		private LevelMappingEntry[] m_entries = null;

		#endregion // Private Instance Fields
	}
}
