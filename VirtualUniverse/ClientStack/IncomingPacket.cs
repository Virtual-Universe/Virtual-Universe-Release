/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using OpenMetaverse.Packets;

namespace VirtualUniverse.ClientStack
{
    /// <summary>
    ///     Holds a reference to a <seealso cref="LLUDPClient" /> and a <seealso cref="Packet" />
    ///     for incoming packets
    /// </summary>
    public sealed class IncomingPacket
    {
        /// <summary>
        ///     Client this packet came from
        /// </summary>
        public LLUDPClient Client;

        /// <summary>
        ///     Packet data that has been received
        /// </summary>
        public Packet Packet;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="client">Reference to the client this packet came from</param>
        /// <param name="packet">Packet data</param>
        public IncomingPacket(LLUDPClient client, Packet packet)
        {
            Client = client;
            Packet = packet;
        }
    }
}