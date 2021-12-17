using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Infrastucture.Dao;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.EntityTypeConfiguration
{
    [ExcludeFromCodeCoverage]
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserDao>
    {
        public void Configure(EntityTypeBuilder<UserDao> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(150);
            builder.HasIndex(u => u.UserName).IsUnique(true);
            builder.Property(u => u.FirstName).IsRequired(true).HasMaxLength(100);
            builder.Property(u => u.LastName).IsRequired(true).HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired(true).HasMaxLength(150);
            builder.Property(u => u.Password).IsRequired(true).HasMaxLength(100);
            builder.Property(u => u.Role).HasConversion(r => (int)r, r => (UserRole)r).IsRequired(true);
            builder.HasMany(u => u.DrinksOwned).WithOne(d => d.Owner).HasForeignKey(d => d.OwnerId);
        }
    }
}
