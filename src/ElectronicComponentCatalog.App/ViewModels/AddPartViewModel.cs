using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Helpers;
using System.Threading.Tasks;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels
{
    /// <summary>
    /// Handles single-part addition logic.
    /// </summary>
    public partial class AddPartViewModel : ObservableObject
    {
        private readonly IComponentRepository _componentRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly DialogService _dialog;

        [ObservableProperty] private string _name = string.Empty;
        [ObservableProperty] private string _commonName = string.Empty;
        [ObservableProperty] private string _categoryName = string.Empty;
        [ObservableProperty] private string _value = string.Empty;
        [ObservableProperty] private string _unit = string.Empty;
        [ObservableProperty] private string _quantity = "0";

        public AddPartViewModel(IComponentRepository compRepo, ICategoryRepository catRepo, DialogService dialog)
        {
            _componentRepo = compRepo;
            _categoryRepo = catRepo;
            _dialog = dialog;
        }

        [RelayCommand]
        private async Task AddAsync()
        {
            if (!ValidationHelper.HasValues(Name, CommonName, CategoryName, Value, Unit))
            {
                await _dialog.ShowMessageAsync("Validation", "Please fill in all fields.");
                return;
            }

            if (!decimal.TryParse(Value.Replace("k", "000"), out var val) || !int.TryParse(Quantity, out var qty))
            {
                await _dialog.ShowMessageAsync("Validation", "Invalid numeric values.");
                return;
            }

            var category = await _categoryRepo.GetByNameAsync(CategoryName) ?? new Category(CategoryName, "");
            var spec = new ComponentSpecification(val, Unit);
            var component = new Component(Name, CommonName, category, spec, qty);
            await _componentRepo.AddAsync(component);
            await _dialog.ShowMessageAsync("Success", $"{Name} added successfully.");
        }
    }
}
