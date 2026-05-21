namespace SpaceGame.Core;

public interface IRotating
{
    Angle CurrentAngle { get; set; }
    Angle AngleVelocity { get; }
}

