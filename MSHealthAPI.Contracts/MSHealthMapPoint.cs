//-----------------------------------------------------------------------
// <copyright file="MSHealthMapPoint.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents map point details for activity.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthMapPoint
    {

        #region Properties

        /// <summary>
        /// Gets or sets the number of seconds that have elapsed since mapping began, typically the start of a run or other activity.
        /// </summary>
        [JsonProperty(PropertyName = "secondsSinceStart",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? SecondsSinceStart { get; set; }

        /// <summary>
        /// Gets or sets the type of map point.
        /// </summary>
        [JsonProperty(PropertyName = "mapPointType",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthMapPointType Type { get; set; }

        /// <summary>
        /// Gets or sets the absolute ordering of this point relative to the others in its set, starting from 0.
        /// </summary>
        [JsonProperty(PropertyName = "ordinal",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public long? Ordinal { get; set; }

        /// <summary>
        /// Gets or sets the distance not including distance traveled while paused, 
        /// it is the distance that splits are based off of, since splits ignore 
        /// paused time.
        /// </summary>
        [JsonProperty(PropertyName = "actualDistance",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? ActualDistance { get; set; }

        /// <summary>
        /// Gets or sets the total distance from the start point to this map point.
        /// </summary>
        [JsonProperty(PropertyName = "totalDistance",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the heart rate at the time of this map point.
        /// </summary>
        [JsonProperty(PropertyName = "heartRate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? HeartRate { get; set; }

        /// <summary>
        /// Gets or sets the pace.
        /// </summary>
        [JsonProperty(PropertyName = "pace",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? Pace { get; set; }

        /// <summary>
        /// Gets or sets a number between 0 and 100 that denotes the pace/speed between the 
        /// slowest and fastest instantaneous pace for the overall route.
        /// </summary>
        /// <remarks>
        /// Slowest segment in the route (highest pace, lowest speed) is 0 and fastest segment 
        /// (lowest pace, highest speed) is 100. Only makes sense in the context of the set of 
        /// all map points.
        /// </remarks>
        [JsonProperty(PropertyName = "scaledPace",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? ScaledPace { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        [JsonProperty(PropertyName = "speed",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? Speed { get; set; }

        /// <summary>
        /// Gets or sets the GPS location for this map point.
        /// </summary>
        [JsonProperty(PropertyName = "location",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthGPSPoint Location { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not this map point occurred during paused time.
        /// </summary>
        [JsonProperty(PropertyName = "isPaused",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public bool? IsPaused { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not this map point is the first one since the activity resumed.
        /// </summary>
        [JsonProperty(PropertyName = "isResume",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public bool? IsResume { get; set; }

        #endregion

    }

}
