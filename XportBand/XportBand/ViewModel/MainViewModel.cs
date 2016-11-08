//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                var activitiesList = new List<MSHealthActivity>();
                if (activities?.BikeActivities != null)
                    activitiesList.AddRange(activities.BikeActivities);
                if (activities?.RunActivities != null)
                    activitiesList.AddRange(activities.RunActivities);
                if (activities?.SleepActivities != null)
                    activitiesList.AddRange(activities.SleepActivities);
                if (activities?.FreePlayActivities != null)
                    activitiesList.AddRange(activities.FreePlayActivities);
                if (activities?.GolfActivities != null)
                    activitiesList.AddRange(activities.GolfActivities);
                if (activities?.GuidedWorkoutActivities != null)
                    activitiesList.AddRange(activities.GuidedWorkoutActivities);
                if (activities?.HikeActivities != null)
                    activitiesList.AddRange(activities.HikeActivities);
                activitiesList = activitiesList.OrderByDescending(a => a.StartTime).ToList();
                foreach (var activity in activitiesList) { Activities.Add(activity); }
                await _dialogService.ShowMessage($"{activities.ItemCount:N0} activities found.", "Activities");
            }
            catch (Exception exception)
            {
                Serilog.Log.ForContext<MainViewModel>().Error(exception, "Error while reading MSHealth Activities.");
                await _dialogService.ShowError(exception, "Error", "OK", null);
            }
        }

        #endregion

    }

}
