//-----------------------------------------------------------------------
// <copyright file="INavigable.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------

namespace XportBand.Model
{

    /// <summary>
    /// Interface to define Navigable ViewModel.
    /// </summary>
    public interface INavigable
    {

        #region Methods

        /// <summary>
        /// Handler when a ViewModel is activated.
        /// </summary>
        /// <param name="parameter">The data sent to view.</param>
        void Activate(object parameter);

        /// <summary>
        /// Handler when a ViewModel is deactivated.
        /// </summary>
        /// <param name="parameter">The data sent from view.</param>
        void Deactivate(object parameter);

        #endregion

    }

}
