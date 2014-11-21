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
	/// An always empty <see cref="ICollection"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// A singleton implementation of the <see cref="ICollection"/>
	/// interface that always represents an empty collection.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
#if !NETCF
	[Serializable]
#endif
	public sealed class EmptyCollection : ICollection
	{
		#region Private Instance Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="EmptyCollection" /> class. 
		/// </summary>
		/// <remarks>
		/// <para>
		/// Uses a private access modifier to enforce the singleton pattern.
		/// </para>
		/// </remarks>
		private EmptyCollection()
		{
		}

		#endregion Private Instance Constructors
  
		#region Public Static Properties

		/// <summary>
		/// Gets the singleton instance of the empty collection.
		/// </summary>
		/// <returns>The singleton instance of the empty collection.</returns>
		/// <remarks>
		/// <para>
		/// Gets the singleton instance of the empty collection.
		/// </para>
		/// </remarks>
		public static EmptyCollection Instance
		{
			get { return s_instance; }
		}

		#endregion Public Static Properties

		#region Implementation of ICollection

		/// <summary>
		/// Copies the elements of the <see cref="ICollection"/> to an 
		/// <see cref="Array"/>, starting at a particular Array index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> 
		/// that is the destination of the elements copied from 
		/// <see cref="ICollection"/>. The Array must have zero-based 
		/// indexing.</param>
		/// <param name="index">The zero-based index in array at which 
		/// copying begins.</param>
		/// <remarks>
		/// <para>
		/// As the collection is empty no values are copied into the array.
		/// </para>
		/// </remarks>
		public void CopyTo(System.Array array, int index)
		{
			// copy nothing
		}

		/// <summary>
		/// Gets a value indicating if access to the <see cref="ICollection"/> is synchronized (thread-safe).
		/// </summary>
		/// <value>
		/// <b>true</b> if access to the <see cref="ICollection"/> is synchronized (thread-safe); otherwise, <b>false</b>.
		/// </value>
		/// <remarks>
		/// <para>
		/// For the <see cref="EmptyCollection"/> this property is always <c>true</c>.
		/// </para>
		/// </remarks>
		public bool IsSynchronized
		{
			get	{ return true; }
		}

		/// <summary>
		/// Gets the number of elements contained in the <see cref="ICollection"/>.
		/// </summary>
		/// <value>
		/// The number of elements contained in the <see cref="ICollection"/>.
		/// </value>
		/// <remarks>
		/// <para>
		/// As the collection is empty the <see cref="Count"/> is always <c>0</c>.
		/// </para>
		/// </remarks>
		public int Count
		{
			get { return 0; }
		}

		/// <summary>
		/// Gets an object that can be used to synchronize access to the <see cref="ICollection"/>.
		/// </summary>
		/// <value>
		/// An object that can be used to synchronize access to the <see cref="ICollection"/>.
		/// </value>
		/// <remarks>
		/// <para>
		/// As the collection is empty and thread safe and synchronized this instance is also
		/// the <see cref="SyncRoot"/> object.
		/// </para>
		/// </remarks>
		public object SyncRoot
		{
			get { return this; }
		}

		#endregion Implementation of ICollection

		#region Implementation of IEnumerable

		/// <summary>
		/// Returns an enumerator that can iterate through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="IEnumerator"/> that can be used to 
		/// iterate through the collection.
		/// </returns>
		/// <remarks>
		/// <para>
		/// As the collection is empty a <see cref="NullEnumerator"/> is returned.
		/// </para>
		/// </remarks>
		public IEnumerator GetEnumerator()
		{
			return NullEnumerator.Instance;
		}

		#endregion Implementation of IEnumerable

		#region Private Static Fields

		/// <summary>
		/// The singleton instance of the empty collection.
		/// </summary>
		private readonly static EmptyCollection s_instance = new EmptyCollection();
  
		#endregion Private Static Fields
	}
}
