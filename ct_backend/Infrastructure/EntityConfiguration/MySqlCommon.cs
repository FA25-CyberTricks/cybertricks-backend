using ct_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ct_backend.Infrastructure.EntityConfiguration
{
    public static class MySqlCommon
    {
        //public static PropertyBuilder<Guid> AsChar36(this PropertyBuilder<Guid> b)
        //    => b.HasConversion(
        //           to => to.ToString(),
        //           from => Guid.Parse(from))
        //         .HasMaxLength(36)
        //         .HasColumnType("char(36)");

        //public static PropertyBuilder<Guid?> AsChar36(this PropertyBuilder<Guid?> b)
        //    => b.HasConversion(
        //           to => to.HasValue ? to.Value.ToString() : null,
        //           from => from == null ? (Guid?)null : Guid.Parse(from!))
        //         .HasMaxLength(36)
        //         .HasColumnType("char(36)");

        public static void ConfigureTimestamps<T>(this EntityTypeBuilder<T> b) where T : BaseEntity
        {
            b.Property(x => x.CreatedAt)
             .HasColumnType("datetime")
             .HasDefaultValueSql("CURRENT_TIMESTAMP");

            b.Property(x => x.UpdatedAt)
             .HasColumnType("datetime")
             .HasDefaultValueSql("CURRENT_TIMESTAMP")
             .ValueGeneratedOnAddOrUpdate();
        }

        public static PropertyBuilder<TEnum> AsStringEnum<TEnum>(this PropertyBuilder<TEnum> b)
          where TEnum : struct, Enum
          => b.HasConversion(new EnumToStringConverter<TEnum>());

        // Nullable
        public static PropertyBuilder<TEnum?> AsStringEnum<TEnum>(this PropertyBuilder<TEnum?> b)
            where TEnum : struct, Enum
            => b.HasConversion(new EnumToStringConverter<TEnum>());
    }
}
