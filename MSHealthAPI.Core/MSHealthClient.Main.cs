//-----------------------------------------------------------------------
// <copyright file="MSHealthClient.Main.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using System.Text;
using MSHealthAPI.Contracts;

namespace MSHealthAPI.Core
{

    /// <summary>
    /// Client to consume Microsoft Health Cloud API.
    /// </summary>
    public sealed partial class MSHealthClient : IMSHealthClient
    {

        #region Constants

        /// <summary>
        /// Microsoft Health Cloud API - URL for Sign-in requests.
        /// </summary>
        private const string SIGNIN_URI = "https://login.live.com/oauth20_authorize.srf";

        /// <summary>
        /// Microsoft Health Cloud API - URL for Sign-out requests.
        /// </summary>
        private const string SIGNOUT_URI = "https://login.live.com/oauth20_logout.srf";

        /// <summary>
        /// Microsoft Health Cloud API - Base URL for requests.
        /// </summary>
        private const string BASE_URI = "https://api.microsofthealth.net";

        /// <summary>
        /// Microsoft Health Cloud API - URL for redirection response on authentication requests.
        /// </summary>
        private const string REDIRECT_URI = "https://login.live.com/oauth20_desktop.srf";

        /// <summary>
        /// Microsoft Health Cloud API - URL for Token requests.
        /// </summary>
        private const string TOKEN_URI = "https://login.live.com/oauth20_token.srf";

        /// <summary>
        /// Microsoft Health Cloud API - Path for authentication requests.
        /// </summary>
        private const string AUTH_PATH = "/oauth20_desktop.srf";

        /// <summary>
        /// Microsoft Health Cloud API - Read Profile Scope.
        /// </summary>
        private const string SCOPE_READ_PROFILE = "mshealth.ReadProfile";

        /// <summary>
        /// Microsoft Health Cloud API - Read Activity History Scope.
        /// </summary>
        private const string SCOPE_READ_ACTIVITY_HISTORY = "mshealth.ReadActivityHistory";

        /// <summary>
        /// Microsoft Health Cloud API - Read Devices Scope.
        /// </summary>
        private const string SCOPE_READ_DEVICES = "mshealth.ReadDevices";

        /// <summary>
        /// Microsoft Health Cloud API - Read Activity Location Scope.
        /// </summary>
        private const string SCOPE_READ_ACTIVITY_LOCATION = "mshealth.ReadActivityLocation";

        /// <summary>
        /// Microsoft Health Cloud API - Offline Access Scope.
        /// </summary>
        private const string SCOPE_OFFLINE_ACCESS = "offline_access";

        /// <summary>
        /// Microsoft Health Cloud API - Path for Profile Details.
        /// </summary>
        private const string PROFILE_PATH = "/v1/me/Profile";

        /// <summary>
        /// Microsoft Health Cloud API - Path for Devices Collection Details.
        /// </summary>
        private const string DEVICES_PATH = "/v1/me/Devices";

        /// <summary>
        /// Microsoft Health Cloud API - Path for Device Details.
        /// </summary>
        private const string DEVICE_PATH = "/v1/me/Devices/{0}";

        /// <summary>
        /// Microsoft Health Cloud API - Path for Daily Summaries Details.
        /// </summary>
        private const string SUMMARIES_DAILY_PATH = "/v1/me/Summaries/Daily";

        /// <summary>
        /// Microsoft Health Cloud API - Path for Hourly Summaries Details.
        /// </summary>
        private const string SUMMARIES_HOURLY_PATH = "/v1/me/Summaries/Hourly";

        /// <summary>
        /// Microsoft Health Cloud API - Path for Activities Collection Details.
        /// </summary>
        private const string ACTIVITIES_PATH = "/v1/me/Activities";

        /// <summary>
        /// Microsoft Health Cloud API - Path for Activity Details.
        /// </summary>
        private const string ACTIVITY_PATH = "/v1/me/Activities/{0}";

        #endregion

        #region Inner Members

        /// <summary>
        /// The Client ID for registerd app.
        /// </summary>
        private readonly string msClientId;

        /// <summary>
        /// The Client Secret for registerd app.
        /// </summary>
        private readonly string msClientSecret;

        /// <summary>
        /// The "list" of authorization scopes that app requires.
        /// </summary>
        private readonly MSHealthScope moScope;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether API Client is Signed-in to Microsoft Health.
        /// </summary>
        public bool IsSignedIn { get; private set; }

        /// <summary>
        /// Gets <see cref="MSHealthToken"/> instance
        /// </summary>
        public MSHealthToken Token { get; private set; }

        /// <summary>
        /// Gets URL to request Sign-in.
        /// </summary>
        public Uri SignInUri
        {
            get
            {
                var loUri = new UriBuilder(SIGNIN_URI);
                var loQuery = new StringBuilder();
                var lsScopes = string.Empty;
                // Build query
                loQuery.AppendFormat("redirect_uri={0}", Uri.EscapeDataString(REDIRECT_URI));
                loQuery.AppendFormat("&client_id={0}", Uri.EscapeDataString(msClientId));
                // Append required scopes
                if (moScope != MSHealthScope.None)
                {
                    if (moScope.HasFlag(MSHealthScope.ReadProfile) || moScope.HasFlag(MSHealthScope.All))
                        lsScopes += " " + SCOPE_READ_PROFILE;
                    if (moScope.HasFlag(MSHealthScope.ReadActivityHistory) || moScope.HasFlag(MSHealthScope.All))
                        lsScopes += " " + SCOPE_READ_ACTIVITY_HISTORY;
                    if (moScope.HasFlag(MSHealthScope.ReadDevices) || moScope.HasFlag(MSHealthScope.All))
                        lsScopes += " " + SCOPE_READ_DEVICES;
                    if (moScope.HasFlag(MSHealthScope.ReadActivityLocation) || moScope.HasFlag(MSHealthScope.All))
                        lsScopes += " " + SCOPE_READ_ACTIVITY_LOCATION;
                    if (moScope.HasFlag(MSHealthScope.OfflineAccess) || moScope.HasFlag(MSHealthScope.All))
                        lsScopes += " " + SCOPE_OFFLINE_ACCESS;
                    lsScopes = lsScopes.Trim();
                    loQuery.AppendFormat("&scope={0}", Uri.EscapeDataString(lsScopes));
                }
                loQuery.Append("&response_type=code");
                loUri.Query = loQuery.ToString();
                // Return URL
                return loUri.Uri;
            }
        }

        /// <summary>
        /// Gets URL to request Sign-out.
        /// </summary>
        public Uri SignOutUri
        {
            get
            {
                var loUri = new UriBuilder(SIGNOUT_URI);
                var loQuery = new StringBuilder();
                // Build query
                loQuery.AppendFormat("redirect_uri={0}", Uri.EscapeDataString(REDIRECT_URI));
                loQuery.AppendFormat("&client_id={0}", Uri.EscapeDataString(msClientId));
                loUri.Query = loQuery.ToString();
                // Return URL
                return loUri.Uri;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="MSHealthClient"/> class.
        /// </summary>
        /// <param name="clientId">The Client ID for registerd app.</param>
        /// <param name="clientSecret">The Client Secret for registerd app.</param>
        /// <param name="scope">The "list" of authorization scopes that app requires.</param>
        public MSHealthClient(string clientId, string clientSecret, MSHealthScope scope)
        {
            msClientId = clientId;
            msClientSecret = clientSecret;
            moScope = scope;
        }

        #endregion

    }

}
