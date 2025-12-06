using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data
{
    /// <summary>
    /// Design-time factory for creating the AppDbContext instance
    /// used by EF Core CLI tools for migrations and updates.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Look for the configuration in the App project directory
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../ElectronicComponentCatalog.App");
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("Configuration/appsettings.json", optional: false)
                .AddJsonFile("Configuration/appsettings.Development.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection") ??
                                   $"Data Source={Path.Combine(basePath, "../../Data/Database/catalog.db")}";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            Console.WriteLine($"[INFO] Using database: {connectionString}");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
