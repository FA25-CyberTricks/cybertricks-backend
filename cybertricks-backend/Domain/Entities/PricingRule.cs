using System.ComponentModel.DataAnnotations;
using ct.backend.Domain.Enum;

namespace ct.backend.Domain.Entities
{
    public class PricingRule : BaseEntity
    {
        public int PricingRuleId { get; set; }

        // Mỗi store có thể có nhiều rule giá
        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;

        // Rule có thể áp dụng cho toàn store, hoặc 1 room type, hoặc 1 machine type
        public RoomType? RoomType { get; set; }

        // Giá cơ bản
        [Range(0, 999999)]
        public decimal BasePricePerHour { get; set; }

        // Phụ phí hoặc giảm giá theo giờ
        [Range(0, 24)]
        public int StartHour { get; set; }  // ví dụ 8

        [Range(0, 24)]
        public int EndHour { get; set; }    // ví dụ 18
        public decimal? HourlyMultiplier { get; set; } // ví dụ: 1.2 giờ cao điểm

        // Có thể thêm ngày trong tuần (nếu muốn phân biệt cuối tuần)
        public string? DayOfWeek { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public PricingStatus Status { get; set; } = PricingStatus.Active;
    } 
}
