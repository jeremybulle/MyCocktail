using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCocktail.Infrastucture.Dao;

namespace MyCocktail.Infrastucture.EntityTypeConfiguration
{
    public class AlcoholicEntityTypeConfiguration : IEntityTypeConfiguration<AlcoholicDao>
    {
        public void Configure(EntityTypeBuilder<AlcoholicDao> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
            builder.HasIndex(a => a.Name).IsUnique(true);
            builder.HasMany(a => a.Drinks).WithOne(d => d.Alcoholic).HasForeignKey(d => d.AlcoholicId);
        }

    }
}
