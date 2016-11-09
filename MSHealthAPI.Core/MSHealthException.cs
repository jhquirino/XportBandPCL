//-----------------------------------------------------------------------
// <copyright file="MSHealthException.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents error that occur during <see cref="IMSHealthClient"/> operations execution.
    /// </summary>
    /// <seealso cref="Exception" />
    public sealed class MSHealthException : Exception
    {

        #region Properties

        /// <summary>
        /// Gets the error response for request.
        /// </summary>
        public MSHealthError Error { get; private set; }

        /// <summary>
        /// Gets the error <see cref="System.Net.HttpWebResponse"/> for request.
        /// </summary>
        public HttpWebResponse HttpWebResponse { get; private set; }

        /// <summary>
        /// Gets the path for the request that raises the error.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the query for the request that raises the error.
        /// </summary>
        public string Query { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MSHealthException"/> class.
        /// </summary>
        /// <param name="message"><see cref="Exception.Message"/>.</param>
        /// <param name="innerException"><see cref="Exception.InnerException"/>.</param>
        /// <param name="path">Path for the request that raises the error.</param>
        /// <param name="query">Query for the request that raises the error.</param>
        public MSHealthException(string message, Exception innerException, string path, string query) :
            base(message, innerException)
        {
            Path = path;
            Query = query;
            // Check if inner exception is a WebException
            var loWebException = innerException as WebException;
            if (loWebException != null)
            {
                // Get WebResponse for inner Exception, and handle it
                if (loWebException.Response != null)
                {
                    HttpWebResponse = loWebException.Response as HttpWebResponse;
                    if (HttpWebResponse != null)
                        Serilog.Log.ForContext<MSHealthException>().Debug("StatusCode: {statusCode}", HttpWebResponse.StatusCode);
                    // Get response details
                    using (Stream loResponseStream = loWebException.Response.GetResponseStream())
                    {
                        using (StreamReader loStreamReader = new StreamReader(loResponseStream))
                        {
                            // Read response as string (must be a Json string)
                            var lsErrorResponse = loStreamReader.ReadToEnd();
                            if (HttpWebResponse != null)
                                Serilog.Log.ForContext<MSHealthException>().Verbose(lsErrorResponse);
                            if (!string.IsNullOrEmpty(lsErrorResponse))
                            {
                                // Deserialize response (Json)
                                var loSerializerSettings = new JsonSerializerSettings();
                                loSerializerSettings.Error = (sender, args) =>
                                {
                                    Serilog.Log.ForContext<MSHealthException>().Verbose(args.ErrorContext.Error.Message);
                                    args.ErrorContext.Handled = true;
                                };
                                Error = JsonConvert.DeserializeObject<MSHealthError>(lsErrorResponse, loSerializerSettings);
                            }
                        }
                    }
                }
            }
        }

        #endregion

    }

}
