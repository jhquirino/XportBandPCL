using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace XportBand.Services
{

    public class DialogService : IDialogService
    {

        MetroWindow _mainWindow;

        private bool EnsureMainMetroWindow()
        {
            if (_mainWindow == null)
            {
                if (System.Windows.Application.Current.MainWindow == null)
                    return false;
                _mainWindow = System.Windows.Application.Current.MainWindow as MetroWindow;
                if (_mainWindow == null) return false;
            }
            return true;
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            if (!EnsureMainMetroWindow()) return;

            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = buttonText
            };

            var result = await _mainWindow.ShowMessageAsync(title, error.Message, MessageDialogStyle.Affirmative, settings);

            afterHideCallback?.Invoke();
        }

        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            if (!EnsureMainMetroWindow()) return;

            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = buttonText
            };

            var result = await _mainWindow.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative, settings);

            afterHideCallback?.Invoke();
        }

        public async Task ShowMessage(string message, string title)
        {
            if (!EnsureMainMetroWindow()) return;
            //if (_mainWindow == null)
            //{
            //    if (System.Windows.Application.Current.MainWindow == null)
            //        return;
            //    _mainWindow = System.Windows.Application.Current.MainWindow as MetroWindow;
            //    if (_mainWindow == null) return;
            //}
            //await Task.Run(() => System.Windows.MessageBox.Show(message, title));
            await _mainWindow.ShowMessageAsync(title, message);
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            if (!EnsureMainMetroWindow()) return;

            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = buttonText
            };

            var result = await _mainWindow.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative, settings);

            afterHideCallback?.Invoke();
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            if (!EnsureMainMetroWindow()) return false;

            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = buttonConfirmText,
                NegativeButtonText = buttonCancelText
            };

            var result = await _mainWindow.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegative, settings);

            afterHideCallback?.Invoke(result == MessageDialogResult.Affirmative);

            return result == MessageDialogResult.Affirmative;
        }

        public async Task ShowMessageBox(string message, string title)
        {
            if (!EnsureMainMetroWindow()) return;

            await _mainWindow.ShowMessageAsync(title, message);
        }

    }

}
