using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table name
            builder.ToTable("User");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Timestamps của User đang nullable (khác BaseEntity):
            builder.Property(x => x.CreatedAt).HasColumnType("datetime");
            builder.Property(x => x.UpdatedAt).HasColumnType("datetime");
            builder.Property(x => x.LastLogin).HasColumnType("datetime");

            // Index tiện lợi
            builder.HasIndex(x => x.Email);
            builder.HasIndex(x => x.PhoneNumber);
        }
    }
}
