using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities;
using Xunit.Abstractions;
using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Domain
{
    public class ComponentTests
    {
        private readonly TestHelper _helper;

        public ComponentTests(ITestOutputHelper output) => _helper = new TestHelper(output);

        [Fact(DisplayName = "Component can be created successfully")]
        [Trait("Category", "Domain")]
        public void Component_Create_Success()
        {
            try
            {
                var category = new Category("Resistors", "Standard resistors");
                var spec = new ComponentSpecification(1000, "Ohm");
                var comp = new Component("1k Resistor", "Resistor", category, spec, 10);

                comp.Name.Should().Be("1k Resistor");
                comp.QuantityOnHand.Should().Be(10);
                comp.Specification.Value.Should().Be(1000);
                _helper.Diagnostics.WriteInfo("Component created successfully and validated.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Component_Create_Success), ex);
                throw;
            }
        }

        [Fact(DisplayName = "Component creation fails with invalid data")]
        [Trait("Category", "Domain")]
        public void Component_Create_InvalidData_Throws()
        {
            try
            {
                var category = new Category("Capacitors", "Standard caps");
                Action act = () => new Component("", "Cap", category, new ComponentSpecification(-10, "F"), 0);
                act.Should().Throw<ArgumentException>();
                _helper.Diagnostics.WriteInfo("Invalid component creation correctly threw exception.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(nameof(Component_Create_InvalidData_Throws), ex);
                throw;
            }
        }
    }
}
