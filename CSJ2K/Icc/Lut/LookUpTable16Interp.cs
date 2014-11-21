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
namespace CSJ2K.Icc.Lut
{
	
	/// <summary> An interpolated 16 bit lut
	/// 
	/// </summary>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A.Kern
	/// </author>
	public class LookUpTable16Interp:LookUpTable16
	{
		
		/// <summary> Construct the lut from the curve data</summary>
		/// <oaram>   curve the data </oaram>
		/// <oaram>   dwNumInput the lut size </oaram>
		/// <oaram>   dwMaxOutput the lut max value </oaram>
		public LookUpTable16Interp(ICCCurveType curve, int dwNumInput, int dwMaxOutput):base(curve, dwNumInput, dwMaxOutput)
		{
			
			int dwLowIndex, dwHighIndex; // Indices of interpolation points
			double dfLowIndex, dfHighIndex; // FP indices of interpolation points
			double dfTargetIndex; // Target index into interpolation table
			double dfRatio; // Ratio of LUT input points to curve values
			double dfLow, dfHigh; // Interpolation values
			double dfOut; // Output LUT value
			
			dfRatio = (double) (curve.count - 1) / (double) (dwNumInput - 1);
			
			for (int i = 0; i < dwNumInput; i++)
			{
				dfTargetIndex = (double) i * dfRatio;
				dfLowIndex = System.Math.Floor(dfTargetIndex);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				dwLowIndex = (int) dfLowIndex;
				dfHighIndex = System.Math.Ceiling(dfTargetIndex);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				dwHighIndex = (int) dfHighIndex;
				
				if (dwLowIndex == dwHighIndex)
					dfOut = ICCCurveType.CurveToDouble(curve.entry(dwLowIndex));
				else
				{
					dfLow = ICCCurveType.CurveToDouble(curve.entry(dwLowIndex));
					dfHigh = ICCCurveType.CurveToDouble(curve.entry(dwHighIndex));
					dfOut = dfLow + (dfHigh - dfLow) * (dfTargetIndex - dfLowIndex);
				}
				
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				lut[i] = (short) System.Math.Floor(dfOut * dwMaxOutput + 0.5);
			}
		}
		
		/* end class LookUpTable16Interp */
	}
}