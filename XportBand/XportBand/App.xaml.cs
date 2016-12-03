using System.Reflection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Xamarin.Forms;
using XportBand.Extensions;
using XportBand.View;
using XportBand.ViewModel;

namespace XportBand
{
	public partial class App : Application
	{

		private static ViewModelLocator _locator;

		public static ViewModelLocator Locator
		{
			get { return _locator ?? (_locator = new ViewModelLocator()); }
		}

		public App()
		{
			InitializeComponent();

			Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));
			var levelSwitch = new LoggingLevelSwitch();
			levelSwitch.MinimumLevel = LogEventLevel.Verbose;
			var debugLogTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {SourceContext} [{Level}] {Message}{NewLine}{Exception}";
			Log.Logger = new LoggerConfiguration()
								.MinimumLevel.ControlledBy(levelSwitch)
								.WriteTo.Debug(outputTemplate: debugLogTemplate)
								.CreateLogger();

			Log.Debug("====== resource debug info =========");
			var assembly = typeof(App).GetTypeInfo().Assembly;
			foreach (var res in assembly.GetManifestResourceNames())
				Log.Debug("found resource: " + res);
			Log.Debug("====================================");

			// This lookup NOT required for Windows platforms - the Culture will be automatically set
			if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
			{
				// determine the correct, supported .NET culture
				var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
				XportBand.Properties.Resources.Culture = ci; // set the RESX for resource localization
				DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
			}

			//NavigationPage shellView = new NavigationPage(new MainView());
			//MainPage = shellView;
			MainPage = new NavigationPage(new MainView());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
