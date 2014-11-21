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
using SmartInspect.Core;

namespace SmartInspect.Repository.Hierarchy
{
	/// <summary>
	/// The <see cref="RootLogger" /> sits at the root of the logger hierarchy tree. 
	/// </summary>
	/// <remarks>
	/// <para>
	/// The <see cref="RootLogger" /> is a regular <see cref="Logger" /> except 
	/// that it provides several guarantees.
	/// </para>
	/// <para>
	/// First, it cannot be assigned a <c>null</c>
	/// level. Second, since the root logger cannot have a parent, the
	/// <see cref="EffectiveLevel"/> property always returns the value of the
	/// level field without walking the hierarchy.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public class RootLogger : Logger
	{
		#region Public Instance Constructors

		/// <summary>
		/// Construct a <see cref="RootLogger"/>
		/// </summary>
		/// <param name="level">The level to assign to the root logger.</param>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="RootLogger" /> class with
		/// the specified logging level.
		/// </para>
		/// <para>
		/// The root logger names itself as "root". However, the root
		/// logger cannot be retrieved by name.
		/// </para>
		/// </remarks>
		public RootLogger(Level level) : base("root")
		{
			this.Level = level;
		}

		#endregion Public Instance Constructors

		#region Override implementation of Logger

		/// <summary>
		/// Gets the assigned level value without walking the logger hierarchy.
		/// </summary>
		/// <value>The assigned level value without walking the logger hierarchy.</value>
		/// <remarks>
		/// <para>
		/// Because the root logger cannot have a parent and its level
		/// must not be <c>null</c> this property just returns the
		/// value of <see cref="Logger.Level"/>.
		/// </para>
		/// </remarks>
		override public Level EffectiveLevel 
		{
			get 
			{
				return base.Level;
			}
		}

		/// <summary>
		/// Gets or sets the assigned <see cref="Level"/> for the root logger.  
		/// </summary>
		/// <value>
		/// The <see cref="Level"/> of the root logger.
		/// </value>
		/// <remarks>
		/// <para>
		/// Setting the level of the root logger to a <c>null</c> reference
		/// may have catastrophic results. We prevent this here.
		/// </para>
		/// </remarks>
		override public Level Level
		{
			get { return base.Level; }
			set
			{
				if (value == null) 
				{
					LogLog.Error(declaringType, "You have tried to set a null level to root.", new LogException());
				}
				else 
				{
					base.Level = value;
				}
			}
		}

		#endregion Override implementation of Logger

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the RootLogger class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(RootLogger);

	    #endregion Private Static Fields
	}
}
