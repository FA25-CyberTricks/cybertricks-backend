using ct_backend.Domain.Entities;
using ct_backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> b)
        {
            b.ToTable("Stores");
            b.HasKey(x => x.StoreId);

            b.Property(x => x.BrandId).IsRequired();
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
            b.Property(x => x.Address).HasMaxLength(500);
            b.Property(x => x.ContactPhone).HasMaxLength(50);
            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20)
             .HasDefaultValue(StoreStatus.active);
            b.Property(x => x.DisplayOrder).HasDefaultValue(0);

            b.HasIndex(x => new { x.BrandId, x.Name }); // tìm kiếm nhanh theo brand + tên

            b.HasMany(x => x.Images)
             .WithOne(i => i.Store)
             .HasForeignKey(i => i.StoreId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Floors)
             .WithOne(f => f.Store)
             .HasForeignKey(f => f.StoreId)
             .OnDelete(DeleteBehavior.Cascade);

            b.ConfigureTimestamps();
        }
    }
}
