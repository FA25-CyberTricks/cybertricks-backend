using ct.backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct.backend.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int PaymentId { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; } = default!;

        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        public PaymentMethod Method { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.captured;

        [MaxLength(100)] 
        public string? ProviderRef { get; set; }
        public DateTime PaidAt { get; set; }

        public virtual IEnumerable<Refund> Refunds { get; set; }
    }
}