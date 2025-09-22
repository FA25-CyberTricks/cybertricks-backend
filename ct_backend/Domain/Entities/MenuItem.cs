using System.ComponentModel.DataAnnotations;

namespace ct_backend.Domain.Entities
{
    public class MenuItem : BaseEntity
    {
        public int ItemId { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = default!;

        public int? CategoryId { get; set; }
        public virtual MenuCategory? Category { get; set; }

        [MaxLength(500)]
        public string Url { get; set; } = default!;

        [MaxLength(200)] 
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public bool Active { get; set; } = true;

        public  virtual IEnumerable <OrderItem> OrderItems { get; set; }
    }
}
