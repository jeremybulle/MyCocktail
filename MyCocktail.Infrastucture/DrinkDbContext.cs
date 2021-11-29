using Microsoft.EntityFrameworkCore;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.EntityTypeConfiguration;

namespace MyCocktail.Infrastucture
{
    public class DrinkDbContext : DbContext
    {

        public DbSet<AlcoholicDao> Alcoholics { get; set; }
        public DbSet<CategoryDao> Categories { get; set; }
        public DbSet<DrinkDao> Drinks { get; set; }
        public DbSet<GlassDao> Glasses { get; set; }
        public DbSet<IngredientDao> Ingredients { get; set; }
        public DbSet<MeasureDao> Measures { get; set; }
        public DbSet<UserDao> Users { get; set; }

        public DbSet<FavoriteDao> Favorites { get; set; }

        public DrinkDbContext() : base()
        {

        }

        public DrinkDbContext(DbContextOptions<DrinkDbContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DrinkDb;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlcoholicEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DrinkEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GlassEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IngredientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeasureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteEntityTypeConfiguration());
        }
    }
}
