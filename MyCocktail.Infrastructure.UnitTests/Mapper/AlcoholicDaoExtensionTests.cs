using AutoFixture;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Mapper;
using System;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class AlcoholicDaoExtensionTests
    {
        private readonly Fixture _fixture;

        public AlcoholicDaoExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToModel_WhenValidParameters_ShouldReturnAlcoholic()
        {
            //Arrange
            var alcoholicDao = new AlcoholicDao()
            {
                Id = Guid.NewGuid(),
                Name = _fixture.Create<string>()
            };

            //Assert
            var result = alcoholicDao.ToModel();

            //Act
            Assert.True(result.Id == alcoholicDao.Id);
            Assert.True(result.Name == alcoholicDao.Name);
        }
    }
}
