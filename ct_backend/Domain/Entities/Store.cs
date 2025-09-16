using ct_backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ct_backend.Domain.Entities
{
    public class Store : BaseEntity
    {
        public int StoreId { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = default!;

        [MaxLength(200)]
        public string Name { get; set; } = default!;

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(50)] 
        public string? ContactPhone { get; set; }

        public StoreStatus Status { get; set; } = StoreStatus.active;
        public int DisplayOrder { get; set; } = 0;

        public virtual IEnumerable<StoreImage> Images { get; set; }
        public virtual IEnumerable<Floor> Floors { get; set; }
        public virtual IEnumerable<Booking> Bookings { get; set; }
    }
}
