using AutoFixture;
using FluentAssertions;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Mapper;
using System;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class IngredientExtensionTests
    {
        private readonly Fixture _fixture;

        public IngredientExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToDao_WhenValidParameters_ShouldReturnIngredientDao()
        {
            //Arrange
            var ingredient = _fixture.Create<Ingredient>();

            //Act
            var result = ingredient.ToDao();

            //Assert
            Assert.True(result.Id == ingredient.Id);
            Assert.True(result.Name == ingredient.Name);
        }

        [Fact]
        public void ToDao_WhenIngredientHasNullId_ShouldReturnIngredientDaoWithNotNullId()
        {
            //Arrange
            var ingredient = new Ingredient() { Id = null, Name = _fixture.Create<string>() };

            //Act
            var result = ingredient.ToDao();

            //Assert
            Assert.True(result.Name == ingredient.Name);
        }

        [Fact]
        public void ToDao_WhenNullParameters_ShouldThrowArgumentNullException()
        {
            //Arrange
            Ingredient ingredient = null;

            //Act
            var ex = Record.Exception( () => ingredient.ToDao());

            //Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }
    }
}
