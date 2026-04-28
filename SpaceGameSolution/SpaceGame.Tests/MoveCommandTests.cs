using System;
using Moq;
using SpaceGame.Core;
using Xunit;

namespace SpaceGame.Tests
{
    public class MoveCommandTests
    {
        [Fact]
        public void Move_ObjectWithPositionAndVelocity_UpdatesPositionCorrectly()
        {
            // Arrange
            var mock = new Mock<IMovingObject>();
            mock.SetupProperty(o => o.Position, new Vector(12, 5));
            mock.Setup(o => o.Velocity).Returns(new Vector(-4, 1));
            var command = new MoveCommand(mock.Object);

            // Act
            command.Execute();

            // Assert
            Assert.Equal(new Vector(8, 6), mock.Object.Position);
        }

        [Fact]
        public void Move_WhenPositionIsNull_ThrowsInvalidOperationException()
        {
            var mock = new Mock<IMovingObject>();
            mock.Setup(o => o.Position).Returns((Vector)null!);
            mock.Setup(o => o.Velocity).Returns(new Vector(1, 1));
            var command = new MoveCommand(mock.Object);

            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void Move_WhenVelocityIsNull_ThrowsInvalidOperationException()
        {
            var mock = new Mock<IMovingObject>();
            mock.Setup(o => o.Position).Returns(new Vector(0, 0));
            mock.Setup(o => o.Velocity).Returns((Vector)null!);
            var command = new MoveCommand(mock.Object);

            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void Move_WhenSettingPositionThrows_ThrowsInvalidOperationException()
        {
            var mock = new Mock<IMovingObject>();
            mock.Setup(o => o.Position).Returns(new Vector(0, 0));
            mock.Setup(o => o.Velocity).Returns(new Vector(1, 1));
            mock.SetupSet(o => o.Position = It.IsAny<Vector>()).Throws<Exception>();
            var command = new MoveCommand(mock.Object);

            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }
    }
}
