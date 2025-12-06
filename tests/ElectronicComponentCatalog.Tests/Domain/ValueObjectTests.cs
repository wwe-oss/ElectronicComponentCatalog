using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;
using Xunit.Abstractions;
using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Domain
{
    public class ValueObjectTests
    {
        private readonly TestHelper _helper;

        public ValueObjectTests(ITestOutputHelper output) => _helper = new TestHelper(output);

        [Fact(DisplayName = "ComponentSpecification stores and compares values correctly")]
        [Trait("Category", "Domain")]
        public void Specification_Value_Comparison()
        {
            try
            {
                var s1 = new ComponentSpecification(4700, "Ohm");
                var s2 = new ComponentSpecification(4700, "Ohm");

                s1.Should().BeEquivalentTo(s2);
                s1.Unit.Should().Be("Ohm");
                _helper.Diagnostics.WriteInfo("Specification comparison passed.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Specification_Value_Comparison), ex);
                throw;
            }
        }

        [Fact(DisplayName = "Invalid specification throws exception")]
        [Trait("Category", "Domain")]
        public void Specification_Invalid_Throws()
        {
            try
            {
                Action act = () => new ComponentSpecification(-5, "V");
                act.Should().Throw<ArgumentException>();
                _helper.Diagnostics.WriteInfo("Negative specification correctly threw exception.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Specification_Invalid_Throws), ex);
                throw;
            }
        }
    }
}
