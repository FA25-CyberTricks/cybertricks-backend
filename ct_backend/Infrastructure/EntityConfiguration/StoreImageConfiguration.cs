using ct_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class StoreImageConfiguration : IEntityTypeConfiguration<StoreImage>
    {
        public void Configure(EntityTypeBuilder<StoreImage> b)
        {
            b.ToTable("StoreImages");
            b.HasKey(x => x.ImageId);

            b.Property(x => x.Url).HasMaxLength(500).IsRequired();
            b.Property(x => x.Caption).HasMaxLength(200);
            b.Property(x => x.SortOrder).HasDefaultValue(0);
            b.Property(x => x.IsCover).HasDefaultValue(false);

            b.HasIndex(x => new { x.StoreId, x.IsCover });

            b.ConfigureTimestamps();
        }
    }
}
