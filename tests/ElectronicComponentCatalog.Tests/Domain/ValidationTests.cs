using System;
using Xunit;
using FluentAssertions;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Exceptions;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Domain
{
    public class ValidationTests
    {
        [Fact]
        public void Component_Should_Throw_When_Name_Is_Null()
        {
            Action act = () => new Component
            {
                Name = null!,
                Specification = new ComponentSpecification("10k", "Ohm"),
                QuantityOnHand = 5
            };

            act.Should().Throw<ValidationException>()
               .WithMessage("*Name*");
        }

        [Fact]
        public void Specification_Should_Throw_When_Value_Is_Invalid()
        {
            Action act = () => new ComponentSpecification("", "Ohm");
            act.Should().Throw<ValidationException>()
               .WithMessage("*Value*");
        }

        [Fact]
        public void InventoryRecord_Should_Have_Valid_Defaults()
        {
            var record = new InventoryRecord { ComponentId = 1, Count = 10 };
            record.LastUpdated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
        }
    }
}
