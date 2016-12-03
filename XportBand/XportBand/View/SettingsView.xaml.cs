using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Xamarin.Forms;
using XportBand.ViewModel;

namespace XportBand.View
{
	public partial class SettingsView : TabbedPage
	{

		public SettingsViewModel ViewModel
		{
			get
			{
				return (SettingsViewModel)BindingContext;
			}
		}

		public SettingsView()
		{
			InitializeComponent();
			BindingContext = App.Locator.Settings;
			// Register MVVM Message (handles URI navigation)
			Messenger.Default.Register<Uri>(this, (uri) => HandleNavigateUri(uri));
		}

		#region MVVM Messaging Handlers

		/// <summary>
		/// Handles MVVM Message for URI navigation.
		/// </summary>
		/// <param name="uri">The URI.</param>
		private void HandleNavigateUri(Uri uri)
		{
			// Navigate to URI on WebView
			wvwSignIn.Source = uri;
			//wbwSignIn.Navigate(uri);
		}

		#endregion

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var navigable = ViewModel as XportBand.Model.INavigable;
			if (navigable != null)
				navigable.Activate(null);
		}

	}

}
