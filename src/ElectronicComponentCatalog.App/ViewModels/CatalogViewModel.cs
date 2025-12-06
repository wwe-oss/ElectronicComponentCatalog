using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels
{
    /// <summary>
    /// Displays and filters component list.
    /// </summary>
    public partial class CatalogViewModel : ObservableObject
    {
        private readonly IComponentRepository _repository;

        [ObservableProperty] private string _searchText = string.Empty;
        public ObservableCollection<Component> Components { get; } = new();

        public CatalogViewModel(IComponentRepository repository)
        {
            _repository = repository;
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            Components.Clear();
            var results = string.IsNullOrWhiteSpace(SearchText)
                ? await _repository.GetAllAsync()
                : await _repository.SearchByNameAsync(SearchText);

            foreach (var c in results.OrderBy(x => x.Name))
                Components.Add(c);
        }

        public async Task LoadAsync() => await SearchAsync();
    }
}
