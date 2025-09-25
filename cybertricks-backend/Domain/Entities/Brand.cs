using ct.backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct.backend.Domain.Entities
{
    public class Brand : BaseEntity
    {
        public int BrandId { get; set; }

        [MaxLength(50)] 
        public string Code { get; set; } = default!;

        [MaxLength(200)] 
        public string Name { get; set; } = default!;

        [MaxLength(255)] 
        public string? ContactEmail { get; set; }

        [MaxLength(50)] 
        public string? ContactPhone { get; set; }

        public string? Description { get; set; }
        public double AvgRating { get; set; } = 0.0;
        public int RatingCount { get; set; } = 0;

        public BrandStatus Status { get; set; } = BrandStatus.inactive;

        public virtual IEnumerable<Store> Stores { get; set; }
        public virtual IEnumerable<MenuCategory> MenuCategorys { get; set; }
    }
}
