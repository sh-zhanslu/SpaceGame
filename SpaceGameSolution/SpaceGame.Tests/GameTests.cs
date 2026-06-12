using SpaceGame.Core;
using App;
using Xunit;

namespace SpaceGame.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameUpdate_ShouldExecuteAllCommandsInReceiver()
        {
            // 1. Создаём и выполняем команду регистрации всех зависимостей, связанных с Game
            var registerGame = new RegisterIoCDependencyGame();
            registerGame.Execute(); // Внутри регистрирует "Game.Receiver" и "Game.Instance"

            // 2. Получаем зарегистрированный экземпляр Game через IoC
            var game = Ioc.Resolve<Game>("Game.Instance");

            // 3. Получаем зарегистрированный приёмник команд (очередь) через IoC
            var receiver = Ioc.Resolve<ICommandReceiver>("Game.Receiver");

            // 4. Создаём флаг, который станет true, если тестовая команда выполнится
            bool wasExecuted = false;

            // 5. Создаём тестовую команду, которая при выполнении установит флаг в true
            var testCommand = new ActionCommand(() => wasExecuted = true);

            // 6. Помещаем тестовую команду в приёмник (очередь)
            receiver.Put(testCommand);

            // 7. Вызываем обновление игры – Game должен забрать команду из очереди и выполнить её
            game.Update();

            // 8. Проверяем, что команда действительно выполнилась (флаг стал true)
            Assert.True(wasExecuted);
        }
    }
}
