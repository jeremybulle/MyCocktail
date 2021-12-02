using FluentAssertions;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.Helper
{
    public class DateHelperTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]

        public void DateFromString_WithNullOrEmptyParameter_ShouldThrowArgumentNullException(string inputValue)
        {
            //Arrange
            var dateAsString = inputValue;

            //Act

            Action act = () => DateHelper.DateFromString(dateAsString);

            //Assert

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void DateFromString_WithValidParameter_ShouldReturnValidDateTime()
        {
            //Arrange
            var dateAsString = "2021-12-02 11:06:32";
            var expected = new DateTime(2021, 12, 02);

            //Act

            var result = DateHelper.DateFromString(dateAsString);

            //Assert

            Assert.Equal(expected, result);

        }
    }
}
