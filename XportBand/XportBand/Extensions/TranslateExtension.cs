using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XportBand.Extensions
{

	/// <summary>
	/// Implementations of this interface MUST convert iOS and Android
	/// platform-specific locales to a value supported in .NET because
	/// ONLY valid .NET cultures can have their RESX resources loaded and used.
	/// </summary>
	/// <remarks>
	/// Lists of valid .NET cultures can be found here:
	///   http://www.localeplanet.com/dotnet/
	///   http://www.csharp-examples.net/culture-names/
	/// You should always test all the locales implemented in your application.
	/// </remarks>
	public interface ILocalize
	{
		///	<summary>
		/// This method must evaluate platform-specific locale settings
		/// and convert them (when necessary) to a valid .NET locale.
		/// </summary>
		CultureInfo GetCurrentCultureInfo();

		/// <summary>
		/// CurrentCulture and CurrentUICulture must be set in the platform project, 
		/// because the Thread object can't be accessed in a PCL.
		/// </summary>
		void SetLocale(CultureInfo ci);
	}

	/// <summary>
	/// Helper class for splitting locales like
	///   iOS: ms_MY, gsw_CH
	///   Android: in-ID
	/// into parts so we can create a .NET culture (or fallback culture)
	/// </summary>
	public class PlatformCulture
	{
		public PlatformCulture(string platformCultureString)
		{
			if (string.IsNullOrEmpty(platformCultureString))
				throw new ArgumentException("Expected culture identifier", nameof(platformCultureString)); // in C# 6 use nameof(platformCultureString)

			PlatformString = platformCultureString.Replace("_", "-"); // .NET expects dash, not underscore
			var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
			if (dashIndex > 0)
			{
				var parts = PlatformString.Split('-');
				LanguageCode = parts[0];
				LocaleCode = parts[1];
			}
			else
			{
				LanguageCode = PlatformString;
				LocaleCode = "";
			}
		}
		public string PlatformString { get; private set; }
		public string LanguageCode { get; private set; }
		public string LocaleCode { get; private set; }
		public override string ToString()
		{
			return PlatformString;
		}
	}

	// You exclude the 'Extension' suffix when using in Xaml markup
	[ContentProperty("Text")]
	public class TranslateExtension : IMarkupExtension
	{
		readonly CultureInfo ci;
		const string ResourceId = "XportBand.Properties.Resources";

		public TranslateExtension()
		{
			if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
			{
				ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
			}
		}

		public string Text { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Text == null)
				return "";

			var rsxMgr = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

			var translation = rsxMgr.GetString(Text, ci);
			if (translation == null)
			{
#if DEBUG
				throw new ArgumentException(string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name), nameof(Text));
#else
				translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
			}
			return translation;
		}
	}

}
