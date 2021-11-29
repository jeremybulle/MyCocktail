using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCocktail.Infrastucture.Dao;

namespace MyCocktail.Infrastucture.EntityTypeConfiguration
{
    public class FavoriteEntityTypeConfiguration : IEntityTypeConfiguration<FavoriteDao>
    {
        public void Configure(EntityTypeBuilder<FavoriteDao> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.IdDrink).IsRequired(true);
            builder.Property(f => f.IdUser).IsRequired(true);
            builder.HasIndex(f => f.IdUser);
            builder.HasOne(f => f.Drink).WithMany(d => d.Favorites).HasForeignKey(f => f.IdDrink).IsRequired(true);
            builder.HasOne(f => f.User).WithMany(u => u.Favorites).HasForeignKey(f => f.IdUser).IsRequired(true);
        }
    }
}
