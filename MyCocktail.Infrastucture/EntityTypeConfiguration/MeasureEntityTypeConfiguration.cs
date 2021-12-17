using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCocktail.Infrastucture.Dao;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.EntityTypeConfiguration
{
    [ExcludeFromCodeCoverage]
    public class MeasureEntityTypeConfiguration : IEntityTypeConfiguration<MeasureDao>
    {
        public void Configure(EntityTypeBuilder<MeasureDao> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Quantity).HasMaxLength(100);
            builder.Property(m => m.DrinkId).IsRequired(true);
            builder.HasIndex(m => m.DrinkId);
            builder.Property(m => m.IngredientId).IsRequired(true);
            builder.HasOne(m => m.Drink).WithMany(d => d.Measures).HasForeignKey(m => m.DrinkId).IsRequired(false);
            builder.HasOne(m => m.Ingredient).WithMany(i => i.Measures).HasForeignKey(m => m.IngredientId);
        }
    }
}
