//-----------------------------------------------------------------------
// <copyright file="MSHealthClient.Async.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Core
{
    using Contracts;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq;

    /// <summary>
    /// Client to consume Microsoft Health Cloud API.
    /// </summary>
    public sealed partial class MSHealthClient : IMSHealthClient
    {

        #region IMSHealthClient Async implementation

        public async Task<MSHealthRedirectResult> HandleRedirectAsync(Uri uri)
        {
            MSHealthRedirectResult loResult = MSHealthRedirectResult.None;
            // Check if URL has Authentication path
            if (uri != null &&
                uri.LocalPath.StartsWith(AUTH_PATH, StringComparison.OrdinalIgnoreCase))
            {
                HttpValueCollection loValues = HttpUtility.ParseQueryString(uri.Query);
                // Read Authentication Code
                HttpValue loCode = loValues.FirstOrDefault((entry) => entry.Key.Equals("code", StringComparison.OrdinalIgnoreCase));
                // Read Authentication Errors
                HttpValue loError = loValues.FirstOrDefault((entry) => entry.Key.Equals("error", StringComparison.OrdinalIgnoreCase));
                HttpValue loErrorDesc = loValues.FirstOrDefault((entry) => entry.Key.Equals("error_description", StringComparison.OrdinalIgnoreCase));
                // Check the code to see if this is sign-in or sign-out
                if (loCode != null)
                {
                    // Check error and throw Exception
                    if (loError != null)
                        throw new Exception(string.Format("{0}\r\n{1}", loError.Value, loErrorDesc.Value));
                    // Get Token
                    try
                    {
                        // Signed-in
                        Token = await GetTokenAsync(loCode.Value, false);
                        IsSignedIn = true;
                        loResult = MSHealthRedirectResult.SignIn;
                    }
                    catch
                    {
                        // Error
                        Token = null;
                        IsSignedIn = false;
                        loResult = MSHealthRedirectResult.Error;
                        throw;
                    }
                }
                else
                {
                    // Signed-out
                    Token = null;
                    IsSignedIn = false;
                    loResult = MSHealthRedirectResult.SignOut;
                }
            }
            return loResult;
        }

        public async Task<MSHealthActivities> ListActivitiesAsync(DateTime? startTime = default(DateTime?), DateTime? endTime = default(DateTime?), string ids = null, MSHealthActivityType type = MSHealthActivityType.Unknown, MSHealthActivityInclude include = MSHealthActivityInclude.None, string deviceIds = null, MSHealthSplitDistanceType splitDistanceType = MSHealthSplitDistanceType.None, int? maxPageSize = default(int?))
        {
            throw new NotImplementedException();
        }

        public async Task<MSHealthSummaries> ListDailySummariesAsync(DateTime? startTime = default(DateTime?), DateTime? endTime = default(DateTime?), string deviceIds = null, int? maxPageSize = default(int?))
        {
            throw new NotImplementedException();
        }

        public async Task<MSHealthSummaries> ListHourlySummariesAsync(DateTime? startTime = default(DateTime?), DateTime? endTime = default(DateTime?), string deviceIds = null, int? maxPageSize = default(int?))
        {
            throw new NotImplementedException();
        }

        public async Task<MSHealthDevices> ListDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<MSHealthActivity> ReadActivityAsync(string id, MSHealthActivityInclude include = MSHealthActivityInclude.None)
        {
            throw new NotImplementedException();
        }

        public async Task<MSHealthDevice> ReadDeviceAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<MSHealthProfile> ReadProfileAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RefreshTokenAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateTokenAsync(MSHealthToken token, bool refreshIfInvalid = true)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Async methods

        /// <summary>
        /// Gets Authentication Token for Microsoft Health Cloud API.
        /// </summary>
        /// <param name="code">Authentication Code or Refresh Token.</param>
        /// <param name="isRefresh">Flag to determine if is a Refresh Token request.</param>
        /// <returns><see cref="MSHealthToken"/> instance.</returns>
        private async Task<MSHealthToken> GetTokenAsync(string code, bool isRefresh)
        {
            MSHealthToken loToken = null;
            UriBuilder loUri = new UriBuilder(TOKEN_URI);
            StringBuilder loQuery = new StringBuilder();
            WebRequest loWebRequest;
            // Build base query
            loQuery.AppendFormat("redirect_uri={0}", Uri.EscapeDataString(REDIRECT_URI));
            loQuery.AppendFormat("&client_id={0}", Uri.EscapeDataString(msClientId));
            loQuery.AppendFormat("&client_secret={0}", Uri.EscapeDataString(msClientSecret));
            // Check if is refresh request
            if (isRefresh)
            {
                // Build refresh query
                loQuery.AppendFormat("&refresh_token={0}", Uri.EscapeDataString(code));
                loQuery.Append("&grant_type=refresh_token");
            }
            else
            {
                // Build new token query
                loQuery.AppendFormat("&code={0}", Uri.EscapeDataString(code));
                loQuery.Append("&grant_type=authorization_code");
            }
            // Prepare complete URL
            loUri.Query = loQuery.ToString();
            loWebRequest = WebRequest.Create(loUri.Uri);//HttpWebRequest.Create(loUri.Uri);
            try
            {
                // Perform request and handle response
                using (WebResponse loWebResponse = await loWebRequest.GetResponseAsync())
                {
                    using (Stream loResponseStream = loWebResponse.GetResponseStream())
                    {
                        using (StreamReader loStreamReader = new StreamReader(loResponseStream))
                        {
                            string lsResponse = loStreamReader.ReadToEnd();
                            // TODO: Parse JSON error
                            //JsonObject loJsonResponse = JsonObject.Parse(lsResponse);
                            //IJsonValue loJsonValue = null;
                            //string lsError = null;
                            //// Check for error
                            //if (loJsonResponse.TryGetValue("error", out loJsonValue) && loJsonValue != null)
                            //    lsError = loJsonValue.GetString();
                            //if (!string.IsNullOrEmpty(lsError))
                            //    throw new Exception(lsError);

                            // Deserialize Json response
                            loToken = JsonConvert.DeserializeObject<MSHealthToken>(lsResponse);
                            if (string.IsNullOrEmpty(loToken.RefreshToken))
                                loToken.RefreshToken = code;
                        }
                    }
                }
            }
            catch (Exception loException)
            {
                throw new MSHealthException(loException.Message, loException, loUri.Path, loUri.Query);
            }

            return loToken;
        }

        /// <summary>
        /// Perform general Microsoft Health API requests using <see cref="BASE_URI"/>.
        /// </summary>
        /// <param name="path">Path to resource to request.</param>
        /// <param name="query">Query to resource to request.</param>
        /// <returns><see cref="string"/> response to request (generally is a Json string).</returns>
        private async Task<string> PerformRequest(string path, string query = null)
        {
            string lsResponse = null;
            UriBuilder loUriBuilder = null;

            // Validate Token and Refresh if necessary
            if (moScope.HasFlag(MSHealthScope.OfflineAccess))
            {
                if (!(await ValidateTokenAsync(Token, true)))
                {
                    throw new ArgumentNullException("Token");
                }
            }
            // Prepare URL request
            loUriBuilder = new UriBuilder(BASE_URI);
            loUriBuilder.Path += path;
            loUriBuilder.Query = query;
            WebRequest loWebRequest = WebRequest.Create(loUriBuilder.Uri); //HttpWebRequest.Create(loUriBuilder.Uri);
            loWebRequest.Headers[HttpRequestHeader.Authorization] = string.Format("{0} {1}", Token.TokenType, Token.AccessToken);
            try
            {
                // Perform request and handle response
                using (WebResponse loWebResponse = await loWebRequest.GetResponseAsync())
                {
                    using (Stream loResponseStream = loWebResponse.GetResponseStream())
                    {
                        using (StreamReader loStreamReader = new StreamReader(loResponseStream))
                        {
                            // Get response as string
                            lsResponse = await loStreamReader.ReadToEndAsync();
                        }
                    }
                }
            }
            catch (Exception loException)
            {
                throw new MSHealthException(loException.Message, loException, path, query);
            }

            return lsResponse;
        }

        #endregion

    }

}
