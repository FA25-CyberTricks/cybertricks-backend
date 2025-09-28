using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.TokenHash).HasMaxLength(128).IsRequired();
            b.HasIndex(x => x.TokenHash).IsUnique();         // tra nhanh + chống trùng
        }
    }
}
