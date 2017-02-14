/*
 * Copyright (c) Contributors, http://virtual-planets.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 * For an explanation of the license of each contributor and the content it 
 * covers please see the Licenses directory.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual Universe Project nor the
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
 */

/*
 * No license given,
 * taken from http://csharper.fairblog.ro/2010/05/compiling-c-projects-at-runtime-parsing-the-csproj-file 
 */

namespace RunTimeCompiler
{
	/// <summary>
	///     This is a reader for C# project files, or more precisely a
	///     wrapper to a reader of all .csproj.
	///     As the structure of the .csproj file appears to change with the version
	///     of Visual Studio, this class must use the appropriate .csproj reader.
	///     AllCsprojReader (the only class used currently to process .csproj files)
	///     support .csproj files created with Visual Studio 2005+.
	///     Visual Studio 2010 is the most recent version of Visual Studio, and the
	///     .csproj files it generates are processed ok by AllCsprojReader.
	///     I do not know if .csproj files created with Visual Studio 2001-2003 can
	///     be processed successfully.
	/// </summary>
	public class CsprojReader : IProjectReader
	{
		#region IProjectReader Members

		/// <summary>
		///     Defined in IProjectReader.
		///     It is used to check if the specified file can be opened by this
		///     project-reader.
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool CanOpen (string filename)
		{
			//TODO: Add some logic here: check extension to be .csproj,
			//open the .csproj file and get Visual Studio version and see
			//if that version of .csproj can be open...
			return true;
		}

		/// <summary>
		///     Defined in IProjectReader.
		///     It is used to retrieve all the data needed for UI and compilation.
		/// </summary>
		/// <param name="filename">The name (and path) of the C# project file.</param>
		/// <returns></returns>
		public BasicProject ReadProject (string filename)
		{
			AllCsprojReader reader;
			if (!CanOpen (filename))
				return null;
			reader = new AllCsprojReader ();
			return reader.ReadProject (filename);
		}

		#endregion
	}
}