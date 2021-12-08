using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCocktail.Infrastucture.Dao;

namespace MyCocktail.Infrastucture.EntityTypeConfiguration
{
    public class IngredientEntityTypeConfiguration : IEntityTypeConfiguration<IngredientDao>
    {
        public void Configure(EntityTypeBuilder<IngredientDao> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(250);
            builder.HasIndex(i => i.Name).IsUnique(true);
            builder.HasMany(i => i.Measures).WithOne(m => m.Ingredient).HasForeignKey(m => m.IngredientId);
        }
    }
}
