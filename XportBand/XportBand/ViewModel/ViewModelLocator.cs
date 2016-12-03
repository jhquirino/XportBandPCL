//-----------------------------------------------------------------------
// <copyright file="ViewModelLocator.cs" company="Jorge Alberto Hernández Quirino">
// Copyright (c) Jorge Alberto Hernández Quirino 2015-2017. All rights reserved.
// </copyright>
// <author>Jorge Alberto Hernández Quirino</author>
//-----------------------------------------------------------------------
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using MSHealthAPI.Contracts;
using MSHealthAPI.Core;
using XportBand.Services;
#if XAMARIN
using Xamarin.Forms;
using XportBand.View;
#endif

namespace XportBand.ViewModel
{

    public class ViewModelLocator
    {

        #region Constants

        /// <summary>
        /// Client ID registered for XportBand app.
        /// </summary>
        public const string MSHEALTH_CLIENT_ID = "000000004017E7B0";

        /// <summary>
        /// Client Secret registered for XportBand app.
        /// </summary>
        public const string MSHEALTH_CLIENT_SECRET = "-Z0MoiAX96sx2aEAmhfpDfc4CoaKcxAL";

        /// <summary>
        /// Access types (scopes) required for XportBand app to work with.
        /// </summary>
        public const MSHealthScope MSHEALTH_SCOPE = MSHealthScope.ReadProfile |
                                                    MSHealthScope.ReadActivityHistory |
                                                    MSHealthScope.ReadActivityLocation |
                                                    MSHealthScope.OfflineAccess;

        /// <summary>
        /// HACK: ID for registered Nike+ App.
        /// </summary>
        public const string NIKEPLUS_APP = "NIKEPLUSGPS";

        /// <summary>
        /// HACK: Client ID for registered Nike+ App.
        /// </summary>
        public const string NIKEPLUS_CLIENT_ID = "9dfa1aef96a54441dfaac68c4410e063";

        /// <summary>
        /// HACK: Client Secret for registered Nike+ App.
        /// </summary>
        public const string NIKEPLUS_CLIENT_SECRET = "3cbd1f1908bc1553";

        #endregion

        #region ViewModel references

#if DESKTOP
        /// <summary>
        /// Gets the ViewModel for Shell.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public ShellViewModel Shell
        {
            get { return ServiceLocator.Current.GetInstance<ShellViewModel>(); }
        }
#endif

        /// <summary>
        /// Gets the ViewModel for Main.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        /// <summary>
        /// Gets the ViewModel for Settings.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsViewModel Settings
        {
            get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
        /// </summary>
        static ViewModelLocator()
        {
            /*
             * Initialize IoC
             */
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            /*
             * Register and Configure Services
             */
#if XAMARIN
            var dialogSvc = new DialogService();
            SimpleIoc.Default.Register<IDialogService>(() => dialogSvc);
            dialogSvc.Initialize(Application.Current.MainPage);

            var navigationSvc = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationSvc);
            navigationSvc.Configure(nameof(MainView), typeof(MainView));
			navigationSvc.Configure(nameof(SettingsView), typeof(SettingsView));
            navigationSvc.Initialize((NavigationPage)Application.Current.MainPage);
#endif
#if DESKTOP
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
#endif
            var msHealthClient = new MSHealthClient(MSHEALTH_CLIENT_ID, MSHEALTH_CLIENT_SECRET, MSHEALTH_SCOPE);
            SimpleIoc.Default.Register<IMSHealthClient>(() => msHealthClient);
            /*
             * Register ViewModels
             */
#if DESKTOP
            SimpleIoc.Default.Register<ShellViewModel>();
#endif
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        #endregion

    }

}
