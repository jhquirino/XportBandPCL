//-----------------------------------------------------------------------
// <copyright file="MSHealthProfile.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// General profile of the person using Microsoft Health.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthProfile
    {

        #region Properties

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [JsonProperty(PropertyName = "firstName",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's middle name.
        /// </summary>
        [JsonProperty(PropertyName = "middleName",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [JsonProperty(PropertyName = "lastName",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the last update time of the user's profile record.
        /// </summary>
        [JsonProperty(PropertyName = "lastUpdateTime",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// Gets or sets the user's birth date.
        /// </summary>
        [JsonProperty(PropertyName = "birthdate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Gets or sets the user's postal code.
        /// </summary>
        [JsonProperty(PropertyName = "postalCode",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the user's gender.
        /// </summary>
        [JsonProperty(PropertyName = "gender",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the user's current height.
        /// </summary>
        [JsonProperty(PropertyName = "height",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the user's current weight.
        /// </summary>
        [JsonProperty(PropertyName = "weight",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets the user's preferred locale.
        /// </summary>
        [JsonProperty(PropertyName = "preferredLocale",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string PreferredLocale { get; set; }

        #endregion

    }

}
