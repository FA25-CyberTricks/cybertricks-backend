using System.ComponentModel.DataAnnotations;

namespace ct.backend.Domain.Entities
{
    public class Refund : BaseEntity
    {
        public int RefundId { get; set; }

        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; } = default!;

        public decimal Amount { get; set; }

        [MaxLength(200)] 
        public string? Reason { get; set; }

        public DateTime RefundedAt { get; set; }
    }
}