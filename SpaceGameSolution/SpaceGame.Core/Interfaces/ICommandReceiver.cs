namespace SpaceGame.Core;

public interface ICommandReceiver
{
    void Receive(ICommand command);
    ICommand Take();      
    int Count { get; }
    void Put(ICommand command);
}