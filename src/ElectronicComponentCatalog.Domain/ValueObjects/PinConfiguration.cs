using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.ValueObjects
{
    /// <summary>
    /// Represents pin configuration and layout for components.
    /// </summary>
    public record PinConfiguration
    {
        public int PinCount { get; init; }
        public string Layout { get; init; }
        public string? Description { get; init; }

        public PinConfiguration(int pinCount, string layout, string? description = null)
        {
            if (pinCount <= 0) throw new ArgumentOutOfRangeException(nameof(pinCount));
            if (string.IsNullOrWhiteSpace(layout)) throw new ArgumentNullException(nameof(layout));
            PinCount = pinCount;
            Layout = layout;
            Description = description;
        }
    }
}
