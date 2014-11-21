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
using ICCTagTable = CSJ2K.Icc.Tags.ICCTagTable;
using ICCTag = CSJ2K.Icc.Tags.ICCTag;
namespace CSJ2K.Icc
{
	
	/// <summary> This profile is constructed by parsing an ICCProfile and
	/// is the profile actually applied to the image.
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.icc.ICCProfile">
	/// </seealso>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A. Kern
	/// </author>
	public abstract class RestrictedICCProfile
	{
		/// <summary>Returns the appropriate input type enum. </summary>
		public abstract int Type{get;}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'eol '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly System.String eol = System.Environment.NewLine;
		
		/// <summary> Factory method for creating a RestrictedICCProfile from 
		/// 3 component curve and colorant data.
		/// </summary>
		/// <param name="rcurve">red curve
		/// </param>
		/// <param name="gcurve">green curve
		/// </param>
		/// <param name="bcurve">blue curve
		/// </param>
		/// <param name="rcolorant">red colorant
		/// </param>
		/// <param name="gcolorant">green colorant
		/// </param>
		/// <param name="bcolorant">blue colorant
		/// </param>
		/// <returns> MatrixBasedRestrictedProfile
		/// </returns>
		public static RestrictedICCProfile createInstance(ICCCurveType rcurve, ICCCurveType gcurve, ICCCurveType bcurve, ICCXYZType rcolorant, ICCXYZType gcolorant, ICCXYZType bcolorant)
		{
			
			return MatrixBasedRestrictedProfile.createInstance(rcurve, gcurve, bcurve, rcolorant, gcolorant, bcolorant);
		}
		
		/// <summary> Factory method for creating a RestrictedICCProfile from 
		/// gray curve data.
		/// </summary>
		/// <param name="gcurve">gray curve
		/// </param>
		/// <returns> MonochromeInputRestrictedProfile
		/// </returns>
		public static RestrictedICCProfile createInstance(ICCCurveType gcurve)
		{
			return MonochromeInputRestrictedProfile.createInstance(gcurve);
		}
		
		/// <summary>Component index       </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'GRAY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'GRAY' was moved to static method 'icc.RestrictedICCProfile'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly int GRAY;
		/// <summary>Component index       </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'RED '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'RED' was moved to static method 'icc.RestrictedICCProfile'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly int RED;
		/// <summary>Component index       </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'GREEN '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'GREEN' was moved to static method 'icc.RestrictedICCProfile'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly int GREEN;
		/// <summary>Component index       </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'BLUE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'BLUE' was moved to static method 'icc.RestrictedICCProfile'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly int BLUE;
		/// <summary>input type enumerator </summary>
		public const int kMonochromeInput = 0;
		/// <summary>input type enumerator </summary>
		public const int kThreeCompInput = 1;
		
		/// <summary>Curve data    </summary>
		public ICCCurveType[] trc;
		/// <summary>Colorant data </summary>
		public ICCXYZType[] colorant;
		
		/// <summary> Construct the common state of all gray RestrictedICCProfiles</summary>
		/// <param name="gcurve">curve data
		/// </param>
		protected internal RestrictedICCProfile(ICCCurveType gcurve)
		{
			trc = new ICCCurveType[1];
			colorant = null;
			trc[GRAY] = gcurve;
		}
		
		/// <summary> Construct the common state of all 3 component RestrictedICCProfiles
		/// 
		/// </summary>
		/// <param name="rcurve">red curve
		/// </param>
		/// <param name="gcurve">green curve
		/// </param>
		/// <param name="bcurve">blue curve
		/// </param>
		/// <param name="rcolorant">red colorant
		/// </param>
		/// <param name="gcolorant">green colorant
		/// </param>
		/// <param name="bcolorant">blue colorant
		/// </param>
		protected internal RestrictedICCProfile(ICCCurveType rcurve, ICCCurveType gcurve, ICCCurveType bcurve, ICCXYZType rcolorant, ICCXYZType gcolorant, ICCXYZType bcolorant)
		{
			trc = new ICCCurveType[3];
			colorant = new ICCXYZType[3];
			
			trc[RED] = rcurve;
			trc[GREEN] = gcurve;
			trc[BLUE] = bcurve;
			
			colorant[RED] = rcolorant;
			colorant[GREEN] = gcolorant;
			colorant[BLUE] = bcolorant;
		}
		
		/* end class RestrictedICCProfile */
		static RestrictedICCProfile()
		{
			GRAY = ICCProfile.GRAY;
			RED = ICCProfile.RED;
			GREEN = ICCProfile.GREEN;
			BLUE = ICCProfile.BLUE;
		}
	}
}