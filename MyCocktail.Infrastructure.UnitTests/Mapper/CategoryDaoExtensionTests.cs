using AutoFixture;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Mapper;
using System;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class CategoryDaoExtensionTests
    {
        private readonly Fixture _fixture;

        public CategoryDaoExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToModel_WhenValidParameters_ShouldReturnCategory()
        {
            //Arrange
            var categoryDao = new CategoryDao()
            {
                Id = Guid.NewGuid(),
                Name = _fixture.Create<string>()
            };

            //Assert
            var result = categoryDao.ToModel();

            //Act
            Assert.True(result.Id == categoryDao.Id);
            Assert.True(result.Name == categoryDao.Name);
        }
    }
}
