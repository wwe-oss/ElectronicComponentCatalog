﻿using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Repositories;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Services;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Views;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.ViewModels;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App
{
	/// <summary>
	/// Configures the MAUI application, dependency injection, and EF Core database.
	/// </summary>
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();

			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont(filename: "OpenSans-Regular.ttf", alias: "OpenSansRegular");
				});

		// Register configuration
		builder.Services.AddSingleton<ConfigurationService>();

		// Configure EF Core SQLite
		builder.Services.AddDbContext<AppDbContext>(options =>
		{
			var config = new ConfigurationService();
			options.UseSqlite(connectionString: config.GetConnectionString());
		});

		// Register repositories
		builder.Services.AddScoped<IComponentRepository, ComponentRepository>();
		builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
		builder.Services.AddScoped<IInventoryService, InventoryRepository>();

		// Register shared services + views
		builder.Services.AddSingleton<DialogService>();
		builder.Services.AddSingleton<NavigationService>();
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<CatalogViewModel>();
		builder.Services.AddTransient<AddPartViewModel>();
		builder.Services.AddTransient<BulkAddViewModel>();
		builder.Services.AddTransient<SearchViewModel>();
		builder.Services.AddTransient<SettingsViewModel>();

			return builder.Build();
		}
	}
}
