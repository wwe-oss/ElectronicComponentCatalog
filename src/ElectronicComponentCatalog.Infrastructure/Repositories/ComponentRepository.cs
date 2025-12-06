using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for Component entity.
    /// </summary>
    public class ComponentRepository : RepositoryBase<Component>, IComponentRepository
    {
        public ComponentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Component>> GetByCategoryAsync(Guid categoryId) =>
            await _dbSet.Where(c => c.Category.Id == categoryId).ToListAsync();

        public async Task<IEnumerable<Component>> SearchByNameAsync(string query) =>
            await _dbSet.Where(c => EF.Functions.Like(c.Name, $"%{query}%") ||
                                    EF.Functions.Like(c.CommonName, $"%{query}%"))
                        .ToListAsync();
    }
}
