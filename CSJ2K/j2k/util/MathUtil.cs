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
	
	/// <summary> This class contains a collection of utility methods fro mathematical
	/// operations. All methods are static.
	/// 
	/// </summary>
	public class MathUtil
	{
		
		/// <summary> Method that calculates the floor of the log, base 2, of 'x'. The
		/// calculation is performed in integer arithmetic, therefore, it is exact.
		/// 
		/// </summary>
		/// <param name="x">The value to calculate log2 on.
		/// 
		/// </param>
		/// <returns> floor(log(x)/log(2)), calculated in an exact way.
		/// 
		/// </returns>
		public static int log2(int x)
		{
			int y, v;
			// No log of 0 or negative
			if (x <= 0)
			{
				throw new System.ArgumentException("" + x + " <= 0");
			}
			// Calculate log2 (it's actually floor log2)
			v = x;
			y = - 1;
			while (v > 0)
			{
				v >>= 1;
				y++;
			}
			return y;
		}
		
		/// <summary> Method that calculates the Least Common Multiple (LCM) of two strictly
		/// positive integer numbers.
		/// 
		/// </summary>
		/// <param name="x1">First number
		/// 
		/// </param>
		/// <param name="x2">Second number
		/// 
		/// </param>
		public static int lcm(int x1, int x2)
		{
			if (x1 <= 0 || x2 <= 0)
			{
				throw new System.ArgumentException("Cannot compute the least " + "common multiple of two " + "numbers if one, at least," + "is negative.");
			}
			int max, min;
			if (x1 > x2)
			{
				max = x1;
				min = x2;
			}
			else
			{
				max = x2;
				min = x1;
			}
			for (int i = 1; i <= min; i++)
			{
				if ((max * i) % min == 0)
				{
					return i * max;
				}
			}
			throw new System.ApplicationException("Cannot find the least common multiple of numbers " + x1 + " and " + x2);
		}
		
		/// <summary> Method that calculates the Least Common Multiple (LCM) of several
		/// positive integer numbers.
		/// 
		/// </summary>
		/// <param name="x">Array containing the numbers.
		/// 
		/// </param>
		public static int lcm(int[] x)
		{
			if (x.Length < 2)
			{
				throw new System.ApplicationException("Do not use this method if there are less than" + " two numbers.");
			}
			int tmp = lcm(x[x.Length - 1], x[x.Length - 2]);
			for (int i = x.Length - 3; i >= 0; i--)
			{
				if (x[i] <= 0)
				{
					throw new System.ArgumentException("Cannot compute the least " + "common multiple of " + "several numbers where " + "one, at least," + "is negative.");
				}
				tmp = lcm(tmp, x[i]);
			}
			return tmp;
		}
		
		/// <summary> Method that calculates the Greatest Common Divisor (GCD) of two
		/// positive integer numbers.
		/// 
		/// </summary>
		public static int gcd(int x1, int x2)
		{
			if (x1 < 0 || x2 < 0)
			{
				throw new System.ArgumentException("Cannot compute the GCD " + "if one integer is negative.");
			}
			int a, b, g, z;
			
			if (x1 > x2)
			{
				a = x1;
				b = x2;
			}
			else
			{
				a = x2;
				b = x1;
			}
			
			if (b == 0)
				return 0;
			
			g = b;
			
			while (g != 0)
			{
				z = a % g;
				a = g;
				g = z;
			}
			return a;
		}
		
		/// <summary> Method that calculates the Greatest Common Divisor (GCD) of several
		/// positive integer numbers.
		/// 
		/// </summary>
		/// <param name="x">Array containing the numbers.
		/// 
		/// </param>
		public static int gcd(int[] x)
		{
			if (x.Length < 2)
			{
				throw new System.ApplicationException("Do not use this method if there are less than" + " two numbers.");
			}
			int tmp = gcd(x[x.Length - 1], x[x.Length - 2]);
			for (int i = x.Length - 3; i >= 0; i--)
			{
				if (x[i] < 0)
				{
					throw new System.ArgumentException("Cannot compute the least " + "common multiple of " + "several numbers where " + "one, at least," + "is negative.");
				}
				tmp = gcd(tmp, x[i]);
			}
			return tmp;
		}
	}
}