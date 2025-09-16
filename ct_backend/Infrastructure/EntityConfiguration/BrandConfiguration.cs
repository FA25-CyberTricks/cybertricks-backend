using ct_backend.Domain.Entities;
using ct_backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> b)
        {
            b.ToTable("Brands");
            b.HasKey(x => x.BrandId);

            b.HasIndex(x => x.Code).IsUnique();

            b.Property(x => x.Code).HasMaxLength(50).IsRequired();
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
            b.Property(x => x.ContactEmail).HasMaxLength(255);
            b.Property(x => x.ContactPhone).HasMaxLength(50);
            b.Property(x => x.Description);

            b.Property(x => x.AvgRating).HasDefaultValue(0d);
            b.Property(x => x.RatingCount).HasDefaultValue(0);

            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20)
             .HasDefaultValue(BrandStatus.active);

            // 1 Brand - N Stores
            b.HasMany(x => x.Stores)
             .WithOne(s => s.Brand)
             .HasForeignKey(s => s.BrandId)
             .OnDelete(DeleteBehavior.Cascade);

            b.ConfigureTimestamps();
        }
    }
}
