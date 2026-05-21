namespace SpaceGame.Core
{
    public class MoveCommand : ICommand
    {
        private readonly IMovingObject moving;
        public MoveCommand(IMovingObject moving)
        {
            this.moving = moving;
        }
        public void Execute()
        {
            moving.Position += moving.Velocity;
        }
    }
}