using ct_backend.Domain.Entities;
using ct_backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> b)
        {
            b.ToTable("Bookings");
            b.HasKey(x => x.BookingId);

            b.Property(x => x.BookingCode).HasMaxLength(30).IsRequired();
            b.HasIndex(x => x.BookingCode).IsUnique();

            b.Property(x => x.Note).HasMaxLength(500);
            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20)
             .HasDefaultValue(BookingStatus.reserved);

            b.Property(x => x.EstimatedAmt).HasColumnType("decimal(12,2)");

            b.HasOne(x => x.Store)
             .WithMany(x => x.Bookings)
             .HasForeignKey(x => x.StoreId)
             .OnDelete(DeleteBehavior.Restrict);

            // ⚠️ FK tới User (Client) đang Guid nhưng User.Id là string → cần đổi type hoặc User<Guid>
            b.HasOne(x => x.Client)
             .WithMany(x => x.Bookings)
             .HasForeignKey(x => x.ClientId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(x => x.BookingMachines)
             .WithOne(bm => bm.Booking)
             .HasForeignKey(bm => bm.BookingId)
             .OnDelete(DeleteBehavior.Cascade);

            b.ConfigureTimestamps();
        }
    }
}
