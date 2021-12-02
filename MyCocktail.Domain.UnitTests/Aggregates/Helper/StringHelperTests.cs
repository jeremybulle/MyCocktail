using FluentAssertions;
using MyCocktail.Domain.Helper;
using System;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.Helper
{
    public class StringHelperTests
    {
        [Fact]
        public void ContainUnAuhtorizedChar_WhithValidParameters_ShouldReturnFalse()
        {
            //Arrange
            var stringToTest = "Toto";

            //Act
            var result = stringToTest.ContainUnAuhtorizedChar("a");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ContainUnAuhtorizedChar_WhithNotValidParameters_ShouldReturnFalse()
        {
            //Arrange
            var stringToTest = "Toto";

            //Act
            var result = stringToTest.ContainUnAuhtorizedChar("o");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainUnAuhtorizedChar_WhithNullParameters_ShouldThrowArgumentNullException()
        {
            //Arrange
            var stringToTest1 = "Toto";
            string stringToTest2 = null;

            //Act
            Action act1 = () => stringToTest1.ContainUnAuhtorizedChar(null);
            Action act2 = () => stringToTest2.ContainUnAuhtorizedChar();

            //Assert
            act1.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'UnAutorizedChar')");
            act2.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'stringToTest')");
        }
    }
}
