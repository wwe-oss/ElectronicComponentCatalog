using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Views;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services
{
    /// <summary>
    /// Handles runtime navigation within the main shell.
    /// </summary>
    public class NavigationService
    {
        private readonly IServiceProvider _provider;
        private ContentView? _contentRegion;
        private readonly Dictionary<string, Func<ContentPage>> _routes;

        public NavigationService(IServiceProvider provider)
        {
            _provider = provider;
            _routes = new()
            {
                { "CatalogView", () => ActivatorUtilities.CreateInstance<CatalogView>(_provider) },
                { "AddPartView", () => ActivatorUtilities.CreateInstance<AddPartView>(_provider) },
                { "BulkAddView", () => ActivatorUtilities.CreateInstance<BulkAddView>(_provider) },
                { "SearchView", () => ActivatorUtilities.CreateInstance<SearchView>(_provider) },
                { "SettingsView", () => ActivatorUtilities.CreateInstance<SettingsView>(_provider) }
            };
        }

        public void SetContentRegion(ContentView region) => _contentRegion = region;

        public async Task NavigateToAsync(string route)
        {
            if (_contentRegion == null || !_routes.ContainsKey(route))
                return;

            var page = _routes[route]();
            _contentRegion.Content = page.Content;
            await Task.CompletedTask;
        }
    }
}
