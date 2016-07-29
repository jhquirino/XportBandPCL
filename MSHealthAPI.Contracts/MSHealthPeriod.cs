//-----------------------------------------------------------------------
// <copyright file="MSHealthPeriod.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents the length of the time bucket for which a summary is calculated.
    /// </summary>
    public enum MSHealthPeriod
    {
        /// <summary>
        /// Unknown period.
        /// </summary>
        Unknown,
        /// <summary>
        /// Activity period
        /// </summary>
        Activity,
        /// <summary>
        /// Minute period.
        /// </summary>
        Minute,
        /// <summary>
        /// Quarter-hourly period.
        /// </summary>
        QuarterHourly,
        /// <summary>
        /// Half-hourly period.
        /// </summary>
        HalfHourly,
        /// <summary>
        /// Hourly period.
        /// </summary>
        Hourly,
        /// <summary>
        /// Daily period.
        /// </summary>
        Daily,
        /// <summary>
        /// Weekly period.
        /// </summary>
        Weekly,
        /// <summary>
        /// Last 30 days period.
        /// </summary>
        Last30Days,
        /// <summary>
        /// Calendar month period.
        /// </summary>
        CalendarMonth,
        /// <summary>
        /// Last 90 days period.
        /// </summary>
        Last90Days,
        /// <summary>
        /// Calendar year period.
        /// </summary>
        CalendarYear,
        /// <summary>
        /// Last 365 days period.
        /// </summary>
        Last365Days,
        /// <summary>
        /// Segment period.
        /// </summary>
        Segment,
    }

}
