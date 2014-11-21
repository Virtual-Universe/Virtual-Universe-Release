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
	/// An entry in the <see cref="LevelMapping"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// This is an abstract base class for types that are stored in the
	/// <see cref="LevelMapping"/> object.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public abstract class LevelMappingEntry : IOptionHandler
	{
		#region Public Instance Constructors

		/// <summary>
		/// Default protected constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Default protected constructor
		/// </para>
		/// </remarks>
		protected LevelMappingEntry() 
		{
		}

		#endregion // Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// The level that is the key for this mapping 
		/// </summary>
		/// <value>
		/// The <see cref="Level"/> that is the key for this mapping 
		/// </value>
		/// <remarks>
		/// <para>
		/// Get or set the <see cref="Level"/> that is the key for this
		/// mapping subclass.
		/// </para>
		/// </remarks>
		public Level Level
		{
			get { return m_level; }
			set { m_level = value; }
		}

		#endregion // Public Instance Properties

		#region IOptionHandler Members

		/// <summary>
		/// Initialize any options defined on this entry
		/// </summary>
		/// <remarks>
		/// <para>
		/// Should be overridden by any classes that need to initialise based on their options
		/// </para>
		/// </remarks>
		virtual public void ActivateOptions()
		{
			// default implementation is to do nothing
		}

		#endregion // IOptionHandler Members

		#region Private Instance Fields

		private Level m_level; 

		#endregion // Private Instance Fields
	}
}
