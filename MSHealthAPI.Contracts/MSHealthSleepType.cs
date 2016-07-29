//-----------------------------------------------------------------------
// <copyright file="MSHealthSleepType.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{

    /// <summary>
    /// Represents the sleep state.
    /// </summary>
    public enum MSHealthSleepType
    {
        /// <summary>
        /// Unknown sleep state.
        /// </summary>
        Unknown,
        /// <summary>
        /// Undifferentiated sleep state.
        /// </summary>
        UndifferentiatedSleep,
        /// <summary>
        /// Restless sleep state.
        /// </summary>
        RestlessSleep,
        /// <summary>
        /// Restful sleep state.
        /// </summary>
        RestfulSleep
    }

}
