using App;

namespace SpaceGame.Core
{
    public class Game
    {
        // Приватное поле, хранящее ссылку на приёмник команд(очередь)
        private readonly ICommandReceiver _receiver;

        // Конструктор – получает приёмник команд извне 
        public Game(ICommandReceiver receiver)
        {
            _receiver = receiver; // Сохраняем полученный приёмник в поле
        }

        // Метод, который обрабатывает все накопленные команды
        public void Update()
        {
            // Пока в приёмнике есть команды (свойство Count > 0)
            while (_receiver.Count > 0)
            {
                // Извлекаем команду из начала очереди (Take удаляет её)
                var cmd = _receiver.Take();
                // Выполняем команду
                cmd.Execute();
            }
        }
    }
}