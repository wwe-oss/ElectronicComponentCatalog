using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels
{
    /// <summary>
    /// Manages navigation commands for the main shell.
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;

        public MainViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task NavigateCatalogAsync() => await _navigationService.NavigateToAsync("CatalogView");
        [RelayCommand]
        private async Task NavigateAddPartAsync() => await _navigationService.NavigateToAsync("AddPartView");
        [RelayCommand]
        private async Task NavigateBulkAddAsync() => await _navigationService.NavigateToAsync("BulkAddView");
        [RelayCommand]
        private async Task NavigateSearchAsync() => await _navigationService.NavigateToAsync("SearchView");
        [RelayCommand]
        private async Task NavigateSettingsAsync() => await _navigationService.NavigateToAsync("SettingsView");
    }
}
