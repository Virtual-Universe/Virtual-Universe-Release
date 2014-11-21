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

// .NET Compact Framework 1.0 has no support for reading assembly attributes
#if !NETCF

using System;
using System.Reflection;

using SmartInspect.Repository;

namespace SmartInspect.Config
{
	/// <summary>
	/// Base class for all SmartInspect configuration attributes.
	/// </summary>
	/// <remarks>
	/// This is an abstract class that must be extended by 
	/// specific configurators. This attribute allows the
	/// configurator to be parameterized by an assembly level
	/// attribute.
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	[AttributeUsage(AttributeTargets.Assembly)]
	public abstract class ConfiguratorAttribute : Attribute, IComparable
	{
		private int m_priority = 0;

		/// <summary>
		/// Constructor used by subclasses.
		/// </summary>
		/// <param name="priority">the ordering priority for this configurator</param>
		/// <remarks>
		/// <para>
		/// The <paramref name="priority"/> is used to order the configurator
		/// attributes before they are invoked. Higher priority configurators are executed
		/// before lower priority ones.
		/// </para>
		/// </remarks>
		protected ConfiguratorAttribute(int priority)
		{
			m_priority = priority;
		}

		/// <summary>
		/// Configures the <see cref="ILoggerRepository"/> for the specified assembly.
		/// </summary>
		/// <param name="sourceAssembly">The assembly that this attribute was defined on.</param>
		/// <param name="targetRepository">The repository to configure.</param>
		/// <remarks>
		/// <para>
		/// Abstract method implemented by a subclass. When this method is called
		/// the subclass should configure the <paramref name="targetRepository"/>.
		/// </para>
		/// </remarks>
		public abstract void Configure(Assembly sourceAssembly, ILoggerRepository targetRepository);

		/// <summary>
		/// Compare this instance to another ConfiguratorAttribute
		/// </summary>
		/// <param name="obj">the object to compare to</param>
		/// <returns>see <see cref="IComparable.CompareTo"/></returns>
		/// <remarks>
		/// <para>
		/// Compares the priorities of the two <see cref="ConfiguratorAttribute"/> instances.
		/// Sorts by priority in descending order. Objects with the same priority are
		/// randomly ordered.
		/// </para>
		/// </remarks>
		public int CompareTo(object obj)
		{
			// Reference equals
			if ((object)this == obj)
			{
				return 0;
			}

			int result = -1;

			ConfiguratorAttribute target = obj as ConfiguratorAttribute;
			if (target != null)
			{
				// Compare the priorities
				result = target.m_priority.CompareTo(m_priority);
				if (result == 0)
				{
					// Same priority, so have to provide some ordering
					result = -1;
				}
			}
			return result;
		}
	}
}

#endif //!NETCF