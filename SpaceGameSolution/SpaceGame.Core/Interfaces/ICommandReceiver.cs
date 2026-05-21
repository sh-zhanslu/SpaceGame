namespace SpaceGame.Core;

public interface ICommandReceiver
{
    void Receive(ICommand command);
}