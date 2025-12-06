using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Interfaces;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for Category entity.
    /// </summary>
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<Category?> GetByNameAsync(string name) =>
            await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }
}
