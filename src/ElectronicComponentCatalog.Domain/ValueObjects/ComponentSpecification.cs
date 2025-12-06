using System;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Enums;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents electrical specifications for a component.
    /// </summary>
    public record ComponentSpecification
    {
        public decimal Value { get; init; }
        public string Unit { get; init; }
        public decimal? Tolerance { get; init; }
        public decimal? VoltageRating { get; init; }
        public decimal? PowerRating { get; init; }

        public ComponentSpecification(decimal value, string unit, decimal? tolerance = null, decimal? voltageRating = null, decimal? powerRating = null)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
            if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentNullException(nameof(unit));
            Value = value;
            Unit = unit;
            Tolerance = tolerance;
            VoltageRating = voltageRating;
            PowerRating = powerRating;
        }
    }
}
