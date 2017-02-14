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
	///     This class should keep a list of known file extensions and
	///     the registered IProjectReader for each extension.
	///     When a project file is loaded the ReadProject method is called
	///     to read that project and get all data needed for the UI and
	///     compilation.
	///     This class is implemented as a singleton.
	/// </summary>
	public class ProjectReader
	{
		#region Singleton pattern

		/// <summary>
		///     Private reference to a ProjectReader instance.
		/// </summary>
		private static ProjectReader _instance;

		/// <summary>
		///     Private constructor.
		/// </summary>
		private ProjectReader ()
		{
		}

		/// <summary>
		///     The only accessor for a ProjectReader instance.
		/// </summary>
		public static ProjectReader Instance {
			get {
				if (_instance == null)
					_instance = new ProjectReader ();
				return _instance;
			}
		}

		#endregion

		/// <summary>
		///     This method is used to read the content of a project file and get
		///     all the data needed for UI and compilation.
		///     Current implementation always use CsprojReader. It will be changed
		///     as more project-readers will be developed.
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public BasicProject ReadProject (string filename)
		{
			//TODO: Add some logic here: check filename extension, if it is
			//.csproj use CsprojReader, check if it can open the file and read
			//the project or use an other IProjectReader if the current version
			//of .csproj is unknown to CsprojReader.

			//Get the appropriate reader for the filetype.
			//As filetype is always .csproj I'll just use CsprojReader.
			CsprojReader reader = new CsprojReader ();
			//TODO: Decide if this is needed. ReadProject should only be called after CanOpen, 
			//so I may not need to check it again.
			if (!reader.CanOpen (filename))
				return null;
			return reader.ReadProject (filename);
		}
	}
}