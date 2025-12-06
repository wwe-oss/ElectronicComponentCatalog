using Microsoft.Maui.Controls;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Views
{
    public partial class BulkAddView : ContentPage
    {
        public BulkAddView(BulkAddViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnBulkAddClicked(object sender, EventArgs e)
        {
            if (BindingContext is BulkAddViewModel vm)
                await vm.ProcessBulkAddAsync();
        }
    }
}
