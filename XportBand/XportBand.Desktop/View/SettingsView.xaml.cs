using System;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using XportBand.ViewModel;

namespace XportBand.View
{
    /// <summary>
    /// Description for SettingsView.
    /// </summary>
    public partial class SettingsView : UserControl
    {

        public SettingsViewModel ViewModel
        {
            get
            {
                return (SettingsViewModel)DataContext;
            }
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SettingsView class.
        /// </summary>
        public SettingsView()
        {
            InitializeComponent();
            DataContext = App.Locator.Settings;
            // Register MVVM Message (handles URI navigation)
            Messenger.Default.Register<Uri>(this, (uri) => HandleNavigateUri(uri));
        }

        #endregion

        #region MVVM Messaging Handlers

        /// <summary>
        /// Handles MVVM Message for URI navigation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        private void HandleNavigateUri(Uri uri)
        {
            // Navigate to URI on WebView
            wbwSignIn.Navigate(uri);
        }

        #endregion
    }
}