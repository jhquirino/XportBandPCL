using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;

namespace XportBand.Services
{

    public class DialogService : IDialogService
    {

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public async Task ShowMessage(string message, string title)
        {
            //throw new NotImplementedException();
            //await Task.Factory.StartNew(async () => System.Windows.MessageBox.Show(message, title));
            await Task.Run(() => System.Windows.MessageBox.Show(message, title));
            //System.Windows.MessageBox.Show(message, title);
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessageBox(string message, string title)
        {
            throw new NotImplementedException();
        }

    }

}
