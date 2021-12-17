using AutoFixture;
using MyCocktail.Domain.Aggregates.DrinkAggregate;
using MyCocktail.Infrastucture.Mapper;
using System;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class DrinkExtensionTests
    {
        private readonly Fixture _fixture;

        public DrinkExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToDao_WhenValidParameters_ShouldReturnDrinkDao()
        {
            //Arrange
            var drink = new Drink()
            {
                Alcoholic = _fixture.Create<Alcoholic>(),
                Category = _fixture.Create<Category>(),
                DateModified = DateTime.Now,
                Glass = _fixture.Create<Glass>(),
                Id = Guid.NewGuid(),
                IdOwner = Guid.NewGuid(),
                IdSource = null,
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>(),
            };

            //Act
            var result = drink.ToDao();

            //Assert
            Assert.True(result.AlcoholicId == drink.Alcoholic.Id);
            Assert.True(result.Alcoholic.Id == drink.Alcoholic.Id);
            Assert.True(result.Alcoholic.Name == drink.Alcoholic.Name);
            Assert.True(result.CategoryId == drink.Category.Id);
            Assert.True(result.Category.Id == drink.Category.Id);
            Assert.True(result.Category.Name == drink.Category.Name);
            Assert.True(result.DateModified == drink.DateModified);
            Assert.True(result.GlassId == drink.Glass.Id);
            Assert.True(result.Glass.Id == drink.Glass.Id);
            Assert.True(result.Glass.Name == drink.Glass.Name);
            Assert.True(result.Id == drink.Id);
            Assert.True(result.IdSource == drink.IdSource);
            Assert.True(result.Instruction == drink.Instruction);
            Assert.True(result.Name == drink.Name);
            Assert.True(result.OwnerId == drink.IdOwner);
            Assert.True(result.UrlPicture == drink.UrlPicture.ToString());
        }

        [Fact]
        public void ToDao_WhenParametersHaveNoId_ShouldReturnDrinkDao()
        {
            //Arrange
            var drink = new Drink()
            {
                Alcoholic = new Alcoholic() { Id = null, Name = _fixture.Create<string>() },
                Category = new Category() { Id = null , Name = _fixture.Create<string>() },
                DateModified = DateTime.Now,
                Glass = new Glass() { Id = null, Name = _fixture.Create<string>() },
                Id = Guid.NewGuid(),
                IdOwner = Guid.NewGuid(),
                IdSource = null,
                Instruction = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                UrlPicture = _fixture.Create<Uri>(),
            };

            //Act
            var result = drink.ToDao();

            //Assert
            Assert.True(result.Alcoholic.Name == drink.Alcoholic.Name);
            Assert.True(result.Category.Name == drink.Category.Name);
            Assert.True(result.DateModified == drink.DateModified);
            Assert.True(result.Glass.Name == drink.Glass.Name);
            Assert.True(result.Id == drink.Id);
            Assert.True(result.IdSource == drink.IdSource);
            Assert.True(result.Instruction == drink.Instruction);
            Assert.True(result.Name == drink.Name);
            Assert.True(result.OwnerId == drink.IdOwner);
            Assert.True(result.UrlPicture == drink.UrlPicture.ToString());
        }
    }
}
