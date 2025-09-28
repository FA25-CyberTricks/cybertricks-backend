using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class StoreStaffConfiguration : IEntityTypeConfiguration<StoreStaff>
    {
        public void Configure(EntityTypeBuilder<StoreStaff> b)
        {
            b.ToTable("StoreStaffs");
            b.HasKey(x => x.StoreStaffId);

            b.Property(x => x.UserId)
             .IsRequired()
             .HasMaxLength(255);

            b.HasOne(x => x.Store)
             .WithMany() // hoặc .WithMany(s => s.Staffs) nếu bạn thêm nav ở Store
             .HasForeignKey(x => x.StoreId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.User)
             .WithMany(u => u.StoreStaffs)    
             .HasForeignKey(x => x.UserId)
             .HasPrincipalKey(u => u.Id)  
             .OnDelete(DeleteBehavior.Restrict);

            // ✅ Tránh trùng staff trong cùng 1 store
            b.HasIndex(x => new { x.StoreId, x.UserId }).IsUnique();

            b.ConfigureTimestamps();
        }
    }
}
