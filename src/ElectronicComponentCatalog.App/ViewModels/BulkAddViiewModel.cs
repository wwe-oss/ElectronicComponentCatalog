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

        [ObservableProperty]
        public partial string BulkText { get; set; } = string.Empty;

        public BulkAddViewModel(IComponentRepository compRepo, ICategoryRepository catRepo, DialogService dialog)
        {
            _componentRepo = compRepo;
            _categoryRepo = catRepo;
            _dialog = dialog;
        }

        [RelayCommand]
        private async Task ImportAsync()
        {
            var lines = BulkText.Split(separator: '\n', options: System.StringSplitOptions.RemoveEmptyEntries);
            int count = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(',', System.StringSplitOptions.TrimEntries);
                if (parts.Length < 5) continue;
                var cat = await _categoryRepo.GetByNameAsync(name: parts[2]) ?? new Category(name: parts[2], description: "");
                var valueText = parts[3];
                var normalizedValue = valueText.Replace(oldValue: "k", newValue: "000");
                if (!decimal.TryParse(normalizedValue, result: out _) || !int.TryParse(s: parts[5], result: out int qty)) continue;
                var spec = new ComponentSpecification(value: valueText, unit: parts[4]);
                var c = new Component(name: parts[0], commonName: parts[1], category: cat, specification: spec, quantityOnHand: qty);
                await _componentRepo.AddAsync(entity: c);
                count++;
            }
            await _dialog.ShowMessageAsync("Import Complete", $"{count} parts imported.");
        }

        public Task ProcessBulkAddAsync() => ImportAsync();
    }
}
