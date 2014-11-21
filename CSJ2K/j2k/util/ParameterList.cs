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
	
	/// <summary> This class holds modules options and parameters as they are provided to the
	/// encoder or the decoder. Each option and its associated parameters are
	/// stored as strings.
	/// 
	/// <p>This class is built on the standard Java Properties class. Consequently,
	/// it offers facilities to load and write parameters from/to a file. In the
	/// meantime, a ParameterList object can also handle default parameters for
	/// each option.</p>
	/// 
	/// <p>Each parameter can be retrieved as a string or as an specific primitive
	/// type (int, float, etc).</p>
	/// 
	/// <p>For more details see the Properties class.</p>
	/// 
	/// <p>Note that this class does not support multiple occurrences of parameters
	/// (for a parameter name, only one value is possible). Also there is no
	/// particular order of the parameters.</p>
	/// 
	/// </summary>
	/// <seealso cref="Properties">
	/// 
	/// </seealso>
	//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
	[Serializable]
	public class ParameterList:System.Collections.Specialized.NameValueCollection
	{
        // COVNERSION PROBLEM?
        private ParameterList defaults;

		/// <summary> Returns the default ParameterList.
		/// 
		/// </summary>
		/// <returns> Default ParameterList
		/// 
		/// </returns>
		virtual public ParameterList DefaultParameterList
		{
			get
			{
				//UPGRADE_ISSUE: Field 'java.util.Properties.defaults' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilPropertiesdefaults_f'"
				return (ParameterList) defaults;
			}
			
		}
		
		/// <summary> Constructs an empty ParameterList object. It can be later completed by
		/// adding elements one by one, by loading them from a file, or by
		/// initializing them from an argument string.
		/// 
		/// </summary>
		//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public ParameterList():base()
		{
		}
		
		/// <summary> Constructs an empty ParameterList object with the provided default
		/// parameters. The list can be later updated by adding elements one by
		/// one, by loading them from a file, or by initializing them from an
		/// argument string.
		/// 
		/// </summary>
		/// <param name="def">The defaults parameters
		/// 
		/// </param>
		//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public ParameterList(ParameterList def):base()
		{
            defaults=def;
		}
		
		/// <summary> Parses the parameters from an argument list, such as as the one in the
		/// command line, and integrates them in this parameter list.
		/// 
		/// <p>All options must be preceded by '-' and then followed by one or more
		/// words, which constitues the values. The name of the options constitute
		/// the name of the parameters. The only exception is for boolean options,
		/// in which case if they are preceded by '-' they will be turned on, and
		/// if preceded by '+' they will be turned off. The string value of a
		/// boolean option is "on" or "off". Note that the '-' and '+' characters
		/// can not precede any word which would be a value for an option unless
		/// they are numeric values (otherwise it would be considered as a boolean
		/// option). Note also that the name of an option can not start with a
		/// number.</p>
		/// 
		/// <p>No option can appear more than once. If so happens an exception is
		/// thrown.</p>
		/// 
		/// <p>For instance the string:
		/// 
		/// <quote> "-Ffilters w5x3 -Wlev 5 -Qtype reversible </quote>
		/// 
		/// <p>will create the following parameter list:
		/// 
		/// <pre>
		/// Ffilers  w5x3
		/// Wlev     5
		/// Qtype    reversible
		/// </pre></p>
		/// 
		/// </summary>
		/// <param name="argv">The argument list.
		/// 
		/// </param>
		/// <exception cref="StringFormatException">if there are invalid arguments in
		/// 'argv'
		/// 
		/// </exception>
		public virtual void  parseArgs(System.String[] argv)
		{
			int k;
			char c, c2;
			System.String pname;
			System.Text.StringBuilder pvalue;
			
			// Read options
			k = - 1;
			// Skip empty arguments
			do 
			{
				k++;
				if (k >= argv.Length)
				{
					// Nothing to put in parameters
					return ;
				}
			}
			while (argv[k].Length <= 0);
			
			// Check that we start with an option and that its is not a number
			c = argv[k][0];
			if (c != '-' && c != '+')
			{
				// It's not an option
				throw new StringFormatException("Argument list does not" + " start with an option: " + argv[k]);
			}
			if (argv[k].Length >= 2 && System.Char.IsDigit(argv[k][1]))
			{
				throw new StringFormatException("Numeric option name: " + argv[k]);
			}
			pvalue = new System.Text.StringBuilder();
			while (k < argv.Length)
			{
				// Read parameter name
				if (argv[k].Length <= 1)
				{
					throw new StringFormatException("Option \"" + argv[k] + "\" is too short.");
				}
				c = argv[k][0];
				pname = argv[k++];
				pvalue.Length = 0;
				// Are there any more arguments?
				if (k >= argv.Length)
				{
					// No more words in argument list => must be boolean
					pvalue.Append((c == '-')?"on":"off");
				}
				else
				{
					c2 = argv[k][0];
					// Is next word an option or a value?
					if (c2 == '-' || c2 == '+')
					{
						// Next word could be an option
						if (argv[k].Length <= 1)
						{
							throw new StringFormatException("Option or argument \"" + argv[k] + "\" too short");
						}
						if (!System.Char.IsDigit(argv[k][1]))
						{
							// Not a number => we have a boolean option in pname
							pvalue.Append((c == '-')?"on":"off");
						}
					}
					if (pvalue.Length == 0)
					{
						// No value yet
						// It should not a boolean option, read the values
						if (c == '+')
						{
							throw new StringFormatException("Boolean option \"" + pname + "\" has a value");
						}
						// We have at least one value
						pvalue.Append(argv[k++]);
						while (k < argv.Length)
						{
							// If empty string skip it
							if (argv[k].Length == 0)
							{
								k++;
								continue;
							}
							c = argv[k][0];
							if (c == '-' || c == '+')
							{
								// Next word could be an option
								if (argv[k].Length <= 1)
								{
									throw new StringFormatException("Option or " + "argument \"" + argv[k] + "\" too short");
								}
								if (!System.Char.IsDigit(argv[k][1]))
								{
									// It's an option => stop
									break;
								}
							}
							pvalue.Append(' '); // Add a space
							pvalue.Append(argv[k++]);
						}
					}
				}
				// Now put parameter and value in the list
                /*
				if (this[(System.String) pname.Substring(1)] != null)
				{
					// Option is repeated => ERROR
					throw new StringFormatException("Option \"" + pname + "\" appears more than once");
				}
                 */
				System.Object tempObject;
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				tempObject = this[(System.String) pname.Substring(1)];
				this[(System.String) pname.Substring(1)] = (System.String) pvalue.ToString();
				System.Object generatedAux4 = tempObject;
			}
		}
		
		/// <summary> Returns the value of the named parameter, as a string. The value can
		/// come from teh defaults, if there are.
		/// 
		/// </summary>
		/// <param name="pname">The parameter name.
		/// 
		/// </param>
		/// <returns> the value of the parameter as a string, or null if there is no
		/// parameter with the name 'pname'.
		/// 
		/// </returns>
		public virtual System.String getParameter(System.String pname)
		{
			System.String pval;
			
			pval = ((System.String) this[(System.String) pname]);
			//UPGRADE_ISSUE: Field 'java.util.Properties.defaults' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilPropertiesdefaults_f'"
			if (pval == null && defaults != null)
			{
				// if parameter is not there
				// Look in defaults
				//UPGRADE_ISSUE: Field 'java.util.Properties.defaults' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilPropertiesdefaults_f'"
				pval = defaults.Get(pname);
			}
			return pval;
		}
		
		/// <summary> Returns the value of the named parameter as a boolean. The value "on"
		/// is interpreted as 'true', while the value "off" is interpreted as
		/// 'false'. If the parameter has another value then an
		/// StringFormatException is thrown. If the parameter 'pname' is not in the
		/// parameter list, an IllegalArgumentException is thrown.
		/// 
		/// </summary>
		/// <param name="pname">The parameter name.
		/// 
		/// </param>
		/// <returns> the value of the parameter as a boolean.
		/// 
		/// </returns>
		/// <exception cref="StringFormatException">If the parameter has a value which is
		/// neither "on" nor "off".
		/// 
		/// </exception>
		/// <exception cref="IllegalArgumentException">If there is no parameter with the
		/// name 'pname' in the parameter list.
		/// 
		/// </exception>
		public virtual bool getBooleanParameter(System.String pname)
		{
			System.String s = (System.String) getParameter(pname);
			
			if (s == null)
			{
				throw new System.ArgumentException("No parameter with name " + pname);
			}
			else if (s.Equals("on"))
			{
				return true;
			}
			else if (s.Equals("off"))
			{
				return false;
			}
			else
			{
				throw new StringFormatException("Parameter \"" + pname + "\" is not boolean: " + s);
			}
		}
		
		/// <summary> Returns the value of the named parameter as an int. If the parameter
		/// has a non-numeric value a NumberFormatException is thrown. If the
		/// parameter has a multiple word value than the first word is returned as
		/// an int, others are ignored. If the parameter 'pname' is not in the
		/// parameter list, an IllegalArgumentException is thrown.
		/// 
		/// </summary>
		/// <param name="pname">The parameter name.
		/// 
		/// </param>
		/// <returns> the value of the parameter as an int.
		/// 
		/// </returns>
		/// <exception cref="NumberFormatException">If the parameter has a non-numeric
		/// value.
		/// 
		/// </exception>
		/// <exception cref="IllegalArgumentException">If there is no parameter with the
		/// name 'pname' in the parameter list.
		/// 
		/// </exception>
		public virtual int getIntParameter(System.String pname)
		{
			System.String s = (System.String) getParameter(pname);
			
			if (s == null)
			{
				throw new System.ArgumentException("No parameter with name " + pname);
			}
			else
			{
				try
				{
					return System.Int32.Parse(s);
				}
				catch (System.FormatException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new System.FormatException("Parameter \"" + pname + "\" is not integer: " + e.Message);
				}
			}
		}
		
		/// <summary> Returns the value of the named parameter as a float. If the parameter
		/// has a non-numeric value a NumberFormatException is thrown. If the
		/// parameter has a multiple word value than the first word is returned as
		/// an int, others are ignored. If the parameter 'pname' is not in the
		/// parameter list, an IllegalArgumentException is thrown.
		/// 
		/// </summary>
		/// <param name="pname">The parameter name.
		/// 
		/// </param>
		/// <exception cref="NumberFormatException">If the parameter has a non-numeric
		/// value.
		/// 
		/// </exception>
		/// <exception cref="IllegalArgumentException">If there is no parameter with the
		/// name 'pname' in the parameter list.
		/// 
		/// </exception>
		/// <returns> the value of the parameter as a float.
		/// 
		/// </returns>
		public virtual float getFloatParameter(System.String pname)
		{
			System.String s = (System.String) getParameter(pname);
			
			if (s == null)
			{
				throw new System.ArgumentException("No parameter with name " + pname);
			}
			else
			{
				try
				{
					// Unfortunately there is no method to convert from a string
					// directly to a float
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Float.floatValue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					return (float) (System.Single.Parse(s));
				}
				catch (System.FormatException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new System.FormatException("Parameter \"" + pname + "\" is not floating-point: " + e.Message);
				}
			}
		}
		
		/// <summary> Checks if the parameters which name starts with the prefix 'prfx' in
		/// the parameter list are all in the list of valid parameter names
		/// 'plist'. If there is a parameter that is not in 'plist' an
		/// IllegalArgumentException is thrown with an explanation message. The
		/// default parameters are also included in the check.
		/// 
		/// </summary>
		/// <param name="prfx">The prefix of parameters to check.
		/// 
		/// </param>
		/// <param name="plist">The list of valid parameter names for the 'prfx'
		/// prefix. If null it is considered that no names are valid.
		/// 
		/// </param>
		/// <exception cref="IllegalArgumentException">If there's a parameter name
		/// starting with 'prfx' which is not in the valid list of parameter names.
		/// 
		/// </exception>
		public virtual void  checkList(char prfx, System.String[] plist)
		{
			System.Collections.IEnumerator args;
			System.String val;
			int i;
			bool isvalid;
			
			args = Keys.GetEnumerator();
			
			//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
			while (args.MoveNext())
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				val = ((System.String) args.Current);
				if (val.Length > 0 && val[0] == prfx)
				{
					isvalid = false;
					if (plist != null)
					{
						for (i = plist.Length - 1; i >= 0; i--)
						{
							if (val.Equals(plist[i]))
							{
								isvalid = true;
								break;
							}
						}
					}
					if (!isvalid)
					{
						// Did not find valid flag
						throw new System.ArgumentException("Option '" + val + "' is " + "not a valid one.");
					}
				}
			}
		}
		
		/// <summary> Checks if the parameters which names do not start with any of the
		/// prefixes in 'prfxs' in this ParameterList are all in the list of valid
		/// parameter names 'plist'. If there is a parameter that is not in 'plist'
		/// an IllegalArgumentException is thrown with an explanation message. The
		/// default parameters are also included in the check.
		/// 
		/// </summary>
		/// <param name="prfxs">The prefixes of parameters to ignore.
		/// 
		/// </param>
		/// <param name="plist">The list of valid parameter names. If null it is
		/// considered that no names are valid.
		/// 
		/// </param>
		/// <exception cref="IllegalArgumentException">If there's a parameter name not
		/// starting with 'prfx' which is not in the valid list of parameter names.
		/// 
		/// </exception>
		public virtual void  checkList(char[] prfxs, System.String[] plist)
		{
			System.Collections.IEnumerator args;
			System.String val, strprfxs;
			int i;
			bool isvalid;
			
			args = Keys.GetEnumerator();
			strprfxs = new System.String(prfxs);
			
			//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
			while (args.MoveNext())
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				val = ((System.String) args.Current);
				if (val.Length > 0 && strprfxs.IndexOf((System.Char) val[0]) == - 1)
				{
					isvalid = false;
					if (plist != null)
					{
						for (i = plist.Length - 1; i >= 0; i--)
						{
							if (val.Equals(plist[i]))
							{
								isvalid = true;
								break;
							}
						}
					}
					if (!isvalid)
					{
						throw new System.ArgumentException("Option '" + val + "' is " + "not a valid one.");
					}
				}
			}
		}
		
		/// <summary> Converts the usage information to a list of parameter names in a single
		/// array. The usage information appears in a 2D array of String. The first
		/// dimensions contains the different options, the second dimension
		/// contains the name of the option (first element), the synopsis and the
		/// explanation. This method takes the names of the different options in
		/// 'pinfo' and returns them in a single array of String.
		/// 
		/// </summary>
		/// <param name="pinfo">The list of options and their usage info (see above).
		/// 
		/// </param>
		/// <returns> An array with the names of the options in pinfo. If pinfo is
		/// null, null is returned.
		/// 
		/// </returns>
		public static System.String[] toNameArray(System.String[][] pinfo)
		{
			System.String[] pnames;
			
			if (pinfo == null)
			{
				return null;
			}
			
			pnames = new System.String[pinfo.Length];
			
			for (int i = pinfo.Length - 1; i >= 0; i--)
			{
				pnames[i] = pinfo[i][0];
			}
			return pnames;
		}
	}
}