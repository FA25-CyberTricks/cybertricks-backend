using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> b)
        {
            b.ToTable("Vouchers");

            b.HasKey(x => x.VoucherId);

            // Code duy nhất (có thể thêm StoreId vào index nếu muốn unique theo Store)
            b.HasIndex(x => x.Code).IsUnique();

            // FK -> Store (nullable: voucher toàn hệ thống)
            b.HasOne(x => x.Store)
             .WithMany() // nếu Store có ICollection<Voucher> thì .WithMany(s => s.Vouchers)
             .HasForeignKey(x => x.StoreId)
             .OnDelete(DeleteBehavior.Cascade);

            // Kiểu cột
            b.Property(x => x.StartDate).HasColumnType("datetime");
            b.Property(x => x.EndDate).HasColumnType("datetime");

            b.Property(x => x.DiscountAmount).HasColumnType("decimal(10,2)");
            b.Property(x => x.DiscountPercent).HasColumnType("decimal(5,2)");
            b.Property(x => x.MinOrderAmount).HasColumnType("decimal(10,2)");
            b.Property(x => x.MaxDiscountAmount).HasColumnType("decimal(10,2)");

            // Enum string
            b.Property(x => x.Status).AsStringEnum();

            // Timestamps (BaseEntity)
            b.ConfigureTimestamps();

            // Index hay dùng để tra cứu nhanh
            b.HasIndex(x => new { x.StoreId, x.Status, x.StartDate, x.EndDate });

            // Check constraints (Pomelo MySQL)
            // 1) Chỉ được chọn MỘT loại giảm giá (amount XOR percent) và phải > 0
            b.ToTable(t =>
            {
                t.HasCheckConstraint("ck_voucher_discount_one",
                    @"(
                      (DiscountAmount IS NOT NULL AND DiscountAmount > 0 AND (DiscountPercent IS NULL OR DiscountPercent = 0))
                      OR (DiscountPercent IS NOT NULL AND DiscountPercent > 0 AND DiscountAmount IS NULL)
                    )");

                t.HasCheckConstraint("ck_voucher_percent_range",
                    @"(DiscountPercent IS NULL OR (DiscountPercent >= 0 AND DiscountPercent <= 100))");

                t.HasCheckConstraint("ck_voucher_dates",
                    @"(EndDate >= StartDate)");

                t.HasCheckConstraint("ck_voucher_usage_limit",
                    @"(UsageLimit IS NULL OR UsageLimit >= UsedCount)");
            });
        }
    }
}
