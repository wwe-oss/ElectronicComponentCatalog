﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Repositories;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Services;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests
{
    /// <summary>
    /// Configures the shared DI container for xUnit tests.
    /// </summary>
    public static class TestStartup
    {
        public static ServiceProvider BuildServiceProvider()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json", optional: false)
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);

            // EF Core InMemory for testing
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TestDb"));

            // Register repositories & services
            services.AddScoped<IComponentRepository, ComponentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IInventoryService, InventoryRepository>();
            services.AddSingleton<ConfigurationService>();

            services.AddSingleton<DialogService>();
            services.AddSingleton<NavigationService>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<CatalogViewModel>();
            services.AddTransient<AddPartViewModel>();
            services.AddTransient<BulkAddViewModel>();
            services.AddTransient<SearchViewModel>();
            services.AddTransient<SettingsViewModel>();

            services.AddLogging();

            return services.BuildServiceProvider();
        }
    }
}
