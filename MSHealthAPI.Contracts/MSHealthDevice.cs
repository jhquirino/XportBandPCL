//-----------------------------------------------------------------------
// <copyright file="MSHealthDevice.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using Newtonsoft.Json;

namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents details about device.
    /// </summary>
    [JsonObject]
    public sealed class MSHealthDevice
    {

        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier of the device.
        /// </summary>
        [JsonProperty(PropertyName = "id",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the name the user has given the device.
        /// </summary>
        /// <remarks>
        /// Not available in the Developer Preview.
        /// </remarks>
        [JsonProperty(PropertyName = "displayName",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the date the device was last synced with the service.
        /// </summary>
        /// <remarks>
        /// Not available in the Developer Preview.
        /// </remarks>
        [JsonProperty(PropertyName = "lastSuccessfulSync",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime LastSuccessfulSync { get; set; }

        /// <summary>
        /// Gets or sets the family.
        /// </summary>
        [JsonProperty(PropertyName = "deviceFamily",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthDeviceFamily Family { get; set; }

        /// <summary>
        /// Gets or sets the device's hardware version.
        /// </summary>
        [JsonProperty(PropertyName = "hardwareVersion",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string HardwareVersion { get; set; }

        /// <summary>
        /// Gets or sets the device's software version.
        /// </summary>
        [JsonProperty(PropertyName = "softwareVersion",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// Gets or sets the name of the model of the device.
        /// </summary>
        [JsonProperty(PropertyName = "modelName",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer of the device.
        /// </summary>
        [JsonProperty(PropertyName = "manufacturer",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the current status of the device.
        /// </summary>
        [JsonProperty(PropertyName = "deviceStatus",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public MSHealthDeviceStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the date the device was first registered.
        /// </summary>
        /// <remarks>
        /// Not available in the Developer Preview.
        /// </remarks>
        [JsonProperty(PropertyName = "createdDate",
                      NullValueHandling = NullValueHandling.Ignore,
                      Required = Required.Default)]
        public DateTime? CreatedDate { get; set; }

        #endregion

    }
    
}
