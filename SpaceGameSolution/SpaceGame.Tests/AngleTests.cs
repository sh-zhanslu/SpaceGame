using SpaceGame.Core;
using Xunit;

namespace SpaceGame.Tests
{
    public class AngleTests
    {
        public AngleTests()
        {
            // Устанавливаем общий знаменатель для всех тестов 
            Angle.Denominator = 8;
        }

        [Fact]
        public void Add_Angles_ReturnsReducedAngle()
        {
            var a = new Angle(5); // 5/8
            var b = new Angle(7); // 7/8
            var sum = a + b;      // 12/8 -> 4/8
            Assert.Equal(4, sum.Numerator);
        }

        [Fact]
        public void Equals_SameReducedAngles_ReturnsTrue()
        {
            var a = new Angle(15); // 15 mod 8 = 7
            var b = new Angle(23); // 23 mod 8 = 7
            Assert.True(a.Equals(b));
        }

        [Fact]
        public void EqualsOperator_SameReducedAngles_ReturnsTrue()
        {
            var a = new Angle(15);
            var b = new Angle(23);
            Assert.True(a == b);
        }

        [Fact]
        public void Equals_DifferentAngles_ReturnsFalse()
        {
            var a = new Angle(1);
            var b = new Angle(2);
            Assert.False(a.Equals(b));
        }

        [Fact]
        public void NotEqualsOperator_DifferentAngles_ReturnsTrue()
        {
            var a = new Angle(1);
            var b = new Angle(2);
            Assert.True(a != b);
        }

        [Fact]
        public void GetHashCode_ExistsAndConsistent()
        {
            var a = new Angle(5);
            var b = new Angle(5);
            Assert.NotEqual(0, a.GetHashCode());
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        // Дополнительный тест на синус/косинус 
        [Fact]
        public void SinCos_ReturnsCorrectValues()
        {
            var angle0 = new Angle(0);
            Assert.Equal(1.0, Angle.Cos(angle0), 5);
            Assert.Equal(0.0, Angle.Sin(angle0), 5);

            var angle90 = new Angle(2); // 2/8 = 1/4 оборота = 90°
            Assert.Equal(0.0, Angle.Cos(angle90), 5);
            Assert.Equal(1.0, Angle.Sin(angle90), 5);
        }
    }
}