using FluentAssertions;
using MyCocktail.Domain.Helper;
using System;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.Helper
{
    public class PasswordHasherTests
    {
        [Fact]
        public void Check_WithValidPassword_ShouldReturnTrue()
        {
            //Arrange
            var passwordToTest = "Toto";
            var passwordHashed = PasswordHasher.Hash("Toto");

            //Act
            var result = PasswordHasher.Check(passwordHashed, passwordToTest);

            //Result
            Assert.True(result);
        }

        [Fact]
        public void Check_WithValidPassword_ShouldReturnFalse()
        {
            //Arrange
            var passwordToTest = "Toto";
            var passwordHashed = PasswordHasher.Hash("Tata");

            //Act
            var result = PasswordHasher.Check(passwordHashed, passwordToTest);

            //Result
            Assert.False(result);
        }

        [Fact]
        public void Hash_WithNullParameter_ShouldThrowNullArgumentException()
        {
            //Arrange
            string passwordToTest = null;

            //Act
            Action act = () => PasswordHasher.Hash(passwordToTest);

            //Result
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'password')");
        }

        [Fact]
        public void Hash_WithvalidParameter_ShouldNotThrowException()
        {
            //Arrange
            string passwordToTest = "Toto";

            //Act
            var ex = Record.Exception(() => PasswordHasher.Hash(passwordToTest));

            //Result
            ex.Should().BeNull();
        }
    }
}
