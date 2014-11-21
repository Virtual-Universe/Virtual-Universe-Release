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
	
	/// <summary> Toplevel class for a int [] lut.
	/// 
	/// </summary>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A. Kern
	/// </author>
	//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
	abstract public class LookUpTable32:LookUpTable
	{
		
		/// <summary>Maximum output value of the LUT </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'dwMaxOutput '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int dwMaxOutput;
		/// <summary>the lut values.                 </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'lut '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public int[] lut;
		
		/// <summary> Create an abbreviated string representation of a 16 bit lut.</summary>
		/// <returns> the lut as a String
		/// </returns>
		public override System.String ToString()
		{
			System.Text.StringBuilder rep = new System.Text.StringBuilder("[LookUpTable32 ");
			//int row, col;
			rep.Append("max= " + dwMaxOutput);
			rep.Append(", nentries= " + dwNumInput);
			return rep.Append("]").ToString();
		}
		
		/// <summary> Create the string representation of a 32 bit lut.</summary>
		/// <returns> the lut as a String
		/// </returns>
		public virtual System.String toStringWholeLut()
		{
			System.Text.StringBuilder rep = new System.Text.StringBuilder("[LookUpTable32" + eol);
			int row, col;
			rep.Append("max output = " + dwMaxOutput + eol);
			for (row = 0; row < dwNumInput / 10; ++row)
			{
				rep.Append("lut[" + 10 * row + "] : ");
				for (col = 0; col < 10; ++col)
				{
					rep.Append(lut[10 * row + col] + " ");
				}
				rep.Append(eol);
			}
			// Partial row.
			rep.Append("lut[" + 10 * row + "] : ");
			for (col = 0; col < dwNumInput % 10; ++col)
				rep.Append(lut[10 * row + col] + " ");
			rep.Append(eol + eol);
			return rep.ToString();
		}
		
		/// <summary> Factory method for getting a 32 bit lut from a given curve.</summary>
		/// <param name="curve"> the data
		/// </param>
		/// <param name="dwNumInput">the size of the lut 
		/// </param>
		/// <param name="dwMaxOutput">max output value of the lut
		/// </param>
		/// <returns> the lookup table
		/// </returns>
		public static LookUpTable32 createInstance(ICCCurveType curve, int dwNumInput, int dwMaxOutput)
		{
			if (curve.count == 1)
				return new LookUpTable32Gamma(curve, dwNumInput, dwMaxOutput);
			else
				return new LookUpTable32Interp(curve, dwNumInput, dwMaxOutput);
		}
		
		/// <summary> Construct an empty 32 bit</summary>
		/// <param name="dwNumInput">the size of the lut t lut.
		/// </param>
		/// <param name="dwMaxOutput">max output value of the lut
		/// </param>
		protected internal LookUpTable32(int dwNumInput, int dwMaxOutput):base(null, dwNumInput)
		{
			lut = new int[dwNumInput];
			this.dwMaxOutput = dwMaxOutput;
		}
		
		/// <summary> Construct a 16 bit lut from a given curve.</summary>
		/// <param name="curve">the data
		/// </param>
		/// <param name="dwNumInput">the size of the lut t lut.
		/// </param>
		/// <param name="dwMaxOutput">max output value of the lut
		/// </param>
		protected internal LookUpTable32(ICCCurveType curve, int dwNumInput, int dwMaxOutput):base(curve, dwNumInput)
		{
			this.dwMaxOutput = dwMaxOutput;
			lut = new int[dwNumInput];
		}
		
		/// <summary> lut accessor</summary>
		/// <param name="index">of the element
		/// </param>
		/// <returns> the lut [index]
		/// </returns>
		public int elementAt(int index)
		{
			return lut[index];
		}
		
		/* end class LookUpTable32 */
	}
}