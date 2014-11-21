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
	/// Implementation of Stacks collection for the <see cref="SmartInspect.ThreadContext"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Implementation of Stacks collection for the <see cref="SmartInspect.ThreadContext"/>
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class ThreadContextStacks
	{
		private readonly ContextPropertiesBase m_properties;

		#region Public Instance Constructors

		/// <summary>
		/// Internal constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="ThreadContextStacks" /> class.
		/// </para>
		/// </remarks>
		internal ThreadContextStacks(ContextPropertiesBase properties)
		{
			m_properties = properties;
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets the named thread context stack
		/// </summary>
		/// <value>
		/// The named stack
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets the named thread context stack
		/// </para>
		/// </remarks>
		public ThreadContextStack this[string key]
		{
			get 
			{
				ThreadContextStack stack = null;

				object propertyValue = m_properties[key];
				if (propertyValue == null)
				{
					// Stack does not exist, create
					stack = new ThreadContextStack();
					m_properties[key] = stack;
				}
				else
				{
					// Look for existing stack
					stack = propertyValue as ThreadContextStack;
					if (stack == null)
					{
						// Property is not set to a stack!
						string propertyValueString = SystemInfo.NullText;

						try
						{
							propertyValueString = propertyValue.ToString();
						}
						catch
						{
						}

						LogLog.Error(declaringType, "ThreadContextStacks: Request for stack named ["+key+"] failed because a property with the same name exists which is a ["+propertyValue.GetType().Name+"] with value ["+propertyValueString+"]");

						stack = new ThreadContextStack();
					}
				}

				return stack;
			}
		}

		#endregion Public Instance Properties

	    #region Private Static Fields

	    /// <summary>
	    /// The fully qualified type of the ThreadContextStacks class.
	    /// </summary>
	    /// <remarks>
	    /// Used by the internal logger to record the Type of the
	    /// log message.
	    /// </remarks>
	    private readonly static Type declaringType = typeof(ThreadContextStacks);

	    #endregion Private Static Fields
	}
}

