using AutoFixture;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class IngredientDaoExtensionTests
    {
        private readonly Fixture _fixture;

        public IngredientDaoExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToModel_WhenValidParameters_ShouldReturnIngredient()
        {
            //Arrange
            var ingredientDao = new IngredientDao()
            {
                Id = Guid.NewGuid(),
                Name = _fixture.Create<string>()
            };

            //Assert
            var result = ingredientDao.ToModel();

            //Act
            Assert.True(result.Id == ingredientDao.Id);
            Assert.True(result.Name == ingredientDao.Name);
        }

        [Fact]
        public void ToModel_WhenValidParameters_ShouldReturnIEnumerableIngredients()
        {
            //Arrange
            var ingredientsDao = new List<IngredientDao>();

            for (int i = 0; i < 3; i++)
            {
                ingredientsDao.Add(new IngredientDao() { Id = Guid.NewGuid(), Name = _fixture.Create<string>() });
            }

            //Assert
            var result = ingredientsDao.ToModel();

            //Act
            Assert.True(ingredientsDao.All(i => result.Any(i2 => i2.Id == i.Id && i2.Name == i.Name)));
            Assert.True(result.Count() == ingredientsDao.Count);
        }
    }
}
