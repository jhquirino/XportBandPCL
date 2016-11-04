//-----------------------------------------------------------------------
// <copyright file="MSHealthPerformanceSummary.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents the performance summary data during a period.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthPerformanceSummary
    {

        #region Properties

        /// <summary>
        /// Gets or sets the heart rate when the user finished the exercise.
        /// </summary>
        [JsonProperty(PropertyName = "finishHeartRate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? FinishHeartRate { get; set; }

        /// <summary>
        /// Gets or sets the heart rate one minute after the user finished the exercise.
        /// </summary>
        [JsonProperty(PropertyName = "recoveryHeartRateAt1Minute",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? RecoveryHeartRateAt1Minute { get; set; }

        /// <summary>
        /// Gets or sets the heart rate two minutes after the user finished the exercise.
        /// </summary>
        [JsonProperty(PropertyName = "recoveryHeartRateAt2Minutes",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? RecoveryHeartRateAt2Minutes { get; set; }

        /// <summary>
        /// Gets or sets the breakdown of the heart rate zones during the exercise.
        /// </summary>
        [JsonProperty(PropertyName = "heartRateZones",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthHeartRateZones HeartRateZones { get; set; }

        #endregion

    }

}
