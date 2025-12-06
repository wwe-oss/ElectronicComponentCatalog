using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Services
{
    /// <summary>
    /// Provides access to application configuration using appsettings.json.
    /// </summary>
    public class ConfigurationService
    {
        private readonly IConfigurationRoot _configuration;

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }

        public string GetConnectionString()
        {
            var relativePath = _configuration["Database:Path"] ?? "Data/database/catalog.db";
            var fullPath = Path.GetFullPath(relativePath);
            var dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir!);
            return $"Data Source={fullPath}";
        }
    }
}
