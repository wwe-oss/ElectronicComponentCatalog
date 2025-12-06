using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Repositories;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Services;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;

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
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});

			// Register configuration
			builder.Services.AddSingleton<ConfigurationService>();

			// Configure EF Core SQLite
			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				var config = new ConfigurationService();
				options.UseSqlite(config.GetConnectionString());
			});

			// Register repositories
			builder.Services.AddScoped<IComponentRepository, ComponentRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<IInventoryService, InventoryRepository>();

			return builder.Build();
		}
	}
}
