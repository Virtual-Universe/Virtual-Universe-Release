/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CSharp;

//using Microsoft.JScript;

namespace VirtualUniverse.ScriptEngine.VirtualScript.CompilerTools
{
    public class CSConverter : IScriptConverter
    {
        private readonly CSharpCodeProvider CScodeProvider = new CSharpCodeProvider();

        #region IScriptConverter Members

        public string DefaultState
        {
            get { return ""; }
        }

        public void Initialise(Compiler compiler)
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

        private string CreateCompilerScript(string compileScript)
        {
            compileScript = compileScript.Replace("string",
                                                  "VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLString");

            compileScript = compileScript.Replace("integer",
                                                  "VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLInteger");

            compileScript = compileScript.Replace("float",
                                                  "VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.LSLFloat");

            compileScript = compileScript.Replace("list",
                                                  "VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.list");

            compileScript = compileScript.Replace("rotation",
                                                  "VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.Quaternion");

            compileScript = compileScript.Replace("vector",
                                                  "VirtualUniverse.ScriptEngine.VirtualScript.LSL_Types.Vector3");
            string compiledScript = "";
            compiledScript = String.Empty +
                             "using VirtualUniverse.ScriptEngine.VirtualScript.Runtime;\n" +
                             "using VirtualUniverse.ScriptEngine.VirtualScript;\n" +
                             "using System;\n" +
                             "using System.Collections.Generic;\n" +
                             "using System.Collections;\n" +
                             "namespace Script\n" +
                             "{\n";

            compiledScript +=
                "public class ScriptClass : VirtualUniverse.ScriptEngine.VirtualScript.Runtime.ScriptBaseClass, IDisposable\n";
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
        private readonly CSharpCodeProvider CScodeProvider = new CSharpCodeProvider();

        public bool m_addLSLAPI;
        public bool m_allowUnsafe;
        private Compiler m_compiler;
        public List<string> m_includedAssemblies = new List<string>();
        public List<string> m_includedDefines = new List<string>();

        #region IScriptConverter Members

        public string DefaultState
        {
            get { return ""; }
        }

        public void Initialise(Compiler compiler)
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
                                                                 "VirtualUniverse.BotManager.dll"));
                foreach (
                    string line in m_includedAssemblies.Where(line => !parameters.ReferencedAssemblies.Contains(line)))
                {
                    parameters.ReferencedAssemblies.Add(Path.Combine(rootPath,
                                                                     line));
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

        private string CreateCompilerScript(string compileScript)
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
            compiledScript = String.Empty +
                             "using VirtualUniverse.ScriptEngine.VirtualScript.Runtime;\n" +
                             "using VirtualUniverse.ScriptEngine.VirtualScript;\n" +
                             "using VirtualUniverse.ScriptEngine.VirtualScript.APIs.Interfaces;\n" +
                             "using VirtualUniverse.Framework;\n" +
                             "using VirtualUniverse.Framework.ClientInterfaces;\n" +
                             "using VirtualUniverse.Framework.ConsoleFramework;\n" +
                             "using VirtualUniverse.Framework.DatabaseInterfaces;\n" +
                             "using VirtualUniverse.Framework.Modules;\n" +
                             "using VirtualUniverse.Framework.Physics;\n" +
                             "using VirtualUniverse.Framework.PresenceInfo;\n" +
                             "using VirtualUniverse.Framework.SceneInfo;\n" +
                             "using VirtualUniverse.Framework.Servers;\n" +
                             "using VirtualUniverse.Framework.Services;\n" +
                             "using VirtualUniverse.Framework.Utilities;\n" +
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
                "public class ScriptClass : VirtualUniverse.ScriptEngine.VirtualScript.Runtime.ScriptBaseClass, IDisposable\n";
            compiledScript += "{\n";
            compiledScript +=
                compileScript;

            compiledScript += "\n}"; // Close Class

            compiledScript += "\n}"; // Close Namespace

            return compiledScript;
        }

        private void ReadLine(string line)
        {
            if (line.StartsWith("#include"))
            {
                line = line.Replace("#include", "");
                if (line.EndsWith(";"))
                    line = line.Remove(line.Length - 1);
                line = line.TrimStart(' ');
                m_includedDefines.Add(line); //TODO: Add a check here
            }
            else if (line.StartsWith("#assembly"))
            {
                line = line.Replace("#assembly", "");
                if (line.EndsWith(";"))
                    line = line.Remove(line.Length - 1);
                line = line.TrimStart(' ');
                m_includedAssemblies.Add(line); //TODO: Add a check here
            }
            else if (line.StartsWith("#threaded"))
            {
            }
            else if (line.StartsWith("#useLSLAPI"))
            {
                m_addLSLAPI = true;
            }
            else if (line.StartsWith("#allowUnsafe"))
            {
                m_allowUnsafe = true;
            }
        }

        private void LSLReadLine(ref string line)
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