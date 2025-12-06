using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
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

        public async Task<string> SaveDatabasePathAsync(string databasePath)
        {
            if (string.IsNullOrWhiteSpace(databasePath))
            {
                throw new ArgumentException("Database path cannot be empty.", nameof(databasePath));
            }

            var normalizedPath = Path.GetFullPath(databasePath.Trim());
            var directory = Path.GetDirectoryName(normalizedPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(path: directory))
            {
                Directory.CreateDirectory(path: directory);
            }

            var configFilePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            var configDirectory = Path.GetDirectoryName(path: configFilePath);
            if (!string.IsNullOrEmpty(configDirectory))
            {
                Directory.CreateDirectory(path: configDirectory);
            }
            var rawJson = File.Exists(configFilePath)
                ? await File.ReadAllTextAsync(configFilePath)
                : "{}";
            var root = JsonNode.Parse(string.IsNullOrWhiteSpace(rawJson) ? "{}" : rawJson) as JsonObject ?? new JsonObject();
            var databaseNode = root["Database"] as JsonObject ?? new JsonObject();

            databaseNode["Path"] = normalizedPath;
            databaseNode["ConnectionString"] = $"Data Source={normalizedPath}";
            root["Database"] = databaseNode;

            var options = new JsonSerializerOptions { WriteIndented = true };
            await File.WriteAllTextAsync(configFilePath, root.ToJsonString(options));
            return normalizedPath;
        }
    }
}
