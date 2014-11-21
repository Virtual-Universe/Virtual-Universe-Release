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
	
	/// <summary> Date Time format for tags
	/// 
	/// </summary>
	/// <version> 	1.0
	/// </version>
	/// <author> 	Bruce A. Kern
	/// </author>
	public class ICCDateTime
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'size' was moved to static method 'icc.types.ICCDateTime'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly int size;
		
		/// <summary>Year datum.   </summary>
		public short wYear;
		/// <summary>Month datum.  </summary>
		// Number of the actual year (i.e. 1994)
		public short wMonth;
		/// <summary>Day datum.    </summary>
		// Number of the month (1-12)
		public short wDay;
		/// <summary>Hour datum.   </summary>
		// Number of the day
		public short wHours;
		/// <summary>Minute datum. </summary>
		// Number of hours (0-23)
		public short wMinutes;
		/// <summary>Second datum. </summary>
		// Number of minutes (0-59)
		public short wSeconds; // Number of seconds (0-59)
		
		/// <summary>Construct an ICCDateTime from parts </summary>
		public ICCDateTime(short year, short month, short day, short hour, short minute, short second)
		{
			wYear = year; wMonth = month; wDay = day;
			wHours = hour; wMinutes = minute; wSeconds = second;
		}
		
		/// <summary>Write an ICCDateTime to a file. </summary>
		//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
		public virtual void  write(System.IO.FileStream raf)
		{
			System.IO.BinaryWriter temp_BinaryWriter;
			temp_BinaryWriter = new System.IO.BinaryWriter(raf);
			temp_BinaryWriter.Write((System.Int16) wYear);
			System.IO.BinaryWriter temp_BinaryWriter2;
			temp_BinaryWriter2 = new System.IO.BinaryWriter(raf);
			temp_BinaryWriter2.Write((System.Int16) wMonth);
			System.IO.BinaryWriter temp_BinaryWriter3;
			temp_BinaryWriter3 = new System.IO.BinaryWriter(raf);
			temp_BinaryWriter3.Write((System.Int16) wDay);
			System.IO.BinaryWriter temp_BinaryWriter4;
			temp_BinaryWriter4 = new System.IO.BinaryWriter(raf);
			temp_BinaryWriter4.Write((System.Int16) wHours);
			System.IO.BinaryWriter temp_BinaryWriter5;
			temp_BinaryWriter5 = new System.IO.BinaryWriter(raf);
			temp_BinaryWriter5.Write((System.Int16) wMinutes);
			System.IO.BinaryWriter temp_BinaryWriter6;
			temp_BinaryWriter6 = new System.IO.BinaryWriter(raf);
			temp_BinaryWriter6.Write((System.Int16) wSeconds);
		}
		
		/// <summary>Return a ICCDateTime representation. </summary>
		public override System.String ToString()
		{
			//System.String rep = "";
			return System.Convert.ToString(wYear) + "/" + System.Convert.ToString(wMonth) + "/" + System.Convert.ToString(wDay) + " " + System.Convert.ToString(wHours) + ":" + System.Convert.ToString(wMinutes) + ":" + System.Convert.ToString(wSeconds);
		}
		
		/* end class ICCDateTime*/
		static ICCDateTime()
		{
			size = 6 * ICCProfile.short_size;
		}
	}
}