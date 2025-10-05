using ct.backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ct.backend.Infrastructure.EntityConfiguration;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> b)
    {
        b.ToTable("Favorites");

        b.HasKey(x => x.Id);

        // Unique: 1 user không được favorite cùng 1 store nhiều lần
        b.HasIndex(x => new { x.UserId, x.StoreId }).IsUnique();

        // FK -> User
        b.HasOne(x => x.User)
         .WithMany()                 // nếu User có ICollection<Favorite> Favorites thì đổi thành .WithMany(u => u.Favorites)
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Cascade);

        // FK -> Store
        b.HasOne(x => x.Store)
         .WithMany()                 // nếu Store có ICollection<Favorite> Favorites thì đổi thành .WithMany(s => s.Favorites)
         .HasForeignKey(x => x.StoreId)
         .OnDelete(DeleteBehavior.Cascade);

        // Timestamps
        b.ConfigureTimestamps();
    }
}