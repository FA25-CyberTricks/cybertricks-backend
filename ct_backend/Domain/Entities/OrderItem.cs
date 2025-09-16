using System.ComponentModel.DataAnnotations;

namespace ct_backend.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = default!;

        public int ItemId { get; set; }
        public virtual MenuItem Item { get; set; } = default!;

        public int Qty { get; set; } = 1;
        public decimal UnitPrice { get; set; }

        [MaxLength(200)] public string? Note { get; set; }
    }
}