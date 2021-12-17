using AutoFixture;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Domain.Helper;
using MyCocktail.Infrastucture.Mapper;
using System;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class UserExtensionTests
    {
        private readonly Fixture _fixture;

        public UserExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToDao_WhenValidParameters_ShouldReturnUserDao()
        {
            //Arrange
            var user = new User()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Email = "toto@gmail.com",
                FirstName = "john",
                LastName = "doe",
                Password = _fixture.Create<string>(),
                Role = UserRole.Unidentified,
                UserName = _fixture.Create<string>(),
            };

            //Act
            var result = user.ToDao();

            //Assert
            Assert.True(result.CreationDate.Date == user.CreationDate.Date);
            Assert.True(result.Email == user.Email);
            Assert.True(result.FirstName == user.FirstName);
            Assert.True(result.Id == user.Id);
            Assert.True(PasswordHasher.Check(result.Password, user.Password));
            Assert.True(result.Role == user.Role);
            Assert.True(result.UserName == user.UserName);
        }


        [Fact]
        public void ToDao_WhenValidParametersAndNullId_ShouldReturnUserDao()
        {
            //Arrange
            var user = new User()
            {
                Id = null,
                CreationDate = DateTime.Now,
                Email = "toto@gmail.com",
                FirstName = "john",
                LastName = "doe",
                Password = _fixture.Create<string>(),
                Role = UserRole.Unidentified,
                UserName = _fixture.Create<string>(),
            };

            //Act
            var result = user.ToDao();

            //Assert
            Assert.True(result.CreationDate.Date == user.CreationDate.Date);
            Assert.True(result.Email == user.Email);
            Assert.True(result.FirstName == user.FirstName);
            Assert.True(PasswordHasher.Check(result.Password, user.Password));
            Assert.True(result.Role == user.Role);
            Assert.True(result.UserName == user.UserName);
        }

        [Fact]
        public void ToDao_WhenNullParameters_ShouldReturnNull()
        {
            //Arrange
            User user = null;

            //Act
            var result = user.ToDao();

            //Assert
            Assert.Null(result);
        }
    }
}
