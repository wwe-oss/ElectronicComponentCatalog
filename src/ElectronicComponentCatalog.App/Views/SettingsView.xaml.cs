using Microsoft.Maui.Controls;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Views
{
    public partial class SettingsView : ContentPage
    {
        public SettingsView(SettingsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (BindingContext is SettingsViewModel vm)
                await vm.SaveSettingsAsync();
        }
    }
}
