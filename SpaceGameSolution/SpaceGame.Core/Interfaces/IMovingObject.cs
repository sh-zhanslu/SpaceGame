using SpaceGame.Core;

namespace SpaceGame.Core
{
    public interface IMovingObject
    {
        Vectors Position { get; set; }
        Vectors Velocity { get; }
    }
}
