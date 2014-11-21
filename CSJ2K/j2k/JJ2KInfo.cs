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
namespace CSJ2K.j2k
{
	
	/// <summary> This class holds general JJ2000 information, such as the version number,
	/// copyright, contact address, etc.
	/// 
	/// </summary>
	public class JJ2KInfo
	{
		
		/// <summary>The version number (such as 2.0, 2.1.1, etc.) </summary>
		public const System.String version = "5.1";
		
		/// <summary> The copyright message string. Double newlines separate paragraphs.
		/// Newlines should be respected when displaying the message.
		/// 
		/// </summary>
		public const System.String copyright = "This software module was originally developed by Rapha\u00ebl " + "Grosbois " + "and Diego Santa Cruz (Swiss Federal Institute of Technology-EPFL); " + "Joel Askel\u00f6f (Ericsson Radio Systems AB); and Bertrand " + "Berthelot, " + "David Bouchard, F\u00e9lix Henry, Gerard Mozelle and Patrice Onno " + "(Canon" + " Research Centre " + "France S.A) in the course of development of the JPEG 2000 standard " + "as " + "specified by ISO/IEC 15444 (JPEG 2000 Standard). This software " + "module is an implementation of a part of the JPEG 2000 Standard. " + "Swiss Federal Institute of Technology-EPFL, Ericsson Radio Systems " + "AB and Canon Research Centre France S.A (collectively JJ2000 " + "Partners) agree not to assert against ISO/IEC and users of the JPEG " + "2000 Standard (Users) any of their rights under the copyright, not " + "including other intellectual property rights, for this software " + "module with respect to the usage by ISO/IEC and Users of this " + "software module or modifications thereof for use in hardware or " + "software products claiming conformance to the JPEG 2000 Standard. " + "Those intending to use this software module in hardware or software " + "products are advised that their use may infringe existing patents. " + "The original developers of this software module, JJ2000 Partners " + "and " + "ISO/IEC assume no liability for use of this software module or " + "modifications thereof. No license or right to this software module " + "is granted for non JPEG 2000 Standard conforming products. JJ2000 " + "Partners have full right to use this software module for his/her " + "own purpose, assign or donate this software module to any third " + "party and to inhibit third parties from using this software module " + "for non JPEG 2000 Standard conforming products. This copyright " + "notice must be included in all copies or derivative works of this " + "software module.\n\nCopyright (c) 1999/2000 JJ2000 Partners.";
		
		/// <summary>The bug reporting e-mail address </summary>
		public const System.String bugaddr = "jj2000-bugs@ltssg3.epfl.ch";
	}
}