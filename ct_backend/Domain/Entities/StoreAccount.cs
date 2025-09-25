namespace ct_backend.Domain.Entities
{
    public class StoreAccount : BaseEntity 
    {
        public int StoreAccountId { get; set; }

        public int StoreId { get; set; }

        public string BankName { get; set; } = default!;

        public string AccountNumber { get; set; } = default!;

        public string AccountHolder { get; set; } = default!;

        // Navigation property
        public virtual Store Store { get; set; } = default!;
    }
}
