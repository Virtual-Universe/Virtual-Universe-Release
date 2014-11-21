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
namespace CSJ2K.j2k.util
{
	
	/// <summary> This class contains a colleaction of utility static methods for arrays.
	/// 
	/// </summary>
	public class ArrayUtil
	{
		
		/// <summary>The maximum array size to do element by element copying, larger
		/// arrays are copyied in a n optimized way. 
		/// </summary>
		public const int MAX_EL_COPYING = 8;
		
		/// <summary>The number of elements to copy initially in an optimized array copy </summary>
		public const int INIT_EL_COPYING = 4;
		
		/// <summary> Reinitializes an int array to the given value in an optimized way. If
		/// the length of the array is less than MAX_EL_COPYING, then the array
		/// is set element by element in the normal way, otherwise the first
		/// INIT_EL_COPYING elements are set element by element and then
		/// System.arraycopy is used to set the other parts of the array.
		/// 
		/// </summary>
		/// <param name="arr">The array to set.
		/// 
		/// </param>
		/// <param name="val">The value to set the array to.
		/// 
		/// 
		/// 
		/// </param>
		public static void  intArraySet(int[] arr, int val)
		{
			int i, len, len2;
			
			len = arr.Length;
			// Set array to 'val' in an optimized way
			if (len < MAX_EL_COPYING)
			{
				// Not worth doing optimized way
				for (i = len - 1; i >= 0; i--)
				{
					// Set elements
					arr[i] = val;
				}
			}
			else
			{
				// Do in optimized way
				len2 = len >> 1;
				for (i = 0; i < INIT_EL_COPYING; i++)
				{
					// Set first elements
					arr[i] = val;
				}
				for (; i <= len2; i <<= 1)
				{
					// Copy values doubling size each time
					Array.Copy(arr, 0, arr, i, i);
				}
				if (i < len)
				{
					// Copy values to end
					Array.Copy(arr, 0, arr, i, len - i);
				}
			}
		}
		
		/// <summary> Reinitializes a byte array to the given value in an optimized way. If
		/// the length of the array is less than MAX_EL_COPYING, then the array
		/// is set element by element in the normal way, otherwise the first
		/// INIT_EL_COPYING elements are set element by element and then
		/// System.arraycopy is used to set the other parts of the array.
		/// 
		/// </summary>
		/// <param name="arr">The array to set.
		/// 
		/// </param>
		/// <param name="val">The value to set the array to.
		/// 
		/// 
		/// 
		/// </param>
		public static void  byteArraySet(byte[] arr, byte val)
		{
			int i, len, len2;
			
			len = arr.Length;
			// Set array to 'val' in an optimized way
			if (len < MAX_EL_COPYING)
			{
				// Not worth doing optimized way
				for (i = len - 1; i >= 0; i--)
				{
					// Set elements
					arr[i] = val;
				}
			}
			else
			{
				// Do in optimized way
				len2 = len >> 1;
				for (i = 0; i < INIT_EL_COPYING; i++)
				{
					// Set first elements
					arr[i] = val;
				}
				for (; i <= len2; i <<= 1)
				{
					// Copy values doubling size each time
					Array.Copy(arr, 0, arr, i, i);
				}
				if (i < len)
				{
					// Copy values to end
					Array.Copy(arr, 0, arr, i, len - i);
				}
			}
		}
	}
}