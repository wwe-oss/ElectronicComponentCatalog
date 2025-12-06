using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly ConfigurationService _config;

        [ObservableProperty] private string _databasePath = string.Empty;

        public SettingsViewModel(ConfigurationService config)
        {
            _config = config;
            DatabasePath = _config.GetConnectionString().Replace(oldValue: "Data Source=", newValue: "");
        }

        [RelayCommand]
        private async Task OpenFolderAsync()
        {
            var folder = System.IO.Path.GetDirectoryName(DatabasePath)!;
            Process.Start("explorer.exe", folder);
            await Task.CompletedTask;
        }
    }
}
