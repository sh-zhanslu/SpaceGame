using SpaceGame.Core;

namespace SpaceGame.Core
{
    public interface IMovingObject
    {
        Vector Position { get; set; }
        Vector Velocity { get; }
    }
}
