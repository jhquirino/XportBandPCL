//-----------------------------------------------------------------------
// <copyright file="MSHealthHeartRateSummary.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents the summary of heart rate data during a period.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthHeartRateSummary
    {

        #region Properties

        /// <summary>
        /// Gets or sets the length of the time bucket for which the summary is calculated.
        /// </summary>
        [JsonProperty(PropertyName = "period",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthPeriod Period { get; set; }

        /// <summary>
        /// Gets or sets the average heart rate achieved during the period.
        /// </summary>
        [JsonProperty(PropertyName = "averageHeartRate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? AverageHeartRate { get; set; }

        /// <summary>
        /// Gets or sets the peak heart rate achieved during the period.
        /// </summary>
        [JsonProperty(PropertyName = "peakHeartRate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? PeakHeartRate { get; set; }

        /// <summary>
        /// Gets or sets the lowest heart rate achieved during the period.
        /// </summary>
        [JsonProperty(PropertyName = "lowestHeartRate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? LowestHeartRate { get; set; }

        #endregion

    }

}
