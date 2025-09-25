using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class StoreAccountConfiguration : IEntityTypeConfiguration<StoreAccount>
    {
        public void Configure(EntityTypeBuilder<StoreAccount> builder)
        {
            builder.ToTable("StoreAccounts");

            builder.HasKey(sa => sa.StoreAccountId);

            builder.Property(sa => sa.BankName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(sa => sa.AccountNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(sa => sa.AccountHolder)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(sa => sa.Store)
                   .WithMany(s => s.StoreAccounts)
                   .HasForeignKey(sa => sa.StoreId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.ConfigureTimestamps();
        }
    }
}
