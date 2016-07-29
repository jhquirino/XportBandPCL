//-----------------------------------------------------------------------
// <copyright file="MSHealthSegmentType.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents the segment type of an activity.
    /// </summary>
    public enum MSHealthSegmentType
    {
        /// <summary>
        /// Unknown segment type.
        /// </summary>
        Unknown,
        /// <summary>
        /// Run segment type.
        /// </summary>
        Run,
        /// <summary>
        /// Free play segment type.
        /// </summary>
        FreePlay,
        /// <summary>
        /// Doze segment type.
        /// </summary>
        Doze,
        /// <summary>
        /// Sleep segment type.
        /// </summary>
        Sleep,
        /// <summary>
        /// Snooze segment type.
        /// </summary>
        Snooze,
        /// <summary>
        /// Awake segment type.
        /// </summary>
        Awake,
        /// <summary>
        /// Guided workout segment type.
        /// </summary>
        GuidedWorkout,
        /// <summary>
        /// Bike segment type.
        /// </summary>
        Bike,
        /// <summary>
        /// Pause segment type.
        /// </summary>
        Pause,
        /// <summary>
        /// Resume segment type.
        /// </summary>
        Resume,
        /// <summary>
        /// Distance based interval segment type.
        /// </summary>
        DistanceBasedInterval,
        /// <summary>
        /// Time based interval segment type.
        /// </summary>
        TimeBasedInterval,
        /// <summary>
        /// Golf hole segment type.
        /// </summary>
        GolfHole,
        /// <summary>
        /// Golf shot segment type.
        /// </summary>
        GolfShot,
        /// <summary>
        /// Not worn segment type.
        /// </summary>
        NotWorn,
        /// <summary>
        /// Hike segment type.
        /// </summary>
        Hike,
    }

}
