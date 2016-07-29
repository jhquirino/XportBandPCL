//-----------------------------------------------------------------------
// <copyright file="IMSHealthClient.Main.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
namespace MSHealthAPI.Contracts
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for Client to consume Microsoft Health Cloud API.
    /// </summary>
    public partial interface IMSHealthClient
    {

        #region Properties

        /// <summary>
        /// Gets a value indicating whether API Client is Signed-in to Microsoft Health.
        /// </summary>
        bool IsSignedIn { get; }

        /// <summary>
        /// Gets <see cref="MSHealthToken"/> instance
        /// </summary>
        MSHealthToken Token { get; }

        /// <summary>
        /// Gets URL to request Sign-in.
        /// </summary>
        Uri SignInUri { get; }

        /// <summary>
        /// Gets URL to request Sign-out.
        /// </summary>
        Uri SignOutUri { get; }

        #endregion

    }

}
