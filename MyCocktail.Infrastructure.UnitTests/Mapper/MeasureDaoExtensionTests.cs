using AutoFixture;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Mapper;
using System;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class MeasureDaoExtensionTests
    {
        private readonly Fixture _fixture;

        public MeasureDaoExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToModel_WhenValidParameters_ShouldReturnMeasure()
        {
            //Arrange
            var measureDao = new MeasureDao()
            {
                Id = Guid.NewGuid(),
                Ingredient = new IngredientDao() { Id = Guid.NewGuid(), Name = _fixture.Create<string>() },
                Quantity = _fixture.Create<int>().ToString()
            };

            //Act
            var result = measureDao.ToModel();

            //Assert
            Assert.True(result.Id == measureDao.Id);
            Assert.True(result.Ingredient.Id == measureDao.Ingredient.Id);
            Assert.True(result.Ingredient.Name == measureDao.Ingredient.Name);
            Assert.True(result.Quantity == measureDao.Quantity);
        }
    }
}
