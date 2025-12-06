using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities
{
    /// <summary>
    /// Represents a record of inventory changes for a component.
    /// </summary>
    public class InventoryRecord
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid ComponentId { get; private set; }
        public int QuantityChange { get; private set; }
        public DateTime LastUpdated { get; private set; } = DateTime.UtcNow;
        public string? Reason { get; private set; }
        

        public InventoryRecord(Guid componentId, int quantityChange, string? reason = null)
        {
            ComponentId = componentId;
            QuantityChange = quantityChange;
            Reason = reason;
        }
    }
}
