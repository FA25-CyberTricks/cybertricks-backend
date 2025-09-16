using ct_backend.Domain.Enum;

namespace ct_backend.Domain.Entities
{
    public class BrandOwner : BaseEntity
    {
        public int BrandOwnerId { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = default!;

        // IdentityUser.Id là string
        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;

        // Nếu muốn đánh dấu brand chính (tuỳ chọn):
        public bool IsPrimary { get; set; } = false;
    }
}
