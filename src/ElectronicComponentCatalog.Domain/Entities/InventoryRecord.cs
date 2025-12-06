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
        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
        public string? Reason { get; private set; }
        /// <summary>
        /// Todo: Updated patched method Count
        /// </summary>
        /// <value></value>
        public int Count {get; private set; }
        /// <summary>
        /// Todo: Update patched method LastUpdated
        /// </summary>
        /// <value></value>
        public DateTime LastUpdated {get; private set; }
        /// <summary>
        /// Todo: Update patched method Component
        /// </summary>
        /// <value></value>
        public Component Component {get; private set; }

        public InventoryRecord(Guid componentId, int quantityChange, string? reason = null)
        {
            ComponentId = componentId;
            QuantityChange = quantityChange;
            Reason = reason;
        }
    }
}
