using ct.backend.Domain.Enum;

namespace ct.backend.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int OrderId { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;

        public string ClientId { get; set; } = default!;
        public virtual User Client { get; set; } = default!;

        public int? BookingId { get; set; }
        public virtual Booking? Booking { get; set; }

        public int? InvoiceId { get; set; }
        public virtual Invoice? Invoice { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.pending;
        public DateTime PlacedAt { get; set; }
        public DateTime? DoneAt { get; set; }

        public string? StaffId { get; set; }
        public virtual User? Staff { get; set; }

        public decimal? Total { get; set; }

        public virtual IEnumerable<OrderItem> Items { get; set; }
    }
}
