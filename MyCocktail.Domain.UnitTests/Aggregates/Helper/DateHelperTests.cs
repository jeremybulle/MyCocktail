using FluentAssertions;
using MyCocktail.Domain.Helper;
using System;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.Helper
{
    public class DateHelperTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]

        public void DateFromString_WithNullOrEmptyParameter_ShouldThrowArgumentNullException(string inputDate)
        {
            //Arrange
            var dateAsString = inputDate;

            //Act
            Action act = () => DateHelper.DateFromString(dateAsString);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void DateFromString_WithValidParameter_ShouldReturnValidDateTime()
        {
            //Arrange
            var dateAsString1 = "2021-12-30 11:06:32";
            var expected1 = new DateTime(2021, 12, 30, 11, 06, 32);
            var dateAsString2 = "2021-12-02 00:00:00";
            var expected2 = new DateTime(2021, 12, 02);

            //Act
            var result1 = DateHelper.DateFromString(dateAsString1);
            var result2 = DateHelper.DateFromString(dateAsString2);

            //Assert
            Assert.Equal(expected1, result1);
            Assert.Equal(expected2, result2);
        }

        [Theory]
        [InlineData("-2021-12-02 11:30:42")]
        [InlineData("2021--12-02 11:30:42")]
        [InlineData("2021--12-02 11:-30:42")]
        [InlineData("2021--02 11:30:42")]
        [InlineData("2021-02 11:30:42")]
        public void DateFromString_WithNoValidParameter_ShouldThrowArgumentException(string inputDate)
        {
            //Arrange
            var dateAsString = inputDate;

            //Act
            Action act = () => DateHelper.DateFromString(dateAsString);

            //Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
