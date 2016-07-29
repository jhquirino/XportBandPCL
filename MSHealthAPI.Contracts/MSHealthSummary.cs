//-----------------------------------------------------------------------
// <copyright file="MSHealthSummary.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Represents details about a summary.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthSummary
    {

        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        [JsonProperty(PropertyName = "userId",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the start time of the period.
        /// </summary>
        [JsonProperty(PropertyName = "startTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the period.
        /// </summary>
        [JsonProperty(PropertyName = "endTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the parent day of the period.
        /// </summary>
        [JsonProperty(PropertyName = "parentDay",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string ParentDay { get; set; }

        /// <summary>
        /// Gets or sets the is transit day.
        /// </summary>
        /// <remarks>
        /// <see langword="true"/> if the user transitioned time zones during this period, else <see langword="false"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "isTransitDay",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public bool? IsTransitDay { get; set; }

        /// <summary>
        /// Gets or sets the length of the time bucket for which the summary is calculated.
        /// </summary>
        [JsonProperty(PropertyName = "period",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthPeriod Period { get; set; }

        /// <summary>
        /// Gets or sets the duration of the period.
        /// </summary>
        [JsonProperty(PropertyName = "duration",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the steps during the period.
        /// </summary>
        [JsonProperty(PropertyName = "stepsTaken",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? Steps { get; set; }

        /// <summary>
        /// Gets or sets the active hours during the period.
        /// </summary>
        [JsonProperty(PropertyName = "activeHours",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? ActiveHours { get; set; }

        /// <summary>
        /// Gets or sets the UV exposure as time in the sun.
        /// </summary>
        [JsonProperty(PropertyName = "uvExposure",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string UVExposure { get; set; }

        /// <summary>
        /// Gets or sets the summary of the calories burned during the period.
        /// </summary>
        [JsonProperty(PropertyName = "caloriesBurnedSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthCaloriesBurnedSummary CaloriesBurnedSummary { get; set; }

        /// <summary>
        /// Gets or sets the heart rate data during the period.
        /// </summary>
        [JsonProperty(PropertyName = "heartRateSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthHeartRateSummary HeartRateSummary { get; set; }

        /// <summary>
        /// Gets or sets the summary of the distance data during the period.
        /// </summary>
        [JsonProperty(PropertyName = "distanceSummary",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthDistanceSummary DistanceSummary { get; set; }

        #endregion

    }

}
