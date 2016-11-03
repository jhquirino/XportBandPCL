using System.Windows;
using GalaSoft.MvvmLight.Threading;
using Serilog;

namespace XportBand
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        static App()
        {
            var levelSwitch = new Serilog.Core.LoggingLevelSwitch();
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));
                levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;
                var debugLogTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {SourceContext} [{Level}] {Message}{NewLine}{Exception}";
                Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.ControlledBy(levelSwitch)
                                    .WriteTo.Debug(outputTemplate: debugLogTemplate)
                                    .CreateLogger();
            }
            else
            {
#if DEBUG
                levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
#else
                levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Information;
#endif
                var fileLogTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}|{SourceContext}|{Level}|{Message}{NewLine}{Exception}";
                Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.ControlledBy(levelSwitch)
                                    .WriteTo.File(@"C:\temp\XportBand.log", outputTemplate: fileLogTemplate, shared: true)
                                    .CreateLogger();
            }
            DispatcherHelper.Initialize();
        }

    }

}
