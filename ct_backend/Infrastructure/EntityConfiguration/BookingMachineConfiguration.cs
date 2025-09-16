using ct_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class BookingMachineConfiguration : IEntityTypeConfiguration<BookingMachine>
    {
        public void Configure(EntityTypeBuilder<BookingMachine> b)
        {
            b.ToTable("BookingMachines");
            b.HasKey(x => x.BookingMachineId);

            b.Property(x => x.RateSnapshot).HasColumnType("decimal(12,2)");

            b.HasOne(x => x.Booking)
             .WithMany(z => z.BookingMachines)
             .HasForeignKey(x => x.BookingId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Machine)
             .WithMany(x => x.BookingMachines)
             .HasForeignKey(x => x.MachineId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.BookingId, x.MachineId }).IsUnique();

            b.ConfigureTimestamps();
        }
    }
}
