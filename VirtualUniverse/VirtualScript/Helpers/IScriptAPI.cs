/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Framework;
using VirtualUniverse.Framework.SceneInfo;
using OpenMetaverse;

namespace VirtualUniverse.ScriptEngine.VirtualUniverse
{
    public interface IScriptApi
    {
        //
        // Each API has an identifier, which is used to load the
        // proper runtime assembly at load time.
        //

        /// <summary>
        ///     Returns the plugin name
        /// </summary>
        /// <returns></returns>
        string Name { get; }

        /// <summary>
        ///     The name of the interface that is used to implement the functions
        /// </summary>
        string InterfaceName { get; }

        /// <summary>
        ///     Any assemblies that may need referenced to implement your Api.
        ///     If you are adding an Api, you will need to have the path to your assembly in this
        ///     (along with any other assemblies you may need). You can use this code to add the current assembly
        ///     to this list:
        ///     "this.GetType().Assembly.Location"
        ///     as shown in the Bot_API.cs in Aurora.BotManager.
        /// </summary>
        string[] ReferencedAssemblies { get; }

        /// <summary>
        ///     If you do not use the standard namespaces for your API module, you will need to add them here
        ///     As shown in the Bot_API.cs in Aurora.BotManager.
        /// </summary>
        string[] NamespaceAdditions { get; }

        void Initialize(IScriptModulePlugin engine, ISceneChildEntity part, uint localID, UUID item,
                        ScriptProtectionModule module);

        /// <summary>
        ///     Make a copy of the api so that it can be used again
        /// </summary>
        /// <returns></returns>
        IScriptApi Copy();
    }
}