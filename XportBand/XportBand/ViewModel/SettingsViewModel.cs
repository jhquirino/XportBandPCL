//-----------------------------------------------------------------------
// <copyright file="SettingsViewModel.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2016. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using System;
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
		/// Backing store for <see cref="IsBusy"/> property.
		/// </summary>
		private bool _isBusy;

		/// <summary>
		/// Backing store for <see cref="IsMSHealthBusy"/> property.
		/// </summary>
		private bool _isMSHealthBusy;

		/// <summary>
		/// Backing store for <see cref="IsMSHealthSignedIn"/> property.
		/// </summary>
		private bool _isMSHealthSignedIn;

		/// <summary>
		/// Backing store for <see cref="MSHealthProfileName"/> property.
		/// </summary>
		private string _msHealthProfileName;

		/// <summary>
		/// Backing store for <see cref="ShowSignIn"/> property.
		/// </summary>
		private bool _showSignIn;

		/// <summary>
		/// Backing store for <see cref="GoBackCommand"/> property.
		/// </summary>
		private ICommand _goBack;

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
		/// Gets or sets a value indicating whether this instance is busy.
		/// </summary>
		/// <value><c>true</c> if is busy; otherwise, <c>false</c>.</value>
		public bool IsBusy
		{
			get { return _isBusy; }
			set { Set(() => IsBusy, ref _isBusy, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is busy.
		/// </summary>
		/// <value><c>true</c> if is busy; otherwise, <c>false</c>.</value>
		public bool IsMSHealthBusy
		{
			get { return _isMSHealthBusy; }
			set { Set(() => IsMSHealthBusy, ref _isMSHealthBusy, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether user is signed-into Microsoft Health.
		/// </summary>
		public bool IsMSHealthSignedIn
		{
			get { return _isMSHealthSignedIn; }
			set { Set(() => IsMSHealthSignedIn, ref _isMSHealthSignedIn, value); }
		}

		/// <summary>
		/// Gets or sets the Profile Name when signed-into Microsoft Health.
		/// </summary>
		public string MSHealthProfileName
		{
			get { return _msHealthProfileName; }
			set { Set(() => MSHealthProfileName, ref _msHealthProfileName, value); }
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
		public ICommand GoBackCommand
		{
			get { return _goBack ?? (_goBack = new RelayCommand(GoBack)); }
		}

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

		/// <summary>
		/// Gets the command to execute: Navigated.
		/// </summary>
		/// <value>The navigated command.</value>
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
		/// Goes to previous page (main page).
		/// </summary>
		private void GoBack()
		{
			_navigationService.GoBack();
		}

		/// <summary>
		/// Invokes Sign-in to Microsoft Health service.
		/// </summary>
		private void SignInMSHealth()
		{
			IsBusy = true;
			IsMSHealthBusy = true;
			ShowSignIn = true;
			Messenger.Default.Send(_msHealthClient.SignInUri);
		}

		/// <summary>
		/// Invokes Sign-in to Microsoft Health service.
		/// </summary>
		private void SignOutMSHealth()
		{
			IsBusy = true;
			IsMSHealthBusy = true;
			ShowSignIn = true;
			Messenger.Default.Send(_msHealthClient.SignOutUri);
		}

		/// <summary>
		/// Handles NavigationCompleted/Navigated event from WebBrowser/WebView.
		/// </summary>
		/// <param name="args">Arguments for event.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Potential Code Quality Issues", "RECS0165:Asynchronous methods should return a Task instead of void", Justification = "Command actions must return void")]
		private async void Navigated(object args)
		{
			Uri navigatedUri = null;
#if DESKTOP
            var eventArgs = args as System.Windows.Navigation.NavigationEventArgs;
            navigatedUri = eventArgs?.Uri;
#elif XAMARIN
			var eventArgs = args as Xamarin.Forms.WebNavigatedEventArgs;
			navigatedUri = new Uri(eventArgs?.Url);
#endif
			try
			{
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
							MSHealthProfileName = string.Format("{0} {1}", profile.FirstName?.Trim(), profile.LastName?.Trim());
						}
						break;
					case MSHealthRedirectResult.SignOut:
						ShowSignIn = false;
						IsMSHealthSignedIn = false;
						Settings.MSHealthToken = null;
						break;
				}
			}
			catch (Exception ex)
			{
				await _dialogService.ShowError(ex, "Error", "OK", null);
			}
			finally
			{
				IsBusy = false;
				IsMSHealthBusy = false;
			}
		}

		#endregion

		#region INavigable Implementation

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Potential Code Quality Issues", "RECS0165:Asynchronous methods should return a Task instead of void", Justification = "Command actions must return void")]
		public async void Activate(object parameter)
		{
			if (Settings.MSHealthToken != null)
			{
				IsMSHealthSignedIn = false;
				try
				{
					IsBusy = true;
					IsMSHealthBusy = true;
					if (await _msHealthClient.ValidateTokenAsync(Settings.MSHealthToken))
					{
						var profile = await _msHealthClient.ReadProfileAsync();
						MSHealthProfileName = string.Format("{0} {1}", profile.FirstName?.Trim(), profile.LastName?.Trim());
						Settings.MSHealthToken = _msHealthClient.Token;
						IsMSHealthSignedIn = true;
					}
					else
					{
						Settings.MSHealthToken = null;
					}
				}
				catch (Exception ex)
				{
					Serilog.Log.Error(ex, "Failed to validate/refresh Microsoft Health Token.");
					Settings.MSHealthToken = null;
				}
				finally
				{
					IsBusy = false;
					IsMSHealthBusy = false;
				}
			}
		}

		public void Deactivate(object parameter)
		{
			//throw new NotImplementedException();
		}

		#endregion

	}

}
