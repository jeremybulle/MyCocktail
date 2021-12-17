using AutoFixture;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Mapper;
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
    }
}
