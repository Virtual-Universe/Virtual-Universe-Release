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
using ICCProfile = CSJ2K.Icc.ICCProfile;
namespace CSJ2K.Icc.Types
{
	
	/// <summary> This class describes the ICCProfile Version as contained in
	/// the header of the ICC Profile.
	/// 
	/// </summary>
	/// <seealso cref="jj2000.j2k.icc.ICCProfile">
	/// </seealso>
	/// <seealso cref="jj2000.j2k.icc.types.ICCProfileHeader">
	/// </seealso>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A. Kern
	/// </author>
	public class ICCProfileVersion
	{
		/// <summary>Field size </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'size' was moved to static method 'icc.types.ICCProfileVersion'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly int size;
		
		/// <summary>Major revision number in binary coded decimal </summary>
		public byte uMajor;
		/// <summary>Minor revision in high nibble, bug fix revision           
		/// in low nibble, both in binary coded decimal   
		/// </summary>
		public byte uMinor;
		
		private byte reserved1;
		private byte reserved2;
		
		/// <summary>Construct from constituent parts. </summary>
		public ICCProfileVersion(byte major, byte minor, byte res1, byte res2)
		{
			uMajor = major; uMinor = minor; reserved1 = res1; reserved2 = res2;
		}
		
		/// <summary>Construct from file content. </summary>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public virtual void  write(System.IO.FileStream raf)
		{
			raf.WriteByte(uMajor); raf.WriteByte(uMinor); raf.WriteByte(reserved1); raf.WriteByte(reserved2);
		}
		
		/// <summary>String representation of class instance. </summary>
		public override System.String ToString()
		{
			return "Version " + uMajor + "." + uMinor;
		}
		
		/* end class ICCProfileVersion */
		static ICCProfileVersion()
		{
			size = 4 * ICCProfile.byte_size;
		}
	}
}