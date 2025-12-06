using System;
using System.Threading.Tasks;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces
{
    /// <summary>
    /// Defines operations for managing inventory adjustments and records.
    /// </summary>
    public interface IInventoryService
    {
        Task AddStockAsync(Guid componentId, int amount, string? reason = null);
        Task RemoveStockAsync(Guid componentId, int amount, string? reason = null);
    }
}
