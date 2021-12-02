using FluentAssertions;
using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates.Helper
{
    public class EnumerableHelperTests
    {
        public static List<Object> _collection = new List<Object>();

        public EnumerableHelperTests()
        {

        }

        [Fact]
        public void IsNullOrEmpty_WhithNullOrEmptyCollection_ShouldReturnTrue()
        {
            //Arange
            var collectionEmpty = new List<Object>();
            IEnumerable<Object> collectionNull = null;


            //Act
            var result1 = collectionEmpty.IsNullOrEmpty();
            var result2 = collectionNull.IsNullOrEmpty();

            //Assert
            Assert.True(result1);
            Assert.True(result2);
        }

        [Fact]
        public void IsNullOrEmpty_WhithNoNullOrEmptyCollection_ShouldReturnFalse()
        {
            //Arange
            var collectionEmpty = new List<Object>() { "toto" };

            //Act
            var result1 = collectionEmpty.IsNullOrEmpty();

            //Assert
            Assert.False(result1);
        }

        [Fact]
        public void PurgeNullValue_WithValidParameter_ShouldRemoveNullValueFromCollection()
        {
            //Arrange
            var dictToTest = new Dictionary<string, Object>();
            dictToTest.Add("key1", "toto");
            dictToTest.Add("key2", null);
            dictToTest.Add("key3", "toto2");
            dictToTest.Add("key4", null);

            //Act
            dictToTest.PurgeNullValue();

            //Assert
            dictToTest.Should().NotContain(entry => entry.Value == null);
        }

        [Fact]
        public void PurgeNullValue_WithNullParameter_ShouldNotThrowException()
        {
            //Arrange
            Dictionary<string, Object> dictToTest = null;

            //Act
            var ex = Record.Exception(() => dictToTest.PurgeNullValue());

            //Assert
            ex.Should().BeNull();
        }

        [Fact]
        public void PurgeEmptyAndNullValue_WithValidParameter_ShouldRemoveNullValueFromCollection()
        {
            //Arrange
            var dictToTest = new Dictionary<Object, string>();
            dictToTest.Add("key1", "toto");
            dictToTest.Add("key2", null);
            dictToTest.Add("key3", "toto2");
            dictToTest.Add("key4", "");

            //Act
            dictToTest.PurgeEmptyAndNullValue();

            //Assert
            dictToTest.Should().NotContain(entry => entry.Value == null);
            dictToTest.Should().NotContain(entry => entry.Value == "");
        }

        [Fact]
        public void PurgeEmptyAndNullValue_WithNullParameter_ShouldNotThrowException()
        {
            //Arrange
            Dictionary<Object, string> dictToTest = null;

            //Act
            var ex = Record.Exception(() => dictToTest.PurgeEmptyAndNullValue());

            //Assert
            ex.Should().BeNull();
        }

        [Fact]
        public void ContainsAllItems_WhithAllElementOfSecondCollectionAreIncludeInFirstCollection_ShouldReturnTrue()
        {
            //Arrange
            var collection1 = new List<string>() { "toto", "tata", "tati", "tonton" };
            var collection2 = new List<string>() { "toto", "tonton" };

            //Act
            var result = collection1.ContainsAllItems(collection2);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsAllItems_WhithNotAllElementOfSecondCollectionAreIncludeInFirstCollection_ShouldReturnTrue()
        {
            //Arrange
            var collection1 = new List<string>() { "toto", "tata", "tati", "tonton" };
            var collection2 = new List<string>() { "toto", "42" };

            //Act
            var result = collection1.ContainsAllItems(collection2);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ContainsAllItems_WhithCollectionTestedNull_ShouldThrowNullArgumentExecption()
        {
            //Arrange
            List<string> collection1 = null;
            var collection2 = new List<string>() { "toto", "42" };

            //Act
            Action act = () => collection1.ContainsAllItems(collection2);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ContainsAllItems_WhithNullCollectionSearchInSecondCollection_ShouldReturnTrue()
        {
            //Arrange
            var collection1 = new List<string>() { "toto", "tata", "tati", "tonton" };
            List<string> collection2 = null;

            //Act
            var result = collection1.ContainsAllItems(collection2);

            //Assert
            Assert.True(result);
        }

    }
}
