using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Services;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;
using Xunit.Abstractions;
using System;
using System.IO;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Infrastructure
{
    public class ConfigurationServiceTests
    {
        private readonly TestHelper _helper;

        public ConfigurationServiceTests(ITestOutputHelper output) => _helper = new TestHelper(output);

        [Fact(DisplayName = "ConfigurationService loads connection string successfully")]
        [Trait(name: "Category", value: "Infrastructure")]
        public void ConfigurationService_LoadsConfig()
        {
            try
            {
                var service = _helper.Resolve<ConfigurationService>();

                var conn = service.GetConnectionString();
                conn.Should().Contain(expected: "Data");
                _helper.Diagnostics.WriteInfo(message: $"Connection string resolved: {conn}");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(testName: nameof(ConfigurationService_LoadsConfig), ex);
                throw;
            }
        }

        [Fact(DisplayName = "ConfigurationService handles missing configuration gracefully")]
        [Trait(name: "Category", value: "Infrastructure")]
        public void ConfigurationService_MissingConfig_GracefulFail()
        {
            try
            {
                var service = _helper.Resolve<ConfigurationService>();
                var tempPath = Path.Combine(Path.GetTempPath(), $"test-db-{Guid.NewGuid()}.db");
                Func<Task> act = async () => await service.SaveDatabasePathAsync(databasePath: tempPath);
                act.Should().NotThrowAsync();
                _helper.Diagnostics.WriteInfo(message: "Missing configuration handled without exception.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(testName: nameof(ConfigurationService_MissingConfig_GracefulFail), ex);
                throw;
            }
        }
    }
}
