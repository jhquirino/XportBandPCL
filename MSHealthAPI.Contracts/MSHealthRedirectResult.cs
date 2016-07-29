//-----------------------------------------------------------------------
// <copyright file="MSHealthRedirectResult.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    /// <summary>
    /// Result for Redirect on Sign-in/Sign-out process..
    /// </summary>
    public enum MSHealthRedirectResult
    {
        /// <summary>
        /// No relevant redirect was handled.
        /// </summary>
        None,
        /// <summary>
        /// Redirect successfully on Sing-in request.
        /// </summary>
        SignIn,
        /// <summary>
        /// Redirect successfully on Sing-out request.
        /// </summary>
        SignOut,
        /// <summary>
        /// Redirect failed to handle request.
        /// </summary>
        Error,
    }
}
