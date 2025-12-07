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
        public int Count { get; private set; }
        public Component Component { get; private set; } = null!;
        public DateTime LastUpdated { get; private set; } = DateTime.UtcNow;
        public string? Reason { get; private set; }
        

        public InventoryRecord(Guid componentId, int count, string? reason = null)
        {
            ComponentId = componentId;
            Count = count;
            Reason = reason;
        }
    }
}
