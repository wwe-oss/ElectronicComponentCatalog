using System;
using Microsoft.Maui.Controls;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Views
{
	/// <summary>
	/// Main shell of the application with sidebar navigation.
	/// </summary>
	public partial class MainPage : ContentPage
	{
		private readonly NavigationService _navigationService;

		public MainPage(MainViewModel viewModel, NavigationService navigationService)
		{
			_navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
			InitializeComponent();
			ArgumentNullException.ThrowIfNull(viewModel);
			BindingContext = viewModel;
			_navigationService.SetContentRegion(ContentRegion);
		}
	}
}
