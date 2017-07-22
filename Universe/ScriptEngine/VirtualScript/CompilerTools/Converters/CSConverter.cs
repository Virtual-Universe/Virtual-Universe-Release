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

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CSharp;

//using Microsoft.JScript;

namespace Universe.ScriptEngine.VirtualScript.CompilerTools
{
    public class CSConverter : IScriptConverter
    {
        readonly CSharpCodeProvider CScodeProvider = new CSharpCodeProvider();

        #region IScriptConverter Members

        public string DefaultState
        {
            get { return ""; }
        }

        public void Initialize(Compiler compiler)
        {
        }

        public void Convert(string Script, out string CompiledScript,
                            out object PositionMap)
        {
            CompiledScript = CreateCompilerScript(Script);
            PositionMap = null;
        }

        public string Name
        {
            get { return "cs"; }
        }

        public CompilerResults Compile(CompilerParameters parameters, bool isFile, string Script)
        {
            bool complete = false;
            bool retried = false;
            CompilerResults results;
            do
            {
                lock (CScodeProvider)
                {
                    if (isFile)
                        results = CScodeProvider.CompileAssemblyFromFile(
                            parameters, Script);
                    else
                        results = CScodeProvider.CompileAssemblyFromSource(
                            parameters, Script);
                }
                // Deal with an occasional segv in the compiler.
                // Rarely, if ever, occurs twice in succession.
                // Line # == 0 and no file name are indications that
                // this is a native stack trace rather than a normal
                // error log.
                if (results.Errors.Count > 0)
                {
                    if (!retried && string.IsNullOrEmpty(results.Errors[0].FileName) &&
                        results.Errors[0].Line == 0)
                    {
                        // System.Console.WriteLine("retrying failed compilation");
                        retried = true;
                    }
                    else
                    {
                        complete = true;
                    }
                }
                else
                {
                    complete = true;
                }
            } while (!complete);
            return results;
        }

        public void FinishCompile(IScriptModulePlugin plugin, ScriptData data, IScript Script)
        {
        }

        public void FindErrorLine(CompilerError CompErr, object PositionMap, string script, out int LineN, out int CharN)
        {
            LineN = CompErr.Line;
            CharN = CompErr.Column;
        }

        #endregion

        public void Dispose()
        {
        }

        string CreateCompilerScript(string compileScript)
        {
            compileScript = compileScript.Replace("string",
                                                  "Universe.ScriptEngine.VirtualScript.LSL_Types.LSLString");

            compileScript = compileScript.Replace("integer",
                                                  "Universe.ScriptEngine.VirtualScript.LSL_Types.LSLInteger");

            compileScript = compileScript.Replace("float",
                                                  "Universe.ScriptEngine.VirtualScript.LSL_Types.LSLFloat");

            compileScript = compileScript.Replace("list",
                                                  "Universe.ScriptEngine.VirtualScript.LSL_Types.list");

            compileScript = compileScript.Replace("rotation",
                                                  "Universe.ScriptEngine.VirtualScript.LSL_Types.Quaternion");

            compileScript = compileScript.Replace("vector",
                                                  "Universe.ScriptEngine.VirtualScript.LSL_Types.Vector3");
            string compiledScript = "";
            compiledScript = string.Empty +
                             "using Universe.ScriptEngine.VirtualScript.Runtime;\n" +
                             "using Universe.ScriptEngine.VirtualScript;\n" +
                             "using System;\n" +
                             "using System.Collections.Generic;\n" +
                             "using System.Collections;\n" +
                             "namespace Script\n" +
                             "{\n";

            compiledScript +=
                "public class ScriptClass : Universe.ScriptEngine.VirtualScript.Runtime.ScriptBaseClass, IDisposable\n";
            compiledScript += "{\n";
            compiledScript +=
                compileScript;

            compiledScript += "\n}"; // Close Class

            compiledScript += "\n}"; // Close Namespace

            return compiledScript;
        }
    }

    public class AScriptConverter : IScriptConverter
    {
        readonly CSharpCodeProvider CScodeProvider = new CSharpCodeProvider();
        Compiler m_compiler;

        public bool m_addLSLAPI;
        public bool m_allowUnsafe;
        public List<string> m_includedAssemblies = new List<string>();
        public List<string> m_includedDefines = new List<string>();

        #region IScriptConverter Members

        public string DefaultState
        {
            get { return ""; }
        }

        public void Initialize(Compiler compiler)
        {
            m_compiler = compiler;
        }

        public void Convert(string Script, out string CompiledScript,
                            out object PositionMap)
        {
            #region Reset

            m_includedDefines.Clear();
            m_includedAssemblies.Clear();
            m_allowUnsafe = false;
            m_addLSLAPI = false;

            #endregion

            CompiledScript = CreateCompilerScript(Script);
            PositionMap = 22 + m_includedDefines.Count;
        }

        public string Name
        {
            get { return "ascript"; }
        }

        public CompilerResults Compile(CompilerParameters parameters, bool isFile, string Script)
        {
            string rootPath =
                Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            if (rootPath != null)
            {
                parameters.ReferencedAssemblies.Add(Path.Combine(rootPath,
                                                                 "OpenMetaverse.dll"));
                parameters.ReferencedAssemblies.Add(Path.Combine(rootPath,
                                                                 "OpenMetaverseTypes.dll"));
                parameters.ReferencedAssemblies.Add(Path.Combine(rootPath,
                                                                 "OpenMetaverse.StructuredData.dll"));
                parameters.ReferencedAssemblies.Add(Path.Combine(rootPath,
                                                                 "Universe.BotManager.dll"));
                foreach (
                    string line in m_includedAssemblies.Where(line => !parameters.ReferencedAssemblies.Contains(line)))
                {
                    parameters.ReferencedAssemblies.Add(Path.Combine(rootPath, line));
                }
            }
            bool complete = false;
            bool retried = false;
            CompilerResults results;
            do
            {
                lock (CScodeProvider)
                {
                    if (isFile)
                        results = CScodeProvider.CompileAssemblyFromFile(
                            parameters, Script);
                    else
                        results = CScodeProvider.CompileAssemblyFromSource(
                            parameters, Script);
                }
                // Deal with an occasional segv in the compiler.
                // Rarely, if ever, occurs twice in succession.
                // Line # == 0 and no file name are indications that
                // this is a native stack trace rather than a normal
                // error log.
                if (results.Errors.Count > 0)
                {
                    if (!retried && string.IsNullOrEmpty(results.Errors[0].FileName) &&
                        results.Errors[0].Line == 0)
                    {
                        // System.Console.WriteLine("retrying failed compilation");
                        retried = true;
                    }
                    else
                    {
                        complete = true;
                    }
                }
                else
                {
                    complete = true;
                }
            } while (!complete);
            return results;
        }

        public void FinishCompile(IScriptModulePlugin plugin, ScriptData data, IScript Script)
        {
            Script.SetSceneRefs(data.World, data.Part, false);
        }

        public void FindErrorLine(CompilerError CompErr, object PositionMap, string script, out int LineN, out int CharN)
        {
            LineN = CompErr.Line - ((int)PositionMap) + 1;
            CharN = CompErr.Column - 1;
        }

        #endregion

        public void Dispose()
        {
        }

        string CreateCompilerScript(string compileScript)
        {
            bool newLine = true;
            bool reading = true;
            string lastLine = "";
            for (int i = 0; i < compileScript.Length; i++)
            {
                if (compileScript[i] == '\n')
                {
                    if (lastLine != "")
                        ReadLine(lastLine);
                    reading = false;
                    lastLine = "";
                    newLine = true;
                }
                if (!newLine)
                {
                    reading = false;
                    continue;
                }
                if (compileScript.Length <= i + 2)
                    continue;
                if (!reading &&
                    !(compileScript[i + 1] == '#' || (compileScript[i + 1] == '/' && compileScript[i + 2] == '/')))
                {
                    newLine = false;
                    continue;
                }
                reading = true;
                if (compileScript[i] != '\n')
                    lastLine += compileScript[i];
                compileScript = compileScript.Remove(i, 1);
                i--; //Removed a letter, remove a char here as well
            }
            if (m_addLSLAPI)
            {
                string[] lines = compileScript.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    LSLReadLine(ref lines[i]);
                }
                compileScript = string.Join("\n", lines);
            }
            string compiledScript = "";
            compiledScript = string.Empty +
                             "using Universe.ScriptEngine.VirtualScript.Runtime;\n" +
                             "using Universe.ScriptEngine.VirtualScript;\n" +
                             "using Universe.ScriptEngine.VirtualScript.APIs.Interfaces;\n" +
                             "\n" +
                             "using Universe.Framework.ClientInterfaces;\n" +
                             "using Universe.Framework.ConsoleFramework;\n" +
                             "using Universe.Framework.DatabaseInterfaces;\n" +
                             "using Universe.Framework.Modules;\n" +
                             "using Universe.Framework.Physics;\n" +
                             "using Universe.Framework.PresenceInfo;\n" +
                             "using Universe.Framework.SceneInfo;\n" +
                             "using Universe.Framework.Servers;\n" +
                             "using Universe.Framework.Services;\n" +
                             "using Universe.Framework.Utilities;\n" +
                             "using OpenMetaverse;\n" +
                             "using System;\n" +
                             "using System.Collections.Generic;\n" +
                             "using System.Collections;\n";
            foreach (string line in m_includedDefines)
            {
                compiledScript += "using " + line + ";\n";
            }
            compiledScript += "namespace Script\n" +
                              "{\n";

            compiledScript +=
                "public class ScriptClass : Universe.ScriptEngine.VirtualScript.Runtime.ScriptBaseClass, IDisposable\n";
            compiledScript += "{\n";
            compiledScript +=
                compileScript;

            compiledScript += "\n}"; // Close Class

            compiledScript += "\n}"; // Close Namespace

            return compiledScript;
        }

        void ReadLine(string line)
        {
            if (line.StartsWith ("#include", StringComparison.Ordinal))
            {
                line = line.Replace("#include", "");
                if (line.EndsWith (";", StringComparison.Ordinal))
                    line = line.Remove(line.Length - 1);
                line = line.TrimStart(' ');
                m_includedDefines.Add(line); //TODO: Add a check here
            }
            else if (line.StartsWith ("#assembly", StringComparison.Ordinal))
            {
                line = line.Replace("#assembly", "");
                if (line.EndsWith (";", StringComparison.Ordinal))
                    line = line.Remove(line.Length - 1);
                line = line.TrimStart(' ');
                m_includedAssemblies.Add(line); //TODO: Add a check here
            }
            else if (line.StartsWith ("#threaded", StringComparison.Ordinal))
            {
            }
            else if (line.StartsWith ("#useLSLAPI", StringComparison.Ordinal))
            {
                m_addLSLAPI = true;
            }
            else if (line.StartsWith ("#allowUnsafe", StringComparison.Ordinal))
            {
                m_allowUnsafe = true;
            }
        }

        void LSLReadLine(ref string line)
        {
            string testLine = line;
            foreach (KeyValuePair<string, IScriptApi> functionName in m_compiler.ScriptEngine.GetAllFunctionNamesAPIs())
            {
                string newline = testLine.Replace(functionName.Key, "<>");
                if (newline != testLine)
                {
                    testLine = newline;
                    line = line.Replace(functionName.Key,
                                                  "((" + functionName.Value.InterfaceName + ")m_apis[\"" +
                                                  functionName.Value.Name + "\"])." + functionName.Key);
                }
            }
        }
    }
}
