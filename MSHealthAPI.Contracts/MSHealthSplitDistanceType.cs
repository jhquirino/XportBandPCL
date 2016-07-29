//-----------------------------------------------------------------------
// <copyright file="MSHealthSplitDistanceType.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents the length of splits used in each activity.
    /// </summary>
    public enum MSHealthSplitDistanceType
    {
        /// <summary>
        /// No length.
        /// </summary>
        None,
        /// <summary>
        /// Distance length in miles.
        /// </summary>
        Mile,
        /// <summary>
        /// Distance length in kilometers.
        /// </summary>
        Kilometer,
    }

}
