using System.ComponentModel.DataAnnotations;

namespace ct_backend.Features.Stores
{
    public class StoreDto
    {
        public int? StoreId { get; set; }

        public int? BrandId { get; set; }

        [MaxLength(200)]
        public string? Name { get; set; } = default!;

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? ContactPhone { get; set; }
    }
}