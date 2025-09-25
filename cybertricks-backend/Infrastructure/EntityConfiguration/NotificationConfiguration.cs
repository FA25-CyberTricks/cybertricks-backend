using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> b)
        {
            b.ToTable("Notifications");
            b.HasKey(x => x.NotificationId);

            b.Property(x => x.Type).AsStringEnum().HasMaxLength(20);
            b.Property(x => x.Channel).AsStringEnum().HasMaxLength(20);
            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20);

            b.Property(x => x.Title).HasMaxLength(150).IsRequired();
            b.Property(x => x.Content).IsRequired();
            b.Property(x => x.DataJson).HasColumnType("json");

            // ⚠️ UserId Guid ↔ User.Id string
            b.HasOne(x => x.User)
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => new { x.UserId, x.Status, x.Channel });

            b.ConfigureTimestamps();
        }
    }
}
