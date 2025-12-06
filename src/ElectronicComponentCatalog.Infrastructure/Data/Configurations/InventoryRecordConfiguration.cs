using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data.Configurations
{
    /// <summary>
    /// EF Core configuration for InventoryRecord entity.
    /// </summary>
    public class InventoryRecordConfiguration : IEntityTypeConfiguration<InventoryRecord>
    {
        public void Configure(EntityTypeBuilder<InventoryRecord> builder)
        {
            builder.ToTable("InventoryRecords");
            builder.HasKey(ir => ir.Id);
            builder.Property(ir => ir.QuantityChange).IsRequired();
            builder.Property(ir => ir.Timestamp).IsRequired();
        }
    }
}
