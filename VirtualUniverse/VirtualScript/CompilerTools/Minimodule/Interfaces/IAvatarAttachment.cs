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
    public interface IAvatarAttachment
    {
        /// <value>
        ///     Describes where on the avatar the attachment is located
        /// </value>
        int Location { get; }

        /// <value>
        ///     Accessor to the rez'ed asset, representing the attachment
        /// </value>
        IObject Asset { get; }
    }
}