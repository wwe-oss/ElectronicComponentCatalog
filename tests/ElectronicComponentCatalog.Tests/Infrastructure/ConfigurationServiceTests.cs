using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Services;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;
using Xunit.Abstractions;
using System;
using Microsoft.Extensions.Configuration;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Infrastructure
{
    public class ConfigurationServiceTests
    {
        private readonly TestHelper _helper;

        public ConfigurationServiceTests(ITestOutputHelper output) => _helper = new TestHelper(output);

        [Fact(DisplayName = "ConfigurationService loads connection string successfully")]
        [Trait("Category", "Infrastructure")]
        public void ConfigurationService_LoadsConfig()
        {
            try
            {
                var config = _helper.Resolve<IConfiguration>();
                var service = new ConfigurationService(config);

                var conn = service.GetConnectionString();
                conn.Should().Contain("Data");
                _helper.Diagnostics.WriteInfo($"Connection string resolved: {conn}");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(ConfigurationService_LoadsConfig), ex);
                throw;
            }
        }

        [Fact(DisplayName = "ConfigurationService handles missing configuration gracefully")]
        [Trait("Category", "Infrastructure")]
        public void ConfigurationService_MissingConfig_GracefulFail()
        {
            try
            {
                var fakeConfig = new ConfigurationBuilder().AddInMemoryCollection().Build();
                var service = new ConfigurationService(fakeConfig);

                Action act = () => service.GetConnectionString();
                act.Should().NotThrow();
                _helper.Diagnostics.WriteInfo("Missing configuration handled without exception.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(ConfigurationService_MissingConfig_GracefulFail), ex);
                throw;
            }
        }
    }
}
