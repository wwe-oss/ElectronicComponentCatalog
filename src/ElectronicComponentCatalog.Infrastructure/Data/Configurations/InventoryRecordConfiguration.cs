using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data.Configurations
{
    public class InventoryRecordConfiguration : IEntityTypeConfiguration<InventoryRecord>
    {
        public void Configure(EntityTypeBuilder<InventoryRecord> builder)
        {
            builder.ToTable("InventoryRecords");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.ComponentId)
                   .IsRequired();

            builder.Property(i => i.Count)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(i => i.LastUpdated)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(i => i.Component)
                   .WithMany()
                   .HasForeignKey(i => i.ComponentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
