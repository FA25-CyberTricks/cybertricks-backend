using System.ComponentModel.DataAnnotations;
using ct.backend.Domain.Enum;

namespace ct.backend.Domain.Entities
{
    public class Voucher : BaseEntity
    {
        public int VoucherId { get; set; }

        [MaxLength(50)]
        public string Code { get; set; } = default!;   // Mã voucher (duy nhất)

        [MaxLength(500)]
        public string Description { get; set; } = default!; // Mô tả

        // Áp dụng cho toàn hệ thống hay 1 store cụ thể
        public int? StoreId { get; set; }
        public virtual Store? Store { get; set; }

        // Thời gian hiệu lực
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Giảm theo số tiền (VND) HOẶC theo %
        [Range(0, 999999)]
        public decimal? DiscountAmount { get; set; }   // vd: 20000

        [Range(0, 100)]
        public decimal? DiscountPercent { get; set; }  // vd: 10 = 10%

        // Ràng buộc dùng thêm (tuỳ chọn)
        [Range(0, 999999)]
        public decimal? MinOrderAmount { get; set; }   // ĐH tối thiểu

        [Range(0, 999999)]
        public decimal? MaxDiscountAmount { get; set; } // trần giảm giá khi dùng %

        public int? UsageLimit { get; set; }           // Số lần được dùng (null = không giới hạn)
        public int UsedCount { get; set; } = 0;        // Đã dùng

        public VoucherStatus Status { get; set; } = VoucherStatus.Active;
    }
}
