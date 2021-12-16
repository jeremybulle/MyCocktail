using AutoFixture;
using Castle.Core.Internal;
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

        [Fact]
        public void GetIngredientStr_WhenValidParameters_ShouldReturnDictionnaryWithAllIngredient()
        {
            //Arrange
            var drinkSource = _fixture.Create<DrinkSource>();

            //Act
            var result = drinkSource.GetIngredientStr();

            //Assert
            Assert.True(drinkSource.strIngredient1.IsNullOrEmpty() ? result[1] == null : result[1] == drinkSource.strIngredient1);
            Assert.True(drinkSource.strIngredient2.IsNullOrEmpty() ? result[2] == null : result[2] == drinkSource.strIngredient2);
            Assert.True(drinkSource.strIngredient3.IsNullOrEmpty() ? result[3] == null : result[3] == drinkSource.strIngredient3);
            Assert.True(drinkSource.strIngredient4.IsNullOrEmpty() ? result[4] == null : result[4] == drinkSource.strIngredient4);
            Assert.True(drinkSource.strIngredient5.IsNullOrEmpty() ? result[5] == null : result[5] == drinkSource.strIngredient5);
            Assert.True(drinkSource.strIngredient6.IsNullOrEmpty() ? result[6] == null : result[6] == drinkSource.strIngredient6);
            Assert.True(drinkSource.strIngredient7.IsNullOrEmpty() ? result[7] == null : result[7] == drinkSource.strIngredient7);
            Assert.True(drinkSource.strIngredient8.IsNullOrEmpty() ? result[8] == null : result[8] == drinkSource.strIngredient8);
            Assert.True(drinkSource.strIngredient9.IsNullOrEmpty() ? result[9] == null : result[9] == drinkSource.strIngredient9);
            Assert.True(drinkSource.strIngredient10.IsNullOrEmpty() ? result[10] == null : result[10] == drinkSource.strIngredient10);
            Assert.True(drinkSource.strIngredient11.IsNullOrEmpty() ? result[11] == null : result[11] == drinkSource.strIngredient11);
            Assert.True(drinkSource.strIngredient12.IsNullOrEmpty() ? result[12] == null : result[12] == drinkSource.strIngredient12);
            Assert.True(drinkSource.strIngredient13.IsNullOrEmpty() ? result[13] == null : result[13] == drinkSource.strIngredient13);
            Assert.True(drinkSource.strIngredient14.IsNullOrEmpty() ? result[14] == null : result[14] == drinkSource.strIngredient14);
            Assert.True(drinkSource.strIngredient15.IsNullOrEmpty() ? result[15] == null : result[15] == drinkSource.strIngredient15);
        }

        [Fact]
        public void GetMeasureStr_WhenValidParameters_ShouldReturnDictionnaryWithAllMeasures()
        {
            //Arrange
            var drinkSource = _fixture.Create<DrinkSource>();

            //Act
            var result = drinkSource.GetMeasureStr();

            //Assert
            Assert.True(drinkSource.strMeasure1.IsNullOrEmpty() ? result[1] == null : result[1] == drinkSource.strMeasure1);
            Assert.True(drinkSource.strMeasure2.IsNullOrEmpty() ? result[2] == null : result[2] == drinkSource.strMeasure2);
            Assert.True(drinkSource.strMeasure3.IsNullOrEmpty() ? result[3] == null : result[3] == drinkSource.strMeasure3);
            Assert.True(drinkSource.strMeasure4.IsNullOrEmpty() ? result[4] == null : result[4] == drinkSource.strMeasure4);
            Assert.True(drinkSource.strMeasure5.IsNullOrEmpty() ? result[5] == null : result[5] == drinkSource.strMeasure5);
            Assert.True(drinkSource.strMeasure6.IsNullOrEmpty() ? result[6] == null : result[6] == drinkSource.strMeasure6);
            Assert.True(drinkSource.strMeasure7.IsNullOrEmpty() ? result[7] == null : result[7] == drinkSource.strMeasure7);
            Assert.True(drinkSource.strMeasure8.IsNullOrEmpty() ? result[8] == null : result[8] == drinkSource.strMeasure8);
            Assert.True(drinkSource.strMeasure9.IsNullOrEmpty() ? result[9] == null : result[9] == drinkSource.strMeasure9);
            Assert.True(drinkSource.strMeasure10.IsNullOrEmpty() ? result[10] == null : result[10] == drinkSource.strMeasure10);
            Assert.True(drinkSource.strMeasure11.IsNullOrEmpty() ? result[11] == null : result[11] == drinkSource.strMeasure11);
            Assert.True(drinkSource.strMeasure12.IsNullOrEmpty() ? result[12] == null : result[12] == drinkSource.strMeasure12);
            Assert.True(drinkSource.strMeasure13.IsNullOrEmpty() ? result[13] == null : result[13] == drinkSource.strMeasure13);
            Assert.True(drinkSource.strMeasure14.IsNullOrEmpty() ? result[14] == null : result[14] == drinkSource.strMeasure14);
            Assert.True(drinkSource.strMeasure15.IsNullOrEmpty() ? result[15] == null : result[15] == drinkSource.strMeasure15);
        }
    }
}
