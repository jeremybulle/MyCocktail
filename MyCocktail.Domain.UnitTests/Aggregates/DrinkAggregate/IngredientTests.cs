using AutoFixture;
using FluentAssertions;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using System;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.DrinkAggregate
{
    public class IngredientTests
    {
        private readonly Fixture _fixture;

        public IngredientTests()
        {
            _fixture = new Fixture();
        }


        [Fact]
        public void Constructor_WithValidProperties_ShouldNotThrowException()
        {
            //Arrange

            //Act
            var ex = Record.Exception(() => new Ingredient()
            {
                Id = Guid.NewGuid(),
                Name = "Lemon juice"
            });

            //Assert
            ex.Should().BeNull();
        }

        [Fact]
        public void Constructor_WithValidProperties_ShouldHaveTrimedName()
        {
            //Arrange
            var ingredient = new Ingredient()
            {
                Id = Guid.NewGuid(),
                Name = " Lemon Juice "
            };

            //Act
            bool result;
            if (ingredient.Name[0] == ' ' || ingredient.Name[ingredient.Name.Length - 1] == ' ')
            {
                result = true;
            }
            else
            {
                result = false;
            }

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Constructor_WithValidProperties_ShouldHaveLowerCasedName()
        {
            //Arrange
            var ingredient = new Ingredient()
            {
                Id = Guid.NewGuid(),
                Name = "LemOn juiCe"
            };

            //Act
            bool result;
            if (ingredient.Name.Contains("L") || ingredient.Name.Contains("O") || ingredient.Name.Contains("C"))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Constructor_WhenNonValidName_ShouldThrowArgumentException()
        {
            //Arrange

            //Act
            var ex1 = Record.Exception(() => new Ingredient()
            {
                Name = null
            });
            var ex2 = Record.Exception(() => new Ingredient()
            {
                Name = ""
            });

            //Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void GetHascode_WhenDifferentIngredient_ShouldReturnNonEqualHashCode()
        {
            //Arrange
            var ingredient1 = _fixture.Create<Ingredient>();
            var ingredient2 = _fixture.Create<Ingredient>();

            //Act
            var hashCode1 = ingredient1.GetHashCode();
            var hashCode2 = ingredient2.GetHashCode();

            //Assert
            Assert.False(ingredient1.Id == ingredient2.Id && ingredient1.Name == ingredient2.Name);
            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHascode_WhenSameIngredientPropertiesValue_ShouldReturnEqualHashCode()
        {
            //Arrange
            var ingredient1 = _fixture.Create<Ingredient>();
            var ingredient2 = new Ingredient() { Id = ingredient1.Id, Name = ingredient1.Name };

            //Act
            var hashCode1 = ingredient1.GetHashCode();
            var hashCode2 = ingredient2.GetHashCode();

            //Assert
            Assert.True(ingredient1.Id == ingredient2.Id && ingredient1.Name == ingredient2.Name);
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void Equals_WhenDifferentIngredient_ShouldReturnFalse()
        {
            //Arrange
            var ingredient1 = _fixture.Create<Ingredient>();
            var ingredient2 = _fixture.Create<Ingredient>();

            //Act
            var result1 = ingredient1.Equals(ingredient2);
            var result2 = ingredient2.Equals(ingredient1);

            //Assert
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void Equals_WhenSameIngredientPropertiesValue_ShouldReturnTrue()
        {
            //Arrange
            var ingredient1 = _fixture.Create<Ingredient>();
            var ingredient2 = new Ingredient() { Id = ingredient1.Id, Name = ingredient1.Name };

            //Act
            var result1 = ingredient1.Equals(ingredient2);
            var result2 = ingredient2.Equals(ingredient1);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
        }
    }
}
