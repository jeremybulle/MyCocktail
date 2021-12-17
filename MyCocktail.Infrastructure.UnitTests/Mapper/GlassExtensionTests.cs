using AutoFixture;
using FluentAssertions;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Mapper;
using System;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class GlassExtensionTests
    {
        private readonly Fixture _fixture;

        public GlassExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToModel_WhenValidParameters_ShouldReturnGlass()
        {
            //Arrange
            var glassDao = new GlassDao()
            {
                Id = Guid.NewGuid(),
                Name = _fixture.Create<string>()
            };

            //Assert
            var result = glassDao.ToModel();

            //Act
            Assert.True(result.Id == glassDao.Id);
            Assert.True(result.Name == glassDao.Name);
        }

        [Fact]
        public void ToModel_WhenNullParameter_ShouldThrowArgumentNullException()
        {
            //Arrange
            GlassDao glassDao = null;

            //Assert
            var ex = Record.Exception( ()=> glassDao.ToModel());

            //Act
            ex.Should().BeOfType<ArgumentNullException>();
        }
    }
}
