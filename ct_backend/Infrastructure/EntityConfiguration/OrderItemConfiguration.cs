using ct_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> b)
        {
            b.ToTable("OrderItems");
            b.HasKey(x => x.OrderItemId);

            b.Property(x => x.Qty).HasDefaultValue(1);
            b.Property(x => x.UnitPrice).HasColumnType("decimal(12,2)");
            b.Property(x => x.Note).HasMaxLength(200);

            b.HasOne(x => x.Order)
             .WithMany(o => o.Items)
             .HasForeignKey(x => x.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Item)
             .WithMany(i => i.OrderItems)
             .HasForeignKey(x => x.ItemId)
             .OnDelete(DeleteBehavior.Restrict);

            b.ConfigureTimestamps();
        }
    }
}
