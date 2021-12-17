using AutoFixture;
using FluentAssertions;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using System;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.DrinkAggregate
{
    public class GlassTests
    {
        private readonly Fixture _fixture;

        public GlassTests()
        {
            _fixture = new Fixture();
        }

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

        [Fact]
        public void GetHascode_WhenDifferentGlass_ShouldReturnNonEqualHashCode()
        {
            //Arrange
            var glass1 = _fixture.Create<Glass>();
            var glass2 = _fixture.Create<Glass>();

            //Act
            var hashCode1 = glass1.GetHashCode();
            var hashCode2 = glass2.GetHashCode();

            //Assert
            Assert.False(glass1.Id == glass2.Id && glass1.Name == glass2.Name);
            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHascode_WhenSameGlassPropertiesValue_ShouldReturnEqualHashCode()
        {
            //Arrange
            var glass1 = _fixture.Create<Glass>();
            var glass2 = new Glass() { Id = glass1.Id, Name = glass1.Name };

            //Act
            var hashCode1 = glass1.GetHashCode();
            var hashCode2 = glass2.GetHashCode();

            //Assert
            Assert.True(glass1.Id == glass2.Id && glass1.Name == glass2.Name);
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void Equals_WhenDifferentGlass_ShouldReturnFalse()
        {
            //Arrange
            var glass1 = _fixture.Create<Glass>();
            var glass2 = _fixture.Create<Glass>();

            //Act
            var result1 = glass1.Equals(glass2);
            var result2 = glass2.Equals(glass1);

            //Assert
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void Equals_WhenSameGlassPropertiesValue_ShouldReturnTrue()
        {
            //Arrange
            var glass1 = _fixture.Create<Glass>();
            var glass2 = new Glass() { Id = glass1.Id, Name = glass1.Name };

            //Act
            var result1 = glass1.Equals(glass2);
            var result2 = glass2.Equals(glass1);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
        }
    }
}
