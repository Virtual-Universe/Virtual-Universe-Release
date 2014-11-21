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
using System.Collections.Specialized;

using SmartInspect.Util;

namespace SmartInspect.Core
{
	/// <summary>
	/// Mapping between string name and Level object
	/// </summary>
	/// <remarks>
	/// <para>
	/// Mapping between string name and <see cref="Level"/> object.
	/// This mapping is held separately for each <see cref="SmartInspect.Repository.ILoggerRepository"/>.
	/// The level name is case insensitive.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class LevelMap
	{
		#region Member Variables

		/// <summary>
		/// Mapping from level name to Level object. The
		/// level name is case insensitive
		/// </summary>
		private Hashtable m_mapName2Level = SystemInfo.CreateCaseInsensitiveHashtable();

		#endregion

		/// <summary>
		/// Construct the level map
		/// </summary>
		/// <remarks>
		/// <para>
		/// Construct the level map.
		/// </para>
		/// </remarks>
		public LevelMap()
		{
		}

		/// <summary>
		/// Clear the internal maps of all levels
		/// </summary>
		/// <remarks>
		/// <para>
		/// Clear the internal maps of all levels
		/// </para>
		/// </remarks>
		public void Clear()
		{
			// Clear all current levels
			m_mapName2Level.Clear();
		}

		/// <summary>
		/// Lookup a <see cref="Level"/> by name
		/// </summary>
		/// <param name="name">The name of the Level to lookup</param>
		/// <returns>a Level from the map with the name specified</returns>
		/// <remarks>
		/// <para>
		/// Returns the <see cref="Level"/> from the
		/// map with the name specified. If the no level is
		/// found then <c>null</c> is returned.
		/// </para>
		/// </remarks>
		public Level this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}

				lock(this)
				{
					return (Level)m_mapName2Level[name];
				}
			}
		}

		/// <summary>
		/// Create a new Level and add it to the map
		/// </summary>
		/// <param name="name">the string to display for the Level</param>
		/// <param name="value">the level value to give to the Level</param>
		/// <remarks>
		/// <para>
		/// Create a new Level and add it to the map
		/// </para>
		/// </remarks>
		/// <seealso cref="M:Add(string,int,string)"/>
		public void Add(string name, int value)
		{
			Add(name, value, null);
		}

		/// <summary>
		/// Create a new Level and add it to the map
		/// </summary>
		/// <param name="name">the string to display for the Level</param>
		/// <param name="value">the level value to give to the Level</param>
		/// <param name="displayName">the display name to give to the Level</param>
		/// <remarks>
		/// <para>
		/// Create a new Level and add it to the map
		/// </para>
		/// </remarks>
		public void Add(string name, int value, string displayName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw SmartInspect.Util.SystemInfo.CreateArgumentOutOfRangeException("name", name, "Parameter: name, Value: ["+name+"] out of range. Level name must not be empty");
			}

			if (displayName == null || displayName.Length == 0)
			{
				displayName = name;
			}

			Add(new Level(value, name, displayName));
		}

		/// <summary>
		/// Add a Level to the map
		/// </summary>
		/// <param name="level">the Level to add</param>
		/// <remarks>
		/// <para>
		/// Add a Level to the map
		/// </para>
		/// </remarks>
		public void Add(Level level)
		{
			if (level == null)
			{
				throw new ArgumentNullException("level");
			}
			lock(this)
			{
				m_mapName2Level[level.Name] = level;
			}
		}

		/// <summary>
		/// Return all possible levels as a list of Level objects.
		/// </summary>
		/// <returns>all possible levels as a list of Level objects</returns>
		/// <remarks>
		/// <para>
		/// Return all possible levels as a list of Level objects.
		/// </para>
		/// </remarks>
		public LevelCollection AllLevels
		{
			get
			{
				lock(this)
				{
					return new LevelCollection(m_mapName2Level.Values);
				}
			}
		}

		/// <summary>
		/// Lookup a named level from the map
		/// </summary>
		/// <param name="defaultLevel">the name of the level to lookup is taken from this level. 
		/// If the level is not set on the map then this level is added</param>
		/// <returns>the level in the map with the name specified</returns>
		/// <remarks>
		/// <para>
		/// Lookup a named level from the map. The name of the level to lookup is taken
		/// from the <see cref="Level.Name"/> property of the <paramref name="defaultLevel"/>
		/// argument.
		/// </para>
		/// <para>
		/// If no level with the specified name is found then the 
		/// <paramref name="defaultLevel"/> argument is added to the level map
		/// and returned.
		/// </para>
		/// </remarks>
		public Level LookupWithDefault(Level defaultLevel)
		{
			if (defaultLevel == null)
			{
				throw new ArgumentNullException("defaultLevel");
			}

			lock(this)
			{
				Level level = (Level)m_mapName2Level[defaultLevel.Name];
				if (level == null)
				{
					m_mapName2Level[defaultLevel.Name] = defaultLevel;
					return defaultLevel;
				}
				return level;
			}
		}
	}
}
