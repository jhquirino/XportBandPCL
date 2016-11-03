using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
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

    }

}
