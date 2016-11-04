//-----------------------------------------------------------------------
// <copyright file="MSHealthScope.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Scopes (or access types) for different types of user data on Microsoft Health.
    /// </summary>
    [Flags]
    public enum MSHealthScope
    {
        /// <summary>
        /// No access.
        /// </summary>
        None = 0,
        /// <summary>
        /// Access to profile data.
        /// </summary>
        /// <remarks>
        /// Profile includes things like name, gender, weight, and age.
        /// Email address will not be shared.
        /// </remarks>
        ReadProfile = 1,
        /// <summary>
        /// Access to daily and historical activity information.
        /// </summary>
        /// <remarks>
        /// Activity history includes things like runs, workouts, sleep, and daily steps.
        /// </remarks>
        ReadActivityHistory = 2,
        /// <summary>
        /// Access information about the devices associated with used Microsoft Health account.
        /// </summary>
        ReadDevices = 4,
        /// <summary>
        /// Access location information for activities.
        /// </summary>
        ReadActivityLocation = 8,
        /// <summary>
        /// Receive a refresh token so it can work offline even when the user isn't active.
        /// </summary>
        OfflineAccess = 16,
        /// <summary>
        /// All previous access listed (except <see cref="MSHealthScope.None"/>).
        /// </summary>
        All = 32,
    }

}
