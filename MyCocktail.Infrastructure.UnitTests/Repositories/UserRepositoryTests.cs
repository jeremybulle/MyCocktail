using AutoFixture;
using FluentAssertions;
using MyCocktail.Domain.Aggregates.UserAggregate;
using MyCocktail.Infrastructure.UnitTests.FakeDbContext;
using MyCocktail.Infrastucture;
using MyCocktail.Infrastucture.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyCocktail.Infrastructure.UnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _repo;
        private readonly Fixture _fixture;

        public UserRepositoryTests()
        {
            _repo = new UserRepository(MockDrinkDbContext.GetMockedDbContext().Object);
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
        public async Task AddAsync_WhenPassworIsNull_ShouldThrowArgumentException()
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
            ex.Should().BeOfType<ArgumentException>();
        }
    }
}
