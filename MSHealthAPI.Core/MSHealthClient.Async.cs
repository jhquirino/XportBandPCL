//-----------------------------------------------------------------------
// <copyright file="MSHealthClient.Async.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MSHealthAPI.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MSHealthAPI.Core
{

    /// <summary>
    /// Client to consume Microsoft Health Cloud API.
    /// </summary>
    public sealed partial class MSHealthClient : IMSHealthClient
    {

        #region IMSHealthClient Async implementation

        /// <summary>
        /// Handles redirect when accessing to Microsoft Health, to determine if 
        /// Sign-in/Sign-out process was successfull.
        /// </summary>
        /// <param name="uri">Redirect URL.</param>
        /// <returns><see cref="MSHealthRedirectResult"/> of Sign-in/Sign-out process.</returns>
        public async Task<MSHealthRedirectResult> HandleRedirectAsync(Uri uri)
        {
            MSHealthRedirectResult loResult = MSHealthRedirectResult.None;
            // Check if URL has Authentication path
            if (uri != null &&
                uri.LocalPath.StartsWith(AUTH_PATH, StringComparison.OrdinalIgnoreCase))
            {
                var loValues = HttpUtility.ParseQueryString(uri.Query);
                // Read Authentication Code
                var loCode = loValues.FirstOrDefault((entry) => entry.Key.Equals("code", StringComparison.OrdinalIgnoreCase));
                // Read Authentication Errors
                var loError = loValues.FirstOrDefault((entry) => entry.Key.Equals("error", StringComparison.OrdinalIgnoreCase));
                var loErrorDesc = loValues.FirstOrDefault((entry) => entry.Key.Equals("error_description", StringComparison.OrdinalIgnoreCase));
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

        /// <summary>
        /// Gets a list of activities, that match specified parameters, associated with this user's Microsoft Health profile.
        /// </summary>
        /// <param name="startTime">Filters the set of returned activities to those starting after the specified <see cref="DateTime"/>, inclusive.</param>
        /// <param name="endTime">Filters the set of returned activities to those starting before the specified <see cref="DateTime"/>, exclusive. </param>
        /// <param name="ids">The comma-separated list of activity ids to return.</param>
        /// <param name="type">The <see cref="MSHealthActivityType"/> to return (supports multi-values).</param>
        /// <param name="include">The <see cref="MSHealthActivityInclude"/> properties to return: Details, MinuteSummaries, MapPoints  (supports multi-values).</param>
        /// <param name="deviceIds">Filters the set of returned activities based on the comma-separated list of device ids provided.</param>
        /// <param name="splitDistanceType">The length of splits (<see cref="MSHealthSplitDistanceType"/>) used in each activity.</param>
        /// <param name="maxPageSize">The maximum number of entries to return per page.</param>
        /// <returns>Instance of <see cref="MSHealthActivities"/> with collection of activities that matched specified parameters.</returns>
        public async Task<MSHealthActivities> ListActivitiesAsync(DateTime? startTime = default(DateTime?), 
                                                                  DateTime? endTime = default(DateTime?), 
                                                                  string ids = null, 
                                                                  MSHealthActivityType type = MSHealthActivityType.Unknown, 
                                                                  MSHealthActivityInclude include = MSHealthActivityInclude.None, 
                                                                  string deviceIds = null, 
                                                                  MSHealthSplitDistanceType splitDistanceType = MSHealthSplitDistanceType.None, 
                                                                  int? maxPageSize = default(int?))
        {
            MSHealthActivities loActivities = null;
            var loQuery = new StringBuilder();
            string lsResponse;
            string lsParamValue;
            // Check StartTime, and append to query if applies
            if (startTime != null && startTime.HasValue)
                loQuery.AppendFormat("&startTime={0}", Uri.EscapeDataString(startTime.Value.ToUniversalTime().ToString("O")));
            // Check EndTime, and append to query if applies
            if (endTime != null && endTime.HasValue)
                loQuery.AppendFormat("&endTime={0}", Uri.EscapeDataString(endTime.Value.ToUniversalTime().ToString("O")));
            // Check ActivityIds, and append to query if applies
            if (!string.IsNullOrEmpty(ids))
                loQuery.AppendFormat("&activityIds={0}", Uri.EscapeDataString(ids));
            // Check ActivityTypes, and append to query if applies
            if (type != MSHealthActivityType.Unknown)
            {
                lsParamValue = string.Empty;
                if (type.HasFlag(MSHealthActivityType.Custom))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.Custom);
                if (type.HasFlag(MSHealthActivityType.CustomExercise))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.CustomExercise);
                if (type.HasFlag(MSHealthActivityType.CustomComposite))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.CustomComposite);
                if (type.HasFlag(MSHealthActivityType.Run))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.Run);
                if (type.HasFlag(MSHealthActivityType.Sleep))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.Sleep);
                if (type.HasFlag(MSHealthActivityType.FreePlay))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.FreePlay);
                if (type.HasFlag(MSHealthActivityType.GuidedWorkout))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.GuidedWorkout);
                if (type.HasFlag(MSHealthActivityType.Bike))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.Bike);
                if (type.HasFlag(MSHealthActivityType.Golf))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.Golf);
                if (type.HasFlag(MSHealthActivityType.RegularExercise))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.RegularExercise);
                if (type.HasFlag(MSHealthActivityType.Hike))
                    lsParamValue += string.Format(",{0}", MSHealthActivityType.Hike);
                lsParamValue = lsParamValue.TrimStart(new char[] { ',' });
                loQuery.AppendFormat("&activityTypes={0}", lsParamValue);
            }
            // Check ActivityIncludes, and append to query if applies
            if (include != MSHealthActivityInclude.None)
            {
                lsParamValue = string.Empty;
                if (include.HasFlag(MSHealthActivityInclude.Details))
                    lsParamValue += string.Format(",{0}", MSHealthActivityInclude.Details);
                if (include.HasFlag(MSHealthActivityInclude.MinuteSummaries))
                    lsParamValue += string.Format(",{0}", MSHealthActivityInclude.MinuteSummaries);
                if (include.HasFlag(MSHealthActivityInclude.MapPoints))
                    lsParamValue += string.Format(",{0}", MSHealthActivityInclude.MapPoints);
                lsParamValue = lsParamValue.TrimStart(new char[] { ',' });
                loQuery.AppendFormat("&activityIncludes={0}", lsParamValue);
            }
            // Check DeviceIds, and append to query if applies
            if (!string.IsNullOrEmpty(deviceIds))
                loQuery.AppendFormat("&deviceIds={0}", Uri.EscapeDataString(deviceIds));
            // Check SplitDistanceType, and append to query if applies
            switch (splitDistanceType)
            {
                case MSHealthSplitDistanceType.Mile:
                    loQuery.AppendFormat("&splitDistanceType={0}", MSHealthSplitDistanceType.Mile);
                    break;
                case MSHealthSplitDistanceType.Kilometer:
                    loQuery.AppendFormat("&splitDistanceType={0}", MSHealthSplitDistanceType.Kilometer);
                    break;
                //case MSHealthSplitDistanceType.None:
                //default:
                //    break;
            }
            // Check MaxPageSize, and append to query if applies
            if (maxPageSize != null && maxPageSize.HasValue && maxPageSize.Value > 0)
                loQuery.AppendFormat("&maxPageSize={0}", maxPageSize.Value);

            // Perform request using BASE_URI, ACTIVITIES_PATH and query string
            lsResponse = await PerformRequestAsync(ACTIVITIES_PATH, loQuery.ToString().TrimStart(new char[] { '&' }));
            // Deserialize Json response (use Converters for Enum, DateTime and TimeSpan values)
            var loSerializerSettings = new JsonSerializerSettings();
            loSerializerSettings.Converters.Add(new StringEnumConverter());
            loSerializerSettings.Converters.Add(new IsoDateTimeConverter());
            loSerializerSettings.Converters.Add(new TimeSpanConverter());
            loActivities = JsonConvert.DeserializeObject<MSHealthActivities>(lsResponse, loSerializerSettings);

            return loActivities;
        }

        /// <summary>
        /// Lists daily summary data for this user by date range.
        /// </summary>
        /// <param name="startTime">Filters the set of returned summaries to those starting after the specified <see cref="DateTime"/>, inclusive.</param>
        /// <param name="endTime">Filters the set of returned summaries to those starting before the specified <see cref="DateTime"/>, exclusive. </param>
        /// <param name="deviceIds">Filters the set of returned summaries based on the comma-separated list of device ids provided.</param>
        /// <param name="maxPageSize">The maximum number of entries to return per page. Defaults to 48 for hourly and 31 for daily.</param>
        /// <returns>Instance of <see cref="MSHealthSummaries"/> with summary details.</returns>
        public async Task<MSHealthSummaries> ListDailySummariesAsync(DateTime? startTime = default(DateTime?), DateTime? endTime = default(DateTime?), string deviceIds = null, int? maxPageSize = default(int?))
        {
            MSHealthSummaries loSummaries = null;
            var loQuery = new StringBuilder();
            string lsResponse;

            // Check StartTime, and append to query if applies
            if (startTime != null && startTime.HasValue)
                loQuery.AppendFormat("&startTime={0}", Uri.EscapeDataString(startTime.Value.ToUniversalTime().ToString("O")));
            // Check EndTime, and append to query if applies
            if (endTime != null && endTime.HasValue)
                loQuery.AppendFormat("&endTime={0}", Uri.EscapeDataString(endTime.Value.ToUniversalTime().ToString("O")));
            // Check DeviceIds, and append to query if applies
            if (!string.IsNullOrEmpty(deviceIds))
                loQuery.AppendFormat("&deviceIds={0}", Uri.EscapeDataString(deviceIds));
            // Check MaxPageSize, and append to query if applies
            if (maxPageSize != null && maxPageSize.HasValue && maxPageSize.Value > 0)
                loQuery.AppendFormat("&maxPageSize={0}", maxPageSize.Value);

            // Perform request using BASE_URI, SUMMARIES_DAILY_PATH and query string
            lsResponse = await PerformRequestAsync(SUMMARIES_DAILY_PATH, loQuery.ToString().TrimStart(new char[] { '&' }));
            // Deserialize Json response (use Converters for Enum, DateTime and TimeSpan values)
            var loSerializerSettings = new JsonSerializerSettings();
            loSerializerSettings.Converters.Add(new StringEnumConverter());
            loSerializerSettings.Converters.Add(new IsoDateTimeConverter());
            loSerializerSettings.Converters.Add(new TimeSpanConverter());
            loSummaries = JsonConvert.DeserializeObject<MSHealthSummaries>(lsResponse, loSerializerSettings);

            return loSummaries;
        }

        /// <summary>
        /// Lists hourly summary data for this user by date range.
        /// </summary>
        /// <param name="startTime">Filters the set of returned summaries to those starting after the specified <see cref="DateTime"/>, inclusive.</param>
        /// <param name="endTime">Filters the set of returned summaries to those starting before the specified <see cref="DateTime"/>, exclusive. </param>
        /// <param name="deviceIds">Filters the set of returned summaries based on the comma-separated list of device ids provided.</param>
        /// <param name="maxPageSize">The maximum number of entries to return per page. Defaults to 48 for hourly and 31 for daily.</param>
        /// <returns>Instance of <see cref="MSHealthSummaries"/> with summary details.</returns>
        public async Task<MSHealthSummaries> ListHourlySummariesAsync(DateTime? startTime = default(DateTime?), DateTime? endTime = default(DateTime?), string deviceIds = null, int? maxPageSize = default(int?))
        {
            MSHealthSummaries loSummaries = null;
            var loQuery = new StringBuilder();
            string lsResponse;

            // Check StartTime, and append to query if applies
            if (startTime != null && startTime.HasValue)
                loQuery.AppendFormat("&startTime={0}", Uri.EscapeDataString(startTime.Value.ToUniversalTime().ToString("O")));
            // Check EndTime, and append to query if applies
            if (endTime != null && endTime.HasValue)
                loQuery.AppendFormat("&endTime={0}", Uri.EscapeDataString(endTime.Value.ToUniversalTime().ToString("O")));
            // Check DeviceIds, and append to query if applies
            if (!string.IsNullOrEmpty(deviceIds))
                loQuery.AppendFormat("&deviceIds={0}", Uri.EscapeDataString(deviceIds));
            // Check MaxPageSize, and append to query if applies
            if (maxPageSize != null && maxPageSize.HasValue && maxPageSize.Value > 0)
                loQuery.AppendFormat("&maxPageSize={0}", maxPageSize.Value);

            // Perform request using BASE_URI, SUMMARIES_HOURLY_PATH and query string
            lsResponse = await PerformRequestAsync(SUMMARIES_HOURLY_PATH, loQuery.ToString().TrimStart(new char[] { '&' }));
            // Deserialize Json response (use Converters for Enum, DateTime and TimeSpan values)
            var loSerializerSettings = new JsonSerializerSettings();
            loSerializerSettings.Converters.Add(new StringEnumConverter());
            loSerializerSettings.Converters.Add(new IsoDateTimeConverter());
            loSerializerSettings.Converters.Add(new TimeSpanConverter());
            loSummaries = JsonConvert.DeserializeObject<MSHealthSummaries>(lsResponse, loSerializerSettings);

            return loSummaries;
        }

        /// <summary>
        /// Get the details about the devices associated with this user's Microsoft Health profile.
        /// </summary>
        /// <returns>Instance of <see cref="MSHealthDevices"/> with devices details.</returns>
        public async Task<MSHealthDevices> ListDevicesAsync()
        {
            MSHealthDevices loDevices = null;
            // Perform request using BASE_URI and DEVICES_PATH
            string lsResponse = await PerformRequestAsync(DEVICES_PATH);
            // Deserialize Json response
            loDevices = JsonConvert.DeserializeObject<MSHealthDevices>(lsResponse, new StringEnumConverter());
            return loDevices;
        }

        /// <summary>
        /// Get the details of an activity by its id.
        /// </summary>
        /// <param name="id">The id of the activity to get.</param>
        /// <param name="include">The <see cref="MSHealthActivityInclude"/> properties to return: Details, MinuteSummaries, MapPoints  (supports multi-values).</param>
        /// <returns><see cref="MSHealthActivity"/> instance with activity details.</returns>
        public async Task<MSHealthActivity> ReadActivityAsync(string id, MSHealthActivityInclude include = MSHealthActivityInclude.None)
        {
            MSHealthActivity loActivity = null;
            var loQuery = new StringBuilder();
            string lsResponse;
            string lsParamValue;

            // Check ActivityIncludes, and append to query if applies
            if (include != MSHealthActivityInclude.None)
            {
                lsParamValue = string.Empty;
                if (include.HasFlag(MSHealthActivityInclude.Details))
                    lsParamValue += string.Format(",{0}", MSHealthActivityInclude.Details);
                if (include.HasFlag(MSHealthActivityInclude.MinuteSummaries))
                    lsParamValue += string.Format(",{0}", MSHealthActivityInclude.MinuteSummaries);
                if (include.HasFlag(MSHealthActivityInclude.MapPoints))
                    lsParamValue += string.Format(",{0}", MSHealthActivityInclude.MapPoints);
                lsParamValue = lsParamValue.TrimStart(new char[] { ',' });
                loQuery.AppendFormat("&activityIncludes={0}", lsParamValue);
            }
            // Perform request using BASE_URI, ACTIVITY_PATH, id and query string
            lsResponse = await PerformRequestAsync(string.Format(ACTIVITY_PATH, id), loQuery.ToString().TrimStart(new char[] { '&' }));
            // Deserialize Json response (use Converters for Enum, DateTime and TimeSpan values)
            var loSerializerSettings = new JsonSerializerSettings();
            loSerializerSettings.Converters.Add(new StringEnumConverter());
            loSerializerSettings.Converters.Add(new IsoDateTimeConverter());
            loSerializerSettings.Converters.Add(new TimeSpanConverter());
            loActivity = JsonConvert.DeserializeObject<MSHealthActivity>(lsResponse, loSerializerSettings);

            return loActivity;
        }

        /// <summary>
        /// Get the details about the requested device associated with this user's Microsoft Health profile.
        /// </summary>
        /// <param name="id">The id of the device</param>
        /// <returns><see cref="MSHealthDevice"/> instance with device details.</returns>
        public async Task<MSHealthDevice> ReadDeviceAsync(string id)
        {
            MSHealthDevice loDevice = null;
            // Perform request using BASE_URI, DEVICE_PATH and id
            string lsResponse = await PerformRequestAsync(string.Format(DEVICE_PATH, id));
            // Deserialize Json response
            loDevice = JsonConvert.DeserializeObject<MSHealthDevice>(lsResponse, new StringEnumConverter());
            return loDevice;
        }

        /// <summary>
        /// Get the details about this user from their Microsoft Health profile.
        /// </summary>
        /// <returns><see cref="MSHealthProfile"/> instance with profile details.</returns>
        public async Task<MSHealthProfile> ReadProfileAsync()
        {
            MSHealthProfile loProfile = null;
            // Perform request using BASE_URI and PROFILE_PATH
            string lsResponse = await PerformRequestAsync(PROFILE_PATH);
            // Deserialize Json response
            loProfile = JsonConvert.DeserializeObject<MSHealthProfile>(lsResponse);
            return loProfile;
        }

        /// <summary>
        /// Refresh current <see cref="Token"/>.
        /// </summary>
        /// <returns><see langword="true"/> if refresh successfull, otherwise, <see langword="false"/>.</returns>
        public async Task<bool> RefreshTokenAsync()
        {
            bool lbRefresh = false;
            // Check current token
            if (Token != null)
            {
                // Get Token
                Token = await GetTokenAsync(Token.RefreshToken, true);
                lbRefresh = true;
            }
            return lbRefresh;
        }

        /// <summary>
        /// Verifies <paramref name="token"/>instance validity (<see cref="MSHealthToken.ExpirationTime"/>),
        /// replaces current <see cref="Token"/> and if <paramref name="refreshIfInvalid"/>
        /// is <see langword="true"/>, calls <see cref="RefreshTokenAsync"/>.
        /// </summary>
        /// <param name="token">Instance of <see cref="MSHealthToken"/> to validate.</param>
        /// <param name="refreshIfInvalid">Flag to enforce Token refresh if is not valid.</param>
        /// <returns><see langword="true"/> if specified token is valid or has been refresh successfull, otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// It only wworks if <see cref="MSHealthToken.RefreshToken"/> is available, to obtain it,
        /// it's necessary to set use <see cref="MSHealthScope.OfflineAccess"/>.
        /// </remarks>
        public async Task<bool> ValidateTokenAsync(MSHealthToken token, bool refreshIfInvalid = true)
        {
            bool lbValid = false;
            // Check input token
            if (token != null)
            {
                // Check instance token
                if (Token != null)
                {
                    // Compare AccessToken for input vs instance
                    if (!string.IsNullOrEmpty(token.AccessToken) &&
                        !token.AccessToken.Equals(Token.AccessToken, StringComparison.OrdinalIgnoreCase))
                    {
                        // Different AccessToken, so, asign input to instance
                        Token = token;
                    }
                }
                else
                {
                    // No instance token, so, asign input
                    Token = token;
                }
                // Check Token's ExpirationTime
                if (Token.ExpirationTime.CompareTo(DateTime.Now) <= 0)
                {
                    // Already Expired, so, refresh Token
                    if (refreshIfInvalid)
                        lbValid = await RefreshTokenAsync();
                }
                else
                {
                    lbValid = true;
                }
            }
            return lbValid;
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
            var loUri = new UriBuilder(TOKEN_URI);
            var loQuery = new StringBuilder();
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
                            var lsResponse = loStreamReader.ReadToEnd();
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
        private async Task<string> PerformRequestAsync(string path, string query = null)
        {
            string lsResponse = null;
            UriBuilder loUriBuilder = null;

            // Validate Token and Refresh if necessary
            if (moScope.HasFlag(MSHealthScope.OfflineAccess))
            {
                if (!(await ValidateTokenAsync(Token, true)))
                {
                    throw new ArgumentException("Invalid Microsoft Health Token.");
                }
            }
            // Prepare URL request
            loUriBuilder = new UriBuilder(BASE_URI);
            loUriBuilder.Path += path;
            loUriBuilder.Query = query;
            var loWebRequest = WebRequest.Create(loUriBuilder.Uri); //HttpWebRequest.Create(loUriBuilder.Uri);
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
