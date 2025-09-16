using ct_backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct_backend.Domain.Entities
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
        public double AvgRating { get; set; }
        public int RatingCount { get; set; }

        public BrandStatus Status { get; set; } = BrandStatus.active;

        public virtual IEnumerable<Store> Stores { get; set; }
        public virtual IEnumerable<MenuCategory> MenuCategorys { get; set; }
    }
}
