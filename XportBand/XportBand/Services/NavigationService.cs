using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace XportBand.Services
{

	public class NavigationService : INavigationService
	{

		private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
		private NavigationPage _navigation;

		private bool EnsureMainPage()
		{
			if (_navigation == null)
			{
				if (Application.Current.MainPage == null)
					return false;
				_navigation = Application.Current.MainPage as NavigationPage;
				if (_navigation == null) return false;
			}
			return true;
		}

		public string CurrentPageKey
		{
			get
			{
				lock (_pagesByKey)
				{
					if (_navigation.CurrentPage == null)
					{
						return null;
					}

					var pageType = _navigation.CurrentPage.GetType();

					return _pagesByKey.ContainsValue(pageType)
						? _pagesByKey.First(p => p.Value == pageType).Key
						: null;
				}
			}
		}

		public void GoBack()
		{
			if (!EnsureMainPage()) return;
			_navigation.PopAsync();
		}

		public void NavigateTo(string pageKey)
		{
			if (!EnsureMainPage()) return;
			NavigateTo(pageKey, null);
		}

		public void NavigateTo(string pageKey, object parameter)
		{
			if (!EnsureMainPage()) return;
			lock (_pagesByKey)
			{
				if (_pagesByKey.ContainsKey(pageKey))
				{
					var type = _pagesByKey[pageKey];
					ConstructorInfo constructor = null;
					object[] parameters = null;

					if (parameter == null)
					{
						constructor = type.GetTypeInfo()
							.DeclaredConstructors
							.FirstOrDefault(c => !c.GetParameters().Any());

						parameters = new object[]
						{
						};
					}
					else
					{
						constructor = type.GetTypeInfo()
							.DeclaredConstructors
							.FirstOrDefault(
								c =>
								{
									var p = c.GetParameters();
									return p.Count() == 1
										   && p[0].ParameterType == parameter.GetType();
								});

						parameters = new[]
						{
							parameter
						};
					}

					if (constructor == null)
					{
						throw new InvalidOperationException(
							"No suitable constructor found for page " + pageKey);
					}

					var page = constructor.Invoke(parameters) as Page;
					_navigation.PushAsync(page);
				}
				else
				{
					throw new ArgumentException(
						string.Format(
							"No such page: {0}. Did you forget to call NavigationService.Configure?",
							pageKey), nameof(pageKey));
				}
			}
		}

		public void Configure(string pageKey, Type pageType)
		{
			lock (_pagesByKey)
			{
				if (_pagesByKey.ContainsKey(pageKey))
				{
					_pagesByKey[pageKey] = pageType;
				}
				else
				{
					_pagesByKey.Add(pageKey, pageType);
				}
			}
		}

		public void Initialize(NavigationPage navigation)
		{
			_navigation = navigation;
		}

	}

}
