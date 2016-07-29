//-----------------------------------------------------------------------
// <copyright file="MSHealthDistanceSummary.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the summary of distance data during a period.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthDistanceSummary
    {

        #region Constants

        /// <summary>
        /// Factor to convert (divide by) elevation values returned to meters.
        /// </summary>
        public const double ELEVATION_FACTOR = 100d;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the length of the time bucket for which the summary is calculated.
        /// </summary>
        [JsonProperty(PropertyName = "period",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthPeriod Period { get; set; }

        /// <summary>
        /// Gets or sets the total distance during the period.
        /// </summary>
        /// <remarks>
        /// If this is a time-based summary, e.g. hourly or daily, then this is the total 
        /// distance of the period. If this is an activity split summary, e.g. splits of a 
        /// run, then this is the split distance, e.g. 1 mile, 1 kilometer. For the last  
        /// split of the activity, this value may be less than the full split distance.
        /// </remarks>
        [JsonProperty(PropertyName = "totalDistance",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the total distance covered on foot during the period.
        /// </summary>
        [JsonProperty(PropertyName = "totalDistanceOnFoot",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? TotalDistanceOnFoot { get; set; }

        /// <summary>
        /// Gets or sets the absolute distance including any paused time distance during the period.
        /// </summary>
        [JsonProperty(PropertyName = "actualDistance",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? ActualDistance { get; set; }

        /// <summary>
        /// Gets or sets the cumulative elevation gain accrued during the period in cm.
        /// </summary>
        [JsonProperty(PropertyName = "elevationGain",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? ElevationGain { get; set; }

        /// <summary>
        /// Gets or sets the cumulative elevation loss accrued during this period in cm.
        /// </summary>
        [JsonProperty(PropertyName = "elevationLoss",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? ElevationLoss { get; set; }

        /// <summary>
        /// Gets or sets the maximum elevation during this period in cm.
        /// </summary>
        [JsonProperty(PropertyName = "maxElevation",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? MaxElevation { get; set; }

        /// <summary>
        /// Gets or sets the minimum elevation during this period in cm.
        /// </summary>
        [JsonProperty(PropertyName = "minElevation",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? MinElevation { get; set; }

        /// <summary>
        /// Gets or sets the distance in cm between recorded GPS points.
        /// </summary>
        [JsonProperty(PropertyName = "waypointDistance",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? WaypointDistance { get; set; }

        /// <summary>
        /// Gets or sets the average speed during the period.
        /// </summary>
        [JsonProperty(PropertyName = "speed",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? Speed { get; set; }

        /// <summary>
        /// Gets or sets the average pace during the period.
        /// </summary>
        [JsonProperty(PropertyName = "pace",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? Pace { get; set; }

        /// <summary>
        /// Gets or sets the total distance to the end of this period divided by total time to the end of this period.
        /// </summary>
        [JsonProperty(PropertyName = "overallPace",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? OverallPace { get; set; }

        #endregion

    }

}
