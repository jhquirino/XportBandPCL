//-----------------------------------------------------------------------
// <copyright file="MSHealthGPSPoint.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a segment segments associated with this activity.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthGPSPoint
    {

        #region Constants

        /// <summary>
        /// Factor to convert (divide by) latitud value returned to degrees.
        /// </summary>
        public const double LATITUDE_FACTOR = 10000000d;

        /// <summary>
        /// Factor to convert (divide by) longitude value returned to degrees.
        /// </summary>
        public const double LONGITUDE_FACTOR = 10000000d;

        /// <summary>
        /// Factor to convert (divide by) elevation values returned to meters.
        /// </summary>
        public const double ELEVATION_FACTOR = 100d;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the speed over ground for the GPS point in m/s * 100.
        /// </summary>
        [JsonProperty(PropertyName = "speedOverGround",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? SpeedOverGround { get; set; }

        /// <summary>
        /// Gets or sets the latitude for the GPS point in degrees * 10^7 (+ = North).
        /// </summary>
        [JsonProperty(PropertyName = "latitude",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude for the GPS point in degrees * 10^7 (+ = East).
        /// </summary>
        [JsonProperty(PropertyName = "longitude",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the elevation from mean sea level in m * 100.
        /// </summary>
        [JsonProperty(PropertyName = "elevationFromMeanSeaLevel",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? ElevationFromMeanSeaLevel { get; set; }

        /// <summary>
        /// Gets or sets the estimated horizontal error in m * 100.
        /// </summary>
        [JsonProperty(PropertyName = "estimatedHorizontalError",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? EstimatedHorizontalError { get; set; }

        /// <summary>
        /// Gets or sets the estimated vertical error in m * 100.
        /// </summary>
        [JsonProperty(PropertyName = "estimatedVerticalError",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public double? EstimatedVerticalError { get; set; }

        #endregion

    }

}
