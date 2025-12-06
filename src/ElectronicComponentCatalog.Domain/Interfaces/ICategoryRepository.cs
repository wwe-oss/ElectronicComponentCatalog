using System;
using System.Threading.Tasks;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for managing component categories.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetByNameAsync(string name);
    }
}
