using System;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Enums;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents electrical specifications for a component.
    /// </summary>
    public record ComponentSpecification
    {
        /// <summary>
        /// Todo: Changed Value fron decimal to String since string values will be supplied
        /// </summary>
        /// <value></value>
        public String Value { get; init; }
        public string Unit { get; init; }
        public decimal? Tolerance { get; init; }
        public decimal? VoltageRating { get; init; }
        public decimal? PowerRating { get; init; }
        /// <summary>
        /// Todo: Changed value compaison to a string and adjusted comparison
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        /// <param name="tolerance"></param>
        /// <param name="voltageRating"></param>
        /// <param name="powerRating"></param>
        public ComponentSpecification(String value, string unit, decimal? tolerance = null, decimal? voltageRating = null, decimal? powerRating = null)
        {
            if (value == "") throw new ArgumentOutOfRangeException(nameof(value));
            if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentNullException(nameof(unit));
            Value = value;
            Unit = unit;
            Tolerance = tolerance;
            VoltageRating = voltageRating;
            PowerRating = powerRating;
        }
    }
}
