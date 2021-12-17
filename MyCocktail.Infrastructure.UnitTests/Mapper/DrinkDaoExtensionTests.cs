using AutoFixture;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Domain.Helper;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class DrinkDaoExtensionTests
    {
        private readonly Fixture _fixture;

        public DrinkDaoExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToModel_WhenValidParameters_ShouldReturnDrink()
        {
            //Arrange
            var alcoholicDao = new AlcoholicDao() { Id = Guid.NewGuid(), Name = _fixture.Create<string>() };
            var categoryDao = new CategoryDao() { Id = Guid.NewGuid(), Name = _fixture.Create<string>() };
            var date = DateTime.Now;
            var glassDao = new GlassDao() { Id = Guid.NewGuid(), Name = _fixture.Create<string>() };
            var url = _fixture.Create<Uri>();

            var ingredientsDao = new List<IngredientDao>();
            for (int i = 0; i < 3; i++)
            {
                ingredientsDao.Add(new IngredientDao() { Id = Guid.NewGuid(), Name = _fixture.Create<string>() });
            }

            var measuresDao = new List<MeasureDao>();
            for (int i = 0; i < 3; i++)
            {
                measuresDao.Add(new MeasureDao() { Id = Guid.NewGuid(), Ingredient = ingredientsDao[i], IngredientId = ingredientsDao[i].Id, Quantity = _fixture.Create<int>().ToString() });
            }


            var userDao = new UserDao()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                DrinksOwned = new List<DrinkDao>(),
                Email = "toto@gmail.com",
                Favorites = new List<FavoriteDao>(),
                FirstName = _fixture.Create<string>(),
                LastName = _fixture.Create<string>(),
                Password = _fixture.Create<string>(),
                Role = _fixture.Create<UserRole>(),
                UserName = _fixture.Create<string>()
            };

            var drinkDao = new DrinkDao()
            {
                Alcoholic = alcoholicDao,
                AlcoholicId = alcoholicDao.Id,
                Category = categoryDao,
                CategoryId = categoryDao.Id,
                DateModified = date,
                Favorites = new List<FavoriteDao>(),
                Glass = glassDao,
                GlassId = glassDao.Id,
                Id = Guid.NewGuid(),
                Owner = userDao,
                OwnerId = userDao.Id,
                Instruction = _fixture.Create<string>(),
                Measures = measuresDao,
                Name = _fixture.Create<string>(),
                UrlPicture = url.ToString(),
                IdSource = null
            };

            //Assert
            var result = drinkDao.ToModel();

            //Act
            Assert.True(result.Id == drinkDao.Id);
            Assert.True(result.Name == drinkDao.Name);
            Assert.True(result.IdOwner == drinkDao.OwnerId);
            Assert.True(result.IdSource.IsNullOrEmpty() == drinkDao.IdSource.IsNullOrEmpty());
            Assert.True(result.Instruction == drinkDao.Instruction);
            Assert.True(result.UrlPicture.ToString() == drinkDao.UrlPicture);
            Assert.True(result.Alcoholic.Id == drinkDao.Alcoholic.Id);
            Assert.True(result.Alcoholic.Name == drinkDao.Alcoholic.Name);
            Assert.True(result.Category.Id == drinkDao.Category.Id);
            Assert.True(result.Category.Name == drinkDao.Category.Name);
            Assert.True(measuresDao.All(m => result.GetMeasures().Any(m2 => m.Ingredient.Id == m2.Ingredient.Id && m.Ingredient.Name == m2.Ingredient.Name && m.Quantity == m2.Quantity)));
        }

        [Fact]
        public void ToModel_WhenParameterisNull_ShouldReturnNull()
        {
            //Arrange
            DrinkDao drinkDao = null;

            //Act
            var result = drinkDao.ToModel();

            //Assert
            Assert.Null(result);
        }
    }
}
