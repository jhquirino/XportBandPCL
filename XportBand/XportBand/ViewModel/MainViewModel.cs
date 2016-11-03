//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using MSHealthAPI.Contracts;

namespace XportBand.ViewModel
{

    /// <summary>
    /// ViewModel for Main.
    /// </summary>
    /// <seealso cref="ViewModelBase" />
    public class MainViewModel : ViewModelBase
    {

        #region Inner members

        /// <summary>
        /// Dialog service instance.
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Navigation service instance.
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Microsoft Health service instance.
        /// </summary>
        private readonly IMSHealthClient _msHealthClient;

        /// <summary>
        /// Backing store for <see cref="ListActivitiesCommand"/> property.
        /// </summary>
        private ICommand _listActivitiesCommand;

        #endregion

        #region Commands

        /// <summary>
        /// Gets the command to 'List Activities'.
        /// </summary>
        public ICommand ListActivitiesCommand
        {
            get { return _listActivitiesCommand ?? (_listActivitiesCommand = new RelayCommand(ListActivities)); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="dialogService">Dialog service instance.</param>
        /// <param name="navigationService">Navigation service instance.</param>
        /// <param name="msHealthClient">Microsoft Health service instance.</param>
        public MainViewModel(IDialogService dialogService, INavigationService navigationService, IMSHealthClient msHealthClient)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _msHealthClient = msHealthClient;
        }

        #endregion

        #region Commands Execution

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        private async void ListActivities()
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                var activities = await _msHealthClient.ListActivitiesAsync();
                await _dialogService.ShowMessage($"{activities.ItemCount:N0} activities found.", "Activities");
            }
            catch (Exception exception)
            {
                Serilog.Log.ForContext<MainViewModel>().Error(exception, "Error while reading MSHealth Activities.");
                await _dialogService.ShowMessage(exception.Message, "Error");
            }
        }

        #endregion

    }

}
