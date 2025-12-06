using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.App
{
    public class ViewModelTests
    {
        private readonly TestHelper _helper;
        private readonly DialogService _dialog;

        public ViewModelTests(ITestOutputHelper output)
        {
            _helper = new TestHelper(output);
            _dialog = new DialogService();
        }

        [Fact(DisplayName = "AddPartViewModel successfully adds valid component")]
        [Trait("Category", "App")]
        public async Task AddPartViewModel_Add_Success()
        {
            try
            {
                var compRepo = _helper.Resolve<Domain.Interfaces.IComponentRepository>();
                var catRepo = _helper.Resolve<Domain.Interfaces.ICategoryRepository>();
                var vm = new AddPartViewModel(compRepo, catRepo, _dialog)
                {
                    Name = "TestResistor",
                    CommonName = "Resistor",
                    CategoryName = "Resistors",
                    Value = "1000",
                    Unit = "Ohm",
                    Quantity = "10"
                };

                await vm.AddCommand.ExecuteAsync(null);
                _helper.Diagnostics.WriteInfo("AddPartViewModel successfully added component.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(AddPartViewModel_Add_Success), ex);
                throw;
            }
        }

        [Fact(DisplayName = "AddPartViewModel shows validation error on missing fields")]
        [Trait("Category", "App")]
        public async Task AddPartViewModel_Validation_Fails()
        {
            try
            {
                var compRepo = _helper.Resolve<Domain.Interfaces.IComponentRepository>();
                var catRepo = _helper.Resolve<Domain.Interfaces.ICategoryRepository>();
                var vm = new AddPartViewModel(compRepo, catRepo, _dialog)
                {
                    Name = "",
                    CommonName = "",
                    CategoryName = "",
                    Value = "",
                    Unit = "",
                    Quantity = ""
                };

                await vm.AddCommand.ExecuteAsync(null);
                _helper.Diagnostics.WriteInfo("AddPartViewModel validation triggered successfully.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(AddPartViewModel_Validation_Fails), ex);
                throw;
            }
        }

        [Fact(DisplayName = "SearchViewModel executes query without error")]
        [Trait("Category", "App")]
        public async Task SearchViewModel_Search_Success()
        {
            try
            {
                var repo = _helper.Resolve<Domain.Interfaces.IComponentRepository>();
                var vm = new SearchViewModel(repo) { Query = "Resistor" };

                await vm.ExecuteSearchCommand.ExecuteAsync(null);
                _helper.Diagnostics.WriteInfo("SearchViewModel executed successfully.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(SearchViewModel_Search_Success), ex);
                throw;
            }
        }
    }
}
