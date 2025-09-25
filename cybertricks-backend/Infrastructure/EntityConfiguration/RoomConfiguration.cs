using ct.backend.Domain.Entities;
using ct.backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> b)
        {
            b.ToTable("Rooms");
            b.HasKey(x => x.RoomId);

            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
            b.Property(x => x.Type).AsStringEnum().HasMaxLength(20); // nullable enum
            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20)
             .HasDefaultValue(RoomStatus.active);
            b.Property(x => x.DisplayOrder).HasDefaultValue(0);

            b.HasOne(x => x.Floor)
             .WithMany(f => f.Rooms)
             .HasForeignKey(x => x.FloorId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Machines)
             .WithOne(m => m.Room)
             .HasForeignKey(m => m.RoomId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => new { x.FloorId, x.Name });

            b.ConfigureTimestamps();
        }
    }
}
