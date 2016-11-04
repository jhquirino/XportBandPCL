//-----------------------------------------------------------------------
// <copyright file="MSHealthError.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    #region MSHealthError class

    /// <summary>
    /// Represents error reponse to Microsoft Health Cloud API requests.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthError
    {

        #region Properties

        /// <summary>
        /// Gets or sets the error information.
        /// </summary>
        [JsonProperty(PropertyName = "error",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthErrorInformation Error { get; set; }

        #endregion

    }

    #endregion

    #region MSHealthErrorInformation class

    /// <summary>
    /// Represents error information to Microsoft Health Cloud API requests.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthErrorInformation
    {

        #region Properties

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonProperty(PropertyName = "code",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonProperty(PropertyName = "message",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the error target.
        /// </summary>
        [JsonProperty(PropertyName = "target",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the error details.
        /// </summary>
        [JsonProperty(PropertyName = "details",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default,
                      ItemIsReference = true)]
        public IList<MSHealthErrorInformation> Details { get; set; }

        /// <summary>
        /// Gets or sets the inner error.
        /// </summary>
        [JsonProperty(PropertyName = "innererror",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthErrorInformation InnerError { get; set; }

        #endregion

    }

    #endregion

}
