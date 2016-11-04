//-----------------------------------------------------------------------
// <copyright file="MSHealthActivityInclude.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents details of activity to include in requests to Microsoft Health Cloud API.
    /// </summary>
    [Flags]
    public enum MSHealthActivityInclude
    {
        /// <summary>
        /// No details
        /// </summary>
        None = 0,
        /// <summary>
        /// Include details.
        /// </summary>
        Details = 1,
        /// <summary>
        /// Include minute summaries.
        /// </summary>
        MinuteSummaries = 2,
        /// <summary>
        /// Include map points
        /// </summary>
        MapPoints = 4,
    }

}
