using ct.backend.Domain.Entities;
using ct.backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> b)
        {
            b.ToTable("Reviews");
            b.HasKey(x => x.ReviewId);

            b.Property(x => x.Rating).IsRequired(); // byte
            b.Property(x => x.Content);
            b.Property(x => x.LikesCount).HasDefaultValue(0);
            b.Property(x => x.DislikesCount).HasDefaultValue(0);
            b.Property(x => x.Source).HasMaxLength(20).HasDefaultValue("web");

            b.Property(x => x.Visibility).AsStringEnum().HasMaxLength(20);
            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20);

            b.HasOne(x => x.Brand)
             .WithMany()
             .HasForeignKey(x => x.BrandId)
             .OnDelete(DeleteBehavior.Cascade);

            // ⚠️ UserId Guid ↔ User.Id string
            b.HasOne(x => x.User)
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.BrandId, x.UserId });

            b.ConfigureTimestamps();
        }
    }
}
