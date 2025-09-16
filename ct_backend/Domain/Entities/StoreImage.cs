using System.ComponentModel.DataAnnotations;

namespace ct_backend.Domain.Entities
{
    public class StoreImage : BaseEntity
    {
        public int ImageId { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;

        [MaxLength(500)] 
        public string Url { get; set; } = default!;

        [MaxLength(200)] 
        public string? Caption { get; set; }
        public int SortOrder { get; set; } = 0;
        public bool IsCover { get; set; } = false;
    }
}
