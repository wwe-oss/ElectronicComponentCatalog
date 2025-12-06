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
                /// <summary>
                /// Todo: Removed Var in favor of type? not sure what that does yet though.
                /// Todo: Changed value in ComponentSPecification fron int to string
                /// </summary>
                /// <returns></returns>
                Category? category = new Category("Resistors", description: "Standard resistors");
                ComponentSpecification? spec = new ComponentSpecification(value: "1000", unit: "Ohm");
                Component? comp = new Component(name: "1k Resistor", commonName: "Resistor", category: category, specification: spec, quantityOnHand: 10);

                comp.Name.Should().Be(expected: "1k Resistor");
                comp.QuantityOnHand.Should().Be(expected: 10);
                comp.Specification.Value.Should().Be(expected: "1000");
                _helper.Diagnostics.WriteInfo(message: "Component created successfully and validated.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(testName: nameof(Component_Create_Success), ex: ex);
                throw;
            }
        }

        [Fact(DisplayName = "Component creation fails with invalid data")]
        [Trait(name: "Category", value: "Domain")]
        public void Component_Create_InvalidData_Throws()
        {
            try
            {
                /// <summary>
                /// Todo: Changed -10 to string and added identifers or what ever they are called
                /// </summary>
                /// <returns></returns>
                var category = new Category(name: "Capacitors", description: "Standard caps");
                Action act = () => new Component(name: "", commonName: "Cap", category: category, specification: new ComponentSpecification(value: "-10", unit: "F"), quantityOnHand: 0);
                act.Should().Throw<ArgumentException>();
                _helper.Diagnostics.WriteInfo(message: "Invalid component creation correctly threw exception.");
            }
            catch (Exception ex)
            {
                _helper.Diagnostics.WriteDiagnostic(testName: nameof(Component_Create_InvalidData_Throws), ex: ex);
                throw;
            }
        }
    }
}
