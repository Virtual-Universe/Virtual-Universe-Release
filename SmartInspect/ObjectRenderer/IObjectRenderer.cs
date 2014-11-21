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
using System.IO;

namespace SmartInspect.ObjectRenderer
{
	/// <summary>
	/// Implement this interface in order to render objects as strings
	/// </summary>
	/// <remarks>
	/// <para>
	/// Certain types require special case conversion to
	/// string form. This conversion is done by an object renderer.
	/// Object renderers implement the <see cref="IObjectRenderer"/>
	/// interface.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public interface IObjectRenderer
	{
		/// <summary>
		/// Render the object <paramref name="obj"/> to a string
		/// </summary>
		/// <param name="rendererMap">The map used to lookup renderers</param>
		/// <param name="obj">The object to render</param>
		/// <param name="writer">The writer to render to</param>
		/// <remarks>
		/// <para>
		/// Render the object <paramref name="obj"/> to a 
		/// string.
		/// </para>
		/// <para>
		/// The <paramref name="rendererMap"/> parameter is
		/// provided to lookup and render other objects. This is
		/// very useful where <paramref name="obj"/> contains
		/// nested objects of unknown type. The <see cref="M:RendererMap.FindAndRender(object, TextWriter)"/>
		/// method can be used to render these objects.
		/// </para>
		/// </remarks>
		void RenderObject(RendererMap rendererMap, object obj, TextWriter writer);
	}
}
