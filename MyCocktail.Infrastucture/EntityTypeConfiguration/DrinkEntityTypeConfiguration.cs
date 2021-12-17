using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCocktail.Infrastucture.Dao;
using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Infrastucture.EntityTypeConfiguration
{
    [ExcludeFromCodeCoverage]
    public class DrinkEntityTypeConfiguration : IEntityTypeConfiguration<DrinkDao>
    {
        public void Configure(EntityTypeBuilder<DrinkDao> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasIndex(d => d.Name).IsUnique(true);
            builder.Property(d => d.IdSource).IsRequired(false);
            builder.Property(d => d.Instruction).IsRequired(false);
            builder.Property(d => d.UrlPicture).IsRequired(false);
            builder.Property(d => d.DateModified).IsRequired(true);
            builder.HasIndex(d => d.DateModified).IsUnique(false);
            builder.HasMany(d => d.Measures).WithOne(m => m.Drink).HasForeignKey(m => m.DrinkId).IsRequired(false);
            builder.HasOne(d => d.Glass).WithMany(g => g.Drinks).HasForeignKey(d => d.GlassId).IsRequired();
            builder.HasOne(d => d.Alcoholic).WithMany(a => a.Drinks).HasForeignKey(d => d.AlcoholicId).IsRequired();
            builder.HasOne(d => d.Category).WithMany(c => c.Drinks).HasForeignKey(d => d.CategoryId).IsRequired(false);
            builder.HasOne(d => d.Owner).WithMany(u => u.DrinksOwned).HasForeignKey(d => d.OwnerId);
        }
    }
}
