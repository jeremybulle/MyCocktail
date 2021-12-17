using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Domain.Helper;
using MyCocktail.Infrastucture;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyCocktail.ConsoleApp
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        protected Program()
        {

        }

        static async Task Main(string[] args)
        {
            using (var db = new DrinkDbContext())
            {
                var repo = new DrinkRepository(db) ?? throw new Exception(nameof(DrinkRepository));
                var repoSource = new CocktailDbSourceRepository();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                char[] dico = { 'a', 'b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7','8','9'};

                foreach(var c in dico)
                {

                    System.Console.WriteLine($"Calling Api for the letter => {c}");

                    var result = await repoSource.GetCocktailSourcesByLetter(c);

                    if (result != null)
                    {
                        System.Console.WriteLine($"Cocktails find => {result.Count()}");

                        System.Console.WriteLine("Start Load DB");

                        foreach (var drink in result)
                        {
                            await repo.AddAsync(drink.ToModel());
                        }

                        System.Console.WriteLine("End Load DB\n");
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        System.Console.WriteLine($"Cocktails find => Aucun\n");
                        Thread.Sleep(5000);
                    }

                }
               
                System.Console.WriteLine($" nb Alcoholics = {db.Alcoholics.Count()}");
                System.Console.WriteLine($" nb Glasse = {db.Glasses.Count()}");
                System.Console.WriteLine($" nb Categories = {db.Categories.Count()}");
                System.Console.WriteLine($" nb Ingredients = {db.Ingredients.Count()}");
                System.Console.WriteLine($" nb Drinks = {db.Drinks.Count()}");
                System.Console.WriteLine($" nb Meausures = {db.Measures.Count()}");

                System.Console.WriteLine(db.Measures.GroupBy(m => m.DrinkId, m => m.IngredientId).Count());

                System.Console.WriteLine("Creation de l'Admin");

                var admin = new UserDao()
                {
                    UserName = "Admin",
                    Role = UserRole.Admin,
                    CreationDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Password = PasswordHasher.Hash("Password"),
                    Email = "Admin@toto.com",
                    FirstName = "john",
                    LastName = "Doe",
                };

                db.Users.Add(admin);

                var drinkFav = db.Drinks.FirstOrDefault(d => d.Name.Contains("a"));

                var favorite = new FavoriteDao()
                {
                    Id = Guid.NewGuid(),
                    Drink = drinkFav,
                    IdDrink = drinkFav.Id,
                    IdUser = admin.Id,
                    User = admin,
                };

                db.Favorites.Add(favorite);

                db.SaveChanges();

                System.Console.WriteLine(db.Users.Count() == 1 ? " creation OK" : " creation KO");

                var dbAdmin = db.Users.FirstOrDefault(u => u.UserName == "Admin");
                Console.WriteLine(dbAdmin.Role == UserRole.Admin ? "Role OK" : "Role KO");

                System.Console.ReadKey();
            }

        }
    }
}
