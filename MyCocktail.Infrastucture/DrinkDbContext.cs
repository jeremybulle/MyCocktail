using Microsoft.EntityFrameworkCore;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.EntityTypeConfiguration;

namespace MyCocktail.Infrastucture
{
    public class DrinkDbContext : DbContext
    {

        public virtual DbSet<AlcoholicDao> Alcoholics { get; set; }
        public virtual DbSet<CategoryDao> Categories { get; set; }
        public virtual DbSet<DrinkDao> Drinks { get; set; }
        public virtual DbSet<GlassDao> Glasses { get; set; }
        public virtual DbSet<IngredientDao> Ingredients { get; set; }
        public virtual DbSet<MeasureDao> Measures { get; set; }
        public virtual DbSet<UserDao> Users { get; set; }
        public virtual DbSet<FavoriteDao> Favorites { get; set; }

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
