using ct_backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct_backend.Features.Bookinngs
{
    public class UpdateBookingRequest : AbstractRequest
    {
        //public int? StoreId { get; set; }

        //public string? ClientId { get; set; } = default!;

        [MaxLength(30)]
        public string? BookingCode { get; set; } = default!;

        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        public BookingStatus? Status { get; set; } = BookingStatus.reserved;
        public decimal? EstimatedAmt { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}