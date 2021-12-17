using AutoFixture;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Mapper
{
    public class UserDaoExtensionTests
    {
        private readonly Fixture _fixture;

        public UserDaoExtensionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToModel_WhenValidParameters_ShouldReturnUser()
        {
            //Arrange
            var userDao = new UserDao()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                DrinksOwned = new List<DrinkDao>(),
                Email = "toto@gmail.com",
                Favorites = new List<FavoriteDao>(),
                FirstName = "john",
                LastName = "doe",
                Password = _fixture.Create<string>(),
                Role = UserRole.Unidentified,
                UserName = _fixture.Create<string>()
            };

            //Act
            var result = userDao.ToModel();

            //Assert
            Assert.True(result.Id == userDao.Id);
            Assert.True(result.CreationDate.Date == userDao.CreationDate.Date);
            Assert.True(result.Email == userDao.Email);
            Assert.True(result.FirstName == userDao.FirstName);
            Assert.True(result.LastName == userDao.LastName);
            Assert.True(result.Password == userDao.Password);
            Assert.True(result.Role == userDao.Role);
            Assert.True(result.UserName == userDao.UserName);
        }

        [Fact]
        public void ToModel_WhenValidParameters_ShouldReturnIEnumerableUser()
        {
            //Arrange
            var usersDao = new List<UserDao>();

            for (int i = 0; i < 3; i++)
            {
                usersDao.Add(
                    new UserDao()
                    {
                        Id = Guid.NewGuid(),
                        CreationDate = DateTime.Now,
                        DrinksOwned = new List<DrinkDao>(),
                        Email = "toto@gmail.com",
                        Favorites = new List<FavoriteDao>(),
                        FirstName = "john",
                        LastName = "doe",
                        Password = _fixture.Create<string>(),
                        Role = UserRole.Unidentified,
                        UserName = _fixture.Create<string>(),
                    }
                );
            }

            //Act
            var result = usersDao.ToModel();

            //Assert
            Assert.True(usersDao.All(u => result.Any(u2 =>
            u2.Id == u.Id &&
            u2.CreationDate.Date == u.CreationDate.Date &&
            u2.Email == u.Email &&
            u2.FirstName == u.FirstName &&
            u2.LastName == u.LastName &&
            u2.Password == u.Password &&
            u2.Role == u.Role &&
            u2.UserName == u.UserName)));
        }
    }
}
