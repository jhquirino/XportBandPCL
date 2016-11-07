using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XportBand.ViewModel;

namespace XportBand.View
{
	public partial class SettingsView : ContentPage
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
		}

	}

}
