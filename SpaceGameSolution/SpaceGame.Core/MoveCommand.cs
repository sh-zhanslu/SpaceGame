using System;

namespace SpaceGame.Core
{
    public class MoveCommand : ICommand
    {
        private readonly IMovingObject _movingObject;

        public MoveCommand(IMovingObject movingObject)
        {
            _movingObject = movingObject ?? throw new ArgumentNullException(nameof(movingObject));
        }

        public void Execute()
        {
            if (_movingObject.Position == null)
                throw new InvalidOperationException("Не удаётся получить позицию");
            if (_movingObject.Velocity == null)
                throw new InvalidOperationException("Не удаётся набрать скорость");

            try
            {
                _movingObject.Position = _movingObject.Position + _movingObject.Velocity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Не удаётся установить новую позицию", ex);
            }
        }
    }
}