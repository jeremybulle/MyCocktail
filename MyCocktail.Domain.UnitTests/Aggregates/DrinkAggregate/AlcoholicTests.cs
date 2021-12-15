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
    public class AlcoholicTests
    {
        private readonly Fixture _fixture;

        public AlcoholicTests()
        {
            _fixture = new Fixture();
        }

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

        [Fact]
        public void GetHascode_WhenDifferentAlcoholic_ShouldReturnNonEqualHashCode()
        {
            //Arrange
            var alcoholic1 = _fixture.Create<Alcoholic>();
            var alcoholic2 = _fixture.Create<Alcoholic>();

            //Act
            var hashCode1 = alcoholic1.GetHashCode();
            var hashCode2 = alcoholic2.GetHashCode();

            //Assert
            Assert.False(alcoholic1.Id == alcoholic2.Id && alcoholic1.Name == alcoholic2.Name);
            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHascode_WhenSameAlcoholicPropertiesValue_ShouldReturnEqualHashCode()
        {
            //Arrange
            var alcoholic1 = _fixture.Create<Alcoholic>();
            var alcoholic2 = new Alcoholic() { Id = alcoholic1.Id, Name = alcoholic1.Name };

            //Act
            var hashCode1 = alcoholic1.GetHashCode();
            var hashCode2 = alcoholic2.GetHashCode();

            //Assert
            Assert.True(alcoholic1.Id == alcoholic2.Id && alcoholic1.Name == alcoholic2.Name);
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void Equals_WhenDifferentAlcoholic_ShouldReturnFalse()
        {
            //Arrange
            var alcoholic1 = _fixture.Create<Alcoholic>();
            var alcoholic2 = _fixture.Create<Alcoholic>();

            //Act
            var result1 = alcoholic1.Equals(alcoholic2);
            var result2 = alcoholic2.Equals(alcoholic1);

            //Assert
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void Equals_WhenSameAlcoholicPropertiesValue_ShouldReturnTrue()
        {
            //Arrange
            var alcoholic1 = _fixture.Create<Alcoholic>();
            var alcoholic2 = new Alcoholic() { Id = alcoholic1.Id, Name = alcoholic1.Name };

            //Act
            var result1 = alcoholic1.Equals(alcoholic2);
            var result2 = alcoholic2.Equals(alcoholic1);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
        }
    }
}
