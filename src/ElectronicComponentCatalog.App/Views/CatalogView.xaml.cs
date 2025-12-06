using Microsoft.Maui.Controls;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Views
{
    public partial class CatalogView : ContentPage
    {
        public CatalogView(CatalogViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is CatalogViewModel vm)
                await vm.LoadCatalogAsync();
        }
    }
}
