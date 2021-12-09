using AutoFixture;
using FluentAssertions;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
