using Microsoft.Maui.Controls;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Views
{
    public partial class SearchView : ContentPage
    {
        public SearchView(SearchViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            if (BindingContext is SearchViewModel vm)
                await vm.ExecuteSearchAsync();
        }
    }
}
