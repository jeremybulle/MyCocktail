using AutoFixture;
using FluentAssertions;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.DrinkAggregate
{
    public class DrinkTests
    {
        private Fixture _fixture;

        public DrinkTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Constructor_WhenValidParameter_ShouldNotThrow()
        {
            //Arrange
            var alcoholic = _fixture.Create<Alcoholic>();
            var category = _fixture.Create<Category>();
            var glass = _fixture.Create<Glass>();

            //Act
            var ex = Record.Exception(() => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            });

            //Assert
            ex.Should().BeNull();

        }

        [Fact]
        public void Constructor_WithNullOrEmptyName_ShouldThrowArgumentException()
        {
            //Arrange
            var alcoholic = _fixture.Create<Alcoholic>();
            var category = _fixture.Create<Category>();
            var glass = _fixture.Create<Glass>();

            //Act
            Action act1 = () => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = null,
                UrlPicture = _fixture.Create<Uri>()
            };

            var ex2 = Record.Exception(() => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = "",
                UrlPicture = _fixture.Create<Uri>()
            });

            //Assert
            act1.Should().Throw<ArgumentException>().WithMessage("Name");
            Assert.IsType<ArgumentException>(ex2);
        }

        [Fact]
        public void Constructor_WhenIdOwnerAndIdSourceAreNullOrEmpty_ShouldThrowArgumentException()
        {
            //Arrange
            var alcoholic = _fixture.Create<Alcoholic>();
            var category = _fixture.Create<Category>();
            var glass = _fixture.Create<Glass>();

            //Act
            Action act1 = () => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdSource = null,
                IdOwner = null,
                Instruction = _fixture.Create<string>(),
                Name = null,
                UrlPicture = _fixture.Create<Uri>()
            };

            Action act2 = () => new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = alcoholic,
                Category = category,
                DateModified = DateTime.Now,
                Glass = glass,
                IdSource = "",
                IdOwner = null,
                Instruction = _fixture.Create<string>(),
                Name = null,
                UrlPicture = _fixture.Create<Uri>()
            };

            //Assert
            act1.Should().Throw<ArgumentException>().WithMessage("IdOwner can't be null if IdSource is null");
            act2.Should().Throw<ArgumentException>().WithMessage("IdOwner can't be null if IdSource is null");
        }

        [Fact]
        public void Constructor_WithValidInstruction_ShouldHaveTrimedInstruction()
        {
            //Arrange
            var instruction = " instructions not trimed ";
            var expected = instruction.Trim();

            //Act
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = instruction,
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };

            //Assert
            Assert.Equal(expected, drink.Instruction);
        }

        [Fact]
        public void AddMeasure_WhenDrinkContainMeasure_ShouldNotThrowException()
        {
            //Arrange
            var measures = _fixture.CreateMany<Measure>(4);
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };

            //Act
            var ex = Record.Exception(() =>
            {
                foreach (var measure in measures)
                {
                    drink.AddMeasure(measure);
                }
            });

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public void GetMeasures_WhenDrinkContainsMeasure_ShouldReturnAllMeasures()
        {
            //Arrange
            var measures = _fixture.CreateMany<Measure>(4);
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };
            foreach (var measure in measures)
            {
                drink.AddMeasure(measure);
            }

            //Act
            var result = drink.GetMeasures();


            //Assert
            Assert.Equal(measures.Count(), result.Count());
            Assert.True(!result.Except(measures).Any());
        }

        [Fact]
        public void GetMeasures_WhenDrinkNotContainsMeasure_ShouldReturnEmptyEnumerable()
        {
            //Arrange
            var measures = new List<Measure>();
            var drink = new Drink()
            {
                Id = Guid.NewGuid(),
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                IdOwner = Guid.NewGuid(),
                IdSource = _fixture.Create<string>(),
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>()
            };
            foreach (var measure in measures)
            {
                drink.AddMeasure(measure);
            }

            //Act
            var result = drink.GetMeasures();


            //Assert
            Assert.Equal(measures.Count(), result.Count());
        }


    }
}
