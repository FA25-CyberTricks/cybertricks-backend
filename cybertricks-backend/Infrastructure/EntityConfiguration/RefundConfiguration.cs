using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class RefundConfiguration : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> b)
        {
            b.ToTable("Refunds");
            b.HasKey(x => x.RefundId);

            b.Property(x => x.Amount).HasColumnType("decimal(12,2)");
            b.Property(x => x.Reason).HasMaxLength(200);

            b.HasOne(x => x.Payment)
             .WithMany(p => p.Refunds)
             .HasForeignKey(x => x.PaymentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.ConfigureTimestamps();
        }
    }
}
