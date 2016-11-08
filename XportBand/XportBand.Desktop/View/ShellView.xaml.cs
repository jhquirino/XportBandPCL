using System.Windows;
using MahApps.Metro.Controls;
using XportBand.Model;

namespace XportBand.View
{

    /// <summary>
    /// Description for ShellView.
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        /// <summary>
        /// Initializes a new instance of the ShellView class.
        /// </summary>
        public ShellView()
        {
            InitializeComponent();
            DataContext = App.Locator.Shell;
        }

        private void dckManager_ActiveContentChanged(object sender, System.EventArgs e)
        {
            INavigable navigable = null;
            if (dockMain.IsActive)
            {
                navigable = vwMain.ViewModel as INavigable;
            }
            else if (dockSettings.IsActive)
            {
                navigable = vwSettings.ViewModel as INavigable;
            }
            else if (dockDetails.IsActive)
            {
                //navigable = vwDetails.ViewModel as INavigable;
            }


            if (navigable != null) navigable.Activate(null);
        }

    }

}