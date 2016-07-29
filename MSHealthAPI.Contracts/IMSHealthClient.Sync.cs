//-----------------------------------------------------------------------
// <copyright file="IMSHealthClient.Sync.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using System;

    /// <summary>
    /// Interface for Client to consume Microsoft Health Cloud API.
    /// </summary>
    public partial interface IMSHealthClient
    {

        #region Sync Methods

        /// <summary>
        /// Handles redirect when accessing to Microsoft Health, to determine if 
        /// Sign-in/Sign-out process was successfull.
        /// </summary>
        /// <param name="uri">Redirect URL.</param>
        /// <returns><see cref="MSHealthRedirectResult"/> of Sign-in/Sign-out process.</returns>
        MSHealthRedirectResult HandleRedirect(Uri uri);

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
        /// <param name="maxPageSize">The maximum number of entries to return per page. Defaults to 1000.</param>
        /// <returns>Instance of <see cref="MSHealthActivities"/> with collection of activities that matched specified parameters.</returns>
        MSHealthActivities ListActivities(DateTime? startTime = default(DateTime?),
                                          DateTime? endTime = default(DateTime?),
                                          string ids = null,
                                          MSHealthActivityType type = MSHealthActivityType.Unknown,
                                          MSHealthActivityInclude include = MSHealthActivityInclude.None,
                                          string deviceIds = null,
                                          MSHealthSplitDistanceType splitDistanceType = MSHealthSplitDistanceType.None,
                                          int? maxPageSize = default(int?));

        /// <summary>
        /// Lists daily summary data for this user by date range.
        /// </summary>
        /// <param name="startTime">Filters the set of returned summaries to those starting after the specified <see cref="DateTime"/>, inclusive.</param>
        /// <param name="endTime">Filters the set of returned summaries to those starting before the specified <see cref="DateTime"/>, exclusive. </param>
        /// <param name="deviceIds">Filters the set of returned summaries based on the comma-separated list of device ids provided.</param>
        /// <param name="maxPageSize">The maximum number of entries to return per page. Defaults to 48 for hourly and 31 for daily.</param>
        /// <returns></returns>
        MSHealthSummaries ListDailySummaries(DateTime? startTime = default(DateTime?),
                                             DateTime? endTime = default(DateTime?),
                                             string deviceIds = null,
                                             int? maxPageSize = default(int?));

        /// <summary>
        /// Lists hourly summary data for this user by date range.
        /// </summary>
        /// <param name="startTime">Filters the set of returned summaries to those starting after the specified <see cref="DateTime"/>, inclusive.</param>
        /// <param name="endTime">Filters the set of returned summaries to those starting before the specified <see cref="DateTime"/>, exclusive. </param>
        /// <param name="deviceIds">Filters the set of returned summaries based on the comma-separated list of device ids provided.</param>
        /// <param name="maxPageSize">The maximum number of entries to return per page. Defaults to 48 for hourly and 31 for daily.</param>
        /// <returns></returns>
        MSHealthSummaries ListHourlySummaries(DateTime? startTime = default(DateTime?),
                                              DateTime? endTime = default(DateTime?),
                                              string deviceIds = null,
                                              int? maxPageSize = default(int?));

        /// <summary>
        /// Get the details about the devices associated with this user's Microsoft Health profile.
        /// </summary>
        /// <returns>Instance of <see cref="MSHealthDevices"/> with devices details.</returns>
        MSHealthDevices ListDevices();

        /// <summary>
        /// Get the details of an activity by its id.
        /// </summary>
        /// <param name="id">The id of the activity to get.</param>
        /// <param name="include">The <see cref="MSHealthActivityInclude"/> properties to return: Details, MinuteSummaries, MapPoints  (supports multi-values).</param>
        /// <returns><see cref="MSHealthActivity"/> instance with activity details.</returns>
        MSHealthActivity ReadActivity(string id, MSHealthActivityInclude include = MSHealthActivityInclude.None);

        /// <summary>
        /// Get the details about the requested device associated with this user's Microsoft Health profile.
        /// </summary>
        /// <param name="id">The id of the device</param>
        /// <returns><see cref="MSHealthDevice"/> instance with device details.</returns>
        MSHealthDevice ReadDevice(string id);

        /// <summary>
        /// Get the details about this user from their Microsoft Health profile.
        /// </summary>
        /// <returns><see cref="MSHealthProfile"/> instance with profile details.</returns>
        MSHealthProfile ReadProfile();

        /// <summary>
        /// Refresh current <see cref="IMSHealthClient.Token"/>.
        /// </summary>
        /// <returns><see langword="true"/> if refresh successfull, otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// It only works if <see cref="MSHealthToken.RefreshToken"/> is available, to obtain it,
        /// it's necessary to set use <see cref="MSHealthScope.OfflineAccess"/>.
        /// </remarks>
        bool RefreshToken();

        /// <summary>
        /// Verifies <paramref name="token"/>instance validity (<see cref="MSHealthToken.ExpirationTime"/>),
        /// replaces current <see cref="IMSHealthClient.Token"/> and if <paramref name="refreshIfInvalid"/>
        /// is <see langword="true"/>, calls <see cref="IMSHealthClient.RefreshToken"/>.
        /// </summary>
        /// <param name="token">Instance of <see cref="MSHealthToken"/> to validate.</param>
        /// <param name="refreshIfInvalid">Flag to enforce Token refresh if is not valid.</param>
        /// <returns><see langword="true"/> if specified token is valid or has been refresh successfull, otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// It only works if <see cref="MSHealthToken.RefreshToken"/> is available, to obtain it,
        /// it's necessary to set use <see cref="MSHealthScope.OfflineAccess"/>.
        /// </remarks>
        bool ValidateToken(MSHealthToken token, bool refreshIfInvalid = true);

        #endregion

    }

}
