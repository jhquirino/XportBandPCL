//-----------------------------------------------------------------------
// <copyright file="MSHealthDevices.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents details about the devices associated with user's Microsoft Health profile.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthDevices
    {

        #region Properties

        /// <summary>
        /// Gets or sets the collection of devices details.
        /// </summary>
        [JsonProperty(PropertyName = "deviceProfiles",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthDevice> Devices { get; set; }

        /// <summary>
        /// Gets or sets the number of <see cref="Devices"/> returned.
        /// </summary>
        [JsonProperty(PropertyName = "itemCount",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int ItemCount { get; set; }

        #endregion

    }

}
