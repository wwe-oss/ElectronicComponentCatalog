using System;
using System.Text;
using Xunit.Abstractions;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities
{
    /// <summary>
    /// Formats and outputs structured diagnostic information for tests.
    /// </summary>
    public class DiagnosticOutputFormatter
    {
        private readonly ITestOutputHelper _output;

        public DiagnosticOutputFormatter(ITestOutputHelper output) => _output = output;

        public void WriteDiagnostic(string testName, Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine("[ELECTRONICCOMPONENTCATALOG-ERROR]");
            sb.AppendLine($"Test: {testName}");
            sb.AppendLine($"Time: {DateTime.UtcNow:O}");
            sb.AppendLine($"Exception: {ex.GetType().Name} — {ex.Message}");
            if (ex.InnerException != null)
                sb.AppendLine($"Inner: {ex.InnerException.GetType().Name} — {ex.InnerException.Message}");
            sb.AppendLine("StackTrace:");
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine("[END-REPORT]");
            _output.WriteLine(sb.ToString());
        }

        public void WriteInfo(string message)
        {
            _output.WriteLine($"[ELECTRONICCOMPONENTCATALOG-INFO] {DateTime.UtcNow:O} — {message}");
        }
    }
}
