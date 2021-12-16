using AutoFixture;
using FluentAssertions;
using MyCocktail.Domain.EntitySource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyCocktail.Domain.UnitTests.EntitySource
{
    public class DrinkSourceTests
    {

        private readonly Fixture _fixture;

        public DrinkSourceTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Constructor_WhenValidParameters_ShouldReturnDrinkSource()
        {
            //Arrange

            //Act
            var drinkSource = _fixture.Create<DrinkSource>();

            //Assert
            Assert.NotNull(drinkSource);
            drinkSource.Should().BeOfType<DrinkSource>();
        }

        [Fact]
        public void Constructor_WhenValidParameters_ShouldReturnDrinksSource()
        {
            //Arrange

            //Act
            var drinkSource = _fixture.Create<DrinksSource>();

            //Assert
            Assert.NotNull(drinkSource);
            drinkSource.Should().BeOfType<DrinksSource>();
        }
    }
}
