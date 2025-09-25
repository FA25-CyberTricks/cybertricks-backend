using ct.backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct.backend.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public int BookingId { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;

        public string ClientId { get; set; } = default!;
        public virtual User Client { get; set; } = default!;

        [MaxLength(30)] 
        public string BookingCode { get; set; } = default!;

        public DateTime StartAt { get; set; } = DateTime.UtcNow;
        public DateTime EndAt { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.reserved;
        public decimal? EstimatedAmt { get; set; }

        [MaxLength(500)] 
        public string? Note { get; set; }

        public virtual IEnumerable<BookingMachine> BookingMachines { get; set; }
    }
}
