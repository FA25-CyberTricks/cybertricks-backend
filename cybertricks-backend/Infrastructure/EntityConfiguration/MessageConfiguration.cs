using ct.backend.Domain.Entities;
using ct.backend.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> b)
        {
            b.ToTable("Messages");
            b.HasKey(x => x.MessageId);

            b.Property(x => x.Content).IsRequired();

            b.Property(x => x.Type).AsStringEnum().HasMaxLength(20);
            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20);
            b.Property(x => x.ToRole).AsStringEnum().HasMaxLength(20); // nullable enum

            b.HasOne(x => x.Store)
             .WithMany()
             .HasForeignKey(x => x.StoreId)
             .OnDelete(DeleteBehavior.SetNull);

            // ⚠️ FromUserId/ToUserId Guid ↔ User.Id string
            b.HasOne(x => x.FromUser)
             .WithMany()
             .HasForeignKey(x => x.FromUserId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.ToUser)
             .WithMany()
             .HasForeignKey(x => x.ToUserId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasIndex(x => new { x.StoreId, x.FromUserId, x.ToUserId, x.Status });

            b.ConfigureTimestamps();
        }
    }
}
