using ct.backend.Domain.Entities;
using ct.backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct.backend.Features.Bookinngs
{
    public class BookingDto
    {
        public int? StoreId { get; set; }
        public virtual Store? Store { get; set; }

        public string? ClientId { get; set; }
        public virtual User? Client { get; set; }

        [MaxLength(30)]
        public string? BookingCode { get; set; }

        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        public BookingStatus? Status { get; set; }
        public decimal? EstimatedAmt { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}