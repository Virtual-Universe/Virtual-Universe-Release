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
using ICCCurveType = CSJ2K.Icc.Tags.ICCCurveType;
namespace CSJ2K.Icc
{
	
	/// <summary> This class is a 1 component RestrictedICCProfile
	/// 
	/// </summary>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A Kern
	/// </author>
	public class MonochromeInputRestrictedProfile:RestrictedICCProfile
	{
		/// <summary> Get the type of RestrictedICCProfile for this object</summary>
		/// <returns> kMonochromeInput
		/// </returns>
		override public int Type
		{
			get
			{
				return kMonochromeInput;
			}
			
		}
		
		/// <summary> Factory method which returns a 1 component RestrictedICCProfile</summary>
		/// <param name="c">Gray TRC curve
		/// </param>
		/// <returns> the RestrictedICCProfile
		/// </returns>
		public static new RestrictedICCProfile createInstance(ICCCurveType c)
		{
			return new MonochromeInputRestrictedProfile(c);
		}
		
		/// <summary> Construct a 1 component RestrictedICCProfile</summary>
		/// <param name="c">Gray TRC curve
		/// </param>
		private MonochromeInputRestrictedProfile(ICCCurveType c):base(c)
		{
		}
		
		/// <returns> String representation of a MonochromeInputRestrictedProfile
		/// </returns>
		public override System.String ToString()
		{
			System.Text.StringBuilder rep = new System.Text.StringBuilder("Monochrome Input Restricted ICC profile" + eol);
			
			rep.Append("trc[GRAY]:" + eol).Append(trc[GRAY]).Append(eol);
			
			return rep.ToString();
		}
		
		/* end class MonochromeInputRestrictedProfile */
	}
}