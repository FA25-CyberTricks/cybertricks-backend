using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ct.backend.Domain.Entities
{
    public class InvoiceLine : BaseEntity
    {
        public int InvoiceLineId { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; } = default!;

        [MaxLength(300)] 
        public string Description { get; set; } = default!;

        public decimal Qty { get; set; } = 1;
        public decimal UnitPrice { get; set; }

        [NotMapped] 
        public decimal LineTotal => Qty * UnitPrice; // (DB có computed column; ở code tính tạm)
    }
}