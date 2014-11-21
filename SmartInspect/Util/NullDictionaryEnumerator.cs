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
	/// An always empty <see cref="IDictionaryEnumerator"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// A singleton implementation of the <see cref="IDictionaryEnumerator"/> over a collection
	/// that is empty and not modifiable.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public sealed class NullDictionaryEnumerator : IDictionaryEnumerator
	{
		#region Private Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NullDictionaryEnumerator" /> class. 
		/// </summary>
		/// <remarks>
		/// <para>
		/// Uses a private access modifier to enforce the singleton pattern.
		/// </para>
		/// </remarks>
		private NullDictionaryEnumerator()
		{
		}

		#endregion Private Instance Constructors

		#region Public Static Properties
  
		/// <summary>
		/// Gets the singleton instance of the <see cref="NullDictionaryEnumerator" />.
		/// </summary>
		/// <returns>The singleton instance of the <see cref="NullDictionaryEnumerator" />.</returns>
		/// <remarks>
		/// <para>
		/// Gets the singleton instance of the <see cref="NullDictionaryEnumerator" />.
		/// </para>
		/// </remarks>
		public static NullDictionaryEnumerator Instance
		{
			get { return s_instance; }
		}

		#endregion Public Static Properties

		#region Implementation of IEnumerator

		/// <summary>
		/// Gets the current object from the enumerator.
		/// </summary>
		/// <remarks>
		/// Throws an <see cref="InvalidOperationException" /> because the 
		/// <see cref="NullDictionaryEnumerator" /> never has a current value.
		/// </remarks>
		/// <remarks>
		/// <para>
		/// As the enumerator is over an empty collection its <see cref="Current"/>
		/// value cannot be moved over a valid position, therefore <see cref="Current"/>
		/// will throw an <see cref="InvalidOperationException"/>.
		/// </para>
		/// </remarks>
		/// <exception cref="InvalidOperationException">The collection is empty and <see cref="Current"/> 
		/// cannot be positioned over a valid location.</exception>
		public object Current 
		{
			get	{ throw new InvalidOperationException(); }
		}
  
		/// <summary>
		/// Test if the enumerator can advance, if so advance.
		/// </summary>
		/// <returns><c>false</c> as the <see cref="NullDictionaryEnumerator" /> cannot advance.</returns>
		/// <remarks>
		/// <para>
		/// As the enumerator is over an empty collection its <see cref="Current"/>
		/// value cannot be moved over a valid position, therefore <see cref="MoveNext"/>
		/// will always return <c>false</c>.
		/// </para>
		/// </remarks>
		public bool MoveNext()
		{
			return false;
		}
  
		/// <summary>
		/// Resets the enumerator back to the start.
		/// </summary>
		/// <remarks>
		/// <para>
		/// As the enumerator is over an empty collection <see cref="Reset"/> does nothing.
		/// </para>
		/// </remarks>
		public void Reset() 
		{
		}

		#endregion Implementation of IEnumerator

		#region Implementation of IDictionaryEnumerator

		/// <summary>
		/// Gets the current key from the enumerator.
		/// </summary>
		/// <remarks>
		/// Throws an exception because the <see cref="NullDictionaryEnumerator" />
		/// never has a current value.
		/// </remarks>
		/// <remarks>
		/// <para>
		/// As the enumerator is over an empty collection its <see cref="Current"/>
		/// value cannot be moved over a valid position, therefore <see cref="Key"/>
		/// will throw an <see cref="InvalidOperationException"/>.
		/// </para>
		/// </remarks>
		/// <exception cref="InvalidOperationException">The collection is empty and <see cref="Current"/> 
		/// cannot be positioned over a valid location.</exception>
		public object Key 
		{
			get	{ throw new InvalidOperationException(); }
		}

		/// <summary>
		/// Gets the current value from the enumerator.
		/// </summary>
		/// <value>The current value from the enumerator.</value>
		/// <remarks>
		/// Throws an <see cref="InvalidOperationException" /> because the 
		/// <see cref="NullDictionaryEnumerator" /> never has a current value.
		/// </remarks>
		/// <remarks>
		/// <para>
		/// As the enumerator is over an empty collection its <see cref="Current"/>
		/// value cannot be moved over a valid position, therefore <see cref="Value"/>
		/// will throw an <see cref="InvalidOperationException"/>.
		/// </para>
		/// </remarks>
		/// <exception cref="InvalidOperationException">The collection is empty and <see cref="Current"/> 
		/// cannot be positioned over a valid location.</exception>
		public object Value 
		{
			get	{ throw new InvalidOperationException(); }
		}

		/// <summary>
		/// Gets the current entry from the enumerator.
		/// </summary>
		/// <remarks>
		/// Throws an <see cref="InvalidOperationException" /> because the 
		/// <see cref="NullDictionaryEnumerator" /> never has a current entry.
		/// </remarks>
		/// <remarks>
		/// <para>
		/// As the enumerator is over an empty collection its <see cref="Current"/>
		/// value cannot be moved over a valid position, therefore <see cref="Entry"/>
		/// will throw an <see cref="InvalidOperationException"/>.
		/// </para>
		/// </remarks>
		/// <exception cref="InvalidOperationException">The collection is empty and <see cref="Current"/> 
		/// cannot be positioned over a valid location.</exception>
		public DictionaryEntry Entry 
		{
			get	{ throw new InvalidOperationException(); }
		}
  
		#endregion Implementation of IDictionaryEnumerator

		#region Private Static Fields

		/// <summary>
		/// The singleton instance of the <see cref="NullDictionaryEnumerator" />.
		/// </summary>
		private readonly static NullDictionaryEnumerator s_instance = new NullDictionaryEnumerator();
  
		#endregion Private Static Fields
	}
}
