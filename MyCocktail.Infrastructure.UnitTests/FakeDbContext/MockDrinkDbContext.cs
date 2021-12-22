using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Infrastucture;
using MyCocktail.Infrastucture.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCocktail.Infrastructure.UnitTests.FakeDbContext
{
    public static class MockDrinkDbContext
    {
        private static readonly Fixture _fixture = new Fixture();
         
        public static Mock<DrinkDbContext> GetMockedDbContext()
        {
            var mockContext = new Mock<DrinkDbContext>();

            #region alcoholicDatas
            var alcoholicDatas = new List<AlcoholicDao>()
            {
                new AlcoholicDao() { Id = Guid.NewGuid(), Name = "alcoholic", Drinks = new List<DrinkDao>()},
                new AlcoholicDao() { Id = Guid.NewGuid(), Name = "non alcoholic", Drinks = new List<DrinkDao>()},
                new AlcoholicDao() { Id = Guid.NewGuid(), Name = "optional alcohol", Drinks = new List<DrinkDao>()}
            }.AsQueryable();


            var mockAlcoholic = new Mock<DbSet<AlcoholicDao>>();
            mockAlcoholic.As<IQueryable<AlcoholicDao>>().Setup(m => m.Provider).Returns(alcoholicDatas.Provider);
            mockAlcoholic.As<IQueryable<AlcoholicDao>>().Setup(m => m.Expression).Returns(alcoholicDatas.Expression);
            mockAlcoholic.As<IQueryable<AlcoholicDao>>().Setup(m => m.ElementType).Returns(alcoholicDatas.ElementType);
            mockAlcoholic.As<IQueryable<AlcoholicDao>>().Setup(m => m.GetEnumerator()).Returns(alcoholicDatas.GetEnumerator());

            mockContext.Setup(c => c.Alcoholics).Returns(mockAlcoholic.Object);
            #endregion

            #region categoryDatas
            var categoryDatas = new List<CategoryDao>()
            {
                new CategoryDao() { Id = Guid.NewGuid(), Name = "beer", Drinks = new List<DrinkDao>()},
                new CategoryDao() { Id = Guid.NewGuid(), Name = "cocktail", Drinks = new List<DrinkDao>()},
                new CategoryDao() { Id = Guid.NewGuid(), Name = "cocoa", Drinks = new List<DrinkDao>()},
                new CategoryDao() { Id = Guid.NewGuid(), Name = "coffee / tea", Drinks = new List<DrinkDao>()},
                new CategoryDao() { Id = Guid.NewGuid(), Name = "ordinary drink", Drinks = new List<DrinkDao>()},
                new CategoryDao() { Id = Guid.NewGuid(), Name = "shot", Drinks = new List<DrinkDao>()}
            }.AsQueryable();


            var mockCategory = new Mock<DbSet<CategoryDao>>();
            mockCategory.As<IQueryable<CategoryDao>>().Setup(m => m.Provider).Returns(categoryDatas.Provider);
            mockCategory.As<IQueryable<CategoryDao>>().Setup(m => m.Expression).Returns(categoryDatas.Expression);
            mockCategory.As<IQueryable<CategoryDao>>().Setup(m => m.ElementType).Returns(categoryDatas.ElementType);
            mockCategory.As<IQueryable<CategoryDao>>().Setup(m => m.GetEnumerator()).Returns(categoryDatas.GetEnumerator());

            mockContext.Setup(c => c.Categories).Returns(mockCategory.Object);
            #endregion

            #region glassDatas
            var glassDatas = new List<GlassDao>()
            {
                new GlassDao() { Id = Guid.NewGuid(), Name = "balloon glass", Drinks = new List<DrinkDao>()},
                new GlassDao() { Id = Guid.NewGuid(), Name = "beer glass", Drinks = new List<DrinkDao>()},
                new GlassDao() { Id = Guid.NewGuid(), Name = "cocktail glass", Drinks = new List<DrinkDao>()},
                new GlassDao() { Id = Guid.NewGuid(), Name = "coffee mug", Drinks = new List<DrinkDao>()},
                new GlassDao() { Id = Guid.NewGuid(), Name = "jar", Drinks = new List<DrinkDao>()},
                new GlassDao() { Id = Guid.NewGuid(), Name = "whiskey glass", Drinks = new List<DrinkDao>()},
                new GlassDao() { Id = Guid.NewGuid(), Name = "wine glass", Drinks = new List<DrinkDao>()},
                new GlassDao() { Id = Guid.NewGuid(), Name = "highball glass", Drinks = new List<DrinkDao>()},
            }.AsQueryable();


            var mockGlass = new Mock<DbSet<GlassDao>>();
            mockGlass.As<IQueryable<GlassDao>>().Setup(m => m.Provider).Returns(glassDatas.Provider);
            mockGlass.As<IQueryable<GlassDao>>().Setup(m => m.Expression).Returns(glassDatas.Expression);
            mockGlass.As<IQueryable<GlassDao>>().Setup(m => m.ElementType).Returns(glassDatas.ElementType);
            mockGlass.As<IQueryable<GlassDao>>().Setup(m => m.GetEnumerator()).Returns(glassDatas.GetEnumerator());

            mockContext.Setup(c => c.Glasses).Returns(mockGlass.Object);
            #endregion

            #region ingredientDatas
            var ingredientDatas = new List<IngredientDao>()
            {
                new IngredientDao() { Id = Guid.NewGuid(), Name = "7-up", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "absinthe", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "banana", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "beer", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "blue curacao", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "chocolate", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "cider", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "coffee", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "dark rum", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "honey", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "ice", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "lemon", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "vodka", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "whisky", Measures = new List<MeasureDao>()},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "wine", Measures = new List<MeasureDao>()},
            }.AsQueryable();


            var mockIngredient = new Mock<DbSet<IngredientDao>>();
            mockIngredient.As<IQueryable<IngredientDao>>().Setup(m => m.Provider).Returns(ingredientDatas.Provider);
            mockIngredient.As<IQueryable<IngredientDao>>().Setup(m => m.Expression).Returns(ingredientDatas.Expression);
            mockIngredient.As<IQueryable<IngredientDao>>().Setup(m => m.ElementType).Returns(ingredientDatas.ElementType);
            mockIngredient.As<IQueryable<IngredientDao>>().Setup(m => m.GetEnumerator()).Returns(ingredientDatas.GetEnumerator());

            mockContext.Setup(c => c.Ingredients).Returns(mockIngredient.Object);
            #endregion

            #region userDatas
            IQueryable<UserDao> userDatas = new List<UserDao>()
            {
                new UserDao() { 
                    Id = Guid.NewGuid(), 
                    UserName = "admin",
                    CreationDate = DateTime.Now, 
                    Email = "admin@gmail.com", 
                    FirstName = "john", 
                    LastName="doe", 
                    Password="password", 
                    Role = UserRole.Admin,
                    DrinksOwned = new List<DrinkDao>(),
                    Favorites = new List<FavoriteDao>()
                },
               new UserDao() {
                    Id = Guid.NewGuid(),
                    UserName = "DarkSasuke",
                    CreationDate = DateTime.Now,
                    Email = "jeandupont@gmail.com",
                    FirstName = "jean",
                    LastName="dupont",
                    Password="password",
                    Role = UserRole.User,
                    DrinksOwned = new List<DrinkDao>(),
                    Favorites = new List<FavoriteDao>()
                },
               new UserDao() {
                    Id = Guid.NewGuid(),
                    UserName = "Tati",
                    CreationDate = DateTime.Now,
                    Email = "tatigertrude@gmail.com",
                    FirstName = "tati",
                    LastName="gertrude",
                    Password="password",
                    Role = UserRole.Unidentified,
                    DrinksOwned = new List<DrinkDao>(),
                    Favorites = new List<FavoriteDao>()
                },

            }.AsQueryable();


            var mockUser = new Mock<DbSet<UserDao>>();
            mockUser.As<IQueryable<UserDao>>().Setup(m => m.Provider).Returns(userDatas.Provider);
            mockUser.As<IQueryable<UserDao>>().Setup(m => m.Expression).Returns(userDatas.Expression);
            mockUser.As<IQueryable<UserDao>>().Setup(m => m.ElementType).Returns(userDatas.ElementType);
            mockUser.As<IQueryable<UserDao>>().Setup(m => m.GetEnumerator()).Returns(userDatas.GetEnumerator());

            //var mockUser = GetDbSet<UserDao>(userDatas);

            mockContext.Setup(c => c.Users).Returns(mockUser.Object);
            #endregion

            #region drinkDatas
            var drinkDatas = new List<DrinkDao>()
            {
                new DrinkDao() {
                    Id = Guid.NewGuid(),
                    Name = "whisky honey",
                    Alcoholic = alcoholicDatas.FirstOrDefault(a => a.Name == "alcoholic"),
                    AlcoholicId = alcoholicDatas.FirstOrDefault(a => a.Name == "alcoholic").Id,
                    Category = categoryDatas.FirstOrDefault(c => c.Name == "ordinary drink"),
                    CategoryId = categoryDatas.FirstOrDefault(c => c.Name == "ordinary drink").Id,
                    DateModified = DateTime.Now,
                    Glass = glassDatas.FirstOrDefault(g => g.Name == "whiskey glass"),
                    GlassId = glassDatas.FirstOrDefault(g => g.Name == "whiskey glass").Id,
                    IdSource = _fixture.Create<int>().ToString(),
                    Instruction = _fixture.Create<string>(),
                    Owner = null,
                    OwnerId = null,
                    UrlPicture = _fixture.Create<Uri>().ToString(),
                    Favorites = new List<FavoriteDao>(),
                    Measures = new List<MeasureDao>()
                },
                new DrinkDao() {
                    Id = Guid.NewGuid(),
                    Name = "Beer lemon",
                    Alcoholic = alcoholicDatas.FirstOrDefault(a => a.Name == "alcoholic"),
                    AlcoholicId = alcoholicDatas.FirstOrDefault(a => a.Name == "alcoholic").Id,
                    Category = categoryDatas.FirstOrDefault(c => c.Name == "beer"),
                    CategoryId = categoryDatas.FirstOrDefault(c => c.Name == "beer").Id,
                    DateModified = DateTime.Now,
                    Glass = glassDatas.FirstOrDefault(g => g.Name == "beer glass"),
                    GlassId = glassDatas.FirstOrDefault(g => g.Name == "beer glass").Id,
                    IdSource = _fixture.Create<int>().ToString(),
                    Instruction = _fixture.Create<string>(),
                    Owner = null,
                    OwnerId = null,
                    UrlPicture = _fixture.Create<Uri>().ToString(),
                    Favorites = new List<FavoriteDao>(),
                    Measures = new List<MeasureDao>()
                },
                new DrinkDao() {
                    Id = Guid.NewGuid(),
                    Name = "Rum banana",
                    Alcoholic = alcoholicDatas.FirstOrDefault(a => a.Name == "alcoholic"),
                    AlcoholicId = alcoholicDatas.FirstOrDefault(a => a.Name == "alcoholic").Id,
                    Category = categoryDatas.FirstOrDefault(c => c.Name == "cocktail"),
                    CategoryId = categoryDatas.FirstOrDefault(c => c.Name == "cocktail").Id,
                    DateModified = DateTime.Now,
                    Glass = glassDatas.FirstOrDefault(g => g.Name == "cocktail glass"),
                    GlassId = glassDatas.FirstOrDefault(g => g.Name == "cocktail glass").Id,
                    IdSource = _fixture.Create<int>().ToString(),
                    Instruction = _fixture.Create<string>(),
                    Owner = null,
                    OwnerId = null,
                    UrlPicture = _fixture.Create<Uri>().ToString(),
                    Favorites = new List<FavoriteDao>(),
                    Measures = new List<MeasureDao>()
                },

            }.AsQueryable();
            
            
            var mockDrink = new Mock<DbSet<DrinkDao>>();
            mockDrink.As<IQueryable<DrinkDao>>().Setup(m => m.Provider).Returns(drinkDatas.Provider);
            mockDrink.As<IQueryable<DrinkDao>>().Setup(m => m.Expression).Returns(drinkDatas.Expression);
            mockDrink.As<IQueryable<DrinkDao>>().Setup(m => m.ElementType).Returns(drinkDatas.ElementType);
            mockDrink.As<IQueryable<DrinkDao>>().Setup(m => m.GetEnumerator()).Returns(drinkDatas.GetEnumerator());

            mockContext.Setup(c => c.Ingredients).Returns(mockIngredient.Object);
            #endregion

            #region measureDatas
            var measureDatas = new List<MeasureDao>();

            var measure = new MeasureDao()
            {
                Id = Guid.NewGuid(),
                DrinkId = drinkDatas.First(d => d.Name == "whisky honey").Id,
                Drink = drinkDatas.First(d => d.Name == "whisky honey"),
                Ingredient = ingredientDatas.First(i => i.Name == "whisky"),
                IngredientId = ingredientDatas.First(i => i.Name == "whisky").Id,
                Quantity = "1L",
            };

            measureDatas.Add(measure);
            drinkDatas.First(d => d.Name == "whisky honey").Measures.Add(measureDatas.First(m => m.Id == measure.Id));
            ingredientDatas.First(i => i.Name == "whisky").Measures.Add(measureDatas.First(m => m.Id == measure.Id));

            measure = new MeasureDao()
            {
                Id = Guid.NewGuid(),
                DrinkId = drinkDatas.First(d => d.Name == "whisky honey").Id,
                Drink = drinkDatas.First(d => d.Name == "whisky honey"),
                Ingredient = ingredientDatas.First(i => i.Name == "honey"),
                IngredientId = ingredientDatas.First(i => i.Name == "honey").Id,
                Quantity = "1 cac"
            };

            measureDatas.Add(measure);
            drinkDatas.First(d => d.Name == "whisky honey").Measures.Add(measureDatas.First(m => m.Id == measure.Id));
            ingredientDatas.First(i => i.Name == "honey").Measures.Add(measureDatas.First(m => m.Id == measure.Id));

            measure = new MeasureDao()
            {
                Id = Guid.NewGuid(),
                DrinkId = drinkDatas.First(d => d.Name == "whisky honey").Id,
                Drink = drinkDatas.First(d => d.Name == "whisky honey"),
                Ingredient = ingredientDatas.First(i => i.Name == "ice"),
                IngredientId = ingredientDatas.First(i => i.Name == "ice").Id,
                Quantity = "1 cac"
            };

            measureDatas.Add(measure);
            drinkDatas.First(d => d.Name == "whisky honey").Measures.Add(measureDatas.First(m => m.Id == measure.Id));
            ingredientDatas.First(i => i.Name == "ice").Measures.Add(measureDatas.First(m => m.Id == measure.Id));

            measure = new MeasureDao()
            {
                Id = Guid.NewGuid(),
                DrinkId = drinkDatas.First(d => d.Name == "Beer lemon").Id,
                Drink = drinkDatas.First(d => d.Name == "Beer lemon"),
                Ingredient = ingredientDatas.First(i => i.Name == "beer"),
                IngredientId = ingredientDatas.First(i => i.Name == "beer").Id,
                Quantity = "1 pinte"
            };
            measureDatas.Add(measure);
            drinkDatas.First(d => d.Name == "Beer lemon").Measures.Add(measureDatas.First(m => m.Id == measure.Id));
            ingredientDatas.First(i => i.Name == "beer").Measures.Add(measureDatas.First(m => m.Id == measure.Id));

            measure = new MeasureDao()
            {
                Id = Guid.NewGuid(),
                DrinkId = drinkDatas.First(d => d.Name == "Beer lemon").Id,
                Drink = drinkDatas.First(d => d.Name == "Beer lemon"),
                Ingredient = ingredientDatas.First(i => i.Name == "lemon"),
                IngredientId = ingredientDatas.First(i => i.Name == "lemon").Id,
                Quantity = "1"
            };
            measureDatas.Add(measure);
            drinkDatas.First(d => d.Name == "Beer lemon").Measures.Add(measureDatas.First(m => m.Id == measure.Id));
            ingredientDatas.First(i => i.Name == "lemon").Measures.Add(measureDatas.First(m => m.Id == measure.Id));

            measure = new MeasureDao()
            {
                Id = Guid.NewGuid(),
                DrinkId = drinkDatas.First(d => d.Name == "Rum banana").Id,
                Drink = drinkDatas.First(d => d.Name == "Rum banana"),
                Ingredient = ingredientDatas.First(i => i.Name == "dark rum"),
                IngredientId = ingredientDatas.First(i => i.Name == "dark rum").Id,
                Quantity = "1"
            };
            measureDatas.Add(measure);
            drinkDatas.First(d => d.Name == "Rum banana").Measures.Add(measureDatas.First(m => m.Id == measure.Id));
            ingredientDatas.First(i => i.Name == "dark rum").Measures.Add(measureDatas.First(m => m.Id == measure.Id));

            measure = new MeasureDao()
            {
                Id = Guid.NewGuid(),
                DrinkId = drinkDatas.First(d => d.Name == "Rum banana").Id,
                Drink = drinkDatas.First(d => d.Name == "Rum banana"),
                Ingredient = ingredientDatas.First(i => i.Name == "banana"),
                IngredientId = ingredientDatas.First(i => i.Name == "banana").Id,
                Quantity = "1"
            };
            measureDatas.Add(measure);
            drinkDatas.First(d => d.Name == "Rum banana").Measures.Add(measureDatas.First(m => m.Id == measure.Id));
            ingredientDatas.First(i => i.Name == "banana").Measures.Add(measureDatas.First(m => m.Id == measure.Id));

            #endregion

            #region favoriteDatas

            var favoriteDatas = new List<FavoriteDao>();

            var favorite = new FavoriteDao()
            {
                Id = Guid.NewGuid(),
                Drink = drinkDatas.First(d => d.Name == "Beer lemon"),
                IdDrink = drinkDatas.First(d => d.Name == "Beer lemon").Id,
                User = userDatas.First(u => u.UserName == "Tati"),
                IdUser = userDatas.First(u => u.UserName == "Tati").Id
            };

            favoriteDatas.Add(favorite);
            userDatas.First(u => u.UserName == "Tati").Favorites.Add(favoriteDatas.First(f => f.Id == favorite.Id));
            drinkDatas.First(d => d.Name == "Rum banana").Favorites.Add(favoriteDatas.First(f => f.Id == favorite.Id));

            favorite = new FavoriteDao()
            {
                Id = Guid.NewGuid(),
                Drink = drinkDatas.First(d => d.Name == "Beer lemon"),
                IdDrink = drinkDatas.First(d => d.Name == "Beer lemon").Id,
                User = userDatas.First(u => u.UserName == "DarkSasuke"),
                IdUser = userDatas.First(u => u.UserName == "DarkSasuke").Id
            };

            favoriteDatas.Add(favorite);
            userDatas.First(u => u.UserName == "DarkSasuke").Favorites.Add(favoriteDatas.First(f => f.Id == favorite.Id));
            drinkDatas.First(d => d.Name == "Beer lemon").Favorites.Add(favoriteDatas.First(f => f.Id == favorite.Id));

            #endregion

            userDatas.First(u => u.UserName == "admin").DrinksOwned.Add(drinkDatas.First(d => d.Name == "whisky honey"));
            drinkDatas.First(d => d.Name == "whisky honey").Owner = userDatas.First(u => u.UserName == "admin");
            drinkDatas.First(d => d.Name == "whisky honey").OwnerId = userDatas.First(u => u.UserName == "admin").Id;

            return mockContext;
        }

        public static Mock<DbSet<T>> GetDbSet<T>(IQueryable<T> TestData) where T : class
        {
            var MockSet = new Mock<DbSet<T>>();
            MockSet.As<IAsyncEnumerable<T>>().Setup(x => x.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<T>(TestData.GetEnumerator()));
            MockSet.As<IQueryable<T>>().Setup(x => x.Provider).Returns(new TestAsyncQueryProvider<T>(TestData.Provider));
            MockSet.As<IQueryable<T>>().Setup(x => x.Expression).Returns(TestData.Expression);
            MockSet.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(TestData.ElementType);
            MockSet.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(TestData.GetEnumerator());
            return MockSet;
        }
    }
}
