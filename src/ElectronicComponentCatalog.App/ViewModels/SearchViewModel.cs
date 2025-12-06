using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly IComponentRepository _repo;

        [ObservableProperty] private string _query = string.Empty;
        public ObservableCollection<Component> Results { get; } = new();

        public SearchViewModel(IComponentRepository repo) => _repo = repo;

        [RelayCommand]
        public async Task ExecuteSearchAsync()
        {
            Results.Clear();
            var found = await _repo.SearchByNameAsync(Query);
            foreach (var f in found) Results.Add(f);
        }
    }
}
