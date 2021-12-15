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
    public class CategoryTests
    {

        private readonly Fixture _fixture;

        public CategoryTests()
        {
            _fixture = new Fixture();
        }

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

        [Fact]
        public void GetHascode_WhenDifferentCategory_ShouldReturnNonEqualHashCode()
        {
            //Arrange
            var category1 = _fixture.Create<Category>();
            var category2 = _fixture.Create<Category>();

            //Act
            var hashCode1 = category1.GetHashCode();
            var hashCode2 = category2.GetHashCode();

            //Assert
            Assert.False(category1.Id == category2.Id && category1.Name == category2.Name);
            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHascode_WhenSameCategoryPropertiesValue_ShouldReturnEqualHashCode()
        {
            //Arrange
            var category1 = _fixture.Create<Category>();
            var category2 = new Category() { Id = category1.Id, Name = category1.Name };

            //Act
            var hashCode1 = category1.GetHashCode();
            var hashCode2 = category2.GetHashCode();

            //Assert
            Assert.True(category1.Id == category2.Id && category1.Name == category2.Name);
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void Equals_WhenDifferentCategory_ShouldReturnFalse()
        {
            //Arrange
            var category1 = _fixture.Create<Category>();
            var category2 = _fixture.Create<Category>();

            //Act
            var result1 = category1.Equals(category2);
            var result2 = category2.Equals(category1);

            //Assert
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void Equals_WhenSameCategoryPropertiesValue_ShouldReturnTrue()
        {
            //Arrange
            var category1 = _fixture.Create<Category>();
            var category2 = new Category() { Id = category1.Id, Name = category1.Name };

            //Act
            var result1 = category1.Equals(category2);
            var result2 = category2.Equals(category1);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
        }
    }
}
