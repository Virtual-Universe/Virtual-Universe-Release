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
    ///     This interface defines a way of working with project-reader.
    ///     Currently the only project-reader developed is the one for c# projects,
    ///     but many other readers can be developed for various languages/IDEs.
    /// </summary>
    public interface IProjectReader
    {
        /// <summary>
        ///     This method will be used to check if the specified project file
        ///     can be opened.
        ///     Implementations may involve operations like: checking the file extension,
        ///     reading the file header (or some specific sectors) and getting
        ///     the file version.
        ///     Note!
        ///     If the call returns true it does not guaranties calls to ReadProject
        ///     will be successful.
        /// </summary>
        /// <param name="filename">
        ///     The name of the project file. It should include
        ///     path (absolute or relative).
        /// </param>
        /// <returns>
        ///     True if the project-reader "believes" he can read the project
        ///     file.
        /// </returns>
        bool CanOpen(string filename);

        /// <summary>
        ///     This method will be used to read a project file and extract all the
        ///     data needed for the UI and for compilation.
        /// </summary>
        /// <param name="filename">
        ///     The name of the project file. It should include
        ///     path (absolute or relative).
        /// </param>
        /// <returns>All the data needed for UI and compilation</returns>
        BasicProject ReadProject(string filename);
    }
}