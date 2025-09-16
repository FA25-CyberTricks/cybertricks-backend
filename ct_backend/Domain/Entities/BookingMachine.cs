namespace ct_backend.Domain.Entities
{
    public class BookingMachine : BaseEntity
    {
        public int BookingMachineId { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; } = default!;

        public int MachineId { get; set; }
        public virtual Machine Machine { get; set; } = default!;

        public decimal? RateSnapshot { get; set; }
    }
}
