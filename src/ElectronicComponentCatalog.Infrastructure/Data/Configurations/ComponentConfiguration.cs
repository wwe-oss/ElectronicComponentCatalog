using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data.Configurations
{
    /// <summary>
    /// EF Core configuration for Component entity.
    /// </summary>
    public class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.ToTable("Components");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(128);
            builder.Property(c => c.CommonName).HasMaxLength(128);
            builder.Property(c => c.QuantityOnHand).IsRequired();
            builder.OwnsOne(c => c.Specification);
            builder.OwnsOne(c => c.PinConfiguration);
            builder.HasOne(c => c.Category)
                   .WithMany(cat => cat.Components)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
