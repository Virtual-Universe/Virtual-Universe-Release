/***************************************************************************
 *	                VIRTUAL REALITY PUBLIC SOURCE LICENSE
 * 
 * Date				: Sun January 1, 2006
 * Copyright		: (c) 2006-2014 by Virtual Reality Development Team. 
 *                    All Rights Reserved.
 * Website			: http://www.syndarveruleiki.is
 *
 * Product Name		: Virtual Reality
 * License Text     : packages/docs/VRLICENSE.txt
 * 
 * Planetary Info   : Information about the Planetary code
 * 
 * Copyright        : (c) 2014-2024 by Second Galaxy Development Team
 *                    All Rights Reserved.
 * 
 * Website          : http://www.secondgalaxy.com
 * 
 * Product Name     : Virtual Reality
 * License Text     : packages/docs/SGLICENSE.txt
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the WhiteCore-Sim Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
***************************************************************************/

#if !NETCF
#define HAS_READERWRITERLOCK
#if FRAMEWORK_4_0_OR_ABOVE
#define HAS_READERWRITERLOCKSLIM
#endif
#endif

using System;

namespace SmartInspect.Util
{
	/// <summary>
	/// Defines a lock that supports single writers and multiple readers
	/// </summary>
	/// <remarks>
	/// <para>
	/// <c>ReaderWriterLock</c> is used to synchronize access to a resource. 
	/// At any given time, it allows either concurrent read access for 
	/// multiple threads, or write access for a single thread. In a 
	/// situation where a resource is changed infrequently, a 
	/// <c>ReaderWriterLock</c> provides better throughput than a simple 
	/// one-at-a-time lock, such as <see cref="System.Threading.Monitor"/>.
	/// </para>
	/// <para>
	/// If a platform does not support a <c>System.Threading.ReaderWriterLock</c> 
	/// implementation then all readers and writers are serialized. Therefore 
	/// the caller must not rely on multiple simultaneous readers.
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class ReaderWriterLock
	{
		#region Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="ReaderWriterLock" /> class.
		/// </para>
		/// </remarks>
		public ReaderWriterLock()
		{

#if HAS_READERWRITERLOCK
#if HAS_READERWRITERLOCKSLIM
			m_lock = new System.Threading.ReaderWriterLockSlim();
#else
			m_lock = new System.Threading.ReaderWriterLock();
#endif
#endif
		}

		#endregion Private Instance Constructors
  
		#region Public Methods

		/// <summary>
		/// Acquires a reader lock
		/// </summary>
		/// <remarks>
		/// <para>
		/// <see cref="AcquireReaderLock"/> blocks if a different thread has the writer 
		/// lock, or if at least one thread is waiting for the writer lock.
		/// </para>
		/// </remarks>
		public void AcquireReaderLock()
		{
#if HAS_READERWRITERLOCK
#if HAS_READERWRITERLOCKSLIM
			m_lock.EnterReadLock();
#else
			m_lock.AcquireReaderLock(-1);
#endif
#else
			System.Threading.Monitor.Enter(this);
#endif
		}

		/// <summary>
		/// Decrements the lock count
		/// </summary>
		/// <remarks>
		/// <para>
		/// <see cref="ReleaseReaderLock"/> decrements the lock count. When the count 
		/// reaches zero, the lock is released.
		/// </para>
		/// </remarks>
		public void ReleaseReaderLock()
		{
#if HAS_READERWRITERLOCK
#if HAS_READERWRITERLOCKSLIM
			m_lock.ExitReadLock();
#else
			m_lock.ReleaseReaderLock();

#endif
#else
			System.Threading.Monitor.Exit(this);
#endif
		}

		/// <summary>
		/// Acquires the writer lock
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method blocks if another thread has a reader lock or writer lock.
		/// </para>
		/// </remarks>
		public void AcquireWriterLock()
		{
#if HAS_READERWRITERLOCK
#if HAS_READERWRITERLOCKSLIM
			m_lock.EnterWriteLock();
#else
			m_lock.AcquireWriterLock(-1);
#endif
#else
			System.Threading.Monitor.Enter(this);
#endif
		}

		/// <summary>
		/// Decrements the lock count on the writer lock
		/// </summary>
		/// <remarks>
		/// <para>
		/// ReleaseWriterLock decrements the writer lock count. 
		/// When the count reaches zero, the writer lock is released.
		/// </para>
		/// </remarks>
		public void ReleaseWriterLock()
		{
#if HAS_READERWRITERLOCK
#if HAS_READERWRITERLOCKSLIM
			m_lock.ExitWriteLock();
#else
			m_lock.ReleaseWriterLock();
#endif
#else
			System.Threading.Monitor.Exit(this);
#endif
		}

		#endregion Public Methods

		#region Private Members

#if HAS_READERWRITERLOCK
#if HAS_READERWRITERLOCKSLIM
		private System.Threading.ReaderWriterLockSlim m_lock;
#else
		private System.Threading.ReaderWriterLock m_lock;
#endif

#endif

		#endregion
	}
}