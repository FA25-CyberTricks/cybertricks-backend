using ct.backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct.backend.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public int InvoiceId { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;

        public int? BookingId { get; set; }    // 1 booking ↔ 1 invoice (open)
        public virtual Booking? Booking { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        [MaxLength(50)] 
        public string InvoiceNo { get; set; } = default!;

        public DateTime IssueDate { get; set; }

        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }

        [MaxLength(3)] 
        public string Currency { get; set; } = "VND";
        public InvoiceStatus Status { get; set; } = InvoiceStatus.open;

        public virtual IEnumerable<InvoiceLine> Lines { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
    }
}