//-----------------------------------------------------------------------
// <copyright file="MSHealthActivityType.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using System;

    /// <summary>
    /// Represents type of activity registered.
    /// </summary>
    [Flags]
    public enum MSHealthActivityType
    {
        /// <summary>
        /// Unknown activity.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Custom activity.
        /// </summary>
        Custom = 1,
        /// <summary>
        /// Custom exercise activity.
        /// </summary>
        CustomExercise = 2,
        /// <summary>
        /// Custom composite activity.
        /// </summary>
        CustomComposite = 4,
        /// <summary>
        /// Run activity.
        /// </summary>
        Run = 8,
        /// <summary>
        /// Sleep activity.
        /// </summary>
        Sleep = 16,
        /// <summary>
        /// Free play activity.
        /// </summary>
        FreePlay = 32,
        /// <summary>
        /// Guided workout activity.
        /// </summary>
        GuidedWorkout = 64,
        /// <summary>
        /// Bike activity.
        /// </summary>
        Bike = 128,
        /// <summary>
        /// Golf activity.
        /// </summary>
        Golf = 256,
        /// <summary>
        /// Regular exercise activity.
        /// </summary>
        RegularExercise = 512,
        /// <summary>
        /// Hike activity.
        /// </summary>
        Hike = 1024,
    }

}
