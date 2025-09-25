using ct.backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct.backend.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int ReviewId { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;

        public byte Rating { get; set; }
        public string? Content { get; set; }
        public int LikesCount { get; set; } = 0;
        public int DislikesCount { get; set; } = 0;

        public ReviewVisibility Visibility { get; set; } = ReviewVisibility.@public;
        public ReviewStatus Status { get; set; } = ReviewStatus.approved;

        [MaxLength(20)] 
        public string Source { get; set; } = "web";
    }
}
