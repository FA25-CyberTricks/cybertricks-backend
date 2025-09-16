using ct_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> b)
        {
            b.ToTable("InvoiceLines");
            b.HasKey(x => x.InvoiceLineId);

            b.Property(x => x.Description).HasMaxLength(300).IsRequired();
            b.Property(x => x.Qty).HasColumnType("decimal(12,2)").HasDefaultValue(1);
            b.Property(x => x.UnitPrice).HasColumnType("decimal(12,2)");

            b.HasOne(x => x.Invoice)
             .WithMany(i => i.Lines)
             .HasForeignKey(x => x.InvoiceId)
             .OnDelete(DeleteBehavior.Cascade);

            b.ConfigureTimestamps();
        }
    }
}
