using System.ComponentModel.DataAnnotations;

namespace ct.backend.Domain.Entities
{
    public class MenuCategory : BaseEntity
    {
        public int CategoryId { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = default!;

        [MaxLength(200)] 
        public string Name { get; set; } = default!;
        public bool Active { get; set; } = true;

        public virtual IEnumerable<MenuItem> Items { get; set; }
    }
}
