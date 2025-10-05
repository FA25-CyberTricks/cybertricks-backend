namespace ct.backend.Domain.Entities
{
    public class Favorite : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;
        public virtual User User { get; set; } = default!;
    }
}
