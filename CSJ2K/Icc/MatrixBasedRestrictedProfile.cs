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
using ICCXYZType = CSJ2K.Icc.Tags.ICCXYZType;
namespace CSJ2K.Icc
{
	
	/// <summary> This class is a 3 component RestrictedICCProfile
	/// 
	/// </summary>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A Kern
	/// </author>
	public class MatrixBasedRestrictedProfile:RestrictedICCProfile
	{
		/// <summary> Get the type of RestrictedICCProfile for this object</summary>
		/// <returns> kThreeCompInput
		/// </returns>
		override public int Type
		{
			get
			{
				return kThreeCompInput;
			}
			
		}
		
		/// <summary> Factory method which returns a 3 component RestrictedICCProfile</summary>
		/// <param name="rcurve">Red TRC curve
		/// </param>
		/// <param name="gcurve">Green TRC curve
		/// </param>
		/// <param name="bcurve">Blue TRC curve
		/// </param>
		/// <param name="rcolorant">Red colorant
		/// </param>
		/// <param name="gcolorant">Green colorant
		/// </param>
		/// <param name="bcolorant">Blue colorant
		/// </param>
		/// <returns> the RestrictedICCProfile
		/// </returns>
		public static new RestrictedICCProfile createInstance(ICCCurveType rcurve, ICCCurveType gcurve, ICCCurveType bcurve, ICCXYZType rcolorant, ICCXYZType gcolorant, ICCXYZType bcolorant)
		{
			return new MatrixBasedRestrictedProfile(rcurve, gcurve, bcurve, rcolorant, gcolorant, bcolorant);
		}
		
		/// <summary> Construct a 3 component RestrictedICCProfile</summary>
		/// <param name="rcurve">Red TRC curve
		/// </param>
		/// <param name="gcurve">Green TRC curve
		/// </param>
		/// <param name="bcurve">Blue TRC curve
		/// </param>
		/// <param name="rcolorant">Red colorant
		/// </param>
		/// <param name="gcolorant">Green colorant
		/// </param>
		/// <param name="bcolorant">Blue colorant
		/// </param>
		protected internal MatrixBasedRestrictedProfile(ICCCurveType rcurve, ICCCurveType gcurve, ICCCurveType bcurve, ICCXYZType rcolorant, ICCXYZType gcolorant, ICCXYZType bcolorant):base(rcurve, gcurve, bcurve, rcolorant, gcolorant, bcolorant)
		{
		}
		
		/// <returns> String representation of a MatrixBasedRestrictedProfile
		/// </returns>
		public override System.String ToString()
		{
			System.Text.StringBuilder rep = new System.Text.StringBuilder("[Matrix-Based Input Restricted ICC profile").Append(eol);
			
			rep.Append("trc[RED]:").Append(eol).Append(trc[RED]).Append(eol);
			rep.Append("trc[RED]:").Append(eol).Append(trc[GREEN]).Append(eol);
			rep.Append("trc[RED]:").Append(eol).Append(trc[BLUE]).Append(eol);
			
			rep.Append("Red colorant:  ").Append(colorant[RED]).Append(eol);
			rep.Append("Red colorant:  ").Append(colorant[GREEN]).Append(eol);
			rep.Append("Red colorant:  ").Append(colorant[BLUE]).Append(eol);
			
			return rep.Append("]").ToString();
		}
		
		/* end class MatrixBasedRestrictedProfile */
	}
}