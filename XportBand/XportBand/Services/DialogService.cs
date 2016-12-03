using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace XportBand.Services
{

	public class DialogService : IDialogService
	{

		private Page _dialogPage;

		public void Initialize(Page dialogPage)
		{
			_dialogPage = dialogPage;
		}

		private bool EnsureMainPage()
		{
			if (_dialogPage == null)
			{
				if (Application.Current.MainPage == null)
					return false;
				_dialogPage = Application.Current.MainPage;
				if (_dialogPage == null) return false;
			}
			return true;
		}

		public async Task ShowError(string message,
									string title,
									string buttonText,
									Action afterHideCallback)
		{
			if (!EnsureMainPage()) return;

			await _dialogPage.DisplayAlert(title, message, buttonText);
			afterHideCallback?.Invoke();
		}

		public async Task ShowError(Exception error,
									string title,
									string buttonText,
									Action afterHideCallback)
		{
			if (!EnsureMainPage()) return;

			await _dialogPage.DisplayAlert(title, error.Message, buttonText);
			afterHideCallback?.Invoke();
		}

		public async Task ShowMessage(string message,
									  string title)
		{
			if (!EnsureMainPage()) return;

			await _dialogPage.DisplayAlert(title, message, "OK");
		}

		public async Task ShowMessage(string message,
									  string title,
									  string buttonText,
									  Action afterHideCallback)
		{
			if (!EnsureMainPage()) return;

			await _dialogPage.DisplayAlert(title, message, buttonText);
			afterHideCallback?.Invoke();
		}

		public async Task<bool> ShowMessage(string message,
											string title,
											string buttonConfirmText,
											string buttonCancelText,
											Action<bool> afterHideCallback)
		{
			if (!EnsureMainPage()) return false;

			var result = await _dialogPage.DisplayAlert(title, message, buttonConfirmText, buttonCancelText);
			afterHideCallback?.Invoke(result);
			return result;
		}

		public async Task ShowMessageBox(string message,
										 string title)
		{
			if (!EnsureMainPage()) return;

			await _dialogPage.DisplayAlert(title, message, "OK");
		}

	}

}
