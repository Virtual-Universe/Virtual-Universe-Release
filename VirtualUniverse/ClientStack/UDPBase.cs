/*
 * Software license for this code will be added after the code is completed.
 * This code is in the early stages of development so should not be utilized 
 * at this time.
 * 
 * Thanks
 * Virtual Universe Development Team
 */

using System;
using System.Net;
using System.Net.Sockets;
using VirtualUniverse.Framework.ConsoleFramework;
using OpenMetaverse;
using VirtualUniverse.Framework.Utilities;

namespace VirtualUniverse.ClientStack
{
    /// <summary>
    ///     Base UDP server
    /// </summary>
    public abstract class UDPBase
    {
        /// <summary>
        ///     Flag to process packets asynchronously or synchronously
        /// </summary>
        private bool m_asyncPacketHandling;

        /// <summary>
        ///     Local IP address to bind to in server mode
        /// </summary>
        protected IPAddress m_localBindAddress;

        /// <summary>
        ///     The all important shutdown flag
        /// </summary>
        private volatile bool m_shutdownFlag = true;

        /// <summary>
        ///     UDP port to bind to in server mode
        /// </summary>
        protected int m_udpPort;

        /// <summary>
        ///     UDP socket, used in either client or server mode
        /// </summary>
        private Socket m_udpSocket;

        /// <summary>
        ///     Returns true if the server is currently listening, otherwise false
        /// </summary>
        public bool IsRunning
        {
            get { return !m_shutdownFlag; }
        }

        /// <summary>
        ///     This method is called when an incoming packet is received
        /// </summary>
        /// <param name="buffer">Incoming packet buffer</param>
        protected abstract void PacketReceived(UDPPacketBuffer buffer);

        /// <summary>
        ///     Default initialiser
        /// </summary>
        /// <param name="bindAddress">Local IP address to bind the server to</param>
        /// <param name="port">Port to listening for incoming UDP packets on</param>
        public virtual void Initialise(IPAddress bindAddress, int port)
        {
            m_localBindAddress = bindAddress;
            m_udpPort = port;
        }

        /// <summary>
        ///     Start the UDP server
        /// </summary>
        /// <param name="recvBufferSize">
        ///     The size of the receive buffer for
        ///     the UDP socket. This value is passed up to the operating system
        ///     and used in the system networking stack. Use zero to leave this
        ///     value as the default
        /// </param>
        /// <param name="asyncPacketHandling">
        ///     Set this to true to start
        ///     receiving more packets while current packet handler callbacks are
        ///     still running. Setting this to false will complete each packet
        ///     callback before the next packet is processed
        /// </param>
        /// <remarks>
        ///     This method will attempt to set the SIO_UDP_CONNRESET flag
        ///     on the socket to get newer versions of Windows to behave in a sane
        ///     manner (not throwing an exception when the remote side resets the
        ///     connection). This call is ignored on Mono where the flag is not
        ///     necessary
        /// </remarks>
        public void Start(int recvBufferSize, bool asyncPacketHandling)
        {
            m_asyncPacketHandling = asyncPacketHandling;

            if (m_shutdownFlag)
            {
                const int SIO_UDP_CONNRESET = -1744830452;

                IPEndPoint ipep = new IPEndPoint(m_localBindAddress, m_udpPort);

                m_udpSocket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Dgram,
                    ProtocolType.Udp);

                try
                {
                    // This udp socket flag is not supported under mono, 
                    // so we'll catch the exception and continue
                    if (Util.IsWindows())
                        m_udpSocket.IOControl(SIO_UDP_CONNRESET, new byte[] { 0 }, null);
                    //MainConsole.Instance.Debug("[UDPBASE]: SIO_UDP_CONNRESET flag set");
                }
                catch (SocketException)
                {
                    //MainConsole.Instance.Debug("[UDPBASE]: SIO_UDP_CONNRESET flag not supported on this platform, ignoring");
                }

                if (recvBufferSize != 0)
                    m_udpSocket.ReceiveBufferSize = recvBufferSize;

                m_udpSocket.Bind(ipep);

                // we're not shutting down, we're starting up
                m_shutdownFlag = false;

                // kick off an async receive.  The Start() method will return, the
                // actual receives will occur asynchronously and will be caught in
                // AsyncEndRecieve().
                AsyncBeginReceive();
            }
        }

        /// <summary>
        ///     Stops the UDP server
        /// </summary>
        public void Stop()
        {
            if (!m_shutdownFlag)
            {
                // wait indefinitely for a writer lock.  Once this is called, the .NET runtime
                // will deny any more reader locks, in effect blocking all other send/receive
                // threads.  Once we have the lock, we set shutdownFlag to inform the other
                // threads that the socket is closed.
                m_shutdownFlag = true;
                m_udpSocket.Close();
            }
        }

        private void AsyncBeginReceive()
        {
            // allocate a packet buffer
            //WrappedObject<UDPPacketBuffer> wrappedBuffer = Pool.CheckOut();
            UDPPacketBuffer buf = new UDPPacketBuffer();

            if (!m_shutdownFlag)
            {
                try
                {
                    // kick off an async read
                    m_udpSocket.BeginReceiveFrom(
                        //wrappedBuffer.Instance.Data,
                        buf.Data,
                        0,
                        UDPPacketBuffer.BUFFER_SIZE,
                        SocketFlags.None,
                        ref buf.RemoteEndPoint,
                        AsyncEndReceive,
                        //wrappedBuffer);
                        buf);
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        MainConsole.Instance.Warn(
                            "[UDPBASE]: SIO_UDP_CONNRESET was ignored, attempting to salvage the UDP listener on port " +
                            m_udpPort);
                        bool salvaged = false;
                        while (!salvaged)
                        {
                            try
                            {
                                m_udpSocket.BeginReceiveFrom(
                                    //wrappedBuffer.Instance.Data,
                                    buf.Data,
                                    0,
                                    UDPPacketBuffer.BUFFER_SIZE,
                                    SocketFlags.None,
                                    ref buf.RemoteEndPoint,
                                    AsyncEndReceive,
                                    //wrappedBuffer);
                                    buf);
                                salvaged = true;
                            }
                            catch (SocketException)
                            {
                            }
                            catch (ObjectDisposedException)
                            {
                                return;
                            }
                        }

                        MainConsole.Instance.Warn("[UDPBASE]: Salvaged the UDP listener on port " + m_udpPort);
                    }
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        private void AsyncEndReceive(IAsyncResult iar)
        {
            // Asynchronous receive operations will complete here through the call
            // to AsyncBeginReceive
            if (!m_shutdownFlag)
            {
                // Asynchronous mode will start another receive before the
                // callback for this packet is even fired. Very parallel :-)
                if (m_asyncPacketHandling)
                    AsyncBeginReceive();

                // get the buffer that was created in AsyncBeginReceive
                // this is the received data
                //WrappedObject<UDPPacketBuffer> wrappedBuffer = (WrappedObject<UDPPacketBuffer>)iar.AsyncState;
                //UDPPacketBuffer buffer = wrappedBuffer.Instance;
                UDPPacketBuffer buffer = (UDPPacketBuffer)iar.AsyncState;

                try
                {
                    // get the length of data actually read from the socket, store it with the
                    // buffer
                    buffer.DataLength = m_udpSocket.EndReceiveFrom(iar, ref buffer.RemoteEndPoint);

                    // call the abstract method PacketReceived(), passing the buffer that
                    // has just been filled from the socket read.
                    PacketReceived(buffer);
                }
                catch (SocketException)
                {
                }
                catch (ObjectDisposedException)
                {
                }
                catch (Exception ex)
                {
                    MainConsole.Instance.Error("[UDPBase]: Hit error: " + ex.ToString());
                }
                finally
                {
                    //wrappedBuffer.Dispose();

                    // Synchronous mode waits until the packet callback completes
                    // before starting the receive to fetch another packet
                    if (!m_asyncPacketHandling)
                        AsyncBeginReceive();
                }
            }
        }

        public void SyncSend(UDPPacketBuffer buf)
        {
            if (!m_shutdownFlag)
            {
                try
                {
                    // well not async but blocking 
                    m_udpSocket.SendTo(
                        buf.Data,
                        0,
                        buf.DataLength,
                        SocketFlags.None,
                        buf.RemoteEndPoint);
                }
                catch (SocketException)
                {
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        /* not in use Send Sync now
                public void AsyncBeginSend(UDPPacketBuffer buf)
                {
                    if (!m_shutdownFlag)
                    {
                        try
                        {
                            m_udpSocket.BeginSendTo(
                                buf.Data,
                                0,
                                buf.DataLength,
                                SocketFlags.None,
                                buf.RemoteEndPoint,
                                AsyncEndSend,
                                buf);
 
                        }
                        catch (SocketException) { }
                        catch (ObjectDisposedException) { }
                    }
                }

                void AsyncEndSend(IAsyncResult result)
                {
                    try
                    {
        //                UDPPacketBuffer buf = (UDPPacketBuffer)result.AsyncState;
                        m_udpSocket.EndSendTo(result);
                    }
                    catch (SocketException) { }
                    catch (ObjectDisposedException) { }
                }
         */
    }
}