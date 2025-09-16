using ct_backend.Domain.Entities;
using ct_backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> b)
        {
            b.ToTable("Orders");
            b.HasKey(x => x.OrderId);

            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20)
             .HasDefaultValue(OrderStatus.pending);
            b.Property(x => x.Total).HasColumnType("decimal(12,2)");

            b.HasOne(x => x.Store)
             .WithMany()
             .HasForeignKey(x => x.StoreId)
             .OnDelete(DeleteBehavior.Restrict);

            // ⚠️ ClientId Guid ↔ User.Id string
            b.HasOne(x => x.Client)
             .WithMany()
             .HasForeignKey(x => x.ClientId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Booking)
             .WithMany()
             .HasForeignKey(x => x.BookingId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Invoice)
             .WithOne()
             .HasForeignKey<Order>(/* không có FK sang Invoice trong entity */)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Staff)
             .WithMany()
             .HasForeignKey(x => x.StaffId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(x => x.Items)
             .WithOne(i => i.Order)
             .HasForeignKey(i => i.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

            b.ConfigureTimestamps();
        }
    }
}
