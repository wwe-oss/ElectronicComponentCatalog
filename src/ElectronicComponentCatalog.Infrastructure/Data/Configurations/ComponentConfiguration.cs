using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Domain.Entities;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.Infrastructure.Data.Configurations
{
    public class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.ToTable("Components");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.CommonName)
                   .HasMaxLength(100);

            builder.OwnsOne(c => c.Specification, spec =>
            {
                spec.Property(s => s.Value).HasColumnName("Value");
                spec.Property(s => s.Unit).HasColumnName("Unit").HasMaxLength(25);
            });

            builder.Property(c => c.QuantityOnHand)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.HasOne(c => c.Category)
                   .WithMany(cat => cat.Components)
                   .HasForeignKey(c => c.CategoryId);
        }
    }
}
