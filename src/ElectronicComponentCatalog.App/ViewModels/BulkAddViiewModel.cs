using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;
using System.Threading.Tasks;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels
{
    /// <summary>
    /// Allows multi-line part import.
    /// </summary>
    public partial class BulkAddViewModel : ObservableObject
    {
        private readonly IComponentRepository _componentRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly DialogService _dialog;

        [ObservableProperty] private string _bulkText = string.Empty;

        public BulkAddViewModel(IComponentRepository compRepo, ICategoryRepository catRepo, DialogService dialog)
        {
            _componentRepo = compRepo;
            _categoryRepo = catRepo;
            _dialog = dialog;
        }

        [RelayCommand]
        private async Task ImportAsync()
        {
            var lines = _bulkText.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);
            int count = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(',', System.StringSplitOptions.TrimEntries);
                if (parts.Length < 5) continue;
                var cat = await _categoryRepo.GetByNameAsync(parts[2]) ?? new Category(parts[2], "");
                if (!decimal.TryParse(parts[3], out var val) || !int.TryParse(parts[5], out var qty)) continue;
                var spec = new ComponentSpecification(val, parts[4]);
                var c = new Component(parts[0], parts[1], cat, spec, qty);
                await _componentRepo.AddAsync(c);
                count++;
            }
            await _dialog.ShowMessageAsync("Import Complete", $"{count} parts imported.");
        }
    }
}
