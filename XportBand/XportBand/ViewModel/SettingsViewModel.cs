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
using XportBand.Model;

namespace XportBand.ViewModel
{

    /// <summary>
    /// ViewModel for Settings.
    /// </summary>
    /// <seealso cref="ViewModelBase" />
    public class SettingsViewModel : ViewModelBase, INavigable
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
        /// Inner member for <see cref="IsMSHealthSignedIn"/> property.
        /// </summary>
        private bool _isMSHealthSignedIn = false;

        /// <summary>
        /// Backing store for <see cref="ShowSignIn"/> property.
        /// </summary>
        private bool _showSignIn;

        /// <summary>
        /// Backing store for <see cref="SignInMSHealthCommand"/> property.
        /// </summary>
        private ICommand _signInMSHealth;

        /// <summary>
        /// Backing store for <see cref="SignOutMSHealthCommand"/> property.
        /// </summary>
        private ICommand _signOutMSHealth;

        /// <summary>
        /// Backing store for <see cref="NavigatedCommand"/> property.
        /// </summary>
        private ICommand _navigatedCommand;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether user is signed-into Microsoft Health.
        /// </summary>
        public bool IsMSHealthSignedIn
        {
            get { return _isMSHealthSignedIn; }
            set { Set(() => IsMSHealthSignedIn, ref _isMSHealthSignedIn, value); }
        }

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

        /// <summary>
        /// Gets the command to execute: Sign-out Microsoft Health.
        /// </summary>
        public ICommand SignOutMSHealthCommand
        {
            get { return _signOutMSHealth ?? (_signOutMSHealth = new RelayCommand(SignOutMSHealth)); }
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
        /// Invokes Sign-in to Microsoft Health service.
        /// </summary>
        private void SignOutMSHealth()
        {
            ShowSignIn = true;
            Messenger.Default.Send(_msHealthClient.SignOutUri);
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
                    ShowSignIn = false;
                    IsMSHealthSignedIn = _msHealthClient.IsSignedIn;
                    Settings.MSHealthToken = _msHealthClient.Token;
                    if (_msHealthClient.IsSignedIn)
                    {   
                        var profile = await _msHealthClient.ReadProfileAsync();
                        await _dialogService.ShowMessage($"Welcome {profile.FirstName}. Your last update was on {profile.LastUpdateTime:yyyy-MM-dd HH:mm:ss}", "Sign-in");
                    }
                    break;
                case MSHealthRedirectResult.SignOut:
                    ShowSignIn = false;
                    IsMSHealthSignedIn = false;
                    Settings.MSHealthToken = null;
                    break;
            }
        }

        #endregion

        #region INavigable Implementation

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Potential Code Quality Issues", "RECS0165:Asynchronous methods should return a Task instead of void", Justification = "Command actions must return void")]
        public async void Activate(object parameter)
        {
            //throw new NotImplementedException();
            if (Settings.MSHealthToken != null)
            {
                ////;
                try
                {
                    if (await _msHealthClient.ValidateTokenAsync(Settings.MSHealthToken))
                    {
                        IsMSHealthSignedIn = true;
                        Settings.MSHealthToken = _msHealthClient.Token;
                    }
                    else
                    {
                        IsMSHealthSignedIn = false;
                        Settings.MSHealthToken = null;
                    }
                }
                catch (Exception)
                {
                    IsMSHealthSignedIn = false;
                    Settings.MSHealthToken = null;
                    //throw;
                }
            }
            else
            {
                IsMSHealthSignedIn = false;
            }
        }

        public void Deactivate(object parameter)
        {
            //throw new NotImplementedException();
        }

        #endregion

    }

}
