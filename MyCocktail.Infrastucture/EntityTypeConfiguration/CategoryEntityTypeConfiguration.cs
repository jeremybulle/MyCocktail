using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCocktail.Infrastucture.Dao;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.EntityTypeConfiguration
{
    [ExcludeFromCodeCoverage]
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<CategoryDao>
    {
        public void Configure(EntityTypeBuilder<CategoryDao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
            builder.HasIndex(c => c.Name).IsUnique(true);
            builder.HasMany(c => c.Drinks).WithOne(d => d.Category).HasForeignKey(d => d.CategoryId);
        }
    }
}
