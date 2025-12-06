using System;
using System.Collections.Generic;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Enums;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities
{
    /// <summary>
    /// Represents a single electrical component in the catalog.
    /// </summary>
    public class Component
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string CommonName { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }
        public ComponentSpecification Specification { get; private set; }
        public PinConfiguration? PinConfiguration { get; private set; }
        public int QuantityOnHand { get; private set; }
        public string? Notes { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        
        public Component(string name, string commonName, Category category, ComponentSpecification specification, int quantityOnHand = 0, string? notes = null, PinConfiguration? pinConfiguration = null)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            CommonName = commonName ?? throw new ArgumentNullException(paramName: nameof(commonName));
            Category = category ?? throw new ArgumentNullException(paramName: nameof(category));
            CategoryId = category.Id;
            Specification = specification ?? throw new ArgumentNullException(paramName: nameof(specification));
            QuantityOnHand = quantityOnHand >= 0 ? quantityOnHand : throw new ArgumentOutOfRangeException(paramName: nameof(quantityOnHand));
            Notes = notes;
            PinConfiguration = pinConfiguration;
        }

        public void IncrementStock(int amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(paramName: nameof(amount));
            QuantityOnHand += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecrementStock(int amount)
        {
            if (amount <= 0 || amount > QuantityOnHand)
                throw new ArgumentOutOfRangeException(paramName: nameof(amount));
            QuantityOnHand -= amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateNotes(string? notes)
        {
            Notes = notes;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
