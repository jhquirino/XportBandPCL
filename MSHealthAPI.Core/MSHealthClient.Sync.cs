//-----------------------------------------------------------------------
// <copyright file="MSHealthClient.Sync.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Core
{
    using Contracts;
    using System;

    /// <summary>
    /// Client to consume Microsoft Health Cloud API.
    /// </summary>
    public sealed partial class MSHealthClient : IMSHealthClient
    {

        #region IMSHealthClient Sync implementation

        public MSHealthRedirectResult HandleRedirect(Uri uri)
        {
            throw new NotImplementedException();
        }

        public MSHealthActivities ListActivities(DateTime? startTime = default(DateTime?), DateTime? endTime = default(DateTime?), string ids = null, MSHealthActivityType type = MSHealthActivityType.Unknown, MSHealthActivityInclude include = MSHealthActivityInclude.None, string deviceIds = null, MSHealthSplitDistanceType splitDistanceType = MSHealthSplitDistanceType.None, int? maxPageSize = default(int?))
        {
            throw new NotImplementedException();
        }

        public MSHealthSummaries ListDailySummaries(DateTime? startTime = default(DateTime?), DateTime? endTime = default(DateTime?), string deviceIds = null, int? maxPageSize = default(int?))
        {
            throw new NotImplementedException();
        }

        public MSHealthSummaries ListHourlySummaries(DateTime? startTime = default(DateTime?), DateTime? endTime = default(DateTime?), string deviceIds = null, int? maxPageSize = default(int?))
        {
            throw new NotImplementedException();
        }

        public MSHealthDevices ListDevices()
        {
            throw new NotImplementedException();
        }

        public MSHealthActivity ReadActivity(string id, MSHealthActivityInclude include = MSHealthActivityInclude.None)
        {
            throw new NotImplementedException();
        }

        public MSHealthDevice ReadDevice(string id)
        {
            throw new NotImplementedException();
        }

        public MSHealthProfile ReadProfile()
        {
            throw new NotImplementedException();
        }

        public bool RefreshToken()
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(MSHealthToken token, bool refreshIfInvalid = true)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
