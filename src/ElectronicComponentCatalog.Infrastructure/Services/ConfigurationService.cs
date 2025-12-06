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
                .SetBasePath(basePath: AppContext.BaseDirectory)
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }

        public string GetConnectionString()
        {
            var relativePath = _configuration[key: "Database:Path"] ?? "Data/database/catalog.db";
            var fullPath = Path.GetFullPath(path: relativePath);
            var dir = Path.GetDirectoryName(path: fullPath);
            if (!Directory.Exists(path: dir)) Directory.CreateDirectory(path: dir!);
            return $"Data Source={fullPath}";
        }
    }
}
