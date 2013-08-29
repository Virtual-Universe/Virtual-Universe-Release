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
using Microsoft.VisualBasic;

//using Microsoft.JScript;

namespace VirtualUniverse.ScriptEngine.VirtualScript.CompilerTools
{
    public class VBConverter : IScriptConverter
    {
        private readonly VBCodeProvider VBcodeProvider = new VBCodeProvider();

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
            // Remove the //vb chars
            Script = Script.Substring(4, Script.Length - 4);
            CompiledScript = CreateCompilerScript(Script);
            PositionMap = null;
        }

        public string Name
        {
            get { return "vb"; }
        }

        public CompilerResults Compile(CompilerParameters parameters, bool isFile, string Script)
        {
            bool complete = false;
            bool retried = false;
            CompilerResults results;
            do
            {
                lock (VBcodeProvider)
                {
                    if (isFile)
                        results = VBcodeProvider.CompileAssemblyFromFile(
                            parameters, Script);
                    else
                        results = VBcodeProvider.CompileAssemblyFromSource(
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
            compileScript = String.Empty +
                            "Imports VirtualUniverse.ScriptEngine.VirtualScript.Runtime: Imports VirtualUniverse.ScriptEngine.VirtualScript: Imports System.Collections.Generic: " +
                            String.Empty + "NameSpace Script:" +
                            String.Empty +
                            "Public Class ScriptClass: Inherits VirtualUniverse.ScriptEngine.VirtualScript.Runtime.ScriptBaseClass: " +
                            "\r\nPublic Sub New()\r\nEnd Sub: " +
                            compileScript +
                            ":End Class :End Namespace\r\n";
            return compileScript;
        }
    }
}