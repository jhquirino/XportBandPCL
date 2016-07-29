//-----------------------------------------------------------------------
// <copyright file="MSHealthMapPointType.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents type of map point in activity.-
    /// </summary>
    public enum MSHealthMapPointType
    {
        /// <summary>
        /// Unknown map point type.
        /// </summary>
        Unknown,
        /// <summary>
        /// Start of activity.
        /// </summary>
        Start,
        /// <summary>
        /// End of activity.
        /// </summary>
        End,
        /// <summary>
        /// Split of activity.
        /// </summary>
        Split,
        /// <summary>
        /// Waypoint of activity.
        /// </summary>
        Waypoint,
    }

}
