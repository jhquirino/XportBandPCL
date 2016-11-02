using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSHealthAPI.Contracts;
using System.Threading.Tasks;
using Serilog;

namespace APIUnitTest
{

    /// <summary>
    /// Class to perform Unit Tests on Client for Microsoft Health Cloud API.
    /// </summary>
    [TestClass]
    public class UnitTestMSHealthAPI
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

        #endregion

        #region Inner members

        /// <summary>
        /// Client to perform requests to Microsoft Health Cloud API.
        /// </summary>
        IMSHealthClient moClient;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes common resources for all tests.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));
            var levelSwitch = new Serilog.Core.LoggingLevelSwitch();
            levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;
            string fileLogTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}|{Level}|{Message}{NewLine}{Exception}";
            if (System.Diagnostics.Debugger.IsAttached)
                Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.ControlledBy(levelSwitch)
                                    .WriteTo.Debug()
                                    .CreateLogger();
            else
                Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.ControlledBy(levelSwitch)
                                    .WriteTo.File(@"C:\temp\MSHealthAPI.log", outputTemplate: fileLogTemplate, shared: true)
                                    .CreateLogger();

            moClient = new MSHealthAPI.Core.MSHealthClient(MSHEALTH_CLIENT_ID, MSHEALTH_CLIENT_SECRET, MSHEALTH_SCOPE);
            Log.Information(Uri.EscapeUriString(moClient.SignInUri.ToString()));
        }

        #endregion

        #region Positive Tests

        /// <summary>
        /// Tests handling of redirect for Sign-in action.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// It is necessary to login in a webbrowser and replace the URL redirected.
        /// </remarks>
        [TestMethod]
        public async Task TestHandleRedirectSignIn()
        {
            MSHealthRedirectResult loResult = MSHealthRedirectResult.None;
            var loUri = new Uri("https://login.live.com/oauth20_desktop.srf?code=M3d6bd4f7-363d-c69b-404f-be70a86af8d9&lc=3082");
            loResult = await moClient.HandleRedirectAsync(loUri);
            Assert.AreEqual(MSHealthRedirectResult.SignIn, loResult);
            Assert.AreEqual(true, moClient.IsSignedIn);
        }

        #endregion

    }

}
