using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XportBand.Extensions
{
	
	[ContentProperty("Source")]
	public class ImageResourceExtension : IMarkupExtension
	{
		public string Source { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (string.IsNullOrEmpty(Source?.Trim()))
			{
				return null;
			}
			// Do your translation lookup here, using whatever method you require
			var imageSource = ImageSource.FromResource(Source);

			return imageSource;
		}
	}
}
