using ct_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class MenuItemImageConfiguration : IEntityTypeConfiguration<MenuItemImage>
    {
        public void Configure(EntityTypeBuilder<MenuItemImage> b)
        {
            b.ToTable("MenuItemImages");
            b.HasKey(x => x.ImageId);

            b.Property(x => x.Url).HasMaxLength(500).IsRequired();
            b.Property(x => x.Caption).HasMaxLength(200);
            b.Property(x => x.SortOrder).HasDefaultValue(0);
            b.Property(x => x.IsPrimary).HasDefaultValue(false);

            b.HasIndex(x => new { x.ItemId, x.IsPrimary });

            b.ConfigureTimestamps();
        }
    }
}
