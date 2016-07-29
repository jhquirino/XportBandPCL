//-----------------------------------------------------------------------
// <copyright file="MSHealthSummaries.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Represents details about the summaries associated with user's Microsoft Health profile.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthSummaries
    {

        #region Properties

        /// <summary>
        /// Gets or sets the collection of summaries.
        /// </summary>
        [JsonProperty(PropertyName = "summaries",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthSummary> Summaries { get; set; }

        /// <summary>
        /// Gets or sets the URI for the next page of data.
        /// </summary>
        [JsonProperty(PropertyName = "nextPage",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string NextPage { get; set; }

        /// <summary>
        /// Gets or sets the number of <see cref="Summaries"/> returned.
        /// </summary>
        [JsonProperty(PropertyName = "itemCount",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int ItemCount { get; set; }

        #endregion

    }

}
