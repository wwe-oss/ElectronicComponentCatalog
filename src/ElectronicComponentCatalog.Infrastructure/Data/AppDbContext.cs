using Microsoft.EntityFrameworkCore;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data.Configurations;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data
{
    /// <summary>
    /// EF Core database context for the Electronic Component Catalog.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Component> Components => Set<Component>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<InventoryRecord> InventoryRecords => Set<InventoryRecord>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComponentConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryRecordConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
