using System;
using SpaceGame.Core;
using Xunit;

namespace SpaceGame.Tests
{
    public class VectorTests
    {
        [Fact]
        public void Add_Vectors_ReturnsSum()
        {
            var v1 = new Vector(1, -1, 2);
            var v2 = new Vector(-1, 1, -2);
            var result = v1 + v2;
            Assert.Equal(new Vector(0, 0, 0), result);
        }

        [Fact]
        public void Add_DifferentDimensions_ThrowsArgumentException()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2);
            Assert.Throws<ArgumentException>(() => v1 + v2);
        }

        [Fact]
        public void Add_DifferentDimensionsReverse_ThrowsArgumentException()
        {
            var v1 = new Vector(1, 2);
            var v2 = new Vector(1, 2, 3);
            Assert.Throws<ArgumentException>(() => v1 + v2);
        }

        [Fact]
        public void Equals_SameComponents_ReturnsTrue()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2, 3);
            Assert.True(v1.Equals(v2));
        }

        [Fact]
        public void EqualsOperator_SameComponents_ReturnsTrue()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2, 3);
            Assert.True(v1 == v2);
        }

        [Fact]
        public void Equals_DifferentComponents_ReturnsFalse()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2, 4);
            Assert.False(v1.Equals(v2));
        }

        [Fact]
        public void NotEqualsOperator_DifferentComponents_ReturnsTrue()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2, 4);
            Assert.True(v1 != v2);
        }

        [Fact]
        public void GetHashCode_ExistsAndIsConsistent()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(1, 2, 3);
            Assert.NotEqual(0, v1.GetHashCode());
            Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
        }
    }
}
