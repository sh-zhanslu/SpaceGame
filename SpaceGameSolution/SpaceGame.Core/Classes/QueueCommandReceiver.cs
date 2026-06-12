using System.Collections.Generic;
using App;

namespace SpaceGame.Core
{
    // Класс, реализующий ICommandReceiver – приёмник команд, хранящий их в очереди
    public class QueueCommandReceiver : ICommandReceiver
    {
        // Приватное поле – очередь команд (неизменяемая ссылка, но содержимое можно менять)
        private readonly Queue<ICommand> _queue = new Queue<ICommand>();

        // добавить команду в конец очереди
        public void Put(ICommand cmd) => _queue.Enqueue(cmd);

        // извлечь команду из начала очереди (удаляет её)
        public ICommand Take() => _queue.Dequeue();

        // Свойство, возвращающее текущее количество команд в очереди
        public int Count => _queue.Count;

        // Реализация метода интерфейса ICommandReceiver: принять команду и положить её в очередь
        public void Receive(ICommand command)
        {
            Put(command);
        }
    }
}