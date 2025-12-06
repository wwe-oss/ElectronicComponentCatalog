using System;
using System.Collections.Generic;
using System.Linq;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Tests.Utilities
{
    /// <summary>
    /// Builds deterministic sample data sets for tests.
    /// </summary>
    public static class TestDataBuilder
    {
        private static readonly (string Name, string Description)[] CategoryDefinitions =
        {
            ("Resistors", "Fixed and variable resistors"),
            ("Capacitors", "Electrolytic, ceramic, film"),
            ("Switches", "Toggle, push button, rotary")
        };

        public static List<Category> GetCategories() => CreateCategories();

        public static List<Component> GetComponents(IEnumerable<Category>? categories = null)
        {
            var resolvedCategories = categories?.ToList() ?? CreateCategories();
            var resistorCategory = resolvedCategories.First(c => c.Name == "Resistors");
            var capacitorCategory = resolvedCategories.First(c => c.Name == "Capacitors");

            var resistorSpec = new ComponentSpecification("10k", "Ohm");
            var capacitorSpec = new ComponentSpecification("100nF", "F");

            return new List<Component>
            {
                new Component("Resistor 10k", "R10K", resistorCategory, resistorSpec, quantityOnHand: 50),
                new Component("Capacitor 100nF", "C100nF", capacitorCategory, capacitorSpec, quantityOnHand: 75)
            };
        }

        public static List<InventoryRecord> GetInventoryRecords(IEnumerable<Component>? components = null)
        {
            var resolvedComponents = components?.ToList() ?? GetComponents();
            if (resolvedComponents.Count < 2)
                resolvedComponents.AddRange(GetComponents());

            var first = resolvedComponents[0];
            var second = resolvedComponents[1];

            return new List<InventoryRecord>
            {
                new InventoryRecord(first.Id, first.QuantityOnHand),
                new InventoryRecord(second.Id, second.QuantityOnHand)
            };
        }

        private static List<Category> CreateCategories() => CategoryDefinitions
            .Select(def => new Category(def.Name, def.Description))
            .ToList();
    }
}
