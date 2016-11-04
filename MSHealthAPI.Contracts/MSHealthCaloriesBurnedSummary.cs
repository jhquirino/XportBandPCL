//-----------------------------------------------------------------------
// <copyright file="MSHealthCaloriesBurnedSummary.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents the summary of calories burned during a period.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthCaloriesBurnedSummary
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
        /// Gets or sets the total calories burned during the period.
        /// </summary>
        [JsonProperty(PropertyName = "totalCalories",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int? TotalCalories { get; set; }

        #endregion

    }

}
