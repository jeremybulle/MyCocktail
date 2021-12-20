﻿using AutoFixture;
using FluentAssertions;
using Moq;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Infrastructure.UnitTests.FakeDbContext;
using MyCocktail.Infrastucture;
using MyCocktail.Infrastucture.Dao;
using MyCocktail.Infrastucture.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly Mock<DrinkDbContext> _context;
        private readonly IUserRepository _repo;
        private readonly Fixture _fixture;

        public UserRepositoryTests()
        {
            _context = MockDrinkDbContext.GetMockedDbContext();
            _repo = new UserRepository(_context.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public void Constructor_WhenDbContextIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            DrinkDbContext context = null;

            //Act
            var ex = Record.Exception(() => new UserRepository(context));

            //Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_WhenDbContextIsNullValid_ShouldNotThrowException()
        {
            //Arrange
            DrinkDbContext context = MockDrinkDbContext.GetMockedDbContext().Object;

            //Act
            var ex = Record.Exception(() => new UserRepository(context));

            //Assert
            Assert.Null(ex);
        }

        [Fact]
        public async Task AddAsync_WhenParamIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            User user = null;

            //Act
            var ex = await Record.ExceptionAsync(async () =>  await _repo.AddAsync(user));

            //Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task AddAsync_WhenPassworIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            User user = new User()
            {
                UserName = "SuperToto",
                CreationDate = DateTime.Now,
                Email = "totototo@gmail.com",
                FirstName = "toto",
                LastName = "doe",
                Id = Guid.NewGuid(),
                Password = null,
                Role = UserRole.User
            };
            
            //Act
            var ex = await Record.ExceptionAsync(async () => await _repo.AddAsync(user));

            //Assert
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task AddAsync_WhenPassworIsEmpty_ShouldThrowArgumentException()
        {
            //Arrange
            User user = new User()
            {
                UserName = "SuperToto",
                CreationDate = DateTime.Now,
                Email = "totototo@gmail.com",
                FirstName = "toto",
                LastName = "doe",
                Id = Guid.NewGuid(),
                Password = "",
                Role = UserRole.User
            };

            //Act
            var ex = await Record.ExceptionAsync(async () => await _repo.AddAsync(user));

            //Assert
            ex.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public async Task AddAsync_WhenUserIsValid_ShouldNotThrowException()
        {
            //Arrange
            User user = new User()
            {
                UserName = "SuperToto",
                CreationDate = DateTime.Now,
                Email = "totototo@gmail.com",
                FirstName = "toto",
                LastName = "doe",
                Id = Guid.NewGuid(),
                Password = "mdp",
                Role = UserRole.User
            };

            //Act
            var ex = await Record.ExceptionAsync(async () => await _repo.AddAsync(user));

            //Assert
            Assert.Null(ex);
            //Assert.Equal(user.UserName, _context.Object.Users.FirstOrDefault(u => u.Id == user.Id).UserName);
            _context.Verify(c => c.Users.AddAsync(It.IsAny<UserDao>(),It.IsAny<CancellationToken>()), Times.Once);
            
        }
    }
}
