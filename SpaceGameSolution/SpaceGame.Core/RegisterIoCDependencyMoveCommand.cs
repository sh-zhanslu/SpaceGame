using System.Collections.Generic;

namespace SpaceGame.Core
{
    public class RegisterIoCDependencyMoveCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Register("Commands.Move", args =>
            {
                // args[0] ожидается как игровой объект (IDictionary<string, object>)
                var gameObject = (IDictionary<string, object>)args[0];

                // Создаём адаптер IMovingObject через IoC 
                var movingObject = (IMovingObject)Ioc.Resolve("Adapters.IMovingObject", gameObject);
                return new MoveCommand(movingObject);
            });
        }
    }
}