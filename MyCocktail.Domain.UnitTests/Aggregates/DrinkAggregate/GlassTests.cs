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
    public class GlassTests
    {
        [Fact]
        public void Constructor_WithValidProperties_ShouldNotThrowException()
        {
            //Arrange

            //Act
            var ex = Record.Exception(() => new Glass()
            {
                Id = Guid.NewGuid(),
                Name = "Beer Glass"
            });

            //Assert
            ex.Should().BeNull();
        }

        [Fact]
        public void Constructor_WithValidProperties_ShouldHaveTrimedName()
        {
            //Arrange
            var glass = new Glass()
            {
                Id = Guid.NewGuid(),
                Name = " Beer Glass "
            };

            //Act
            bool result;
            if (glass.Name[0] == ' ' || glass.Name[glass.Name.Length - 1] == ' ')
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
            var glass = new Glass()
            {
                Id = Guid.NewGuid(),
                Name = "BeeR Glass"
            };

            //Act
            bool result;
            if (glass.Name.Contains("B") || glass.Name.Contains("R") || glass.Name.Contains("G"))
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
            var ex1 = Record.Exception(() => new Glass()
            {
                Name = null
            });
            var ex2 = Record.Exception(() => new Glass()
            {
                Name = ""
            });

            //Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }
    }
}
