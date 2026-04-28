using System.Collections.Generic;
using Moq;
using SpaceGame.Core;
using Xunit;

namespace SpaceGame.Tests
{
    public class RegisterIoCDependencyMoveCommandTests
    {
        [Fact]
        public void Execute_RegistersDependency_CanResolveMoveCommand()
        {
            // Arrange: создаём мок для IMovingObject, который будет возвращён адаптером
            var mockMovingObject = new Mock<IMovingObject>().Object;

            // Регистрируем заглушку для "Adapters.IMovingObject"
            Ioc.Register("Adapters.IMovingObject", args =>
            {
                // args[0] - это игровой объект (словарь)
                return mockMovingObject;
            });

            // Создаём и выполняем команду регистрации
            var registerCommand = new RegisterIoCDependencyMoveCommand();
            registerCommand.Execute();

            // Act: разрешаем "Commands.Move" с фиктивным игровым объектом
            var fakeGameObject = new Dictionary<string, object>();
            var resolvedCommand = Ioc.Resolve("Commands.Move", fakeGameObject);

            // Assert
            Assert.IsType<MoveCommand>(resolvedCommand);
        }
    }
}
