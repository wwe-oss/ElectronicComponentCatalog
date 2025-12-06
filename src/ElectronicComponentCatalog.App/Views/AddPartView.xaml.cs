using Microsoft.Maui.Controls;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Views
{
    public partial class AddPartView : ContentPage
    {
        public AddPartView(AddPartViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (BindingContext is AddPartViewModel vm)
                await vm.SavePartAsync();
        }
    }
}
