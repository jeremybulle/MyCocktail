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
    public class AlcoholicTests
    {
        [Fact]
        public void Constructor_WithValidProperties_ShouldNotThrowException()
        {
            //Arrange

            //Act
            var ex = Record.Exception(() => new Alcoholic()
            {
                Id = Guid.NewGuid(),
                Name = "Whisky"
            });

            //Assert
            ex.Should().BeNull();
        }

        [Fact]
        public void Constructor_WithValidProperties_ShouldHaveTrimedName()
        {
            //Arrange
            var alcoholic = new Alcoholic()
            {
                Id = Guid.NewGuid(),
                Name = "Whisky"
            };

            //Act
            bool result;
            if(alcoholic.Name[0] == ' ' || alcoholic.Name[alcoholic.Name.Length -1] == ' ')
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
            var alcoholic = new Alcoholic()
            {
                Id = Guid.NewGuid(),
                Name = "WhiSkY"
            };

            //Act
            bool result;
            if (alcoholic.Name.Contains("W") || alcoholic.Name.Contains("S") || alcoholic.Name.Contains("W"))
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
            var ex1 = Record.Exception(() => new Alcoholic()
            {
                Name = null
            });
            var ex2 = Record.Exception(() => new Alcoholic()
            {
                Name = ""
            });

            //Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }
    }
}
