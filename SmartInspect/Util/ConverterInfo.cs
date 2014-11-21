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

namespace SmartInspect.Util
{
    /// <summary>
    /// Wrapper class used to map converter names to converter types
    /// </summary>
    /// <remarks>
    /// <para>
    /// Pattern converter info class used during configuration by custom
    /// PatternString and PatternLayer converters.
    /// </para>
    /// </remarks>
    public sealed class ConverterInfo
    {
        private string m_name;
        private Type m_type;
        private readonly PropertiesDictionary properties = new PropertiesDictionary();

        /// <summary>
        /// default constructor
        /// </summary>
        public ConverterInfo()
        {
        }

        /// <summary>
        /// Gets or sets the name of the conversion pattern
        /// </summary>
        /// <remarks>
        /// <para>
        /// The name of the pattern in the format string
        /// </para>
        /// </remarks>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or sets the type of the converter
        /// </summary>
        /// <remarks>
        /// <para>
        /// The value specified must extend the 
        /// <see cref="PatternConverter"/> type.
        /// </para>
        /// </remarks>
        public Type Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public void AddProperty(PropertyEntry entry)
        {
            properties[entry.Key] = entry.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public PropertiesDictionary Properties
        {
            get { return properties; }
        }
    }
}
