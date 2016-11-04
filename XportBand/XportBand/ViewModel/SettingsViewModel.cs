//-----------------------------------------------------------------------
// <copyright file="SettingsViewModel.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MSHealthAPI.Contracts;

namespace XportBand.ViewModel
{

    /// <summary>
    /// ViewModel for Settings.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class SettingsViewModel : ViewModelBase
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
        /// Backing store for <see cref="ShowSignIn"/> property.
        /// </summary>
        private bool _showSignIn;

        /// <summary>
        /// Backing store for <see cref="SignInMSHealthCommand"/> property.
        /// </summary>
        private ICommand _signInMSHealth;

        /// <summary>
        /// Backing store for <see cref="NavigatedCommand"/> property.
        /// </summary>
        private ICommand _navigatedCommand;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether Sign-in Web Page must be shown.
        /// </summary>
        public bool ShowSignIn
        {
            get { return _showSignIn; }
            set { Set(() => ShowSignIn, ref _showSignIn, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the command to execute: Sign-in Microsoft Health.
        /// </summary>
        public ICommand SignInMSHealthCommand
        {
            get { return _signInMSHealth ?? (_signInMSHealth = new RelayCommand(SignInMSHealth)); }
        }

        public ICommand NavigatedCommand
        {
            get { return _navigatedCommand ?? (_navigatedCommand = new RelayCommand<object>(Navigated)); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="dialogService">Dialog service instance.</param>
        /// <param name="navigationService">Navigation service instance.</param>
        /// <param name="msHealthClient">Microsoft Health service instance.</param>
        public SettingsViewModel(IDialogService dialogService, INavigationService navigationService, IMSHealthClient msHealthClient)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _msHealthClient = msHealthClient;
        }

        #endregion

        #region Commands Execution

        /// <summary>
        /// Invokes Sign-in to Microsoft Health service.
        /// </summary>
        private void SignInMSHealth()
        {
            ShowSignIn = true;
            Messenger.Default.Send(_msHealthClient.SignInUri);
        }

        /// <summary>
        /// Handles NavigationCompleted event from WebView.
        /// </summary>
        /// <param name="args">Arguments for event.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Potential Code Quality Issues", "RECS0165:Asynchronous methods should return a Task instead of void", Justification = "Command actions must return void")]
        private async void Navigated(object args)
        {
            Uri navigatedUri = null;
#if DESKTOP
            var eventArgs = args as System.Windows.Navigation.NavigationEventArgs;
            navigatedUri = eventArgs?.Uri;
#endif
            var msHealthResult = await _msHealthClient.HandleRedirectAsync(navigatedUri);
            switch (msHealthResult)
            {
                case MSHealthRedirectResult.SignIn:
                    // 
                    if (_msHealthClient.IsSignedIn)
                    {
                        var profile = await _msHealthClient.ReadProfileAsync();
                        await _dialogService.ShowMessage($"Welcome {profile.FirstName}. Your last update was on {profile.LastUpdateTime:yyyy-MM-dd HH:mm:ss}", "Sign-in");
                        // TODO: Persist Token (or communicate to other ViewModel's)
                        //Serilog.Log.Debug("Signed-in: ", _msHealthClient.Token);
                    }
                    ShowSignIn = false;
                    break;
            }
        }

        #endregion

    }

}
