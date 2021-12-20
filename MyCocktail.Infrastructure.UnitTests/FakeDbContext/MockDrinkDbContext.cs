using Microsoft.EntityFrameworkCore;
using Moq;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Infrastucture;
using MyCocktail.Infrastucture.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastructure.UnitTests.FakeDbContext
{
    public static class MockDrinkDbContext
    {
        public static Mock<DrinkDbContext> GetMockedDbContext()
        {
            var mockContext = new Mock<DrinkDbContext>();

            #region alcoholicDatas
            var alcoholicDatas = new List<AlcoholicDao>()
            {
                new AlcoholicDao() { Id = Guid.NewGuid(), Name = "alcoholic"},
                new AlcoholicDao() { Id = Guid.NewGuid(), Name = "non alcoholic"},
                new AlcoholicDao() { Id = Guid.NewGuid(), Name = "optional alcohol"}
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
                new CategoryDao() { Id = Guid.NewGuid(), Name = "beer" },
                new CategoryDao() { Id = Guid.NewGuid(), Name = "cocktail" },
                new CategoryDao() { Id = Guid.NewGuid(), Name = "cocoa" },
                new CategoryDao() { Id = Guid.NewGuid(), Name = "coffee / tea" },
                new CategoryDao() { Id = Guid.NewGuid(), Name = "ordinary drink" },
                new CategoryDao() { Id = Guid.NewGuid(), Name = "shot" }
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
                new GlassDao() { Id = Guid.NewGuid(), Name = "balloon glass" },
                new GlassDao() { Id = Guid.NewGuid(), Name = "beer glass" },
                new GlassDao() { Id = Guid.NewGuid(), Name = "cocktail glass" },
                new GlassDao() { Id = Guid.NewGuid(), Name = "coffee mug" },
                new GlassDao() { Id = Guid.NewGuid(), Name = "jar" },
                new GlassDao() { Id = Guid.NewGuid(), Name = "whiskey glass" },
                new GlassDao() { Id = Guid.NewGuid(), Name = "wine glass" },
                new GlassDao() { Id = Guid.NewGuid(), Name = "highball glass" },
                new GlassDao() { Id = Guid.NewGuid(), Name = "highball glass" }
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
                new IngredientDao() { Id = Guid.NewGuid(), Name = "7-up"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "absinthe"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "banana"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "beer"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "blue curacao"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "chocolate"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "cider"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "coffee"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "dark rum"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "honey"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "ice"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "lemon"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "vodka"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "whisky"},
                new IngredientDao() { Id = Guid.NewGuid(), Name = "wine"},
            }.AsQueryable();


            var mockIngredient = new Mock<DbSet<IngredientDao>>();
            mockIngredient.As<IQueryable<IngredientDao>>().Setup(m => m.Provider).Returns(ingredientDatas.Provider);
            mockIngredient.As<IQueryable<IngredientDao>>().Setup(m => m.Expression).Returns(ingredientDatas.Expression);
            mockIngredient.As<IQueryable<IngredientDao>>().Setup(m => m.ElementType).Returns(ingredientDatas.ElementType);
            mockIngredient.As<IQueryable<IngredientDao>>().Setup(m => m.GetEnumerator()).Returns(ingredientDatas.GetEnumerator());

            mockContext.Setup(c => c.Ingredients).Returns(mockIngredient.Object);
            #endregion

            #region userDatas
            var userDatas = new List<UserDao>()
            {
                new UserDao() { 
                    Id = Guid.NewGuid(), 
                    CreationDate = DateTime.Now, 
                    Email = "admin@gmail.com", 
                    FirstName = "john", 
                    LastName="doe", 
                    Password="password", 
                    Role = UserRole.Admin
                },
               new UserDao() {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    Email = "jeandupont@gmail.com",
                    FirstName = "jean",
                    LastName="dupont",
                    Password="password",
                    Role = UserRole.User
                },
               new UserDao() {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now,
                    Email = "tatigertrude@gmail.com",
                    FirstName = "tati",
                    LastName="gertrude",
                    Password="password",
                    Role = UserRole.Unidentified
                },

            }.AsQueryable();


            var mockUser = new Mock<DbSet<UserDao>>();
            mockUser.As<IQueryable<UserDao>>().Setup(m => m.Provider).Returns(userDatas.Provider);
            mockUser.As<IQueryable<UserDao>>().Setup(m => m.Expression).Returns(userDatas.Expression);
            mockUser.As<IQueryable<UserDao>>().Setup(m => m.ElementType).Returns(userDatas.ElementType);
            mockUser.As<IQueryable<UserDao>>().Setup(m => m.GetEnumerator()).Returns(userDatas.GetEnumerator());

            mockContext.Setup(c => c.Users).Returns(mockUser.Object);
            #endregion

            #region DrinkDatas
            var DrinkDatas = new List<DrinkDao>()
            {
                new DrinkDao() { 
                    Id = Guid.NewGuid(), 
                    Name = "7-up"
                },
                
            }.AsQueryable();


            var mockDrink = new Mock<DbSet<DrinkDao>>();
            mockDrink.As<IQueryable<DrinkDao>>().Setup(m => m.Provider).Returns(DrinkDatas.Provider);
            mockDrink.As<IQueryable<DrinkDao>>().Setup(m => m.Expression).Returns(DrinkDatas.Expression);
            mockDrink.As<IQueryable<DrinkDao>>().Setup(m => m.ElementType).Returns(DrinkDatas.ElementType);
            mockDrink.As<IQueryable<DrinkDao>>().Setup(m => m.GetEnumerator()).Returns(DrinkDatas.GetEnumerator());

            mockContext.Setup(c => c.Ingredients).Returns(mockIngredient.Object);
            #endregion





            return mockContext;
        }
    }
}
