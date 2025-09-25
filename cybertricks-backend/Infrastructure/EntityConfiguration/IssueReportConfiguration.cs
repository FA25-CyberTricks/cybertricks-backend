using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class IssueReportConfiguration : IEntityTypeConfiguration<IssueReport>
    {
        public void Configure(EntityTypeBuilder<IssueReport> b)
        {
            b.ToTable("IssueReports");
            b.HasKey(x => x.IssueId);

            b.Property(x => x.Scope).AsStringEnum().HasMaxLength(20);
            b.Property(x => x.Category).AsStringEnum().HasMaxLength(20);
            b.Property(x => x.Priority).AsStringEnum().HasMaxLength(20);
            b.Property(x => x.Status).AsStringEnum().HasMaxLength(20);

            b.Property(x => x.Title).HasMaxLength(200).IsRequired();
            b.Property(x => x.Description).IsRequired();
            b.Property(x => x.Source).HasMaxLength(20).HasDefaultValue("web");

            // ⚠️ ReporterId Guid ↔ User.Id string
            b.HasOne(x => x.Reporter)
             .WithMany()
             .HasForeignKey(x => x.ReporterId)
             .OnDelete(DeleteBehavior.Restrict);

            b.ConfigureTimestamps();
        }
    }
}
