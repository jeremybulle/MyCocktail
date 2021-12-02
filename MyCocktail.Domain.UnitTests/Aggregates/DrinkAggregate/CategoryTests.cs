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
    public class CategoryTests
    {
        [Fact]
        public void Constructor_WithValidProperties_ShouldNotThrowException()
        {
            //Arrange

            //Act
            var ex = Record.Exception(() => new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Homemade Liqueur"
            });

            //Assert
            ex.Should().BeNull();
        }

        [Fact]
        public void Constructor_WithValidProperties_ShouldHaveTrimedName()
        {
            //Arrange
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = " homemade liqueur "
            };

            //Act
            bool result;
            if (category.Name[0] == ' ' || category.Name[category.Name.Length - 1] == ' ')
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
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "HomemaDe liquEur"
            };

            //Act
            bool result;
            if (category.Name.Contains("H") || category.Name.Contains("D") || category.Name.Contains("E"))
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
            var ex1 = Record.Exception(() => new Category()
            {
                Name = null
            });
            var ex2 = Record.Exception(() => new Category()
            {
                Name = ""
            });

            //Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }
    }
}
