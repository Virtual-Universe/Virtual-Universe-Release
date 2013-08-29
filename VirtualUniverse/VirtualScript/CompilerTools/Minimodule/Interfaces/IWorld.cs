/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;

namespace VirtualUniverse.ScriptEngine.VirtualScript.MiniModule
{
    public class NewUserEventArgs : EventArgs
    {
        public IAvatar Avatar;
    }

    public delegate void OnNewUserDelegate(IWorld sender, NewUserEventArgs e);

    public class ChatEventArgs : EventArgs
    {
        public int Channel;
        public IEntity Sender;
        public string Text;
    }

    public delegate void OnChatDelegate(IWorld sender, ChatEventArgs e);

    public interface IWorld
    {
        IObjectAccessor Objects { get; }
        IAvatar[] Avatars { get; }
        IParcel[] Parcels { get; }
        IHeightmap Terrain { get; }
        IWorldAudio Audio { get; }


        event OnChatDelegate OnChat;
        event OnNewUserDelegate OnNewUser;
    }
}