namespace ct.backend.Domain.Entities
{
    public class VoucherUsage
    {
        public int VoucherUsageId { get; set; }

        public int VoucherId { get; set; }
        public virtual Voucher Voucher { get; set; } = default!;

        public string UserId { get; set; }
        public virtual User User { get; set; } = default!;

        public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    }
}
