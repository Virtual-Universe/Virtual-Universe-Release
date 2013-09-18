/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using VirtualUniverse.Framework.ClientInterfaces;
using OpenMetaverse;
using OpenMetaverse.Packets;
using VirtualUniverse.Framework;

namespace VirtualUniverse.ClientStack
{
    public delegate void UnackedPacketMethod(OutgoingPacket packet);

    /// <summary>
    ///     Holds a reference to the <seealso cref="LLUDPClient" /> this packet is
    ///     destined for, along with the serialized packet data, sequence number
    ///     (if this is a resend), number of times this packet has been resent,
    ///     the time of the last resend, and the throttling category for this
    ///     packet
    /// </summary>
    public sealed class OutgoingPacket
    {
        /// <summary>
        ///     Packet data to send
        /// </summary>
        public UDPPacketBuffer Buffer;

        /// <summary>
        ///     Category this packet belongs to
        /// </summary>
        public ThrottleOutPacketType Category;

        /// <summary>
        ///     Client this packet is destined for
        /// </summary>
        public LLUDPClient Client;

        /// <summary>
        ///     The delegate to be called when this packet is sent
        /// </summary>
        public UnackedPacketMethod FinishedMethod;

        /// <summary>
        ///     The packet we are sending
        /// </summary>
        public Packet Packet;

        /// <summary>
        ///     The # of times the server has attempted to send this packet
        /// </summary>
        public int ReSendAttempt;

        /// <summary>
        ///     Number of times this packet has been resent
        /// </summary>
        public int ResendCount;

        /// <summary>
        ///     Sequence number of the wrapped packet
        /// </summary>
        public uint SequenceNumber;

        /// <summary>
        ///     Environment.TickCount when this packet was last sent over the wire
        /// </summary>
        public int TickCount;

        /// <summary>
        ///     The delegate to be called if this packet is determined to be unacknowledged
        /// </summary>
        public UnackedPacketMethod UnackedMethod;

        public int WhoDoneIt;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="client">Reference to the client this packet is destined for</param>
        /// <param name="buffer">
        ///     Serialized packet data. If the flags or sequence number
        ///     need to be updated, they will be injected directly into this binary buffer
        /// </param>
        /// <param name="category">Throttling category for this packet</param>
        /// <param name="resendMethod">The delegate to be called if this packet is determined to be unacknowledged</param>
        /// <param name="finishedMethod">The delegate to be called when this packet is sent</param>
        /// <param name="packet"></param>
        public OutgoingPacket(LLUDPClient client, UDPPacketBuffer buffer,
                              ThrottleOutPacketType category, UnackedPacketMethod resendMethod,
                              UnackedPacketMethod finishedMethod, Packet packet)
        {
            Client = client;
            Buffer = buffer;
            Category = category;
            UnackedMethod = resendMethod;
            FinishedMethod = finishedMethod;
            Packet = packet;
        }

        public void Destroy(int whoDoneIt)
        {
            WhoDoneIt = whoDoneIt;
            /*if(!PacketPool.Instance.ReturnPacket(Packet))
                Packet = null;
            Buffer = null;
            FinishedMethod = null;
            UnackedMethod = null;
            Client = null;
            SequenceNumber = 0;*/
        }
    }
}