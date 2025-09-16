using ct_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class BrandOwnerConfiguration : IEntityTypeConfiguration<BrandOwner>
    {
        public void Configure(EntityTypeBuilder<BrandOwner> b)
        {
            b.ToTable("BrandOwners");
            b.HasKey(x => x.BrandOwnerId);

            b.Property(x => x.UserId)
             .IsRequired()
             .HasMaxLength(255);

            b.HasOne(x => x.Brand)
             .WithMany() // hoặc .WithOne(br => br.Owner) nếu bạn thêm nav ở Brand
             .HasForeignKey(x => x.BrandId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.User)
             .WithMany(u => u.BrandOwners)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            // ✅ Mỗi brand chỉ có 1 owner
            b.HasIndex(x => x.BrandId).IsUnique();

            // (tuỳ) nếu muốn 1 user chỉ được làm owner tối đa 1 brand, thêm:
            // b.HasIndex(x => x.UserId).IsUnique();

            b.ConfigureTimestamps();
        }
    }
}