using System;
using System.Collections.Generic;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities
{
    /// <summary>
    /// Builds deterministic sample data sets for tests.
    /// </summary>
    public static class TestDataBuilder
    {
        public static List<Category> GetCategories() => new()
        {
            new Category { Id = 1, Name = "Resistors", Description = "Fixed and variable resistors" },
            new Category { Id = 2, Name = "Capacitors", Description = "Electrolytic, ceramic, film" },
            new Category { Id = 3, Name = "Switches", Description = "Toggle, push button, rotary" }
        };

        public static List<Component> GetComponents()
        {
            var resistorSpec = new ComponentSpecification("10k", "Ohm");
            var capacitorSpec = new ComponentSpecification("100nF", "F");

            return new()
            {
                new Component
                {
                    Id = 1,
                    Name = "Resistor 10k",
                    CommonName = "R10K",
                    Specification = resistorSpec,
                    CategoryId = 1,
                    QuantityOnHand = 50
                },
                new Component
                {
                    Id = 2,
                    Name = "Capacitor 100nF",
                    CommonName = "C100nF",
                    Specification = capacitorSpec,
                    CategoryId = 2,
                    QuantityOnHand = 75
                }
            };
        }

        public static List<InventoryRecord> GetInventoryRecords() => new()
        {
            new InventoryRecord { Id = 1, ComponentId = 1, Count = 50, LastUpdated = DateTime.UtcNow },
            new InventoryRecord { Id = 2, ComponentId = 2, Count = 75, LastUpdated = DateTime.UtcNow }
        };
    }
}
