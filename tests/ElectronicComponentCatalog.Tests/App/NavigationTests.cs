using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;
using Microsoft.Maui.Controls;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.App
{
    public class NavigationTests
    {
        private readonly TestHelper _helper;

        public NavigationTests(ITestOutputHelper output)
        {
            _helper = new TestHelper(output);
        }

        [Fact(DisplayName = "NavigationService navigates between views correctly")]
        [Trait("Category", "App")]
        public async Task NavigationService_Navigate_Success()
        {
            try
            {
                var nav = new NavigationService(_helper.Services);
                nav.SetContentRegion(new ContentView());
                await nav.NavigateToAsync("CatalogView");
                _helper.Diagnostics.WriteInfo("NavigationService successfully navigated to CatalogView.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(NavigationService_Navigate_Success), ex);
                throw;
            }
        }

        [Fact(DisplayName = "NavigationService gracefully handles invalid route")]
        [Trait("Category", "App")]
        public async Task NavigationService_InvalidRoute_Handled()
        {
            try
            {
                var nav = new NavigationService(_helper.Services);
                nav.SetContentRegion(new ContentView());
                await nav.NavigateToAsync("InvalidViewName");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(NavigationService_InvalidRoute_Handled), ex);
                ex.Should().BeOfType<InvalidOperationException>();
            }
        }
    }
}
