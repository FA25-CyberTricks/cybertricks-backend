using ct.backend.Domain.Entities;
using ct.backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> b)
        {
            b.ToTable("Payments");
            b.HasKey(x => x.PaymentId);

            b.Property(x => x.Method).AsStringEnum().HasMaxLength(30);
            b.Property(x => x.Status).AsStringEnum().HasMaxLength(30);
            b.Property(x => x.Amount).HasColumnType("decimal(12,2)");
            b.Property(x => x.ProviderRef).HasMaxLength(100);

            b.HasOne(x => x.Invoice)
             .WithMany(i => i.Payments)
             .HasForeignKey(x => x.InvoiceId)
             .OnDelete(DeleteBehavior.Cascade);

            // ⚠️ UserId Guid? ↔ User.Id string
            b.HasOne(x => x.User)
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(x => x.Refunds)
             .WithOne(r => r.Payment)
             .HasForeignKey(r => r.PaymentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.ConfigureTimestamps();
        }
    }
}
