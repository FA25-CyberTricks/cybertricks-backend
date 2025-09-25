using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class StoreManagerConfiguration : IEntityTypeConfiguration<StoreManager>
    {
        public void Configure(EntityTypeBuilder<StoreManager> b)
        {
            b.ToTable("StoreManagers");
            b.HasKey(x => x.StoreManagerId);

            b.Property(x => x.UserId)
             .IsRequired()
             .HasMaxLength(255);

            b.HasOne(x => x.Store)
             .WithMany() // hoặc .WithMany(s => s.Managers) nếu bạn thêm nav ở Store
             .HasForeignKey(x => x.StoreId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.User)
             .WithMany() // hoặc .WithMany(u => u.StoreManagers)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            // ✅ Tránh trùng staff trong cùng 1 store
            b.HasIndex(x => new { x.StoreId, x.UserId }).IsUnique();

            b.ConfigureTimestamps();
        }
    }
}
