using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ct.backend.Domain.Entities;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class VoucherUsageConfiguration : IEntityTypeConfiguration<VoucherUsage>
    {
        public void Configure(EntityTypeBuilder<VoucherUsage> b)
        {
            b.ToTable("VoucherUsages");

            b.HasKey(x => x.VoucherUsageId);

            // FK
            b.HasOne(x => x.Voucher)
             .WithMany() // nếu Voucher có ICollection<VoucherUsage> thì thêm
             .HasForeignKey(x => x.VoucherId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.User)
             .WithMany() // nếu User có ICollection<VoucherUsage> thì thêm
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            // Mỗi user chỉ được dùng 1 voucher 1 lần
            b.HasIndex(x => new { x.VoucherId, x.UserId }).IsUnique();

            b.Property(x => x.UsedAt).HasColumnType("datetime");
        }
    }
}
