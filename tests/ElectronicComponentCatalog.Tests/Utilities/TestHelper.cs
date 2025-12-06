using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities
{
    /// <summary>
    /// Provides access to common test dependencies and diagnostics.
    /// </summary>
    public class TestHelper
    {
        public IServiceProvider Services { get; }
        public DiagnosticOutputFormatter Diagnostics { get; }

        public TestHelper(ITestOutputHelper output)
        {
            Services = TestStartup.BuildServiceProvider();
            Diagnostics = new DiagnosticOutputFormatter(output);
        }

        public T Resolve<T>() where T : notnull => Services.GetRequiredService<T>();
    }
}
