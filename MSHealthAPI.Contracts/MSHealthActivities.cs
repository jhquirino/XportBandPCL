//-----------------------------------------------------------------------
// <copyright file="MSHealthActivities.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents details about the activities associated with users Microsoft Health profile.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthActivities
    {

        #region Properties

        /// <summary>
        /// Gets or sets the collection of bike activities.
        /// </summary>
        [JsonProperty(PropertyName = "bikeActivities",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthActivity> BikeActivities { get; set; }

        /// <summary>
        /// Gets or sets the collection of free play activities.
        /// </summary>
        [JsonProperty(PropertyName = "freePlayActivities",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthActivity> FreePlayActivities { get; set; }

        /// <summary>
        /// Gets or sets the collection of golf activities.
        /// </summary>
        [JsonProperty(PropertyName = "golfActivities",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthActivity> GolfActivities { get; set; }

        /// <summary>
        /// Gets or sets the collection of guided workout activities.
        /// </summary>
        [JsonProperty(PropertyName = "guidedWorkoutActivities",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthActivity> GuidedWorkoutActivities { get; set; }

        /// <summary>
        /// Gets or sets the collection of run activities.
        /// </summary>
        [JsonProperty(PropertyName = "runActivities",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthActivity> RunActivities { get; set; }

        /// <summary>
        /// Gets or sets the collection of sleep activities.
        /// </summary>
        [JsonProperty(PropertyName = "sleepActivities",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthActivity> SleepActivities { get; set; }

        /// <summary>
        /// Gets or sets the collection of hike activities.
        /// </summary>
        [JsonProperty(PropertyName = "hikeActivities",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthActivity> HikeActivities { get; set; }

        /// <summary>
        /// Gets or sets the URI for the next page of data.
        /// </summary>
        [JsonProperty(PropertyName = "nextPage",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string NextPage { get; set; }

        /// <summary>
        /// Gets or sets the number of activities returned.
        /// </summary>
        [JsonProperty(PropertyName = "itemCount",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int ItemCount { get; set; }

        #endregion

    }

}
