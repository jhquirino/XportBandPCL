//-----------------------------------------------------------------------
// <copyright file="MSHealthDeviceFamily.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents the device family.
    /// </summary>
    public enum MSHealthDeviceFamily
    {
        /// <summary>
        /// Unknown family device.
        /// </summary>
        Unknown,
        /// <summary>
        /// Microsoft Band device.
        /// </summary>
        Band,
        /// <summary>
        /// Windows device.
        /// </summary>
        Windows,
        /// <summary>
        /// Android device.
        /// </summary>
        Android,
        /// <summary>
        /// iOS device.
        /// </summary>
        IOS,
    }

}
