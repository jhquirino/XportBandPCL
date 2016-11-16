using System.Windows;
using GalaSoft.MvvmLight.Threading;
using Serilog;
using XportBand.ViewModel;

namespace XportBand
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get { return _locator ?? (_locator = new ViewModelLocator()); }
        }

        static App()
        {
            Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));
            Log.Logger = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();
            DispatcherHelper.Initialize();
        }

    }

}
