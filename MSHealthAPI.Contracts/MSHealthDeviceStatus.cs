//-----------------------------------------------------------------------
// <copyright file="MSHealthDeviceStatus.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents the status device.
    /// </summary>
    public enum MSHealthDeviceStatus
    {
        /// <summary>
        /// Unknown status.
        /// </summary>
        Unknown,
        /// <summary>
        /// Device is Active.
        /// </summary>
        Active,
        /// <summary>
        /// Device is Inactive.
        /// </summary>
        Inactive,
    }

}
