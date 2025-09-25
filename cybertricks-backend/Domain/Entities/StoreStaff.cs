namespace ct.backend.Domain.Entities
{
    public class StoreStaff : BaseEntity
    {
        public int StoreStaffId { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;

        public bool IsPrimary { get; set; } = false;
    }
}
