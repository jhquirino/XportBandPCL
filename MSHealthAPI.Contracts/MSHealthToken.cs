//-----------------------------------------------------------------------
// <copyright file="MSHealthToken.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Authentication Token to Microsoft Health Cloud API.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthToken
    {

        #region Constants

        /// <summary>
        /// Default token type.
        /// </summary>
        public const string TOKEN_TYPE = "bearer";

        #endregion

        #region Properties

        /// <summary>
        /// Authorization type: "Bearer" in this case.
        /// </summary>
        [JsonProperty(PropertyName = "token_type",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string TokenType { get; set; } = TOKEN_TYPE;

        /// <summary>
        /// The amount of time in seconds when the access token is valid.
        /// </summary>
        /// <remarks>
        /// You can request a new access token by using the refresh token (if available),
        /// or by repeating the authentication request from the beginning.
        /// </remarks>
        [JsonProperty(PropertyName = "expires_in",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public long ExpiresIn { get; set; }

        /// <summary>
        /// A space-separated list of scopes that your app requires.
        /// </summary>
        [JsonProperty(PropertyName = "scope",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string Scope { get; set; }

        /// <summary>
        /// Access token to authenticate against Microsoft Health Cloud APIs
        /// </summary>
        [JsonProperty(PropertyName = "access_token",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string AccessToken { get; set; }

        /// <summary>
        /// The refresh token received previously.
        /// </summary>
        [JsonProperty(PropertyName = "refresh_token",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Time when current access token was created.
        /// </summary>
        [JsonIgnore]
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Expected Time when current access token expires.
        /// </summary>
        /// <remarks>
        /// This value is calculated using <see cref="CreationTime"/> and
        /// <see cref="ExpiresIn"/> values.
        /// </remarks>
        [JsonIgnore]
        public DateTime ExpirationTime
        {
            get { return CreationTime.AddSeconds(ExpiresIn); }
        }

        #endregion

        //#region Constructors

        ///// <summary>
        ///// Initializes a new instance of <see cref="MSHealthToken"/> class.
        ///// </summary>
        //public MSHealthToken()
        //{
        //    TokenType = TOKEN_TYPE;
        //    CreationTime = DateTime.Now;
        //}

        //#endregion

    }

}
