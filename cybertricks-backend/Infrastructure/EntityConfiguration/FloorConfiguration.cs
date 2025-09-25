using ct.backend.Domain.Entities;
using ct.backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class FloorConfiguration : IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> b)
        {
            b.ToTable("Floors");
            b.HasKey(x => x.FloorId);

            b.Property(x => x.FloorNumber).IsRequired();
            b.Property(x => x.Name).HasMaxLength(200);
            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20)
             .HasDefaultValue(FloorStatus.active);
            b.Property(x => x.DisplayOrder).HasDefaultValue(0);

            b.HasOne(x => x.Store)
             .WithMany(s => s.Floors)
             .HasForeignKey(x => x.StoreId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Rooms)
             .WithOne(r => r.Floor)
             .HasForeignKey(r => r.FloorId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => new { x.StoreId, x.FloorNumber }).IsUnique();

            b.ConfigureTimestamps();
        }
    }
}
