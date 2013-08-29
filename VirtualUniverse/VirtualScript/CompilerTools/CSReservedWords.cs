/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using System.Collections.Generic;

namespace VirtualUniverse.ScriptEngine.VirtualScript.CompilerTools
{
    /// <summary>
    ///     A container for all of the reserved C# words that are not also reserved words in LSL.
    ///     The words must be maintained in alphabetical order.
    ///     The words that are key words in lsl are picked up by the lsl compiler as errors.
    ///     The LSL reserved words have been left in the list as comments for completeness
    /// </summary>
    internal class CSReservedWords
    {
        private static readonly List<string> reservedWords = new List<string>(new[]
                                                                                  {
                                                                                      "abstract", "as", "async",
                                                                                      "base", "bool", "break", "byte",
                                                                                      "case", "catch", "char", "checked"
                                                                                      , "class", "const", "continue",
                                                                                      "decimal", "default", "delegate",
                                                                                      //"do",
                                                                                      "double",
                                                                                      //"else",
                                                                                      "enum",
                                                                                      //"event",
                                                                                      "explicit", "extern",
                                                                                      "false", "finally", "fixed",
                                                                                      //"float","for",
                                                                                      "foreach",
                                                                                      "goto",
                                                                                      //"if",
                                                                                      "implicit", "in", "int",
                                                                                      "interface", "internal", "is",
                                                                                      "lock", "long",
                                                                                      "namespace", "new", "null",
                                                                                      "object", "operator", "out",
                                                                                      "override",
                                                                                      "params", "private", "protected",
                                                                                      "public",
                                                                                      "readonly", "ref",
                                                                                      //"return",
                                                                                      "sbyte", "sealed", "short",
                                                                                      "sizeof", "stackalloc", "static",
                                                                                      //"string",
                                                                                      "struct", "switch",
                                                                                      "this", "throw", "true", "try",
                                                                                      "typeof",
                                                                                      "uint", "ulong", "unchecked",
                                                                                      "unsafe", "ushort", "using",
                                                                                      "virtual", "void", "volatile",
                                                                                      //"while"
                                                                                  });

        /// <summary>
        ///     Returns true if the passed string is in the list of reserved words with
        ///     a little simple pre-filtering.
        /// </summary>
        internal static bool IsReservedWord(string word)
        {
            // A couple of quick filters to weed out single characters, ll functions and
            // anything that starts with an uppercase letter
            if (String.IsNullOrEmpty(word)) return false;
            if (word.Length < 2) return false;
            if (word.StartsWith("ll")) return false;
            char first = word.ToCharArray(0, 1)[0];
            if (first >= 'A' && first <= 'Z') return false;

            return (reservedWords.BinarySearch(word) >= 0);
        }
    }
}