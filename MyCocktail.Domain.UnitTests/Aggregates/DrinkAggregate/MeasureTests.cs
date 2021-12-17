using AutoFixture;
using FluentAssertions;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using System;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.DrinkAggregate
{
    public class MeasureTests
    {
        private readonly Fixture _fixture;
        public MeasureTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Constructor_WithValidProperties_ShouldNotThrowException()
        {
            //Arrange
            var ingredient = _fixture.Create<Ingredient>();

            //Act
            var ex = Record.Exception(() => new Measure()
            {
                Id = Guid.NewGuid(),
                Ingredient = ingredient,
                Quantity = _fixture.Create<string>()
            });

            //Assert
            ex.Should().BeNull();
        }

        [Fact]
        public void Constructor_WithNullIngredient_ShouldThrowArgumentNullException()
        {
            //Arrange
            Ingredient ingredient = null;

            //Act
            var ex = Record.Exception(() => new Measure()
            {
                Id = Guid.NewGuid(),
                Ingredient = ingredient,
                Quantity = _fixture.Create<string>()
            });

            //Assert
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void Constructor_WithValidQuantity_ShouldHaveTrimedName()
        {
            //Arrange
            var ingredient = _fixture.Create<Ingredient>();
            var measure = new Measure()
            {
                Ingredient = ingredient,
                Quantity = " 1 Can Sweetened "
            };

            //Act
            bool result;
            if (measure.Quantity[0] == ' ' || measure.Quantity[measure.Quantity.Length - 1] == ' ')
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
        public void Constructor_WithValidQuantity_ShouldHaveLowerCasedName()
        {
            //Arrange
            var ingredient = _fixture.Create<Ingredient>();
            var measure = new Measure()
            {
                Ingredient = ingredient,
                Quantity = " 1 Can SweeTened "
            };

            //Act
            bool result;
            if (measure.Quantity.Contains("C") || measure.Quantity.Contains("S") || measure.Quantity.Contains("T"))
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
        public void Constructor_WhenNullOrEmptyQuantity_ShouldReturnEmptyString()
        {
            //Arrange
            var ingredient = _fixture.Create<Ingredient>();
            var expected = "";

            //Act
            var measure1 = new Measure()
            {
                Ingredient = ingredient,
                Quantity = null
            };

            var measure2 = new Measure()
            {
                Ingredient = ingredient,
                Quantity = ""
            };

            //Assert
            Assert.Equal(measure1.Quantity, expected);
            measure2.Quantity.Should().BeEquivalentTo("");
        }

        [Fact]
        public void idProperty_WhenNull_ShouldReturnNull()
        {
            //Arrange
            var ingredient = _fixture.Create<Ingredient>();
            var measure = new Measure() { Id = null, Ingredient = ingredient, Quantity = "Arthour Couillaire" };

            //Act
            var result = measure.Id;

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void idProperty_WhenValidId_ShouldReturnSameId()
        {
            //Arrange
            var ingredient = _fixture.Create<Ingredient>();
            var id = Guid.NewGuid();
            var measure = new Measure() { Id = id, Ingredient = ingredient, Quantity = "Arthour Couillaire" };

            //Act
            var result = measure.Id;

            //Assert
            Assert.Equal(id, result);
        }
    }
}
