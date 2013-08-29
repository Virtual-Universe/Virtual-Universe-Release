/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using System.IO;
using System.Reflection;

namespace VirtualUniverse.ScriptEngine.VirtualScript
{
    public class AssemblyResolver
    {
        private readonly string PathToSearch = "";

        public AssemblyResolver(string pathToSearch)
        {
            PathToSearch = pathToSearch;
        }

        public Assembly OnAssemblyResolve(object sender,
                                          ResolveEventArgs args)
        {
            if (!(sender is AppDomain))
                return null;

            string[] pathList = new[]
                                    {
                                        Path.Combine(Directory.GetCurrentDirectory(), "bin"),
                                        Path.Combine(Directory.GetCurrentDirectory(), PathToSearch),
                                        Path.Combine(Directory.GetCurrentDirectory(),
                                                     Path.Combine(PathToSearch, "Scripts")),
                                        Directory.GetCurrentDirectory(),
                                    };

            string assemblyName = args.Name;
            if (assemblyName.IndexOf(",") != -1)
                assemblyName = args.Name.Substring(0, args.Name.IndexOf(","));

            foreach (string s in pathList)
            {
                string path = Path.Combine(s, assemblyName) + ".dll";

                if (File.Exists(path))
                    return Assembly.Load(AssemblyName.GetAssemblyName(path));
            }
            return null;
        }
    }
}