using ct_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> b)
        {
            b.ToTable("MenuItems");
            b.HasKey(x => x.ItemId);

            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
            b.Property(x => x.Price).HasColumnType("decimal(12,2)");
            b.Property(x => x.Active).HasDefaultValue(true);
            b.Property(x => x.Url).HasMaxLength(500).IsRequired();

            b.HasOne(x => x.Brand)
             .WithMany()
             .HasForeignKey(x => x.BrandId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Category)
             .WithMany(c => c.Items)
             .HasForeignKey(x => x.CategoryId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(x => x.OrderItems)
             .WithOne(oi => oi.Item)
             .HasForeignKey(oi => oi.ItemId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.BrandId, x.Name });

            b.ConfigureTimestamps();
        }
    }
}
