using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCocktail.Infrastucture.Dao;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.EntityTypeConfiguration
{
    [ExcludeFromCodeCoverage]
    public class GlassEntityTypeConfiguration : IEntityTypeConfiguration<GlassDao>
    {
        public void Configure(EntityTypeBuilder<GlassDao> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).IsRequired().HasMaxLength(150);
            builder.HasIndex(g => g.Name).IsUnique(true);
            builder.HasMany(g => g.Drinks).WithOne(d => d.Glass).HasForeignKey(d => d.GlassId);
        }
    }
}
