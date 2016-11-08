using System.Windows.Controls;
using XportBand.ViewModel;

namespace XportBand.View
{

    /// <summary>
    /// Description for MainView.
    /// </summary>
    public partial class MainView : UserControl
    {

        public MainViewModel ViewModel
        {
            get
            {
                return (MainViewModel)DataContext;
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainView class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            DataContext = App.Locator.Main;
        }
    }
}