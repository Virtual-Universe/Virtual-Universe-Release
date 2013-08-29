/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public interface IAvatar : IEntity
    {
        bool IsChildAgent { get; }

        /// <value>
        ///     Array of worn attachments, empty but not null, if no attachments are worn
        /// </value>
        IAvatarAttachment[] Attachments { get; }

        /// <summary>
        ///     Request to open an url clientside
        /// </summary>
        void LoadUrl(IObject sender, string message, string url);
    }
}