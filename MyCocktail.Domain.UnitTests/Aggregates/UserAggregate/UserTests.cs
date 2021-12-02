using FluentAssertions;
using MyCocktail.Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.UserAggregate
{
    public class UserTests
    {
        [Fact]
        public void Constructor_WhenValidProperties_ShouldNotThrowException()
        {
            //Arrange

            //Act
            var ex = Record.Exception(() => new User()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Email = "toto@gmail.com",
                FirstName ="John",
                LastName = "Doe",
                Password = "password",
                UserName = "42",
                Role = UserRole.Unidentified
            });

            //Assert
            ex.Should().BeNull();
        }

        [Fact]
        public void Constructor_WhenNonValidUserName_ShouldThrowArgumentException()
        {
            //Arrange

            //Act
            var ex1 = Record.Exception(() => new User()
            {
                UserName = null
            });
            var ex2 = Record.Exception(() => new User()
            {
                UserName = ""
            });

            //Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void Constructor_WhenNonValidFirstName_ShouldThrowArgumentException()
        {
            //Arrange

            //Act
            var ex1 = Record.Exception(() => new User()
            {
                FirstName = null
            });
            var ex2 = Record.Exception(() => new User()
            {
                FirstName = ""
            });
            var ex3 = Record.Exception(() => new User()
            {
                FirstName = "t@t@"
            });

            //Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void Constructor_WhenNonValidLastName_ShouldThrowArgumentException()
        {
            //Arrange

            //Act
            var ex1 = Record.Exception(() => new User()
            {
                LastName = null
            });
            var ex2 = Record.Exception(() => new User()
            {
                LastName = ""
            });
            var ex3 = Record.Exception(() => new User()
            {
                LastName = "t@t@"
            });

            //Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void Constructor_WhenNonValidEmail_ShouldThrowArgumentException()
        {
            //Arrange

            //Act
            var ex1 = Record.Exception(() => new User()
            {
                Email = null
            });
            var ex2 = Record.Exception(() => new User()
            {
                Email = ""
            });
            var ex3 = Record.Exception(() => new User()
            {
                Email = "mailWithoutDotAndWithoutAtSign"
            });

            //Assert
            ex1.Should().BeOfType<ArgumentException>();
            ex2.Should().BeOfType<ArgumentException>();
            ex3.Should().BeOfType<ArgumentException>();
        }

    }
}
