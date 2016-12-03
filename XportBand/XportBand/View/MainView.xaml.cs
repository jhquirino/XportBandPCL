using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XportBand.Model;
using XportBand.ViewModel;

namespace XportBand.View
{

	public partial class MainView : ContentPage
	{

		public MainViewModel ViewModel
		{
			get
			{
				return (MainViewModel)BindingContext;
			}
		}

		public MainView()
		{
			InitializeComponent();
			BindingContext = App.Locator.Main;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var navigable = ViewModel as INavigable;
			if (navigable != null)
				navigable.Activate(null);
		}

	}

}
