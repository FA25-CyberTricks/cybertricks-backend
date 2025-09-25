using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
    {
        public void Configure(EntityTypeBuilder<MenuCategory> b)
        {
            b.ToTable("MenuCategories");
            b.HasKey(x => x.CategoryId);

            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
            b.Property(x => x.Active).HasDefaultValue(true);

            b.HasOne(x => x.Brand)
              .WithMany(x => x.MenuCategorys)
              .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Items)
             .WithOne(i => i.Category)
             .HasForeignKey(i => i.CategoryId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasIndex(x => new { x.BrandId, x.Name });

            b.ConfigureTimestamps();
        }
    }
}
