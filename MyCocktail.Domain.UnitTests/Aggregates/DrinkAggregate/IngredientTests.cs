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
    public class IngredientTests
    {
        public class GlassTests
        {
            [Fact]
            public void Constructor_WithValidProperties_ShouldNotThrowException()
            {
                //Arrange

                //Act
                var ex = Record.Exception(() => new Ingredient()
                {
                    Id = Guid.NewGuid(),
                    Name = "Lemon juice"
                });

                //Assert
                ex.Should().BeNull();
            }

            [Fact]
            public void Constructor_WithValidProperties_ShouldHaveTrimedName()
            {
                //Arrange
                var ingredient = new Ingredient()
                {
                    Id = Guid.NewGuid(),
                    Name = " Lemon Juice "
                };

                //Act
                bool result;
                if (ingredient.Name[0] == ' ' || ingredient.Name[ingredient.Name.Length - 1] == ' ')
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
                var ingredient = new Ingredient()
                {
                    Id = Guid.NewGuid(),
                    Name = "LemOn juiCe"
                };

                //Act
                bool result;
                if (ingredient.Name.Contains("L") || ingredient.Name.Contains("O") || ingredient.Name.Contains("C"))
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
                var ex1 = Record.Exception(() => new Ingredient()
                {
                    Name = null
                });
                var ex2 = Record.Exception(() => new Ingredient()
                {
                    Name = ""
                });

                //Assert
                ex1.Should().BeOfType<ArgumentException>();
                ex2.Should().BeOfType<ArgumentException>();
            }
        }
    }
}
