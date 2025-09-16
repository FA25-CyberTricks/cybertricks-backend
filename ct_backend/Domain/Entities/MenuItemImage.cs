using System.ComponentModel.DataAnnotations;

namespace ct_backend.Domain.Entities
{
    public class MenuItemImage : BaseEntity
    {
        public int ImageId { get; set; }

        public int ItemId { get; set; }
        public virtual MenuItem Item { get; set; } = default!;

        [MaxLength(500)] 
        public string Url { get; set; } = default!;

        [MaxLength(200)] 
        public string? Caption { get; set; }

        public int SortOrder { get; set; } = 0;
        public bool IsPrimary { get; set; } = false;
    }
}
