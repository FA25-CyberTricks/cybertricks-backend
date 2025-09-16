using ct_backend.Domain.Entities;
using ct_backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> b)
        {
            b.ToTable("Invoices");
            b.HasKey(x => x.InvoiceId);

            b.Property(x => x.Currency).HasMaxLength(3).IsRequired();
            b.Property(x => x.Subtotal).HasColumnType("decimal(12,2)");
            b.Property(x => x.TaxAmount).HasColumnType("decimal(12,2)");
            b.Property(x => x.Total).HasColumnType("decimal(12,2)");

            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20)
             .HasDefaultValue(InvoiceStatus.open);

            b.HasOne(x => x.Store)
             .WithMany()
             .HasForeignKey(x => x.StoreId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Booking)
             .WithOne()
             .HasForeignKey<Invoice>(x => x.BookingId)
             .OnDelete(DeleteBehavior.SetNull);

            // ⚠️ UserId = Guid? nhưng User.Id là string → cần đồng bộ type
            b.HasOne(x => x.User)
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(x => x.Lines)
             .WithOne(l => l.Invoice)
             .HasForeignKey(l => l.InvoiceId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Payments)
             .WithOne(p => p.Invoice)
             .HasForeignKey(p => p.InvoiceId)
             .OnDelete(DeleteBehavior.Cascade);

            b.ConfigureTimestamps();
        }
    }
}
