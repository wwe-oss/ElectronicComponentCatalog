using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for accessing and managing components.
    /// </summary>
    public interface IComponentRepository : IRepository<Component>
    {
        Task<IEnumerable<Component>> GetByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Component>> SearchByNameAsync(string query);
    }
}
