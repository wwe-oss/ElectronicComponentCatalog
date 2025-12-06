using System;
using System.Threading.Tasks;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Repositories
{
    /// <summary>
    /// Handles inventory adjustments and records.
    /// </summary>
    public class InventoryRepository : IInventoryService
    {
        private readonly AppDbContext _context;

        public InventoryRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddStockAsync(Guid componentId, int amount, string? reason = null)
        {
            var component = await _context.Components.FindAsync(componentId);
            if (component == null) throw new InvalidOperationException("Component not found.");
            component.IncrementStock(amount);
            _context.InventoryRecords.Add(new Domain.Entities.InventoryRecord(componentId, amount, reason));
            await _context.SaveChangesAsync();
        }

        public async Task RemoveStockAsync(Guid componentId, int amount, string? reason = null)
        {
            var component = await _context.Components.FindAsync(componentId);
            if (component == null) throw new InvalidOperationException("Component not found.");
            component.DecrementStock(amount);
            _context.InventoryRecords.Add(new Domain.Entities.InventoryRecord(componentId, -amount, reason));
            await _context.SaveChangesAsync();
        }
    }
}
