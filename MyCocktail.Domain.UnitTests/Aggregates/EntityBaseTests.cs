using AutoFixture;
using MyCocktail.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyCocktail.Domain.UnitTests.Aggregates
{
    public class EntityBaseTests
    {

        private readonly Fixture _fixture;

        public EntityBaseTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void GetHascode_WhenDifferentEntityBase_ShouldReturnNonEqualHashCode()
        {
            //Arrange
            var entity1 = _fixture.Create<EntityBase>();
            var entity2 = _fixture.Create<EntityBase>();

            //Act
            var hashCode1 = entity1.GetHashCode();
            var hashCode2 = entity2.GetHashCode();

            //Assert
            Assert.False(entity1.Id == entity2.Id && entity1.Name == entity2.Name);
            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHascode_WhenSameEntityBasePropertiesValue_ShouldReturnEqualHashCode()
        {
            //Arrange
            var entity1 = _fixture.Create<EntityBase>();
            var entity2 = new EntityBase() { Id = entity1.Id, Name = entity1.Name };

            //Act
            var hashCode1 = entity1.GetHashCode();
            var hashCode2 = entity2.GetHashCode();

            //Assert
            Assert.True(entity1.Id == entity2.Id && entity1.Name == entity2.Name);
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void Equals_WhenDifferentEntityBase_ShouldReturnFalse()
        {
            //Arrange
            var entity1 = _fixture.Create<EntityBase>();
            var entity2 = _fixture.Create<EntityBase>();

            //Act
            var result1 = entity1.Equals(entity2);
            var result2 = entity2.Equals(entity1);

            //Assert
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void Equals_WhenSameEntityBasePropertiesValue_ShouldReturnTrue()
        {
            //Arrange
            var entity1 = _fixture.Create<EntityBase>();
            var entity2 = new EntityBase() { Id = entity1.Id, Name = entity1.Name };

            //Act
            var result1 = entity1.Equals(entity2);
            var result2 = entity2.Equals(entity1);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
        }
    }
}
