//-----------------------------------------------------------------------
// <copyright file="MSHealthHeartRateZones.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Represents the mapping of the amount of time spent in a given heart rate zone during a segment.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthHeartRateZones
    {

        #region Properties

        /// <summary>
        /// Gets or sets the number of minutes where the HR was below 50% of the user’s max HR.
        /// </summary>
        [JsonProperty(PropertyName = "underHealthyHeart",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? UnderHealthyHeart { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes where the HR was below 50% of the user’s max HR.
        /// </summary>
        /// <remarks>
        /// This field is deprecated. The correct field name is now <see cref="UnderHealthyHeart"/>. 
        /// Populated for backwards compatibility until V2.
        /// </remarks>
        [JsonProperty(PropertyName = "underAerobic",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? UnderAerobic { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes where the HR was between 70-80% of the user’s max HR.
        /// </summary>
        [JsonProperty(PropertyName = "aerobic",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? Aerobic { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes where the HR was between 80-90% of the user’s max HR.
        /// </summary>
        [JsonProperty(PropertyName = "anaerobic",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? Anaerobic { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes where the HR was between 60-70% of the user’s max HR.
        /// </summary>
        [JsonProperty(PropertyName = "fitnessZone",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? FitnessZone { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes where the HR was between 50-60% of the user’s max HR.
        /// </summary>
        [JsonProperty(PropertyName = "healthyHeart",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? HealthyHeart { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes where the HR was between 90-100% of the user’s max HR.
        /// </summary>
        [JsonProperty(PropertyName = "redline",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? Redline { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes above the user’s max HR.
        /// </summary>
        [JsonProperty(PropertyName = "overRedline",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? OverRedline { get; set; }

        #endregion

    }

}
