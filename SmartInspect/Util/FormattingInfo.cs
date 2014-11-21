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
using SmartInspect.Util;

namespace SmartInspect.Util
{
	/// <summary>
	/// Contain the information obtained when parsing formatting modifiers 
	/// in conversion modifiers.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Holds the formatting information extracted from the format string by
	/// the <see cref="PatternParser"/>. This is used by the <see cref="PatternConverter"/>
	/// objects when rendering the output.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class FormattingInfo
	{
		#region Public Instance Constructors

		/// <summary>
		/// Defaut Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="FormattingInfo" /> class.
		/// </para>
		/// </remarks>
		public FormattingInfo() 
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="FormattingInfo" /> class
		/// with the specified parameters.
		/// </para>
		/// </remarks>
		public FormattingInfo(int min, int max, bool leftAlign) 
		{
			m_min = min;
			m_max = max;
			m_leftAlign = leftAlign;
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets or sets the minimum value.
		/// </summary>
		/// <value>
		/// The minimum value.
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets or sets the minimum value.
		/// </para>
		/// </remarks>
		public int Min
		{
			get { return m_min; }
			set { m_min = value; }
		}

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>
		/// The maximum value.
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets or sets the maximum value.
		/// </para>
		/// </remarks>
		public int Max
		{
			get { return m_max; }
			set { m_max = value; }
		}

		/// <summary>
		/// Gets or sets a flag indicating whether left align is enabled
		/// or not.
		/// </summary>
		/// <value>
		/// A flag indicating whether left align is enabled or not.
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets or sets a flag indicating whether left align is enabled or not.
		/// </para>
		/// </remarks>
		public bool LeftAlign
		{
			get { return m_leftAlign; }
			set { m_leftAlign = value; }
		}

		#endregion Public Instance Properties

		#region Private Instance Fields

		private int m_min = -1;
		private int m_max = int.MaxValue;
		private bool m_leftAlign = false;

		#endregion Private Instance Fields
	}
}
