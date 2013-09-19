﻿/*
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
    internal interface IScheduler
    {
        /// <summary>
        ///     Fire this scheduler only when the region has
        ///     a user in it.
        /// </summary>
        bool IfOccupied { get; set; }

        /// <summary>
        ///     Fire this only when simulator performance
        ///     is reasonable. (eg sysload &lt;= 1.0)
        /// </summary>
        bool IfHealthy { get; set; }

        /// <summary>
        ///     Fire this event only when the region is visible
        ///     to a child agent, or there is a full agent
        ///     in this region.
        /// </summary>
        bool IfVisible { get; set; }

        /// <summary>
        ///     Determines whether this runs in the master scheduler thread, or a new thread
        ///     is spawned to handle your request. Running in scheduler may mean that your
        ///     code does not execute perfectly on time, however will result in better
        ///     region performance.
        /// </summary>
        /// <remarks>
        ///     Default: true
        /// </remarks>
        bool Schedule { get; set; }

        /// <summary>
        ///     Schedule an event callback to occur
        ///     when 'time' is elapsed.
        /// </summary>
        /// <param name="time">The period to wait before executing</param>
        void RunIn(TimeSpan time);

        /// <summary>
        ///     Schedule an event callback to fire
        ///     every "time". Equivilent to a repeating
        ///     timer.
        /// </summary>
        /// <param name="time">The period to wait between executions</param>
        void RunAndRepeat(TimeSpan time);
    }
}