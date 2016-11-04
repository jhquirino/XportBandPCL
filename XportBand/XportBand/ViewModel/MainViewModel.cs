//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.ObjectModel;
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
        /// Inner member for <see cref="Activities"/> property.
        /// </summary>
        private ObservableCollection<MSHealthActivity> _activities;

        /// <summary>
        /// Backing store for <see cref="ListActivitiesCommand"/> property.
        /// </summary>
        private ICommand _listActivitiesCommand;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of Activities found by specified criteria.
        /// </summary>
        public ObservableCollection<MSHealthActivity> Activities
        {
            get { return _activities ?? (_activities = new ObservableCollection<MSHealthActivity>()); }
            set { Set(() => Activities, ref _activities, value); }
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Potential Code Quality Issues", "RECS0165:Asynchronous methods should return a Task instead of void", Justification = "Command actions must return void")]
        private async void ListActivities()
        {
            try
            {
                Activities.Clear();
                var activities = await _msHealthClient.ListActivitiesAsync();
                if (activities?.BikeActivities != null)
                    foreach (var activity in activities.BikeActivities) { Activities.Add(activity); }
                if (activities?.RunActivities != null)
                    foreach (var activity in activities.RunActivities) { Activities.Add(activity); }
                if (activities?.SleepActivities != null)
                    foreach (var activity in activities.SleepActivities) { Activities.Add(activity); }
                if (activities?.FreePlayActivities != null)
                    foreach (var activity in activities.FreePlayActivities) { Activities.Add(activity); }
                if (activities?.GolfActivities != null)
                    foreach (var activity in activities.GolfActivities) { Activities.Add(activity); }
                if (activities?.GuidedWorkoutActivities != null)
                    foreach (var activity in activities.GuidedWorkoutActivities) { Activities.Add(activity); }
                if (activities?.HikeActivities != null)
                    foreach (var activity in activities.HikeActivities) { Activities.Add(activity); }
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
